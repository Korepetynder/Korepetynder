param location string = resourceGroup().location

param sqlServerName string

param sqlServerAdLogin string
param sqlServerAdSid string
param sqlServerAdTenantId string

param dbName string
param dbSkuName string
param dbSkuTierName string

resource sqlServer 'Microsoft.Sql/servers@2021-11-01-preview' = {
  name: sqlServerName
  location: location
  properties: {
    publicNetworkAccess: 'Enabled'
    minimalTlsVersion: '1.2'
    administrators: {
      administratorType: 'ActiveDirectory'
      login: sqlServerAdLogin
      sid: sqlServerAdSid
      tenantId: sqlServerAdTenantId
      azureADOnlyAuthentication: true
    }
  }

  resource sqlServerDatabase 'databases' = {
    name: dbName
    location: location
    sku: {
      name: dbSkuName
      tier: dbSkuTierName
    }
  }

  resource azureFirewallRule 'firewallRules' = {
    name: 'AzureFirewallRule'
    properties: {
      startIpAddress: '0.0.0.0'
      endIpAddress: '0.0.0.0'
    }
  }
}
