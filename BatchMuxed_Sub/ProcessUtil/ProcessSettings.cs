using System.Threading;

namespace BatchMuxer_Sub.ProcessUtil
{
    public class ProcessSettings
    {
        public string FileName { get; set; }
        public string AdditionalInfo { get; set; }
        public string Arguments { get; set; } = "";
        public string WorkingDirectory { get; set; } = "";
        public string InputText { get; set; } = null;
        public int Timeout_milliseconds { get; set; } = -1;
        public bool ReadOutput { get; set; }
        public bool ShowWindow { get; set; }
        public bool KeepWindowOpen { get; set; }
        public bool StartAsAdministrator { get; set; }
        public string StartAsUsername { get; set; }
        public string StartAsUsername_Password { get; set; }
        public string StartAsUsername_Domain { get; set; }
        public bool DontReadExitCode { get; set; }
        public bool ThrowExceptions { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
