using Expert_OCR_Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Expert_OCR_Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}