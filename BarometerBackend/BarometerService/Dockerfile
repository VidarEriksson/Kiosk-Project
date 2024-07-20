#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG Configuration
WORKDIR /src
COPY ["BarometerService/BarometerService.csproj", "BarometerService/"]
RUN dotnet restore "./BarometerService/BarometerService.csproj"
COPY . .
WORKDIR "/src/BarometerService"
RUN dotnet build "./BarometerService.csproj" -c $Configuration -o /app/build

FROM build AS publish
ARG Configuration
ARG Version
ARG Description
RUN dotnet publish "./BarometerService.csproj" -c $Configuration -o /app/publish /p:UseAppHost=false -p:Version="$Version" -p:Description="$Description"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BarometerService.dll"]