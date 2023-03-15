#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Demo.MicroService.ProjectMicroService/Demo.MicroService.ProjectMicroService.csproj", "Demo.MicroService.ProjectMicroService/"]
COPY ["Demo.MicroService.BusinessDomain/Demo.MicroService.BusinessDomain.csproj", "Demo.MicroService.BusinessDomain/"]
COPY ["Demo.MicroService.BusinessModel/Demo.MicroService.BusinessModel.csproj", "Demo.MicroService.BusinessModel/"]
COPY ["Demo.MicroService.Core/Demo.MicroService.Core.csproj", "Demo.MicroService.Core/"]
COPY ["Demo.MicroService.Repository/Demo.MicroService.Repository.csproj", "Demo.MicroService.Repository/"]
RUN dotnet restore "Demo.MicroService.ProjectMicroService/Demo.MicroService.ProjectMicroService.csproj"
COPY . .
WORKDIR "/src/Demo.MicroService.ProjectMicroService"
RUN dotnet build "Demo.MicroService.ProjectMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.MicroService.ProjectMicroService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.MicroService.ProjectMicroService.dll"]