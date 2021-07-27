using System.ComponentModel;
using Xamarin.Forms;

namespace PassingData.Controls
{
    public class AdvancedSearchControl : TableView
    {
        // private TableView Content;
        public AdvancedSearchControl(string Keyword)
        {
                Root = new TableRoot
                {
                    /*
                    new TableSection("Ring")
                    {
                        // TableSection constructor takes title as an optional parameter
                        new SwitchCell { Text = "New Voice Mail" },
                        new SwitchCell { Text = "New Mail", On = true }
                    }, */
                    new TableSection("Filters")
                    {
                        new EntryCell()
                        {
                            Label = "To be answered by",
                            Text    = Keyword 
                        },
                        new EntryCell()
                        {
                            Label = "To be raised in Parliament by",
                            Text    = Keyword 
                        },
                        new EntryCell()
                        {
                            Label = "Keyword",
                            Text    = Keyword 
                        }
                    }
                };
                Intent = TableIntent.Form;
        }

        /*
        public static readonly BindableProperty KeywordProperty =
         
            BindableProperty.Create(
                nameof(Keyword),
                typeof(string),
                typeof(AdvancedSearchControl),
                null,
                BindingMode.TwoWay);
                propertyChanged: OnKeywordPropertyChanged);
        
        // TODO: implement 
        private static void OnKeywordPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
             
        }

        public string Keyword 
        {
           get => (string) GetValue(KeywordProperty);
           set => SetValue(KeywordProperty, value);
        }
        */
    }
}