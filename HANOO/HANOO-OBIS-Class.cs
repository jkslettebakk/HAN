#undef OBISDEBUG

namespace HAN_OBIS
{
    class obisCodesClass
    {
        // These values must survive the objects initiate/close.....
        private static string catchVersionidentifier = string.Empty;
        private static string catchModelID = string.Empty;
        private static string catchModelType = string.Empty;

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
            public double currentEnergy { get; set; }
            public double sumEnergy { get; set; }
            public int currentVolte { get; set; }
            public double currentAmpere { get; set; }
        }

        obisValuesStruct obisValues;

        public class longHAN
        {
            [JsonPropertyName("DateTime")]
            public string dateTimePoll { get; set; }
            [JsonPropertyName("VersionIdentifier")]
            public string versionIdentifier { get; set; }
            [JsonPropertyName("ModelID")]
            public string modelID { get; set; }
            [JsonPropertyName("ModelType")]
            public string modelType { get; set; }
            public powerQ1Q4Object activePowerQ1Q4 { get; set; }
            public powerQ2Q3Object reactivePowerQ2Q3 { get; set; }
            public powerReactiveQ1Q2Object reactivePowerQ1Q2 { get; set; }
            public powerReactiveQ1Q4Object reactivePowerQ3Q4 { get; set; }
            public ampereIL1Object ampereIL1 { get; set; }
            public ampereIL3Object ampereIL3 { get; set; }
            public voltUL1Object voltUL1 { get; set; }
            public voltUL2Object voltUL2 { get; set; }
            public voltUL3Object voltUL3 { get; set; }
        }

        public class shortHAN
        {
            [JsonPropertyName("DateTime")]
            public string dateTimePoll { get; set; }
            [JsonPropertyName("VersionIdentifier")]
            public string versionIdentifier { get; set; }
            [JsonPropertyName("ModelID")]
            public string modelID { get; set; }
            [JsonPropertyName("ModelType")]
            public string modelType { get; set; }
            public powerQ1Q4Object activePowerQ1Q4 { get; set; }
        }

        public class powerQ1Q4Object
        {
            [JsonPropertyName("ActivePowerQ1Q4")]
            public double activePowerQ1Q4 { get; set; }
            [JsonPropertyName("ActivePowerQ1Q4UoM")]
            public string UoM { get; set; }
        }

        public class powerQ2Q3Object
        {
            [JsonPropertyName("ReactivePowerQ2Q3")]
            public doubae reactivePowerQ2Q3 { get; set; }
            [JsonPropertyName("ReactivePowerQ2Q3UoM")]
            public string UoM { get; set; }
        }
        
        public class powerReactiveQ1Q2Object
        {
            [JsonPropertyName("ReactivePowerQ1Q2")]
            public double reactivePowerQ1Q2 { get; set; }
            [JsonPropertyName("ReactivePowerQ1Q2UoM")]
            public string UoM { get; set; }
        }

        public class powerReactiveQ1Q4Object
        {
            [JsonPropertyName("ReactivePowerQ3Q4")]
            public double reactivePowerQ3Q4 { get; set; }
            [JsonPropertyName("ReactivePowerQ3Q4UoM")]
            public string UoM { get; set; }
        }

        public class ampereIL1Object
        {
            [JsonPropertyName("AmperePhase1")]
            public double ampereIL1 { get; set; }
            [JsonPropertyName("AmperePhase1UoM")]
            public string UoM { get; set; }
        }

        public class ampereIL3Object
        {
            [JsonPropertyName("AmperePhase3")]
            public double ampereIL3 { get; set; }
            [JsonPropertyName("AmperePhase3UoM")]
            public string UoM { get; set; }
        }

        public class voltUL1Object
        {
            [JsonPropertyName("VoltPhase1")]
            public int voltUL1 { get; set; }
            [JsonPropertyName("VoltPhase1UoM")]
            public string UoM { get; set; }
        }

        public class voltUL2Object
        {
            [JsonPropertyName("VoltPhase2")]
            public int voltUL2 { get; set; }
            [JsonPropertyName("VoltPhase2UoM")]
            public string UoM { get; set; }
        }
        public class voltUL3Object
        {
            [JsonPropertyName("VoltPhase3")]
            public int voltUL3 { get; set; }
            [JsonPropertyName("VoltPhase3UoM")]
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
            public string HAN_Vendor { get; set; }
        }

