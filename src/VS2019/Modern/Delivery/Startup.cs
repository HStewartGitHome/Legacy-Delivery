using Delivery.Areas.Identity;
using Delivery.Data;
using DeliverySupport.Data;
using DeliverySupport.Data.Sim;
using DeliverySupport.DataAccess;
using DeliverySupport.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Delivery
{
    public class Startup
    {
        public static bool UseAuth = true;
        public static bool UseSerilog = true;
        public static bool UseSimData = true;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (UseAuth == true)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            }

            services.AddRazorPages();
            services.AddServerSideBlazor();

            if (UseAuth == true)
            {
                services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            }
            if (UseSimData == true)
            {
                services.AddSingleton<IItemDataService, ItemSimDataService>();
                services.AddSingleton<IOrderDataService, OrderSimDataService>();
                services.AddSingleton<IMessageDataService, MessageSimDataService>();
                services.AddSingleton<ICategoryDataService, CategorySimDataService>();
            }
            else
            {
                services.AddScoped<ISqlDataAccess, SqlDataAccess>();
                services.AddScoped<IItemDataService, ItemSqlDataService>();
                services.AddScoped<IOrderDataService, OrderSqlDataService>();
                services.AddScoped<IMessageDataService, MessageSqlDataService>();
                services.AddScoped<ICategoryDataService, CategorySqlDataService>();
            }
            services.AddScoped<IOrderCart, OrderCart>();
            services.AddScoped<IDataCleanup, DataCleanup>();
            services.AddScoped<INextImage, NextImage>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            if (UseAuth == true)
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }

            app.UseEndpoints(endpoints =>
            {
                if (UseAuth == true)
                {
                    endpoints.MapControllers();
                }
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
