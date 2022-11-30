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


# docker build -t recipeapp.userinterface .

#run front-end
# docker run -it -p 8001:80 --rm --name recipeapps3-adminfrontend mattijs99/recipeapps3:adminfrontend

#run back-end
# docker run -it -p 8000:80 --rm -e ASPNETCORE_ENVIRONMENT=Development --name recipeapps3-backend mattijs99/recipeapps3:back-end-api