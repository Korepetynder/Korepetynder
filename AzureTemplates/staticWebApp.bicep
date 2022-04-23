param location string = resourceGroup().location

param staticWebAppName string
param staticWebAppSkuTier string
param staticWebAppSkuName string
param branchName string
param repositoryUrl string
param repositoryToken string

resource staticWebApp 'Microsoft.Web/staticSites@2021-03-01' = {
  name: staticWebAppName
  location: location
  sku: {
    tier: staticWebAppSkuTier
    name: staticWebAppSkuName
  }
  properties: {
    branch: branchName
    repositoryUrl: repositoryUrl
    repositoryToken: repositoryToken
    buildProperties: {
      appLocation: 'korepetynder-spa/'
      outputLocation: 'dist/korepetynder-spa/'
    }
  }
}
