using Prism.Mvvm;

namespace CustomerManager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "取引先一覧";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
