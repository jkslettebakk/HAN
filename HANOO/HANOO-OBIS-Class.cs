#undef OBISDEBUG

namespace HAN_OBIS
{
    class obisCodesClass
    {
        // These values must survive the objects initiate/close.....
        private static string catchVersionidentifier = string.Empty;
        private static string catchModelID = string.Empty;
        private static string catchModelType = string.Empty;
        private static int loops = 1;
        private static bool redirectStandardError = true;
        private static string apiAdressToEndpoint = string.Empty;

        // OBIS types - Standard accoring to NVE
        private protected const byte TYPE_STRING = 0x0a;
        private protected const byte TYPE_UINT32_0x06 = 0x06;
        private protected const byte TYPE_INT16_0x10  = 0x10;
        private protected const byte TYPE_OCTETS_0x09 = 0x09;
        private protected const byte TYPE_UINT16_0x12 = 0x12;
        private protected const byte STRUCT_TEXT_0x02 = 0x02;
        private protected const byte STRUCT_DATA_0x03 = 0x03;
        private protected const int  oBISLength  = 6;

        struct obisValuesStruct
        {
            public double currentEnergy { get; set; } = 0.0;
            public double sumEnergy { get; set; } = 0.0;
            public int currentVolte { get; set; } = 0;
            public double currentAmpere { get; set; }  = 0.0;
        }

        obisValuesStruct obisValues;

        public class List1
        { // HAN List 1 data
            public string dateTimePoll { get; set; }
            public string versionIdentifier { get; set; }
            public string modelID { get; set; }
            public string modelType { get; set; }
            public activePowerQ1Q4Object activePowerQ1Q4 { get; set; }
        }

        public class List2
        { // HAN List 2 data
            public string dateTimePoll { get; set; }
            public string versionIdentifier { get; set; }
            public string modelID { get; set; }
            public string modelType { get; set; }
            public activePowerQ1Q4Object activePowerQ1Q4 { get; set; }
            public activePowerQ2Q3Object activePowerQ2Q3 { get; set; }
            public reactivePowerQ1Q2Object reactivePowerQ1Q2 { get; set; }
            public reactivePowerQ3Q4Object reactivePowerQ3Q4 { get; set; }
            public ampereIL1Object ampereIL1 { get; set; }
            public ampereIL3Object ampereIL3 { get; set; }
            public voltUL1Object voltUL1 { get; set; }
            public voltUL2Object voltUL2 { get; set; }
            public voltUL3Object voltUL3 { get; set; }
        }

        public class List3
        { // HAN List 3 data
            public string dateTimePoll { get; set; }
            public string versionIdentifier { get; set; }
            public string modelID { get; set; }
            public string modelType { get; set; }
            public activePowerQ1Q4Object activePowerQ1Q4 { get; set; }
            public activePowerQ2Q3Object activePowerQ2Q3 { get; set; }
            public reactivePowerQ1Q2Object reactivePowerQ1Q2 { get; set; }
            public reactivePowerQ3Q4Object reactivePowerQ3Q4 { get; set; }
            public ampereIL1Object ampereIL1 { get; set; }
            public ampereIL3Object ampereIL3 { get; set; }
            public voltUL1Object voltUL1 { get; set; }
            public voltUL2Object voltUL2 { get; set; }
            public voltUL3Object voltUL3 { get; set; }
            public activePowerAQ1Q4Object activePowerAQ1Q4 { get; set; }
            public activePowerAQ2Q3Object activePowerAQ2Q3 { get; set; }
            public reactivePowerRQ1Q2Object reactivePowerRQ1Q2 { get; set; }
            public reactivePowerRQ3Q4Object reactivePowerRQ3Q4 { get; set; }
        }

        public class activePowerQ1Q4Object
        {
            public double activePowerQ1Q4 { get; set; }
            public string UoM { get; set; }
        }

        public class activePowerQ2Q3Object
        {
            public double activePowerQ2Q3 { get; set; }
            public string UoM { get; set; }
        }

        public class activePowerAQ1Q4Object
        {
            public double activePowerAQ1Q4 { get; set; }
            public string UoM { get; set; }
        }

        public class activePowerAQ2Q3Object
        {
            public double activePowerAQ2Q3 { get; set; }
            public string UoM { get; set; }
        }

        public class reactivePowerQ1Q2Object
        {
            public double reactivePowerQ1Q2 { get; set; }
            public string UoM { get; set; }
        }

        public class reactivePowerQ3Q4Object
        {
            public double reactivePowerQ3Q4 { get; set; }
            public string UoM { get; set; }
        }

        public class reactivePowerRQ3Q4Object
        {
            public double reactivePowerRQ3Q4 { get; set; }
            public string UoM { get; set; }
        }

        public class reactivePowerRQ1Q2Object
        {
            public double reactivePowerRQ1Q2 { get; set; }
            public string UoM { get; set; }
        }

        public class ampereIL1Object
        {
            public double ampereIL1 { get; set; }
            public string UoM { get; set; }
        }

