FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MediatorOne/MediatorOne.csproj", "MediatorOne/"]
RUN dotnet restore "MediatorOne/MediatorOne.csproj"
COPY . .
WORKDIR "/src/MediatorOne"
RUN dotnet build "MediatorOne.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MediatorOne.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediatorOne.dll"]
