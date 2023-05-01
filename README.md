[![Continuous Integration (not deployment)](https://github.com/erikato-git/webapi/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/erikato-git/webapi/actions/workflows/ci-cd.yml)

# Webapi - Template for projects build in ASP.NET Core and React

### Run application in VS Code:
#### Run in development mode:
Activate database. Run postgres in a docker container. <br>
Make sure docker/docker-desktop is install on your computer > open up terminal and type:
```
docker run -d -p 5432:5432 -e POSTGRES_USERNAME=postgres -e POSTGRES_PASSWORD=example postgres:latest
```
make sure the docker container is running by typing ```docker ps``` <br>

Activate server. Navigate terminal to 'webapi/webapi' (same folder as 'webapi.csproj') and type:
```
dotnet run
```
Activate client. Open up a new terminal and navigate to 'webapi/clientapp' in terminal and type:
```
npm install
```
to install all dependencies from package.json and then type:
```
npm start
```
Open up a browser and navigate to url: http://localhost:3000/

<br>

#### Run in production mode with docker-compose:
Navigate to same folder as docker-compose.yml in terminal (the root of the project) and type:
```
docker-compose up --build -d
```
Open up browser and navigate to url: http://localhost:5165/

<br>

### Template overview:
Server:
- Simple example of interactions between Repository, Interface and Controller.
- DataContext-class with init data configured with dependency injection Program.cs.
- AddDbContext configured in three different modes in postgres with dependency injection in Program.cs (can easily be changed to mssql): 
  - 1. Development mode: Configured to one connection-string used for developement.
  - 2. Production mode: Configured to another connection-string used for production.
  - 3. Deployment mode: Configured to use connection-string provided by the host eg. Heroku or flyio.
- Cors-policy: Server is configured to listen to client on http://localhost:3000
- Security is configured for http-header-requests and makes an 'A' on https://securityheaders.com/
- FallbackController configures the default-landing page when running the application the index.html of the build-version of client which is to be find in 'wwwwroot'.
- Continuous-Integration: configured to notice if errors occur during build and tests when pusing to main branch on github. 

Client:
- Environment variables: different environment variables are configured to the build process of the client. For deployment 'http://localhost:5165/' in the url needs to be removed when client received requests from the server.
- Builds: Besides environmental variables the different build modes are configured to make a production build of the client-app deployed on backend in folder 'wwwroot' and the deployment mode is sat to not generate source map which will expose client source code in the browser. The two build modes can be activated in the terminal by typing ```npm run build:dev``` or ```npm run build:deploy```  
- Simple example of the receiving a request from the server (based on the running environment)

Tests:
- Simple test on a server-method call. Checking for status-code and data-type.
- Fake database based on UseInMemoryDatabase and the DataContext-class on the server.
- Fake Controller-class using the actual repository-class on the server provided with the fake database for testing. Automapper can be configured (optional). 

<br>




