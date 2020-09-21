rem Stop containers

call ./stop.cmd

rem Remove containers

docker rm docker_postoffice.mongo_1
docker rm docker_postoffice.redis_1
