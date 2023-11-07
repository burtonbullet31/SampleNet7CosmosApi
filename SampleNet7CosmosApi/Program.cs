using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleNet7CosmosApi.Interfaces;
using SampleNet7CosmosApi.Repositories;
using SampleNet7CosmosApi.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureHostConfiguration(c =>
    {
        c.AddJsonFile("local.settings.json", false);
        c.AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        var client = new CosmosClient(hostContext.Configuration
                .GetSection("Values")["CozzyEndpoint"],
            hostContext.Configuration
                .GetSection("Values")["CozzyKey"]);
        services.AddSingleton<IDbRepository>(
            new CosmosDbRepository(
                client.GetContainer(hostContext.Configuration
                        .GetSection("Values")["CozzyDbName"],
                    hostContext.Configuration
                        .GetSection("Values")["SedanTableName"]),
                client.GetContainer(hostContext.Configuration
                        .GetSection("Values")["CozzyDbName"],
                    hostContext.Configuration
                        .GetSection("Values")["PickupTableName"]),
                client.GetContainer(hostContext.Configuration
                        .GetSection("Values")["CozzyDbName"],
                    hostContext.Configuration
                        .GetSection("Values")["SuvTableName"])));
        services.AddSingleton<IService, VehicleService>();
    })
    .Build();

host.Run();