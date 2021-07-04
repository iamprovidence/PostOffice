# Terraform

Terraform is an infrastructure as code software tool that enables you to create, change, destroy infrastructure.

## SetUp

1. [Download Terraform](https://learn.hashicorp.com/terraform/azure/install_az)
2. [Setup PATH](https://stackoverflow.com/questions/1618280/where-can-i-set-path-to-make-exe-on-windows)
3. [Download and install Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?view=azure-cli-latest)

## Run

Start by logging in using
```
az login
```
Then you can run commands like
```
terraform plan
```

Or

```
terraform apply
```

## Infrastructure overview

<p align="center">
  <img align="center" src="/docs/images/deployment-diagram.png"/>  
</p>
