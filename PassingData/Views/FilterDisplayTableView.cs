using System;
using System.Linq;
using PassingData.Models;
using Xamarin.Forms;

namespace PassingData.Views
{
    public class FilterDisplayTableView : TableView
    {
        private FilterChoices filterContext;
        public FilterDisplayTableView(FilterChoices filterContext)
        {
            BindingContext = filterContext;
            this.filterContext = filterContext;
            BackgroundColor = Color.NavajoWhite;
            Intent = TableIntent.Settings;
            VerticalOptions = LayoutOptions.Start;
            HeightRequest = Height; 
            var root = new TableRoot();
            var section1 = new TableSection() { Title = "Filters - click to edit"};
            var section2 = new TableSection() { };

            var authorityList = new Label()
            {
                Text = String.Join(",", filterContext.SelectedAuthorities.Select((a => a.ShortestName)))
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
                        }
                    }
                }
            };
            whoShouldAnswerItView.Tapped += OnMoreButtonClicked;
            
            var entry3 = new EntryCell { Label = "Who should raise it in Parliament?", Placeholder = "Not sure" };
            var keywordentry = new EntryCell
            {
                Label = "Keyword", 
                Placeholder = "?", 
                Text = filterContext.SearchKeyword ?? null,
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
			
           	var departmentExploringPage = new ExploringPageWithSearchAndPreSelections(BackgroundElectorateAndMPData.AllAuthorities, 
                filterContext.SelectedAuthorities, message);
           	await Navigation.PushAsync (departmentExploringPage);
        }

        private void OnKewordEntryCompleted(object sender, EventArgs e)
        {
            filterContext.SearchKeyword = ((EntryCell)sender).Text;
        }
    }
}