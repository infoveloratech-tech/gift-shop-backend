FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY gift-shop.csproj ./
RUN dotnet restore gift-shop.csproj

COPY . .
RUN dotnet publish gift-shop.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "gift-shop.dll"]