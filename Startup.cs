namespace Formary
{
    using System.Threading.Tasks;
    using Formary.Models;
    using Formary.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var configDbSection = Configuration.GetSection("CosmosDb");
            var cosmosClient = InitializeCosmosClientInstance(configDbSection);
            services.AddSingleton(cosmosClient);

            var database = CreateCosmosDatabaseAsync(cosmosClient, configDbSection).GetAwaiter().GetResult();
            services.AddSingleton(database);

            services.AddSingleton<IDeliveryService, DeliveryService>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IStepService, StepService>();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Customer}/{action=Index}/{id?}");
            });
        }
       
        private static CosmosClient InitializeCosmosClientInstance(IConfigurationSection configurationSection)
        {
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account, key);
            CosmosClient client = clientBuilder
                                .WithConnectionModeDirect()                                
                                .Build();            
            return client;
        }
        private static async Task<Database> CreateCosmosDatabaseAsync(CosmosClient client, IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            return await client.CreateDatabaseIfNotExistsAsync(databaseName);            
        }       
    }
}
