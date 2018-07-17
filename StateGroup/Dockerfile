FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY StateGroup/StateGroup.csproj StateGroup/
RUN dotnet restore StateGroup/StateGroup.csproj
COPY . .
WORKDIR /src/StateGroup
RUN dotnet build StateGroup.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish StateGroup.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StateGroup.dll"]
