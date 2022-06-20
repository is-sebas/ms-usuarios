FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ms-usuarios.csproj", "."]
RUN dotnet restore "./ms-usuarios.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ms-usuarios.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ms-usuarios.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ms-usuarios.dll
