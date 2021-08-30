namespace BatchMuxer_Sub.ProcessUtil
{
    public class ProcessResult
    {
        public string Output { get; set; }
        public string AdditionalInfo { get; set; }
        public string OutputError { get; set; }
        public int ExitCode { get; set; }
        public bool WasCancelled { get; set; }
        public bool WasSuccessful { get; set; }
    }
}
