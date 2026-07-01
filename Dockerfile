# -------------------------
# BUILD STAGE
# -------------------------
FROM mcr.microsoft.com/dotnet/nightly/sdk:10.0-preview AS build

WORKDIR /src

COPY . .

RUN dotnet restore registration.csproj

RUN dotnet publish registration.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# -------------------------
# RUNTIME STAGE
# -------------------------
FROM mcr.microsoft.com/dotnet/nightly/aspnet:10.0-preview

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet","registration.dll"]