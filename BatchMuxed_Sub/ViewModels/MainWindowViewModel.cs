using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

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
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }
        private void Navigate(string navigatePath)
        {
            _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }

        private bool _isAutoClean;

        public bool IsAutoClean { get => _isAutoClean; set => SetProperty(ref _isAutoClean, value); }
    }
}
