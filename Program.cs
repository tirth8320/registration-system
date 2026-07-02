using Microsoft.EntityFrameworkCore;
using registration.Data;
using registration.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<EmailServices>();

builder.Services.AddScoped<ErrorLogService>();

// PostgreSQL Connection
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;

    options.Cookie.SecurePolicy =
       CookieSecurePolicy.SameAsRequest;

    options.Cookie.SameSite =
        SameSiteMode.Lax;

    options.Cookie.Name =
        "Registration.Session";
});

var app = builder.Build();

//Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStatusCodePagesWithReExecute("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] =
        "no-cache, no-store, must-revalidate";

    context.Response.Headers["Pragma"] = "no-cache";

    context.Response.Headers["Expires"] = "0";

    await next();
});

app.UseMiddleware<registration.Middleware.GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();