        public class ampereIL3Object
        {
            public double ampereIL3 { get; set; }
            public string UoM { get; set; }
        }

        public class voltUL1Object
        {
            public int voltUL1 { get; set; }
            public string UoM { get; set; }
        }

        public class voltUL2Object
        {
            public int voltUL2 { get; set; }
            public string UoM { get; set; }
        }
        public class voltUL3Object
        {
            public int voltUL3 { get; set; }
            public string UoM { get; set; }
        }

        private protected struct obisCodesStruct
        {
            public byte A { get; set; }
            public byte B { get; set; }
            public byte C { get; set; }
            public byte D { get; set; }
            public byte E { get; set; }
            public byte F { get; set; }
            public string UoM { get; set; }
            public double scale { get; set; }
            public string obis { get; set; }
            public string obisType { get; set; }
            public string objectName { set; get; }
            public string vendorObject { set; get; }
            public string HAN_Vendor { get; set; }
        }

        //a obis struct list
        List<obisCodesStruct> legalObisCodes = new List<obisCodesStruct>
        {
            new obisCodesStruct{A=1,B=0,C=1, D=7,E=0,  F=255,obis="1.0.1.7.0.255",  UoM= "W",    scale = 1.0000,objectName="Q1Q4",              vendorObject="Q1+Q4",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=1,C= 0,D=2,E=129,F=255,obis="1.1.0.2.129.255",UoM= " ",    scale = 1.0000,objectName="Version identifier",vendorObject="Version identifier",HAN_Vendor ="Aidon & Kamstrup"},
            new obisCodesStruct{A=0,B=0,C=96,D=1,E=  0,F=255,obis="0.0.96.1.0.255", UoM= " ",    scale = 1.0000,objectName="Model ID",          vendorObject="Model ID",          HAN_Vendor ="Aidon"}, 
            new obisCodesStruct{A=0,B=0,C=96,D=1,E=  7,F=255,obis="0.0.96.1.7.255", UoM= " ",    scale = 1.0000,objectName="Model type",        vendorObject="Model type",        HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 2,D=7,E=  0,F=255,obis="1.0.2.7.0.255",  UoM= "W",    scale = 1.0000,objectName="Q2Q3",              vendorObject="Q2+Q3",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 3,D=7,E=  0,F=255,obis="1.0.3.7.0.255",  UoM= "kVAr", scale = 1.0000,objectName="Q1Q2",              vendorObject="Q1+Q2",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 4,D=7,E=  0,F=255,obis="1.0.4.7.0.255",  UoM= "kVAr", scale = 1.0000,objectName="Q3Q4",              vendorObject="Q3+Q4",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=31,D=7,E=  0,F=255,obis="1.0.31.7.0.255", UoM= "A",    scale = 0.1000,objectName="IL1",               vendorObject="IL1",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=51,D=7,E=  0,F=255,obis="1.0.51.7.0.255", UoM= "A",    scale = 0.1000,objectName="IL2",               vendorObject="IL2",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=71,D=7,E=  0,F=255,obis="1.0.71.7.0.255", UoM= "A",    scale = 0.1000,objectName="IL3",               vendorObject="IL3",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=32,D=7,E=  0,F=255,obis="1.0.32.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL1",               vendorObject="UL1",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=52,D=7,E=  0,F=255,obis="1.0.52.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL2",               vendorObject="UL2",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=72,D=7,E=  0,F=255,obis="1.0.72.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL3",               vendorObject="UL3",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=0,B=0,C= 1,D=0,E=  0,F=255,obis="0.0.1.0.0.255",  UoM= " ",    scale = 1.0000,objectName="Clock and Date",    vendorObject="Clock and Date",    HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 1,D=8,E=  0,F=255,obis="1.0.1.8.0.255",  UoM= "Wh",   scale = 0.1000,objectName="AQ1Q4",             vendorObject="A+Q1+Q4",           HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 2,D=8,E=  0,F=255,obis="1.0.2.8.0.255",  UoM= "Wh",   scale = 0.1000,objectName="AQ2Q3",             vendorObject="A-Q2+Q3",           HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 3,D=8,E=  0,F=255,obis="1.0.3.8.0.255",  UoM= "kVArh",scale = 0.1000,objectName="RQ1Q2",             vendorObject="R+Q1+Q2",           HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 4,D=8,E=  0,F=255,obis="1.0.4.8.0.255",  UoM= "kVArh",scale = 0.1000,objectName="RQ3Q4",             vendorObject="R-Q3+Q4",           HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=1,C= 0,D=0,E=  5,F=255,obis="1.1.0.0.5.255",  UoM= " ",    scale = 1.0000,objectName="GS1 number",        vendorObject="GS1 number",        HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 1,D=7,E=  0,F=255,obis="1.1.1.7.0.255",  UoM= "W",    scale = 0.1000,objectName="P14",               vendorObject="P14",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 2,D=7,E=  0,F=255,obis="1.1.2.7.0.255",  UoM= "W",    scale = 0.1000,objectName="P23",               vendorObject="P23",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=31,D=7,E=  0,F=255,obis="1.1.31.7.0.255", UoM= "A",    scale = 0.0010,objectName="IL1",               vendorObject="IL1",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=51,D=7,E=  0,F=255,obis="1.1.51.7.0.255", UoM= "A",    scale = 0.0010,objectName="IL2",               vendorObject="IL2",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=71,D=7,E=  0,F=255,obis="1.1.71.7.0.255", UoM= "A",    scale = 0.0010,objectName="IL3",               vendorObject="IL3",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=32,D=7,E=  0,F=255,obis="1.1.32.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL1",               vendorObject="UL1",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=52,D=7,E=  0,F=255,obis="1.1.52.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL2",               vendorObject="UL2",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=72,D=7,E=  0,F=255,obis="1.1.72.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL3",               vendorObject="UL3",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=0,B=1,C= 1,D=0,E=  0,F=255,obis="0.1.1.0.0.255",  UoM= " ",    scale = 1.0000,objectName="RTC",               vendorObject="RTC",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 1,D=8,E=  0,F=255,obis="1.1.1.8.0.255",  UoM= "Wh",   scale = 0.1000,objectName="A14",               vendorObject="A14",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 2,D=8,E=  0,F=255,obis="1.1.2.8.0.255",  UoM= "Wh",   scale = 0.1000,objectName="A23",               vendorObject="A23",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=96,D=1,E=  1,F=255,obis="1.1.96.1.1.255", UoM= " ",    scale = 1.0000,objectName="Meter type",        vendorObject="Meter type",        HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 3,D=7,E=  0,F=255,obis="1.1.3.7.0.255",  UoM= "Var",  scale = 1.0000,objectName="Q12",               vendorObject="Q12",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 4,D=7,E=  0,F=255,obis="1.1.4.7.0.255",  UoM= "Var",  scale = 1.0000,objectName="Q34",               vendorObject="Q34",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 3,D=8,E=  0,F=255,obis="1.1.3.8.0.255",  UoM= "Varh", scale = 1.0000,objectName="R12",               vendorObject="R12",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 4,D=8,E=  0,F=255,obis="1.1.4.8.0.255",  UoM= "Varh", scale = 1.0000,objectName="R34",               vendorObject="R34",               HAN_Vendor ="Kamstrup"}
        };

