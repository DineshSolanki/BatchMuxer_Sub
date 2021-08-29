using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using BatchMuxer_Sub.Modules;
using HandyControl.Controls;
using HandyControl.Tools;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BatchMuxer_Sub.ViewModels
{
    public class ConfigViewModel : BindableBase
    {
        public ConfigViewModel(IRegionManager manager)
        {
            BrowseForMkvMerge = new DelegateCommand(BrowseMkvMergePath);
            SaveCommand = new DelegateCommand(Save);
            _regionManager = manager;
            MkvMergePath = _settings.MkvMergePath;
            _languages=Util.CreateLanguageDt();
            _languageCode = _settings.SubtitleCode;
        }
        private readonly IRegionManager _regionManager;
        private readonly AppConfig _settings = GlobalDataHelper.Load<AppConfig>();
        private DataTable _languages;

        public DataTable Languages { get => _languages; set => SetProperty(ref _languages, value); }

        private string _languageCode;

        public string LanguageCode { get => _languageCode; set => SetProperty(ref _languageCode, value); }

        private string _mkvMergePath="";

        public string MkvMergePath { get => _mkvMergePath; set => SetProperty(ref _mkvMergePath, value); }

        public DelegateCommand BrowseForMkvMerge { get; }
        public DelegateCommand SaveCommand { get; }
        private void BrowseMkvMergePath()
        {
            var folderBrowserDialog = Util.NewFileBrowserDialog("Select Path of mkvmerge.exe","mkvmerge.exe");
            var dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult != null && (bool)!dialogResult) return;
            MkvMergePath = folderBrowserDialog.FileName;
        }

        private void Save()
        {
            if (File.Exists(MkvMergePath))
            {
                _settings.MkvMergePath = MkvMergePath;
                _settings.SubtitleCode = LanguageCode;
                _settings.Save();
                MessageBox.Show(CustomMessageBox.Info("Configurations saved!", "Successful"));
            }
            else
            {
                MessageBox.Show(CustomMessageBox.Error("MkvMerge path is incorrect", "Error"));
            }
        }
    }
}
