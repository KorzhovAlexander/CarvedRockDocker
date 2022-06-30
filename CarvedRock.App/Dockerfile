FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarvedRock.App/CarvedRock.App.csproj", "CarvedRock.App/"]
RUN dotnet restore "CarvedRock.App/CarvedRock.App.csproj"
COPY . .
WORKDIR "/src/CarvedRock.App"
RUN dotnet build "CarvedRock.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarvedRock.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarvedRock.App.dll"]
