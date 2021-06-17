# load external docker images into the local repository
# to prevent their reload
docker pull mcr.microsoft.com/dotnet/core/sdk:3.1.300

# build our own docker images and services
docker-compose -f docker-compose.yml -f docker-compose.development.yml build
