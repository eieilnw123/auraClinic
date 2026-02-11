using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Services;

namespace Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenantProvider) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ข้อมูลจะถูกกรองตาม TenantId อัตโนมัติทุกครั้งที่ดึงข้อมูล
        modelBuilder.Entity<Patient>().HasQueryFilter(p => p.TenantId == tenantProvider.GetTenantId());

        // เบอร์โทรห้ามซ้ำภายในคลินิกเดียวกัน
        modelBuilder.Entity<Patient>()
            .HasIndex(p => new { p.TenantId, p.PhoneNumber })
            .IsUnique();
    }
}