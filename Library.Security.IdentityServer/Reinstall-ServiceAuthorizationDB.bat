@ECHO OFF
docker-compose -f ./postgres/dev/docker-compose.yaml -p local_service_authorization_db stop
docker-compose -f ./postgres/dev/docker-compose.yaml -p local_service_authorization_db rm -f
docker volume prune -f
docker-compose -f ./postgres/dev/docker-compose.yaml -p local_service_authorization_db up -d --build
pause
