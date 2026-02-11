using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(
    AppDbContext db,
    ITenantProvider tenantProvider,
    IDistributedCache cache) : ControllerBase
{
    // --- A2. Create Patient ---
    [HttpPost]
    public async Task<IActionResult> Create(Patient patient)
    {
        // B2. Enforcement: Viewer ห้ามสร้าง Patient
        var role = Request.Headers["X-User-Role"].ToString();
        if (role == "Viewer")
            return Forbid("Viewers are not allowed to create patients.");

        // กำหนด TenantId ให้กับข้อมูลคนไข้ตามที่ดึงได้จาก Header
        patient.TenantId = tenantProvider.GetTenantId();

        try
        {
            db.Patients.Add(patient);
            await db.SaveChangesAsync();

            // D3. Invalidation: ล้าง Cache เมื่อมีการสร้างคนไข้ใหม่
            await cache.RemoveAsync($"patients_list_{patient.TenantId}");

            return Ok(patient);
        }
        catch (DbUpdateException)
        {
            // ส่ง Safe Error เมื่อเบอร์โทรซ้ำภายใน Tenant เดียวกัน
            return BadRequest("เบอร์โทรศัพท์นี้ถูกใช้งานแล้วในคลินิกของคุณ");
        }
    }

    // --- A3. List Patients ---
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] Guid? branchId)
    {
        var tenantId = tenantProvider.GetTenantId();
        var cacheKey = $"patients_list_{tenantId}";

        // D1. Caching: ตรวจสอบใน Redis ก่อน
        var cachedData = await cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            return Ok(JsonSerializer.Deserialize<List<Patient>>(cachedData));
        }

        // ดึงข้อมูล (Global Query Filter ใน AppDbContext จะกรอง Tenant ให้เองโดยอัตโนมัติ)
        var query = db.Patients.AsQueryable();

        if (branchId.HasValue)
            query = query.Where(p => p.PrimaryBranchId == branchId);

        var list = await query
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        // เก็บข้อมูลลง Cache เป็นเวลา 5 นาที
        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(list), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });

        return Ok(list);
    }
}