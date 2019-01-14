FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Shop.Api/Shop.Api.csproj Shop.Api/
RUN dotnet restore Shop.Api/Shop.Api.csproj
COPY . .
WORKDIR /src/Shop.Api
RUN dotnet build Shop.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Shop.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shop.Api.dll"]
