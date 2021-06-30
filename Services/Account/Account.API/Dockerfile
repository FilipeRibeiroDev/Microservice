#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Account/Account.API/Account.API.csproj", "Services/Account/Account.API/"]
COPY ["Services/Account/Account.Infrastructure/Account.Infrastructure.csproj", "Services/Account/Account.Infrastructure/"]
COPY ["Services/Account/Account.Domain/Account.Domain.csproj", "Services/Account/Account.Domain/"]
RUN dotnet restore "Services/Account/Account.API/Account.API.csproj"
COPY . .
WORKDIR "/src/Services/Account/Account.API"
RUN dotnet build "Account.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Account.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.API.dll"]