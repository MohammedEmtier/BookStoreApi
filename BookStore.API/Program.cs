using BookStore.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Webhost = CreateHostBuilder(args).Build();
            RunMigration(Webhost);
            Webhost.Run();
        }

        private static void RunMigration(IHost webhost)
        {
            using (var socpe = webhost.Services.CreateScope())
            {
                var db = socpe.ServiceProvider.GetRequiredService<BookStoreContext>();
                db.Database.Migrate();
            }

        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
