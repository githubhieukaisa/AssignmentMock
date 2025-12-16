# Giai đoạn 1: Base Image (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# Sử dụng user non-root mặc định của .NET 8 để bảo mật hơn
USER app 
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Giai đoạn 2: Build Image (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# LƯU Ý QUAN TRỌNG: 
# Vì context là "../FU_House_Finder", nên file .csproj nằm ngay tại root của context.
# Ta copy trực tiếp nó vào thư mục hiện tại (.)
COPY ["FU_House_Finder.csproj", "."]

# Restore các package
RUN dotnet restore "./FU_House_Finder.csproj"

# Copy toàn bộ source code còn lại vào image
COPY . .

# Build dự án
WORKDIR "/src/."
RUN dotnet build "./FU_House_Finder.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Giai đoạn 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FU_House_Finder.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Giai đoạn 4: Final (Tạo image cuối cùng)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FU_House_Finder.dll"]