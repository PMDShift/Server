FROM microsoft/dotnet:runtime
ADD ./Server/bin/Release/netcoreapp2.1/publish ./app
CMD ["dotnet", "/app/Server.dll"]