using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppRunning.Data;
using WebAppRunning.Models;
using WebAppRunning.Services;
using ITSRunningDbRepository;

namespace WebAppRunning
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RunnerAuthenticationConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            var cs = Configuration.GetConnectionString("RunnersDbConnectionString");

            //singleton for runner repository
            services.AddSingleton<IRunnerRepository>(new RunnerRepository(cs));

            //singleton for activity repository
            services.AddSingleton<IActivityRepository>(new ActivityRepository(cs));

            //singleton for race repository
            services.AddSingleton<IRaceRepository>(new RaceRepository(cs));

            //singleton for telemetry repository
            services.AddSingleton<ITelemetryRepository>(new TelemetryRepository(cs));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Map}/{action=Index}/{id?}");
            });
        }
    }
}
