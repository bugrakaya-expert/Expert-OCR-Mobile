using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Expert_OCR_Mobile.Services;
using Xamarin.Essentials;
using System.IO;
using System.Net.Http;
using Xamarin.Forms.PlatformConfiguration;
using System.Net.Sockets;
using ZXing;

namespace Expert_OCR_Mobile.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraViewPage : ContentPage
    {
        private Image _image = new Image();
        bool _verify = false;
        public byte[]? _buffer;
        public int MIN_PORT = 1111, MAX_PORT = 65534, _PORT = 0;

        public CameraViewPage()
        {



            InitializeComponent();
            zoomLabel.Text = string.Format("Zoom: {0}", zoomSlider.Value);
            //VerifyCameraPermission();



        }
        public async void VerifyCameraPermission()
        {
            _verify = await CHECK_CAMERA_PERMISSION.VerifyPermissions();

        }
        void ZoomSlider_ValueChanged(object? sender, ValueChangedEventArgs e)
        {
            /*
			cameraView.Zoom = (float)zoomSlider.Value;
			zoomLabel.Text = string.Format("Zoom: {0}", Math.Round(zoomSlider.Value));*/

        }

        void VideoSwitch_Toggled(object? sender, ToggledEventArgs e)
        {/*
			var captureVideo = e.Value;

			if (captureVideo)
				cameraView.CaptureMode = CameraCaptureMode.Video;
			else
				cameraView.CaptureMode = CameraCaptureMode.Photo;

			previewPicture.IsVisible = !captureVideo;

			doCameraThings.Text = e.Value ? "Start Recording"
				: "Snap Picture";*/
        }

        // You can also set it to Default and External


        void FrontCameraSwitch_Toggled(object? sender, ToggledEventArgs e)
            => cameraView.CameraOptions = e.Value ? CameraOptions.Front : CameraOptions.Back;

        // You can also set it to Torch (always on) and Auto
        void FlashSwitch_Toggled(object? sender, ToggledEventArgs e)
            => cameraView.FlashMode = e.Value ? CameraFlashMode.On : CameraFlashMode.Off;

        void DoCameraThings_Clicked(object? sender, EventArgs e)
        {
            cameraView.Shutter();
            doCameraThings.Text = cameraView.CaptureMode == CameraCaptureMode.Video
                ? "Stop Recording"
                : "Snap Picture";


        }
        void SendPicture_Clicked(object? sender, EventArgs e)
        {

        }
        void RemovePicture_Clicked(object? sender, EventArgs e)
        {

            removePicture.IsVisible = false;
            sendPicture.IsVisible = false;
            previewPicture.IsVisible = false;
            cameraView.IsVisible = true;
            doCameraThings.IsVisible = true;
            previewPicture.Source = null;
        }
        void CameraView_OnAvailable(object? sender, bool e)
        {
            previewPicture.IsVisible = false;
            if (e)
            {
                zoomSlider.Value = cameraView.Zoom;
                var max = cameraView.MaxZoom;
                if (max > zoomSlider.Minimum && max > zoomSlider.Value)
                    zoomSlider.Maximum = max;
                else
                    zoomSlider.Maximum = zoomSlider.Minimum + 1; // if max == min throws exception
            }

            doCameraThings.IsEnabled = e;
            zoomSlider.IsEnabled = e;
            zoomSlider.IsVisible = false;
            zoomLabel.IsVisible = false;
        }

        void CameraView_MediaCaptured(object? sender, MediaCapturedEventArgs e)
        {
            if (e.ImageData == null)
                App.Current.MainPage.DisplayAlert("Kamera Hatası", "Resim çekimi başarasız. Lütfen tekrar deneyiniz", "cancel");
            else
            {
                switch (cameraView.CaptureMode)
                {
                    default:
                    case CameraCaptureMode.Default:
                    case CameraCaptureMode.Photo:
                        //previewVideo.IsVisible = false;
                        previewPicture.IsVisible = true;
                        previewPicture.Rotation = e.Rotation;
                        previewPicture.Source = e.Image;
                        doCameraThings.Text = "Snap Picture";
                        break;
                    case CameraCaptureMode.Video:
                        previewPicture.IsVisible = false;
                        //previewVideo.IsVisible = true;
                        //previewVideo.Source = e.Video;
                        doCameraThings.Text = "Start Recording";
                        break;
                }
                removePicture.IsVisible = true;
                sendPicture.IsVisible = true;
                previewPicture.IsVisible = true;
                cameraView.IsVisible = false;
                doCameraThings.IsVisible = false;



                //_buffer = e.ImageData;


                ScanBarcode(e.ImageData);
            }
        }
        private void ScanBarcode(byte[] buffer)
        {
            System.Drawing.Image _image;
            /*
            BarcodeLib.Barcode _barcode = new BarcodeLib.Barcode();
            BarcodeStandard.SaveData _save = new BarcodeStandard.SaveData();

            System.Drawing.Imaging.ImageFormat _frm = new System.Drawing.Imaging.ImageFormat(Guid.NewGuid());
            Stream _stream = new MemoryStream(buffer);
            _barcode.EncodedImage.Save(_stream,_frm);
            var test = _barcode;
            */

            //BarcodeReader reader = new BarcodeReader();

            using (var ms = new MemoryStream(buffer))
            {
                _image = System.Drawing.Image.FromStream(ms);
            }


            BarcodeReader _reader = new ZXing.BarcodeReader();
            _reader.Decode(buffer);

            /*
            BarcodeReaderGeneric reader = new BarcodeReaderGeneric();
            reader.Decode(_image);
            BarcodeReader*/
           //IBarcodeReader _reader = new IBarcodeReader();

        }
        private async void UploadFile_Clicked(object sender, EventArgs e)
        {/*
			var content = new MultipartFormDataContent();
			content.Add(new StreamContent(_mediaFile.GetStream()),
				"\"file\"",
				$"\"{_mediaFile.Path}\"");
			var httpClient = new HttpClient();
			var uploadServiceBaseAddress = "http://uploadtoserver.azurewebsites.net/api/Files/Upload";
			"http://localhost:12214/api/Files/Upload";
			var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);
			*/
            //RemotePathLabel.Text = await httpResponseMessage.Content.ReadAsStringAsync();
        }
        /*
		public class SaveToGallery : ISaveToGallery
		{
			public async void storePhotoToGallery(byte[] bytes, string fileName)
			{
				Context context = MainActivity.Instance;
				try
				{
					Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
					string path = System.IO.Path.Combine(storagePath.ToString(), fileName);
					System.IO.File.WriteAllBytes(path, bytes);
					var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
					mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(path)));
					context.SendBroadcast(mediaScanIntent);
				}
				catch (Exception ex)
				{
				}
			}
		}

		public interface ISaveToGallery
		{
			void storePhotoToGallery(byte[] bytes, string fileName);
		}*/
    }
}