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
    public partial class DateRangePopup : PopupPage
    {
        public SpentVacations Vacation { get; set; }
        public DateRangePopup(SpentVacations Vacation)
        {
            InitializeComponent();

            this.Vacation = Vacation;
        }

        #region Methods

        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            FrameContainer.HeightRequest = -1;

            if (!IsAnimationEnabled)
            {
                //CloseImage.Rotation = 0;
                //CloseImage.Scale = 1;
                //CloseImage.Opacity = 1;

                ////imageKeypad.Scale = 1;
                ////imageKeypad.Opacity = 1;

                //imageMiniPlayMain.Scale = 1;
                //imageMiniPlayMain.Opacity = 1;

                //UsernameEntry.TranslationX = PasswordEntry.TranslationX = 0;
                //UsernameEntry.Opacity = PasswordEntry.Opacity = 1;

                return;
            }

            //CloseImage.Rotation = 30;
            //CloseImage.Scale = 0.3;
            //CloseImage.Opacity = 0;

            ////imageKeypad.Scale = 0.3;
            ////imageKeypad.Opacity = 0;

            //imageMiniPlayMain.Scale = 0.3;
            //imageMiniPlayMain.Opacity = 0;

            //UsernameEntry.TranslationX = PasswordEntry.TranslationX = -10;
            //UsernameEntry.Opacity = PasswordEntry.Opacity = 0;
        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var translateLength = 400u;

            //await Task.WhenAll(
            //	UsernameEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
            //	UsernameEntry.FadeTo(1),
            //	(new Func<Task>(async () =>
            //	{
            //		await Task.Delay(200);
            //		await Task.WhenAll(
            //			PasswordEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
            //			PasswordEntry.FadeTo(1));

            //	}))());

            //await Task.WhenAll(
            //    CloseImage.FadeTo(1),
            //    CloseImage.ScaleTo(1, easing: Easing.SpringOut),
            //    CloseImage.RotateTo(0),
            //    //imageKeypad.ScaleTo(1),
            //    //imageKeypad.FadeTo(1),
            //    imageMiniPlayMain.ScaleTo(1),
            //    imageMiniPlayMain.FadeTo(1));
        }

        protected override async Task OnDisappearingAnimationBeginAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;

            //await Task.WhenAll(
            //	UsernameEntry.FadeTo(0),
            //	PasswordEntry.FadeTo(0),
            //	imageKeypad.FadeTo(0));

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

        private async void TapGestureRecognizer_BtnSaveDates(object sender, EventArgs e)
        {
            if(this.PeriodValidationLabel.IsVisible)
            {
                this.PeriodValidationLabel.IsVisible = false;
            }

            var totalDays = this.EndDate.Date - this.StartDate.Date;

            if(totalDays.Days.Equals(10) || totalDays.Days.Equals(20) || totalDays.Days.Equals(30))
            {
                var vacationPeriod = new SpentVacations() { IDEmployee = this.Vacation.IDEmployee, RefYear = this.Vacation.RefYear, DateStart = this.StartDate.Date, DateEnd = this.EndDate.Date, TotalDays = totalDays.Days };
                SaveVacationPeriod(vacationPeriod);
                ViewModelCollection.VacationDetails.Vacations.Add(vacationPeriod);
                await ViewModelCollection.VacationDetails.ExecuteLoadItemsCommand();
                this.CloseAllPopup();
            }
            else
            {
                this.PeriodValidationLabel.IsVisible = true;
            }
        }

        async void SaveVacationPeriod(SpentVacations vac)
        {
            await App.Database._spentVacations.SaveSpentVacationsAsync(vac);
        }
        #endregion
    }
}