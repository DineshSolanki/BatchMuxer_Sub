using BatchMuxer_Sub.Views;
using Prism.Ioc;

namespace BatchMuxer_Sub
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterDialog<DialogControl, DialogControlViewModel>("MessageBox");
        }

        protected override System.Windows.Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
