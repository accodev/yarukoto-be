FROM mcr.microsoft.com/dotnet/sdk:9.0 AS sdk

WORKDIR /

COPY ./ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY --from=sdk ./out .

EXPOSE 5277 5277
ENTRYPOINT ["dotnet", "Yarukoto.Host.dll"]