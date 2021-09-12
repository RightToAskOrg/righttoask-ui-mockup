using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PassingData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExploringPageWithSearchAndPreSelections : ExploringPageWithSearch 
    {
        public ExploringPageWithSearchAndPreSelections(ObservableCollection<Tag> selectableTags, string message) :  base(selectableTags, message)
        {
            Label testInsert = new Label() 
                { 
                    Text = "This is a test.",
                };
                // authoritySearch.TextChanged += OnKeywordChanged;
                
            MainLayout.Children.Insert(1, testInsert);    
        }
    }
}