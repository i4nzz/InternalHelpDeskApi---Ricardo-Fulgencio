FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["InternalHelpDesk.API/InternalHelpDesk.API.csproj", "InternalHelpDesk.API/"]
COPY ["InternalHelpDeskApi.Application/InternalHelpDeskApi.Application.csproj", "InternalHelpDeskApi.Application/"]
COPY ["InternalHelpDeskApi.Domain/InternalHelpDeskApi.Domain.csproj", "InternalHelpDeskApi.Domain/"]
COPY ["InternalHelpDeskApi.Infrastructure/InternalHelpDeskApi.Infrastructure.csproj", "InternalHelpDeskApi.Infrastructure/"]

RUN dotnet restore "InternalHelpDesk.API/InternalHelpDesk.API.csproj"

COPY . .

WORKDIR "/src/InternalHelpDesk.API"
RUN dotnet publish "InternalHelpDesk.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "InternalHelpDesk.API.dll"]