# 🏥 AuraClinic Management Platform (Multi-tenant POS)

ระบบบริหารจัดการข้อมูลคนไข้สำหรับคลินิกยุคใหม่ รองรับโครงสร้าง Multi-tenancy, Role-Based Access Control (RBAC), Caching และ Messaging System ครบถ้วนตามข้อกำหนด (Section A - D)

## 🌟 Key Features

### Section A: Multi-tenancy & Patient Flow
* **Data Isolation (A2):** ใช้เทคนิค Global Query Filter ใน EF Core เพื่อคัดกรองข้อมูลตาม `TenantId` อัตโนมัติในระดับ Database ป้องกันข้อมูลรั่วไหลระหว่างคลินิก
* **Duplicate Prevention (A3):** เพิ่มความปลอดภัยของข้อมูลด้วย Unique Index (TenantId + PhoneNumber) ป้องกันการลงทะเบียนเบอร์โทรศัพท์ซ้ำภายในคลินิกเดียวกัน
* **Management Interface (A1):** ระบบลงทะเบียนและ Directory คนไข้ที่ใช้งานง่ายและเชื่อมต่อ Backend API แบบ Real-time

### Section B: Role-Based Access Control (RBAC)
* **Multi-role Support (B1, B4):** ระบบรองรับ 3 บทบาทหลัก (Admin, User, Viewer) โดยมีข้อมูล Seeding เริ่มต้นสำหรับ 1 Tenant และ 2 สาขา (Branches)
* **Access Enforcement (B2, B3):** * **Admin/User:** มีสิทธิ์อ่านและเขียนข้อมูล (Register Patient)
    * **Viewer:** มีสิทธิ์อ่านอย่างเดียว (Read-only) โดยระบบจะบล็อกการเข้าถึงทั้งในฝั่ง UI และ Backend (403 Forbidden)

### Section C & D: Scalability & Performance
* **Messaging System (RabbitMQ - C):** ผสานการทำงานกับ MassTransit เพื่อส่ง `PatientCreatedEvent` ทันทีที่มีการลงทะเบียนสำเร็จ รองรับการขยายตัวแบบ Asynchronous
* **Caching Strategy (Redis - D):** ใช้ Tenant-scoped caching เพื่อเก็บรายชื่อคนไข้และข้อมูลสาขา ช่วยลดภาระของฐานข้อมูลและเพิ่มความเร็วในการตอบสนอง

---

## 🏗️ Technical Stack
* **Frontend:** Next.js (App Router), Tailwind CSS, Lucide React
* **Backend:** .NET 9 Web API, Entity Framework Core
* **Database:** PostgreSQL
* **Infrastructure:** Docker, Redis, RabbitMQ

---

## 🚀 Getting Started

### Prerequisites
* Docker Desktop
* Git

### Installation & Run
1. Clone the repository:
   ```bash
   git clone <your-repo-url>
   cd aura-clinic-platform