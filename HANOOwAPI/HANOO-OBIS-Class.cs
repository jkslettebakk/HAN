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
        private static double currentPower { get; set; } = 0.0; // last updated Energy (or Power) (W,kW,kVAr, kWh etc) value
        private static int    currentVolte { get; set; } = 0; // last updated Volt value
        private static double currentAmpere { get; set; }  = 0.0; // Last updated Ampere value
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

        public class List1Object // HAN List 1 class; Aidon & KAIFA (Not for used by Kamstrup (using List2 & 3 only))
        {
            [Key]
            [Required]
            public Guid HANDataId { get; set; }
            [Required]
            [MaxLength(30)]
            public string DateTimePoll { get; set; } = string.Empty;
            [Required]
            [MaxLength(20)]
            public string VersionIdentifier { get; set; } = string.Empty;
            [Required]
            [MaxLength(40)]
            public string ModelID { get; set; } = string.Empty;
            [Required]
            [MaxLength(10)]
            public string ModelType { get; set; } = string.Empty;
            [Required]
            public double ActivePowerQ1Q4 { get; set; }
            [Required]
            [MaxLength(5)]
            public string UoMQ1Q4 { get; set; } = string.Empty;
        }

        public class List2Object // HAN List 2 class; Aidon, KAIFA & Kamstrup
        {
            [Key]
            [Required]
            public Guid HANDataId { get; set; }
            [Required]
            [MaxLength(30)]
            public string DateTimePoll { get; set; } = string.Empty;
            [Required]
            [MaxLength(20)]
            public string VersionIdentifier { get; set; } = string.Empty;
            [Required]
            [MaxLength(40)]
            public string ModelID { get; set; } = string.Empty;
            [Required]
            [MaxLength(10)]
            public string ModelType { get; set; } = string.Empty;
                [Required]
                public double ActivePowerQ1Q4 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMQ1Q4 { get; set; } = string.Empty;
                [Required]
                public double ActivePowerQ2Q3 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMQ2Q3 { get; set; } = string.Empty;
                [Required]
                public double ReactivePowerQ1Q2 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMQ1Q2 { get; set; } = string.Empty;
                [Required]
                public double ReactivePowerQ3Q4 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMQ3Q4 { get; set; } = string.Empty;
                [Required]
                public double AmpereIL1 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMIL1 { get; set; } = string.Empty;
                [Required]
                public double AmpereIL2 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMIL2 { get; set; } = string.Empty;
                [Required]
                public double AmpereIL3 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMIL3 { get; set; } = string.Empty;
                [Required]
                public int VoltUL1 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMUL1 { get; set; } = string.Empty;
                [Required]
                public int VoltUL2 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMUL2 { get; set; } = string.Empty;
                [Required]
                public int VoltUL3 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMUL3 { get; set; } = string.Empty;
        }

        public class List3Object // HAN List 3 class; ; Aidon, KAIFA & Kamstrup
        {
            [Key]
            [Required]
            public Guid HANDataId { get; set; }
            [Required]
            [MaxLength(30)]
            public string DateTimePoll { get; set; } = string.Empty;
            [Required]
            [MaxLength(20)]
            public string VersionIdentifier { get; set; } = string.Empty;
            [Required]
            [MaxLength(40)]
            public string ModelID { get; set; } = string.Empty;
            [Required]
            [MaxLength(10)]
            public string ModelType { get; set; } = string.Empty;
                [Required]
                [MaxLength(40)]
                public string MeeterTime { get; set; } = string.Empty;
                [Required]
                public double ActivePowerQ1Q4 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMQ1Q4 { get; set; } = string.Empty;
                [Required]
                public double ActivePowerQ2Q3 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMQ2Q3 { get; set; } = string.Empty;
                [Required]
                public double ReactivePowerQ1Q2 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMQ1Q2 { get; set; } = string.Empty;
                [Required]
                public double ReactivePowerQ3Q4 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMQ3Q4 { get; set; } = string.Empty;
                [Required]
                public double AmpereIL1 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMIL1 { get; set; } = string.Empty;
                [Required]
                public double AmpereIL2 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMIL2 { get; set; } = string.Empty;
                [Required]
                public double AmpereIL3 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMIL3 { get; set; } = string.Empty;
                [Required]
                public int VoltUL1 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMUL1 { get; set; } = string.Empty;
                [Required]
                public int VoltUL2 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMUL2 { get; set; } = string.Empty;
                [Required]
                public int VoltUL3 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMUL3 { get; set; } = string.Empty;
                [Required]
                public double ActivePowerAQ1Q4 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMAQ1Q4 { get; set; } = string.Empty;
                [Required]
                public double ActivePowerAQ2Q3 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMAQ2Q3 { get; set; } = string.Empty;
                [Required]
                public double ReactivePowerRQ1Q2 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMRQ1Q2 { get; set; } = string.Empty;
                [Required]
                public double ReactivePowerRQ3Q4 { get; set; }
                [Required]
                [MaxLength(5)]
                public string UoMRQ3Q4 { get; set; } = string.Empty;
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
            public double resolution { get; set; }
            public double format { get; set; }
            public string obis { get; set; }
            public string obisType { get; set; }
            public string objectName { set; get; }
            public string vendorObject { set; get; }
            public string HAN_Vendor { get; set; }
        }

        //a obis struct list
        private protected static List<obisCodesStruct> legalObisCodes = new List<obisCodesStruct>
        {
            new obisCodesStruct{A=1,B=0,C=1, D=7,E=0,  F=255,obis="1.0.1.7.0.255",  UoM= "kW",   format=4.3,resolution=0.0010,objectName="Q1Q4",              vendorObject="Q1+Q4",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=1,C= 0,D=2,E=129,F=255,obis="1.1.0.2.129.255",UoM= " ",    format=0.0,resolution=1.0000,objectName="Version identifier",vendorObject="Version identifier",HAN_Vendor ="Aidon & Kamstrup"},
            new obisCodesStruct{A=0,B=0,C=96,D=1,E=  0,F=255,obis="0.0.96.1.0.255", UoM= " ",    format=0.0,resolution=1.0000,objectName="Model ID",          vendorObject="Model ID",          HAN_Vendor ="Aidon"}, 
            new obisCodesStruct{A=0,B=0,C=96,D=1,E=  7,F=255,obis="0.0.96.1.7.255", UoM= " ",    format=0.0,resolution=1.0000,objectName="Model type",        vendorObject="Model type",        HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 2,D=7,E=  0,F=255,obis="1.0.2.7.0.255",  UoM= "kW",   format=4.3,resolution=0.0010,objectName="Q2Q3",              vendorObject="Q2+Q3",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 3,D=7,E=  0,F=255,obis="1.0.3.7.0.255",  UoM= "kVAr", format=4.3,resolution=0.0010,objectName="Q1Q2",              vendorObject="Q1+Q2",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 4,D=7,E=  0,F=255,obis="1.0.4.7.0.255",  UoM= "kVAr", format=4.3,resolution=0.0010,objectName="Q3Q4",              vendorObject="Q3+Q4",             HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=31,D=7,E=  0,F=255,obis="1.0.31.7.0.255", UoM= "A",    format=0.1,resolution=0.1000,objectName="IL1",               vendorObject="IL1",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=51,D=7,E=  0,F=255,obis="1.0.51.7.0.255", UoM= "A",    format=0.1,resolution=0.1000,objectName="IL2",               vendorObject="IL2",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=71,D=7,E=  0,F=255,obis="1.0.71.7.0.255", UoM= "A",    format=0.1,resolution=0.1000,objectName="IL3",               vendorObject="IL3",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=32,D=7,E=  0,F=255,obis="1.0.32.7.0.255", UoM= "V",    format=0.1,resolution=0.1000,objectName="UL1",               vendorObject="UL1",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=52,D=7,E=  0,F=255,obis="1.0.52.7.0.255", UoM= "V",    format=0.1,resolution=0.1000,objectName="UL2",               vendorObject="UL2",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C=72,D=7,E=  0,F=255,obis="1.0.72.7.0.255", UoM= "V",    format=0.1,resolution=0.1000,objectName="UL3",               vendorObject="UL3",               HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=0,B=0,C= 1,D=0,E=  0,F=255,obis="0.0.1.0.0.255",  UoM= " ",    format=1.0,resolution=1.0000,objectName="Clock and Date",    vendorObject="Clock and Date",    HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 1,D=8,E=  0,F=255,obis="1.0.1.8.0.255",  UoM= "kWh",  format=0.1,resolution=0.0100,objectName="AQ1Q4",             vendorObject="A+Q1+Q4",           HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 2,D=8,E=  0,F=255,obis="1.0.2.8.0.255",  UoM= "kWh",  format=0.1,resolution=0.0100,objectName="AQ2Q3",             vendorObject="A-Q2+Q3",           HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 3,D=8,E=  0,F=255,obis="1.0.3.8.0.255",  UoM= "kVArh",format=0.1,resolution=0.0100,objectName="RQ1Q2",             vendorObject="R+Q1+Q2",           HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=0,C= 4,D=8,E=  0,F=255,obis="1.0.4.8.0.255",  UoM= "kVArh",format=0.1,resolution=0.0100,objectName="RQ3Q4",             vendorObject="R-Q3+Q4",           HAN_Vendor ="Aidon"},
            new obisCodesStruct{A=1,B=1,C= 0,D=0,E=  5,F=255,obis="1.1.0.0.5.255",  UoM= " ",    format=1.0,resolution=1.0000,objectName="GS1 number",        vendorObject="GS1 number",        HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 1,D=7,E=  0,F=255,obis="1.1.1.7.0.255",  UoM= "W",    format=0.1,resolution=0.1000,objectName="P14",               vendorObject="P14",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 2,D=7,E=  0,F=255,obis="1.1.2.7.0.255",  UoM= "W",    format=0.1,resolution=0.1000,objectName="P23",               vendorObject="P23",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=31,D=7,E=  0,F=255,obis="1.1.31.7.0.255", UoM= "A",    format=0.0,resolution=0.0010,objectName="IL1",               vendorObject="IL1",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=51,D=7,E=  0,F=255,obis="1.1.51.7.0.255", UoM= "A",    format=0.0,resolution=0.0010,objectName="IL2",               vendorObject="IL2",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=71,D=7,E=  0,F=255,obis="1.1.71.7.0.255", UoM= "A",    format=0.0,resolution=0.0010,objectName="IL3",               vendorObject="IL3",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=32,D=7,E=  0,F=255,obis="1.1.32.7.0.255", UoM= "V",    format=0.1,resolution=0.1000,objectName="UL1",               vendorObject="UL1",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=52,D=7,E=  0,F=255,obis="1.1.52.7.0.255", UoM= "V",    format=0.1,resolution=0.1000,objectName="UL2",               vendorObject="UL2",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=72,D=7,E=  0,F=255,obis="1.1.72.7.0.255", UoM= "V",    format=0.1,resolution=0.1000,objectName="UL3",               vendorObject="UL3",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=0,B=1,C= 1,D=0,E=  0,F=255,obis="0.1.1.0.0.255",  UoM= " ",    format=1.0,resolution=1.0000,objectName="RTC",               vendorObject="RTC",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 1,D=8,E=  0,F=255,obis="1.1.1.8.0.255",  UoM= "Wh",   format=0.1,resolution=0.1000,objectName="A14",               vendorObject="A14",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 2,D=8,E=  0,F=255,obis="1.1.2.8.0.255",  UoM= "Wh",   format=0.1,resolution=0.1000,objectName="A23",               vendorObject="A23",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C=96,D=1,E=  1,F=255,obis="1.1.96.1.1.255", UoM= " ",    format=1.0,resolution=1.0000,objectName="Meter type",        vendorObject="Meter type",        HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 3,D=7,E=  0,F=255,obis="1.1.3.7.0.255",  UoM= "Var",  format=1.0,resolution=1.0000,objectName="Q12",               vendorObject="Q12",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 4,D=7,E=  0,F=255,obis="1.1.4.7.0.255",  UoM= "Var",  format=1.0,resolution=1.0000,objectName="Q34",               vendorObject="Q34",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 3,D=8,E=  0,F=255,obis="1.1.3.8.0.255",  UoM= "Varh", format=1.0,resolution=1.0000,objectName="R12",               vendorObject="R12",               HAN_Vendor ="Kamstrup"},
            new obisCodesStruct{A=1,B=1,C= 4,D=8,E=  0,F=255,obis="1.1.4.8.0.255",  UoM= "Varh", format=1.0,resolution=1.0000,objectName="R34",               vendorObject="R34",               HAN_Vendor ="Kamstrup"}
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
        
        // API functions
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
            PostAsyncLoops++;
            // public abstract Microsoft.AspNetCore.Http.IResponseCookies Cookies { get; };

            if ( logApi )
            {
                Console.WriteLine("{0}\tStatus Posting to {1} = {2}",PostAsyncLoops,UriString,result.StatusCode);
            }
            if ( !result.IsSuccessStatusCode )
            {
                string json = await result.Content.ReadAsStringAsync();

                // Data from the "result" object
                Console.WriteLine("{0}\tStatus etter PostAsync to {1} with (result.StatusCode)={2}",PostAsyncLoops,UriString,result.StatusCode);
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
                Console.WriteLine("Running \"result.Dispose()\".");
                // 
                result.Dispose();
                // 
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

            // Instansiate the objects..... For small footpring devices I could instansiated just the needed by using "numberOfObjects"
            List1Object HANList1 = new List1Object();
            HANList1.DateTimePoll = dateTimeString;

            List2Object HANList2 = new List2Object();
            HANList2.DateTimePoll = dateTimeString;

            List3Object HANList3 = new List3Object();
            HANList3.DateTimePoll = dateTimeString;

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

                        // prosess string from OBIS byte array
                        string text = string.Empty;
                        for (int j=0; j<oBISdata[cOSEMIndex]; j++) text += Convert.ToChar(oBISdata[cOSEMIndex + 1 + j]);
                            if(LLogOBIS) Console.WriteLine("{0}",text);
                        if ( (legalObisCodesIndex == 01) && (text.Length > 0 ) ) // Meeter Model type
                        { 
                            catchVersionidentifier = text; 
                            HANList1.VersionIdentifier = catchVersionidentifier;
                            HANList2.VersionIdentifier = catchVersionidentifier;
                            HANList3.VersionIdentifier = catchVersionidentifier;
                        } else if ( (legalObisCodesIndex == 02) && (text.Length > 0 ) ) // Meeter Model type
                        { 
                            catchModelID = text; 
                            HANList1.ModelID = catchModelID;                            
                            HANList2.ModelID = catchModelID;                            
                            HANList3.ModelID = catchModelID;                            
                        } else if ( (legalObisCodesIndex == 03) && (text.Length > 0 ) )  // Meeter Model type
                        { 
                            catchModelType = text; 
                            HANList1.ModelType = catchModelType;
                            HANList2.ModelType = catchModelType;
                            HANList3.ModelType = catchModelType;
                        } else if ( legalObisCodesIndex == 13 && (text.Length > 0 ) ) // Time and Date in meeter
                        {
                            HANList3.MeeterTime = dateTime.ToString("O");
                            if ( oBISdata.Length <= cOSEMIndex + 8 ) {

                            HANList3.MeeterTime = ((int)    ((oBISdata[cOSEMIndex+1]<<8)+(oBISdata[cOSEMIndex+2]))).ToString("D4") + "-" +
                                                            ((int) (oBISdata[cOSEMIndex+3])).ToString("D2") + "-" +
                                                            ((int) (oBISdata[cOSEMIndex+4])).ToString("D2") + "T" +
                                                            ((int) (oBISdata[cOSEMIndex+6])).ToString("D2") + ":" +
                                                            ((int) (oBISdata[cOSEMIndex+7])).ToString("D2") + ":" +
                                                            ((int) (oBISdata[cOSEMIndex+8])).ToString();
                            } else
                            HANList3.MeeterTime = dateTime.ToString("O");
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

                        // Set Json heading data to comblete List1, List2 and List3 Json block
                        if ( catchVersionidentifier.Length > 0 )
                        {
                            HANList1.VersionIdentifier = catchVersionidentifier;
                            HANList2.VersionIdentifier = catchVersionidentifier;
                            HANList3.VersionIdentifier = catchVersionidentifier;
                        }
                        if ( catchModelID.Length > 0 )
                        {
                            HANList1.ModelID = catchModelID;
                            HANList2.ModelID = catchModelID;
                            HANList3.ModelID = catchModelID;
                        }
                        if ( catchModelType.Length > 0 )
                        {
                            HANList1.ModelType = catchModelType;
                            HANList2.ModelType = catchModelType;
                            HANList3.ModelType = catchModelType;
                        }

                        // Data block to be prosessed
                        cOSEMIndex = cOSEMIndex + 4 + oBISLength;

                        if(LLogOBIS) Console.WriteLine("Start Data at cOSEMIndex = {0}, value = {1:X2} ",cOSEMIndex, oBISdata[cOSEMIndex]);

                        if ( oBISdata[cOSEMIndex] == TYPE_UINT32_0x06 )
                        {
                            // Power/Energy values (W og kW)
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
                                Console.WriteLine("Error. Index to long. cOSEMIndex+4={0}, oBISdata.length={1}, OBIS code {2}",cOSEMIndex+4, oBISdata.Length,legalObisCodes[legalObisCodesIndex].obis);
                                Console.WriteLine("Will return to DMLS for next OBIS block. Dumping problem OBIS block... I need to change the DLMS rotine, but not now...");
                                foreach( byte b in oBISdata ) Console.Write("{0:X2} ",b);                               
                                Console.WriteLine();
                                return;
                            }
                            currentPower = Math.Round(((((oBISdata[cOSEMIndex+1]<<24)+oBISdata[cOSEMIndex+2]<<16)+oBISdata[cOSEMIndex+3]<<8)+oBISdata[cOSEMIndex+4]<<0)*legalObisCodes[legalObisCodesIndex].resolution,3);

                            if ( legalObisCodesIndex == 00 ) // Powercollection list 1, 2 and 3; Kamstrup will never be updated for List 1
                            {  // Wat
                                HANList1.ActivePowerQ1Q4 = currentPower;
                                HANList1.UoMQ1Q4 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList2.ActivePowerQ1Q4 = currentPower;
                                HANList2.UoMQ1Q4 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ActivePowerQ1Q4 = currentPower;
                                HANList3.UoMQ1Q4 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 04 ) // Powercollection list 2 and 3
                            {// Watt
                                HANList2.ActivePowerQ2Q3 = currentPower;
                                HANList2.UoMQ2Q3 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ActivePowerQ2Q3 = currentPower;
                                HANList3.UoMQ2Q3 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 05 ) // kilo Volt Reactive list 2 and 3
                            {// KVAr
                                HANList2.ReactivePowerQ1Q2 = currentPower;
                                HANList2.UoMQ1Q2 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ReactivePowerQ1Q2 = currentPower;
                                HANList3.UoMQ1Q2 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 06 ) // kilo Volt Reactive list 2 and 3
                            {// KVAr
                                HANList2.ReactivePowerQ3Q4 = currentPower;
                                HANList2.UoMQ3Q4 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ReactivePowerQ3Q4 = currentPower;
                                HANList3.UoMQ3Q4 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 14 ) // kilo Volt Reactive list 3
                            {   // Wh - What per houre 
                                HANList3.ActivePowerAQ1Q4 = currentPower;
                                HANList3.UoMAQ1Q4 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 15 ) // kilo Volt Reactive list 3
                            {   // Wh
                                HANList3.ActivePowerAQ2Q3 = currentPower;
                                HANList3.UoMAQ2Q3 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 16 ) // kilo Volt Reactive list 3
                            {   // KVArh
                                HANList3.ReactivePowerRQ1Q2 = currentPower;
                                HANList3.UoMRQ1Q2 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 17 ) // kilo Volt Reactive list 3
                            {   // KVArh
                                HANList3.ReactivePowerRQ3Q4 = currentPower;
                                HANList3.UoMRQ3Q4 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                             else
                            {   // Some HAN data not treated correctly
                                Console.WriteLine("\nError or one unhandled OBIS? legalObisCodesIndex = {0} at {1}",legalObisCodesIndex,dateTimeString);
                                Console.WriteLine("Energi consumption type = {0}, value= {1} and UoM= {2}",oBISdata[cOSEMIndex],currentPower,legalObisCodes[legalObisCodesIndex].UoM);
                            }

                            if(LLogOBIS) Console.WriteLine("{0:0.00} {1}", currentPower, legalObisCodes[legalObisCodesIndex].UoM);
                            cOSEMIndex += 11; // Next COSEM block position
                        } 
                        else if ( oBISdata[cOSEMIndex] == TYPE_INT16_0x10 )
                        { // Current (A)
                            if(LLogOBIS) Console.WriteLine("Found TYPE_INT16_0x10 (1 byte) + 2 byte data + 02 02 + UoM?? (4 byte) == 9 bytes");
                            if ( oBISdata.Length > cOSEMIndex + 9 + 2) structType = oBISdata[cOSEMIndex + 9 + 1];
                            if(LLogOBIS) Console.Write("{0} has {1}=",legalObisCodes[legalObisCodesIndex].obis,legalObisCodes[legalObisCodesIndex].objectName);

                            if(LLogOBIS)
                            {
                                for ( int j=0; j< 9; j++) Console.Write("{0:X2} ",oBISdata[cOSEMIndex+j]);
                                Console.Write("({0}) ",legalObisCodes[legalObisCodesIndex].UoM);
                            }
                            currentAmpere =  Math.Round(((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].resolution, 1);

                            if ( legalObisCodesIndex == 07 ) // A
                            {
                                HANList2.AmpereIL1 = currentAmpere;
                                HANList2.UoMIL1 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.AmpereIL1 = currentAmpere;
                                HANList3.UoMIL1 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 08 ) // A
                            {
                                HANList2.AmpereIL2 = currentAmpere;
                                HANList2.UoMIL2 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.AmpereIL2 = currentAmpere;
                                HANList3.UoMIL2 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 09 ) // A
                            {
                                HANList2.AmpereIL3 = currentAmpere;
                                HANList2.UoMIL3 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.AmpereIL3 = currentAmpere;
                                HANList3.UoMIL3 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                             else
                            {   // Some HAN data not treated correctly
                                Console.WriteLine("\nError or one houre OBIS code? legalObisCodesIndex = {0} at {1}",legalObisCodesIndex,dateTimeString);
                                Console.WriteLine("Currency consumption type = {0}, value= {1} and UoM= {2}",oBISdata[cOSEMIndex],currentAmpere,legalObisCodes[legalObisCodesIndex].UoM);
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
                            currentVolte = (int) (((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].resolution);
                            value = (ushort) (((oBISdata[cOSEMIndex+1]<<8)+oBISdata[cOSEMIndex+2]<<0)*legalObisCodes[legalObisCodesIndex].resolution);

                            if ( legalObisCodesIndex == 05 ) // KVAr
                            {
                                // HANList2.ReactivePowerQ1Q2 = value;
                                // HANList2.UoMRQ1Q2 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ReactivePowerQ1Q2 = currentPower;
                                HANList3.UoMRQ1Q2 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 06 ) // KVAr
                            {
                                // HANList2.ReactivePowerQ3Q4 = value;
                                // HANList2.UoMRQ3Q4 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.ReactivePowerQ3Q4 = currentAmpere;
                                HANList3.UoMRQ3Q4 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 10 ) // V
                            {
                                HANList2.VoltUL1 = currentVolte;
                                HANList2.UoMUL1 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.VoltUL1 = currentVolte;
                                HANList3.UoMUL1 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 11 ) // V
                            {
                                HANList2.VoltUL2 = currentVolte;
                                HANList2.UoMUL2 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.VoltUL2 = currentVolte;
                                HANList3.UoMUL2 = legalObisCodes[legalObisCodesIndex].UoM;
                            }
                            else if ( legalObisCodesIndex == 12 ) // V
                            {
                                HANList2.VoltUL3 = currentVolte;
                                HANList2.UoMUL3 = legalObisCodes[legalObisCodesIndex].UoM;
                                HANList3.VoltUL3 = currentVolte;
                                HANList3.UoMUL3 = legalObisCodes[legalObisCodesIndex].UoM;
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

            if( !String.IsNullOrEmpty(HANList1.VersionIdentifier) || !String.IsNullOrEmpty(HANList2.VersionIdentifier) || !String.IsNullOrEmpty(HANList3.VersionIdentifier))
            {
                // We need the Compressed Jason in most cases for display or database REST point/CRUD storage

                if ( numberOfObjects == List1Objects ) // List1Object
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList1);
                    apiAdressToEndpoint = OOuCP.uCP.HANOODefaultParameters.HANApiEndPoint + "/List1";
                    // Console.WriteLine("\nList 1:\n{0}",jSONstringCompressed);
                    if ( OOuCP.uCP.HANOODefaultParameters.lJsonToApi && OOuCP.uCP.HANOODefaultParameters.lList1 )
                    {
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
                        await sendJsonToEndpoint(jSONstringCompressed, apiAdressToEndpoint, OOuCP.uCP.HANOODefaultParameters.lApi);
                    }
                }
                else if ( numberOfObjects == List3Objects ) // Long list (List3Object) > 250. One object per hr...
                {
                    jSONstringCompressed = JsonSerializer.Serialize(HANList3);
                    apiAdressToEndpoint = OOuCP.uCP.HANOODefaultParameters.HANApiEndPoint + "/List3";
                    Console.WriteLine("\nList 3; Houerly log:\n{0}",jSONstringCompressed); // Make sure I have a log of this :-)
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
            // HANList2 = null;
            // HANList3 = null;
            return;
        }
    }
}
