﻿using AspNetCoreRateLimit;
using Hutech.Exam.Server.BUS;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Server.Installers;
using Hutech.Exam.Server.Middleware;
using Hutech.Exam.Shared.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Them cac dich vu service
ConfigureServices(builder.Services, builder.Configuration);


var app = builder.Build();

//Khởi chạy RabbitMQ
var scope = app.Services.CreateScope();
var consumeService = scope.ServiceProvider.GetRequiredService<RabbitMqCTBTService>();
//if (consumeService != null)
//{
//    Task.Run(() => consumeService.ConsumeMessages());
//}

// Thêm các middleware trung gian xử lí các request, trước khi trả về reponse
app.UseMiddleware<GlobalExceptionMiddleware>();
Configure(app);

static void Configure(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseResponseCompression();
    app.UseHttpsRedirection();
    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseClientRateLimiting();


    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");
    app.MapHub<ChiTietCaThiHub>("/ChiTietCaThiHub");
    app.MapHub<MainHub>("/MainHub");

    app.Run();
}

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.InstallerServicesInAssembly(configuration);


    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
}