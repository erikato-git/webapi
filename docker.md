### Run postgres
appsettings.json -> "DefaultConnection": "Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=webapi"
docker run -d -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres postgres:latest

### Run Webapi from container and access postgres also running in a container
appsettings.json -> "DefaultConnection": "Server=host.docker.internal; Port=5432; User Id=postgres; Password=postgres; Database=webapi"
### Build webapi-image and run container
docker build -t webapi .
docker run -p 5165:80 -d webapi


### Følg guiden fra dockerhubs officielle documentation:
https://docs.docker.com/language/dotnet/develop/
1. Create volume to persist data
docker volume create postgres-data

2. Create a network so two containers can talk to each other
docker network create postgres-net

3. Run a postgres-container connected to our network
docker run --rm -d -v postgres-data:/var/lib/postgresql/data \
  --network postgres-net \
  --name db \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=example \
  postgres

4. Make sure postgres is installed and make the necessary connections:

*.csproj
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

Program.cs
builder.Services.AddDbContext<SchoolContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("SchoolContext")));

appsettings.json
"ConnectionStrings": {
       "SchoolContext": "Host=db;Database=my_db;Username=postgres;Password=example"
   }

5. Make sure the database is created during the build of the ASP.Net Core application

Program.cs
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Add 10 seconds delay to ensure the db server is up to accept connections
        // This won't be needed in a real-world application.
        System.Threading.Thread.Sleep(10000);
        var context = services.GetRequiredService<DataContext>();
        var created = context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

6. Build docker-file for the ASP.NET Core application:

docker build --tag dotnet-docker .

7. Run the ASP.NET Core application on the same network as the postgres-container

docker run \
  --rm -d \
  --network postgres-net \
  --name dotnet-app \
  -p 5000:80 \
  dotnet-docker


