namespace AlpaStock.Core.DTOs
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }
        public string DisplayMessage { get; set; }
        public T Result { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
