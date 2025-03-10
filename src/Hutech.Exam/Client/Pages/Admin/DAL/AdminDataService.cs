using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.Models;

namespace Hutech.Exam.Client.Pages.Admin.DAL
{
    public class AdminDataService
    {
        public CaThiDto CaThi { get; set; } = new();
        public UserDto User { get; set; } = new();
    }
}
