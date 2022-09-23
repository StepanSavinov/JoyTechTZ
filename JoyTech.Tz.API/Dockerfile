FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JoyTech.Tz.API/JoyTech.Tz.API.csproj", "JoyTech.Tz.API/"]
RUN dotnet restore "JoyTech.Tz.API/JoyTech.Tz.API.csproj"
COPY . .
WORKDIR "/src/JoyTech.Tz.API"
RUN dotnet build "JoyTech.Tz.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JoyTech.Tz.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JoyTech.Tz.API.dll"]
