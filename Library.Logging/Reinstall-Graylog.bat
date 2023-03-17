@ECHO OFF
docker-compose -f ./GrayLog/docker-compose.yaml -p local_graylog stop
docker-compose -f ./GrayLog/docker-compose.yaml -p local_graylog rm -f
docker volume prune -f
docker-compose -f ./GrayLog/docker-compose.yaml -p local_graylog up -d --build
pause
