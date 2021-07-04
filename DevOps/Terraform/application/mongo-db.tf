resource "azurerm_redis_cache" "post-office-redis" {
  name                = "redis"
  location            = azurerm_resource_group.post-office-rg.location
  resource_group_name = azurerm_resource_group.post-office-rg.name
  capacity            = 2
  family              = "C"
  sku_name            = "Standard"
  enable_non_ssl_port = false
  minimum_tls_version = "1.2"

  redis_configuration {
  }
}

resource "azurerm_cosmosdb_mongo_database" "post-office-mongo-db" {
  name                = "mongo-db"
  location            = azurerm_resource_group.post-office-rg.location
  resource_group_name = azurerm_resource_group.post-office-rg.name
  throughput          = 400
}