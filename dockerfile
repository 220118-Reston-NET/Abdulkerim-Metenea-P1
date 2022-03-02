
FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /app
# copy csproj and restore as distinct layers
COPY *.sln .
COPY StoreAppApi/*.csproj StoreAppApi/
COPY storeBL/*.csproj storeBL/
COPY storeDL/*.csproj storeDL/
COPY storeModel/*.csproj storeModel/
COPY UnitTest/*.csproj UnitTest/

COPY . ./
RUN dotnet publish -c Release -o publish
# all above for building and publishing application

FROM mcr.microsoft.com/dotnet/sdk:latest AS runtime

WORKDIR /app
COPY --from=build app/publish ./
cmd ["dotnet", "StoreAppApi.dll"]

expose 80
## docker build  -t  from the terminal 
#docker run -d -p 5000:80 -t [nameOfImage]

## for the documentation https://hub.docker.com/_/microsoft-dotnet-sdk/

