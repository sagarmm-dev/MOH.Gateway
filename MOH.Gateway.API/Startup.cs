using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MOH.Gateway.API.Models;
using MOH.Gateway.Common;
using MOH.Gateway.Services;
using MOH.Gateway.Services.Interfaces;
using MOH.Gateway.Services.Model;

namespace MOH.Gateway.API
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
            services.AddControllers(config =>
            {
                //config.Filters.Add(new RouteCheckFilter());
            });
            // Add our Config object so it can be injected
            services.AddHttpClient();
            services.AddSingleton<IHttpHelper, HttpHelper>();

            services.Configure<NoAuth>(Configuration.GetSection("NoAuth"));
            services.Configure<ApiService>(Configuration.GetSection("ApiService"));
            services.AddScoped<IRoutingService, RoutingService>();
            services.AddScoped<IRequestHeaderHandler, RequestHeaderHandler>();
            services.AddScoped<IRequestBodyHandler, RequestBodyHandler>();
            services.AddScoped<IRequestUriHandler, RequestUriHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
