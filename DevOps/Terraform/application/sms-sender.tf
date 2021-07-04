resource "azurerm_function_app" "sms-sender" {
  name                = "SmsSender"
  location            = azurerm_resource_group.post-office-rg.location
  resource_group_name = azurerm_resource_group.post-office-rg.name
  app_service_plan_id = azurerm_app_service_plan.functions.id
  default_tags        = local.default_tags
  
  os_type                    = "linux"
  version                    = "~3"
}