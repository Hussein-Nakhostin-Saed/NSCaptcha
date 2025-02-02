FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:8.0 as backend-build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./NSCaptcha/NSCaptcha.csproj", "NSCaptcha/"]
COPY ["./NSCaptcha.Test/NSCaptcha.Test.csproj", "NSCaptcha.Test/"]
RUN dotnet restore "./NSCaptcha.Test/NSCaptcha.Test.csproj"
COPY . .
RUN dotnet build "./NSCaptcha.Test/NSCaptcha.Test.csproj" -c $BUILD_CONFIGURATION -o /app/build 

FROM backend-build as backend-publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NSCaptcha.Test/NSCaptcha.Test.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base as final
WORKDIR /app
COPY --from=backend-publish /app/publish .

COPY Fonts /usr/local/share/fonts
RUN apt-get update && apt-get install -y fontconfig
RUN fc-cache -f -v

ENTRYPOINT ["dotnet","NSCaptcha.Test.dll"]
EXPOSE 5229