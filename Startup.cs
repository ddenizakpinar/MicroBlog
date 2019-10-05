using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroBlog.Models;
using MicroBlog.Repository.Abstract;
using MicroBlog.Repository.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MicroBlog
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=blog.db"));
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
