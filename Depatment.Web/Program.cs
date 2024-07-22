using DepartmentModule.UseCase.AutoMapper;
using DepartmentModule.UseCase.Departments;
using DepartmentModule.UseCase.Departments.Interfaces;
using DepartmentModule.UseCase.PluginInterface;
using DepartmentModule.UseCase.PluginInterface.Services;
using DepartmentModule.UseCase.Reminders;
using HostedService;
using Microsoft.EntityFrameworkCore;
using Plugin.EFCore;
using ServicesPlugin;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDepartmentRepository, Departmentrepository>();
builder.Services.AddScoped<IGetListParentDepartmentsUseCase,GetListParentDepartmentsUseCase>();
builder.Services.AddScoped<IGetListSubDepartmentsUseCase, GetListSubDepartmentsUseCase>();
builder.Services.AddScoped<ICreateDepartmentUseCase, CreateDepartmentUseCase>();
builder.Services.AddScoped<IGetDepartmentDetailUseCase, GetDepartmentDetailUseCase>();
builder.Services.AddScoped<IGetAllDepartmentsUseCase, GetAllDepartmentsUseCase>();
builder.Services.AddScoped<IGetSubDepartmentsUseCase, GetSubDepartmentsUseCase>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
//builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IEmailService, MockEmailService>();

// Register repositories and use cases
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
builder.Services.AddScoped<CreateReminderUseCase>();
builder.Services.AddScoped<GetRemindersUseCase>();
builder.Services.AddScoped<SendRemindersUseCase>();

// Register hosted service
builder.Services.AddHostedService<ReminderHostedService>();
builder.Host.UseSerilog((context, configuration) => {
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/Log.txt", rollingInterval: RollingInterval.Day);
});


var app = builder.Build(); if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Department}/{action=Index}/{id?}");


app.Run();
