FROM node:18 AS build
WORKDIR /app

COPY package.json ./
RUN npm install

COPY . ./

RUN npm run build

FROM node:18-slim
WORKDIR /app

COPY --from=build /app/dist /app/dist

RUN npm install -g http-server

EXPOSE 5174
CMD ["http-server", "dist", "--host", "0.0.0.0", "--port", "5174"]