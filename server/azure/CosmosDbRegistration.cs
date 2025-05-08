using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Azure.Identity;

namespace DemoService.Service
{
    /// <summary>
    /// Registration class for Azure Cosmos DB services and implementations
    /// </summary>
    public static class CosmosDbRegistration
    {
        /// <summary>
        /// Registers the Cosmos DB client and related services for dependency injection
        /// </summary>
        /// <param name="builder">The web application builder</param>
        public static void RegisterCosmosServices(this WebApplicationBuilder builder)
        {
            // Register the HttpContextAccessor for accessing the HTTP context
            builder.Services.AddHttpContextAccessor();
            
            // Get configuration settings
            var cosmosEndpoint = builder.Configuration["Configuration:AzureCosmosDb:Endpoint"];
            var cosmosDatabaseName = builder.Configuration["Configuration:AzureCosmosDb:DatabaseName"] ?? "WidgetDb";
            
            // Validate configuration
            ValidateCosmosDbConfiguration(cosmosEndpoint);
            
            // Configure Cosmos DB client options
            var cosmosClientOptions = new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                },
                ConnectionMode = ConnectionMode.Direct
            };
            
            builder.Services.AddSingleton(serviceProvider => 
            {
                var credential = new DefaultAzureCredential();
                
                // Create Cosmos client with token credential authentication
                return new CosmosClient(cosmosEndpoint, credential, cosmosClientOptions);
            });

            // Initialize Cosmos DB if needed
            builder.Services.AddHostedService<TypeSpec.Helpers.CosmosDbInitializer>();
            
            // Register WidgetsCosmos implementation of IWidgets
            builder.Services.AddScoped<IWidgets, WidgetsCosmos>();
        }

        /// <summary>
        /// Validates the Cosmos DB configuration settings
        /// </summary>
        /// <param name="cosmosEndpoint">The Cosmos DB endpoint</param>
        /// <exception cref="ArgumentException">Thrown when configuration is invalid</exception>
        private static void ValidateCosmosDbConfiguration(string cosmosEndpoint)
        {
            if (string.IsNullOrEmpty(cosmosEndpoint))
            {
                throw new ArgumentException("Cosmos DB Endpoint must be specified in configuration");
            }
        }
    }
}