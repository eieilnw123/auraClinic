# Clinic POS - Backend Assessment (.NET 9)

### Key Features Implemented:
1. **Multi-tenancy Isolation**: Implemented using EF Core Global Query Filters based on `X-Tenant-Id` header.
2. **Data Integrity**: Enforced via Composite Unique Index on `TenantId` and `PhoneNumber`.
3. **Role Security**: Access control for `Admin` vs `Viewer` roles.
4. **Caching Layer**: Integrated Redis for high-performance data retrieval with automatic cache invalidation.

### How to Run:
1. `docker compose up -d`
2. `dotnet run`
3. Access Swagger at `http://localhost:5268/swagger`

### Evidence of Success:
- Tested with multiple Tenant IDs and confirmed data isolation.
- Verified that duplicate phone numbers are blocked within the same tenant.