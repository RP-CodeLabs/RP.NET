using System;
using System.Collections.Generic;

namespace RP.Net.Common
{
    public class Result<T> : Result
    {
        private readonly T _value;
        public Result(T value, bool isSuccess, Dictionary<string, object> error) : base(isSuccess, error)
        {
            _value = value;
        }

        public T Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException();
                }
                return _value;
            }
        }
    }

    public class Result
    {
        public Result(bool isSuccess, Dictionary<string, object> error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Dictionary<string, object> Error { get; }

        public static Result Ok() => new Result(true, new Dictionary<string, object>());

        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, new Dictionary<string, object>());

        public static Result Fail(Dictionary<string, object> error) => new Result(false, error);

        public static Result<T> Fail<T>(T value, Dictionary<string, object> error) => new Result<T>(value, false, error);
    }
}