        private void showObisValues()
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

        private string showObis(int index)
        {
            if ( index >= 0 && index < legalObisCodes.Count)
                return legalObisCodes[index].obis;
            return "OBIS code " + index + " not found";

        }

        private int isObisFound(byte a, byte b, byte c, byte d, byte e, byte f)
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

        private int isObisFound(byte[] data, int start )
        {
            if ( data.Length < 6 ) return -1;
            return isObisFound(data[start + 0],data[start + 1],data[start + 2],data[start + 3],data[start + 4],data[start + 5]);
        }

        private string oBISCode(byte[] data, int start)
        {
            string cosem = string.Empty;
            if ( data.Length < 6 ) return "";
                cosem = data[start + 0].ToString("X2") + "." + 
                        data[start + 1].ToString("X2") + "." +
                        data[start + 2].ToString("X2") + "." +
                        data[start + 3].ToString("X2") + "." +
                        data[start + 4].ToString("X2") + "." +
                        data[start + 5].ToString("X2");
                Console.WriteLine("{0}", cosem);
            return cosem;
        }

        private string UoMObisCode(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            int location = isObisFound(a, b, c, d, e, f);
            if (location > -1)
                return legalObisCodes[location].UoM;
            return "Error in UoM";
        }

        protected internal void oBISBlock( byte[] oBISdata, OOUserConfigurationParameters OOuCP ) // COSEM block
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
            bool LLogOBIS = OOuCP.uCP.HANOODefaultParameters.LogOBIS;

            if (LLogOBIS)
            {
                Console.WriteLine("-------------------------------------------------------------\n" +
                                "Complete OBIS block");
                for (int i = 0; i < oBISdata.Length; i++)
                {
                    if ( i != 0 )
                    {
                        if ( (i % 10) == 0 ) Console.Write(" ");
                        if ( (i % 40) == 0 ) Console.WriteLine();
                    }
                    Console.Write("{0:X2} ",oBISdata[i]);
                }
                Console.WriteLine("\nBlock length={0}",oBISdata.Length);
                Console.WriteLine("-------------------------------------------------------------");                
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
            int numberOfObjects = oBISdata[1];
            int cOSEMIndex = 02;   // keep track of KEY position in OBIS buffer, start in position 2 (first Struct)
            int structType = oBISdata[cOSEMIndex + 1]; // first Struct location for the switch() function 
            int legalObisCodesIndex;
            // Find local time
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(localZone.Id));
            // Loacl time in database friendly "ISO 8601" format (i.e. "2021-12-18T20:31:11.6880753")
            string dateTimeString = dateTime.ToString("O");

