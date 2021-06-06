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
		private ObservableCollection<Tag> authorities = new ObservableCollection<Tag>();

		public ObservableCollection<Tag> Authorities
		{
			get { return authorities; }
		}

		public ExploringPage()
        {
            InitializeComponent();

			AuthorityListView.ItemsSource = authorities;
			
			authorities.Add(new Tag{TagLabel = "This is a test authority", Selected = true});
			authorities.Add(new Tag{TagLabel = "This is a different test authority", Selected =false});
		}
    }
}