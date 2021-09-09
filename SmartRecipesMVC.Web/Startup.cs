using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartRecipesMVC.Application;
using SmartRecipesMVC.Application.ViewModels.IngredientVm;
using SmartRecipesMVC.Application.ViewModels.RecipeVm;
using SmartRecipesMVC.Domain.Model;
using SmartRecipesMVC.Domain.Model.Connections;
using SmartRecipesMVC.Infrastructure;
using SmartRecipesMVC.Web.Services;

namespace SmartRecipesMVC.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DATABASE
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // RAZOR
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddFluentValidation(fv =>
                {
                    fv.DisableDataAnnotationsValidation = true;
                    fv.ImplicitlyValidateChildProperties = true;
                });

            // DEFAULT IDENTITY
            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Context>();

            // DEPENDENCY INJECTION
            services.AddApplication();
            services.AddApplication();
            services.AddInfrastructure();

            //FLUENT VALIDATION
            services.AddTransient<IValidator<NewRecipeVm>, NewRecipeValidation>();

            // POLICY
            services.AddAuthorization(option =>
            {

            });

            //REGISTER AND LOGIN RULES
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = false;
            });

            // GOOGLE AUTHENTICATION
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                IConfigurationSection googConfigurationSection = Configuration.GetSection("Authentication:Google");
                googleOptions.ClientId = googConfigurationSection["ClientId"];
                googleOptions.ClientSecret = googConfigurationSection["ClientSecret"];
            });

            // FACEBOOK AUTHENTICATION
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                IConfigurationSection facebookConfigurationSection = Configuration.GetSection("Authentication:Facebook");
                facebookOptions.AppId = facebookConfigurationSection["AppId"];
                facebookOptions.AppSecret = facebookConfigurationSection["AppSecret"];
            });

            // EMAIL SENDER (SENDGRID)
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/myLog-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
