﻿using Application.Abstractions.Commons.Results;

namespace Application.Models.DTOs.Commons.Results
{
    public class ResultDto : IBaseResult
    {
        public ResultDto(int statusCode, bool success, string message) : this(statusCode, success)
        {
            Message = message;
        }

        public ResultDto(int statusCode, bool success)
        {
            Success = success;
            StatusCode = statusCode;
        }

        public int StatusCode { get; }

        public bool Success { get; }

        public string? Message { get; }
    }
}
