using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Client.Pages.Admin
{
    // class sử dụng để lưu trong SessionStorage
    internal class StoredData
    {
        public DotThiDto? DotThi { get; set; }
        public MonHocDto? MonHoc { get; set; }
        public LopAoDto? LopAo { get; set; }
        public string? LanThi { get; set; }
    }
}
