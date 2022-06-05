FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarvedRockDocker/CarvedRockDocker.csproj", "CarvedRockDocker/"]
RUN dotnet restore "CarvedRockDocker/CarvedRockDocker.csproj"
COPY . .
WORKDIR "/src/CarvedRockDocker"
RUN dotnet build "CarvedRockDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarvedRockDocker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarvedRockDocker.dll"]
