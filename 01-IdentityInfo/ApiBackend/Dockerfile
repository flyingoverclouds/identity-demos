#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiBackend/ApiBackend.csproj", "ApiBackend/"]
RUN dotnet restore "ApiBackend/ApiBackend.csproj"
COPY . .
WORKDIR "/src/ApiBackend"
RUN dotnet build "ApiBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiBackend.dll"]