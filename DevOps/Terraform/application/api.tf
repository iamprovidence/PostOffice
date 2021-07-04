resource "azurerm_app_service" "post-office-api" {
  name                = "PostOfficeApi"
  location            = azurerm_resource_group.post-office-rg.location
  resource_group_name = azurerm_resource_group.post-office-rg.name
  app_service_plan_id = azurerm_app_service_plan.web-app.id
}