using BigIndianArts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportMenuPage : ContentPage
	{
		public ObservableCollection<MenuModel> Option_list { get; }
		public ReportMenuPage ()
		{
			InitializeComponent (); 
			Option_list = new ObservableCollection<MenuModel>()
			{
				new MenuModel(){m_id="1",icon="easel.png",name="Daily"},
				new MenuModel(){m_id="2",icon="man.png",name="Monthly"},
				//new MenuModel(){m_id="3",icon="handshake.png",name="Settlements"},


			};
			_collection.ItemsSource = Option_list;
        }
        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        async private void ServiceListItem_Tapped(object sender, EventArgs e)
        {
            TappedEventArgs _tapped = e as TappedEventArgs;
            var _selectedBtn = _tapped.Parameter as MenuModel;
            if (_selectedBtn.m_id == "1")
            {
                await Navigation.PushAsync(new ExpenceReportPage());
            }
            else if (_selectedBtn.m_id == "2")
            {
                await Navigation.PushAsync(new MonthlyReport());
            }
            //else if (_selectedBtn.m_id == "3")
            //{
            //    await Navigation.PushAsync(new SettlementPage(_artistData));
            //}

        }
    }
}