dotnet restore

dotnet build -c Release

dotnet publish -c Release

dotnet ./bin/Release/netcoreapp2.2/DocumentProcessing.dll