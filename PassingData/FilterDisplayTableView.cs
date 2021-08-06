using System;

using Xamarin.Forms;

namespace PassingData
{
    public class FilterDisplayTableView : TableView
    {
        // TODO: This should probably be done with bindings rather than
        // by passing the filters into the constructor.
        public FilterDisplayTableView (FilterContext filters)
        {
            Intent = TableIntent.Menu;
            var root = new TableRoot ();
            var section1 = new TableSection () {Title = "Filters"};

            // var text = new TextCell { Text = "TextCell", Detail = "TextCell Detail" };
            var entry = new EntryCell { Label = "Who should answer the question?", Placeholder = "?" };
            var entry2 = new EntryCell { Label = "Who should raise the question in Parliament?", Placeholder = "?" };
            var keywordentry = new EntryCell { Label = "Keyword", Placeholder = "?", Text = filters.FilterKeyword ?? null};
            // var switchc = new SwitchCell { Text = "SwitchCell Text" };
            // var image = new ImageCell { Text = "ImageCell Text", Detail = "ImageCell Detail", ImageSource = "XamarinLogo.png" };

            section1.Add (entry);
            section1.Add (entry2);
            section1.Add (keywordentry);
            Root = root;
            root.Add (section1);
        }
    }
}