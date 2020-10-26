# PostOffice

<img alt="AppVeyor" src="https://ci.appveyor.com/api/projects/status/jwba6m4ilyksdf5w?svg=true">

<img align="center" src="/docs/images/logo.png" width="350"/>

## Wiki

To get more information on this project check our [wiki](https://github.com/iamprovidence/PostOffice/wiki).


## Git

Branch naming convention:
```diff
feature/task-name-{card-id}
fix/task-name-{card-id}
```

## Tools

- [SignalR](https://dotnet.microsoft.com/apps/aspnet/real-time)
- [MongoDB](https://www.mongodb.com/)
- [Redis](https://redis.io/)
- [Azure Functions](https://azure.microsoft.com/ru-ru/downloads/)

## Environment

Before starting verify you have all required tools installed and configured:

- [Docker](https://www.docker.com/get-started)
- [.NET Core SDK v3.1](https://dotnet.microsoft.com/download)
- [Azure SDK](https://azure.microsoft.com/ru-ru/downloads/)

## Machine setup

Install this tools for better developing experience:

- [MongoDB Compass](https://www.mongodb.com/products/compass)
- [Azure storage explorer](https://azure.microsoft.com/da-dk/features/storage-explorer/)
- [Azure Functions CLI](https://www.npmjs.com/package/azure-functions-core-tools)
- [Redis CLI](https://github.com/microsoftarchive/redis/releases/tag/win-3.2.100)

## Configure

#### SwitchStartupProject

If you are using ``VisualStudio`` you can download [this extensions](https://heptapod.host/thirteen/switchstartupproject) that simplifies selecting multiple projects.

To use it, you need to setup its configuration file - ``PostOffice.sln.startup.json``.

#### .env

The ``.env`` file is used to define environment variables that will be applied before starting any project in the solution. 
It is a file tool to override values for any common configuration settings like connection strings.

#### setup.cmd

The ``.env`` and ``PostOffice.sln.startup.json`` file is not stored in git repository and you have to create them on your own. 
Use ``.env.sample`` and ``PostOffice.sln.startup_sample.json`` files correspondingly as a samples.

Use ``scripts/setup.cmd`` to create initial version of ``.env`` and ``PostOffice.sln.startup.json``. 
The script is basically just copying the sample files.

Verify that you have correct values for connection strings. 
You are allowed to do any changes in ``.env`` and ``PostOffice.sln.startup.json``. 

> **Note services use IP address  of your ``virtual machine``. To find it out type ``docker-machine ip`` in console**

## Restore dependencies

Use ``scripts/init.cmd`` to restore dependencies on client and server sides.

## Build

Use ``scripts/build.cmd`` to build all projects in solution.

> To build solution for developing purpose open ``PostOffice.sln`` in ``VisualStudio`` and press ``ctrl + shift + B``

## Run

- Start all required services. Run ``Infrastructure/runall.cmd``
- Start all required projects. Run ``script/runall.cmd``

## Run develop

- Start all required services. Run ``Infrastructure/runall.cmd``
- Run API server in ``VisualStudio`` or with console ``dotnet run --project PostOffice.API``
- Run SmsSender service in ``VisualStudio`` or with console ``func start``
- Run Angular server with console ``npm run start``
