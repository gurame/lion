#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Lion.API/Lion.API.csproj", "src/Lion.API/"]
RUN dotnet restore "src/Lion.API/Lion.API.csproj"
COPY . .
WORKDIR "/src/src/Lion.API"
RUN dotnet build "Lion.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lion.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lion.API.dll"]