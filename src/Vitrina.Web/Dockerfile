FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Vitrina.Web/Vitrina.Web.csproj", "Vitrina.Web/"]
COPY ["Vitrina.Infrastructure.DataAccess/Vitrina.Infrastructure.DataAccess.csproj", "Vitrina.Infrastructure.DataAccess/"]
COPY ["Vitrina.Infrastructure.Abstractions/Vitrina.Infrastructure.Abstractions.csproj", "Vitrina.Infrastructure.Abstractions/"]
COPY ["Vitrina.Domain/Vitrina.Domain.csproj", "Vitrina.Domain/"]
COPY ["Vitrina.UseCases/Vitrina.UseCases.csproj", "Vitrina.UseCases/"]
COPY ["Vitrina.UseCases.Common/Vitrina.UseCases.Common.csproj", "Vitrina.UseCases.Common/"]
RUN dotnet restore "Vitrina.Web/Vitrina.Web.csproj"
COPY . .
WORKDIR "/src/Vitrina.Web"
RUN dotnet build "Vitrina.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Vitrina.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vitrina.Web.dll"]
