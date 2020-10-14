for /R %~dp0.. %%f in (*.csproj) do dotnet build %%f
npm run build