rem Stop and Remove containers

call ./clearcache.cmd

rem Remove images

docker rmi postoffice_mongo:latest
docker rmi postoffice_redis:latest
