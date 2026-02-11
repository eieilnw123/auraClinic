# 🤖 AI_PROMPTS.md - AI Usage & Judgment Log

บันทึกการใช้งาน AI ในการสร้างระบบ AuraClinic เพื่อแสดงถึงการตัดสินใจและการแก้ปัญหาเชิงเทคนิค

## 1. Initial Project Scaffolding
- **Prompt:** "ช่วยสร้าง Dockerfile สำหรับ .NET 9 Backend และ Next.js Frontend โดยต้องรันผ่าน Docker Compose ได้ในคำสั่งเดียว"
- **Iteration:** มีการปรับปรุง Dockerfile ของ Backend โดยย้ายคำสั่ง `ENV ASPNETCORE_URLS` มาไว้ที่ Stage 2 (Run) เนื่องจาก AI ตอนแรกใส่ไว้ที่ Stage 1 ทำให้ตอนรันจริงพอร์ตไม่ถูกเปิดตามที่ตั้งไว้

## 2. Infrastructure Debugging (CORS & Docker Networking)
- **Prompt:** "เจอ Error CORS blocked: No 'Access-Control-Allow-Origin' และ ERR_EMPTY_RESPONSE ใน Docker"
- **AI Judgment:** - **Accepted:** การย้ายลำดับ `app.UseCors()` มาไว้ก่อน `app.UseAuthorization()` ตามคำแนะนำของ AI
  - **Accepted:** เปลี่ยน Connection String จาก `localhost` เป็น `db` เพื่อให้ Container สื่อสารกันได้ผ่าน Docker Network
  - **Rejected:** AI เคยแนะนำให้ตั้งค่า CORS เป็น `AllowAnyOrigin()` ทั้งหมด ซึ่งในตอนแรกยอมรับเพื่อให้ระบบผ่าน Test ได้เร็ว แต่ใน Production จริงควรระบุเป็น Domain ที่แน่นอน

## 3. Multi-tenancy Implementation
- **Prompt:** "ขอวิธีทำ Tenant Isolation ใน EF Core ที่ Developer ไม่สามารถลืม Filter ได้"
- **Decision:** เลือกใช้ **Global Query Filter** ตามคำแนะนำของ AI เพราะเป็นการ Enforcement ที่ระดับลึกที่สุด (Database Layer) แทนที่จะทำที่ระดับ Controller

## 4. Handling Guid Errors
- **Prompt:** "Cannot implicitly convert type 'string' to 'System.Guid' ในส่วนของ Unit Test"
- **Adjustment:** AI แนะนำให้ใช้ `Guid.Parse()` เพื่อแปลงค่าจาก Mock Data ให้ตรงกับ Data Type ใน Model

## 5. Caching & Messaging
- **Prompt:** "ขอตัวอย่างการเพิ่ม Redis และ RabbitMQ ใน Program.cs ของ .NET 9"
- **Validation:** ตรวจสอบความถูกต้องของพอร์ตและ Hostname ให้ตรงกับที่ระบุใน `docker-compose.yml`