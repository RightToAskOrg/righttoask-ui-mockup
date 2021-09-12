using System;

using Xamarin.Forms;

namespace PassingData
{
    public class FilterDisplayTableView : TableView
    {
        public FilterDisplayTableView(ReadingContext readingContext)
        {
            BindingContext = readingContext;
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

            /*
            var entry2 = new EntryCell
            {
                Label = "Who should answer it?",
                Placeholder = "Not sure",
            };
            entry2.Completed += OnWhoShouldAnswerCompleted;
            */

            var authorityList = new ListView
            {
                VerticalOptions = LayoutOptions.Start, 
                SelectionMode = ListViewSelectionMode.None, 
                ItemsSource = readingContext.OtherAuthorities,
                HasUnevenRows = true,
                ItemTemplate = authorityDataTemplate
            };
            // TODO Figure out how to pick up clicks.
            authorityList.ItemTapped += Authority_Selected;
            // BindableLayout.SetItemsSource(authorityList, readingContext.OtherAuthorities);
            // BindableLayout.SetItemTemplate(authorityList, authorityDataTemplate);
                

                // var authorityListView = new ViewCell { View = authorityList };
                
            var moreButton = new ViewCell
            {
                View = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Children = {  new Label { Text = "More..." } }
                }
            };
            moreButton.Tapped += OnMoreButtonClicked;

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
                        // authorityList.ParentView,
                        // new View{ authorityList},
                        new StackLayout
                        {
                            // Orientation = StackOrientation.Vertical,
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Start,
                            Children =
                            {
                                authorityList,
                                moreButton.View
                            }
                        }
                    }
                }
            };
            
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

            // section1.Add(entry2);
            section1.Add(whoShouldAnswerItView);
            section1.Add(moreButton);
            section2.Add(entry3);
            section2.Add(keywordentry);
            Root = root;
            root.Add(section1);
            root.Add(section2);

        }

        async void OnMoreButtonClicked(object sender, EventArgs e)
        {
			string message = "Choose the authorities that should answer your question";
			
           	var departmentExploringPage = new ExploringPageWithSearch(((ReadingContext) BindingContext).OtherAuthorities, message);
            departmentExploringPage.BindingContext = BindingContext;
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