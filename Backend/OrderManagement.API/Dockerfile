# Etapa 1: Base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos .csproj
COPY ["Backend/OrderManagement.API/OrderManagement.API.csproj", "OrderManagement.API/"]
COPY ["Backend/OrderManagement.Application/OrderManagement.Application.csproj", "OrderManagement.Application/"]
COPY ["Backend/OrderManagement.Domain/OrderManagement.Domain.csproj", "OrderManagement.Domain/"]
COPY ["Backend/OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj", "OrderManagement.Infrastructure/"]

# Restaurar pacotes
RUN dotnet restore "OrderManagement.API/OrderManagement.API.csproj"

# Copiar todos os arquivos restantes
COPY Backend/ ./

# Definir o diretório de trabalho
WORKDIR "/src/OrderManagement.API"

# Compilar Projeto
RUN dotnet build "OrderManagement.API.csproj" -c Release -o /app/build

# Etapa 3: Publish
FROM build AS publish
RUN dotnet publish "OrderManagement.API.csproj" -c Release -o /app/publish

# Etapa 4: Runtime (produção)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#CMD dotnet ef database update --no-build && dotnet OrderManagement.API.dll
COPY Backend/OrderManagement.API/scripts/entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]
