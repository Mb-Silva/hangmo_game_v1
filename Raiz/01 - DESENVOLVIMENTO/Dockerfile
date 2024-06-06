# Estágio de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia os arquivos de solução e projeto
COPY Hangmo.Server/*.csproj ./Hangmo.Server/
COPY Hangmo.Repository/*.csproj ./Hangmo.Repository/
COPY *.sln ./

# Restaura as dependências
RUN dotnet restore

# Copia todos os arquivos do projeto
COPY . ./

# Publica a aplicação
RUN dotnet publish -c Release -o out

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados do estágio de construção
COPY --from=build-env /app/out .

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Hangmo.Server.dll