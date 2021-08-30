using BatchMuxer_Sub.Modules;
using HandyControl.Tools;
using Jot;

namespace BatchMuxer_Sub
{
    public static class Services
    {
        public static readonly Tracker Tracker = new();
        public static AppConfig Settings = GlobalDataHelper.Load<AppConfig>();
    }
}
