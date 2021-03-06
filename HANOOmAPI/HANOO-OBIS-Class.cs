#undef OBISDEBUG

namespace HAN_OBIS
{
    class obisCodesClass
    {
        // These values must survive the objects initiate/close.....
        private static string catchVersionidentifier = string.Empty;
        private static string catchModelID = string.Empty;
        private static string catchModelType = string.Empty;
        private static string apiAdressToEndpoint = string.Empty;
        private static double currentEnergy { get; set; } = 0.0;
        private static double sumEnergy { get; set; } = 0.0;
        private static int    currentVolte { get; set; } = 0;
        private static double currentAmpere { get; set; }  = 0.0;
        private static int PostAsyncLoops = 0; 
        private static byte List1Objects = 1;
        private static byte List2Objects = 12;
        private static byte List3Objects = 17;


        // OBIS types - Standard accoring to NVE
        private protected const byte TYPE_STRING = 0x0a;
        private protected const byte TYPE_UINT32_0x06 = 0x06; // 
        private protected const byte TYPE_INT16_0x10  = 0x10;
        private protected const byte TYPE_OCTETS_0x09 = 0x09;
        private protected const byte TYPE_UINT16_0x12 = 0x12;
        private protected const byte STRUCT_TEXT_0x02 = 0x02;
        private protected const byte STRUCT_DATA_0x03 = 0x03;
        private protected const int  oBISLength  = 6;

        public class List1 // HAN List 1 class; Aidon & KAIFA (Not for Kamstrup)
        {
            public string dateTimePoll { get; set; } = string.Empty;
            public string versionIdentifier { get; set; } = string.Empty;
            public string modelID { get; set; } = string.Empty;
            public string modelType { get; set; } = string.Empty;
//            public activePowerQ1Q4Object activePowerQ1Q4 = new activePowerQ1Q4Object();
            public activePowerQ1Q4Object activePowerQ1Q4 { get; set; }

            public void Dispose() // something to considder?
            {
                // dateTimePoll = string.Empty;
                // versionIdentifier { get; set; } = string.Empty;
                // modelID { get; set; } = string.Empty;
                // modelType { get; set; } = string.Empty;
                // activePowerQ1Q4 = null;
                // Console.WriteLine("Disposed"); // Do I need a Dispose routine? 
            }
        }

        public class List2 // HAN List 2 class; Aidon, KAIFA & Kamstrup
        {
            public string dateTimePoll { get; set; } = string.Empty;
            public string versionIdentifier { get; set; } = string.Empty;
            public string modelID { get; set; } = string.Empty;
            public string modelType { get; set; } = string.Empty;
            public activePowerQ1Q4Object activePowerQ1Q4 { get; set; }
            public activePowerQ2Q3Object activePowerQ2Q3 { get; set; }
            public reactivePowerQ1Q2Object reactivePowerQ1Q2 { get; set; }
            public reactivePowerQ3Q4Object reactivePowerQ3Q4 { get; set; }
            public ampereIL1Object ampereIL1 { get; set; }
            public ampereIL2Object ampereIL2 { get; set; }
            public ampereIL3Object ampereIL3 { get; set; }
            public voltUL1Object voltUL1 { get; set; }
            public voltUL2Object voltUL2 { get; set; }
            public voltUL3Object voltUL3 { get; set; }
        }

        public class List3 // HAN List 3 class; ; Aidon, KAIFA & Kamstrup
        {
            public string dateTimePoll { get; set; } = string.Empty;
            public string versionIdentifier { get; set; } = string.Empty;
            public string modelID { get; set; } = string.Empty;
            public string modelType { get; set; } = string.Empty;
            public activePowerQ1Q4Object activePowerQ1Q4 { get; set; }
            public activePowerQ2Q3Object activePowerQ2Q3 { get; set; }
            public reactivePowerQ1Q2Object reactivePowerQ1Q2 { get; set; }
            public reactivePowerQ3Q4Object reactivePowerQ3Q4 { get; set; }
            public ampereIL1Object ampereIL1 { get; set; }
            public ampereIL2Object ampereIL2 { get; set; }
            public ampereIL3Object ampereIL3 { get; set; }
            public voltUL1Object voltUL1 { get; set; }
            public voltUL2Object voltUL2 { get; set; }
            public voltUL3Object voltUL3 { get; set; }
            public meeterTimeObject meeterTime { get; set; }
            public activePowerAQ1Q4Object activePowerAQ1Q4 { get; set; }
            public activePowerAQ2Q3Object activePowerAQ2Q3 { get; set; }
            public reactivePowerRQ1Q2Object reactivePowerRQ1Q2 { get; set; }
            public reactivePowerRQ3Q4Object reactivePowerRQ3Q4 { get; set; }
        }

