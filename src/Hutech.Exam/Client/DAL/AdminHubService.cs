
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

namespace Hutech.Exam.Client.DAL
{
    public class AdminHubService : IAsyncDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private HubConnection? hubConnection;

        public AdminHubService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<HubConnection> GetConnectionAsync()
        {
            var nav = _serviceProvider.GetRequiredService<NavigationManager>();
            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(nav.ToAbsoluteUri("/adminhub"), options =>
                    {
                        options.Transports = HttpTransportType.WebSockets; // Ưu tiên WebSockets nếu có thể
                    })
                    .WithAutomaticReconnect() // Tự động kết nối lại nếu mất mạng
                    .Build();

                hubConnection.Closed += async (error) =>
                {
                    await Task.Delay(5000); // Chờ 5s trước khi thử kết nối lại
                    await GetConnectionAsync(); // Thử kết nối lại
                };

                await hubConnection.StartAsync();

                await hubConnection.InvokeAsync("JoinGroupAdmin");
            }

            return hubConnection;
        }
        public async ValueTask DisposeAsync()
        {
            if (hubConnection != null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}
