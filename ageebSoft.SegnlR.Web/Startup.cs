﻿using System;
using System.Linq;
using System.Security.Claims;
using ageebSoft.SignlR.Core.Models;
using ageebSoft.SignlR.Web.Models.data;
using ageebSoft.SignlR.Web.Models.DB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ageebSoft.SignlR.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public virtual void SetupDatabase(IServiceCollection services)
        {
            services.AddDbContext<MyDB>();

            // services.AddIdentity<MyUser, IdentityRole>();
            /*.AddEntityFrameworkStores<MyDB>()
            .AddDefaultTokenProviders();*/
        }

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
            SetupDatabase(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddSignalR();
           // services.AddSingleton<IUserIdProvider, NameUserIdProvider>();




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

            app.Use(async (context, next) =>
            {
                string userName = context.Request.Query["userName"];
                //string userName2 = context.Request.Body["userName"];
                //string userName2 = context.Request.Form["userName"];
                if (!string.IsNullOrEmpty(userName) && userName != "undefined" && !userName.Equals("null"))
                {
                    MyDB mydb = new MyDB();
                    MyUser user = null;
                    try { user = mydb.MyUsers.FirstOrDefault(x => x.UserName == userName); } catch { }
                    try
                    {
                        if (user == null)
                        {
                            user = new MyUser { UserName = userName, Id = Guid.NewGuid(), Date1 = DateTime.Now, Note = "fromWebSite" };
                            mydb.Add(user);
                            mydb.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {


                    }

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
