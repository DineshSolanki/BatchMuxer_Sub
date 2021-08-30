

using System.Text.Json;
using HandyControl.Tools;

namespace BatchMuxer_Sub.Modules
{
    public class AppConfig : GlobalDataHelper
    {
        public string MkvMergePath { get; set; } = "";
        public bool IsAutoClean { get; set; } = false;
        public string SubtitleCode = "en";
        public override string FileName { get; set; }
        public override JsonSerializerOptions JsonSerializerOptions { get; set; }
        public override int FileVersion { get; set; }
    }
}
