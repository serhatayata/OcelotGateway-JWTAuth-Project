namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LogDetailWithException : LogDetail
    {
        public string ExceptionMessage { get; set; }
        //public string Date => DateTime.Now.ToString("yyyyMMddHHmmssfff");
    }
}
