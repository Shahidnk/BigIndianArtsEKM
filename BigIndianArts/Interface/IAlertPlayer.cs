using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Interface
{
    public interface IAlertPlayer
    {
        void AlertMessege(string messege);

        void CloseApp();

        bool IsNetworkAvailable();
    }
}
