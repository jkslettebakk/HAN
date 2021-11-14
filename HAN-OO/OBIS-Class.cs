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
        // OBIS types
        public const byte TYPE_STRING = 0x0a;
        public const byte TYPE_UINT32 = 0x06;
        public const byte TYPE_INT16 = 0x10;
        public const byte TYPE_OCTETS = 0x09;
        public const byte TYPE_UINT16 = 0x12;
        public const byte TYPE_TEXT   = 0x02;
        public const byte TYPE_DATA   = 0x03;
        public const int oBISLength = 6;


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
            public string obisType { get; set; }
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
            new obisCodesStruct {A=1, B=0, C=1,  D=7, E=0,   F=255, obis= "1.0.1.7.0.255",  bytes=11, UoM= "W",    objectName="Q1+Q4",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=1, C= 0, D=2, E=129, F=255, obis= "1.1.0.2.129.255",bytes=0, UoM= "' '",  objectName="Version identifier", HAN_Vendor ="Aidon & Kamstrup"},
            new obisCodesStruct {A=0, B=0, C=96, D=1, E=  0, F=255, obis= "0.0.96.1.0.255", bytes=0, UoM= "' '",  objectName="Model ID",           HAN_Vendor ="Aidon"}, 
            new obisCodesStruct {A=0, B=0, C=96, D=1, E=  7, F=255, obis= "0.0.96.1.7.255", bytes=0, UoM= "' '",  objectName="Model type",         HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 2, D=7, E=  0, F=255, obis= "1.0.2.7.0.255",  bytes=0, UoM= "W",    objectName="Q2+Q3",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 3, D=7, E=  0, F=255, obis= "1.0.3.7.0.255",  bytes=0, UoM= "kVAr", objectName="Q1+Q2",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 4, D=7, E=  0, F=255, obis= "1.0.4.7.0.255",  bytes=0, UoM= "kVAr", objectName="Q3+Q4",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=31, D=7, E=  0, F=255, obis= "1.0.31.7.0.255", bytes=0, UoM= "A",    objectName="IL1",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=51, D=7, E=  0, F=255, obis= "1.0.51.7.0.255", bytes=0, UoM= "A",    objectName="IL2",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=71, D=7, E=  0, F=255, obis= "1.0.71.7.0.255", bytes=0, UoM= "A",    objectName="IL3",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=32, D=7, E=  0, F=255, obis= "1.0.32.7.0.255", bytes=0, UoM= "V",    objectName="UL1",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=52, D=7, E=  0, F=255, obis= "1.0.52.7.0.255", bytes=0, UoM= "V",    objectName="UL2",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C=72, D=7, E=  0, F=255, obis= "1.0.72.7.0.255", bytes=0, UoM= "V",    objectName="UL3",                HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=0, B=0, C= 1, D=0, E=  0, F=255, obis= "0.0.1.0.0.255",  bytes=0, UoM= "' '",  objectName="Clock and Date",     HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 1, D=8, E=  0, F=255, obis= "1.0.1.8.0.255",  bytes=0, UoM= "Wh",   objectName="Q1+Q4",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 2, D=8, E=  0, F=255, obis= "1.0.2.8.0.255",  bytes=0, UoM= "Wh",   objectName="Q2+Q3",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 3, D=8, E=  0, F=255, obis= "1.0.3.8.0.255",  bytes=0, UoM= "kVArh",objectName="Q1+Q2",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=0, C= 4, D=8, E=  0, F=255, obis= "1.0.4.8.0.255",  bytes=0, UoM= "kVArh",objectName="Q3+Q4",              HAN_Vendor ="Aidon"},
            new obisCodesStruct {A=1, B=1, C= 0, D=0, E=  5, F=255, obis= "1.1.0.0.5.255",  bytes=0, UoM= "' '",  objectName="GS1 number",         HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 1, D=7, E=  0, F=255, obis= "1.1.1.7.0.255",  bytes=0, UoM= "W",    objectName="P14",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 2, D=7, E=  0, F=255, obis= "1.1.2.7.0.255",  bytes=0, UoM= "W",    objectName="P23",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=31, D=7, E=  0, F=255, obis= "1.1.31.7.0.255", bytes=0, UoM= "A",    objectName="IL1",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=51, D=7, E=  0, F=255, obis= "1.1.51.7.0.255", bytes=0, UoM= "A",    objectName="IL2",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=71, D=7, E=  0, F=255, obis= "1.1.71.7.0.255", bytes=0, UoM= "A",    objectName="IL3",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=32, D=7, E=  0, F=255, obis= "1.1.32.7.0.255", bytes=0, UoM= "V",    objectName="UL1",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=52, D=7, E=  0, F=255, obis= "1.1.52.7.0.255", bytes=0, UoM= "V",    objectName="UL2",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=72, D=7, E=  0, F=255, obis= "1.1.72.7.0.255", bytes=0, UoM= "V",    objectName="UL3",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=0, B=1, C= 1, D=0, E=  0, F=255, obis= "0.1.1.0.0.255",  bytes=0, UoM= "' '",  objectName="RTC",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 1, D=8, E=  0, F=255, obis= "1.1.1.8.0.255",  bytes=0, UoM= "Wh",   objectName="A14",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 2, D=8, E=  0, F=255, obis= "1.1.2.8.0.255",  bytes=0, UoM= "Wh",   objectName="A23",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C=96, D=1, E=  1, F=255, obis= "1.1.96.1.1.255", bytes=0, UoM= "' '",  objectName="Meter type",         HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 3, D=7, E=  0, F=255, obis= "1.1.3.7.0.255",  bytes=0, UoM= "var",  objectName="Q12",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 4, D=7, E=  0, F=255, obis= "1.1.4.7.0.255",  bytes=0, UoM= "var",  objectName="Q34",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 3, D=8, E=  0, F=255, obis= "1.1.3.8.0.255",  bytes=0, UoM= "varh", objectName="R12",                HAN_Vendor ="Kamstrup"},
            new obisCodesStruct {A=1, B=1, C= 4, D=8, E=  0, F=255, obis= "1.1.4.8.0.255",  bytes=0, UoM= "varh", objectName="R34",                HAN_Vendor ="Kamstrup"}
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
            // COSEM - 01 0C 02 02 09 06 01 01 00 02 81 FF 0A 0B 41 49 44 4F 4E 5F 56 30 30 30 31 02 02 09 06.......
            // cOSEMIndex -  01 02 03 04 05 06 07 08 08 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27.......
            // cOSEMIndex -     02      +03 = 05              12 + 11 + 2 = 25                       25
            // COSEM - 01 01 02 03 09 06 01 00 01 07 00 FF 06 00 00 09 7A 02 02 0F 00 16 1B
            // cOSEM data -  01 02 03 04 05 06 07 08 08 10 11 12 13 14 15 16 17 18 19 20 21
            // cOSEMIndex -     02                                           17
            // 01 <- 
            //    01 <- 01 element
            int numberOfObjects = data[1];
            int cOSEMIndex = 02;   // keep track of KEY position in OBIS buffer, start in position 2 (first Struct)
            int structType = data[cOSEMIndex + 1]; // first Struct location for the switch() function 
            int legalObisCodesIndex;

            for ( int i = 0; i < numberOfObjects; i++ ) // Array of objects (number of objects)
            { 
                legalObisCodesIndex = -1;
                switch (structType)  // switch on type of struct element (text (0x02) or data (0x03))
                {
                    case(TYPE_TEXT): // Struct of 0x02 elements (text fields), text; = 10 bytes + 1 + text.length = 11 + text.length
                        cOSEMIndex += 3;  //
                        if(logOBIS) Console.WriteLine("Struct value {0:x2} ok. with structType = {1:x2}. cOSEMIndex = {2}.",TYPE_TEXT,structType,cOSEMIndex);
                        legalObisCodesIndex = isObisFound( data, cOSEMIndex ); // Obis code start at cOSEMIndex
                        if(logOBIS && legalObisCodesIndex > -1 ) Console.WriteLine("COSEM Object = {0}", showObis(legalObisCodesIndex) );
                        // text block to be prosessed
                        for (int j=0; j<=data[cOSEMIndex]; j++) Console.Write("{0}",Convert.ToChar(data[cOSEMIndex+j]));
                        // Prepare for next Struc block
                        cOSEMIndex += TYPE_OCTETS + 1;
                        cOSEMIndex += data[cOSEMIndex] +1; // Position for next OBIS block
                        structType = data[cOSEMIndex];
                        if(logOBIS) Console.WriteLine("\nNext structType= {0}",structType);                        
                        break;
                    case(TYPE_DATA): // Struct av 3 elementer (data fields), data; = 10 bytes + 11 bytes data (21)
                        cOSEMIndex += 4;  //
                        if(logOBIS) Console.WriteLine("Struct value {0:x2} ok. with structType = {1:x2}. cOSEMIndex = {2}.",TYPE_TEXT,structType,cOSEMIndex);
                        legalObisCodesIndex = isObisFound( data, cOSEMIndex );  // Obis code starts at cOSEMIndex
                        if(logOBIS && legalObisCodesIndex > -1 ) Console.WriteLine("COSEM Object = {0}", showObis(legalObisCodesIndex) );
                        // Data block to be prosessed
                        cOSEMIndex += TYPE_OCTETS - 2;
                        structType = cOSEMIndex;
                        Console.WriteLine("Start = {0}, lengde = {1}",structType,structType + 9);
                        for ( int j=0; j< structType + 9; j++) Console.Write("{0:x2} ",data[structType+j]);
                        structType += 2;                        
                        break;
                    default:
                        Console.WriteLine("Struct value {0} not known.", data[structType]);
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
