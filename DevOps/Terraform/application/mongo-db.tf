resource "azurerm_cosmosdb_mongo_database" "post-office-mongo-db" {
  name                = "mongo-db"
  location            = azurerm_resource_group.post-office-rg.location
  resource_group_name = azurerm_resource_group.post-office-rg.name
  throughput          = 400
}
