FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY Evento.Api/Evento.Api.csproj Evento.Api/
COPY Evento.Infrastructure/Evento.InfraStructure.csproj Evento.Infrastructure/
COPY Evento.Core/Evento.Core.csproj Evento.Core/
RUN dotnet restore "Evento.Api/Evento.Api.csproj"
COPY . .
WORKDIR "/src/Evento.Api"
RUN dotnet build "Evento.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Evento.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Evento.Api.dll"]
