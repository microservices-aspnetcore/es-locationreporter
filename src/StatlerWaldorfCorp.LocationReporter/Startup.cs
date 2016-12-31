using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using System.Linq;
using StatlerWaldorfCorp.LocationReporter.Models;
using StatlerWaldorfCorp.LocationReporter.Events;
using StatlerWaldorfCorp.LocationReporter.Services;

namespace StatlerWaldorfCorp.LocationReporter
{
    public class Startup
    {
        public static string[] Args { get; set; } = new String[] {};
        
        private ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory) 
        {
            var builder = new ConfigurationBuilder()
		        .AddEnvironmentVariables()
		        .AddCommandLine(Startup.Args);

	        Configuration = builder.Build();

    	    this.loggerFactory = loggerFactory;
	        this.loggerFactory.AddConsole(LogLevel.Information);
	        this.loggerFactory.AddDebug();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddMvc();
            services.AddSingleton(typeof(IEventEmitter), new AMQPEventEmitter());
            services.AddSingleton(typeof(ICommandEventConverter), new CommandEventConverter());
            services.AddScoped(typeof(ITeamServiceClient), typeof(HttpTeamServiceClient));
        }

        public void Configure(IApplicationBuilder app) 
        {
            app.UseMvc();
        }
    }
}

