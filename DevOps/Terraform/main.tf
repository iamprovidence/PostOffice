# TODO: setup storage account for storing tfstate

provider "azurerm" {
  features {}
}

locals {
  default_tags  = {
    CreationType  = "Terraform"
  }
}
