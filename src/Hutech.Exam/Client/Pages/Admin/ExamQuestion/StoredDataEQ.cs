using Hutech.Exam.Shared.DTO;

namespace Hutech.Exam.Client.Pages.Admin.ExamQuestion
{
    public class StoredDataEQ
    {
        public MonHocDto? Subject { get; set; }

        public DeThiDto? Exam { get; set; }

        public NhomCauHoiDto? GroupQuestion { get; set; }

        public CloDto? Clo { get; set; }
    }
}
