#is like A virtual OS and like our CI/CD build Agent, need to specify
# from mcr.microsoft.com/dotnet/sdk:latest as build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
# copy csproj and restore as distinct layers
COPY *.sln .
COPY StoreAppApi/*.csproj StoreAppApi/
COPY StoreBL/*.csproj StoreBL/
COPY StoreDL/*.csproj StoreDL/
COPY StoreModel/*.csproj StoreModel/
COPY UnitTest/*.csproj UnitTest/

COPY . ./
RUN dotnet publish -c Releas -o publish
## all above for building and publishing application

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime

WORKDIR /app
COPY --from=build/app/publish ./
cmd ["dotnet", "StoreAppApi.dll"]

expose 80
## docker build  -t  from the terminal 
#docker run -d -p 5000:80 -t [nameOfImage]

## for the documentation https://hub.docker.com/_/microsoft-dotnet-sdk/

