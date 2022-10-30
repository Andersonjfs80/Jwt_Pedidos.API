#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
LABEL version="1.0.2" description="Aplicacao ASP .NET Core MVC"
COPY dist /app
WORKDIR /app
EXPOSE 80/tcp
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Jwt_Lista_Compras/Jwt_Lista_Compras.csproj", "Jwt_Lista_Compras/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "Jwt_Lista_Compras/Jwt_Lista_Compras.csproj"
COPY . .
WORKDIR "/src/Jwt_Lista_Compras"
RUN dotnet build "Jwt_Lista_Compras.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jwt_Lista_Compras.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jwt_Pedidos_v1.API.dll"]

