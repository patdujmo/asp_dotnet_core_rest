# Basis-Image für das Build-Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Projektdateien kopieren und Wiederherstellung der Abhängigkeiten
COPY *.csproj ./
RUN dotnet restore

# Den Rest des Codes kopieren und das Projekt kompilieren
COPY . ./
RUN dotnet publish -c Release -o /out

# Basis-Image für das Runtime-Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Kompilierte Anwendung kopieren
COPY --from=build /out .

# Container-Startbefehl
ENTRYPOINT ["dotnet", "MemberAPI.dll"]
