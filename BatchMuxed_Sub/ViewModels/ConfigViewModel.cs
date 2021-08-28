using System;
using System.Collections.ObjectModel;
using System.Data;
using BatchMuxer_Sub.Modules;
using Prism.Commands;
using Prism.Mvvm;

namespace BatchMuxer_Sub.ViewModels
{
    public class ConfigViewModel : BindableBase
    {
        public ConfigViewModel()
        {
            BrowseForMkvMerge = new DelegateCommand(BrowseMkvMergePath);
        }

        private void BrowseMkvMergePath()
        {
            var folderBrowserDialog = Util.NewFolderBrowserDialog("Select Path of mkvmerge.exe");
            var dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult != null && (bool)!dialogResult) return;
            MkvMergePath = folderBrowserDialog.SelectedPath;
        }

        private DataTable _languages = Util.CreateLanguageDt();

        public DataTable Languages { get => _languages; set => SetProperty(ref _languages, value); }

        private string _languageCode="";

        public string LanguageCode { get => _languageCode; set => SetProperty(ref _languageCode, value); }

        private string _mkvMergePath="";

        public string MkvMergePath { get => _mkvMergePath; set => SetProperty(ref _mkvMergePath, value); }

        public DelegateCommand BrowseForMkvMerge { get; private set; }
    }
}
