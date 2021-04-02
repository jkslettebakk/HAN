#undef DEBUG
#undef DLSMDEBUG
#undef COSEMDEBUG
#define JSONDEBUG
#define COSEMSTRUCTURE

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Slettebakk_DLMS
{
    class DLMS
    {
        public List<byte> DLMSCOSEMlist = new List<byte>();
        public byte frameFormatType { set; get; } // DLMS frame type 3 (iFrame) expected (0b1010/0xA)
        public byte frameSegmentBit { set; get; }
        public int dLMSframeLength { set; get; }  // DLMS datablock length without 0x7E start and stop flag
        public static byte DLMSflag = 0x7E; // start flag indicator for DLMS frames (inclusive electric meeters)
        public static byte frameFormatTypeMask = 0b11110000;
        public static byte frameSegmentBitMask = 0b00001000;
        public byte iFrame = 0b1010;   // value "ten" (10,0xA)
        public byte sFrame = 0b0001;   // value "one" (1)
        public byte uFrame = 0b0011;    // value "tre" (3)
        public byte controlFlag;
        public byte[] hcs = new byte[2];
        public uint dLMSfcs;
        public uint dLMSfcsInvert;
        public uint dLMShcs;
        public byte dLMScontrol;
        public bool DLMSStartFlag = false;

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
        }

            //a obis struct list
            List<obisCodesStruct> legalObisCodes_Aidon = new List<obisCodesStruct>{
                new obisCodesStruct {A=1, B=0, C=1,  D=7, E=0,   F=255, obis= "1.0.1.7.0.255",  UoM= "kW"},
                new obisCodesStruct {A=1, B=1, C= 0, D=2, E=129, F=255, obis= "1.1.0.2.129.255",UoM= "(Vendor version)"},
                new obisCodesStruct {A=0, B=0, C=96, D=1, E=  0, F=255, obis= "0.0.96.1.0.255", UoM= "(Model serial number)"},
                new obisCodesStruct {A=0, B=0, C=96, D=1, E=  7, F=255, obis= "0.0.96.1.7.255", UoM= "(Model type)"},
                new obisCodesStruct {A=1, B=0, C= 2, D=7, E=  0, F=255, obis= "1.0.2.7.0.255",  UoM= "kW"},
                new obisCodesStruct {A=1, B=0, C= 3, D=7, E=  0, F=255, obis= "1.0.3.7.0.255",  UoM= "kVAr"},
                new obisCodesStruct {A=1, B=0, C= 4, D=7, E=  0, F=255, obis= "1.0.4.7.0.255",  UoM= "kVAr"},
                new obisCodesStruct {A=1, B=0, C=31, D=7, E=  0, F=255, obis= "1.0.31.7.0.255", UoM= "A"},
                new obisCodesStruct {A=1, B=0, C=51, D=7, E=  0, F=255, obis= "1.0.51.7.0.255", UoM=  "A"},
                new obisCodesStruct {A=1, B=0, C=71, D=7, E=  0, F=255, obis= "1.0.71.7.0.255", UoM=  "A"},
                new obisCodesStruct {A=1, B=0, C=32, D=7, E=  0, F=255, obis= "1.0.32.7.0.255", UoM= "V"},
                new obisCodesStruct {A=1, B=0, C=52, D=7, E=  0, F=255, obis= "1.0.52.7.0.255", UoM= "V"},
                new obisCodesStruct {A=1, B=0, C=72, D=7, E=  0, F=255, obis= "1.0.72.7.0.255", UoM= "V"},
                new obisCodesStruct {A=0, B=0, C= 1, D=0, E=  0, F=255, obis= "0.0.1.0.0.255",  UoM= ""},
                new obisCodesStruct {A=1, B=0, C= 1, D=8, E=  0, F=255, obis= "1.0.1.8.0.255",  UoM= "kWh"},
                new obisCodesStruct {A=1, B=0, C= 2, D=8, E=  0, F=255, obis= "1.0.2.8.0.255",  UoM= "kWh"},
                new obisCodesStruct {A=1, B=0, C= 3, D=8, E=  0, F=255, obis= "1.0.3.8.0.255",  UoM= "kVArh"},
                new obisCodesStruct {A=1, B=0, C= 4, D=8, E=  0, F=255, obis= "1.0.4.8.0.255",  UoM= "kVArh"}
            };

            public void showObisValues()
            {
                for ( int i=0; i < legalObisCodes_Aidon.Count; i++  ) Console.WriteLine("Struct obis={0}.{1}.{2}.{3}.{4}.{5} {6}",legalObisCodes_Aidon[i].A,legalObisCodes_Aidon[i].B,legalObisCodes_Aidon[i].C,legalObisCodes_Aidon[i].D,legalObisCodes_Aidon[i].E,legalObisCodes_Aidon[i].F,legalObisCodes_Aidon[i].UoM);
            }

            public int isObisFound(byte a, byte b, byte c, byte d, byte e, byte f)
            {
                for ( int i=0; i < legalObisCodes_Aidon.Count; i++ )
                {
                    if ( legalObisCodes_Aidon[i].A == a && legalObisCodes_Aidon[i].B == b && legalObisCodes_Aidon[i].C == c && legalObisCodes_Aidon[i].D == d && legalObisCodes_Aidon[i].E == e && legalObisCodes_Aidon[i].F == f)
                    return i;
                }
                return -1;
            }

            public string UoMObisCode(byte a, byte b, byte c, byte d, byte e, byte f)
            {
                int location = isObisFound(a, b, c, d, e, f);
                if (location > -1 )
                    return legalObisCodes_Aidon[location].UoM;
                return "Error in UoM";
            }
            
            public void analyseObisData(List<byte> obisList, int start)
            {

            }
    }

        class Crc16Class
        {
            private const ushort polynomial = 0x8408;
            private static ushort[] table = new ushort[256];

            static Crc16Class()
            {
                ushort value;
                ushort temp;
                for (ushort i = 0; i < table.Length; ++i)
                {
                    value = 0;
                    temp = i;
                    for (byte j = 0; j < 8; ++j)
                    {
                        if (((value ^ temp) & 0x0001) != 0)
                        {
                            value = (ushort)((value >> 1) ^ polynomial);
                        }
                        else
                        {
                            value >>= 1;
                        }
                        temp >>= 1;
                    }
                    table[i] = value;
                }
            }

            public ushort ComputeChecksum(List<byte> data)
            {
                return ComputeChecksum(data, 0, data.Count);
            }

            public ushort ComputeChecksum(List<byte> data, int start, int length)
            {
                ushort fcs = 0xffff;
                try
                {
                    for (int i = start; i < (start + length); i++)
                    {
                        var index = (fcs ^ data[i]) & 0xff;
                        fcs = (ushort)((fcs >> 8) ^ table[index]);
                    }
                    fcs ^= 0xffff;
                    return fcs;
                }
                catch
                {
                    Console.WriteLine("Error in ComputeChecksum");
                    return 0;
                }
            }
        }

        class COSEMClass
        {
            List<byte> cOSEMHeadingData = new List<byte>();
            List<byte> cOSEMData = new List<byte>();
            int cosemHeadingDataLength = 9;
            int cosemDataLength;
            int dLMSstartCosemBlock = 9;
            byte[] LLC = new byte[3]; // LLC (Logic Link Control) frame header, E6 E7 00
            byte[] UIFrameHeade = new byte[6]; // (00 00 00 00)
            public byte[] cOSEMtypeobject = new byte[2]; // Object or Array
            public string tabLength = "\t";
            obisCodesClass obisCode = new obisCodesClass();

            public void cOSEMInitialize()
            {
                cOSEMtypeobject[0] = 0x02; // initialize to two
                cOSEMtypeobject[1] = 0x00; // initiated to zero
                #if DEBUG
                    Console.WriteLine("cOSEMInitialize initialised");
                #endif
            }

            void cOSEMHeading()
            {
                LLC[0] = cOSEMHeadingData[0]; // E6 (Aidon values? General meetering values?)
                LLC[1] = cOSEMHeadingData[1]; // E7
                LLC[2] = cOSEMHeadingData[2]; // 00
                UIFrameHeade[0] = cOSEMHeadingData[3]; // 0F
                UIFrameHeade[1] = cOSEMHeadingData[4]; // 40
                UIFrameHeade[2] = cOSEMHeadingData[5]; // 00
                UIFrameHeade[3] = cOSEMHeadingData[6]; // 00
                UIFrameHeade[4] = cOSEMHeadingData[7]; // 00
                UIFrameHeade[5] = cOSEMHeadingData[8]; // 00
            }


            string cOSEMObisCode( List<byte> data, int start, int length )
            {
                string jsonString = ",\n\'obis code\' : \'";
                #if DEBUG
                    Console.WriteLine("cOSEMObisCode input: start={0},length={1}",start,length);
                    for ( int k = start; k < (start + length + 1); k++)  Console.Write("{0:X2}.",cOSEMData[k]);
                    Console.WriteLine();
                    Console.Write("Obis Code: ");
                #endif

                for ( int k = start; k < (start + length); k++) 
                    {
                        jsonString +=  (int) (data[k]) + ".";
                        #if DEBUG
                            Console.Write("{0:X2}.",cOSEMData[k]);
                        #endif
                    }
                    jsonString += (int) (data[(start + length + 1)]) + "\'";
                #if DEBUG
                    Console.WriteLine("{0:X2}.",cOSEMData[(start + length + 1)]);
                    Console.WriteLine($"Obis found? Return = {obisCode.isObisFound(data[start+0],data[start+1],data[start+2],data[start+3],data[start+4],data[start+5])}");
                #endif
                jsonString += ",\n";
                return jsonString;
            }

            public void cOSEMDataBlock(List<byte> dLMScOSEMData)
            {
                cOSEMHeadingData.Clear(); // Prepare new COSEM data heading
                cOSEMData.Clear(); // Prepare new COSEM data
                for (int i = dLMSstartCosemBlock; i < dLMSstartCosemBlock + cosemHeadingDataLength; i++)  // extract COSEM heading from DLMS block
                    cOSEMHeadingData.Add(dLMScOSEMData[i]);  // get COSEMClass data extracted out of DLMS data block
                for (int i = dLMSstartCosemBlock + cosemHeadingDataLength; i < dLMScOSEMData.Count - 3; i++)  // extract COSEM block from DLMS block
                    cOSEMData.Add(dLMScOSEMData[i]);  // get COSEMClass data extracted out of DLMS data block
                // cosemHeadingDataLength = cOSEMHeadingData.Count;
                cosemDataLength = cOSEMData.Count;
                cOSEMHeading();

                if ( cOSEMData[0] == 0x01 ) // Array of x (cOSEMData[1]) elements (lines)
                {
                    #if DEBUG || COSEMSTRUCTURE
                        Console.WriteLine("{0}Debug: New array ({1:X2}) of {2:X2} array objects",tabLength,cOSEMData[0],cOSEMData[1]);
                        for (int i = 2; i < cosemDataLength; i++) // for debug purposes
                            {
                                Console.Write("{0:X2} ", cOSEMData[i]);
                            }
                            Console.WriteLine();
                    #endif
                    int cOSEMDataLocation = 1; // Prepare handling of COSEM data block from Meeter

                    string structJson = "";

                    for ( int i = 0; i < cOSEMData[cOSEMDataLocation]; i++ ) // Loop trough all Array objects (normally 1 or 12 for Aidon; 01 0C)
                    {

                        cOSEMDataLocation++; // Increase counters to start on "sctructure" elements

                        string currentCOSEMString ="";

                        switch (cOSEMData[cOSEMDataLocation]) // switch on Structure (0x02) or .... (0x03) and prepare for length (++)
                        {
                            case 0x02: // Structure with 2 elements (COSEM Code + data Text about the Mettering system)
                                if (cOSEMDataLocation < cosemDataLength) cOSEMDataLocation++;
                                Console.WriteLine("In 0x02 Structure. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                    /* Sample structure 0x02
                                    01 0C // 01 12 = 01=Array of 12 elements
                                          0202 // Structure of 02 elements
                                               0906 // Octet of 0x06 elements
                                                    01 01 00 02 81 FF // OBIS list version identifyer 
                                               0A0B // Visible string mark (0x0A) of 0x0B elements
                                                    41 49 44 4F 4E 5F 56 30 30 30 31 // "A I D O N _ V 0 0 0 1"
                                          0202 // Struct (02) of 02 elements
                                               0906 // Octet (0x09) of 0x06 elements
                                                    00 00 60 01 00 FF // Metering point ID
                                               0A10 // Visible string (= 10) of 0x10 (16) elements
                                                    37 33 35 39 39 39 32 39 30 35 34 37 38 33 36 32 // "7 3 5 9 ......"
                                          ........
                                    */
                                int k;
                                int innerLoopElements = cOSEMData[cOSEMDataLocation];
                                for ( int j = 0; j < innerLoopElements; j++)  // Start loop and increment to first element
                                {
                                    if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++; // increase counter to start on Structure elements

                                    switch (cOSEMData[cOSEMDataLocation])
                                    {
                                        case 0x09: // octet string, OBIS code (e.g 01.00.01.07.00.FF = Active Power Q1+Q4)
                                            if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++;
                                            Console.WriteLine("In 0x09 octet string. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                            // Call obis code analyse
                                            structJson += "\n\'obis code\' : \'";
                                            #if DEBUG
                                                Console.Write("Obis Code: ");
                                            #endif
                                            #if COSEMSTRUCTURE
                                                Console.Write("\t\t{0:X2} {1:X2}",cOSEMData[cOSEMDataLocation-1],cOSEMData[cOSEMDataLocation]);
                                            #endif
                                            obisCode.analyseObisData(cOSEMData, cOSEMDataLocation);
                                            for ( k = (cOSEMDataLocation +1); k < (cOSEMDataLocation + cOSEMData[cOSEMDataLocation]); k++) // normally 6 characters
                                                {
                                                    structJson += (int) (cOSEMData[k]) + ".";
                                                    currentCOSEMString += (int) (cOSEMData[k]) + ".";
                                                    #if DEBUG || COSEMSTRUCTURE
                                                        if ( k == (cOSEMDataLocation +1) ) Console.Write("\t");
                                                        Console.Write("{0}.",cOSEMData[k]);
                                                    #endif
                                                }
                                                structJson += (int) (cOSEMData[cOSEMDataLocation + cOSEMData[cOSEMDataLocation]]) + "\'";
                                                currentCOSEMString += (int) (cOSEMData[cOSEMDataLocation + cOSEMData[cOSEMDataLocation]]);
                                                #if DEBUG || COSEMSTRUCTURE
                                                    Console.WriteLine("{0} {1}",cOSEMData[cOSEMDataLocation+cOSEMData[cOSEMDataLocation]],obisCode.UoMObisCode(cOSEMData[cOSEMDataLocation +1],cOSEMData[cOSEMDataLocation +2],cOSEMData[cOSEMDataLocation +3],cOSEMData[cOSEMDataLocation +4],cOSEMData[cOSEMDataLocation +5],cOSEMData[cOSEMDataLocation +6]));
                                                #endif
                                            #if DEBUG
                                                Console.WriteLine();
                                            #endif
                                            #if DEBUG
                                                Console.WriteLine("cOSEMDataLocation inn = {0}, cOSEMData[{0}]={1:X2}",cOSEMDataLocation,cOSEMData[cOSEMDataLocation]);
                                            #endif
                                            cOSEMDataLocation += cOSEMData[cOSEMDataLocation];
                                            #if DEBUG
                                                Console.WriteLine("cOSEMDataLocation ut = {0}, cOSEMData[{0}]={1:X2}",cOSEMDataLocation,cOSEMData[cOSEMDataLocation]);
                                            #endif
                                            // structJson += cOSEMObisCode(cOSEMData, cOSEMDataLocation+1, 5);
                                            Console.WriteLine("0x09 structJson=\n{0}",structJson);
                                            break;  
                                        case 0x0A: // Visible string
                                            if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++;
                                            Console.WriteLine("In 0x0A visible text. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                            string localText = "";
                                            #if DEBUG
                                                Console.Write("Visible string: ");
                                            #endif
                                            #if COSEMSTRUCTURE
                                                Console.Write("\t\t{0:X2} {1:X2}",cOSEMData[cOSEMDataLocation-1],cOSEMData[cOSEMDataLocation]);
                                            #endif
                                            for (k = (cOSEMDataLocation + 1); k < (cOSEMDataLocation + 1) + (cOSEMData[cOSEMDataLocation]); k++) 
                                                {
                                                    localText += (char) cOSEMData[k];
                                                    #if DEBUG || COSEMSTRUCTURE
                                                        if ( k == (cOSEMDataLocation +1) ) Console.Write("\t");
                                                        Console.Write("{0}",(char) cOSEMData[k]);
                                                    #endif
                                                }
                                            #if DEBUG || COSEMSTRUCTURE
                                                Console.WriteLine();
                                            #endif
                                            // structJson += "\'\n}";
                                            cOSEMDataLocation += cOSEMData[cOSEMDataLocation];
                                            Console.WriteLine("COSEM Code = {0}",currentCOSEMString);
                                                switch(currentCOSEMString)
                                                {
                                                    case "1.1.0.2.129.255":
                                                        structJson += ",\n\'List version identifier\' : \'" + localText + "\'";
                                                        break;
                                                    case "0.0.96.1.0.255":
                                                        structJson += ",\n\'Model serial number\' : \'" + localText + "\'";
                                                        break;
                                                    case "0.0.96.1.7.255":
                                                        structJson += ",\n\'Model type\' : \'" + localText + "\'";
                                                        break;
                                                    default:
                                                        Console.WriteLine("Obis code not implemented? - {0}",localText);
                                                        break;
                                                }
                                                Console.WriteLine("debug: {0}",structJson);
                                            break;
                                        default:
                                            switch (cOSEMData[cOSEMDataLocation])
                                                {
                                                    case 0x06:
                                                        Console.WriteLine("kW message and data");
                                                        Console.WriteLine("In 0x06 cosem code. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                                        //normal structur 0x06="double-long-unsigned" (32bit=4 Bytes)
                                                        // 00 00 06 13 = 1555 kW
                                                        // then
                                                        // 0x02 (Structure with two elements) 
                                                        // 0x02
                                                        // 0f = Int8
                                                        // 00 : 0
                                                        // 16 : enum
                                                        // 1b : Watt active power
                                                        int power = (int) ( ((cOSEMData[cOSEMDataLocation])<<24) | ((cOSEMData[cOSEMDataLocation+1])<<16) | ((cOSEMData[cOSEMDataLocation+2])<<8) | (cOSEMData[cOSEMDataLocation+3]));
                                                        cOSEMDataLocation += 4;
                                                        Console.WriteLine("Active Power Q4&Q1={0}\n",(int) power);
                                                        structJson += ",\n\'Power Q4&Q1\' : \'" + (int) power + "\',\n\'UoM\' : \'kW\'";
                                                        Console.WriteLine("0x06 structJson=\n{0}",structJson);
                                                        cOSEMDataLocation += 6;
                                                        break;
                                                    case 0x10:
                                                        Console.WriteLine("0x10 - Not implemented yet.");
                                                        Console.WriteLine("In 0x10 - not handled yet. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                                        for (int l = cOSEMDataLocation; l < (cOSEMDataLocation + 5); l++ ) Console.Write("{0:X2}",cOSEMData[cOSEMDataLocation]);
                                                        cOSEMDataLocation += 5;
                                                        Console.WriteLine();
                                                        break;
                                                    case 0x12:
                                                        Console.WriteLine("0x12 - Not implemented yet.");
                                                        Console.WriteLine("In 0x10 - not handled yet. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                                        for (int l = cOSEMDataLocation; l < (cOSEMDataLocation + 5); l++ ) Console.Write("{0:X2}",cOSEMData[cOSEMDataLocation]);
                                                        cOSEMDataLocation += 5;
                                                        Console.WriteLine();
                                                        break;
                                                    case 0x16:
                                                        Console.WriteLine("0x16 - Not implemented yet.");
                                                        Console.WriteLine("In 0x10 - not handled yet. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                                        for (int l = cOSEMDataLocation; l < (cOSEMDataLocation + 2); l++ ) Console.Write("{0:X2}",cOSEMData[cOSEMDataLocation]);
                                                        cOSEMDataLocation += 1;
                                                        Console.WriteLine();
                                                        break;
                                                    default:
                                                        Console.WriteLine("Inner 0x06,0x10, 0x12 and 0x16 covered. Value = {0:X2} in location {1} not handled yet.",cOSEMData[cOSEMDataLocation-1],(cOSEMDataLocation-1));
                                                        break;
                                                }
                                                Console.WriteLine("Outer 0x09 and 0x0A loop: Inner 0x06,0x10, 0x12 and 0x16 covered. Value = {0:X2} in location {1} not handled yet",cOSEMData[cOSEMDataLocation-1],(cOSEMDataLocation-1));
                                            break;
                                    }
                                    // structJson += "\n}";
                                    #if COSEMDEBUG
                                        Console.WriteLine("{0}",structJson);
                                    #endif
                                };
                                #if DEBUG || COSEMDEBUG
                                    Console.WriteLine("Structure 0x02 done. Result:\n{0}",structJson);
                                #endif
                                break;
                            case 0x03: // Structure with Metering data ()
                                if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++;
                                Console.WriteLine("In 0x03. Structure with meetering data. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                #if COSEMSTRUCTURE
                                    Console.WriteLine("\t{0:X2} {1:X2}",cOSEMData[cOSEMDataLocation-1],cOSEMData[cOSEMDataLocation]);
                                #endif
                                #if DEBUG
                                    Console.WriteLine("Prepare Meetering Data Loop");
                                #endif
                                innerLoopElements = cOSEMData[cOSEMDataLocation];
                                for ( int j = 0; j < innerLoopElements; j++)
                                {
                                    if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++;
                                    #if DEBUG
                                        Console.WriteLine("In metering data loop. j = {0}, Location is cOSEMDataLocation={1}, Octet or Power values = {2:X2}",
                                            j, cOSEMDataLocation,cOSEMData[cOSEMDataLocation]);
                                    #endif
                                    switch (cOSEMData[cOSEMDataLocation])
                                    {
                                        case 0x09: // octet string, OBIS code (e.g 01.00.01.07.00.FF = Active Power Q1+Q4)
                                            if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++;
                                            Console.WriteLine("In 0x9 - octet string. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                            #if DEBUG
                                                Console.Write("Obis Code: ");
                                            #endif
                                            structJson += ",\n\'Obis Code\' : \'";
                                            #if COSEMSTRUCTURE
                                                Console.Write("\t\t{0:X2} {1:X2}",cOSEMData[cOSEMDataLocation-1],cOSEMData[cOSEMDataLocation]);
                                            #endif
                                            for ( k = (cOSEMDataLocation +1); k < (cOSEMDataLocation + 7); k++) 
                                            {
                                                structJson += (char) cOSEMData[k] + ".";
                                                #if DEBUG || COSEMSTRUCTURE
                                                Console.Write("{0}",(char) cOSEMData[k]);
                                                #endif
                                            }
                                            #if DEBUG
                                            Console.WriteLine();
                                            #endif
                                            // cOSEMDataLocation += k - 1;
                                            structJson += "\'";
                                            cOSEMDataLocation += 6;
                                            #if DEBUG
                                                Console.WriteLine("Next element 0x06? {0:X2}",cOSEMData[cOSEMDataLocation]);
                                            #endif
                                            break;  
                                        case 0x06: // Metering data power
                                            if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++;
                                            Console.WriteLine("In 0x06 - meetering data power. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                            #if DEBUG
                                                Console.Write("Data type unsigned32 : ");
                                            #endif
                                            int unsignedLength = 10;
                                            #if COSEMSTRUCTURE
                                                Console.Write("\t\t{0:X2} {1:X2}",cOSEMData[cOSEMDataLocation-1],cOSEMData[cOSEMDataLocation]);
                                            #endif
                                            structJson += ",\n\'Metering Power \' : \'";
                                            for (k = (cOSEMDataLocation + 1); k < (cOSEMDataLocation + 1) + unsignedLength; k++) 
                                            {
                                                structJson += cOSEMData[k];
                                                #if DEBUG
                                                Console.Write("{0:X2}", cOSEMData[k]);
                                                #endif
                                            }
                                            #if DEBUG
                                            Console.WriteLine();
                                            #endif
                                            structJson += "\'";
                                            cOSEMDataLocation += 6; // Last element used
                                            Console.WriteLine("Text loop done (k={0}). Next element={1:X2} and location is {2}",k,cOSEMData[cOSEMDataLocation],cOSEMDataLocation);
                                            break;
                                        case 0x10: // Meetering data current L1 & L3
                                            if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++;
                                            Console.WriteLine("In 0x10 - meetering data current L1&L3. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                            #if DEBUG
                                                Console.Write("Data type Current L1 : ");
                                            #endif
                                            unsignedLength = 10;
                                            structJson += ",\n\'Metering current \' : \'";
                                            for (k = (cOSEMDataLocation + 1); k < (cOSEMDataLocation + 1) + unsignedLength; k++) 
                                            {
                                                structJson += cOSEMData[k];
                                                #if DEBUG
                                                Console.Write("{0:X2}", cOSEMData[k]);
                                                #endif
                                            }
                                            #if DEBUG
                                                Console.WriteLine();
                                            #endif
                                            structJson += "\'";
                                            cOSEMDataLocation += 6; // Last element used
                                            // Console.WriteLine("Text loop done (k={0}). Current element={1:X2} and location is {2}",k,cOSEMData[cOSEMDataLocation],cOSEMDataLocation);
                                            break;
                                        case 0x12: // Meetering data Phase VL1 & VL2 & VL3
                                            if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++;
                                            Console.WriteLine("In 0x12 - meetering data phase VL1&VL3. cOSEMDataLocation = {0}",cOSEMDataLocation);
                                            #if DEBUG
                                                Console.Write("Data type Phase VL1 & VL2 & VL3 : ");
                                            #endif
                                            unsignedLength = 8;
                                            structJson += ",\n\'Metering Phase \' : \'";
                                            for (k = (cOSEMDataLocation + 1); k < (cOSEMDataLocation + 1) + unsignedLength; k++) 
                                            {
                                                structJson += cOSEMData[k];
                                                #if DEBUG
                                                    Console.Write("{0:X2}", cOSEMData[k]);
                                                #endif
                                            }
                                            #if DEBUG
                                                Console.WriteLine();
                                            #endif
                                            structJson += "\'";
                                            cOSEMDataLocation += 6; // Last element used
                                            // Console.WriteLine("Text loop done (k={0}). Current element={1:X2} and location is {2}",k,cOSEMData[cOSEMDataLocation],cOSEMDataLocation);
                                            break;
                                        default:
                                            if (cOSEMDataLocation < cosemDataLength ) cOSEMDataLocation++;
                                            Console.WriteLine("{0}Inner Switch. Value not handled yet. Value = {1:X2} in location {2}", tabLength, cOSEMData[cOSEMDataLocation],cOSEMDataLocation);
                                            break;
                                    };
                                    // structJson += "\n}";
                                }
                                #if DEBUG || COSEMDEBUG
                                    Console.WriteLine("Structure 0x03 done. Result:\n{0}",structJson);
                                #endif
                                break;
                            default:
                                Console.WriteLine("{0}Outer Switch. Value not handled yet. Value = {1:X2} in location {2}", tabLength,cOSEMData[cOSEMDataLocation],cOSEMDataLocation);
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Unknown type ({0:X2}) ***************** with ******************* {1:X2} elements",cOSEMData[9],cOSEMData[10]);
                }
            }

            public void cOSEMDebug(string leadingText, List<byte> byteList, bool eol )
            {
                Console.WriteLine(leadingText);
                foreach (byte b in byteList) Console.Write("{0:X2} ", b);
                if (eol) Console.WriteLine();
            }

        }

        public void readDLMSstreamFromHAN(SerialPort sp)
        {
            if (!sp.IsOpen) sp.Open();
            DLMSCOSEMlist.Clear(); // Prepare and start with empthy list
            int HANbufferLength;
            int bytesProcessed;
            COSEMClass cOSEM = new COSEMClass();
            cOSEM.cOSEMInitialize();

            try
            {   // now, loop forever reading and prosessing HAN port data

                while (true)
                {
                    HANbufferLength = sp.BytesToRead; // get number of available bytes on HAN device
                    bytesProcessed = 0;  // validate if we have processes all bytes in HAN buffer
                    byte[] byteBuffer = new byte[HANbufferLength]; // preapare HAN data buffer

                    for (int i = 0; i < HANbufferLength; i++) byteBuffer[i] = (byte)sp.ReadByte(); // Read all data in HAN device to byteBuffer

                    if (byteBuffer.Length != HANbufferLength)
                        Console.WriteLine(" ******************   ERROR reading HAN port   ****************");

                    // Console.WriteLine("\nBytes read from HAN meeter:{0}", HANbufferLength);
                    for (int i = 0; i < HANbufferLength; i++)
                    {
                        if (byteBuffer[i] == DLMSflag)
                        {
                            if (i + 1 < HANbufferLength)
                                if ((frameType(byteBuffer[i + 1]) == iFrame))
                                {
                                    // Console.Write("\nNew 0x7E & iFrame block!\n{0:X2} ", byteBuffer[i]);
                                    // Is old block prosessed?
                                    if (DLMSStartFlag)
                                    {
                                        DLMSCOSEMlist.Add(byteBuffer[i]); // add last 0x7E flag
                                        #if DEBUG
                                            Console.WriteLine("\n3) Complete block? Add 0x7E");
                                        #endif
                                        DLMSCOSEMCompleteBlock(DLMSCOSEMlist);
                                    }
                                    DLMSCOSEMlist.Add(byteBuffer[i]); // add first 0x7E flag 
                                    DLMSStartFlag = true;
                                }
                                else
                                {
                                    DLMSCOSEMlist.Add(byteBuffer[i]);
                                    // but do we have a start?
                                    if (DLMSStartFlag) // yes, full block
                                    {
                                        #if DEBUG
                                            // Console.WriteLine("{0:X2}\nComplete block. Process...", byteBuffer[i]);
                                            Console.WriteLine("\n2) Complete block...");
                                        #endif
                                        DLMSCOSEMCompleteBlock(DLMSCOSEMlist);
                                        DLMSStartFlag = false;
                                    }

                                }
                            else
                            {
                                DLMSCOSEMlist.Add(byteBuffer[i]);
                                if (DLMSStartFlag)
                                {
                                    #if DEBUG
                                        Console.WriteLine("\n1) Complete block. Process...");
                                    #endif
                                    DLMSCOSEMCompleteBlock(DLMSCOSEMlist);
                                    DLMSStartFlag = false;
                                }
                            }
                        }
                        else
                        {
                            DLMSCOSEMlist.Add(byteBuffer[i]);
                            // Console.Write("{0:X2} ", byteBuffer[i]);
                        }
                        bytesProcessed = i + 1;
                    }
                    // Console.WriteLine("");
                    // ready to start first DLMS block
                    // Console.WriteLine("\nBytes prosessed:{0}. All bytes={1}", bytesProcessed, bytesProcessed == HANbufferLength);
                    System.Threading.Thread.Sleep(1510); // Sleep 1.51s
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nAbnormal exit:\n{0}", ex);
                sp.Close();
                sp.Dispose();
                return;
            }
        }

        public bool frame(byte dlsmFrameByte1, byte dlsmFrameByte2)
        {
            if (dlsmFrameByte1 == 0 || dlsmFrameByte2 == 0) Console.WriteLine("Error in DLMS byte input.");
            frameFormatType = (byte)((dlsmFrameByte1 & frameFormatTypeMask) >> 4);
            if (frameFormatType == 0) Console.WriteLine("Error in xFrame calculation.");
            frameSegmentBit = (byte)(((dlsmFrameByte1) & frameSegmentBitMask) >> 3);
            dLMSframeLength = ((dlsmFrameByte1 & 0b00000111) << 8) | dlsmFrameByte2;
            #if DEBUG
                Console.WriteLine("frame. Byte1={0:x2}, byte2={1:x2} FrameFormatType={2},frameSegmentBit={3},dLMSframeLength={4}", frameFormatType, frameSegmentBit, dLMSframeLength);
            #endif
            if (frameFormatType == iFrame)
            {
                DLMSStartFlag = true;
                return true;
            }
            else
                return false;
        }
        public void dLMSframePropertyData(List<byte> dlmsDataBlock)
        {
            // Pull out the the DLMS header data and set the DLMS c# variable
            frameFormatType = (byte)((dlmsDataBlock[1] & frameFormatTypeMask) >> 4);
            frameSegmentBit = (byte)((dlmsDataBlock[1] & frameSegmentBitMask) >> 3);
            dLMSframeLength = ((dlmsDataBlock[1] & 0b00000111) << 8) | dlmsDataBlock[2];
            #if DEBUG
                Console.WriteLine("Frame heading data:\nFrameFormatType={0}, frameSegmentBit={1:X2},dLMSframeLength={2}", frameFormatType, frameSegmentBit, dLMSframeLength);
            #endif
        }
        public byte frameType(byte b)
        {
            return (byte)(((b) & frameFormatTypeMask) >> 4);
        }

        public int FrameLength(byte dlsmFrameByte1, byte dlsmFrameByte2)
        {
            frameSegmentBit = (byte)(((dlsmFrameByte1) & frameSegmentBitMask) >> 3);
            dLMSframeLength = ((dlsmFrameByte1 & 0b00000111) << 8) | dlsmFrameByte2;
            #if DEBUG
                Console.WriteLine("dlsmFrameByte1&0b00000111={0},dlsmFrameByte2={1},Frame length = {2}", ((dlsmFrameByte1 & 0b00000111) << 8), dlsmFrameByte2, dLMSframeLength);
            #endif
            return dLMSframeLength;
        }

        public void dLMSControlHscFcs(byte control, byte hcs1, byte hcs2, byte fcs1, byte fcs2) // Frame Check Sequence calculation
        {
            dLMSfcs = (ushort)((fcs1 << 8) + fcs2);
            dLMSfcsInvert = (ushort)((fcs2 << 8) + fcs1);
            dLMShcs = (ushort)((hcs1 << 8) + hcs2);
            dLMScontrol = control;
            #if DEBUG
                Console.WriteLine("control byte={0:X2}, dLMSfcs={1:X4}({1}) and dLMShcs={2:X4}({2}) in Frame Check Sequence block.", dLMScontrol, dLMSfcs, dLMShcs);
                Console.WriteLine("fcs inverted ={0:X4} ({0}))", dLMSfcsInvert);
            #endif
        }
        public bool hDLCinformation(byte[] information)
        {
            return true;
        }
        public bool hDLChCS(byte dlsmFrameByte1, byte dlsmFrameByte2) // Header Check Sequence
        {
            hcs[0] = dlsmFrameByte1;
            hcs[1] = dlsmFrameByte2;
            return true;
        }
        public bool HDLCcontroll(byte Controll)
        {
            controlFlag = Controll;
            return true;
        }
        public bool HDLCcosemInformation(string cosemData)
        {
            byte[] cosemBytes = Encoding.ASCII.GetBytes(cosemData);
            #if DEBUG
                Console.WriteLine("Cosem bytes ({0}) to analyse:", cosemBytes.Length);
                for (int i = 0; i < cosemBytes.Length; i++)
                    Console.Write("{0:X2} ", cosemBytes[i]);
                Console.WriteLine(" ");
            #endif
            return true;
        }
        public void DLMSCOSEMCompleteBlock(List<byte> dLMSCOSEMData)
        {
            COSEMClass cOSEM = new COSEMClass();
            Crc16Class crc = new Crc16Class();

            dLMSframePropertyData(dLMSCOSEMData);  // Update heading data
            dLMSControlHscFcs(dLMSCOSEMData[5], dLMSCOSEMData[6], dLMSCOSEMData[7], dLMSCOSEMData[dLMSCOSEMData.Count - 3], dLMSCOSEMData[dLMSCOSEMData.Count - 2]); // Calculate control, hcs and fcs

            if (dLMSfcsInvert == crc.ComputeChecksum(dLMSCOSEMData, 1, dLMSframeLength - 2)) // typical "7E........ A36E7E", dLMSfcsInvert=6EA3
            {

                #if DEBUG
                    Console.WriteLine("DLSMCOSEM datablock:\nList length={0}\nDLMS block length={1},\nValidity (List length and DLMS lengt match)={2}\nFull DLMS data result:", dLMSCOSEMData.Count, dLMSframeLength, (dLMSCOSEMData.Count - 2) == dLMSframeLength && dLMSCOSEMData[0] == dLMSCOSEMData[dLMSframeLength + 1]);
                    dLMSCOSEMDebug("Frame Check ok! DLMS and COSEM block complete",dLMSCOSEMData,true);
                #endif
                cOSEM.cOSEMDataBlock(dLMSCOSEMData);

            }
            else
                Console.WriteLine("Frame Check failed. DLMS and/or COSEM block error");
            DLMSCOSEMlist.Clear();
            DLMSStartFlag = false;            
        }
        public void dLMSCOSEMDebug(string leadingText, List<byte> byteList, bool eol )
        {
            Console.WriteLine(leadingText);
            foreach (byte b in byteList) Console.Write("{0:X2} ", b);
            if (eol) Console.WriteLine();
        }

    }
}