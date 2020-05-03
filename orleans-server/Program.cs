﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using orleans_grains;

namespace orleans_server
{
    class Program
    {
        static Task Main(string[] args)
        {
            
            Console.Title = typeof(Program).Namespace;
            return Host.CreateDefaultBuilder()
                .UseOrleans((builder) =>
                    {
                        builder.UseLocalhostClustering()
                            .AddMemoryGrainStorageAsDefault()
                            .Configure<ClusterOptions>(options =>
                            {
                                options.ClusterId = "orleans-cluster-id";
                                options.ServiceId = "orleans-service-id";
                            })
                            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                            .ConfigureApplicationParts(parts =>
                                parts.AddApplicationPart(typeof(ISessionControlGrain).Assembly).WithReferences());
                    }
                )
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