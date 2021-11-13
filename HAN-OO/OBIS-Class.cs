#undef OBISDEBUG


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HAN_OBIS
{
    class obisCodesClass
    {

        struct obisCodesStruct
        {
            public byte A { get; set; }
            public byte B { get; set; }
            public byte C { get; set; }
            public byte D { get; set; }
            public byte E { get; set; }
            public byte F { get; set; }
            public string UoM { get; set; }
            public string obis { get; set; }
            public int bytes {get; set; }
            public string objectName { set; get; }
            public string HAN_Vendor { get; set; }
        }

        public struct oBIS_1_0_1_7_0_255
        {
            public int activePower { get; set; }

        }

        //a obis struct list
        List<obisCodesStruct> legalObisCodes = new List<obisCodesStruct>
        {
            new obisCodesStruct {A=1, B=0, C=1,  D=7, E=0,   F=255, obis= "1.0.1.7.0.255",  UoM= "W",    objectName="Q1+Q4",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=1, C= 0, D=2, E=129, F=255, obis= "1.1.0.2.129.255",UoM= "' '",  objectName="Version identifier", HAN_Vendor ="Aidon & Kamstrup"},
            new obisCodesStruct {A=0, B=0, C=96, D=1, E=  0, F=255, obis= "0.0.96.1.0.255", UoM= "' '",  objectName="Model ID",           HAN_Vendor ="Aidon"}, 
            new obisCodesStruct {A=0, B=0, C=96, D=1, E=  7, F=255, obis= "0.0.96.1.7.255", UoM= "' '",  objectName="Model type",         HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 2, D=7, E=  0, F=255, obis= "1.0.2.7.0.255",  UoM= "W",    objectName="Q2+Q3",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 3, D=7, E=  0, F=255, obis= "1.0.3.7.0.255",  UoM= "kVAr", objectName="Q1+Q2",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 4, D=7, E=  0, F=255, obis= "1.0.4.7.0.255",  UoM= "kVAr", objectName="Q3+Q4",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=31, D=7, E=  0, F=255, obis= "1.0.31.7.0.255", UoM= "A",    objectName="IL1",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=51, D=7, E=  0, F=255, obis= "1.0.51.7.0.255", UoM= "A",    objectName="IL2",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=71, D=7, E=  0, F=255, obis= "1.0.71.7.0.255", UoM= "A",    objectName="IL3",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=32, D=7, E=  0, F=255, obis= "1.0.32.7.0.255", UoM= "V",    objectName="UL1",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=52, D=7, E=  0, F=255, obis= "1.0.52.7.0.255", UoM= "V",    objectName="UL2",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=72, D=7, E=  0, F=255, obis= "1.0.72.7.0.255", UoM= "V",    objectName="UL3",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=0, B=0, C= 1, D=0, E=  0, F=255, obis= "0.0.1.0.0.255",  UoM= "' '",  objectName="Clock and Date",     HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 1, D=8, E=  0, F=255, obis= "1.0.1.8.0.255",  UoM= "Wh",   objectName="Q1+Q4",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 2, D=8, E=  0, F=255, obis= "1.0.2.8.0.255",  UoM= "Wh",   objectName="Q2+Q3",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 3, D=8, E=  0, F=255, obis= "1.0.3.8.0.255",  UoM= "kVArh",objectName="Q1+Q2",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 4, D=8, E=  0, F=255, obis= "1.0.4.8.0.255",  UoM= "kVArh",objectName="Q3+Q4",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=1, C= 0, D=0, E=  5, F=255, obis= "1.1.0.0.5.255",  UoM= "' '",  objectName="GS1 number",         HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 1, D=7, E=  0, F=255, obis= "1.1.1.7.0.255",  UoM= "W",    objectName="P14",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 2, D=7, E=  0, F=255, obis= "1.1.2.7.0.255",  UoM= "W",    objectName="P23",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=31, D=7, E=  0, F=255, obis= "1.1.31.7.0.255", UoM= "A",    objectName="IL1",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=51, D=7, E=  0, F=255, obis= "1.1.51.7.0.255", UoM= "A",    objectName="IL2",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=71, D=7, E=  0, F=255, obis= "1.1.71.7.0.255", UoM= "A",    objectName="IL3",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=32, D=7, E=  0, F=255, obis= "1.1.32.7.0.255", UoM= "V",    objectName="UL1",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=52, D=7, E=  0, F=255, obis= "1.1.52.7.0.255", UoM= "V",    objectName="UL2",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=72, D=7, E=  0, F=255, obis= "1.1.72.7.0.255", UoM= "V",    objectName="UL3",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=0, B=1, C= 1, D=0, E=  0, F=255, obis= "0.1.1.0.0.255",  UoM= "' '",  objectName="RTC",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 1, D=8, E=  0, F=255, obis= "1.1.1.8.0.255",  UoM= "Wh",   objectName="A14",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 2, D=8, E=  0, F=255, obis= "1.1.2.8.0.255",  UoM= "Wh",   objectName="A23",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=96, D=1, E=  1, F=255, obis= "1.1.96.1.1.255", UoM= "' '",  objectName="Meter type",         HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 3, D=7, E=  0, F=255, obis= "1.1.3.7.0.255",  UoM= "var",  objectName="Q12",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 4, D=7, E=  0, F=255, obis= "1.1.4.7.0.255",  UoM= "var",  objectName="Q34",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 3, D=8, E=  0, F=255, obis= "1.1.3.8.0.255",  UoM= "varh", objectName="R12",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 4, D=8, E=  0, F=255, obis= "1.1.4.8.0.255",  UoM= "varh", objectName="R34",                HAN_Vendor ="Kamstrup"}
        };


        public void showObisValues()
        {
            for (int i = 0; i < legalObisCodes.Count; i++)
                    Console.WriteLine("Struct obis={0:x2}.{1:x2}.{2:x2}.{3:x2}.{4:x2}.{5:x2} ({6,-15}), UoM={7,-5}, objectName={8,-18}, vendor={9}", 
                    legalObisCodes[i].A, 
                    legalObisCodes[i].B, 
                    legalObisCodes[i].C, 
                    legalObisCodes[i].D, 
                    legalObisCodes[i].E, 
                    legalObisCodes[i].F, 
                    legalObisCodes[i].obis, 
                    legalObisCodes[i].UoM,
                    legalObisCodes[i].objectName,
                    legalObisCodes[i].HAN_Vendor);
        }

        public string showObis(int index)
        {
            if ( index >= 0 && index < legalObisCodes.Count)
                return legalObisCodes[index].obis;
            return "OBIS code " + index + " not found";

        }

        public int isObisFound(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            for (int i = 0; i < legalObisCodes.Count; i++)
            {
                if (legalObisCodes[i].A == a && 
                    legalObisCodes[i].B == b && 
                    legalObisCodes[i].C == c && 
                    legalObisCodes[i].D == d && 
                    legalObisCodes[i].E == e && 
                    legalObisCodes[i].F == f)
                    return i;
            }
            return -1;
        }

        public int isObisFound(byte[] data, int start )
        {
                return isObisFound(data[start + 0],data[start + 1],data[start + 2],data[start + 3],data[start + 4],data[start + 5]);
        }

        public string oBISCode(byte[] data, int start)
        {
            string cosem = "";
            int obisLength = 6;
            cosem = data[start + 0].ToString("X2") + "." + 
                    data[start + 1].ToString("X2") + "." +
                    data[start + 2].ToString("X2") + "." +
                    data[start + 3].ToString("X2") + "." +
                    data[start + 4].ToString("X2") + "." +
                    data[start + 5].ToString("X2");
                Console.WriteLine("{0}", cosem);
            return cosem;
        }

        public string UoMObisCode(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            int location = isObisFound(a, b, c, d, e, f);
            if (location > -1)
                return legalObisCodes[location].UoM;
            return "Error in UoM";
        }

        public void oBISBlock( byte[] data, bool logOBIS ) // COSEM block
        {
            // expected full OBIS block from DLMS/COSEM data block
            // Sample expected input:
            // DLMS: 7e a0 2a 41 08 83 13 04 13 e6 e7 00 0f 40 00 00 00 00 01 01 02 03 09 06 01 00 01 07 00 ff 06 00 00 04 51 02 02 0f 00 16 1b 6a dc 7e
            // COSEM:   A0 2A 41 08 83 13 04 13 
            //                                  E6 E7 00 0F 40 00 00 00 00 01 01 02 03 09 06 01 00 01 07 00 FF 06 00 00 04 51 02 02 0F 00 16 1B
            // OBIS:                                                       01 01 ! List with one element
            //                                                                   02 03 
            //                                                                         09 06 
            //                                                                               01 00 01 07 00 FF 
            //                                                                                                 06 00000451 0202 0F00 161B
            string oBISString;
            int oBISIndex;

            if (logOBIS)
            {
                Console.WriteLine("Obis block length=:{0}",data.Length);
                foreach(byte b in data) Console.Write("{0:X2} ",b);
                Console.WriteLine();
            }

            // establish a loop trough all OBIS values
            // 01 <- 
            //    01 <- 01 element
            int numberOfObjects = data[1];
            int switchValue = data[3]; // first switch value
            int cOSEMIndex = 6; // Obis code index

            for ( int i = 0; i < numberOfObjects; i++ )
            {
                switch (data[switchValue])
                {
                    case(0x02):
                        // Struct av 2 elementer
                        Console.WriteLine("Struct value {0} ok.", 0x02);
                        oBISIndex = isObisFound( data, cOSEMIndex );
                        Console.WriteLine("COSEM Object = {0}", showObis(oBISIndex) );
                        switchValue += 11;
                        Console.WriteLine("switchValue= {0} og antall karakterer= {1}",switchValue,data[switchValue]);
                        for (int j=0; j<=data[switchValue]; j++) Console.Write("{0}",(char)data[switchValue+j]);                        
                        switchValue += data[switchValue] + 2;
                        Console.WriteLine("\nswitchValue= {0}",switchValue);                        
                        break;
                    case(0x03):
                        // Struct av 3 elementer
                        Console.WriteLine("Struct value {0} ok.", 0x03);
                        oBISIndex = isObisFound( data, cOSEMIndex );
                        Console.WriteLine("COSEM Object = {0}", showObis(oBISIndex) );                        
                        break;
                    default:
                        Console.WriteLine("Struct value {0} not known.", data[switchValue]);
                        break;
                }
            }

            oBISString = oBISCode( data , 6 );

            oBISIndex = isObisFound( data, 6 );

            if( oBISIndex == 0 )
            {
                Console.WriteLine("Data for {0}:",legalObisCodes[oBISIndex].obis);
                for( int i = 12; i < data.Length; i++ ) Console.Write("{0:x2} ", data[i]);
                Console.WriteLine();
                ulong power = (ulong) (data[15]<<8) + data[16];
                Console.WriteLine("Power consumption bytes: {0:X2} {1:X2}, value = {2} {3}",data[15],data[16],power,legalObisCodes[oBISIndex].UoM);
            }

            Console.WriteLine("Obis code found is {0} with index {1} (with value={2})",oBISString,oBISIndex,legalObisCodes[oBISIndex].obis);

        }


    }
}
