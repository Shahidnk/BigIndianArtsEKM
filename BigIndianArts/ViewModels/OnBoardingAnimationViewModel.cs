using System;
using System.Collections.Generic;
using System.Text;
using Syncfusion.SfRotator.XForms;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using BigIndianArts.Commands;
using BigIndianArts.Models;
using BigIndianArts.Views;
using BigIndianArts.Views.OnBoarding;

namespace BigIndianArts.ViewModels
{
    public class OnBoardingAnimationViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OnBoardingAnimationViewModel" /> class.
        /// </summary>
        public OnBoardingAnimationViewModel()
        {
            SkipCommand = new DelegateCommand(Skip);
            NextCommand = new DelegateCommand(Next);
            Boardings = new ObservableCollection<Boarding>
            {
                new Boarding
                {
                    ImagePath ="onboarding01",
                    Header = "Request Management",
                    Content = "Option to create all your request of IT, HR and Admin",
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding
                {
                    ImagePath ="onboarding02",
                    Header = "Approval Management",
                    Content = "Automated TAT to remind and help you approve all your Team request ",
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding
                {
                    ImagePath ="onboarding03",
                    Header = "Work Management",
                    Content = "Create and assign the task to manage the work",
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding
                {
                    ImagePath ="onboarding04",
                    Header = "Self Learning",
                    Content = "All the training videos to learn and understand the process",
                    RotatorItem = new WalkthroughItemPage()
                }
            };

            // Set bindingcontext to content view.
            foreach (var boarding in Boardings) boarding.RotatorItem.BindingContext = boarding;
        }

        #endregion

        #region Fields

        private ObservableCollection<Boarding> boardings;

        private string nextButtonText = "Next";
        private string skipButtonText = "SKIP";

        private bool isSkipButtonVisible = true;

        private int selectedIndex;

        #endregion

        #region Properties

        public ObservableCollection<Boarding> Boardings
        {
            get => boardings;

            set
            {
                if (boardings == value) return;

                boardings = value;
                OnPropertyChanged();
            }
        }

        public string NextButtonText
        {
            get => nextButtonText;

            set
            {
                if (nextButtonText == value) return;

                nextButtonText = value;
                OnPropertyChanged();
            }
        }
        public string SkipButtonText
        {
            get => skipButtonText;

            set
            {
                if (skipButtonText == value) return;

                skipButtonText = value;
                OnPropertyChanged();
            }
        }

        public bool IsSkipButtonVisible
        {
            get => isSkipButtonVisible;

            set
            {
                if (isSkipButtonVisible == value) return;

                isSkipButtonVisible = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get => selectedIndex;

            set
            {
                if (selectedIndex == value) return;

                selectedIndex = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Skip button is clicked.
        /// </summary>
        public DelegateCommand SkipCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Done button is clicked.
        /// </summary>
        public DelegateCommand NextCommand { get; set; }

        #endregion

        #region Methods     

        private bool ValidateAndUpdateSelectedIndex(int itemCount)
        {
            if (SelectedIndex >= itemCount - 1) return true;

            SelectedIndex++;
            return false;
        }

        /// <summary>
        /// Invoked when the Skip button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Skip(object obj)
        {
            MoveToNextPage();
        }

        /// <summary>
        /// Invoked when the Done button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Next(object obj)
        {
            var itemCount = (obj as SfRotator).ItemsSource.Count();
            if (ValidateAndUpdateSelectedIndex(itemCount)) MoveToNextPage();
        }

        private void MoveToNextPage()
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        #endregion
    }
}
