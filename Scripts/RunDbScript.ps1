# In order to avoid AAD lookup done by "CREATE USER FROM EXTERNAL PROVIDER",
# we are creating users in a different way, which requires SID (Security Identification Number).
# This function converts the given AD Application ID to SID.
# https://erikej.github.io/sqlserver/2021/01/25/azure-sql-advanced-deployment-part3.html
function ConvertTo-Sid {
    param (
        [string]$AppId
    )
    [guid]$guid = [System.Guid]::Parse($AppId)
    foreach ($byte in $guid.ToByteArray()) {
        $byteGuid += [System.String]::Format("{0:X2}", $byte)
    }
    return "0x" + $byteGuid
}

if (-not (Get-Module -ListAvailable -Name SqlServer)) {
    Install-Module -Name SQLServer -Force
}

$migrationsPath = Join-Path -Path "migrations-output" -ChildPath "migrations.sql"

$securePassword = ConvertTo-SecureString -String "$env:APP_PASSWORD" -AsPlainText -Force
$psCredential = New-Object -TypeName System.Management.Automation.PSCredential("$env:APP_ID", $securePassword)
Connect-AzAccount -ServicePrincipal -Credential $psCredential -Tenant "$env:TENANT_ID"

$webAppIdentityName = "app-korepetynder-$env:SUFFIX"
$webApp = Get-AzWebApp -Name "$webAppIdentityName" -ResourceGroupName "Korepetynder-$env:SUFFIX"
$webAppIdentityId = $webApp.Identity.PrincipalId

$graphAuthResponse = Invoke-RestMethod -Method Post `
    -Uri "https://login.microsoftonline.com/$env:TENANT_ID/oauth2/v2.0/token" `
    -Body @{ scope = "https://graph.microsoft.com/.default"; grant_type = "client_credentials"; client_id = "$env:APP_ID"; client_secret = "$env:APP_PASSWORD" } `
    -ContentType "application/x-www-form-urlencoded"
$graphAccessToken = $graphAuthResponse.access_token

$webAppServicePrincipalResponse = Invoke-RestMethod -Method Get `
    -Uri "https://graph.microsoft.com/v1.0/servicePrincipals/$webAppIdentityId" `
    -Headers @{ Authorization = "Bearer $graphAccessToken" }
$webAppId = $webAppServicePrincipalResponse.appId
$webAppSid = ConvertTo-Sid -AppId $webAppId

New-AzSqlServerFirewallRule -FirewallRuleName "GitLabRule" -ServerName "sql-korepetynder-$env:SUFFIX" `
    -ResourceGroupName "Korepetynder-$env:SUFFIX" -StartIpAddress "0.0.0.0" -EndIpAddress "255.255.255.255"

# https://docs.microsoft.com/en-us/powershell/module/sqlserver/invoke-sqlcmd?view=sqlserver-ps#example-12--connect-to-azure-sql-database--or-managed-instance--using-a-service-principal
$request = Invoke-RestMethod -Method Post `
    -Uri "https://login.microsoftonline.com/$env:TENANT_ID/oauth2/token" `
    -Body @{ resource = "https://database.windows.net/"; grant_type = "client_credentials"; client_id = "$env:APP_ID"; client_secret = "$env:APP_PASSWORD" } `
    -ContentType "application/x-www-form-urlencoded"
$accessToken = $request.access_token

$initQueryQuery = "IF NOT EXISTS (SELECT [name] FROM [sys].[database_principals] WHERE [type] = 'E' AND [name] = '$webAppIdentityName')
BEGIN
    CREATE USER [$webAppIdentityName] WITH DEFAULT_SCHEMA=[dbo], SID = $webAppSid, TYPE = E
END

ALTER ROLE db_datareader ADD MEMBER [$webAppIdentityName]
ALTER ROLE db_datawriter ADD MEMBER [$webAppIdentityName]
ALTER ROLE db_ddladmin ADD MEMBER [$webAppIdentityName]
GO
"
# Apply initialization script
Invoke-Sqlcmd -ServerInstance "sql-korepetynder-$env:SUFFIX.database.windows.net" -Database "sqldb-korepetynder-$env:SUFFIX" `
    -AccessToken $accessToken -Query $initQueryQuery

# Apply EF Core migrations
Invoke-Sqlcmd -ServerInstance "sql-korepetynder-$env:SUFFIX.database.windows.net" -Database "sqldb-korepetynder-$env:SUFFIX" `
    -AccessToken $accessToken -InputFile $migrationsPath

Remove-AzSqlServerFirewallRule -FirewallRuleName "GitLabRule" -ServerName "sql-korepetynder-$env:SUFFIX" `
    -ResourceGroupName "Korepetynder-$env:SUFFIX"
