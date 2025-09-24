# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura as dependências
COPY ["src/UniSystem.Api/UniSystem.Api.csproj", "src/UniSystem.Api/"]
COPY ["src/UniSystem.Application/UniSystem.Application.csproj", "src/UniSystem.Application/"]
COPY ["src/UniSystem.Domain/UniSystem.Domain.csproj", "src/UniSystem.Domain/"]
COPY ["src/UniSystem.Infrastructure/UniSystem.Infrastructure.csproj", "src/UniSystem.Infrastructure/"]
COPY ["UniSystem.sln", "."]

RUN dotnet restore "UniSystem.sln"

# Copia o restante do código-fonte
COPY . .
WORKDIR "/src/src/UniSystem.Api"
RUN dotnet build "UniSystem.Api.csproj" -c Release -o /app/build

# Estágio de Publicação
FROM build AS publish
RUN dotnet publish "UniSystem.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UniSystem.Api.dll"]
