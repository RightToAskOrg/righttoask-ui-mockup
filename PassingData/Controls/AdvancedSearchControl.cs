using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace PassingData.Controls
{
    public class AdvancedSearchControl : TableView
    {
        private String aKeyword;
        // private TableView Content;
        // public AdvancedSearchControl(string Keyword)
        public AdvancedSearchControl()
        {

        
            Root = new TableRoot
                {
                    new TableSection("Filters")
                    {
                        new EntryCell()
                        {
                            Label = "To be answered by"+aKeyword,
                            //Text  = Keyword ?? "" 
                            // Text  = AKeyword
                            Text = aKeyword
                        },
                        new EntryCell()
                        {
                            Label = "To be raised in Parliament by",
                            Text    = "Does this work?" 
                        },
                        new EntryCell()
                        {
                            Label = "Keyword",
                            Text    = AKeyword ?? "" 
                        }
                    }
                };
                Intent = TableIntent.Settings;
        }

        public static readonly BindableProperty KeywordProperty =

            BindableProperty.Create(
                nameof(AKeyword),
                typeof(string),
                typeof(AdvancedSearchControl),
                null,
                BindingMode.TwoWay);
                // null,
                // OnKeywordPropertyChanged);
        
                /*
        private static void OnKeywordPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (AdvancedSearchControl) bindable;
            control.Keyword = newvalue?.ToString();
        }
        */

        // TODO: Doesn't need to implement OnPropertyChanged because it's part of the UI anyway? Or does it?
        // TODO: Does SetValue call OnPropertyChanged?
        public string AKeyword 
        {
           get => (string) GetValue(KeywordProperty);
           // set => SetValue(KeywordProperty, value);
           set
           {
              aKeyword = value;
              OnPropertyChanged("Keyword");
           }
           // set => SetValue(KeywordProperty, value);
        }  
    }
}