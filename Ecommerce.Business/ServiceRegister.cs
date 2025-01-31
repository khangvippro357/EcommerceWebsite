﻿using Ecommerce.Business.Interfaces;
using Ecommerce.Business.Services;
using Ecommerce.DataAccessor;
using Ecommerce.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

using System.Reflection;


namespace Ecommerce.Business
{
    public static class ServiceRegister
    {
        public static void AddBusinessLayer(this IServiceCollection services, IConfiguration configuration )
        {
           services.AddDataAccessorLayer(configuration);
           services.AddIdentityLayer(configuration);
           services.AddAutoMapper(Assembly.GetExecutingAssembly());
           services.AddTransient<ICategoryService, CategoryService>();
           services.AddTransient<IProductService, ProductService>();
           services.AddTransient<IProductImageService, ProductImageService>();
           services.AddTransient<IOrganicIdentityUserService, OrganicIdentityUserService>();
           services.AddTransient<IOrderService, OrderService>();
           services.AddTransient<IOrderDetailService, OrderDetailService>();
           services.AddTransient<IRatingService, RatingService>();
        }
    }
}
