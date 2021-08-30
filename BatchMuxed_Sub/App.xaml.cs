using BatchMuxer_Sub.Views;
using Prism.Ioc;
using Prism.Regions;

namespace BatchMuxer_Sub
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Config>();
            containerRegistry.RegisterForNavigation<HomeWindow>("Home");
            //containerRegistry.RegisterDialog<DialogControl, DialogControlViewModel>("MessageBox");
        }

        protected override System.Windows.Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Container.Resolve<IRegionManager>().RegisterViewWithRegion("ContentRegion", typeof(HomeWindow));
        }
    }
}
