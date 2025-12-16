# Giai đoạn 1: Base Image (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# Sử dụng user non-root mặc định của .NET 8
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Giai đoạn 2: Build Image (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Thay đổi tên file .csproj thành FU_House_Finder_Auth.csproj
COPY ["FU_House_Finder_Auth.csproj", "."]

# Restore package cho project Auth
RUN dotnet restore "./FU_House_Finder_Auth.csproj"

# Copy toàn bộ source code
COPY . .

# Build project
WORKDIR "/src/."
RUN dotnet build "./FU_House_Finder_Auth.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Giai đoạn 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FU_House_Finder_Auth.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Giai đoạn 4: Final (Run)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Quan trọng: Sửa tên file .dll chạy lúc khởi động
ENTRYPOINT ["dotnet", "FU_House_Finder_Auth.dll"]