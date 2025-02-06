FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrderManagement/OrderManagement.csproj", "OrderManagement/"]
RUN dotnet restore "OrderManagement/OrderManagement.csproj"
COPY . .
WORKDIR "/src/OrderManagement"
RUN dotnet build "OrderManagement.csproj" -c Release -o /app/build

# Etapa 2: Publish
FROM build AS publish
RUN dotnet publish "OrderManagement.csproj" -c Release -o /app/publish

# Etapa 3: Runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManagement.dll"]
