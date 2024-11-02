using Microsoft.EntityFrameworkCore;
using QLTraSua.Data;

var builder = WebApplication.CreateBuilder(args);

// Thêm IHttpContextAccessor
builder.Services.AddHttpContextAccessor();


// Thêm cấu hình DbContext cho Entity Framework Core
builder.Services.AddDbContext<QLTraSuaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLTraSuaContext"))
);

// Thêm các dịch vụ vào container
builder.Services.AddControllersWithViews(); // Cấu hình dịch vụ cho MVC
builder.Services.AddSession(); // Thêm dịch vụ session

var app = builder.Build();

// Tạo và khởi tạo cơ sở dữ liệu
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<QLTraSuaContext>();
    dbContext.Seed(); // Gọi phương thức Seed để khởi tạo dữ liệu nếu cần
}

// Thiết lập middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Chuyển hướng HTTP sang HTTPS
app.UseStaticFiles(); // Cho phép phục vụ file tĩnh

app.UseRouting(); // Kích hoạt định tuyến

app.UseSession(); // Kích hoạt session

app.UseAuthentication(); // Kích hoạt xác thực (nếu có)
app.UseAuthorization(); // Kích hoạt phân quyền

// Định nghĩa các điểm kết thúc (endpoints)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"); // Điểm kết thúc mặc định



app.Run(); // Chạy ứng dụng
