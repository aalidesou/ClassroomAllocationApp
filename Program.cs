using ClassroomAllocationApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ClassroomAllocationApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddEndpointsApiExplorer(); // Needed for Swagger
            builder.Services.AddSwaggerGen();           // Needed for Swagger

            // Add SQLite DB
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            
            
            // Add Razor Pages and set default route to Login
            builder.Services.AddRazorPages(options =>
            {
                // Route root ("/") to the Login page
                options.Conventions.AddPageRoute("/Account/Login", "");
                // Optional: also allow /login
                options.Conventions.AddPageRoute("/Account/Login", "login");
            });

            var app = builder.Build();

            // Enable Swagger for both Development and Production
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Classroom Allocation API v1");
                c.RoutePrefix = "swagger"; // URL will be /swagger
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthorization();

            // Map Razor Pages and API Controllers
           /* app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");*/
            app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }
}
