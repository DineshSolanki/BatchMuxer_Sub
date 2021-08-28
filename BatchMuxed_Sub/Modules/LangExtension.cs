using BatchMuxer_Sub.Properties.Langs;

namespace BatchMuxer_Sub.Modules
{
    class LangExtension : HandyControl.Tools.Extension.LangExtension
    {
        public LangExtension()
        {
            Source = LangProvider.Instance;
        }
    }
}
