FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY UnicornStore/UnicornStore.csproj UnicornStore/
RUN dotnet restore UnicornStore/UnicornStore.csproj
COPY . .
WORKDIR /src/UnicornStore
ARG USE_POSTGRES=false
RUN echo USE_POSTGRES for build: $USE_POSTGRES
#RUN dotnet build UnicornStore.csproj -c ReleasePostgres -o /app
RUN if [ "$USE_POSTGRES" = "false" ] ; then dotnet build UnicornStore.csproj -c ReleaseSQL -o /app; else dotnet build UnicornStore.csproj -c ReleasePostgres -o /app; fi

FROM build AS publish
#RUN dotnet publish UnicornStore.csproj -c ReleasePostgres -o /app
ARG USE_POSTGRES=false
RUN echo USE_POSTGRES for publish: $USE_POSTGRES
RUN if [ "$USE_POSTGRES" = "false" ] ; then dotnet publish UnicornStore.csproj -c ReleaseSQL -o /app; else dotnet publish UnicornStore.csproj -c ReleasePostgres -o /app; fi

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UnicornStore.dll"]
HEALTHCHECK --interval=30s --timeout=5s --retries=5 --start-period=30s CMD wget --quiet --tries=1 --spider http://localhost/health || exit 1