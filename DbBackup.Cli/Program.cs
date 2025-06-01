using System.CommandLine;
using System.CommandLine.Hosting; // Make sure this is present
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var root = new RootCommand("Universal DB backup utility");

var backup = new Command("backup", "Create a backup");
var restore = new Command("restore", "Restore a backup");

root.Add(backup);
root.Add(restore);

await Host.CreateDefaultBuilder(args)
    .UseSerilog((ctx, cfg) => cfg
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.json", rollingInterval: RollingInterval.Day))
    .ConfigureServices(svc =>
    {
        // place-holders – we’ll implement these extension methods in Sprint 1
        // svc.AddCore();
        // svc.AddAdapters();
        // svc.AddStorage();
    })
    .Build() // <--- ADD THIS LINE
    .RunAsync(); // <--- CHANGE THIS LINE
    await root.InvokeAsync(args);