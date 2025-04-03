using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.SignalR.Client;

namespace Hutech.Exam.Client.DAL
{
    public class AdminDataService
    {
        public UserDto User { get; set; } = new();
        public List<CaThiDto> CaThis { get; set; } = []; // sử dụng cho ng dùng back về trang QLCT 

    }
}
