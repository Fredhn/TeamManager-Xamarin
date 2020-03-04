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
    public partial class InformationPopup : PopupPage
    {
        public InformationPopup(string Title, string Subtitle, string Content)
        {
            InitializeComponent();

            this.Title.Text = Title;
            this.Subtitle.Text = Subtitle;
            this.Content.Text = Content;
        }

        #region Methods
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
        #endregion
    }
}