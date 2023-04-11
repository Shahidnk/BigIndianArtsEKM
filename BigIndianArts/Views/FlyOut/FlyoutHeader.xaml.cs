using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts.Views.FlyOut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutHeader : ContentView
    {
        public FlyoutHeader()
        {
            InitializeComponent();
            lbl_name.Text = App._LoggedUserName;
            lbl_email.Text = App._Role;
        //    lbl_phone.Text = App._LoggedPersonNumber;
            sfAvatar_profileImg.ImageSource = App._LoggedUserImg;
        }
    }
}