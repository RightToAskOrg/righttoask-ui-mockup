using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PassingData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionAskerPage : ContentPage
    {
        public QuestionAskerPage(ReadingContext context)
        {
            BindingContext = context;
            InitializeComponent();
        }

        async void OnNavigateForwardButtonClicked(object sender, EventArgs e)
        {
			var readingPage = new ReadingPage(false, ((ReadingContext) BindingContext).OtherAuthorities);
			readingPage.BindingContext = BindingContext;
			await Navigation.PushAsync (readingPage);
        }
    }
}