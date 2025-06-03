# Use the official .NET SDK image for building the project
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .
# Expose the port (change if your app runs on a different port)
EXPOSE 80
ENTRYPOINT ["dotnet", "ContactAPI.dll"]