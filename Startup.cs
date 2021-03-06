using bettingRouletteAPI.Helpers.Configuration;
using bettingRouletteAPI.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
namespace bettingRouletteAPI
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
            services.AddControllers();
            services.Configure<RouletteDatabaseSettings>(Configuration.GetSection(nameof(RouletteDatabaseSettings)));
            services.AddSingleton<IRouletteDatabaseSettings>(sp => sp.GetRequiredService<IOptions<RouletteDatabaseSettings>>().Value);
            services.AddSingleton<BetsModel>();
            services.AddSingleton<RoulettesModel>();
            services.AddSingleton<TokensModel>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
