using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BankApplication.Data;
using BankApplication.Models;
using BankApplication.Services;
using BankApplication.Repositories;
using BankApplication.BusinessLogic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace BankApplication
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IBranchManager, BranchManager>();
            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddEventLog();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");

                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                                        async context =>
                                        {
                                            context.Response.ContentType = "text/html";
                                            var err = $"<h2>Bank application is experiencing issues. Please log a support ticket with the Global IT Service Desk</h2>";
                                            await context.Response.WriteAsync(err).ConfigureAwait(false);
                                        });
                    });
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            MapConfig.Initialize();

            CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            var adminRoleCheck = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleCheck)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var customerRoleCheck = await roleManager.RoleExistsAsync("Customer");
            if (!customerRoleCheck)
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }
        }
    }
}
