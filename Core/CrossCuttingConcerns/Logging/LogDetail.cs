using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public byte Risk { get; set; }
        public List<LogParameter> LogParameters { get; set; }
    }
}
