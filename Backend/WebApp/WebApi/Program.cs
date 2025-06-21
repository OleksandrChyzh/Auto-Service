using API;
using API.Middlewares;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureAuthorization(builder.Configuration)
    .ConfigureCors()
    .ConfigureSwagger()
    .ConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedAdminUserAsync(services);
}

app.Run();
static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

    string adminEmail = "admin@example.com";
    string adminPassword = "Admin1234"; // має відповідати вимогам

    // Створити роль, якщо ще не існує
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole<int>("Admin"));
    }

    // Перевірити, чи існує користувач
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new User
        {
            UserName = "admin",
            Email = adminEmail,
            EmailConfirmed = true,
            PhoneNumber="+380664455334"
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            throw new Exception("Не вдалося створити адміністратора: " +
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}