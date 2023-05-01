[![Continuous Integration (not deployment)](https://github.com/erikato-git/webapi/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/erikato-git/webapi/actions/workflows/ci-cd.yml)

# Webapi - Template for web-applications build in ASP.NET Core and React

####Run application in VS Code:
#####1. Run in development mode:
1.1.1 Run postgres docker container:
Make sure docker/docker-desktop is install on your computer > open up terminal and type:
```
docker run -d -p 5432:5432 -e POSTGRES_USERNAME=postgres -e POSTGRES_PASSWORD=example postgres:latest
```
make sure the docker container is running by typing ´´´docker ps´´´. 

1.1.2 Navigate webapi/webapi (same folder as 'webapi.csproj') in terminal and type:
```
dotnet run
```
then open up new terminal and navigate webapi/clientapp in terminal and type:
```
npm install
```
then
```
npm start
```
Open up a browser and navigate to url: http://localhost:3000/


#####2. Run docker-compose:
Navigate to same folder as docker-compose.yml in terminal > type:
```
docker-compose up --build -d
```
Open up browser and navigate to url: http://localhost:5165/

Template covers:
Server:
-   Simple example 
Client:
Tests:
