#define COSEMSTRUCTURE
#define KAMSTRUP
#undef AIDON

using System;
using System.IO.Ports;
using System.Collections;
using System.Text;
using HAN_DLMS;
using HAN_COSEM;

namespace HAN_Metering_System
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
#if KAMSTRUP            
            serialPort.Parity = Parity.None; // 0=None, 1=Odd, 2=Even, 3=Mark, 4=Space
#endif
            bool portOk = false;
            
        Console.Write("Available Ports");
        foreach (string s in SerialPort.GetPortNames())
        {
            Console.Write("; {0}", s);
            if ( s == serialPort.PortName) portOk = true;
        }
        Console.WriteLine();

        if ( !portOk ) {
            Console.WriteLine("Port name spesified \"{0}\" not found on this system.",serialPort.PortName);
            Console.WriteLine("Program exception thrown and program stops.");
            throw new InvalidProgramException("Could not find serial port \"" + serialPort.PortName + "\"");
            Environment.Exit(-1);
        }

            // serialPort.ReadTimeout = 1000; // 1 second
            // Initiate DLMS clasess/objects
            DLMS dLMS = new DLMS();
            COSEMClass cOSEM = new COSEMClass();
            cOSEM.cOSEMInitialize();


            serialPort.Open();
            if ( !serialPort.IsOpen )
            {
                Console.WriteLine("Error in opening port {0}",serialPort.PortName);
            }

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
