FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ElectronicVoting.API/ElectronicVoting.API.csproj", "ElectronicVoting.API/"]
RUN dotnet restore "ElectronicVoting.API/ElectronicVoting.API.csproj"
COPY . .
WORKDIR "/src/ElectronicVoting.API"
RUN dotnet build "ElectronicVoting.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElectronicVoting.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElectronicVoting.API.dll"]
