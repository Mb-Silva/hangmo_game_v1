# Est�gio de constru��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia os arquivos de solu��o e projeto
COPY Hangmo.Server/*.csproj ./Hangmo.Server/
COPY Hangmo.Repository/*.csproj ./Hangmo.Repository/
COPY *.sln ./

# Restaura as depend�ncias
RUN dotnet restore

# Copia todos os arquivos do projeto
COPY . ./

# Publica a aplica��o
RUN dotnet publish -c Release -o out

# Est�gio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados do est�gio de constru��o
COPY --from=build-env /app/out .

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Hangmo.Server.dll