#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/KlirTechChallenge.WebApi/KlirTechChallenge.WebApi.csproj", "src/KlirTechChallenge.WebApi/"]
COPY ["src/KlirTechChallenge.Infrastructure.IoC/KlirTechChallenge.Infrastructure.IoC.csproj", "src/KlirTechChallenge.Infrastructure.IoC/"]
COPY ["src/KlirTechChallenge.Infrastructure/KlirTechChallenge.Infrastructure.csproj", "src/KlirTechChallenge.Infrastructure/"]
COPY ["src/KlirTechChallenge.Domain/KlirTechChallenge.Domain.csproj", "src/KlirTechChallenge.Domain/"]
COPY ["src/KlirTechChallenge.Domain.SeedWork/KlirTechChallenge.Domain.SeedWork.csproj", "src/KlirTechChallenge.Domain.SeedWork/"]
COPY ["src/KlirTechChallenge.Application/KlirTechChallenge.Application.csproj", "src/KlirTechChallenge.Application/"]
RUN dotnet restore "src/KlirTechChallenge.WebApi/KlirTechChallenge.WebApi.csproj"
COPY . .
WORKDIR "/src/src/KlirTechChallenge.WebApi"
RUN dotnet build "KlirTechChallenge.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KlirTechChallenge.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KlirTechChallenge.WebApi.dll"]
