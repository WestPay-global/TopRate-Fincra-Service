namespace Fincra.Models.ThirdParty.Response
{
    public class FincraBaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}