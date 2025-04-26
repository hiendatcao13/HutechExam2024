using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Hutech.Exam.Client.DAL
{
    public class StudentHubService : IAsyncDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private HubConnection? hubConnection;

        public StudentHubService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<HubConnection> GetConnectionAsync(long ma_sinh_vien)
        {
            var nav = _serviceProvider.GetRequiredService<NavigationManager>();
            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(nav.ToAbsoluteUri("/MainHub"), options =>
                    {
                        options.Transports = HttpTransportType.WebSockets; // Ưu tiên WebSockets nếu có thể
                    })
                    .WithAutomaticReconnect() // Tự động kết nối lại nếu mất mạng
                    .Build();

                hubConnection.Closed += async (error) =>
                {
                    await Task.Delay(5000); // Chờ 5s trước khi thử kết nối lại
                    await GetConnectionAsync(ma_sinh_vien); // Thử kết nối lại
                };

                await hubConnection.StartAsync();

                // set ConnectionId vào Redis
                var connectionId = await GetConnectionId();
                if (connectionId != null)
                    await SetConnectionIdAPI(connectionId, ma_sinh_vien);
            }

            return hubConnection;
        }
        private async Task<string?> GetConnectionId()
        {
            if(hubConnection != null)
                return await hubConnection.InvokeAsync<string>("GetConnectionId");
            return null;
        }

        private async Task SetConnectionIdAPI(string ConnectionId, long ma_sinh_vien)
        {
            using var scope = _serviceProvider.CreateScope();
            var http = scope.ServiceProvider.GetRequiredService<HttpClient>();

            var data = new
            {
                ConnectionId,
                MaSinhVien = ma_sinh_vien
            };
            var jsonString = JsonSerializer.Serialize(data);
            await http.PutAsync("api/ConnectionId/SetConnectionId", new StringContent(jsonString, Encoding.UTF8, "application/json"));
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