        public class activePowerQ1Q4Object
        {
            public double activePowerQ1Q4 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class activePowerQ2Q3Object
        {
            public double activePowerQ2Q3 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class activePowerAQ1Q4Object
        {
            public double activePowerAQ1Q4 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class activePowerAQ2Q3Object
        {
            public double activePowerAQ2Q3 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class reactivePowerQ1Q2Object
        {
            public double reactivePowerQ1Q2 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class reactivePowerQ3Q4Object
        {
            public double reactivePowerQ3Q4 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class reactivePowerRQ3Q4Object
        {
            public double reactivePowerRQ3Q4 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class reactivePowerRQ1Q2Object
        {
            public double reactivePowerRQ1Q2 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class ampereIL1Object
        {
            public double ampereIL1 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class ampereIL2Object
        {
            public double ampereIL2 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class ampereIL3Object
        {
            public double ampereIL3 { get; set; } = 0.0;
            public string UoM { get; set; } = string.Empty;
        }

        public class meeterTimeObject
        {
            public string meeterTime { get; set; } = string.Empty;
        }

        public class voltUL1Object
        {
            public int voltUL1 { get; set; } = 0;
            public string UoM { get; set; } = string.Empty;
        }

        public class voltUL2Object
        {
            public int voltUL2 { get; set; } = 0;
            public string UoM { get; set; } = string.Empty;
        }
        public class voltUL3Object
        {
            public int voltUL3 { get; set; } = 0;
            public string UoM { get; set; } = string.Empty;
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
        private protected static List<obisCodesStruct> legalObisCodes = new List<obisCodesStruct>
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

        private static void showObisValues()
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

        private static string showObis(int index)
        {
            if ( index >= 0 && index < legalObisCodes.Count)
                return legalObisCodes[index].obis;
            return "OBIS code " + index + " not found";

        }

        private static int areObisFound(byte a, byte b, byte c, byte d, byte e, byte f)
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

        private static int areObisFound(byte[] data, int start )
        {
            if ( data.Length < 6 ) return -1;
            return areObisFound(data[start + 0],data[start + 1],data[start + 2],data[start + 3],data[start + 4],data[start + 5]);
        }

        private static string oBISCode(byte[] data, int start)
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

        private static string UoMObisCode(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            int location = areObisFound(a, b, c, d, e, f);
            if (location > -1)
                return legalObisCodes[location].UoM;
            return "Error in UoM";
        }

        static async Task sendJsonToEndpoint( string jsonString, string endPoint, bool logApi)
        {
            await ExecuteJsonPost(endPoint, jsonString, logApi);
        }

        private static readonly HttpClient client = new HttpClient();

        private static async Task ExecuteJsonPost(string UriString, string Jsonstring, bool logApi)
        {
            // Console.WriteLine("ExecuteJsonPost :\n{0}",Jsonstring);

            string app = "application/json";

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(app));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            Uri uri = new Uri(UriString);
            StringContent postString = new StringContent(Jsonstring, Encoding.UTF8, app);

            HttpResponseMessage result = await client.PostAsync(UriString, postString);

            if ( logApi )
            {
                Console.WriteLine("{0}\tStatus etter PostAsync to {1} with (result.StatusCode)={2}",PostAsyncLoops++,UriString,result.StatusCode);
            }
            if ( !result.IsSuccessStatusCode )
            {
                string json = await result.Content.ReadAsStringAsync();

                // Data from the "result" object
                Console.WriteLine("{0}\tStatus etter PostAsync to {1} with (result.StatusCode)={2}",PostAsyncLoops++,UriString,result.StatusCode);
                // Data from the "result" object
                Console.WriteLine("PostAsync result (result.Content)={0}",result.Content);
                Console.WriteLine("PostAsync result (result.Headers)={0}",result.Headers);
                Console.WriteLine("PostAsync result (result.IsSuccessStatusCode)={0}",result.IsSuccessStatusCode);
                Console.WriteLine("PostAsync result (result.ReasonPhrase)={0}",result.ReasonPhrase);
                Console.WriteLine("PostAsync result (result.RequestMessage)={0}",result.RequestMessage);
                Console.WriteLine("PostAsync result (result.StatusCode)={0}",result.StatusCode);
                Console.WriteLine("PostAsync result (result.TrailingHeaders)={0}",result.TrailingHeaders);
                Console.WriteLine("PostAsync result (result.Version)={0}",result.Version);

                Console.WriteLine("---------------------------------");
                // Some data used in the Post API
                Console.WriteLine("Status etter PostAsync (result):\n{0}",result);
                Console.WriteLine("Status etter PostAsync (Jsonstring):\n{0}",Jsonstring);
                Console.WriteLine("Status etter PostAsync (json):\n{0}",json);
                Console.WriteLine("Uri string = {0}",UriString);
                Console.WriteLine("Post string = {0}",postString);
            }
            // if (result != null) result.Dispose(); // What does this dispose?
            // client.Dispose();            
        }

        public static async Task  oBISBlock( byte[] oBISdata, OOUserConfigurationParameters OOuCP ) // COSEM block
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
            int legalObisCodesIndex = 0;
            // Find local time
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            DateTime dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(localZone.Id));
            // Loacl time in database friendly "ISO 8601" format (i.e. "2021-12-18T20:31:11.6880753")
            string dateTimeString = dateTime.ToString("O");

            List1 HANList1 = new List1 {
                dateTimePoll = dateTimeString,
                versionIdentifier = catchVersionidentifier,
                modelID = catchModelID,
                modelType = catchModelType,
                activePowerQ1Q4 = new activePowerQ1Q4Object {
                    activePowerQ1Q4 = 0.0,
                    UoM = string.Empty
                }
            };
//            List1 HANList1 = new List1();

            // List2 (> 240 bytes) object. List1 is a small block (Power inly) with less than 25 bytes
            List2 HANList2 = new List2 {
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
                ampereIL2 = new ampereIL2Object {
                    ampereIL2 = 0.0,
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

//            List2 HANList2 = new List2();

            // List3 (> 240 bytes) object.
            List3 HANList3 = new List3 {
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
                ampereIL2 = new ampereIL2Object {
                    ampereIL2 = 0.0,
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
                meeterTime = new meeterTimeObject {
                    meeterTime = string.Empty
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

//            List3 HANList3 = new List3();

            if(LLogOBIS) Console.WriteLine("\n------- New COSEM block containing {0} OBIS objects --------------\n",numberOfObjects);

            for ( int i = 0; i < numberOfObjects; i++ ) // Array of objects (number of objects)
            { 
                legalObisCodesIndex = -1; // resetting value
                switch (structType)  // switch on type of struct element (text (0x02) or data (0x03))
                {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
                    case(STRUCT_TEXT_0x02):
                        // Struct of 0x02 elements (text fields), text; = 10 bytes + 1 + text.length = 11 + text.length
                        legalObisCodesIndex = areObisFound( oBISdata, cOSEMIndex + 4); // Obis code start at cOSEMIndex + 4
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
                        if ( (legalObisCodesIndex == 01) && (text.Length > 0 ) ) // Meeter Model type
                        { 
                            catchVersionidentifier = text; 
                            HANList1.versionIdentifier = catchVersionidentifier;
                            HANList2.versionIdentifier = catchVersionidentifier;
                            HANList3.versionIdentifier = catchVersionidentifier;
                        } else if ( (legalObisCodesIndex == 02) && (text.Length > 0 ) ) // Meeter Model type
                        { 
                            catchModelID = text; 
                            HANList1.modelID = catchModelID;                            
                            HANList2.modelID = catchModelID;                            
                            HANList3.modelID = catchModelID;                            
                        } else if ( (legalObisCodesIndex == 03) && (text.Length > 0 ) )  // Meeter Model type
                        { 
                            catchModelType = text; 
                            HANList1.modelType = catchModelType;
                            HANList2.modelType = catchModelType;
                            HANList3.modelType = catchModelType;
                        } else if ( legalObisCodesIndex == 13 && (text.Length > 0 ) ) // Time and Date in meeter
                        {
                            Console.WriteLine("Date & Tome not handled yet. Byte (Octet-String) sequence = ");
                            // format: 2020.01.15
                            // HANList3.meeterTime.meeterTime = text;
                            for (int j=0; j<oBISdata[cOSEMIndex]; j++)  // prosess string from OBIS byte array
                            {
                                Console.Write("{0:x2}",oBISdata[cOSEMIndex + 1 + j]);
                                HANList3.meeterTime.meeterTime += Convert.ToChar(oBISdata[cOSEMIndex + 1 + j]);
                            }
                            Console.WriteLine();
                            int year = ((int) ((oBISdata[cOSEMIndex+1]<<8)+(oBISdata[cOSEMIndex+2])));
                            int month = ((int) (oBISdata[cOSEMIndex+3]));
                            int day = ((int) (oBISdata[cOSEMIndex+4]));
                            int something = ((int) (oBISdata[cOSEMIndex+5]));
                            int hrs = ((int) (oBISdata[cOSEMIndex+6]));
                            int minutes = ((int) (oBISdata[cOSEMIndex+7]));
                            int seconds = ((int) (oBISdata[cOSEMIndex+8]));
                            HANList3.meeterTime.meeterTime = ((int) ((oBISdata[cOSEMIndex+1]<<8)+(oBISdata[cOSEMIndex+2]))).ToString("D4") + "-" +
                                                             ((int) (oBISdata[cOSEMIndex+3])).ToString("D2") + "-" +
                                                             ((int) (oBISdata[cOSEMIndex+4])).ToString("D2") + "T" +
                                                             ((int) (oBISdata[cOSEMIndex+6])).ToString("D2") + ":" +
                                                             ((int) (oBISdata[cOSEMIndex+7])).ToString("D2") + ":" +
                                                             ((int) (oBISdata[cOSEMIndex+8])).ToString("D4");
                            string meeterTime = year.ToString("D4") + "-" +
                                                month.ToString("D2") + "-" +
                                                day.ToString("D2") + "T" +
                                                hrs.ToString("D2") + ":" +
                                                minutes.ToString("D2") + ":" +
                                                seconds.ToString("D4");

                            Console.WriteLine("Meeter current \'int\' date and time:{0}-{1}-{2}T{3}:{4}:{5}",year, month, day,hrs,minutes,seconds);
                            Console.WriteLine("Meeter current \'string\' date and time:{0}",meeterTime);
                            Console.WriteLine("Meeter current \'HANList3.meeterTime.meeterTime\' date and time:{0}",HANList3.meeterTime.meeterTime);
                        } else
                            Console.WriteLine("Error in STRUCT_TEXT_0x02 ({0}) text input from HAN",STRUCT_TEXT_0x02);



                        if(LLogOBIS) Console.WriteLine();

                        // Prepare for next Struc block
                        cOSEMIndex += oBISdata[cOSEMIndex] + 1; // Position end of text, ready for next OBIS block
                        structType = oBISdata[cOSEMIndex + 1];
                        if(LLogOBIS) Console.WriteLine("\nNext structType= {0} in position {1}",structType,cOSEMIndex + 1);                        
                        break;
                        
                    case(STRUCT_DATA_0x03):
                        // Struct av 3 elementer (data fields), data; = 10 bytes + 11/9 bytes data (21)
                        if(LLogOBIS) Console.WriteLine("Struct value {0:x2} ok. with structType = {1:x2}. cOSEMIndex = {2}.",STRUCT_DATA_0x03,structType,cOSEMIndex);
                        legalObisCodesIndex = areObisFound( oBISdata, cOSEMIndex + 4);  // Obis code starts at cOSEMIndex
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

                        if ( oBISdata[cOSEMIndex] == TYPE_UINT32_0x06 )
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
                            if ( oBISdata.Length < cOSEMIndex+4 )
                            {
                                Console.WriteLine("Error. Index to long. Will crash. cOSEMIndex+4={0}, oBISdata.length={1}, OBIS code {2}",cOSEMIndex+4, oBISdata.Length,legalObisCodes[legalObisCodesIndex].obis);
                                Console.WriteLine("Will return to DMLS for next OBIS block.... I need to change the DLMS rotine, bit not now...");
                                foreach( byte b in oBISdata ) Console.Write("{0:X2} ",b);                               
                                Console.WriteLine();
                                return;
                            }
                            currentEnergy = ((((oBISdata[cOSEMIndex+1]<<24)+oBISdata[cOSEMIndex+2]<<16)+oBISdata[cOSEMIndex+3]<<8)+oBISdata[cOSEMIndex+4]<<0)*legalObisCodes[legalObisCodesIndex].scale;
                            value = (uint) currentEnergy;

                            if ( legalObisCodesIndex == 00 ) // Powercollection list 1, 2 and 3
                            {  // Wat
                                HANList1.activePowerQ1Q4.activePowerQ1Q4 = value;
                                HANList1.activePowerQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList2.activePowerQ1Q4.activePowerQ1Q4 = value;
                                HANList2.activePowerQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.activePowerQ1Q4.activePowerQ1Q4 = value;
                                HANList3.activePowerQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 04 ) // Powercollection list 2 and 3
                            {// Watt
                                HANList2.activePowerQ2Q3.activePowerQ2Q3 = value;
                                HANList2.activePowerQ2Q3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.activePowerQ2Q3.activePowerQ2Q3 = value;
                                HANList3.activePowerQ2Q3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 05 ) // kilo Volt Reactive list 2 and 3
                            {// KVAr
                                HANList2.reactivePowerQ1Q2.reactivePowerQ1Q2 = value;
                                HANList2.reactivePowerQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.reactivePowerQ1Q2.reactivePowerQ1Q2 = value;
                                HANList3.reactivePowerQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 06 ) // kilo Volt Reactive list 2 and 3
                            {// KVAr
                                HANList2.reactivePowerQ3Q4.reactivePowerQ3Q4 = value;
                                HANList2.reactivePowerQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.reactivePowerQ3Q4.reactivePowerQ3Q4 = value;
                                HANList3.reactivePowerQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 14 ) // kilo Volt Reactive list 3
                            {   // Wh - What per houre 
                                HANList3.activePowerAQ1Q4.activePowerAQ1Q4 = value;
                                HANList3.activePowerAQ1Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 15 ) // kilo Volt Reactive list 3
                            {   // Wh
                                HANList3.activePowerAQ2Q3.activePowerAQ2Q3 = value;
                                HANList3.activePowerAQ2Q3.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 16 ) // kilo Volt Reactive list 3
                            {   // KVArh
                                HANList3.reactivePowerRQ1Q2.reactivePowerRQ1Q2 = value;
                                HANList3.reactivePowerRQ1Q2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 17 ) // kilo Volt Reactive list 3
                            {   // KVArh
                                HANList3.reactivePowerRQ3Q4.reactivePowerRQ3Q4 = value;
                                HANList3.reactivePowerRQ3Q4.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                             else
                            {   // Some HAN data not treated correctly
                                Console.WriteLine("\nError or one unhandled OBIS? legalObisCodesIndex = {0} at {1}",legalObisCodesIndex,dateTimeString);
                                Console.WriteLine("Energi consumption type = {0}, value= {1} and UoM= {2}",oBISdata[cOSEMIndex],value,legalObisCodes[legalObisCodesIndex].UoM);
                            }

                            if(LLogOBIS) Console.WriteLine("{0:0.00} {1}", currentEnergy, legalObisCodes[legalObisCodesIndex].UoM);
                            cOSEMIndex += 11; // Next COSEM block position
                        } 
                        else if ( oBISdata[cOSEMIndex] == TYPE_INT16_0x10 )
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
                            currentAmpere =  Math.Round(((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].scale, 2);
                            value =  (ushort) Math.Round(((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].scale, 2);

                            if ( legalObisCodesIndex == 07 ) // A
                            {
                                HANList2.ampereIL1.ampereIL1 = value;
                                HANList2.ampereIL1.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ampereIL1.ampereIL1 = value;
                                HANList3.ampereIL1.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 08 ) // A
                            {
                                HANList2.ampereIL2.ampereIL2 = value;
                                HANList2.ampereIL2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ampereIL2.ampereIL2 = value;
                                HANList3.ampereIL2.UoM = legalObisCodes[legalObisCodesIndex].UoM;
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

                            if(LLogOBIS) Console.WriteLine("{0:0.00} {1}", currentAmpere, legalObisCodes[legalObisCodesIndex].UoM);
                            cOSEMIndex += 9; // Next COSEM block position
                        }
                        else if ( oBISdata[cOSEMIndex] == TYPE_UINT16_0x12 )
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
                            currentVolte = (int) (((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].scale);
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
                            if(LLogOBIS) Console.WriteLine("{0:0.00} {1}", currentVolte, legalObisCodes[legalObisCodesIndex].UoM);
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

            if( HANList1.versionIdentifier != "" || HANList2.versionIdentifier != "" || HANList3.versionIdentifier != "")
            {
                // We need the Compressed Jason in most cases for display or database REST point/CRUD storage

                if ( numberOfObjects == List1Objects ) // List1
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList1);
                    apiAdressToEndpoint = OOuCP.uCP.HANOODefaultParameters.HANApiEndPoint + "/List1";
                    // Console.WriteLine("\nList 1:\n{0}",jSONstringCompressed);
                    if ( OOuCP.uCP.HANOODefaultParameters.lJsonToApi && OOuCP.uCP.HANOODefaultParameters.lList1 )
                    {
                        Console.WriteLine("Posting to:{0}\nwith this data:\n{1}",apiAdressToEndpoint,jSONstringCompressed);
                        await sendJsonToEndpoint(jSONstringCompressed, apiAdressToEndpoint, OOuCP.uCP.HANOODefaultParameters.lApi);
                    }
                }
                else if ( numberOfObjects == List2Objects ) // List 2
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList2);
                    apiAdressToEndpoint = OOuCP.uCP.HANOODefaultParameters.HANApiEndPoint + "/List2";
                    // Console.WriteLine("\nList 2:\n{0}",jSONstringCompressed);
                    if ( OOuCP.uCP.HANOODefaultParameters.lJsonToApi && OOuCP.uCP.HANOODefaultParameters.lList2 )
                    {
                        Console.WriteLine("Posting to:{0}\nwith this data:\n{1}",apiAdressToEndpoint,jSONstringCompressed);
                        await sendJsonToEndpoint(jSONstringCompressed, apiAdressToEndpoint, OOuCP.uCP.HANOODefaultParameters.lApi);
                    }
                }
                else if ( numberOfObjects == List3Objects ) // Long list (List3) > 250. One object per hr...
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList3);
                    apiAdressToEndpoint = OOuCP.uCP.HANOODefaultParameters.HANApiEndPoint + "/List3";
                    Console.WriteLine("\nList 3; Houerly log:\n{0}",jSONstringCompressed); // Make sure I have a log of this :-)
                    foreach( byte b in oBISdata) Console.Write("{0:X2} ",b);
                    Console.WriteLine();
                    if ( OOuCP.uCP.HANOODefaultParameters.lJsonToApi && OOuCP.uCP.HANOODefaultParameters.lList3 )
                    {
                        await sendJsonToEndpoint(jSONstringCompressed, apiAdressToEndpoint, OOuCP.uCP.HANOODefaultParameters.lApi);
                    }
                }
                else
                    Console.WriteLine("Unhandled exception in preparing API data to endpoint. Number of objects = {0}",numberOfObjects);

                // Then to schreen
                if ( OOuCP.uCP.HANOODefaultParameters.LogJsonCompressed )
                {
                    Console.WriteLine(jSONstringCompressed);
                }
                else if ( OOuCP.uCP.HANOODefaultParameters.LogJson )
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    if ( numberOfObjects == List1Objects ) // List 1
                        jSONstring = JsonSerializer.Serialize(HANList1, options);
                    else if ( numberOfObjects == List2Objects ) // List 2
                        jSONstring = JsonSerializer.Serialize(HANList2, options);
                    else if ( numberOfObjects == List3Objects ) // List 3
                        jSONstring = JsonSerializer.Serialize(HANList3, options);
                    else
                        Console.WriteLine("Error in LogJson. Number of objects in List ({0}) unhandled.",numberOfObjects);
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
                // HANList1.Dispose(); // Managed by C# 10
                // HANList2.Delete(); // Managed by C# 10
                // HANList3.Delete(); // Managed by C# 10
            }
            else  // No complete LIST to send to endpoint
            {
                Console.WriteLine("Skipping some OBIS blockes. Waiting for text_block (STRUCT_TEXT_0x02={0}) in block position 3",STRUCT_TEXT_0x02);
                Console.WriteLine("-------------------------------------------------------------\n" +
                                "Complete OBIS block found but missing header data for LIST1, LIST2 and LIST3");
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
            // HANList1 = null;
            // HANList1 = null;
            // HANList1 = null;
            return;
        }
    }
}
