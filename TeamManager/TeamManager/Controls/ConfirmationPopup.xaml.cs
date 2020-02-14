using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using System.Linq;
using TeamManager.ViewModels;
using TeamManager.Models;

namespace TeamManager.Controls
{
    public partial class ConfirmationPopup : PopupPage
    {
        public ConfirmationPopupViewModel Context
        {
            get
            {
                return this.BindingContext as ConfirmationPopupViewModel;
            }
        }
        public ConfirmationPopup(string ConfirmationText)
        {
            InitializeComponent();

            ViewModelCollection.ConfirmationPopup = null;
            this.SetBindingContext();
            this.Context.Text = ConfirmationText;
        }

        #region Methods
        
        public void SetBindingContext()
        {
            this.BindingContext = ViewModelCollection.ConfirmationPopup;
        }
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            FrameContainer.HeightRequest = -1;

            if (!IsAnimationEnabled)
            {
                return;
            }
        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            if (!IsAnimationEnabled)
                return;
        }

        protected override async Task OnDisappearingAnimationBeginAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;

            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(300);
                taskSource.TrySetResult(true);
            });

            await taskSource.Task;
        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        public async Task<string> GetResult()
        {
            var result = await ViewModelCollection.ConfirmationPopup.GetValue();
            await PopupNavigation.Instance.PopAllAsync();
            return result;
        }
        #endregion
    }
}