# Run postgres
# appsettings.json -> "DefaultConnection": "Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=webapi"
docker run -d -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres postgres:latest