            var HANList1 = new List1 {
                dateTimePoll = dateTimeString,
                versionIdentifier = catchVersionidentifier,
                modelID = catchModelID,
                modelType = catchModelType,
                activePowerQ1Q4 = new activePowerQ1Q4Object {
                    activePowerQ1Q4 = 0.0,
                    UoM = string.Empty
                }
            };

            // List2 (> 240 bytes) object. List1 is a small block (Power inly) with less than 25 bytes
            var HANList2 = new List2 {
                dateTimePoll = dateTimeString,
                versionIdentifier = catchVersionidentifier,
                modelID = catchModelID,
                modelType = catchModelType,
                activePowerQ1Q4 = new activePowerQ1Q4Object {
                    activePowerQ1Q4 = 0.0,
                    UoM = string.Empty
                },
                activePowerQ2Q3 = new activePowerQ2Q3Object {
                    activePowerQ2Q3 = 0.0,
                    UoM = string.Empty
                },
                reactivePowerQ1Q2 = new reactivePowerQ1Q2Object {
                    reactivePowerQ1Q2 = 0.0,
                    UoM = string.Empty
                },
                reactivePowerQ3Q4 = new reactivePowerQ3Q4Object {
                    reactivePowerQ3Q4 = 0.0,
                    UoM = string.Empty
                },
                ampereIL1 = new ampereIL1Object {
                    ampereIL1 = 0.0,
                    UoM = string.Empty
                },
                ampereIL3 = new ampereIL3Object {
                    ampereIL3 = 0.0,
                    UoM = string.Empty

                },
                voltUL1 = new voltUL1Object {
                    voltUL1 = 0,
                    UoM = string.Empty
                },
                voltUL2 = new voltUL2Object {
                    voltUL2 = 0,
                    UoM = string.Empty
                },
                voltUL3 = new voltUL3Object {
                    voltUL3 = 0,
                    UoM = string.Empty
                }
            };

            // List3 (> 240 bytes) object.
            var HANList3 = new List3 {
                dateTimePoll = dateTimeString,
                versionIdentifier = catchVersionidentifier,
                modelID = catchModelID,
                modelType = catchModelType,
                activePowerQ1Q4 = new activePowerQ1Q4Object {
                    activePowerQ1Q4 = 0.0,
                    UoM = string.Empty
                },
                activePowerQ2Q3 = new activePowerQ2Q3Object {
                    activePowerQ2Q3 = 0.0,
                    UoM = string.Empty
                },
                reactivePowerQ1Q2 = new reactivePowerQ1Q2Object {
                    reactivePowerQ1Q2 = 0.0,
                    UoM = string.Empty
                },
                reactivePowerQ3Q4 = new reactivePowerQ3Q4Object {
                    reactivePowerQ3Q4 = 0.0,
                    UoM = string.Empty
                },
                ampereIL1 = new ampereIL1Object {
                    ampereIL1 = 0.0,
                    UoM = string.Empty
                },
                ampereIL3 = new ampereIL3Object {
                    ampereIL3 = 0.0,
                    UoM = string.Empty

                },
                voltUL1 = new voltUL1Object {
                    voltUL1 = 0,
                    UoM = string.Empty
                },
                voltUL2 = new voltUL2Object {
                    voltUL2 = 0,
                    UoM = string.Empty
                },
                voltUL3 = new voltUL3Object {
                    voltUL3 = 0,
                    UoM = string.Empty
                },
                activePowerAQ1Q4 = new activePowerAQ1Q4Object {
                    activePowerAQ1Q4 = 0,
                    UoM = string.Empty
                },
                activePowerAQ2Q3 = new activePowerAQ2Q3Object {
                    activePowerAQ2Q3 = 0,
                    UoM = string.Empty
                },
                reactivePowerRQ1Q2 = new reactivePowerRQ1Q2Object {
                    reactivePowerRQ1Q2 = 0,
                    UoM = string.Empty
                },
                reactivePowerRQ3Q4 = new reactivePowerRQ3Q4Object {
                    reactivePowerRQ3Q4 = 0,
                    UoM = string.Empty
                }
            };


            if(LLogOBIS) Console.WriteLine("\n-------------- New COSEM block containing OBIS objects --------------\n");

