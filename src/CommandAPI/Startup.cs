using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Models;
using CommandAPI.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get;}
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

            IConfigurationSection configurationSection = Configuration.GetSection("CosmosDB");
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;

            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account,key);
            CosmosClient client = clientBuilder
                                .WithConnectionModeDirect()
                                .Build();

            DatabaseResponse database = client.CreateDatabaseIfNotExistsAsync(databaseName).GetAwaiter().GetResult();
            database.Database.CreateContainerIfNotExistsAsync(containerName,"/id").GetAwaiter().GetResult();

            SqlCommandAPIRepo commandsService = new SqlCommandAPIRepo(client,databaseName,containerName);
            services.AddSingleton<ICommandAPIRepo>(commandsService);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllers();
            });
        }


        // private static async Task<CommandContext> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        // {
        //     string databaseName = configurationSection.GetSection("DatabaseName").Value;
        //     string containerName = configurationSection.GetSection("ContainerName").Value;
        //     string account = configurationSection.GetSection("Account").Value;
        //     string key = configurationSection.GetSection("Key").Value;
        //     Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
        //     CommandContext cosmosDbService = new CommandContext(client, databaseName, containerName);
        //     Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        //     await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

        //     return cosmosDbService;
        // }

        
    }
}
