using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.AdoNet.SqlServer.Persistence;
using Orleans.Configuration;
using Orleans.Hosting;
using orleans_grains;

namespace orleans_server
{
    class Program
    {
        static Task Main(string[] args)
        {
            // 1. 无状态的orleans server   
            // Console.Title = typeof(Program).Namespace;
            // return Host.CreateDefaultBuilder()
            //     .UseOrleans((builder) =>
            //         {
            //             builder.UseLocalhostClustering()
            //                 .AddMemoryGrainStorageAsDefault()
            //                 .Configure<ClusterOptions>(options =>
            //                 {
            //                     options.ClusterId = "orleans-cluster-id";
            //                     options.ServiceId = "orleans-service-id";
            //                 })
            //                 .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
            //                 .ConfigureApplicationParts(parts =>
            //                     parts.AddApplicationPart(typeof(ISessionControlGrain).Assembly).WithReferences());
            //         }
            //     )
            //     .ConfigureServices(services =>
            //     {
            //         services.Configure<ConsoleLifetimeOptions>(options =>
            //         {
            //             options.SuppressStatusMessages = true;
            //         });
            //     })
            //     .ConfigureLogging(builder => { builder.AddConsole(); })
            //     .RunConsoleAsync();
            
            
            // 2. 有状态的orleans server
            return Host.CreateDefaultBuilder()
                .UseOrleans((builder) =>
                {
                    var connectionString =
                        @"Data Source=WIN-BQDI5Q717BG;Initial Catalog=db_orleans;User ID=sa;Password=123456;Integrated Security=True;Pooling=False;Max Pool Size=200;MultipleActiveResultSets=True";

                    //use AdoNet for Persistence
                    builder.AddSqlServerGrainStorageAsDefault(options =>
                    {
                        options.ConnectionString = connectionString;
                        options.UseJsonFormat = true;
                    })
                        .UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "orleans-cluster-id";
                        options.ServiceId = "orleans-service-id";
                    })
                    .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                    .ConfigureApplicationParts(parts =>
                        parts.AddApplicationPart(typeof(ISessionControlGrain).Assembly).WithReferences());
                })
                .ConfigureServices(services =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder => { builder.AddConsole(); })
                .RunConsoleAsync();
        }
    }
}