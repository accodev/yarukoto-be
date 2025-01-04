FROM mcr.microsoft.com/dotnet/sdk:9.0 AS sdk

WORKDIR /

COPY ./ ./
RUN cd Yarukoto.Host && dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY --from=sdk ./Yarukoto.Host/out .

EXPOSE 8080 5277
ENTRYPOINT ["dotnet", "Yarukoto.Host.dll"]