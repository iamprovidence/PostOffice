call ./setup.cmd

rem install dependencies
for /R %~dp0.. %%f in (*.csproj) do dotnet restore %%f
npm run install
