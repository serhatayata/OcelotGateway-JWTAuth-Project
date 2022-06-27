using Newtonsoft.Json;

namespace Core.Extensions
{
    public class ErrorDetails
    {
        public bool Success { get; set; } = false;
        public object Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
