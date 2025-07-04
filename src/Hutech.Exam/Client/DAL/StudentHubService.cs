using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;
using System.Text;
using AutoMapper;
using Hutech.Exam.Shared.DTO.Request.Custom;
using Blazored.SessionStorage;
using Hutech.Exam.Shared.DTO.Request.ChiTietBaiThi;

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
                    .AddMessagePackProtocol() // Sử dụng MessagePack để truyền tải dữ liệu
                    .WithAutomaticReconnect() // Tự động kết nối lại nếu mất mạng
                    .Build();

                await hubConnection.StartAsync();

                // set ConnectionId vào Redis của server
                await SetConnectionIdAsynv(ma_sinh_vien);
            }

            return hubConnection;
        }
        private async Task SetConnectionIdAsynv(long ma_sinh_vien)
        {
            if (hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.SendAsync("SetConnectionId", ma_sinh_vien);
            }
        }
        public async Task SendMessageChiTietBaiThi(ChiTietBaiThiRequest chiTietBaiThi)
        {
            if (hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.SendAsync("SelectDapAn", chiTietBaiThi);
            }
        }
        public async Task<Dictionary<int, ChiTietBaiThiRequest>> RequestTiepTucThi(int ma_chi_tiet_ca_thi)
        {
            if (hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                return await hubConnection.InvokeAsync<Dictionary<int, ChiTietBaiThiRequest>>("RequestTiepTucThi", ma_chi_tiet_ca_thi);
            }
            return [];
        }
        public async Task RequestSubmit(SubmitRequest request)
        {
            if (hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.SendAsync("RequestSubmit", request);
            }
        }

        public async Task RecoverySubmit(SubmitRequest request)
        {
            if(hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.SendAsync("RecoverySubmit", request);
            }
        }

        // hàm được thực hiện khi phía server không thể lấy đủ số bài của thí sinh sau 3 lần thất bại
        public async Task SendMessageListChiTietBaiThi(Dictionary<int, ChiTietBaiThiRequest> listBaiThis)
        {
            if (hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.SendAsync("SendListBaiThi", listBaiThis);
            }
        }

        public async Task LeaveGroup(int ma_ca_thi)
        {
            if (hubConnection != null && hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.SendAsync("LeaveGroupMaCaThi", ma_ca_thi);
            }
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
