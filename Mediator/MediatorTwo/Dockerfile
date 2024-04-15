FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mediator/Mediator.csproj", "Mediator/"]
RUN dotnet restore "Mediator/Mediator.csproj"
COPY . .
WORKDIR "/src/Mediator"
RUN dotnet build "Mediator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mediator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mediator.dll"]
