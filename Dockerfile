FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base

WORKDIR /app 
EXPOSE 5000

ENV DOTNET_RUNNING_IN_CONTAINER 1
ENV ASPNETCORE_URLS=http://+:5000

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . .

# Fix dotnet restore
# RUN curl -o /usr/local/share/ca-certificates/verisign.crt -SsL https://crt.sh/?d=1039083 && update-ca-certificates

# RUN dotnet publish
RUN cd src/FormBuilderApp && dotnet restore && dotnet publish -c Release -o /app/out \
    --runtime linux-x64 \
    --no-self-contained

FROM base as final
WORKDIR /app
COPY --from=build /app/out ./

RUN mkdir -p /app/images
RUN mkdir -p /app/thumbnails

ENTRYPOINT ["dotnet", "FormBuilderApp.dll"]
