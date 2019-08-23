FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY UnicornStore/UnicornStore.csproj UnicornStore/
RUN dotnet restore UnicornStore/UnicornStore.csproj

COPY . .
WORKDIR /src/UnicornStore
ARG BUILD_CONFIG=ReleaseSQL
#RUN echo Build config for 'docker build': ${BUILD_CONFIG}
RUN dotnet build UnicornStore.csproj -c ${BUILD_CONFIG} -o /app

FROM build AS publish
ARG BUILD_CONFIG=ReleaseSQL
#RUN echo Build config for 'docker publish': ${BUILD_CONFIG}
RUN dotnet publish UnicornStore.csproj -c ${BUILD_CONFIG} -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UnicornStore.dll"]
HEALTHCHECK --interval=30s --timeout=5s --retries=5 --start-period=30s CMD wget --quiet --tries=1 --spider http://localhost/health || exit 1