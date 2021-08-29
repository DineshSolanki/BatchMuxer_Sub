using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Prism.Regions;
using BatchMuxer_Sub.Modules;
using BatchMuxer_Sub.ProcessUtil;
using HandyControl.Tools;
using HandyControl.Tools.Extension;

namespace BatchMuxer_Sub.ViewModels
{
    public class HomeWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private static readonly string[] Extensions = { ".mkv", ".webm", ".mp4" };
        private string _mkvMergePath = "";
        public string MkvMergePath { get => _mkvMergePath; set => SetProperty(ref _mkvMergePath, value); }

        private string _languageCode;

        private int _totalTasks;
        public int TotalTasks
        {
            get => _totalTasks;
            set => SetProperty(ref _totalTasks, value);
        }

        private int _completedTasks;
        public int CompletedTasks
        {
            get => _completedTasks;
            set => SetProperty(ref _completedTasks, value);
        }

        private string _cmdOutput;
        public string CmdOutput
        {
            get => _cmdOutput;
            set => SetProperty(ref _cmdOutput, value);
        }

        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }

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

        private async void StartMuxing()
        {
            CompletedTasks = 0;
            if (Path.GetFileName(MkvMergePath).ToLower() != "mkvmerge.exe" || !File.Exists(MkvMergePath)) return;
            IsBusy = true;
            var directoryInfo = MediaPath.ToDirectoryInfo();
            var files = directoryInfo.EnumerateFiles()
                .Where(f => Extensions.Contains(f.Extension.ToLower()))
                .ToArray();
            Util.RenameFiles(files);
            var tasks = files.Select(fi => Util.ProcessFileAsync(fi, MediaPath)).ToList();
            TotalTasks = tasks.Count;
            tasks.ForEach(tsk => tsk.ContinueWith(taskinfo =>
            {
                if (taskinfo.Result?.AdditionalInfo is not null)
                {
                    CmdOutput = taskinfo.Result.AdditionalInfo;
                }
                
                CompletedTasks++;
            }));
            var __ = await Task.WhenAll(tasks); 
            IsBusy = false;

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
