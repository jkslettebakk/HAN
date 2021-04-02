#define COSEMSTRUCTURE

using System;
using System.IO.Ports;
using System.Collections;
using System.Text;
using Slettebakk_DLMS;

namespace HAN_Norway
{
    class Program
    {
        static SerialPort serialPort;
        static int Main(string[] args)
        {
            // serialPort = new SerialPort("COM3");
            serialPort = new SerialPort("/dev/ttyUSB0");
            serialPort.BaudRate = 2400;
            serialPort.Parity = Parity.Even; // 0=None, 1=Odd, 2=Even, 3=Mark, 4=Space
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One; // None (0), One (1), Two (2), OnePointFive (3)

            // serialPort.ReadTimeout = 1000; // 1 second
            // Initiate DLMS clasess/objects
            DLMS dLMS = new DLMS();

            serialPort.Open();
            if ( !serialPort.IsOpen )
            {
                Console.WriteLine("Error in opening port {0}",serialPort.PortName);
            }

            // Console.TreatControlCAsInput = true;

            try
            {
                while ( true )
                {
                    dLMS.readDLMSstreamFromHAN(serialPort);
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine("\nAbnormal exit:\n{0}",ex);
                serialPort.Close();
                serialPort.Dispose();
                Console.WriteLine("Ending program");
                serialPort.Close();
                serialPort.Dispose();
                return 1;
            }
        }
    }
}
