using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using GEEKecommerce_API.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GEEK-Ecommerce API",
        Version = "v1",
        Description = "API Documentation for GEEK-Ecommerce"
    });
});

builder.Services.AddControllers();

// Cấu hình DbContext với SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
        sqlOptions.CommandTimeout(180) // Timeout cho câu lệnh SQL
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GEEK-Commerce API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Xử lý lỗi
    app.UseHsts(); // Sử dụng HSTS để tăng cường bảo mật
}

// Cấu hình CORS
app.UseCors("AllowAll");

// Middleware mặc định
app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing và Authorization
app.UseRouting();
app.UseAuthorization();

// Cấu hình endpoint cho Controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Chạy ứng dụng
app.Run();

