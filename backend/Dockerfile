# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything and build
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Run (ตัวแอปจริงทำงานที่นี่)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# ย้าย ENV และ EXPOSE มาไว้ที่นี่เพื่อให้แอปเห็นค่าตอนรันจริง
ENV ASPNETCORE_URLS=http://+:5268
EXPOSE 5268

# ตรวจสอบว่าไฟล์ชื่อ Backend.dll จริงๆ ใช่ไหม (ถ้าไม่ใช่ให้แก้ให้ตรงกับชื่อโปรเจกต์)
ENTRYPOINT ["dotnet", "Backend.dll"]