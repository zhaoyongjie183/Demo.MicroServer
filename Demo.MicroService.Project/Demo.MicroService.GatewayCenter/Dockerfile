#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Demo.MicroService.GatewayCenter/Demo.MicroService.GatewayCenter.csproj", "Demo.MicroService.GatewayCenter/"]
COPY ["Demo.MicroService.Core/Demo.MicroService.Core.csproj", "Demo.MicroService.Core/"]
RUN dotnet restore "Demo.MicroService.GatewayCenter/Demo.MicroService.GatewayCenter.csproj"
COPY . .
WORKDIR "/src/Demo.MicroService.GatewayCenter"
RUN dotnet build "Demo.MicroService.GatewayCenter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.MicroService.GatewayCenter.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.MicroService.GatewayCenter.dll"]