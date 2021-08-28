using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BatchMuxer_Sub.Properties;
using HandyControl.Tools;

namespace BatchMuxer_Sub.Properties.Langs
{
    class LangProvider : INotifyPropertyChanged

    {
        private static string? _cultureInfoStr;
        internal static LangProvider Instance => ResourceHelper.GetResource<LangProvider>("BatchMuxLangs");
        public static string GetLang(string key) => Lang.ResourceManager.GetString(key, Culture);

        public static void SetLang(DependencyObject dependencyObject, DependencyProperty dependencyProperty, string key)
        {
            BindingOperations.SetBinding(dependencyObject, dependencyProperty, new Binding(key)
            {
                Source = Instance,
                Mode = BindingMode.OneWay
            });
        }

        public static CultureInfo Culture
        {
            get => Lang.Culture;
            set
            {
                if (Equals(_cultureInfoStr, value.EnglishName)) return;
                Lang.Culture = value;
                _cultureInfoStr = value.EnglishName;

                Instance.UpdateLangs();
            }
        }

        private void UpdateLangs()
        {
            OnPropertyChanged(nameof(About));
            OnPropertyChanged(nameof(MkvMergePath));
            OnPropertyChanged(nameof(EnterMkvMergePath));
        }
        public string About => Lang.About;
        public string MkvMergePath => Lang.MkvMergePath;
        public string EnterMkvMergePath => Lang.EnterMkvMergePath;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class LangKeys
    {
        public static string About = nameof(About);
        public static string MkvMergePath = nameof(MkvMergePath);
        public static string EnterMkvMergePath = nameof(EnterMkvMergePath);
    }
}
