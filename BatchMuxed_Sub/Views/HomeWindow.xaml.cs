using System.IO;
using System.Windows.Controls;
using HandyControl.Data;

namespace BatchMuxer_Sub.Views
{
    /// <summary>
    /// Interaction logic for HomeWindow
    /// </summary>
    public partial class HomeWindow : UserControl
    {
        public HomeWindow()
        {
            InitializeComponent();
            txtMediaPath.VerifyFunc = VerifyFunc;
            txtMediaPath.VerifyData();
        }
        private static OperationResult<bool> VerifyFunc(string text)
        {
            return Directory.Exists(text) 
                ? OperationResult.Success()
                : OperationResult.Failed("Directory does not exist.");
        }
    }
}
