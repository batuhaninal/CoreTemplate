﻿namespace Application.Abstractions.Commons.Results
{
    public interface IBaseResult
    {
        int StatusCode { get; }
        bool Success { get; }
        string? Message { get; }
    }
}
