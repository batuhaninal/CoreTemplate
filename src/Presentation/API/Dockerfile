# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY src/Core/Application/*.csproj src/Core/Application/
COPY src/Core/Domain/*.csproj src/Core/Domain/
COPY src/Infrastructure/Adapter/*.csproj src/Infrastructure/Adapter/
COPY src/Infrastructure/OpenTelemetry.Shared/*.csproj src/Infrastructure/OpenTelemetry.Shared/
COPY src/Infrastructure/Persistence/*.csproj src/Infrastructure/Persistence/
COPY src/Presentation/API/*.csproj src/Presentation/API/
RUN dotnet restore src/Presentation/API/*.csproj
COPY . .
RUN dotnet publish src/Presentation/API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","API.dll" ]