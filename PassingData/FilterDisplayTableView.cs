using System;

using Xamarin.Forms;

namespace PassingData
{
    public class FilterDisplayTableView : TableView
    {
        // TODO: This should probably be done with bindings rather than
        // by passing the filters into the constructor.
        public FilterDisplayTableView(ReadingContext readingContext)
        {
            BindingContext = readingContext;
            Intent = TableIntent.Menu;
            var root = new TableRoot();
            var section1 = new TableSection() { Title = "Filters" };

            /*
            var entry1 = new Picker()
            {
                Title = "Level of government: ",
                ItemsSource = ,
            };
            */
            
            var entry2 = new EntryCell
            {
                Label = "Who should answer it?",
                Placeholder = "Not sure",
            };
            entry2.Completed += OnWhoShouldAnswerCompleted;

            var entry3 = new EntryCell { Label = "Who should raise it in Parliament?", Placeholder = "Not sure" };
            var keywordentry = new EntryCell
            {
                Label = "Keyword", 
                Placeholder = "?", 
                Text = ((ReadingContext) BindingContext).SearchKeyword ?? null
            };
            keywordentry.Completed += OnKewordEntryCompleted;
            
            // var switchc = new SwitchCell { Text = "SwitchCell Text" };
            // var image = new ImageCell { Text = "ImageCell Text", Detail = "ImageCell Detail", ImageSource = "XamarinLogo.png" };

            section1.Add(entry2);
            section1.Add(entry3);
            section1.Add(keywordentry);
            Root = root;
            root.Add(section1);

        }

        private void OnKewordEntryCompleted(object sender, EventArgs e)
        {
            ((ReadingContext)BindingContext).SearchKeyword = ((EntryCell)sender).Text;
        }

        private void OnWhoShouldAnswerCompleted(object sender, EventArgs e)
        {
            ((EntryCell)sender).Text = "You entered " + ((EntryCell)sender).Text;
        }
    }
}