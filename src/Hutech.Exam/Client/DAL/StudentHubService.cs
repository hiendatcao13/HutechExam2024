using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using System.Text;
using Hutech.Exam.Shared.DTO.Request;
using AutoMapper;

namespace Hutech.Exam.Client.DAL
{
    public class StudentHubService(IServiceProvider serviceProvider) : IAsyncDisposable
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private HubConnection? hubConnection;

        public async Task<HubConnection> GetConnectionAsync(long ma_sinh_vien)
        {
            var nav = _serviceProvider.GetRequiredService<NavigationManager>();
            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(nav.ToAbsoluteUri("/sinhvienhub"), options =>
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

                // set ConnectionId vào Redis của server
                await hubConnection.InvokeAsync("SetConnectionId", ma_sinh_vien);
            }

            return hubConnection;
        }
        public async Task SendMessageChiTietBaiThi(ChiTietBaiThiRequest chiTietBaiThi)
        {
            if (hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                // ánh xạ cho phần update
                var messageJson = JsonSerializer.Serialize(chiTietBaiThi);
                var messageBytes = Encoding.UTF8.GetBytes(messageJson);

                await hubConnection.SendAsync("SelectDapAn", messageBytes);
            }
        }
        public async Task<Dictionary<int, int>> RequestChiTietBaiThi(int ma_chi_tiet_ca_thi)
        {
            if (hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                return await hubConnection.InvokeAsync<Dictionary<int, int>>("RequestTiepTucThi", ma_chi_tiet_ca_thi);
            }
            return [];
        }
        public async Task<List<bool>> RequestSubmit(int ma_chi_tiet_ca_thi)
        {
            if(hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                return await hubConnection.InvokeAsync<List<bool>>("RequestSubmit", ma_chi_tiet_ca_thi);
            }
            return [];
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
