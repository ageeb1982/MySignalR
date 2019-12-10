using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ageebSoft.SignlR.Core.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ageebSoft.SignlR.Core
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
            //services.AddHostedService<BGServiceStarter<MyHubBackgroundService>>();
            // services.AddHostedService<MyHubBackgroundService>();
            //  services.AddHostedService<NotificatioBackgroundService>();
            //services.AddSingleton<BGServiceStarter<MyHubBackgroundService>>();
            //IHubContext<MyHub>
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthentication();
            services.AddAuthorization();

            SingleAddRedisService(services);
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();




        }

        public virtual void SingleAddRedisService(IServiceCollection services)
        {
            services.AddSignalR().AddRedis(MyHub.ConnectionString);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
           
            //app.UseCors(CorsOptions.AllowAll);

            //لعمل محاكاة لمستخدم قام بتسجيل الدخول حتى نجرب العمل
           
            app.Use(async (context,next)=>
            {
                string userName = context.Request.Query["userName"];
                //string userName2 = context.Request.Body["userName"];
                //string userName2 = context.Request.Form["userName"];
                if(!string.IsNullOrEmpty(userName) && userName!= "undefined" && !userName.Equals("null"))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "ChatRole"));

                    if (userName.ToLower().Contains("admin"))
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                        
                    }
                    context.User = new ClaimsPrincipal(identity);
                }
                await next();
            }
            );
            //app.UseCors("AllowCors");
            app.UseSignalR((routes) =>
            {

                routes.MapHub<MyHub>("/MyHub");
                routes.MapHub<NotificationHub>("/Notifi");
                // routes.MapHub<CalcHub>("/calcHub");

            });
            
           
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
