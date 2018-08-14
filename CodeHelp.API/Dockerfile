FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY CodeHelp.API/CodeHelp.API.csproj CodeHelp.API/
COPY CodeHelp.QueryService/CodeHelp.QueryService.csproj CodeHelp.QueryService/
COPY CodeHelp.Repository/CodeHelp.Repository.csproj CodeHelp.Repository/
COPY CodeHelp.Data.Dapper/CodeHelp.Data.Dapper.csproj CodeHelp.Data.Dapper/
COPY CodeHelp.Common/CodeHelp.Common.csproj CodeHelp.Common/
COPY CodeHelp.Domain/CodeHelp.Domain.csproj CodeHelp.Domain/
COPY CodeHelp.DomainService/CodeHelp.DomainService.csproj CodeHelp.DomainService/
RUN dotnet restore CodeHelp.API/CodeHelp.API.csproj
COPY . .
WORKDIR /src/CodeHelp.API
RUN dotnet build CodeHelp.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CodeHelp.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CodeHelp.API.dll"]
