using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateWithEfDemo.Db;
using HotChocolateWithEfDemo.Domain;
using HotChocolateWithEfDemo.GraphqlApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace HotChocolateWithEfDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProjectsDbContext>(opts =>
            {
                opts
                    .UseLazyLoadingProxies()
                    // .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine)
                    .UseNpgsql(Configuration.GetConnectionString("postgresql"));
            });
            services
                .AddGraphQLServer()
                .AddQueryType(d=>d.Name("Query"))
                .AddTypeExtension<ProjectQuery>()
                .AddTypeExtension<DevelopersQuery>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddMutationType(d=>d.Name("Mutation"))
                .AddTypeExtension<ProjectMutations>()
                .AddInMemorySubscriptions()
                .AddSubscriptionType(d => d.Name("Subscription"))
                .AddTypeExtension<TaskSubscriptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
            
            DbSchemaUpdater.UpgradeDb(Configuration.GetConnectionString("postgresql"));
            
            DbInitializer.Init(app);
        }
    }
}