dotnet restore

dotnet build -c Release

dotnet publish -c Release

$env:ASPNETCORE_ENVIRONMENT="Production"

dotnet ./bin/Release/netcoreapp2.2/DocumentProcessing.dll