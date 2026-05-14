using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Common
{
    public record Result<T>
    {
        public bool IsSuccess { get; init; }
        public T? Data { get; init; }
        public string? Error { get; init; }

        private Result(bool isSuccess, T? data, string? error)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        public static Result<T> Ok(T data) => new Result<T>(true, data, null);
        public static Result<T> Fail(string error) => new Result<T>(false, default, error);
    }
}
