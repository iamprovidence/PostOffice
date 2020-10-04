# PostOffice

<img alt="AppVeyor" src="https://ci.appveyor.com/api/projects/status/jwba6m4ilyksdf5w?svg=true">

<img align="center" src="/docs/images/logo.png" width="350"/>

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

## Environment

Before starting verify you have all required tools installed and configured:

- [Docker](https://www.docker.com/get-started)
- [.NET Core SDK v3.1](https://dotnet.microsoft.com/download)

## Configure

Verify that you have correct values for connection strings. 

> **Note services use IP address  of your ``virtual machine``. To find it out type ``docker-machine ip`` in console**

## Build

- To build client write in console ``npm run install``
- To build a project open ``PostOffice.sln`` in ``VisualStudio`` and press ``ctrl + shift + B``

> Alternative way would be to open project folder in ``console`` and write ``dotnet build PostOffice.sln``

## Run

- Start all required services. Run ``Infrastructure/runall.cmd``
- Run API server in ``VisualStudio`` or with console ``dotnet run --project PostOffice.API``
- Run Angular server with console ``npm run start``
