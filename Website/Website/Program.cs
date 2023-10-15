using Microsoft.AspNetCore.Authentication.Cookies;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSession(options =>
        {
            // Установите тайм-аут сеанса (по умолчанию 20 минут)
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Пример: 30 минут

            // Установите имя куки-сеанса (по умолчанию ".AspNetCore.Session")
            options.Cookie.Name = "MySessionCookie";

            // Настройте параметры куки
            options.Cookie.HttpOnly = true; // Делает куки недоступными для скриптов на клиентской стороне
            options.Cookie.IsEssential = true; // Убедитесь, что куки сеанса всегда отправляются

            // Другие настройки, включая конфигурацию Redis, если необходимо
        });
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}