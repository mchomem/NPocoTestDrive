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
    services.AddScoped<IEmployeeService, EmployeeService>();

    services.AddAutoMapper(typeof(AutoMapperProfile));
}
