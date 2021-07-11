using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.UI.Xam.General
{
    //
    // Resumen:
    //     Interface for Bluetooth printer
    public interface IPrint
    {
        //
        // Resumen:
        //     Print the input stirng to blue tooth printer
        //
        // Parámetros:
        //   input:
        //     input data in string format
        //
        //   printerName:
        //     name of the paired bluetooth printer
        void PrintText(string input);

		bool CheckStatus();

        List<DeviceBluetooth> DevicesName();
    }
}
