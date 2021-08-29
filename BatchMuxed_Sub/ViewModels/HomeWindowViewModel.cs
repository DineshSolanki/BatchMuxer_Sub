using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Prism.Regions;
using BatchMuxer_Sub.Modules;
using HandyControl.Tools;
using HandyControl.Tools.Extension;

namespace BatchMuxer_Sub.ViewModels
{
    public class HomeWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private static readonly string[] Extensions = { ".mkv", ".webm", ".mp4" };
        private string _mkvMergePath="";
        public string MkvMergePath { get => _mkvMergePath; set => SetProperty(ref _mkvMergePath, value); }
        
        private string _languageCode;

        public string CmdOutput;
        private string _mediaPath;
        public string MediaPath { get => _mediaPath; set => SetProperty(ref _mediaPath, value); }
        
        public DelegateCommand BrowseForMediaPath { get; }
        public DelegateCommand StartMuxCommand { get; }
        private readonly AppConfig _settings = GlobalDataHelper.Load<AppConfig>();
        public HomeWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            MkvMergePath = _settings.MkvMergePath;
            _languageCode = _settings.SubtitleCode;
            BrowseForMediaPath = new DelegateCommand(BrowseForMedia);
            StartMuxCommand = new DelegateCommand(StartMuxing);
        }

        private void StartMuxing()
        {
            if (Path.GetFileName(MkvMergePath).ToLower() == "mkvmerge.exe" && File.Exists(MkvMergePath))
            {
                var directoryInfo = MediaPath.ToDirectoryInfo();
                var files = directoryInfo.EnumerateFiles()
                    .Where(f => Extensions.Contains(f.Extension.ToLower()))
                    .ToArray();
                Util.RenameFiles(files);
                files.ForEach(fi =>{
                    Util.ProcessFile(fi,MediaPath, ref CmdOutput);
                });
                
            }
            ;

        }

        private void BrowseForMedia()
        {
            var folderBrowserDialog = Util.NewFolderBrowserDialog("Browse");
            var dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult != null && (bool)!dialogResult) return;
            MediaPath = folderBrowserDialog.SelectedPath;
        }
    }
}
