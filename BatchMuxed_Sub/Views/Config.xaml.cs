using System.IO;
using System.Windows.Controls;
using HandyControl.Data;

namespace BatchMuxer_Sub.Views
{
    /// <summary>
    /// Interaction logic for Config
    /// </summary>
    public partial class Config : UserControl
    {
        public Config()
        {
            InitializeComponent();
            txtMkvMergePath.VerifyFunc = VerifyFunc;
        }

        private static OperationResult<bool> VerifyFunc(string text)
        {
            return Path.GetFileName(text).ToLower() == "mkvmerge.exe" && File.Exists(text) 
                ? OperationResult.Success()
                : OperationResult.Failed("please input full path including mkvmerge.exe");
        }
    }
}
