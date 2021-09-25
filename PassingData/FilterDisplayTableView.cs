using System;
using System.Linq;
using Xamarin.Forms;

namespace PassingData
{
    public class FilterDisplayTableView : TableView
    {
        private ReadingContext _context;
        public FilterDisplayTableView(ReadingContext readingContext)
        {
            BindingContext = readingContext;
            _context = readingContext;
            // Intent = TableIntent.Menu;
            Intent = TableIntent.Settings;
            // HasUnevenRows = true;
            var root = new TableRoot();
            var section1 = new TableSection() { Title = "Filters"};
            var section2 = new TableSection() { };

            // TODO: Export this as its own View; use it in ExploringPageWithSearchAndPreSelections.
            // Also possibly use it in base ExploringPage.
            var authorityDataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                var selectedToggle = new Switch();

                nameLabel.SetBinding(Label.TextProperty, "TagEntity.NickName");
                selectedToggle.SetBinding(Switch.IsToggledProperty, "Selected");

                grid.Children.Add(nameLabel);
                grid.Children.Add(selectedToggle, 1, 0);
                
                return new ViewCell { View = grid };
            });

            var authorityList = new Label()
            {
                Text = String.Join(", ",readingContext.SelectableAuthorities.Where(w => w.Selected).Select(t => t.TagEntity.ShortestName))
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
                        new Label { Text = "Edit" },
                    }
                }
            };
            whoShouldAnswerItView.Tapped += OnMoreButtonClicked;
            
            var entry3 = new EntryCell { Label = "Who should raise it in Parliament?", Placeholder = "Not sure" };
            var keywordentry = new EntryCell
            {
                Label = "Keyword", 
                Placeholder = "?", 
                Text = ((ReadingContext) BindingContext).SearchKeyword ?? null
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
			
           	var departmentExploringPage = new ExploringPageWithSearchAndPreSelections(_context.SelectableAuthorities, message);
           	await Navigation.PushAsync (departmentExploringPage);
        }

        private void OnKewordEntryCompleted(object sender, EventArgs e)
        {
            ((ReadingContext)BindingContext).SearchKeyword = ((EntryCell)sender).Text;
        }

        private void OnWhoShouldAnswerCompleted(object sender, EventArgs e)
        {
            ((EntryCell)sender).Text = "You entered " + ((EntryCell)sender).Text;
        }
        
        // TODO: atm this is copied from ExploringPage.xaml.cs.  Consider refactoring
        // with some utils.
		private async void Authority_Selected(object sender, ItemTappedEventArgs e)
		{
			((Tag) e.Item).Selected = !((Tag) e.Item).Selected;
		}
    }
}