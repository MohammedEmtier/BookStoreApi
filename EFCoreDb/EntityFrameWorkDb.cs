using System;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;


namespace EFCoreDb
{
    public static class EntityFrameWorkDb
    {
        public static IServiceCollection EntityFrameWorkDbSevices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookStoreContext>(
                options =>
                {

                    options.UseSqlServer(configuration.GetConnectionString("BookStoreDB"), sqlServerOptionsAction:
                      b => b.MigrationsAssembly("BookStore.API"));
                });
            //sqlServerOptionsAction: sqlOptions => sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(20), errorNumbersToAdd: null));
            services.AddHangfireServer();
            services.AddHangfire(e => e.UseSqlServerStorage(configuration.GetConnectionString("BookStoreDB")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<BookStoreContext>()
              .AddDefaultTokenProviders();
            return services;
        }
    }
}
