FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

#copying all necessary files for dotnet restore
COPY ./WebApp.sln .
COPY ./WebApp/WebApp.csproj ./WebApp/WebApp.csproj
RUN dotnet restore

#copying all the other files
COPY . ./

# Linux containers
RUN dotnet publish -c Release -o out; exit 0

#building runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY ./WebApp/build/ ./build/
COPY --from=build-env /app/out ./

ENTRYPOINT ["dotnet", "WebApp.dll"]

# docker build .-t app
# docker run -p 44348:80 -d --name webapp app