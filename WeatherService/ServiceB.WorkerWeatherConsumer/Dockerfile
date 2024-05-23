FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ServiceB.WorkerWeatherConsumer/ServiceB.WorkerWeatherConsumer.csproj", "ServiceB.WorkerWeatherConsumer/"]
RUN dotnet restore "ServiceB.WorkerWeatherConsumer/ServiceB.WorkerWeatherConsumer.csproj"
COPY . .
WORKDIR "/src/ServiceB.WorkerWeatherConsumer"
RUN dotnet build "ServiceB.WorkerWeatherConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ServiceB.WorkerWeatherConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceB.WorkerWeatherConsumer.dll"]
