FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 90

ENV ASPNETCORE_URLS=http://*:90
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["LoggingAPI.csproj", "./"]
RUN dotnet restore "LoggingAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "LoggingAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoggingAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoggingAPI.dll"]
