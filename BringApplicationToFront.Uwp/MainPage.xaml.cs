using System;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BringApplicationToFront.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            UpdateTabletModeUi();
        }

        private async void OnLaunchClicked(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("notepad");
            }
        }

        private async void OnLaunchClickedCvtHmi(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("cvthmi");
            }
        }

        private async void OnLaunchClickedNotepad(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("notepad");
            }
        }

        private void OnCheckTabletMode(object sender, RoutedEventArgs e)
        {
            UpdateTabletModeUi();
        }

        private void UpdateTabletModeUi()
        {
            switch (UIViewSettings.GetForCurrentView().UserInteractionMode)
            {
                case UserInteractionMode.Touch:
                    TabletSwitch.IsOn = true;
                    break;

                case UserInteractionMode.Mouse:
                default:
                    TabletSwitch.IsOn = false;
                    break;
            }
        }
    }
}
