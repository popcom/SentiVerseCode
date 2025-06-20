FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./src ./src
RUN dotnet restore src/SentiVerse.Api/SentiVerse.Api.csproj
RUN dotnet publish src/SentiVerse.Api/SentiVerse.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SentiVerse.Api.dll"]
