using Hutech.Exam.Shared.API;

namespace Hutech.Exam.Server.Exceptions
{
    public class AppException : Exception
    {
        public ErrorCode errCode { get; set; }

        public AppException(ErrorCode errCode) : base(errCode.message)
        {
            this.errCode = errCode;
        }
    }
}
