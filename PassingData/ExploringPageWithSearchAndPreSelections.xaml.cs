using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PassingData.Controls;
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
                    Text = "Alread selected",
                };
                // authoritySearch.TextChanged += OnKeywordChanged;
                
            MainLayout.Children.Insert(1, testInsert);

            listPriorSelections(selectableTags);
        }

        private void listPriorSelections(ObservableCollection<Tag> currentSelectableTags)
        {
            ListView selections = new ListView()
            {
                
                ItemTemplate=(DataTemplate)Application.Current.Resources["SelectableDataTemplate"],
                ItemsSource = currentSelectableTags.Where(w => w.Selected)
                // ItemTemplate = StaticResource SelectableDataTemplate,
            };
            MainLayout.Children.Insert(2,selections);
        }
        
    }
}