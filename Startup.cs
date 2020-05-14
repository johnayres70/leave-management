using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using leave_management.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using leave_management.Repository;
using leave_management.Contracts;
using AutoMapper;
using leave_management.Mappings;

namespace leave_management
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // jpa added next 3 references for repository and contracts file
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
            services.AddScoped<ILeaveHistoryRepository, LeaveHistoryRepository>();

            // jpa added automapper reference to middleware
            services.AddAutoMapper(typeof(Maps));

            // jpa disable confirm email address in User Identity
            //   services.AddDefaultIdentity<Employee>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            // jpa added Identity Role
            // jpa change to Emplyee or you get InvalidOperationException: Unable to resolve service for type 'Microsoft.AspNetCore.Identity.UserManager`1[leave_management.Data.Employee]' while attempting to activate 'leave_management.Controllers
            // .. error
            // services.AddDefaultIdentity<Employee>()
           services.AddDefaultIdentity<Employee>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // jpa added new parameters UserManager<Employee> userManager,  RoleManager<IdentityRole> roleManager
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            // jpa     avoide  above invalid operation error
           //  UserManager<Employee> userManager,
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // jpa added for default admin role
           // SeedData.Seed(userManager, roleManager);
            SeedData.Seed(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
