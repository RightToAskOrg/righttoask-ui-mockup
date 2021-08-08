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
        public QuestionAskerPage(ReadingContext context)
        {
            // TODO: Construct this properly.
            FilterContext filters = new FilterContext {FilterKeyword = context.SearchKeyword};
            BindingContext = context;
            // currentContext.FilterKeyword = context.SearchKeyword;

            // AdvancedSearchControl firstSearch = new AdvancedSearchControl();
            // firstSearch.AKeyword = context.SearchKeyword;

            FilterDisplayTableView ttestableView = new FilterDisplayTableView(context);
            
            InitializeComponent();

            // firstSearch.Keyword = "Testing csharp entry.";
            
            /*
            TableView newTable = new TableView
            {
                Root = new TableRoot
                {
                    new TableSection("CppTest: " + firstSearch.AKeyword)
                    {
                        // TableSection constructor takes title as an optional parameter
                        new SwitchCell { Text = "New Mail", On = true }
                    }
                },
                Intent = TableIntent.Settings
            };
 
            WholePage.Children.Insert(0,newTable);
            WholePage.Children.Insert(0,firstSearch);
            */
            WholePage.Children.Insert(0,ttestableView);
        }

        async void OnNavigateForwardButtonClicked(object sender, EventArgs e)
        {
			var readingPage = new ReadingPage(false, ((ReadingContext) BindingContext).OtherAuthorities);
			readingPage.BindingContext = BindingContext;
			await Navigation.PushAsync (readingPage);
        }
    }
}