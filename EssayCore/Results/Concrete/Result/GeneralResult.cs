namespace Essay.Core
{
    public class GeneralResult : IResult
    {
        public bool Success { get;}
        public string Message { get;}

        public GeneralResult(string Message,bool Success) : this(Success)
        {
            this.Message = Message;
        }
        public GeneralResult(bool Success)
        {
            this.Success = Success;
        }
    }
}
