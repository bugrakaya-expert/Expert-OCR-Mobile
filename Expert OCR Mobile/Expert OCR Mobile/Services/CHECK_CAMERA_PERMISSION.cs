using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Expert_OCR_Mobile.Services
{
    public class CHECK_CAMERA_PERMISSION
    {
        public static async Task<bool> VerifyPermissions()
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
    }
}
