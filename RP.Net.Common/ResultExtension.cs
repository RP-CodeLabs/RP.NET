using System;
using System.Collections.Generic;

namespace RP.Net.Common
{
    public static class ResultExtension
    {
        public static Result<TModel> ToResult<TModel>(this Maybe<TModel> @this) where TModel : class
            => @this.HasValue ? Result.Ok(@this.Value) : Result.Fail(@this.Value, new Dictionary<string, object>());

        public static Result Ensure(this Result result, Func<bool> predicate)
            => result.IsFailure ? result 
                : predicate() ? result : Result.Fail(result.Error ?? new Dictionary<string, object>());

        public static Result<TSource> Ensure<TSource>(this Result<TSource> result, Func<bool> predicate)
            => result.IsFailure ? result
                : predicate() ? result : Result.Fail(result.Value, result.Error ?? new Dictionary<string, object>());

        public static Result OnBoth(this Result result, Func<Result, Result> func)
            => result.IsFailure ? result : func(result);

        public static T OnBoth<T>(this Result<T> result, Func<Result, T> func) => func(result);

        public static Result<TOutput> Map<TSource, TOutput>(this Result<TSource> result, Func<TSource, TOutput> func)
            => result.IsFailure ? Result.Fail(func(result.Value), result.Error) : Result.Ok(func(result.Value));

        public static Result<TOutput> OnSuccess<TSource, TOutput>(this Result<TSource> result, Func<TSource, TOutput> func)
            => result.IsFailure ? Result.Fail(func(result.Value), result.Error) : Result.Ok(func(result.Value));

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action(); 
            }
            return result;
        }

        public static Result OnFailure(this Result result, Action<Result> action)
        {
            if (result.IsFailure)
            {
                action(result);
            }
            return result;
        }

        public static Result<TSource> OnFailure<TSource>(this Result<TSource> result, Action<Result> action)
        {
            if (result.IsFailure)
            {
                action(result);
            }
            return result;
        }

        public static Result AddErrorParameters(this Result result, string key, object value)
        {
            if (result.IsFailure)
            {
                result.Error.Add(key, value);
            }
            return result;
        }

        public static Result<TSource> AddErrorParameters<TSource>(this Result<TSource> result, string key, object value) 
        {
            if (result.IsFailure)
            {
                result.Error.Add(key, value);
            }
            return result;
        }
    }
}