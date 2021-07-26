using Xamarin.Forms;

namespace PassingData.Controls
{
    public class AdvancedSearchControl : TableView
    {
        // private TableView Content;
        public AdvancedSearchControl() 
        {
                Root = new TableRoot
                {
                    new TableSection("Ring")
                    {
                        // TableSection constructor takes title as an optional parameter
                        new SwitchCell { Text = "New Voice Mail" },
                        new SwitchCell { Text = "New Mail", On = true }
                    },
                    new TableSection("Keyword")
                    {
                        new EntryCell()
                        {
                            Text    = Keyword 
                        }
                    }
                };
                Intent = TableIntent.Data;
        }
        
        public static readonly BindableProperty KeywordProperty =
            BindableProperty.Create(
                nameof(Keyword),
                typeof(string),
                typeof(AdvancedSearchControl),
                null,
                BindingMode.TwoWay);

        public string Keyword
        {
            get => (string) GetValue(KeywordProperty);
            set => SetValue(KeywordProperty, value);
        }
    }
}