            for ( int i = 0; i < numberOfObjects; i++ ) // Array of objects (number of objects)
            { 
                legalObisCodesIndex = -1;
                switch (structType)  // switch on type of struct element (text (0x02) or data (0x03))
                {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
                    case(STRUCT_TEXT_0x02):
                        // Struct of 0x02 elements (text fields), text; = 10 bytes + 1 + text.length = 11 + text.length
                        legalObisCodesIndex = isObisFound( oBISdata, cOSEMIndex + 4); // Obis code start at cOSEMIndex + 4
                        if(LLogOBIS) Console.WriteLine("COSEM Object = {0}", showObis(legalObisCodesIndex) );
                        
                        // text block to be prosessed
                        cOSEMIndex = cOSEMIndex + 4 + oBISLength; // cOSEMIndex on Obis code; Move to start text....
                        if(LLogOBIS) Console.WriteLine("Start text at cOSEMIndex = {0}, value={1:X2}", cOSEMIndex, oBISdata[cOSEMIndex]);
                        cOSEMIndex += 1; // Index for length of text
                        if(LLogOBIS) Console.WriteLine("Cosem index = {0}, tekst lengde = {1:X2} ({2})", cOSEMIndex, oBISdata[cOSEMIndex], oBISdata[cOSEMIndex]);

                        string text = string.Empty;
                        for (int j=0; j<oBISdata[cOSEMIndex]; j++)  // prosess string from OBIS byte array
                        {
                            if(LLogOBIS) Console.Write("{0}",Convert.ToChar(oBISdata[cOSEMIndex + 1 + j]));
                            text += Convert.ToChar(oBISdata[cOSEMIndex + 1 + j]);
                        }
                        if ( (legalObisCodesIndex == 01) && (text.Length > 0 ) )
                        { 
                            catchVersionidentifier = text; 
                            HANList1.versionIdentifier = catchVersionidentifier;
                            HANList2.versionIdentifier = catchVersionidentifier;
                            HANList3.versionIdentifier = catchVersionidentifier;
                        }
                        if ( (legalObisCodesIndex == 02) && (text.Length > 0 ) )
                        { 
                            catchModelID = text; 
                            HANList1.modelID = catchModelID;                            
                            HANList2.modelID = catchModelID;                            
                            HANList3.modelID = catchModelID;                            
                        }
                        if ( (legalObisCodesIndex == 03) && (text.Length > 0 ) )
                        { 
                            catchModelType = text; 
                            HANList1.modelType = catchModelType;
                            HANList2.modelType = catchModelType;
                            HANList3.modelType = catchModelType;
                        }


                        if(LLogOBIS) Console.WriteLine();

                        // Prepare for next Struc block
                        cOSEMIndex += oBISdata[cOSEMIndex] + 1; // Position end of text, ready for next OBIS block
                        structType = oBISdata[cOSEMIndex + 1];
                        if(LLogOBIS) Console.WriteLine("\nNext structType= {0} in position {1}",structType,cOSEMIndex + 1);                        
                        break;
                        
                    case(STRUCT_DATA_0x03):
                        // Struct av 3 elementer (data fields), data; = 10 bytes + 11/9 bytes data (21)
                        if(LLogOBIS) Console.WriteLine("Struct value {0:x2} ok. with structType = {1:x2}. cOSEMIndex = {2}.",STRUCT_DATA_0x03,structType,cOSEMIndex);
                        legalObisCodesIndex = isObisFound( oBISdata, cOSEMIndex + 4);  // Obis code starts at cOSEMIndex
                        if(LLogOBIS) Console.WriteLine("COSEM Object = {0}", showObis(legalObisCodesIndex) );

                        // Set Json heading data
                        if ( catchVersionidentifier.Length > 0 )
                        {
                            HANList1.versionIdentifier = catchVersionidentifier;
                            HANList2.versionIdentifier = catchVersionidentifier;
                            HANList3.versionIdentifier = catchVersionidentifier;
                        }
                        if ( catchModelID.Length > 0 )
                        {
                            HANList1.modelID = catchModelID;
                            HANList2.modelID = catchModelID;
                            HANList3.modelID = catchModelID;
                        }
                        if ( catchModelType.Length > 0 )
                        {
                            HANList1.modelType = catchModelType;
                            HANList2.modelType = catchModelType;
                            HANList3.modelType = catchModelType;
                        }

                        // Data block to be prosessed
                        cOSEMIndex = cOSEMIndex + 4 + oBISLength;

                        if(LLogOBIS) Console.WriteLine("Start Data at cOSEMIndex = {0}, value = {1:X2} ",cOSEMIndex, oBISdata[cOSEMIndex]);

                        if ( oBISdata[cOSEMIndex] == TYPE_UINT32_0x06)
                        {
                            // Power/Energy values (W og kW)
                            uint value;
                            if(LLogOBIS) Console.WriteLine("Found TYPE_UINT32_0x06 (1 byte) + 4 byte data + 02 02 + UoM?? (4 byte) == 11 bytes");
                            if ( oBISdata.Length > cOSEMIndex + 11 + 2) structType = oBISdata[cOSEMIndex + 11 + 1]; // next data type
                            if(LLogOBIS) Console.Write("{0}=",legalObisCodes[legalObisCodesIndex].objectName);

                            if(LLogOBIS)
                            {
                                for ( int j=0; j< 11; j++) Console.Write("{0:X2} ",oBISdata[cOSEMIndex+j]);
                                Console.Write("({0}) ",legalObisCodes[legalObisCodesIndex].UoM);
                                Console.WriteLine();
                            }
                            obisValues.currentEnergy = ((((oBISdata[cOSEMIndex+1]<<24)+oBISdata[cOSEMIndex+2]<<16)+oBISdata[cOSEMIndex+3]<<8)+oBISdata[cOSEMIndex+4]<<0)*legalObisCodes[legalObisCodesIndex].scale;
                            value = (uint) (((((oBISdata[cOSEMIndex+1]<<24)+oBISdata[cOSEMIndex+2]<<16)+oBISdata[cOSEMIndex+3]<<8)+oBISdata[cOSEMIndex+4]<<0)*legalObisCodes[legalObisCodesIndex].scale);

                            if ( legalObisCodesIndex == 00 )
                            { // Powercollection list 1, 2 and 3
                                HANList1.activePowerQ1Q4.activePowerQ1Q4 = value;
                                HANList1.activePowerQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList2.activePowerQ1Q4.activePowerQ1Q4 = value;
                                HANList2.activePowerQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.activePowerQ1Q4.activePowerQ1Q4 = value;
                                HANList3.activePowerQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 04 ) // Watt
                            {
                                HANList2.activePowerQ2Q3.activePowerQ2Q3 = value;
                                HANList2.activePowerQ2Q3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.activePowerQ2Q3.activePowerQ2Q3 = value;
                                HANList3.activePowerQ2Q3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 05 ) // KVAr
                            {
                                HANList2.reactivePowerQ1Q2.reactivePowerQ1Q2 = value;
                                HANList2.reactivePowerQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.reactivePowerQ1Q2.reactivePowerQ1Q2 = value;
                                HANList3.reactivePowerQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 06 ) // KVAr
                            {
                                HANList2.reactivePowerQ3Q4.reactivePowerQ3Q4 = value;
                                HANList2.reactivePowerQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.reactivePowerQ3Q4.reactivePowerQ3Q4 = value;
                                HANList3.reactivePowerQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 14 ) // Wh
                            {   // What per houre 
                                HANList3.activePowerAQ1Q4.activePowerAQ1Q4 = value;
                                HANList3.activePowerAQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 15 ) // Wh
                            {   // 
                                HANList3.activePowerAQ2Q3.activePowerAQ2Q3 = value;
                                HANList3.activePowerAQ2Q3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 16 ) // KVArh
                            {   // 
                                HANList3.reactivePowerRQ1Q2.reactivePowerRQ1Q2 = value;
                                HANList3.reactivePowerRQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 17 ) // KVArh
                            {   // 
                                HANList3.reactivePowerRQ3Q4.reactivePowerRQ3Q4 = value;
                                HANList3.reactivePowerRQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                             else
                            {   // Some HAN data not treated correctly
                                Console.WriteLine("\nError or one unhandled OBIS? legalObisCodesIndex = {0} at {1}",legalObisCodesIndex,dateTimeString);
                                Console.WriteLine("Energi consumption type = {0}, value= {1} and UoM= {2}",oBISdata[cOSEMIndex],value,legalObisCodes[legalObisCodesIndex].UoM);
                            }

                            if(LLogOBIS) Console.WriteLine("{0:0.00} {1}", obisValues.currentEnergy, legalObisCodes[legalObisCodesIndex].UoM);
                            cOSEMIndex += 11; // Next COSEM block position
                        } 
                        else if ( oBISdata[cOSEMIndex] == TYPE_INT16_0x10)
                        {
                            // Current (A)
                            ushort value;
                            if(LLogOBIS) Console.WriteLine("Found TYPE_INT16_0x10 (1 byte) + 2 byte data + 02 02 + UoM?? (4 byte) == 9 bytes");
                            if ( oBISdata.Length > cOSEMIndex + 9 + 2) structType = oBISdata[cOSEMIndex + 9 + 1];
                            if(LLogOBIS) Console.Write("{0} has {1}=",legalObisCodes[legalObisCodesIndex].obis,legalObisCodes[legalObisCodesIndex].objectName);

                            if(LLogOBIS)
                            {
                                for ( int j=0; j< 9; j++) Console.Write("{0:X2} ",oBISdata[cOSEMIndex+j]);
                                Console.Write("({0}) ",legalObisCodes[legalObisCodesIndex].UoM);
                            }
                            obisValues.currentAmpere =  Math.Round(((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].scale, 2);
                            value =  (ushort) Math.Round(((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].scale, 2);

                            if ( legalObisCodesIndex == 07 ) // A
                            {
                                HANList2.ampereIL1.ampereIL1 = value;
                                HANList2.ampereIL1.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ampereIL1.ampereIL1 = value;
                                HANList3.ampereIL1.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 09 ) // A
                            {
                                HANList2.ampereIL3.ampereIL3 = value;
                                HANList2.ampereIL3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ampereIL3.ampereIL3 = value;
                                HANList3.ampereIL3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                             else
                            {   // Some HAN data not treated correctly
                                Console.WriteLine("\nError or one houre OBIS code? legalObisCodesIndex = {0} at {1}",legalObisCodesIndex,dateTimeString);
                                Console.WriteLine("Currency consumption type = {0}, value= {1} and UoM= {2}",oBISdata[cOSEMIndex],value,legalObisCodes[legalObisCodesIndex].UoM);
                            }

                            if(LLogOBIS) Console.WriteLine("{0:0.00} {1}", obisValues.currentAmpere, legalObisCodes[legalObisCodesIndex].UoM);
                            cOSEMIndex += 9; // Next COSEM block position
                        }
                        else if ( oBISdata[cOSEMIndex] == TYPE_UINT16_0x12)
                        {
                            // Volte (V,kVAr)
                            ushort value;
                            if(LLogOBIS) Console.WriteLine("Found TYPE_UINT16_0x12 (1 byte) + 2 byte data + 02 02 + UoM?? (4 byte) == 9 bytes");
                            if ( oBISdata.Length > cOSEMIndex + 9 +2) structType = oBISdata[cOSEMIndex + 9 + 1];
                            if(LLogOBIS) Console.Write("{0} has {1}=",legalObisCodes[legalObisCodesIndex].obis,legalObisCodes[legalObisCodesIndex].objectName);
                            if(LLogOBIS)
                            {
                                for ( int j=0; j< 9; j++) Console.Write("{0:X2} ",oBISdata[cOSEMIndex+j]);
                                Console.Write("({0}) ",legalObisCodes[legalObisCodesIndex].UoM);
                            }
                            obisValues.currentVolte = (int) (((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].scale);
                            value = (ushort) (((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].scale);

                            if ( legalObisCodesIndex == 05 ) // KVAr
                            {
                                HANList2.reactivePowerQ1Q2.reactivePowerQ1Q2 = value;
                                HANList2.reactivePowerQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.reactivePowerQ1Q2.reactivePowerQ1Q2 = value;
                                HANList3.reactivePowerQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 06 ) // KVAr
                            {
                                HANList2.reactivePowerQ3Q4.reactivePowerQ3Q4 = value;
                                HANList2.reactivePowerQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.reactivePowerQ3Q4.reactivePowerQ3Q4 = value;
                                HANList3.reactivePowerQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 10 ) // V
                            {
                                HANList2.voltUL1.voltUL1 = value;
                                HANList2.voltUL1.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.voltUL1.voltUL1 = value;
                                HANList3.voltUL1.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 11 ) // V
                            {
                                HANList2.voltUL2.voltUL2 = value;
                                HANList2.voltUL2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.voltUL2.voltUL2 = value;
                                HANList3.voltUL2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 12 ) // V
                            {
                                HANList2.voltUL3.voltUL3 = value;
                                HANList2.voltUL3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.voltUL3.voltUL3 = value;
                                HANList3.voltUL3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else
                            {   // Some HAN data not treated correctly
                                Console.WriteLine("\nError or one houre OBIS code? legalObisCodesIndex = {0} at {1}",legalObisCodesIndex,dateTimeString);
                                Console.WriteLine("Volte consumption type = {0}, value= {1} and UoM= {2}",oBISdata[cOSEMIndex],value,legalObisCodes[legalObisCodesIndex].UoM);
                            }
                            if(LLogOBIS) Console.WriteLine("{0:0.00} {1}", obisValues.currentVolte, legalObisCodes[legalObisCodesIndex].UoM);
                            cOSEMIndex += 9; // Next COSEM block position
                        }
                        else
                        {
                            Console.WriteLine("\nNo oBISdata value found on cOSEMIndec = {0}. Value is {1:X2}",cOSEMIndex,oBISdata[cOSEMIndex]);
                            Console.WriteLine("oBISdata[{0}] to end",cOSEMIndex);
                            for ( int j=cOSEMIndex; j<oBISdata.Length; j++)
                            {
                                if ( j != 0 )
                                {
                                    if ( (j % 10) == 0 ) Console.Write(" ");
                                    if ( (j % 40) == 0 ) Console.WriteLine();
                                }
                                Console.Write("{0:X2} ",oBISdata[j]);
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Struct value {0} not known in position cOSEMIndex = {1}.", structType,cOSEMIndex);
                        break;
                }
            }
                // Output result in desiered format
                //
                string jSONstring = string.Empty;
                string jSONstringCompressed = string.Empty;

                // We need the Compressed Jason in most cases for display or database REST point/CRUD storage
                if ( oBISdata.Length < 30 )
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList1);
                    apiAdressToEndpoint = OOuCP.uCP.HANOODefaultParameters.HANApiEndPoint + "/List1";
                    if ( OOuCP.uCP.HANOODefaultParameters.lJsonToApi && OOuCP.uCP.HANOODefaultParameters.lList1 )
                    {
                        bool curlResult = sendJsonToEndpoint(jSONstringCompressed, apiAdressToEndpoint, OOuCP.uCP.HANOODefaultParameters.LogJson);
                        if ( !curlResult ) Console.WriteLine("Curl issue result = {0}",curlResult);
                    }
                }
                else if ( oBISdata.Length < 250 )
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList2);
                    apiAdressToEndpoint = OOuCP.uCP.HANOODefaultParameters.HANApiEndPoint + "/List2";
                    if ( OOuCP.uCP.HANOODefaultParameters.lJsonToApi && OOuCP.uCP.HANOODefaultParameters.lList2 )
                    {
                        bool curlResult = sendJsonToEndpoint(jSONstringCompressed, apiAdressToEndpoint, OOuCP.uCP.HANOODefaultParameters.LogJson);
                        if ( !curlResult ) Console.WriteLine("Curl issue result = {0}",curlResult);
                    }
                }
                else // Long list (List3) > 250. One object per hr...
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList3);
                    apiAdressToEndpoint = OOuCP.uCP.HANOODefaultParameters.HANApiEndPoint + "/List3";
                    Console.WriteLine("\nHouerly log:\n{0}",jSONstringCompressed); // Make sure I have a log of this :-)
                    if ( OOuCP.uCP.HANOODefaultParameters.lJsonToApi && OOuCP.uCP.HANOODefaultParameters.lList3 )
                    {
                        bool curlResult = sendJsonToEndpoint(jSONstringCompressed, apiAdressToEndpoint, OOuCP.uCP.HANOODefaultParameters.LogJson);
                        if ( !curlResult ) Console.WriteLine("Curl issue result = {0}",curlResult);
                    }
                }

                // Then to schreen
                if ( OOuCP.uCP.HANOODefaultParameters.LogJsonCompressed )
                {
                    Console.WriteLine(jSONstringCompressed);
                }
                else if ( OOuCP.uCP.HANOODefaultParameters.LogJson )
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    if ( oBISdata.Length < 30 )
                    {
                        jSONstring = JsonSerializer.Serialize(HANList1, options);
                    }
                    else if ( oBISdata.Length < 250 )
                    {
                        jSONstring = JsonSerializer.Serialize(HANList2, options);
                    }
                    else 
                    {
                        jSONstring = JsonSerializer.Serialize(HANList3, options);
                    }
                    Console.WriteLine(jSONstring);
                }

                // next, call the API and send CRUD to database. Temporary print to screen. Send only every 10 seconds
                if ( !OOuCP.uCP.HANOODefaultParameters.LogJsonCompressed && OOuCP.uCP.HANOODefaultParameters.LogJson)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Json data to CRUD API " + apiAdressToEndpoint);
                    Console.ResetColor();
                    Console.WriteLine(jSONstringCompressed);
                    // span of curl to send data to API endpoint
                } 
        }

