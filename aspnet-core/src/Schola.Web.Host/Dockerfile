FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Schola.Web.Host/Schola.Web.Host.csproj", "src/Schola.Web.Host/"]
COPY ["src/Schola.Web.Core/Schola.Web.Core.csproj", "src/Schola.Web.Core/"]
COPY ["src/Schola.Application/Schola.Application.csproj", "src/Schola.Application/"]
COPY ["src/Schola.Core/Schola.Core.csproj", "src/Schola.Core/"]
COPY ["src/Schola.EntityFrameworkCore/Schola.EntityFrameworkCore.csproj", "src/Schola.EntityFrameworkCore/"]
WORKDIR "/src/src/Schola.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Schola.Web.Host", "src/Schola.Web.Host"]
COPY ["src/Schola.Web.Core", "src/Schola.Web.Core"]
COPY ["src/Schola.Application", "src/Schola.Application"]
COPY ["src/Schola.Core", "src/Schola.Core"]
COPY ["src/Schola.EntityFrameworkCore", "src/Schola.EntityFrameworkCore"]
WORKDIR "/src/src/Schola.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Schola.Web.Host.dll"]
