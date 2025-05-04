namespace Hutech.Exam.Client.Pages.Admin.OrganizeExam.Dialog
{
    public class ErrorMessage
    {
        public List<int> ErrorNulls { get; set; }
        public List<int> ErrorMSSVs { get; set; }
        public List<int> ErrorNames { get; set; }
        public List<int> ErrorSexs { get; set; }
        public List<int> ErrorDoubles { get; set; } // khi mã số sinh viên bị trùng lập
        public List<int> ErrorExists { get; set; } // khi mã số sinh viên đã tồn tại trong ca thi
        public ErrorMessage()
        {
            ErrorNulls = [];
            ErrorMSSVs = [];
            ErrorNames = [];
            ErrorSexs = [];
            ErrorDoubles = [];
            ErrorExists = [];
        }
        public string PrintErrorNull()
        {
            if (ErrorNulls.Count > 0)
            {
                string message = "Kiểm tra phần tử Null dòng: ";
                for(int i = 0; i < ErrorNulls.Count - 1; i ++)
                {
                    message += $"[{ErrorNulls[i]}], ";
                }
                return message;
            }
            return "";
        }
        public string PrintErrorMSSV()
        {
            if (ErrorMSSVs.Count > 0)
            {
                string message = "Kiểm tra phần tử MSSV dòng: ";
                for (int i = 0; i < ErrorMSSVs.Count - 1; i ++)
                {
                    message += $"[{ErrorMSSVs[i]}], ";
                }
                return message;
            }
            return "";
        }
        public string PrintErrorName()
        {
            if (ErrorNames.Count > 0)
            {
                string message = "Kiểm tra phần tử Họ và Tên dòng: ";
                for (int i = 0; i < ErrorNames.Count - 1; i ++)
                {
                    message += $"[{ErrorNames[i]}], ";
                }
                return message;
            }
            return "";
        }
        public string PrintErrorSex()
        {
            if (ErrorSexs.Count > 0)
            {
                string message = "Kiểm tra phần tử Giới tính dòng: ";
                for (int i = 0; i < ErrorSexs.Count - 1; i ++)
                {
                    message += $"[{ErrorSexs[i]}], ";
                }
                return message;
            }
            return "";
        }
        public string PrintErrorDouble()
        {
            if (ErrorDoubles.Count > 0)
            {
                string message = "Mã sinh viên bị trùng lặp dòng: ";
                for (int i = 0; i < ErrorDoubles.Count - 1; i += 2)
                {
                    message += $"[{ErrorDoubles[i]}]-[{ErrorDoubles[i + 1]}], ";
                }
                return message + "   ";
            }
            return "";
        }
        public string PrintErrorExist()
        {
            if (ErrorExists.Count > 0)
            {
                string message = "Mã sinh viên đã tồn tại trong ca thi dòng: ";
                for (int i = 0; i < ErrorExists.Count; i ++)
                {
                    message += $"[{ErrorExists[i]}], ";
                }
                return message;
            }
            return "";
        }
    }
}
