using AspNetCoreRateLimit;
using HealthChecks.UI.Client;
using Hutech.Exam.Server.BUS.RabbitServices;
using Hutech.Exam.Server.Hubs;
using Hutech.Exam.Server.Installers;
using Hutech.Exam.Server.Middleware;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Shared.Profiles;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//Them cac dich vu service
ConfigureServices(builder.Services, builder.Configuration);


var app = builder.Build();


// Thêm các middleware trung gian xử lí các request, trước khi trả về reponse
//app.UseMiddleware<GlobalExceptionMiddleware>();
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

    // Lấy CancellationToken khi ứng dụng dừng lại
    var lifetime = app.Lifetime;
    var cancellationToken = lifetime.ApplicationStopping;

    // Tạo scope để giải quyết dịch vụ scoped
    var scope = app.Services.CreateScope();
    var consumeService = scope.ServiceProvider.GetRequiredService<AnswerQueueService>();
    var consumeService2 = scope.ServiceProvider.GetRequiredService<SubmitQueueService>();
    if (consumeService != null)
    {
        Task.Run(() => consumeService.ConsumeMessagesAsync(cancellationToken));
        Task.Run(() => consumeService2.ConsumeMessagesAsync(cancellationToken));
    }


    app.UseResponseCompression();
    app.UseHttpsRedirection();
    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseMiddleware<GlobalExceptionMiddleware>();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseClientRateLimiting();


    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.MapHub<AdminHub>("/adminhub");
    app.MapHub<SinhVienHub>("/sinhvienhub");

    app.Run();
}

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddAutoMapper(typeof(AllProfiles));
    services.InstallerServicesInAssembly(configuration);


    //services.AddDbContextPool<ApplicationDbContext>(options =>
    //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
}