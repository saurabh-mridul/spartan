FROM microsoft/dotnet:sdk AS build
WORKDIR /app

# copy csproj and restore as dintinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY ./ ./
RUN dotnet publish -c Release -o out

# build runtime image 
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Spartan.dll"]
