﻿using System.Net;
using System.Text.Json.Serialization;

namespace Hutech.Exam.Shared.DTO.API.Response
{
    public class ResponseAPI<TData>
    {
        [JsonPropertyName("timeStamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TData? Data { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("errorCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorCode { get; set; }

        [JsonPropertyName("errorDetails")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorDetails { get; set; }

        public ResponseAPI(bool success, string? message, TData? data, HttpStatusCode statusCode, string? errorCode = null, string? errorDetails = null)
        {
            Timestamp = DateTime.UtcNow;
            Success = success;
            Message = message;
            Data = data;
            StatusCode = (int)statusCode;
            ErrorCode = errorCode;
            ErrorDetails = errorDetails;

            if (success && ((int)statusCode < 200 || (int)statusCode >= 300))
                throw new ArgumentException("Success responses must have a status code in the 2xx range.");
            if (!success && (int)statusCode >= 200 && (int)statusCode < 300)
                throw new ArgumentException("Error responses must have a status code in the 4xx or 5xx range.");
        }

        // Factory methods to generate common responses
        public static ResponseAPI<TData> SuccessResponse(TData data, string message = "Operation successful")
        {
            return new ResponseAPI<TData>(true, message, data, HttpStatusCode.OK);
        }

        public static ResponseAPI<TData> ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string? errorCode = null, string? errorDetails = null)
        {
            return new ResponseAPI<TData>(false, message, default, statusCode, errorCode, errorDetails);
        }

        public static ResponseAPI<TData> NotFoundResponse(string message = "Resource not found", string? errorCode = null, string? errorDetails = null)
        {
            return new ResponseAPI<TData>(false, message, default, HttpStatusCode.NotFound, errorCode, errorDetails);
        }

        public static ResponseAPI<TData> UnauthorizedResponse(string message = "Unauthorized access", string? errorCode = null, string? errorDetails = null)
        {
            return new ResponseAPI<TData>(false, message, default, HttpStatusCode.Unauthorized, errorCode, errorDetails);
        }

        public static ResponseAPI<TData> InternalServerErrorResponse(string message = "Internal server error", string? errorCode = null, string? errorDetails = null)
        {
            return new ResponseAPI<TData>(false, message, default, HttpStatusCode.InternalServerError, errorCode, errorDetails);
        }
    }
}
