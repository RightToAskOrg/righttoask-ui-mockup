using System;
using System.Linq;
using Xamarin.Forms;

namespace PassingData
{
    public class FilterDisplayTableView : TableView
    {
        private ReadingContext context;
        public FilterDisplayTableView(ReadingContext readingContext)
        {
            BindingContext = readingContext;
            context = readingContext;
            BackgroundColor = Color.NavajoWhite;
            Intent = TableIntent.Settings;
            var root = new TableRoot();
            var section1 = new TableSection() { Title = "Filters"};
            var section2 = new TableSection() { };

            var authorityList = new Label()
            {
                // Text = String.Join(", ",readingContext.SelectableAuthorities.Where(w => w.Selected).Select(t => t.TagEntity.ShortestName))
                Text = readingContext.Filters.SelectedAuthorities.ToString()  
            };

            var whoShouldAnswerItView = new ViewCell
            {
                View = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children =
                    {
                        new Label { Text = "Who should answer it?" },
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Start,
                            Children =
                            {
                                authorityList,
                            }
                        },
                        new Label { Text = "Change" },
                    }
                }
            };
            whoShouldAnswerItView.Tapped += OnMoreButtonClicked;
            
            var entry3 = new EntryCell { Label = "Who should raise it in Parliament?", Placeholder = "Not sure" };
            var keywordentry = new EntryCell
            {
                Label = "Keyword", 
                Placeholder = "?", 
                Text = context.Filters.SearchKeyword ?? null
            };
            keywordentry.Completed += OnKewordEntryCompleted;
            
            section1.Add(whoShouldAnswerItView);
            section2.Add(entry3);
            section2.Add(keywordentry);
            Root = root;
            root.Add(section1);
            root.Add(section2);

        }

        async void OnMoreButtonClicked(object sender, EventArgs e)
        {
			string message = "Choose others to add";
			
           	var departmentExploringPage = new ExploringPageWithSearchAndPreSelections(BackgroundElectorateAndMPData.AllAuthorities, context.Filters.SelectedAuthorities, message);
           	await Navigation.PushAsync (departmentExploringPage);
        }

        private void OnKewordEntryCompleted(object sender, EventArgs e)
        {
            context.Filters.SearchKeyword = ((EntryCell)sender).Text;
        }
    }
}