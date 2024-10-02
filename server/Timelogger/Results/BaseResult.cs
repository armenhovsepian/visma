using System.Collections.Generic;
using System.Linq;

namespace Timelogger.Results
{
    public class Result
    {
        private Result(bool isSuccess, IEnumerable<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IEnumerable<string> Errors { get; }

        public static Result Success() => new Result(true, Enumerable.Empty<string>());

        public static Result Failure(IEnumerable<string> errors) => new Result(false, errors);
    }
}
