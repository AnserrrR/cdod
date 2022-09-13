FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["cdod/cdod.csproj", "cdod/"]
RUN dotnet restore "cdod/cdod.csproj"

COPY . .
WORKDIR "/src/cdod"
RUN dotnet build "cdod.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "cdod.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "cdod.dll"]