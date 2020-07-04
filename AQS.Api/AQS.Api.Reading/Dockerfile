#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AQS.Api.Reading/AQS.Api.Reading.csproj", "AQS.Api.Reading/"]
RUN dotnet restore "AQS.Api.Reading/AQS.Api.Reading.csproj"
COPY . .
WORKDIR "/src/AQS.Api.Reading"
RUN dotnet build "AQS.Api.Reading.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AQS.Api.Reading.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AQS.Api.Reading.dll"]