FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "./HomeTask2/HomeTask2.csproj" --disable-parallel
# Build and publish a release
RUN dotnet publish "./HomeTask2/HomeTask2.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "HomeTask2.dll"]
