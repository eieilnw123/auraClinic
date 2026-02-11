using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
var builder = WebApplication.CreateBuilder(args);

// --- 1. Basic Services ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// --- 2. CORS (Section B) ---
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// --- 3. Caching (Section D - Redis) ---
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration["Redis:Configuration"] ?? "redis:6379";
});

// --- Section C: Messaging (RabbitMQ) แบบ Minimal ---
builder.Services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
{
    HostName = "rabbitmq",
    UserName = "guest",
    Password = "guest",
    // ลบ DispatchConsumersAsync ออกเพื่อให้รองรับ v7.0+
    AutomaticRecoveryEnabled = true
});

// --- 5. Database (Section A - PostgreSQL) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Host=db;Database=clinic_pos;Username=admin;Password=password123";
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// --- 6. Auth & Tenant Services ---
builder.Services.AddScoped<ITenantProvider, TenantProvider>();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clinic POS API", Version = "v1" });
    c.AddSecurityDefinition("TenantId", new OpenApiSecurityScheme
    {
        Name = "X-Tenant-Id",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header
    });
});

var app = builder.Build();

// --- 7. Middleware Pipeline ---
app.UseRouting();
app.UseCors("AllowAll"); // ต้องอยู่หลัง Routing เสมอ

if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// --- 8. DB Auto-Migration & Seeding ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();