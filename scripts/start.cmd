start cmd.exe /c "cd .. & dotnet run --project PostOffice.API"
start cmd.exe /c "cd .. & dotnet run --project PostOffice.Angular"

start cmd.exe /c "cd .. & cd PostOffice.SmsSender & func start"
