param location string = resourceGroup().location

param appServicePlanName string
param appServicePlanSku string

param webAppName string
param appInsightsName string
param logWorkspaceId string
param sqlServerName string
param sqlDbName string

var dbConnectionString = 'Server=tcp:${sqlServerName}${environment().suffixes.sqlServerHostname},1433;Initial Catalog=${sqlDbName};Authentication=Active Directory Managed Identity;'

resource appServicePlan 'Microsoft.Web/serverfarms@2021-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: appServicePlanSku
    capacity: 1
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logWorkspaceId
  }
}

resource webApp 'Microsoft.Web/sites@2021-03-01' = {
  name: webAppName
  location: location
  kind: 'app,linux'
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    clientAffinityEnabled: false
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|6.0'
      alwaysOn: true
      http20Enabled: true
      minTlsVersion: '1.2'
    }
  }
  identity: {
    type: 'SystemAssigned'
  }

  resource webAppConfig 'config' = {
    name: 'appsettings'
    properties: {
      APPLICATIONINSIGHTS_CONNECTION_STRING: applicationInsights.properties.ConnectionString
      WEBSITE_RUN_FROM_PACKAGE: '1'
      ASPNETCORE_ENVIRONMENT: 'Production'
    }
  }

  resource webAppLogs 'config' = {
    name: 'logs'
    properties: {
      httpLogs: {
        fileSystem: {
          enabled: true
          retentionInMb: 50
          retentionInDays: 30
        }
      }
    }
  }

  resource webAppConnectionStrings 'config' = {
    name: 'connectionstrings'
    properties: {
      Korepetynder: {
        type: 'SQLAzure'
        value: dbConnectionString
      }
    }
  }
}
