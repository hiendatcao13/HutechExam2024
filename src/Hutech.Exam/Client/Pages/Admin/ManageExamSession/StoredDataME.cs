using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Client.Pages.Admin.ManageExamSession
{
    // class sử dụng để lưu trong SessionStorage
    internal class StoredDataME
    {
        public DotThiDto? DotThi { get; set; }
        public MonHocDto? MonHoc { get; set; }
        public LopAoDto? LopAo { get; set; }
        public int LanThi { get; set; }
    }
}
