resource "azurerm_app_service_plan" "web-app" {
  name                = "WebAppServicePlan"
  location            = azurerm_resource_group.post-office-rg.location
  resource_group_name = azurerm_resource_group.post-office-rg.name
  kind                = "app"
  reserved            = true

  sku {
    tier = "Basic"
    size = "B1"
  }
}

resource "azurerm_app_service_plan" "functions" {
  name                = "FunctionsServicePlan"
  location            = local.region
  location            = azurerm_resource_group.post-office-rg.location
  resource_group_name = azurerm_resource_group.post-office-rg.name
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}
