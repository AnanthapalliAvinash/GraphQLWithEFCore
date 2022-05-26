using GraphQLWithEFCore.Context;
using GraphQLWithEFCore.GraphQL;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWithEFCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(Configuration.GetConnectionString("TestDB")));
            services.AddScoped<Query>();
            services.AddGraphQL(p => SchemaBuilder.New().AddServices(p)
            .AddType<StudentType>()
            .AddQueryType<Query>()
            .Create());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UsePlayground(new PlaygroundOptions
                {
                    QueryPath = "/api",
                    Path = "/playground"
                });
            }
            app.UseGraphQL("/api");

            app.UseRouting();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",async context=>
                {
                    await context.Response.WriteAsync("Hello World");
                });
            });
        }
    }
}
