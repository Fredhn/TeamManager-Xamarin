using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace TeamManager.ViewModels
{
    public class ConfirmationPopupViewModel : BaseViewModel
    {
        private TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
        public Command OKCommand { get; set; }
        public Command CancelCommand { get; set; }
        private string _Text;
        public string Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this._Text = value;
                OnPropertyChanged("Text");
            }
        }
        public ConfirmationPopupViewModel()
        {
            OKCommand = new Command(() => tcs.SetResult("true"));
            CancelCommand = new Command(() => tcs.SetResult("false"));
        }
        public Task<string> GetValue()
        {
            return tcs.Task;
        }
    }
}