using Backend.Models;

namespace Backend.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        // 1. สร้าง Tenant ID ตัวอย่าง
        var myTenant = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // 2. Seed Branches (โจทย์ B4: ต้องมี 2 สาขา)
        if (!context.Branches.Any())
        {
            context.Branches.AddRange(
                new Branch { Id = Guid.NewGuid(), Name = "Siam Square Branch", TenantId = myTenant },
                new Branch { Id = Guid.NewGuid(), Name = "Mega Bangna Branch", TenantId = myTenant }
            );
        }

        // 3. Seed Users (โจทย์ B1 & B4: ต้องมี 3 Roles)
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Username = "admin@auraclinic.com", Role = "Admin", TenantId = myTenant },
                new User { Username = "staff@auraclinic.com", Role = "User", TenantId = myTenant },
                new User { Username = "audit@auraclinic.com", Role = "Viewer", TenantId = myTenant }
            );
        }

        // 4. Seed Patients (โจทย์ A1)
        if (!context.Patients.Any())
        {
            context.Patients.Add(new Patient
            {
                FirstName = "Somsak",
                LastName = "Jaidee",
                PhoneNumber = "0812345678",
                TenantId = myTenant
            });
        }

        context.SaveChanges();
    }
}