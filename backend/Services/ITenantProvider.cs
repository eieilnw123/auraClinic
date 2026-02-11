namespace Backend.Services;

public interface ITenantProvider { Guid GetTenantId(); }

public class TenantProvider(IHttpContextAccessor accessor) : ITenantProvider
{
    public Guid GetTenantId()
    {
        var header = accessor.HttpContext?.Request.Headers["X-Tenant-Id"].ToString();
        return Guid.TryParse(header, out var id) ? id : Guid.Empty;
    }
}