FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY src/ ./
RUN dotnet restore "WorkTime/WorkTime.csproj"
RUN dotnet build "WorkTime/WorkTime.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkTime/WorkTime.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkTime.dll"]
