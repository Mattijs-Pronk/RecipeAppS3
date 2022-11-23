FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["Back-end-API/Back-end-API.csproj", "Back-end-API/"]
RUN dotnet restore "Back-end-API/Back-end-API.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Back-end-API/Back-end-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Back-end-API/Back-end-API.csproj" -c Release -o /app/publish \
   -r alpine-x64 \
   --self-contained true \
   -p:PublishTrimmed=true \
   -p:PublishSingleFile=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./Back-end-API"]


# docker build -t naam .
# docker run -it -p 8000:80 --rm -e ASPNETCORE_ENVIRONMENT=Development --name naam naam