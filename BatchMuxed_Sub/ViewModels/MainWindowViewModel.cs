using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;
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
        }
        private void Navigate(string navigatePath)
        {
            _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }

        private bool _isAutoClean;

        public bool IsAutoClean
        {
            get => _isAutoClean;
            set
            {
                SetProperty(ref _isAutoClean, value);
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
