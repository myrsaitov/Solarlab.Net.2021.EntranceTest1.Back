using System;
using System.Threading.Tasks;
using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Contracts.Models;
using BusinessLogic.Services;
using BusinessLogic.Services.Mapping;
using DataAccess.Repositories.Abstractions;
using DataAccess.Context;
using DataAccess.Context.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;



namespace Dashboard.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            Installer.ConfigureDbContext(services);

            services.AddAutoMapper(typeof(ServiceMappings));
            //services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

            var serviceProvider = services
                .AddTransient<IMyEventService, MyEventService>()
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<ICommentRepository, CommentsRepository>()
                .AddTransient<ITagRepository, TagRepository>()
                .AddTransient<IMyEventTagRepository, MyEventTagRepository>()
                .AddTransient<IMyEventRepository, MyEventRepository>()
                .BuildServiceProvider();
            System.Console.WriteLine("Hello World!");
            var P = serviceProvider.GetService<Context>();
            System.Console.ReadKey();
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceMappings>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}
