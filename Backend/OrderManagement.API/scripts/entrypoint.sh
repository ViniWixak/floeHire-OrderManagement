#!/bin/bash
set -e

# Aguardar conex�o com o banco de dados
echo "Aguardando banco de dados..."
sleep 10

# Aplicar migrations
echo "Executando migrations..."
dotnet ef database update

# Iniciar a aplica��o
echo "Iniciando a aplica��o..."
exec dotnet OrderManagement.API.dll
