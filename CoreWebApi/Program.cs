using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApi.Model;
using CoreWebApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreWebApi
{
    public class Program
    {
        static INavigationService navService;
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            host.Run();

            //TransientDisposablesWithoutDispose();

            //IHost host1 = CreateHostBuilder_1(args).Build();
            //ExemplifyDisposableScoping(host1.Services, "Scope 1");
            Console.WriteLine();

            //ExemplifyDisposableScoping(host.Services, "Scope 2");
            //Console.WriteLine();

            //Console.ReadLine();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureServices(services =>
                //{
                //    services.AddScoped<IRepoService, RepoService>();
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static IHostBuilder CreateHostBuilder_1(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<TransientDisposable>()
                            .AddScoped<ScopedDisposable>()
                            .AddSingleton<SingletonDisposable>());
        }

        static void TransientDisposablesWithoutDispose()
        {
            INavigationService locService = new NavigationService();
            navService = locService;

            var services = new ServiceCollection();
            services.AddTransient<TransientDisposable>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            for (int i = 0; i < 5; ++i)
            {
                _ = serviceProvider.GetRequiredService<TransientDisposable>();
            }

            //serviceProvider.Dispose();
        }

        static void ExemplifyDisposableScoping(IServiceProvider services, string scope)
        {
            Console.WriteLine($"{scope}...");

            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            /*  TransientDisposable and ScopedDisposable not get disposed,
             *  once this method ends, they are only disposed once root IServiceProvider
             *  is disposed.
             */
            _ = services.GetRequiredService<TransientDisposable>();
            _ = services.GetRequiredService<ScopedDisposable>();
            _ = services.GetRequiredService<SingletonDisposable>();

            //IServiceScope scopeInst1 = serviceScopeFactory.CreateScope();
            //provider = scopeInst1.ServiceProvider;

            /*
             * Scoped ServiceProvider, once IServiceScope disposed, 
             * TransientDisposable and ScopedDisposable get disposed
             */
            _ = provider.GetRequiredService<TransientDisposable>();
            _ = provider.GetRequiredService<ScopedDisposable>();
            _ = provider.GetRequiredService<SingletonDisposable>();
        }
    }
}
