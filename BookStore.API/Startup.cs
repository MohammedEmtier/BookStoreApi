using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Hangfire;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using EFCoreDb;
using System;

namespace BookStore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.EntityFrameWorkDbSevices(Configuration).AddApplicationServices(Configuration)
                .AddCustomAuthentication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.CustomApplicationServicesBuilder().CustomAuthenticationBuilder();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //Register Dependences
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAutherRepoistory<Auther>, AutherRepoistory>();
            // Configuration
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            // add Localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                CultureInfo[] suppourtLocale = new[]{
                new CultureInfo("en"),
                new CultureInfo("ar")
            };
                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                options.SupportedCultures = suppourtLocale;
                options.SupportedUICultures = suppourtLocale;
            });
            services.AddMvc().AddDataAnnotationsLocalization();
            /// enable Cors
            services.AddCors(options => options.AddPolicy("DefalutPolicy", op => op.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            // register Nuget Packages
            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(typeof(Startup));
            return services;
        }
        public static IApplicationBuilder CustomApplicationServicesBuilder(this IApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            builder.UseHttpsRedirection();
            builder.UseHangfireDashboard(pathMatch: "/dashboard");
            builder.UseRouting();
            IOptions<RequestLocalizationOptions> locOptions = builder.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            builder.UseRequestLocalization(locOptions.Value);
            return builder;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(option =>
               {
                   option.SaveToken = true;
                   option.RequireHttpsMetadata = false;
                   option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidAudience = Configuration["JWT:ValidAudience"],
                       ValidIssuer = Configuration["JWT:ValidIssuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]))
                   };
               });
            return services;
        }
        public static IApplicationBuilder CustomAuthenticationBuilder(this IApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            builder.UseAuthentication();
            builder.UseAuthorization();
            return builder;
        }

    }
}
