rem Stop containers

call ./stop.cmd

rem Remove containers

docker rm infrastructure_postoffice.mongo_1
docker rm infrastructure_postoffice.redis_1
