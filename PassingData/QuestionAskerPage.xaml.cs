using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PassingData.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PassingData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionAskerPage : ContentPage
    {
        private ReadingContext _context;
        public QuestionAskerPage(ReadingContext context)
        {
            // TODO: Construct this properly.
            FilterContext filters = new FilterContext {FilterKeyword = context.SearchKeyword};
            BindingContext = context;
            _context = context;
            
            FilterDisplayTableView ttestableView = new FilterDisplayTableView(context);
            
            InitializeComponent();

            WholePage.Children.Insert(0,ttestableView);
        }

        async void OnNavigateForwardButtonClicked(object sender, EventArgs e)
        {
			var readingPage = new ReadingPage(false, _context.SelectableAuthorities, _context);
			await Navigation.PushAsync (readingPage);
        }
    }
}