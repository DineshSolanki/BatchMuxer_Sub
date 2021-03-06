using Prism.Commands;
using Prism.Mvvm;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Prism.Regions;
using BatchMuxer_Sub.Modules;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;

namespace BatchMuxer_Sub.ViewModels
{
    public class HomeWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private static readonly string[] Extensions = { ".mkv", ".webm", ".mp4" };
        private string _mkvMergePath = "";
        public string MkvMergePath { get => _mkvMergePath; set => SetProperty(ref _mkvMergePath, value); }

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

        private string _cmdOutput = "";
        public string CmdOutput
        {
            get => _cmdOutput;
            set => SetProperty(ref _cmdOutput, value);
        }

        private string _lastTask = "Processing...";
        public string LastTask
        {
            get => _lastTask;
            set => SetProperty(ref _lastTask, value);
        }

        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }

        private string _mediaPath;
        public string MediaPath { get => _mediaPath; set => SetProperty(ref _mediaPath, value); }

        public DelegateCommand BrowseForMediaPath { get; }
        public DelegateCommand StartMuxCommand { get; }
        public DelegateCommand CleanDirectoryCommand { get; }
        private FileInfo[] _files;
        public HomeWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            MkvMergePath = Services.Settings.MkvMergePath;
            BrowseForMediaPath = new DelegateCommand(BrowseForMedia);
            StartMuxCommand = new DelegateCommand(StartMuxing);
            CleanDirectoryCommand = new DelegateCommand(CleanDirectory);
            var cmdArgs = Util.GetCmdArgs();
            if (!cmdArgs.ContainsKey("path")) return;
            MediaPath = Util.GetCmdArgs()["path"];
            StartMuxing();
        }

        private void CleanDirectory()
        {

            if (!Directory.Exists(MediaPath)) return;
            var files = MediaPath.ToDirectoryInfo().EnumerateFiles()
                .Where(f => Extensions.Contains(f.Extension.ToLower()))
                .ToArray();
            Util.DeleteAndMove(MediaPath, files);
            Growl.SuccessGlobal("Directory Cleaned up!");
        }

        private async void StartMuxing()
        {
            CompletedTasks = 0;
            if (Path.GetFileName(MkvMergePath).ToLower() != "mkvmerge.exe" || !File.Exists(MkvMergePath))
            {
                MessageBox.Show(CustomMessageBox.Error("MkvMerge path is incorrect or not set!","Error"));
                return;
            }
            IsBusy = true;
            var directoryInfo = MediaPath.ToDirectoryInfo();
            _files = directoryInfo.EnumerateFiles()
                .Where(f => Extensions.Contains(f.Extension.ToLower()))
                .ToArray();
            Util.RenameFiles(_files);
            var tasks = _files.Select(fi => Util.ProcessFileAsync(fi, MediaPath, (_, args) => CmdOutput=args.Text)).ToList();
            TotalTasks = tasks.Count;
            tasks.ForEach(tsk => tsk.ContinueWith(taskinfo =>
            {
                var fileName = "muxedFile.log";
                if (taskinfo.Result is null) return;
                CompletedTasks++;
                if (taskinfo.Result.AdditionalInfo is not null)
                {
                    fileName = LastTask = taskinfo.Result.AdditionalInfo;
                }

                if (string.IsNullOrEmpty(taskinfo.Result.Output)) return;
                fileName = $"{fileName.Truncate(10,"")}.log";
                Util.WriteToLogsFolder(fileName,taskinfo.Result.Output);
            }));
            var __ = await Task.WhenAll(tasks);
            Growl.AskGlobal(
                new GrowlInfo
                {
                    Message = "All media processed \n Would you like to open Logs folder?",
                    ShowDateTime = false,
                    StaysOpen = false,
                    ActionBeforeClose = isConfirmed =>
                    {
                        if(isConfirmed) 
                            Util.StartAnyProcess(Util.GetLogsFolder());
                        return true;
                    }
                });
            IsBusy = false;
            if (Services.Settings.IsAutoClean)
            {
                CleanDirectory();
            }
            LastTask = "Processing...";
            CmdOutput = "";
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
