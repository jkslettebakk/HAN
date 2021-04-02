#define COSEMSTRUCTURE

using System;
using System.IO.Ports;
using System.Collections;
using System.Text;
using System.Diagnostics;
using Slettebakk_DLMS;

namespace HAN_Norway
{
    class Program
    {
        static SerialPort serialPort;
        static int Main(string[] args)
        {
            serialPort = new SerialPort("/dev/ttyUSB0");
            serialPort.BaudRate = 2400;
            serialPort.Parity = Parity.Even; // 0=None, 1=Odd, 2=Even, 3=Mark, 4=Space
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One; // None (0), One (1), Two (2), OnePointFive (3)

            // serialPort.ReadTimeout = 1000; // 1 second
            // Initiate DLMS clasess/objects
            DLMS dLMS = new DLMS();

            for (int i=0; ;i++ )
            {
            Console.WriteLine("waiting for debugger attach (i={0:D2}, debugger.IsAttached={1})",i,Debugger.IsAttached);
            if (Debugger.IsAttached) break;
            System.Threading.Thread.Sleep(1000);
            }

            string byteString;
            string[] HANserialPortData = new string[3];
            int oldString = 0; int currentString = 1; int newString = 2; 
            int bytesLength;

            serialPort.Open();

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
                return 1;
            }

            Console.WriteLine("Ending program");
            serialPort.Close();
            serialPort.Dispose();


            Console.WriteLine("Hello World!");
            return 0;
        }
    }
}
