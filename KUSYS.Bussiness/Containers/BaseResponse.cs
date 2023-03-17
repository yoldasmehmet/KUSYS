namespace KUSYS.Bussiness.Containers
{
    public class BaseResponse : MethodExecutionBase
    {
        public object? Data { get; set; }
        public string Message { get; set; } = "-";
    }
}
