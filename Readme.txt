# 🏥 AuraClinic - Multi-Tenant Clinic POS Platform (v1)

ระบบบริหารจัดการคลินิกแบบ Multi-tenant ที่เน้นความปลอดภัยของข้อมูล (Tenant Isolation) และความสามารถในการขยายตัว (Scalability) พัฒนาขึ้นภายใต้เงื่อนไขเวลา 90 นาที

## 🏗️ Architecture & Tenant Safety
เรายึดหลัก **Tenant-safe by default**:
- **Tenant Isolation**: ใช้ **Global Query Filter** ใน EF Core เพื่อบังคับให้ทุก Query ในระดับ Database ต้องผ่านฟิลเตอร์ `TenantId` เสมอ ป้องกันข้อมูลรั่วไหลระหว่าง Tenant
- **Tenant Derivation**: ระบบจะดึง `TenantId` จาก HTTP Header `X-Tenant-Id` ผ่าน `TenantProvider` (Scoped Service)
- **Tech Stack**: 
  - **Backend**: .NET 9 (C#)
  - **Frontend**: Next.js 15 (App Router)
  - **Database**: PostgreSQL (Persistence)
  - **Cache**: Redis (Tenant-scoped caching)
  - **Messaging**: RabbitMQ (Asynchronous event publishing)



## 🚀 How to Run (One Command)
รันระบบทั้งหมด (Backend, Frontend, DB, Redis, RabbitMQ) ด้วยคำสั่งเดียว:

```bash
docker compose up --build