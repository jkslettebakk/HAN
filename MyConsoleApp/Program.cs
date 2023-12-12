using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SerializeExtra
{
    public class Program
    {
        public static void Main( string[] args )
        {
            byte[] time = 
            { 
                0x07, 0xE6, 0x04, 0x18, 0x00, 0x13, 0x00, 0x00, 0xFF, 0x80, 0x00, 0xFF 
            };
            int cOSEMIndex = -1;
            
            foreach( byte b in time) Console.Write("{0:x2} ",b);
            Console.WriteLine();
            foreach( byte b in time) Console.Write("{0} ",b);
            Console.WriteLine("Shift left 8 for year; The rest is bytes. I need to get the GMT/Timesone value.");
            String MeeterTime =    ((uint)((time[cOSEMIndex + 1] << 8) + (time[cOSEMIndex + 2]))).ToString("D4") + "-" +
                                    ((uint)(time[cOSEMIndex + 3])).ToString("D2") + "-" +
                                    ((uint)(time[cOSEMIndex + 4])).ToString("D2") + "T" +
                                    ((uint)(time[cOSEMIndex + 6])).ToString("D2") + ":" +
                                    ((uint)(time[cOSEMIndex + 7])).ToString("D2") + ":" +
                                    ((uint)(time[cOSEMIndex + 8])).ToString("D2");
            Console.WriteLine("a) MeterTime in first {0} (last={1}) elements in text = {2}", cOSEMIndex + 8, time[cOSEMIndex + 8], MeeterTime);
            int daylightSaving = (time[cOSEMIndex + 10] >> 7);
                     MeeterTime = ((uint)((time[cOSEMIndex + 1] << 8) + (time[cOSEMIndex + 2]))).ToString("D4") + "-" +
                                  ((uint)(time[cOSEMIndex + 3])).ToString("D2") + "-" +
                                  ((uint)(time[cOSEMIndex + 4])).ToString("D2") + "T" +
                                  ((uint)(time[cOSEMIndex + 6])).ToString("D2") + ":" +
                                  ((uint)(time[cOSEMIndex + 7])).ToString("D2") + ":" +
                                  ((uint)(time[cOSEMIndex + 8])).ToString("D2") + "+" +
                                  ((uint)daylightSaving).ToString("D2");
            Console.WriteLine("b) MeterTime in first {0} (last={1}) elements in text = {2}", cOSEMIndex + 8, time[cOSEMIndex + 8], MeeterTime);

            Console.WriteLine("Bytes for kWh from HAN:");
            byte[] kWh = 
            { 
                0x00, 0x85, 0x6F, 0x6D
            };
            foreach( byte b in kWh) Console.Write("{0:x2} ",b);
            Console.WriteLine();
            double currentPower = Math.Round( ( (kWh[0] << 24) + (kWh[1] << 16) + (kWh[2] << 8) + (kWh[3] << 0) ) * 0.0100, 3);
            Console.WriteLine("Calculated value (<<24, 16, 8 and 0) * 0.01 ={0}",currentPower);
            double kwh0, kwh1, kwh2, kwh3;
            kwh0 = Math.Round((kWh[0] << 24) * 1.0000);
            kwh1 = Math.Round((kWh[1] << 16) * 1.0000);
            kwh2 = Math.Round((kWh[2] <<  8) * 1.0000);
            kwh3 = Math.Round((kWh[3] <<  0) * 1.0000);
            Console.WriteLine("kwh1={0}, kwh1={1}, kwh2={2}, khh3={3} (with math)",kwh0, kwh1, kwh2, kwh3);
            Console.WriteLine("(kwh1+kwh1+kwh2+khh3)*0.0100={0}",Math.Round((kwh0+kwh1+kwh2+kwh3)*0.0100,3));

            kwh0 = (kWh[0] << 24);
            kwh1 = (kWh[1] << 16);
            kwh2 = (kWh[2] <<  8);
            kwh3 = (kWh[3] <<  0);
            Console.WriteLine("kwh1={0}, kwh1={1}, kwh2={2}, khh3={3}",kwh0, kwh1, kwh2, kwh3);
            Console.WriteLine("(kwh1+kwh1+kwh2+khh3)*0.0100={0}",Math.Round((kwh0+kwh1+kwh2+kwh3)*0.0100,3));

        }
    }
}
