param location string = resourceGroup().location
param staticWebAppApiLocation string

param nameSuffix string
param appServicePlanSku string
param dbSkuName string
param dbSkuTierName string

param sqlServerAdLogin string
param sqlServerAdSid string
param sqlServerAdTenantId string

param staticWebAppSkuTier string
param staticWebAppSkuName string
param branchName string
param repositoryUrl string
param repositoryToken string

param storageAccountSkuName string

var logWorkspaceName = 'log-korepetynder-${nameSuffix}'
var webAppName = 'app-korepetynder-${nameSuffix}'
var appServicePlanName = 'plan-korepetynder-${nameSuffix}'
var sqlServerName = 'sql-korepetynder-${nameSuffix}'
var dbName = 'sqldb-korepetynder-${nameSuffix}'
var webAppInsightsName = 'appi-app-korepetynder-${nameSuffix}'
var staticWebAppName = 'stapp-korepetynder-${nameSuffix}'
var storageAccountName = 'stkorepetynder${nameSuffix}'


module loggingModule 'logging.bicep' = {
  name: 'loggingDeploy'
  params: {
    location: location
    logWorkspaceName: logWorkspaceName
  }
}

module webAppModule 'webApp.bicep' = {
  name: 'webAppDeploy'
  params: {
    location: location
    webAppName: webAppName
    logWorkspaceId: loggingModule.outputs.logWorkspaceId
    appInsightsName: webAppInsightsName
    sqlDbName: dbName
    sqlServerName: sqlServerName
    appServicePlanName: appServicePlanName
    appServicePlanSku: appServicePlanSku
  }
}

module staticWebAppDeploy 'staticWebApp.bicep' = {
  name: 'staticWebAppDeploy'
  params: {
    location: staticWebAppApiLocation
    staticWebAppName: staticWebAppName
    staticWebAppSkuName: staticWebAppSkuName
    staticWebAppSkuTier: staticWebAppSkuTier
    branchName: branchName
    repositoryUrl: repositoryUrl
    repositoryToken: repositoryToken
  }
}

module databaseModule 'database.bicep' = {
  name: 'databaseDeploy'
  params: {
    location: location
    sqlServerName: sqlServerName
    dbName: dbName
    dbSkuName: dbSkuName
    dbSkuTierName: dbSkuTierName
    sqlServerAdLogin: sqlServerAdLogin
    sqlServerAdSid: sqlServerAdSid
    sqlServerAdTenantId: sqlServerAdTenantId
  }
}

module storageModule 'storage.bicep' = {
  name: 'storageDeploy'
  params: {
    location: location
    storageAccountName: storageAccountName
    storageAccountSkuName: storageAccountSkuName
  }
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2020-08-01-preview' = {
  name: guid(resourceGroup().id)
  properties: {
    roleDefinitionId: '/subscriptions/${subscription().subscriptionId}/providers/Microsoft.Authorization/roleDefinitions/6d8ee4ec-f05a-4a1d-8b00-a9b17e38b437'
    principalId: sqlServerAdSid
  }
}
