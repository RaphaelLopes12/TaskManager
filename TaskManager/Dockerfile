# Usa a imagem oficial do .NET SDK para build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Usa a imagem do .NET SDK para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos do projeto e restaura as dependências
COPY ["TaskManager.csproj", "./"]
RUN dotnet restore

# Copia todo o código e compila
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Usa a imagem base para rodar a aplicação
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TaskManager.dll"]
