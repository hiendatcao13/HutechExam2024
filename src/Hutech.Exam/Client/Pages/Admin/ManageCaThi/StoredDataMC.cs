using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Client.Pages.Admin.ManageCaThi
{
    // class sử dụng để lưu trong SessionStorage
    internal class StoredDataMC
    {
        public DotThiDto? DotThi { get; set; }
        public MonHocDto? MonHoc { get; set; }
        public LopAoDto? LopAo { get; set; }
        public string? LanThi { get; set; }
    }
}
