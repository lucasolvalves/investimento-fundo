#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Investimento.Fundo/Investimento.Fundo.csproj", "Investimento.Fundo/"]
COPY ["Investimento.Fundo.Domain/Investimento.Fundo.Domain.csproj", "Investimento.Fundo.Domain/"]
COPY ["Investimento.Fundo.Infrastructure/Investimento.Fundo.Infrastructure.csproj", "Investimento.Fundo.Infrastructure/"]
RUN dotnet restore "Investimento.Fundo/Investimento.Fundo.csproj"
COPY . .
WORKDIR "/src/Investimento.Fundo"
RUN dotnet build "Investimento.Fundo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Investimento.Fundo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Investimento.Fundo.dll"]