using System;

using Xamarin.Forms;

namespace PassingData
{
    public class FilterDisplayTableView : TableView
    {
        // TODO: This should probably be done with bindings rather than
        // by passing the filters into the constructor.
        public FilterDisplayTableView(FilterContext filters)
        {
            BindingContext = filters;
            Intent = TableIntent.Menu;
            var root = new TableRoot();
            var section1 = new TableSection() { Title = "Filters" };

            // var text = new TextCell { Text = "TextCell", Detail = "TextCell Detail" };
            var entry = new EntryCell
            {
                Label = "Who should answer the question?",
                Placeholder = "?"
                // Completed = OnWhoShouldAnswerCompleted
            };

            var entry2 = new EntryCell { Label = "Who should raise the question in Parliament?", Placeholder = "?" };
            var keywordentry = new EntryCell
                { Label = "Keyword", Placeholder = "?", Text = ((FilterContext) BindingContext).FilterKeyword ?? null };
            // var switchc = new SwitchCell { Text = "SwitchCell Text" };
            // var image = new ImageCell { Text = "ImageCell Text", Detail = "ImageCell Detail", ImageSource = "XamarinLogo.png" };

            section1.Add(entry);
            section1.Add(entry2);
            section1.Add(keywordentry);
            Root = root;
            root.Add(section1);

        }

        private void OnWhoShouldAnswerCompleted(object sender, EventArgs e)
        {
            ((EntryCell)sender).Text = "You entered" + ((EntryCell)sender).Text;
        }
    }
}