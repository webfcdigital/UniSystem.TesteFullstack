# Estágio 1: Build do Angular
FROM node:20 AS build
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

# Estágio 2: Servir com Nginx
FROM nginx:alpine
COPY --from=build /usr/src/app/dist/unisystem-frontend/browser /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
