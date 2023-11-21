using Microsoft.Extensions.DependencyInjection;
using NPocoTestDrive.ConsoleApp.Views;
using NPocoTestDrive.ConsoleApp.Views.Interfaces;
using NPocoTestDrive.Data.Contexts;
using NPocoTestDrive.Data.Repositories;
using NPocoTestDrive.Data.Repositories.Interfaces;

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);
ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
var executorService = serviceProvider.GetService<IMenuCon>();
await executorService!.ShowMenu();

static void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<NPocoContext, NPocoContext>();
    services.AddScoped<IMenuCon, MenuCon>();
    services.AddScoped<IEmployeeCon, EmployeeCon>();
    services.AddScoped<IEmployeeRepository, EmployeeRepository>();
}
