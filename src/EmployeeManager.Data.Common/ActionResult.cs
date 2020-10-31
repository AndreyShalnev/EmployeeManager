namespace EmployeeManager.Data.Common
{
    public class ActionResult<TResult> : ActionResult
        where TResult : class
    {
        public ActionResult(bool isSucces) : base(isSucces) { }
        public ActionResult(string message) : base(message) { }

        public ActionResult(TResult result) : base(true)
        {
            Result = result;
        }

        public TResult Result { get; private set; }
    }

    public class ActionResult
    {
        public ActionResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public ActionResult(string message)
        {
            IsSuccess = false;
            Message = message;
        }

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
    }
}
