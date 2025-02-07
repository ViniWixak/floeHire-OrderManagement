# Order Management API

Este é um sistema de gerenciamento de pedidos que utiliza uma arquitetura de microserviços com integração entre bancos de dados relacionais e NoSQL. Ele permite o cadastro de pedidos, manipulação de itens, atualização de status e gerenciamento de dados de clientes.

O backend está implementado em .NET 8.0 e utiliza o SQL Server para armazenar os dados transacionais (Pedidos, Clientes e Itens de Pedidos), enquanto o MongoDB é usado para leitura e consulta de dados otimizada.

## Estrutura do Projeto

O projeto é dividido em:

- **Backend** (API em .NET 8.0)
  - Banco de dados relacional: **SQL Server** (para dados de pedidos, clientes e itens de pedidos).
  - Banco de dados NoSQL: **MongoDB** (para leitura otimizada de pedidos e itens).
  - API RESTful com endpoints para a criação e consulta de pedidos, itens e clientes.
  
- **Frontend** (React).
  - Interface web para visualização, criação e manipulação de pedidos.
  - Comunicação com a API RESTful do backend.

## Funcionalidades

- **Cadastro de Pedidos:** Criação de novos pedidos, associando-os a um cliente.
- **Cadastro de Itens de Pedido:** Adicionar, atualizar e excluir itens em um pedido.
- **Gerenciamento de Status de Pedidos:** Alterar o status de um pedido (Ex: "Em andamento", "Concluído").
- **Consultas de Pedidos:** Obter todos os pedidos ou detalhes de um pedido por ID, com dados otimizados para leitura a partir do MongoDB.

## Requisitos

- Docker (para orquestrar os containers)
- Docker Compose (para orquestrar a construção e execução dos containers)
- .NET SDK (para desenvolvimento do backend)
- MongoDB Compass (opcional, para gerenciar o MongoDB)

## Tecnologias

- **Backend:**
  - .NET 8.0
  - SQL Server
  - MongoDB
  - MediatR (para manipulação de comandos e consultas)
  
- **Frontend:**
  - Tecnologias: React, Vite, TypeScript.
  - Descrição: Aplicação frontend como SPA, que permite o gerenciamento de pedidos, visualização de produtos, administração de pedidos e interação com o carrinho de compras.
  - Estrutura de Pastas:
```
frontend/
  ├── Dockerfile
  ├── README.md
  ├── index.html
  ├── package.json
  ├── public
  └── src
     ├── App.css
     ├── App.tsx
     ├── assets
     ├── components
     ├── context
     ├── index.css
     ├── main.tsx
     ├── pages
     └── routes.tsx
```
- Principais Funcionalidades:
  -  Home: Página inicial com visão geral dos pedidos e produtos.
  -  Pedidos: Visualização dos pedidos realizados.
  -  AdminPedidos e AdminProdutos: Páginas de administração para gerenciamento de pedidos e produtos.
  -  Navbar: Componente de navegação presente em todas as páginas.
  -  CartContext: Gerenciamento de estado para o carrinho de compras.
  -  Rotas: Configuração da navegação da SPA definida em routes.tsx.


## Instruções para Rodar Localmente

### 1. Clonando o Repositório

1. clone o repositório para sua máquina local:

```bash
git clone [https://github.com/seu-usuario/order-management-api.git](https://github.com/ViniWixak/floeHire-OrderManagement.git)
```

2. Navegue até a pasta do projeto:
```bash
cd floeHire-OrderManagement
cd frontend
```

3. Instale as dependências:
```bash
npm install
# ou
yarn install
```

4. Inicie o servidor de desenvolvimento:
```bash
npm start
```
A aplicação será iniciada na URL padrão, geralmente http://localhost:3000.


### 2. Configuração do Ambiente Docker

O projeto já inclui um arquivo `docker-compose.yml` que define os containers necessários para rodar a aplicação:

- **Backend (API)**: Executa a API do sistema de pedidos.
- **MongoDB**: Banco de dados NoSQL para consultas de pedidos.
- **SQL Server**: Banco de dados relacional para armazenar pedidos, clientes e itens de pedidos.

### 3. Rodando com Docker

Na raiz do repositório, execute os seguintes comandos para construir e rodar a aplicação com Docker:

```bash
docker-compose build  # Constrói as imagens dos containers
docker-compose up     # Suba os containers
```

O backend estará disponível em `http://localhost:5000`.

O frontend estará disponível em `http://localhost:3000`.

#### Configuração dos Bancos:

- MongoDB estará disponível em `mongodb://localhost:27017`.
- SQL Server estará disponível em `localhost:1433`.

### 4. Acessando a API

Agora você pode acessar a API através dos seguintes endpoints:

- **POST /orders**: Criar um novo pedido.
- **GET /orders**: Obter todos os pedidos.
- **GET /orders/{id}**: Obter um pedido específico por ID.
- **PUT /orders/{id}/status**: Atualizar o status de um pedido.
- **POST /order-items**: Adicionar um item em um pedido.
- **GET /order-items**: Obter todos os itens de pedido.
- **PUT /order-items/{id}**: Atualizar um item de pedido.

### 5. Conexões com o MongoDB

Se você quiser verificar diretamente os dados no MongoDB, você pode usar o [MongoDB Compass](https://www.mongodb.com/products/compass) e conectar-se ao seu MongoDB local:

- **URL de conexão:** `mongodb://localhost:27017`

### 6. Testes

Os testes unitários estão implementados utilizando o framework **xUnit**. Para rodá-los, execute o seguinte comando:

```bash
dotnet test
```

Isso irá rodar todos os testes de unidade para garantir que as funcionalidades da API estão funcionando conforme o esperado.

## Estrutura de Diretórios

Aqui está a estrutura de diretórios do projeto:

```
OrderManagement/
│
├── Backend/                   # Código do Backend (.NET)
│   ├── Controllers/            # Controladores da API
│   ├── Domain/                 # Entidades e Interfaces do domínio
│   ├── Infrastructure/         # Implementações de repositórios, acesso a dados, etc.
│   ├── Application/            # Comandos, consultas e lógicas de aplicação
│   ├── OrderManagement.csproj  # Arquivo do projeto .NET
│   └── Dockerfile              # Dockerfile para o Backend
│
├── docker-compose.yml          # Arquivo de configuração para orquestrar os containers
├── README.md                  # Este arquivo
└── .git/                       # Repositório Git
├──frontend/
  ├── Dockerfile
  ├── README.md
  ├── index.html
  ├── package.json
  ├── public
  └── src
     ├── App.css
     ├── App.tsx
     ├── assets
     ├── components
     ├── context
     ├── index.css
     ├── main.tsx
     ├── pages
     └── routes.tsx
```

