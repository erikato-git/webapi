# Run postgres
# appsettings.json -> "DefaultConnection": "Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=webapi"
docker run -d -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres postgres:latest

# Run Webapi from container and access postgres also running in a container
# appsettings.json -> "DefaultConnection": "Server=host.docker.internal; Port=5432; User Id=postgres; Password=postgres; Database=webapi"
# Build webapi-image and run container
docker build -t webapi .
docker run -p 5165:80 -d webapi
