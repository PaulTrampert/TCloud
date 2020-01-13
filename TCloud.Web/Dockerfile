#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:lts AS jsbuild
WORKDIR /src/TCloud.Web
COPY ["TCloud.Web/package.json", "TCloud.Web/package-lock.json", "TCloud.Web/.npmrc", "./"]
RUN npm install
COPY "TCloud.Web/" .
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TCloud.Web/TCloud.Web.csproj", "TCloud.Web/"]
RUN dotnet restore "TCloud.Web/TCloud.Web.csproj"
COPY . .
WORKDIR "/src/TCloud.Web"
RUN dotnet build "TCloud.Web.csproj" -c Release -o /app/build

FROM build AS publish
COPY --from=jsbuild /src/TCloud.Web/wwwroot/ ./wwwroot/
RUN dotnet publish "TCloud.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TCloud.Web.dll"]