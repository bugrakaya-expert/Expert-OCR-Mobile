using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;

namespace Expert_OCR_Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private int frameCounter = 0;
        bool PermissionsGranted;
        public AboutViewModel()
        {
            Title = "About";
            /*
            OpenWebCommand = new Command(
                async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart")
                );
            */

            /*
            OpenWebCommand = new Command(
                async () => OpenCam()
                );*/
        }

        private async Task<bool> VerifyPermissions()
        {
            try
            {
                PermissionStatus status = PermissionStatus.Unknown;

                // Camera permission - NOTE: Requires adding Permission to the Android Manifest and iOS pList 
                status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                   //DisplayAlert("Camera Permission Required", "This app will proccess frames taken live from the device's camera", "OK");
                    status = await Permissions.RequestAsync<Permissions.Camera>();

                    if (status != PermissionStatus.Granted)
                        return false;
                }

                // All needed permissions granted 
                return true;
            }
            catch (Exception ex)
            {
                //DisplayAlert("Error", ex.ToString(), "OK");
                return false;
            }
        }

        public ICommand OpenWebCommand { get; }
    }
}