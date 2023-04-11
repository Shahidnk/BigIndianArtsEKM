using BigIndianArts.Models;
using BigIndianArts.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace BigIndianArts.ViewModels
{
    class ShipmentViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ShipmentDetails> ShipmentInfoCollection { get; set; }

        public ShipmentViewModel()
        {
            ShipmentInfoCollection = new ObservableCollection<ShipmentDetails>();
            ShipmentInfoCollection.Add(CreateShipmentInfo("Ordered and Approved", "Seller has processed your order", DateTime.Now.ToString("dd MMM yyyy"), "10:10 AM", StepStatus.Completed, 100));
            ShipmentInfoCollection.Add(CreateShipmentInfo("Packed", "Your item has been picked by courier partner", DateTime.Now.AddDays(1).ToString("dd MMM yyyy"), "01:37 PM", StepStatus.Completed, 100));
            ShipmentInfoCollection.Add(CreateShipmentInfo("Shipped", "", DateTime.Now.AddDays(1).ToString("dd MMM yyyy"), "02:50 PM", StepStatus.InProgress, 50));
            ShipmentInfoCollection.Add(CreateShipmentInfo("Delivered", "", DateTime.Now.AddDays(2).ToString("dd MMM yyyy"), "10:00 AM", StepStatus.NotStarted,0));
        }

        public ShipmentDetails CreateShipmentInfo(string title, string description, string date, string time, StepStatus status, int progress)
        {
            ShipmentDetails shipment = new ShipmentDetails()
            {
                Title = title,
                Description = description,
                Date = date,
                Time = time,
                Status = status,
                ProgressValue = progress
            };

            return shipment;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
