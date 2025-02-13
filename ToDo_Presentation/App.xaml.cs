using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Repositories;
using Business.Services;
using Business.Interfaces;
using DataAccess.Context;
using ToDo_Presentation.Views;


namespace ToDo_Presentation;


public partial class App : Application
{
    public static ServiceProvider ServiceProvider;

    public App()
    {
        ServiceCollection services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
        var scope = ServiceProvider.CreateScope();
        scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreated();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        // Register Database Context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source = ToDoApp.db"));

        // Register Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IListRepository, ListRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();

        // Register Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IListService, ListService>();
        services.AddScoped<ITaskService, TaskService>();

        // Register Main Window
        services.AddSingleton<LoginView>();
        services.AddSingleton<TaskBar>();

    }
}

