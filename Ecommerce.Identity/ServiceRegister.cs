﻿using Ecommerce.DataAccessor.Entities;
using Ecommerce.Identity.Data;
using IdentityServer4.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Identity
{
    public static class ServiceRegister
    {
        public static void AddIdentityLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProfileService, ProfileService>();
            services.AddDbContext<AspNetIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b =>
                    b.MigrationsAssembly(typeof(AspNetIdentityDbContext).Assembly.FullName)
                ));
            services.AddIdentityCore<EcommerceUser>()
                .AddUserManager<UserManager<EcommerceUser>>()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

        }
    }
}
