
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
                    Console.WriteLine("Kết nối SignalR đã đóng. Chờ reconnect..." + error);
                };

                // Khi kết nối lại thành công, gọi lại JoinGroup
                hubConnection.Reconnected += async (connectionId) =>
                {
                    Console.WriteLine($"Kết nối lại SignalR thành công: {connectionId}");
                    await hubConnection.InvokeAsync("JoinGroupAdminMonitor");
                };

                await hubConnection.StartAsync();

                await hubConnection.InvokeAsync("JoinGroupAdminMonitor");
            }

            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await ReconnectionAsync();
            }

            return hubConnection;
        }

        public async Task DisconnectionAsync()
        {
            if (hubConnection != null && hubConnection.State != HubConnectionState.Disconnected)
            {
                await hubConnection.StopAsync();
            }
        }

        public async Task ReconnectionAsync()
        {
            if (hubConnection != null)
            {
                await hubConnection.StartAsync();

                await hubConnection.InvokeAsync("JoinGroupAdminMonitor");
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (hubConnection != null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}
