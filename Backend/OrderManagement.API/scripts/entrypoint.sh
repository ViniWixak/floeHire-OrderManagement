#!/bin/bash
set -e

# Aguardar conexão com o banco de dados
echo "Aguardando banco de dados..."
sleep 10

# Aplicar migrations
echo "Executando migrations..."
dotnet ef database update

# Iniciar a aplicação
echo "Iniciando a aplicação..."
exec dotnet OrderManagement.API.dll
