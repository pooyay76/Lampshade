namespace Framework.Application
{
    public class OperationResult
    {
        public string Message { get; set; }
        public bool IsSucceeded { get; set; }

        public OperationResult()
        {
            IsSucceeded = false;
        }

        public OperationResult Failed(string msg="Operation Failed")
        {
            IsSucceeded = false;
            Message = msg;
            return this;
        }
        public OperationResult Succeeded(string msg = "Operation Succeeded")
        {
            IsSucceeded = true;
            Message = msg;
            return this;
        }
    }
}