        //a obis struct list
        List<obisCodesStruct> legalObisCodes = new List<obisCodesStruct>
        {
            new obisCodesStruct{A=1,B=0,C=1, D=7,E=0,  F=255,obis="1.0.1.7.0.255",  UoM= "W",    scale = 1.0000,objectName="Q1+Q4",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=1,C= 0,D=2,E=129,F=255,obis="1.1.0.2.129.255",UoM= " ",    scale = 1.0000,objectName="Version identifier",HAN_Vendor ="Aidon & Kamstrup"},
            new obisCodesStruct{A=0,B=0,C=96,D=1,E=  0,F=255,obis="0.0.96.1.0.255", UoM= " ",    scale = 1.0000,objectName="Model ID",          HAN_Vendor ="Aidon"}, 
            new obisCodesStruct{A=0,B=0,C=96,D=1,E=  7,F=255,obis="0.0.96.1.7.255", UoM= " ",    scale = 1.0000,objectName="Model type",        HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 2,D=7,E=  0,F=255,obis="1.0.2.7.0.255",  UoM= "W",    scale = 1.0000,objectName="Q2+Q3",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 3,D=7,E=  0,F=255,obis="1.0.3.7.0.255",  UoM= "kVAr", scale = 1.0000,objectName="Q1+Q2",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 4,D=7,E=  0,F=255,obis="1.0.4.7.0.255",  UoM= "kVAr", scale = 1.0000,objectName="Q3+Q4",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=31,D=7,E=  0,F=255,obis="1.0.31.7.0.255", UoM= "A",    scale = 0.1000,objectName="IL1",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=51,D=7,E=  0,F=255,obis="1.0.51.7.0.255", UoM= "A",    scale = 0.1000,objectName="IL2",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=71,D=7,E=  0,F=255,obis="1.0.71.7.0.255", UoM= "A",    scale = 0.1000,objectName="IL3",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=32,D=7,E=  0,F=255,obis="1.0.32.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL1",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=52,D=7,E=  0,F=255,obis="1.0.52.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL2",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=72,D=7,E=  0,F=255,obis="1.0.72.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL3",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=0,B=0,C= 1,D=0,E=  0,F=255,obis="0.0.1.0.0.255",  UoM= " ",    scale = 1.0000,objectName="Clock and Date",    HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 1,D=8,E=  0,F=255,obis="1.0.1.8.0.255",  UoM= "Wh",   scale = 1.0000,objectName="Q1+Q4",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 2,D=8,E=  0,F=255,obis="1.0.2.8.0.255",  UoM= "Wh",   scale = 0.1000,objectName="Q2+Q3",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 3,D=8,E=  0,F=255,obis="1.0.3.8.0.255",  UoM= "kVArh",scale = 1.0000,objectName="Q1+Q2",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 4,D=8,E=  0,F=255,obis="1.0.4.8.0.255",  UoM= "kVArh",scale = 1.0000,objectName="Q3+Q4",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=1,C= 0,D=0,E=  5,F=255,obis="1.1.0.0.5.255",  UoM= " ",    scale = 1.0000,objectName="GS1 number",        HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 1,D=7,E=  0,F=255,obis="1.1.1.7.0.255",  UoM= "W",    scale = 0.1000,objectName="P14",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 2,D=7,E=  0,F=255,obis="1.1.2.7.0.255",  UoM= "W",    scale = 0.1000,objectName="P23",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=31,D=7,E=  0,F=255,obis="1.1.31.7.0.255", UoM= "A",    scale = 0.0010,objectName="IL1",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=51,D=7,E=  0,F=255,obis="1.1.51.7.0.255", UoM= "A",    scale = 0.0010,objectName="IL2",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=71,D=7,E=  0,F=255,obis="1.1.71.7.0.255", UoM= "A",    scale = 0.0010,objectName="IL3",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=32,D=7,E=  0,F=255,obis="1.1.32.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL1",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=52,D=7,E=  0,F=255,obis="1.1.52.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL2",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=72,D=7,E=  0,F=255,obis="1.1.72.7.0.255", UoM= "V",    scale = 0.1000,objectName="UL3",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=0,B=1,C= 1,D=0,E=  0,F=255,obis="0.1.1.0.0.255",  UoM= " ",    scale = 1.0000,objectName="RTC",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 1,D=8,E=  0,F=255,obis="1.1.1.8.0.255",  UoM= "Wh",   scale = 0.1000,objectName="A14",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 2,D=8,E=  0,F=255,obis="1.1.2.8.0.255",  UoM= "Wh",   scale = 0.1000,objectName="A23",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=96,D=1,E=  1,F=255,obis="1.1.96.1.1.255", UoM= " ",    scale = 1.0000,objectName="Meter type",        HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 3,D=7,E=  0,F=255,obis="1.1.3.7.0.255",  UoM= "Var",  scale = 1.0000,objectName="Q12",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 4,D=7,E=  0,F=255,obis="1.1.4.7.0.255",  UoM= "Var",  scale = 1.0000,objectName="Q34",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 3,D=8,E=  0,F=255,obis="1.1.3.8.0.255",  UoM= "Varh", scale = 1.0000,objectName="R12",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 4,D=8,E=  0,F=255,obis="1.1.4.8.0.255",  UoM= "Varh", scale = 1.0000,objectName="R34",               HAN_Vendor ="Kamstrup"}
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
                Console.WriteLine("\n-------------------------------------------------------------");                
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

            // List2 & List3 (> 240 bytes) object. List1 is a small block (Power inly) med mindre en 25 bytes
            var HANList2and3 = new longHAN {
                dateTimePoll = dateTimeString,
                versionIdentifier = catchVersionidentifier,
                modelID = catchModelID,
                modelType = catchModelType,
                activePowerQ1Q4 = new powerQ1Q4Object {
                    activePowerQ1Q4 = 0.0,
                    UoM = string.Empty
                },
            a  reactivePowerQ2Q3 = new powerQ2Q3Object {
                a  reactivePowerQ2Q3 = 0.0,
                    UoM = string.Empty
                },
                reactivePowerQ1Q2 = new powerReactiveQ1Q2Object {
                    reactivePowerQ1Q2 = 0.0,
                    UoM = string.Empty
                },
                powerReactiveQ3Q4 reactiveP newactiveQ1Q4Object {
                    powerReactiveQ3Q4 reactiveP 0.0              UoM = string.Empty
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
            var HANList1 = new shortHAN {
                dateTimePoll = dateTimeString,
                versionIdentifier = catchVersionidentifier,
                modelID = catchModelID,
                modelType = catchModelType,
                activePowerQ1Q4 = new powerQ1Q4Object {
                    activePowerQ1Q4 = 0.0,
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
                            HANList2and3.versionIdentifier = catchVersionidentifier;
                            HANList1.versionIdentifier = catchVersionidentifier;
                        }
                        if ( (legalObisCodesIndex == 02) && (text.Length > 0 ) )
                        { 
                            catchModelID = text; 
                            HANList2and3.modelID = catchModelID;                            
                            HANList1.modelID = catchModelID;                            
                        }
                        if ( (legalObisCodesIndex == 03) && (text.Length > 0 ) )
                        { 
                            catchModelType = text; 
                            HANList2and3.modelType = catchModelType;
                            HANList1.modelType = catchModelType;
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
                            HANList2and3.versionIdentifier = catchVersionidentifier;
                            HANList1.versionIdentifier = catchVersionidentifier;
                        }
                        if ( catchModelID.Length > 0 )
                        {
                            HANList2and3.modelID = catchModelID;
                            HANList1.modelID = catchModelID;
                        }
                        if ( catchModelType.Length > 0 )
                        {
                            HANList2and3.modelType = catchModelType;
                            HANList1.modelType = catchModelType;
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
                            {
                                // Powercollection list 1, 2 and 3
                                HANList1.activePowerQ1Q4.activePowerQ1Q4 = value;
                                HANList1.activePowerQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList2and3.activePowerQ1Q4.activePowerQ1Q4 = value;
                                HANList2and3.activePowerQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 04 ) 
                            {
                                HANList2ana3.reactivePowerQ2Q3.reactivePowerQ2Q3 = value;
                                HANList2ana3.reactivePowerQ2Q3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 05 )
                            {
                                HANList2and3.reactivePowerQ1Q2.reactivePowerQ1Q2 = value;
                                HANList2and3.reactivePowerQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 06 )
                            {
                                HANList2and3.reactivePowerQ3Q4.reactivePowerQ3Q4 = value;
                                HANList2and3.reactivePowerQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                             else
                            {
                                Console.WriteLine("Error? legalObisCodesIndex = {0}",legalObisCodesIndex);
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

                            if ( legalObisCodesIndex == 07 )
                            {
                                HANList2and3.ampereIL1.ampereIL1 = value;
                                HANList2and3.ampereIL1.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 09 ) 
                            {
                                HANList2and3.ampereIL3.ampereIL3 = value;
                                HANList2and3.ampereIL3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                             else
                            {
                                Console.WriteLine("Error? legalObisCodesIndex = {0}",legalObisCodesIndex);
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

                            if ( legalObisCodesIndex == 05 )
                            {
                                HANList2and3.reactivePowerQ1Q2.reactivePowerQ1Q2 = value;
                                HANList2and3.reactivePowerQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 06 )
                            {
                                HANList2and3.reactivePowerQ3Q4.reactivePowerQ3Q4 = value;
                                HANList2and3.reactivePowerQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 10 )
                            {
                                HANList2and3.voltUL1.voltUL1 = value;
                                HANList2and3.voltUL1.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 11 ) 
                            {
                                HANList2and3.voltUL2.voltUL2 = value;
                                HANList2and3.voltUL2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 12 ) 
                            {
                                HANList2and3.voltUL3.voltUL3 = value;
                                HANList2and3.voltUL3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else
                            {
                                Console.WriteLine("Error? legalObisCodesIndex = {0}",legalObisCodesIndex);
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

                // I need the Compressed Jason in all cases for database storage
                if ( oBISdata.Length < 30 )
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList1);
                } else
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList2and3);
                }

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
                    } else
                    {
                        jSONstring = JsonSerializer.Serialize(HANList2and3, options);
                    }
                    Console.WriteLine(jSONstring);
                }

                // next, call the API and send CRUD to database. Temporary print to screen. Send only every 10 seconds
                if ( HANList2and3.voltUL1.voltUL1 > 0 )
                {
                    if ( !OOuCP.uCP.HANOODefaultParameters.LogJsonCompressed && !OOuCP.uCP.HANOODefaultParameters.LogJson)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Json data to CRUD API https://HAN.slettebakk.com:");
                        Console.ResetColor();
                        Console.WriteLine(jSONstringCompressed);
                    }
                }
                // 
        }
    }
}
