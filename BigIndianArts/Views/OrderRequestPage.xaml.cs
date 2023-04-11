using Acr.UserDialogs;
using BigIndianArts.Data;
using BigIndianArts.Helpers;
using BigIndianArts.Interface;
using BigIndianArts.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BigIndianArts.Data.MediaStorageClient;

namespace BigIndianArts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderRequestPage : ContentPage
    {
        APIResponse _apiResponce = new APIResponse();
        private string item;
        private int totalpr;

        public int CommiFrame { get; private set; }

        private string selectedType;
        private int delprize;
        private int sizprize;
        private int colprize;
        private int selectedCount = 1;
        private string selectedSize;
        private int assignID;
        private int assign_empid;
        private string assignName;
        private decimal commisionpercent;
        private decimal commissionAmount;
        private bool submitTap;

        public string TextFile { get; private set; }
        public string LocFile { get; private set; }
        public int GetCol { get; private set; }
        public bool IsFrame { get; private set; }
        public static FileResult MediaResults { get; internal set; }

        public OrderRequestPage()
        {
            InitializeComponent();
        }
        async protected override void OnAppearing()
        {
            await GetAllArtists();
        }
        async private void ImageBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public void totam(string type, int CountFace, string size, bool _isFrame)
        {
            totalpr = 0;




            if (_isFrame == false)
            {
                if (type == "Bigindianarts Simple Color")
                {

                    switch (size)
                    {
                        case "A4":
                            totalpr = 500 * CountFace;

                            break;
                        case "A3":
                            totalpr = 700 * CountFace;

                            break;
                        case "A2":
                            totalpr = 900 * CountFace;
                            break;
                        case "A1":
                            totalpr = 1500 * CountFace;
                            break;
                    }
                }

                if (type == "Bigindianarts Caricature")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 1700 * CountFace;

                            break;
                        case "A3":
                            totalpr = 2000 * CountFace;

                            break;
                        case "A2":
                            totalpr = 3000 * CountFace;
                            break;
                        case "A1":
                            totalpr = 4000 * CountFace;
                            break;
                    }
                }
                if (type == "Bigindianarts Pencil Colour")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 3500 * CountFace;

                            break;
                        case "A3":
                            totalpr = 4500 * CountFace;

                            break;
                        case "A2":
                            totalpr = 6000 * CountFace;
                            break;
                        case "A1":
                            totalpr = 8000 * CountFace;
                            break;
                    }
                }
                if (type == "Bigindianarts Pencil")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 2200 * CountFace;

                            break;
                        case "A3":
                            totalpr = 3000 * CountFace;

                            break;
                        case "A2":
                            totalpr = 4500 * CountFace;
                            break;
                        case "A1":
                            totalpr = 6000 * CountFace;
                            break;
                    }
                }
                if (type == "Bigindianarts Pastel Colour")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 1500 * CountFace;

                            break;
                        case "A3":
                            totalpr = 1700 * CountFace;

                            break;
                        case "A2":
                            totalpr = 2800 * CountFace;
                            break;
                        case "A1":
                            totalpr = 3500 * CountFace;
                            break;
                    }
                }
                if (type == "Bigindianarts Ink Caricature")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 1200 * CountFace;

                            break;
                        case "A3":
                            totalpr = 1500 * CountFace;

                            break;
                        case "A2":
                            totalpr = 2000 * CountFace;
                            break;
                        case "A1":
                            totalpr = 3000 * CountFace;
                            break;
                    }
                }
                if (type == "Bigindianarts Ink")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 1000 * CountFace;

                            break;
                        case "A3":
                            totalpr = 1200 * CountFace;

                            break;
                        case "A2":
                            totalpr = 1800 * CountFace;
                            break;
                        case "A1":
                            totalpr = 2700 * CountFace;
                            break;
                    }
                }
                if (type == "Bigindianarts Charcol")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 1000 * CountFace;

                            break;
                        case "A3":
                            totalpr = 1200 * CountFace;

                            break;
                        case "A2":
                            totalpr = 1800 * CountFace;
                            break;
                        case "A1":
                            totalpr = 2700 * CountFace;
                            break;
                    }
                }

                if (type == "Bigindianarts Digital")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 0 * CountFace;

                            break;
                        case "A3":
                            totalpr = 0 * CountFace;

                            break;
                        case "A2":
                            totalpr = 0 * CountFace;
                            break;
                        case "A1":
                            totalpr = 0 * CountFace;
                            break;
                    }
                }
                if (type == "Bigindianarts Painting")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 0 * CountFace;

                            break;
                        case "A3":
                            totalpr = 0 * CountFace;

                            break;
                        case "A2":
                            totalpr = 0 * CountFace;
                            break;
                        case "A1":
                            totalpr = 0 * CountFace;
                            break;
                    }
                }
                if (type == "Bigindianarts Simple")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = 300 * CountFace;

                            break;
                        case "A3":
                            totalpr = 500 * CountFace;

                            break;
                        case "A2":
                            totalpr = 700 * CountFace;
                            break;
                        case "A1":
                            totalpr = 1000 * CountFace;
                            break;
                    }
                }



            }
            else
            {
                CommiFrame = 0;
                if (type == "Bigindianarts Simple Color")
                {

                    switch (size)
                    {
                        case "A4":
                            totalpr = (500 * CountFace) + 600;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (700 * CountFace) + 900;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (900 * CountFace) + 1500;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (1500 * CountFace) + 2500;
                            CommiFrame = 2500;
                            break;
                    }
                }

                if (type == "Bigindianarts Caricature")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (1700 * CountFace) + 600;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (2000 * CountFace) + 900;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (3000 * CountFace) + 1500;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (4000 * CountFace) + 2500;
                            CommiFrame = 2500;
                            break;
                    }
                }
                if (type == "Bigindianarts Pencil Colour")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (3500 * CountFace) + 600;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (4500 * CountFace) + 900;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (6000 * CountFace) + 1500;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (8000 * CountFace) + 2500;
                            CommiFrame = 2500;
                            break;
                    }
                }
                if (type == "Bigindianarts Pencil")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (2200 * CountFace) + 600;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (3000 * CountFace) + 900;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (4500 * CountFace) + 1500;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (6000 * CountFace) + 2500;
                            CommiFrame = 2500;
                            break;
                    }
                }
                if (type == "Bigindianarts Pastel Colour")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (1500 * CountFace) + 600;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (1700 * CountFace) + 900;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (2800 * CountFace) + 1500;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (3500 * CountFace) + 2500;
                            CommiFrame = 2500;
                            break;
                    }
                }
                if (type == "Bigindianarts Ink Caricature")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (1200 * CountFace) + 600;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (1500 * CountFace) + 900;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (2000 * CountFace) + 1500;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (3000 * CountFace) + 2500;
                            CommiFrame = 2500;
                            break;
                    }
                }
                if (type == "Bigindianarts Ink")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (1000 * CountFace) + 600;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (1200 * CountFace) + 900;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (1800 * CountFace) + 1500;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (2800 * CountFace) + 2500;
                            CommiFrame = 2500;
                            break;
                    }
                }
                if (type == "Bigindianarts Charcol")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (1000 * CountFace) + 600;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (1200 * CountFace) + 900;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (1800 * CountFace) + 1500;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (2700 * CountFace) + 2500;
                            CommiFrame = 2500;
                            break;
                    }
                }
                if (type == "Bigindianarts Digital")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (0 * CountFace) + 0;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (0 * CountFace) + 0;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (0 * CountFace) + 0;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (0 * CountFace) + 0;
                            CommiFrame = 0;
                            break;
                    }
                }
                if (type == "Bigindianarts Painting")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (0 * CountFace) + 0;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (0 * CountFace) + 0;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (0 * CountFace) + 0;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (0 * CountFace) + 0;
                            CommiFrame = 2500;
                            break;
                    }
                }
                if (type == "Bigindianarts Simple")
                {
                    switch (size)
                    {
                        case "A4":
                            totalpr = (300 * CountFace) + 600;
                            CommiFrame = 600;
                            break;
                        case "A3":
                            totalpr = (500 * CountFace) + 900;
                            CommiFrame = 900;
                            break;
                        case "A2":
                            totalpr = (700 * CountFace) + 1500;
                            CommiFrame = 1500;
                            break;
                        case "A1":
                            totalpr = (1000 * CountFace) + 2500;
                            CommiFrame = 2500;
                            break;
                    }
                }
            }

            #region const


            //if (type == "Pencil Portrait")
            //{

            //    switch (GetDel)
            //    {
            //        case 0:
            //            totalpr += 0;
            //            delprize = 0;
            //            break;
            //        case 1:
            //            totalpr += 250;
            //            delprize = 250;
            //            break;

            //    }

            //    switch (GetFrm)
            //    {
            //        case 4:
            //            totalpr += 0;
            //            break;
            //        default:
            //            if (GetSiz == 0)
            //            {
            //                totalpr += 600;
            //                frmprize = 600;
            //            }
            //            else if (GetSiz == 1)
            //            {
            //                totalpr += 900;
            //                frmprize = 900;
            //            }
            //            else if (GetSiz == 2)
            //            {
            //                totalpr += 1500;
            //                frmprize = 1500;
            //            }
            //            else if (GetSiz == 3)
            //            {
            //                totalpr += 2200;
            //                frmprize = 2200;
            //            }
            //            break;

            //    }
            //    switch (GetSiz)
            //    {
            //        case 0:
            //            totalpr += 1200 * Convert.ToInt32(face);
            //            sizprize = 1200 * Convert.ToInt32(face);
            //            break;
            //        case 1:
            //            totalpr += 1700 * Convert.ToInt32(face);
            //            sizprize = 1700 * Convert.ToInt32(face);
            //            break;
            //        case 2:
            //            totalpr += 3900 * Convert.ToInt32(face);
            //            sizprize = 3900 * Convert.ToInt32(face);
            //            break;
            //        case 3:
            //            totalpr += 7000 * Convert.ToInt32(face);
            //            sizprize = 7000 * Convert.ToInt32(face);
            //            break;

            //    }


            //    switch (GetCol)
            //    {
            //        case 0:
            //            totalpr += 0;
            //            colprize = 0;
            //            break;
            //        case 1:
            //            totalpr += Convert.ToInt32(face) * 500;
            //            colprize = Convert.ToInt32(face) * 500;
            //            break;

            //    }







            //}
            //else if (type == "Pencil Caricature")
            //{

            //    switch (GetDel)
            //    {
            //        case 0:
            //            totalpr += 0;
            //            delprize = 0;
            //            break;
            //        case 1:
            //            totalpr += 250;
            //            delprize = 250;
            //            break;

            //    }

            //    switch (GetFrm)
            //    {
            //        case 4:
            //            totalpr += 0;
            //            break;
            //        default:
            //            if (GetSiz == 0)
            //            {
            //                totalpr += 600;
            //                frmprize = 600;
            //            }
            //            else if (GetSiz == 1)
            //            {
            //                totalpr += 900;
            //                frmprize = 900;
            //            }
            //            else if (GetSiz == 2)
            //            {
            //                totalpr += 1500;
            //                frmprize = 1500;
            //            }
            //            else if (GetSiz == 3)
            //            {
            //                totalpr += 2200;
            //                frmprize = 2200;
            //            }
            //            break;

            //    }
            //    switch (GetSiz)
            //    {
            //        case 0:
            //            totalpr += 1200 * Convert.ToInt32(face);
            //            sizprize = 1200 * Convert.ToInt32(face);
            //            break;
            //        case 1:
            //            totalpr += 1700 * Convert.ToInt32(face);
            //            sizprize = 1700 * Convert.ToInt32(face);
            //            break;
            //        case 2:
            //            totalpr += 3900 * Convert.ToInt32(face);
            //            sizprize = 3900 * Convert.ToInt32(face);
            //            break;
            //        case 3:
            //            totalpr += 7000 * Convert.ToInt32(face);
            //            sizprize = 7000 * Convert.ToInt32(face);
            //            break;

            //    }


            //    switch (GetCol)
            //    {
            //        case 0:
            //            totalpr += 0;
            //            colprize = 0;
            //            break;
            //        case 1:
            //            totalpr += 0;
            //            colprize = 0;
            //            break;

            //    }




            //}
            //else if (type == "Simple Sketch")
            //{

            //    switch (GetDel)
            //    {
            //        case 0:
            //            totalpr += 0;
            //            delprize = 0;
            //            break;
            //        case 1:
            //            totalpr += 250;
            //            delprize = 250;
            //            break;

            //    }

            //    switch (GetFrm)
            //    {
            //        case 4:
            //            totalpr += 0;
            //            break;
            //        default:
            //            if (GetSiz == 0)
            //            {
            //                totalpr += 600;
            //                frmprize = 600;
            //            }
            //            else if (GetSiz == 1)
            //            {
            //                totalpr += 900;
            //                frmprize = 900;
            //            }
            //            else if (GetSiz == 2)
            //            {
            //                totalpr += 1500;
            //                frmprize = 1500;
            //            }
            //            else if (GetSiz == 3)
            //            {
            //                totalpr += 2200;
            //                frmprize = 2200;
            //            }
            //            break;

            //    }
            //    switch (GetSiz)
            //    {
            //        case 0:
            //            totalpr += 300 * Convert.ToInt32(face);
            //            sizprize = 300 * Convert.ToInt32(face);
            //            break;
            //        case 1:
            //            totalpr += 450 * Convert.ToInt32(face);
            //            sizprize = 450 * Convert.ToInt32(face);
            //            break;
            //        case 2:
            //            totalpr += 600 * Convert.ToInt32(face);
            //            sizprize = 600 * Convert.ToInt32(face);
            //            break;
            //        case 3:
            //            totalpr += 900 * Convert.ToInt32(face);
            //            sizprize = 900 * Convert.ToInt32(face);
            //            break;

            //    }


            //    switch (GetCol)
            //    {
            //        case 0:
            //            totalpr += 0;
            //            colprize = 0;
            //            break;
            //        case 1:
            //            totalpr += Convert.ToInt32(face) * 200;
            //            colprize = Convert.ToInt32(face) * 200;
            //            break;

            //    }



            //}
            //else if (type == "Simple Caricature")
            //{

            //    switch (GetDel)
            //    {
            //        case 0:
            //            totalpr += 0;
            //            delprize = 0;
            //            break;
            //        case 1:
            //            totalpr += 250;
            //            delprize = 250;
            //            break;

            //    }

            //    switch (GetFrm)
            //    {
            //        case 4:
            //            totalpr += 0;
            //            break;
            //        default:
            //            if (GetSiz == 0)
            //            {
            //                totalpr += 600;
            //                frmprize = 600;
            //            }
            //            else if (GetSiz == 1)
            //            {
            //                totalpr += 900;
            //                frmprize = 900;
            //            }
            //            else if (GetSiz == 2)
            //            {
            //                totalpr += 1500;
            //                frmprize = 1500;
            //            }
            //            else if (GetSiz == 3)
            //            {
            //                totalpr += 2200;
            //                frmprize = 2200;
            //            }
            //            break;

            //    }
            //    switch (GetSiz)
            //    {
            //        case 0:
            //            totalpr += 600 * Convert.ToInt32(face);
            //            sizprize = 600 * Convert.ToInt32(face);
            //            break;
            //        case 1:
            //            totalpr += 800 * Convert.ToInt32(face);
            //            sizprize = 800 * Convert.ToInt32(face);
            //            break;
            //        case 2:
            //            totalpr += 1000 * Convert.ToInt32(face);
            //            sizprize = 1000 * Convert.ToInt32(face);
            //            break;
            //        case 3:
            //            totalpr += 1200 * Convert.ToInt32(face);
            //            sizprize = 1200 * Convert.ToInt32(face);
            //            break;

            //    }


            //    switch (GetCol)
            //    {
            //        case 0:
            //            totalpr += 0;
            //            colprize = 0;
            //            break;
            //        case 1:
            //            totalpr += Convert.ToInt32(face) * 200;
            //            colprize = Convert.ToInt32(face) * 200;
            //            break;

            //    }



            //}


            #endregion


            // totalpr =  sizprize;
            ent_price.Text = totalpr.ToString();
            ent_balance.Text = (Convert.ToDecimal(ent_price.Text) - Convert.ToDecimal(ent_advance.Text)).ToString();
        }
        async private void submit_clicked(object sender, EventArgs e)
        {

            if (submitTap)
            {
                return;
            }
            submitTap = true;
            if (chk_online.IsChecked == false)
            {
                if (string.IsNullOrEmpty(ent_Billnumber.Text))
                {
                    submitTap = false;
                    DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Bill Number");
                    ent_Billnumber.Focus();
                    return;
                }
            }
            if (string.IsNullOrEmpty(assignName))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please Select Artist");
                combo_artist.Focus();
                return;
            }
            if (string.IsNullOrEmpty(combo_art.Text))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please Select Art");
                combo_art.Focus();
                return;
            }
            if (string.IsNullOrEmpty(combo_count.Text))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please select count");
                combo_count.Focus();
                return;
            }
            if (string.IsNullOrEmpty(combo_frame.Text))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please select frame");
                combo_frame.Focus();
                return;
            }
            if (string.IsNullOrEmpty(combo_delivery_mode.Text))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please select delivery mode");
                combo_delivery_mode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(combo_size.Text))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please select size");
                combo_size.Focus();
                return;
            }
            if (string.IsNullOrEmpty(combo_payMethod.Text))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please select payment methode");
                combo_payMethod.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ent_contactNumber.Text))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter contact number");
                ent_contactNumber.Focus();
                return;
            }
            if (!CheckNumber(ent_contactNumber.Text))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Valid number");
                ent_contactNumber.Focus();
                return;
            }
            if (string.IsNullOrEmpty(ent_price.Text))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please enter Price");
                ent_price.Focus();
                return;
            }
            if (string.IsNullOrEmpty(item))
            {
                submitTap = false;
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please Add Image");

                return;
            }

            int priceTotal = 0;
            priceTotal = Convert.ToInt32(ent_price.Text);
            int amount;
            if (IsFrame == true)
            {
                await Task.Delay(20);
                amount = priceTotal - CommiFrame;
                commissionAmount = amount * commisionpercent / 100;
            }
            else
            {
                amount = priceTotal;
                commissionAmount = amount * commisionpercent / 100;
            }



            if (commissionAmount == 0)
            {
                submitTap = false;
                await DisplayAlert("Error", "Artist Commission 0 Please Retry", "OK");
                return;
            }
            OrderModel _orderData = new OrderModel();

            DateTime _now = DateTime.Now;
            _orderData.advance = Convert.ToInt32(ent_advance.Text);
            _orderData.createdon = new DateTime(_now.Date.Year, _now.Date.Month, _now.Date.Day, _now.Hour, _now.Minute, _now.Second);
            _orderData.assign_id = assignID;
            _orderData.assign_empid = assign_empid;
            _orderData.assign_name = assignName;
            _orderData.contact_number = ent_contactNumber.Text;
            _orderData.customer_name = ent_customerName.Text;
            _orderData.bill_number = ent_Billnumber.Text;
            _orderData.frame = combo_frame.Text;
            _orderData.expected_date = new DateTime(date_expected.Date.Year, date_expected.Date.Month, date_expected.Date.Day, time_expected.Time.Hours, time_expected.Time.Minutes, time_expected.Time.Seconds);
            _orderData.people_no = Convert.ToInt32(combo_count.Text);
            _orderData.price = Convert.ToInt32(ent_price.Text);
            _orderData.size = combo_size.Text;
            _orderData.type = combo_art.Text;
            _orderData.orientation = combo_orientation.Text;
            _orderData.commission = commissionAmount.ToString();
            _orderData.status = "Pending";
            _orderData.delivery_mode = combo_delivery_mode.Text;
            _orderData.uploaded_image = item;
            _orderData.remarks = ent_remarks.Text;
            _orderData.paymentmode = combo_payMethod.Text;
            _orderData.online_order = chk_online.IsChecked.ToString();
            _orderData.isVideo = chk_video.IsChecked.ToString();


            _apiResponce = await App._dataManager.OrderTask(_orderData);

            if (_apiResponce == null)
            {
                submitTap = false;
                UserDialogs.Instance.HideLoading();
                DependencyService.Get<IAlertPlayer>().AlertMessege("Please check your network connection or try again later.");

                return;
            }

            if (_apiResponce.IsSuccess == true)
            {
                if (_apiResponce.Data == null)
                {
                    submitTap = false;
                    UserDialogs.Instance.HideLoading();

                    return;
                }

                var dat = _apiResponce.Data;

                await DisplayAlert("Message", "Artist Order Created Successfully", "OK");
                submitTap = false;
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Message", "Artist Order Created failed", "OK");
                submitTap = false;
            }
        }
        public async Task GetAllArtists()
        {
            APIResponse _apiresponse = new APIResponse();
            List<ArtistsModel> _artists = new List<ArtistsModel>();
            _apiresponse = await App._dataManager.GetAllArtists();
            try
            {
                if (_apiresponse == null)
                {
                    DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    return;
                }
                else
                {
                    if (_apiresponse.IsSuccess == true)
                    {
                        if (_apiresponse.Data == null)
                        {
                            DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                            return;
                        }
                        else
                        {
                            _artists = _apiresponse.Data.ConvertAs<List<ArtistsModel>>();

                            if (_artists != null)
                            {

                                foreach (var _item in _artists)
                                {

                                    if (!string.IsNullOrEmpty(_item.creted_on))
                                    {
                                        var _crDate = Convert.ToDateTime(_item.creted_on).ToLocalTime();
                                        _item.dateCreated = _crDate.ToString("dd MMMM yyyy hh:mm tt");
                                    }
                                }


                                combo_artist.DataSource = _artists;
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                    }
                }
            }
            catch (Exception ev)
            {
                DependencyService.Get<IAlertPlayer>().AlertMessege("Oops! Something went wrong");
                return;
            }
        }
        async private void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async Task AddSignToSTorage(byte[] _fileBytes, string _fileName)
        {
            try
            {
                LoadingPage loadingPage = new LoadingPage();
                await PopupNavigation.Instance.PushAsync(loadingPage);
                UserDialogs.Instance.ShowLoading("", MaskType.Black);
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.ConstrainedInternet || current == NetworkAccess.None || current == NetworkAccess.Unknown)
                {

                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "No network connection", "OK");

                    return;
                }

                StoredMediaAssets _addstoragerslt = new StoredMediaAssets();
                List<StoredMediaAssets> _mediaAssetRslt = new List<StoredMediaAssets>();
                MediaStorageClient _mediastorageclient = new MediaStorageClient();

                //List<string> _files = new List<string>();
                //_files.Add(fileData);

                //_apiResponce = await _mediastorageclient.AddToStorage(_files);

                _apiResponce = await _mediastorageclient.AddToStorage(_fileBytes, _fileName);
                if (_apiResponce == null)
                {
                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "Oops!!! Something went wrong", "OK");
                    Application.Current.MainPage = new FlyoutPage();
                    return;
                }

                if (_apiResponce.IsSuccess != true)
                {

                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "API Responce Error", "OK");

                    return;
                }
                if (_apiResponce.Data == null)
                {

                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Alert", "Oops!!! Something went wrong", "OK");


                    return;
                }

                _addstoragerslt = _apiResponce.Data.ConvertAs<StoredMediaAssets>();

                item = _addstoragerslt.Url;
                if (!string.IsNullOrWhiteSpace(item))
                {

                    ent_upload.Text = Path.GetFileName(item);
                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }

                }

            }
            catch (Exception ex)
            {
                if (PopupNavigation.PopupStack.Count > 0)
                {
                    await PopupNavigation.Instance.PopAsync();
                }

                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Alert", "Oops!!! Something went wrong", "OK");
                Application.Current.MainPage = new FlyoutPage();

            }
        }

        async private void ImageButtonClicked(object sender, EventArgs e)
        {
            //try
            //{
            //    var result = await MediaPicker.CapturePhotoAsync();
            //    if (result != null)
            //    {
            //        TextFile = $"File Name: {result.FileName}";
            //        LocFile = result.FullPath;

            //    }
            //    await AddSignToSTorage(LocFile);


            //}
            //catch (Exception ex)
            //{
            //    // The user canceled or something went wrong
            //}
            MediaPopup mediaPage = new MediaPopup(1);
            mediaPage.Disappearing += MediaPage_Disappearing;
            await PopupNavigation.Instance.PushAsync(mediaPage);

        }

        async private void MediaPage_Disappearing(object sender, EventArgs e)
        {
            if (MediaResults != null)
            {
                TextFile = $"File Name: {MediaResults.FileName}";
                LocFile = MediaResults.FullPath;
                var stream = MediaResults.OpenReadAsync().Result;
                byte[] _imgByte = await ResizeImage(stream);
                await AddSignToSTorage(_imgByte, MediaResults.FileName);
            }

        }

        private void Combo_art_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            selectedType = e.Value.ToString();

            totam(selectedType, selectedCount, selectedSize, IsFrame);
        }

        private void Combo_count_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            selectedCount = Convert.ToInt32(e.Value);

            totam(selectedType, selectedCount, selectedSize, IsFrame);
        }

        private void Combo_size_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            selectedSize = e.Value.ToString();

            totam(selectedType, selectedCount, selectedSize, IsFrame);
        }

        private void Combo_frame_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            string _fram = e.Value.ToString();
            if (_fram == "Yes")
            {
                IsFrame = true;
            }
            else
            {
                IsFrame = false;
            }

            totam(selectedType, selectedCount, selectedSize, IsFrame);
        }

        private void Combo_artist_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (!(e.Value is ArtistsModel item))
                return;
            assignID = Convert.ToInt32(item.id);
            assign_empid = Convert.ToInt32(item.login_id);
            assignName = item.name;
            commisionpercent = Convert.ToDecimal(item.commission);

        }

        async private void ent_Billnumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (ent_Billnumber.Text.Count() < 4)
            {
                await DisplayAlert("Alert", "Bill Number Must be 4 Digit", "OK");
            }
        }

        private void chk_online_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                ent_Billnumber.IsEnabled = true;
            }
            else
            {
                ent_Billnumber.IsEnabled = true;
            }
        }

        bool CheckNumber(string strPhoneNumber)
        {
            string MatchPhoneNumberPattern = "^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            if (strPhoneNumber != null)
                return Regex.IsMatch(strPhoneNumber, MatchPhoneNumberPattern);
            else
                return false;


        }

        async private void ent_contactNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (!CheckNumber(ent_contactNumber.Text))
            {
                await DisplayAlert("Alert", "Please enter Valid Phone Number", "OK");
            }
        }

        private void chk_caption_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == false)
            {
                ent_remarks.IsVisible = false;
                lbl_caption.IsVisible = false;
            }
            else
            {
                ent_remarks.IsVisible = true;
                lbl_caption.IsVisible = true;
            }
        }

        private void ent_advance_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ent_advance.Text) && string.IsNullOrWhiteSpace(ent_price.Text))
            {
                ent_balance.Text = (0).ToString();
            }
            else if (string.IsNullOrWhiteSpace(ent_advance.Text))
            {
                ent_balance.Text = (Convert.ToDecimal(ent_price.Text) - 0).ToString();
            }
            else if (string.IsNullOrWhiteSpace(ent_price.Text))
            {
                ent_balance.Text = (0 - Convert.ToDecimal(ent_advance.Text)).ToString();
            }
            else
            {
                ent_balance.Text = (Convert.ToDecimal(ent_price.Text) - Convert.ToDecimal(ent_advance.Text)).ToString();
            }

        }

        private void ent_price_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ent_price.Text) && string.IsNullOrWhiteSpace(ent_advance.Text))
            {
                ent_balance.Text = (0).ToString();
            }
            else if (string.IsNullOrWhiteSpace(ent_advance.Text))
            {
                ent_balance.Text = (Convert.ToDecimal(ent_price.Text) - 0).ToString();
            }
            else if (string.IsNullOrWhiteSpace(ent_price.Text))
            {
                ent_balance.Text = (0 - Convert.ToDecimal(ent_advance.Text)).ToString();
            }
            else
            {
                ent_balance.Text = (Convert.ToDecimal(ent_price.Text) - Convert.ToDecimal(ent_advance.Text)).ToString();
            }
        }

        private void combo_delivery_mode_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }
        protected async Task<byte[]> ResizeImage(Stream stream)
        {
            byte[] imageData;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imageData = ms.ToArray();
            }

            //if (Device.RuntimePlatform == Device.Android)
            //{
            byte[] resizedImage = await DependencyService.Get<IImageResizer>().ResizeImage(imageData);
            // image.Source = ImageSource.FromStream(() => new MemoryStream(resizedImage));
            return resizedImage;
            //}
            //else
            //{
            //    return imageData;
            //}
        }
    }
}