FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine as builder

WORKDIR /src

COPY ["src/Advertising.Common/Advertising.Common.csproj", "src/Advertising.Common/"]
COPY ["src/Advertising.Common.Bus.Contracts/Advertising.Common.Bus.Contracts.csproj", "src/Advertising.Common.Bus.Contracts/"]
COPY ["src/Advertising.Domain.Shared/Advertising.Domain.Shared.csproj", "src/Advertising.Domain.Shared/"]
COPY ["src/Advertising.Domain/Advertising.Domain.csproj", "src/Advertising.Domain/"]
COPY ["src/Advertising.Data/Advertising.Data.csproj", "src/Advertising.Data/"]
COPY ["src/Advertising.Application.AdvertServices/Advertising.Application.AdvertServices.csproj", "src/Advertising.Application.AdvertServices/"]
COPY ["src/Advertising.Api.Core/Advertising.Api.Core.csproj", "src/Advertising.Api.Core/"]
COPY ["src/Advertising.Api/Advertising.Api.csproj", "src/Advertising.Api/"]


RUN dotnet restore "src/Advertising.Api/Advertising.Api.csproj"
COPY . .
WORKDIR "/src/src/Advertising.Api"
RUN dotnet build "Advertising.Api.csproj" -c Release -o /app/build

RUN dotnet publish "Advertising.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine as baseimage
WORKDIR /app
COPY --from=builder /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://*:5000

CMD [ "dotnet", "Advertising.Api.dll" ]