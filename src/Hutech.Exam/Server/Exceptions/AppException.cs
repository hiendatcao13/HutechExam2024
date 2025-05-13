using Hutech.Exam.Shared.API;

namespace Hutech.Exam.Server.Exceptions
{
    public class AppException : Exception
    {
        public ErrorCode ErrorCode { get;}


        public AppException(ErrorCode errorCode) : base(errorCode.Message)
        {
            ErrorCode = errorCode;
        }
    }
}
