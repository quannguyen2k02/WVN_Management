using BusinessLogicLayer.ExternalServices.Mapper;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Cấu hình Database ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// --- 2. Đăng ký Services ---
builder.Services.AddControllers();

// XÓA DÒNG NÀY NẾU CÓ: builder.Services.AddOpenApi(); 
// Dòng trên là của .NET 9, gây xung đột với Swashbuckle.

// Kích hoạt trình khám phá Endpoints (Bắt buộc cho Swashbuckle)
builder.Services.AddEndpointsApiExplorer();

// Cấu hình Swagger Generation (Swashbuckle)
builder.Services.AddSwaggerGen();

// Cấu hình DbContext cho MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
});

builder.Services.AddScoped<ILedModelRepository, LedModelRepository>();
builder.Services.AddScoped<ILedModelService, LedModelService>();
builder.Services.AddScoped<ILedRepository, LedRepository>();
builder.Services.AddScoped<ILineRepository, LineRepository>();

builder.Services.AddAutoMapper(typeof(ApplicationMapper));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Tạo file swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Led Model API V1");
        c.RoutePrefix = "swagger"; // Truy cập tại: https://localhost:xxxx/swagger
    });

    // XÓA DÒNG NÀY NẾU CÓ: app.MapOpenApi();
}
app.UseSwagger(); // Tạo file swagger.json
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Led Model API V1");
    c.RoutePrefix = "swagger"; // Truy cập tại: https://localhost:xxxx/swagger
});
app.UseHttpsRedirection();
app.UseAuthorization();

// Ánh xạ Controllers
// Đảm bảo các Controller có [ApiController] và [Route]
app.MapControllers();

app.Run();