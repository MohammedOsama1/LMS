# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY LMS.Api/*.csproj ./
COPY LMS.Api/. ./LMS.Api/
COPY LMS.Core/. ./LMS.Core/
COPY LMS.EF/. ./LMS.EF/
COPY *.sln .
RUN dotnet restore

# Copy everything else and build
COPY . .

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Expose the port your app will run on
EXPOSE 7063
EXPOSE 5220
EXPOSE 80

# Command to run the application
ENTRYPOINT ["dotnet", "LMS.Api.dll"]
