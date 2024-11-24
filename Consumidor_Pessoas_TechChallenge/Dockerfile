#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Consumidor_Pessoas_TechChallenge/Consumidor_Pessoas_TechChallenge.csproj", "Consumidor_Pessoas_TechChallenge/"]
COPY ["DataAccess_TechChallengePrimeiraFase/DataAccess_TechChallengePrimeiraFase.csproj", "DataAccess_TechChallengePrimeiraFase/"]
COPY ["Entities_TechChallengePrimeiraFase/Entities_TechChallengePrimeiraFase.csproj", "Entities_TechChallengePrimeiraFase/"]
COPY ["Infrastructure_TechChallengePrimeiraFase/Infrastructure_TechChallengePrimeiraFase.csproj", "Infrastructure_TechChallengePrimeiraFase/"]
RUN dotnet restore "./Consumidor_Pessoas_TechChallenge/Consumidor_Pessoas_TechChallenge.csproj"
COPY . .
WORKDIR "/src/Consumidor_Pessoas_TechChallenge"
RUN dotnet build "./Consumidor_Pessoas_TechChallenge.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Consumidor_Pessoas_TechChallenge.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consumidor_Pessoas_TechChallenge.dll"]