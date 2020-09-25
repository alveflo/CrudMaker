using Crudify.TestHost.Database;
using Crudify.TestHost.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Crudify.TestHost
{
    public class TestDto { }

    public class TestEntity { }

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
            services.AddDbContext<TestDbContext>();

            services.AddControllers();
            //services.AddCrud<BlogDto, Blog>("/test");
            //services.AddCrud<PostDto, Post>("/monkeys");

            services.AddCrud(options =>
            {
                options.Add<BlogDto, Blog>("/blogs");
                options.Add<PostDto, Post>("/posts");
            });
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

            //app.AddCrud<BlogDto, Blog, TestDbContext>("/test");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
