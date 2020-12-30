using System;

namespace BusinessLogic.Services.Contracts
{
    public class OperationResult<TResult>
    {
        public static OperationResult<TResult> Ok(TResult result)
        {
            return new OperationResult<TResult>()
            {
                Success = true,
                Result = result
            };
        }

        public static OperationResult<TResult> Failed(string[] errors)
        {
            return new OperationResult<TResult>()
            {
                Errors = errors
            };
        }

        public TResult Result { get; set; }
        public bool Success { get; set; }
        public string[] Errors { get; set; }
        public string GetErrors()
        {
            return string.Join(Environment.NewLine, Errors);
        }
    }
}
