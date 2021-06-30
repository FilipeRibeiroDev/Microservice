#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Node/Node.API/Node.API.csproj", "Services/Node/Node.API/"]
RUN dotnet restore "Services/Node/Node.API/Node.API.csproj"
COPY . .
WORKDIR "/src/Services/Node/Node.API"
RUN dotnet build "Node.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Node.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Node.API.dll"]
