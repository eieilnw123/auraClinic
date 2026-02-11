using Backend.Models;

namespace Backend.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Patients.Any()) return;

        // สร้าง Tenant ID ตัวอย่าง
        var myTenant = Guid.Parse("11111111-1111-1111-1111-111111111111");

        context.Patients.Add(new Patient
        {
            FirstName = "Somsak",
            LastName = "Jaidee",
            PhoneNumber = "0812345678",
            TenantId = myTenant
        });

        context.SaveChanges();
    }
}