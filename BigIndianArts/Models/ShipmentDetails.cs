using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace BigIndianArts.Models
{
    public class ShipmentDetails : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public StepStatus Status { get; set; }

        private int progressValue;
        public int ProgressValue
        {
            get => progressValue;

            set
            {
                progressValue = value;
                OnPropertyChanged("ProgressValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public enum StepStatus
    {
        InProgress,
        NotStarted,
        Completed
    }
}
