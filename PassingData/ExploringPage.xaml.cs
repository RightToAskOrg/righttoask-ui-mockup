using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PassingData
{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExploringPage : ContentPage
	{
		// private ObservableCollection<Tag> authorities = new ObservableCollection<Tag>();

		// public ObservableCollection<Tag> Authorities
		//{
		//	get { return authorities; }
		//}

		public ExploringPage(ObservableCollection<Tag> selectableTags, string message)
		{
			InitializeComponent();

			AuthorityListView.ItemsSource = selectableTags;
			introText.Text = message;
		} 

		private async void Authority_Selected(object sender, ItemTappedEventArgs e)
		{
			((Tag) e.Item).Selected = !((Tag) e.Item).Selected;
		}

		// Note: At the moment, this simply pops the page, i.e. the same
		// as Back.
		// Consider whether the semantics of 'back' should be different from
		// 'done', i.e. whether 'back' should undo.
		async void DoneButton_OnClicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}