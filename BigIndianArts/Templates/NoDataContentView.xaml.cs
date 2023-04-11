using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BigIndianArts.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoDataContentView : ContentView
    {
        public NoDataContentView()
        {
            InitializeComponent();
        }
    }
}