# resource groups

variable "resource_group_name" {
  default = "post-office-rg"
  description = "The name of the resource group"
}

variable "resource_group_location" {
  default = "westus"
  description = "The location of the resource group"
}
