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
            return Directory.Exists(text) && File.Exists(Path.Combine(text, "mkvmerge.exe"))
                ? OperationResult.Success()
                : OperationResult.Failed("mkvmerge.exe not found in this directory");
        }
    }
}
