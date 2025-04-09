using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductApi.Application.Responses
{
    public class BaseResponse
    {
        [JsonPropertyOrder(1)]
        public bool Success { get; set; }
        [JsonPropertyOrder(2)]
        public string? Message { get; set; }
    }

    public class SuccessResponse<T> : BaseResponse
    {
        [JsonPropertyOrder(3)]
        public T? Data { get; set; }

        public SuccessResponse(T data, string message)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public SuccessResponse(T data)
        {
            Success = true;
            Message = "Successfull";
            Data = data;
        }

        public SuccessResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class ErrorResponse : BaseResponse
    {
        [JsonPropertyOrder(3)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; }

        public ErrorResponse(List<string> errors, string message)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }

        public ErrorResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }

}
