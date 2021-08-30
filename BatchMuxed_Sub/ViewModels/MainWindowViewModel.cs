using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using BatchMuxer_Sub.Modules;
using HandyControl.Controls;
using HandyControl.Data;

namespace BatchMuxer_Sub.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "BatchMuxer_Subtitle";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public DelegateCommand<string> NavigateCommand { get; }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            SwitchItemCommand = new DelegateCommand<FunctionEventArgs<object>>(SwitchItem);
            IsAutoClean = Services.Settings.IsAutoClean;
            IsIntegratedInExplorer = Services.Settings.IsIntegratedInExplorer;
        }

        private void Navigate(string navigatePath)
        {
            _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }

        private bool _isIntegratedInExplorer;

        public bool IsIntegratedInExplorer
        {
            get => _isIntegratedInExplorer;
            set
            {
                SetProperty(ref _isIntegratedInExplorer, value);
                if(value) Util.AddToContextMenu(); else Util.RemoveFromContextMenu();
                if (Services.Settings.IsIntegratedInExplorer == value) return;
                Services.Settings.IsIntegratedInExplorer = value;
                Services.Settings.SaveAsync();
            }
        }
        private bool _isAutoClean;

        public bool IsAutoClean
        {
            get => _isAutoClean;
            set
            {
                SetProperty(ref _isAutoClean, value);
                if (Services.Settings.IsAutoClean == value) return;
                Services.Settings.IsAutoClean = value;
                Services.Settings.SaveAsync();
            }
        }

        public DelegateCommand<FunctionEventArgs<object>> SwitchItemCommand { get; }

        private void SwitchItem(FunctionEventArgs<object> args)
        {
            Navigate((args.Info as SideMenuItem)?.Tag.ToString());
        }
    }
}
