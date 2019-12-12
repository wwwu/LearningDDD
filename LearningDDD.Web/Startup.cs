using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using LearningDDD.Application.AutoMapper;
using LearningDDD.Domain.IRepository;
using LearningDDD.Infrastructure.Repository;
using LearningDDD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using LearningDDD.Domain.Bus;
using LearningDDD.Infrastructure.Bus;
using LearningDDD.Domain.Commands.User;
using LearningDDD.Domain.CommandHandlers;
using LearningDDD.Domain.Events.User;
using LearningDDD.Domain.EventHandlers;
using LearningDDD.Domain.Notifications;
using LearningDDD.Infrastructure.EventSourcing;
using LearningDDD.Domain.Events;

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
                cfg.AddProfile(new EntityToDtoMappingProfile());
                cfg.AddProfile(new DtoToDomainMappingProfile());
            });
            services.AddSingleton(mapperConfig);
            services.AddScoped<IMapper, Mapper>();

            //Application Service
            foreach (var item in GetClassAndInterface("LearningDDD.Application", s => s.Name.EndsWith("AppService")))
                services.AddScoped(item.Value, item.Key);
            //Repository
            foreach (var item in GetClassAndInterface("LearningDDD.Infrastructure", s => s.Name.EndsWith("Repository")))
                services.AddScoped(item.Value, item.Key);
            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //MediatR
            services.AddMediatR(typeof(Event));
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            //Domain - Commands
            services.AddScoped<IRequestHandler<CreateUserCommand, Unit>, UserCommandHandlers>();
            services.AddScoped<IRequestHandler<RemoveUserCommand, Unit>, UserCommandHandlers>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, Unit>, UserCommandHandlers>();
            //Domain - Events
            services.AddScoped<INotificationHandler<UserCreatedEvent>, UserEventHandler>();
            //领域通知
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            //EventSourcing
            services.AddScoped<IEventStoreService, EventStoreService>();

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
