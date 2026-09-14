using System;

namespace LibraryService.Models
{
    public class ResponseModel(int status, string message, object? data = null)
    {
        public int Status { get; set; } = status;
        public string Message { get; set; } = message;
        public object? Data { get; set; } = data;
    }
}
