using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Prism.Regions;

namespace BatchMuxer_Sub.ViewModels
{
    public class HomeWindowViewModel : BindableBase
    {
        private string mkvMergePath = "";
        public HomeWindowViewModel(IRegionManager regionManager)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send,
                new Action(() =>
                {
                    regionManager.Regions["ContentRegion"].PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == "MkvMergePath")
                        {
                            mkvMergePath = (string)regionManager.Regions["ContentRegion"].Context;
                        }
                    };
                }));
        }
    }
}
