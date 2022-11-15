FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build


FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]


# docker build -t naam .

#run front-end
# docker run -it -p 8001:80 --rm --name naam naam

#run back-end
# docker run -it -p 8000:80 --rm -e ASPNETCORE_ENVIRONMENT=Development --name naam naam