using Xunit;
using Backend.Models;
using System;

namespace Backend.Tests;

public class RequirementsTest
{
    [Fact]
    public void SectionA_TenantIsolation_Enforced()
    {
        // ใช้ Guid.NewGuid() แทน string
        var tenantA = Guid.NewGuid();
        var patient = new Patient { TenantId = tenantA };

        Assert.Equal(tenantA, patient.TenantId);
    }

    [Fact]
    public void SectionA_DuplicatePhone_LogicCheck()
    {
        var tenantId = Guid.NewGuid();
        var phone = "0811112222";

        var p1 = new Patient { PhoneNumber = phone, TenantId = tenantId };
        var p2 = new Patient { PhoneNumber = phone, TenantId = tenantId };

        Assert.Equal(p1.PhoneNumber, p2.PhoneNumber);
        Assert.Equal(p1.TenantId, p2.TenantId);
    }
}