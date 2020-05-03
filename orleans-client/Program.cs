using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;

namespace orleans_client
{
    class Program
    {
        // private const int InitializeAttemptsBeforeFailing = 5;
        // private static int attempt = 0;
        //
        // static void Main(string[] args)
        // {
        //     try
        //     {
        //         Console.WriteLine($"开始执行....");
        //        
        //         Console.WriteLine($"执行结束");
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex);
        //     }
        // }
        //
        // private static async Task<IClusterClient> InitialiseClient()
        // {
        //     var client = new ClientBuilder()
        //         .UseLocalhostClustering()
        //         .Build();
        //
        //     await client.Connect(RetryFilter);
        //     return client;
        // }
        //
        // private static async Task<bool> RetryFilter(Exception exception)
        // {
        //     if (exception.GetType() != typeof(SiloUnavailableException))
        //     {
        //         Console.WriteLine($"Cluster client failed to connect to cluster with unexpected error.  Exception: {exception}");
        //         return false;
        //     }
        //     attempt++;
        //     Console.WriteLine($"Cluster client attempt {attempt} of {InitializeAttemptsBeforeFailing} failed to connect to cluster.  Exception: {exception}");
        //     if (attempt > InitializeAttemptsBeforeFailing)
        //     {
        //         return false;
        //     }
        //     await Task.Delay(TimeSpan.FromSeconds(3));
        //     return true;
        // }
        // }

        
        static Task Main(string[] args)
        {
            Console.Title = typeof(Program).Namespace;

            return Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ClusterClientHostedService>();
                    services.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>());
                    services.AddSingleton(_ => _.GetService<ClusterClientHostedService>().Client);

                    services.AddHostedService<HelloOrleansClientHostedService>();
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}