resource "azurerm_resource_group" "post-office-rg" {
  name     = var.resource_group_name
  location = var.resource_group_location
  tags     = local.default_tags
}
