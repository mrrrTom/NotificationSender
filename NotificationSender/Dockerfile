#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV PGDATABASE="notificationdb"
ENV PGPASSWORD="1Saitohajime"
ENV PGPORT="5432"
ENV PGUSER="postgres"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NotificationSender/NotificationSender.csproj", "NotificationSender/"]
RUN dotnet restore "NotificationSender/NotificationSender.csproj"
COPY . .
WORKDIR "/src/NotificationSender"
RUN dotnet build "NotificationSender.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationSender.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationSender.dll"]