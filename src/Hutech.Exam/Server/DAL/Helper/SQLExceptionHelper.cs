using System.Data.SqlClient;
using Hutech.Exam.Shared.DTO.API.Response;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.DAL.Helper
{
    public class SQLExceptionHelper<TData>
    {
        private const string DuplicateKeyMessage = "Dữ liệu bị trùng khóa hoặc vi phạm trường unique";
        private const string ForeignKeyViolationMessage = "Vi phạm khóa ngoại";
        private const string DataTooLongMessage = "Dữ liệu quá dài";
        private const string UnknownDatabaseErrorMessage = "Lỗi cơ sở dữ liệu không xác định";
        public static ActionResult HandleSqlException(SqlException ex)
        {
            if (ex == null)
            {
                return new ObjectResult(APIResponse<TData>.ErrorResponse("Exception is null"))
                {
                    StatusCode = 500
                };
            }

            return ex.Number switch
            {
                2601 or 2627 => new ConflictObjectResult(APIResponse<TData>.ErrorResponse(message: DuplicateKeyMessage, errorDetails: ex.Message)),
                547 => new BadRequestObjectResult(APIResponse<TData>.ErrorResponse(message: ForeignKeyViolationMessage, errorDetails: ex.Message)),
                8152 => new BadRequestObjectResult(APIResponse<TData>.ErrorResponse(message: DataTooLongMessage, errorDetails: ex.Message)),
                _ => new ObjectResult(APIResponse<TData>.ErrorResponse(message: UnknownDatabaseErrorMessage, errorDetails: ex.Message))
                {
                    StatusCode = 500
                }
            };
        }
    }
}
