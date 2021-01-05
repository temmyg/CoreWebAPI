using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApi.Model;
using CoreWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

namespace CoreWebApi
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
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();
            services.AddOptions<CookieAuthenticationOptions>(
                        CookieAuthenticationDefaults.AuthenticationScheme)
            .Configure<INavigationService>((options, navService) =>
            {
                options.LoginPath = navService.GetLoginPath();
            });

            services.AddScoped<IRepoService>(sp => new RepoService());
            services.AddScoped<IBankAccount, PremiumBankAccount>();
            services.AddScoped<IBankAccount, PrestigiousBankAccount>();

            services.AddScoped<FillingItem>();
            services.AddScoped<FillingItem>();

            services.AddTransient<IItemFinder, WirelessItemFinder>();

            services.AddCors(options =>
            {
                options.AddPolicy("enableWholeSaleMngmnt", builder => builder.WithOrigins("http://localhost:4202").AllowAnyMethod().AllowAnyHeader());
                /*
                 * below options.AddPolicy statement not working for CORS call, because browser preflight OPTIONS request asking for permission to send 
                 * Access-Control-Request-Headers: content-type "content-type", if server side not allowing header's content-type sent from the reply
                 * browser will not send the actual post request, as the post request need to send content-type header. Request is like below:
                 * OPTIONS http://localhost:3221/api/WholeSale/is-active-invoice HTTP/1.1
                    Host: localhost:3221
                    Connection: keep-alive
                    Accept: * / *
                    Access-Control-Request-Method: POST
                    Access-Control-Request-Headers: content-type
                    ....
                  * Reply is like below, server allows content-type to be sent, with Access-Control-Allow-Headers: content-type
                    HTTP/1.1 204 No Content
                    Server: Microsoft-IIS/10.0
                    Access-Control-Allow-Origin: http://localhost:4202
                    Access-Control-Allow-Headers: content-type
                    Access-Control-Allow-Methods: POST
                    X-Powered-By: ASP.NET
                    Date: Mon, 28 Dec 2020 06:21:19 GMT
                  *
                  */
                // options.AddPolicy("enableWholeSaleMngmnt", builder => builder.WithOrigins("http://localhost:4202").AllowAnyMethod());
            });
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustomActionFilter1));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("enableWholeSaleMngmnt");

            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
		        endpoints.MapControllerRoute(name: "blog",
                			             pattern: "blog/{*article}",
			                             defaults: new { controller = "Blog", action = "Article" });
		        endpoints.MapControllerRoute(name: "default",
                			             pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