        public bool sendJsonToEndpoint(string jsonString, string endPoint, bool LogJson )
        {
            try
            {
                using (Process myProcess = new Process()) // preparing to spawn off a 'curl' OS process
                {
                    // Process parameters
                    myProcess.StartInfo.UseShellExecute = false; // Should be false on dotnet core
                    myProcess.StartInfo.RedirectStandardOutput = true; // false=display result from app, else redirect to /dev/null
                    myProcess.StartInfo.RedirectStandardError = true; // redirectStandardError; // Default false, else redirect to /dev/null...
                    myProcess.StartInfo.FileName = "curl"; // Using curl (temporary) til reprogrammed .NET core API is finished
                    myProcess.StartInfo.CreateNoWindow = false; // true or false gives no change
                    myProcess.StartInfo.ErrorDialog = true; 
                    // Preparing the curl string
                    string jsonStringLinux = jsonString.Replace("\"","\\\""); // Need to change from ' tp " since spawning the process to Linux makse string treatment strange
                    string arguments = "-X POST \"" + endPoint + "\" -H \"Content-Type: application/json\" -d \"" + jsonStringLinux + "\"";
                    myProcess.StartInfo.Arguments = arguments;
                    // Log to screen if needed
                    if ( LogJson ) Console.WriteLine("curl {0}",myProcess.StartInfo.Arguments);
                    // then send data to REST endpoint and further for CRUD processing in database
                    myProcess.Start();
                    myProcess.WaitForExit();
                    if ( !myProcess.StartInfo.RedirectStandardOutput ||  !myProcess.StartInfo.RedirectStandardError )
                        Console.WriteLine(); // Console service makes schreen look messy
                }
                    redirectStandardError = true; // Make sure we do not fill the logfiles. Turn of logging
                    if ( loops == 1 ) 
                    {
                    Console.WriteLine("\nData will be sendt to: {0}",endPoint);
                    Console.WriteLine("Just to indicate we have started. Jsonstring={0}\n",jsonString);
                    Console.Write("."); // just to indicate alive
                    } else
                    Console.Write("."); // just to indicate alive

                    if ( (loops++ % 100) == 0 )
                    {
                        Console.WriteLine("\nData will be sendt to: {0}",endPoint);
                        Console.WriteLine("Just to indicate we are alive (every 100 REST calles (this is number {0})). Jsonstring={1}",loops,jsonString);
                        redirectStandardError = false;
                    }
                    return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}
