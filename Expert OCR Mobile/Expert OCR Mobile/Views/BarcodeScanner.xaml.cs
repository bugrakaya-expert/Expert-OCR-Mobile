using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing.Mobile;
using ZXing.Net.Mobile;
using ZXing.Net;

namespace Expert_OCR_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodeScanner : ContentPage
    {
        public BarcodeScanner()
        {
            InitializeComponent();
        }
       /* private async void ScannerAsync()
        {
            var _snac = ZXingScannerPage();

        }*/
    }
}