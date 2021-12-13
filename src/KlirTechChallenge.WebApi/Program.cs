global using System;
global using MediatR;
global using System.Net;
global using AutoMapper;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using System.Collections.Generic;
global using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using KlirTechChallenge.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using KlirTechChallenge.Infrastructure.Database.Context;


namespace KlirTechChallenge.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = scope.ServiceProvider.GetService<KlirTechChallengeContext>();
            DataSeeder.SeedData(context);
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();                    
            });        
}
