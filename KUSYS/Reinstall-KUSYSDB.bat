@ECHO OFF
docker-compose -f ./postgres/dev/docker-compose.yaml -p kusys_db stop
docker-compose -f ./postgres/dev/docker-compose.yaml -p kusys_db rm -f
docker volume prune -f
docker-compose -f ./postgres/dev/docker-compose.yaml -p kusys_db up -d --build
pause