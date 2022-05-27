#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiProject.Lesson/ApiProject.Lesson.csproj", "ApiProject.Lesson/"]
RUN dotnet restore "ApiProject.Lesson/ApiProject.Lesson.csproj"
COPY . .
WORKDIR "/src/ApiProject.Lesson"
RUN dotnet build "ApiProject.Lesson.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiProject.Lesson.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiProject.Lesson.dll"]