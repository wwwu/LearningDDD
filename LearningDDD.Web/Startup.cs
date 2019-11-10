﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using LearningDDD.Application.AutoMapper;
using LearningDDD.Application.Interface;
using LearningDDD.Application.Implement;
using LearningDDD.Domain.IRepository;
using LearningDDD.Infrastructure.Data.Repository;
using LearningDDD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using LearningDDD.Domain.Core.Bus;
using LearningDDD.Infrastructure.Data.Bus;
using LearningDDD.Domain.Commands.User;
using LearningDDD.Domain.CommandHandlers;

namespace LearningDDD.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region IOC组件注册

            //DbContext
            services.AddDbContextPool<UserContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:DefaultConnection"]), poolSize: 128);

            //AutoMapper
            AutoMapper.IConfigurationProvider mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
            services.AddSingleton(mapperConfig);
            services.AddScoped<IMapper, Mapper>();

            //Application Service
            foreach (var item in GetClassAndInterface("LearningDDD.Application", s => s.Name.EndsWith("AppService")))
                services.AddScoped(item.Value, item.Key);
            //Repository
            foreach (var item in GetClassAndInterface("LearningDDD.Infrastructure.Data", s => s.Name.EndsWith("Repository")))
                services.AddScoped(item.Value, item.Key);
            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //MediatR
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            // Domain - Commands
            services.AddScoped<IRequestHandler<CreateUserCommand, Unit>, UserCommandHandlers>();

            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static List<KeyValuePair<Type,Type>> GetClassAndInterface(string assemblyName, Func<Type, bool> func = null)
        {
            var result = new List<KeyValuePair<Type, Type>>();
            Assembly assembly = Assembly.Load(assemblyName);
            func = func ?? (s => !s.IsInterface);
            var classTypes = assembly.GetTypes().Where(s => !s.IsInterface).Where(func).ToList();
            foreach (var classType in classTypes)
            {
                foreach (var interfaceType in classType.GetInterfaces().Where(func))
                {
                    result.Add(new KeyValuePair<Type, Type>(classType, interfaceType));
                }
            }
            return result;
        }
    }
}
