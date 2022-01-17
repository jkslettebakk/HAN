
namespace HANOOUserConfigurationParameters
{
    public class OOUserConfigurationParameters
    {

        private string jSONfileName = "HANOO-Parameters.json";

        public class HANOODetails
        {
            [JsonPropertyName("Comment")]
            public string Comment { get; set; }

            [JsonPropertyName("Programmer")]
            public string Programmer { get; set; }

            [JsonPropertyName("Phone")]
            public string Phone { get; set; }

            [JsonPropertyName("Street")]
            public string Street { get; set; }

            [JsonPropertyName("PostNumber")]
            public string PostNumber { get; set; }

            [JsonPropertyName("PostName")]
            public string PostName { get; set; }
        }

        public class HANOODeviceData
        {
            [JsonPropertyName("Comment")]
            public string Comment { get; set; }

            [JsonPropertyName("HANDeviceName")]
            public string HANDeviceName { get; set; }

            [JsonPropertyName("serialPortName")]
            public string serialPortName { get; set; }

            [JsonPropertyName("serialPortBaud")]
            public int serialPortBaud { get; set; }

            [JsonPropertyName("serialPortParity")]
            public Parity serialPortParity { get; set; }

            [JsonPropertyName("serialPortDataBits")]
            public int serialPortDataBits { get; set; }

            [JsonPropertyName("serialPortStopBits")]
            public StopBits serialPortStopBits { get; set; }

            [JsonPropertyName("delayBetweenReads")]
            public int delayBetweenReads { get; set; }
        }

        public class HANOOParameters
        {
            [JsonPropertyName("Comment")]
            public string Comment { get; set; }

            [JsonPropertyName("DefaultParameterFile")]
            public string DefaultParameterFile { get; set; }

            [JsonPropertyName("--h")]
            public string H { get; set; }

            [JsonPropertyName("--l")]
            public string L { get; set; }

            [JsonPropertyName("--lDLMS")]
            public string lDLMS { get; set; }

            [JsonPropertyName("--lCOSEM")]
            public string lCOSEM { get; set; }

            [JsonPropertyName("--lOBIS")]
            public string lOBIS { get; set; }

            [JsonPropertyName("--lCRC")]
            public string lCRC { get; set; }

            [JsonPropertyName("--lJson")]
            public string lJson { get; set; }

            [JsonPropertyName("--lJsonCompressed")]
            public string lJsonCompressed { get; set; }

            [JsonPropertyName("--pn <value>")]
            public string pn { get; set; }

            [JsonPropertyName("--pb <value>")]
            public string pb { get; set; }

            [JsonPropertyName("--delay <value>")]
            public string delay { get; set; }

            [JsonPropertyName("--HANApiEndPoint <value>")]
            public string HANApiEndPoint { get; set; }

            [JsonPropertyName("--JsonToApi <value>")]
            public string lJsonToApi { get; set; }

            [JsonPropertyName("--lList1 <value>")]
            public string lList1 { get; set; }

            [JsonPropertyName("--lList2 <value>")]
            public string lList2 { get; set; }

            [JsonPropertyName("--lList3 <value>")]
            public string lList3 { get; set; }

            [JsonPropertyName("--lApi <value>")]
            public string lApi { get; set; }
        }

        public class HANOODefaultParameters
        {
            [JsonPropertyName("Comment")]
            public string Comment { get; set; }

            [JsonPropertyName("help")]
            public bool help { get; set; }

            [JsonPropertyName("Log")]
            public bool Log { get; set; }

            [JsonPropertyName("LogUserConfig")]
            public bool LogUserConfig { get; set; }

            [JsonPropertyName("LogDLMS")]
            public bool LogDLMS { get; set; }

            [JsonPropertyName("LogCOSEM")]
            public bool LogCOSEM { get; set; }

            [JsonPropertyName("LogOBIS")]
            public bool LogOBIS { get; set; }

            [JsonPropertyName("LogCRC")]
            public bool LogCRC { get; set; }

            [JsonPropertyName("LogJson")]
            public bool LogJson { get; set; }

            [JsonPropertyName("LogJsonCompressed")]
            public bool LogJsonCompressed { get; set; }

            [JsonPropertyName("HANApiEndPoint")]
            public string HANApiEndPoint { get; set; }

            [JsonPropertyName("lJsonToApi")]
            public bool lJsonToApi { get; set; }

            [JsonPropertyName("lList1")]
            public bool lList1 { get; set; }

            [JsonPropertyName("lList2")]
            public bool lList2 { get; set; }

            [JsonPropertyName("lList3")]
            public bool lList3 { get; set; }            

            [JsonPropertyName("lApi")]
            public bool lApi { get; set; }            
        }

        public class UserConfigurationParameters // Setting it all together in an object grouping
        {
            [JsonPropertyName("Comment")]
            public string Comment { get; set; }

            [JsonPropertyName("HANOODetails")]
            public HANOODetails HANOODetails { get; set; }

            [JsonPropertyName("HANOODeviceData")]
            public HANOODeviceData HANOODeviceData { get; set; }

            [JsonPropertyName("HANOODefaultParameters")]
            public HANOODefaultParameters HANOODefaultParameters { get; set; }

            [JsonPropertyName("HANOOParameters")]
            public HANOOParameters HANOOParameters { get; set; }
        }

        public UserConfigurationParameters uCP = new UserConfigurationParameters();

        public OOUserConfigurationParameters()
        {
            // UserConfigurationParameters uCP = new UserConfigurationParameters();
            uCP = loadConfigFile();
            if (uCP.HANOODefaultParameters.Log) Console.WriteLine("Initialising/preparing \"OOUserConfigurationParameters\" objects and user data.");
            if (uCP.HANOODefaultParameters.LogUserConfig) displayParameters( uCP );
        }

        public void displayParameters( UserConfigurationParameters parameters )
        {
            Console.WriteLine("userConfigurationParameters.Comment: {0}",parameters.Comment);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("parameters.HANOODetails.Comment: {0}",parameters.HANOODetails.Comment);
            Console.WriteLine("parameters.HANOODetails.Programmer: {0}",parameters.HANOODetails.Programmer);
            Console.WriteLine("parameters.HANOODetails.Phone: {0}",parameters.HANOODetails.Phone);
            Console.WriteLine("parameters.HANOODetails.Street: {0}",parameters.HANOODetails.Street);
            Console.WriteLine("parameters.HANOODetails.PostNumber & details.HANOODetails.PostName: {0} {1}", parameters.HANOODetails.PostNumber, parameters.HANOODetails.PostName );
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("parameters.HANOODeviceData.Comment: {0}",parameters.HANOODeviceData.Comment);
            Console.WriteLine("parameters.HANOODeviceData.HANDeviceName: {0}",parameters.HANOODeviceData.HANDeviceName);
            Console.WriteLine("parameters.HANOODeviceData.serialPortName: {0}",parameters.HANOODeviceData.serialPortName);
            Console.WriteLine("parameters.HANOODeviceData.serialPortBaud: {0}",parameters.HANOODeviceData.serialPortBaud);
            Console.WriteLine("parameters.HANOODeviceData.serialPortParity: {0}",parameters.HANOODeviceData.serialPortParity);
            Console.WriteLine("parameters.HANOODeviceData.serialPortDataBits: {0}",parameters.HANOODeviceData.serialPortDataBits);
            Console.WriteLine("parameters.HANOODeviceData.serialPortStopBits: {0}",parameters.HANOODeviceData.serialPortStopBits);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("parameters.HANOODefaultParameters.Comment: {0}",parameters.HANOODefaultParameters.Comment);
            Console.WriteLine("parameters.HANOODefaultParameters.Log: {0}",parameters.HANOODefaultParameters.Log);
            Console.WriteLine("parameters.HANOODefaultParameters.LogUserConfig: {0}",parameters.HANOODefaultParameters.LogUserConfig);
            Console.WriteLine("parameters.HANOODefaultParameters.LogDLMS: {0}",parameters.HANOODefaultParameters.LogDLMS);
            Console.WriteLine("parameters.HANOODefaultParameters.LogCOSEM: {0}",parameters.HANOODefaultParameters.LogCOSEM);
            Console.WriteLine("parameters.HANOODefaultParameters.LogOBIS: {0}",parameters.HANOODefaultParameters.LogOBIS);
            Console.WriteLine("parameters.HANOODefaultParameters.LogJson: {0}",parameters.HANOODefaultParameters.LogJson);
            Console.WriteLine("parameters.HANOODefaultParameters.LogJsonCompressed: {0}",parameters.HANOODefaultParameters.LogJsonCompressed);
            Console.WriteLine("parameters.HANOODefaultParameters.HANApiEndPoint: {0}",parameters.HANOODefaultParameters.HANApiEndPoint);
            Console.WriteLine("parameters.HANOODefaultParameters.lList1: {0}",parameters.HANOODefaultParameters.lList1);
            Console.WriteLine("parameters.HANOODefaultParameters.lList2: {0}",parameters.HANOODefaultParameters.lList2);
            Console.WriteLine("parameters.HANOODefaultParameters.lList3: {0}",parameters.HANOODefaultParameters.lList3);
            Console.WriteLine("parameters.HANOODefaultParameters.lApi: {0}",parameters.HANOODefaultParameters.lApi);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("parameters.HANOOParameters.Comment: {0}",parameters.HANOOParameters.Comment);
            Console.WriteLine("parameters.HANOOParameters.DefaultParameterFile: {0}",parameters.HANOOParameters.DefaultParameterFile);
            Console.WriteLine("parameters.HANOOParameters.H: {0}",parameters.HANOOParameters.H);
            Console.WriteLine("parameters.HANOOParameters.L: {0}",parameters.HANOOParameters.L);
            Console.WriteLine("parameters.HANOOParameters.pn: {0}",parameters.HANOOParameters.pn);
            Console.WriteLine("parameters.HANOOParameters.pb: {0}",parameters.HANOOParameters.pb);
            Console.WriteLine("parameters.HANOOParameters.lDLMS: {0}",parameters.HANOOParameters.lDLMS);
            Console.WriteLine("parameters.HANOOParameters.lCOSEM: {0}",parameters.HANOOParameters.lCOSEM);
            Console.WriteLine("parameters.HANOOParameters.lOBIS: {0}",parameters.HANOOParameters.lOBIS);
            Console.WriteLine("parameters.HANOOParameters.lJson: {0}",parameters.HANOOParameters.lJson);
            Console.WriteLine("parameters.HANOOParameters.lJsonCompressed: {0}",parameters.HANOOParameters.lJsonCompressed);
            Console.WriteLine("parameters.HANOOParameters.lJsonToApi: {0}",parameters.HANOOParameters.lJsonToApi);            
            Console.WriteLine("--------------------------------------");
        } 

        public UserConfigurationParameters loadConfigFile()
        {

            // Open and analyse config (HAN-OO.config) file
            // string jSONfileName = "HAN-OO-Parameters.json";
            string jSONString;

            // Read JSON file data to string
            try
            {
                jSONString = File.ReadAllText(jSONfileName);                 
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Error reading parameter file {0}",jSONfileName);
                Console.WriteLine("Exception, file not found:\n{0}",ex.FileName);
                Environment.Exit(-1);
                throw;
            }
            catch (System.Exception)
            {
                throw;
            }
            // Console.WriteLine("file conten :\n{0}",jSONString);

            // UserConfigurationParameters userConfigurationParameters = await JsonSerializer.Deserialize<UserConfigurationParameters>(fs);
            UserConfigurationParameters userConfigurationParameters = JsonSerializer.Deserialize<UserConfigurationParameters>(jSONString);

            return userConfigurationParameters;
        }

        public SerialPort getPortParameters(SerialPort spData )
        {
            spData.PortName = "OOUserConfigurationParameters.HANOODeviceData.serialPortName";
            return spData;
        }

        public SerialPort setSerialPortParameters( OOUserConfigurationParameters OOuCP )
        {
            SerialPort sp = new SerialPort();
            string[] ports = SerialPort.GetPortNames();
            if (OOuCP.uCP.HANOODefaultParameters.Log)
                {
                    Console.WriteLine("Available Ports =");
                    foreach( string port in ports )
                        Console.WriteLine(port);
                }
            if(ports.Any(OOuCP.uCP.HANOODeviceData.serialPortName.Contains))
            {
                if( uCP.HANOODefaultParameters.Log ) Console.WriteLine("Matching ports on system and in configuration file ({0})",OOuCP.uCP.HANOODeviceData.serialPortName);                
                sp.BaudRate = OOuCP.uCP.HANOODeviceData.serialPortBaud;
                sp.DataBits = OOuCP.uCP.HANOODeviceData.serialPortDataBits;
                sp.PortName = OOuCP.uCP.HANOODeviceData.serialPortName;
            }
            else
            {
                Console.WriteLine("Error. System Parameters in JSON does not match hardware ports. Using defaults.");
                Console.WriteLine("Port name in SerialPort data={0}",sp.PortName);
            }
                return sp;
        }

        public void getHANOptions( string[] args, OOUserConfigurationParameters OOuCP )
        {
            // analyse command line input
            bool log = uCP.HANOODefaultParameters.Log;
            bool flag = false;
            int value = 0;
            if (log) Console.WriteLine("****    In getHANOptions    ****\nargs.Length = {0}", args.Length);
            // modifying/override JSON parameters from commandline
            // e.g. String management
            // --p "/dev/ttyUSB0" -> -p=Paramerer "PortName", PortName="/dev/ttyUSB0"
            for ( int i = 0; i < args.Length; i++)
            {
                switch ( args[i] )
                {
                    case "--h":
                        // help();
                        uCP.HANOODefaultParameters.help = true;
                        break;
                    case "--p":
                        displayParameters( OOuCP.uCP );
                        break;
                    case "--br": // Set Baud Rate. --br 2400. Will set serialBaudRate = 2400.
                        if( i+1 < args.Length )
                        {
                            if ( int.TryParse( args[i+1], out value) )
                            {
                                if (log) Console.WriteLine("--br option and value = {0}",args[i+1]);
                                if (log) Console.WriteLine("--br option changed current Baud Baud from {0} to {1}",uCP.HANOODeviceData.serialPortBaud,value);
                                uCP.HANOODeviceData.serialPortBaud = value;
                            }
                            else
                                if (log) Console.WriteLine("--br option and value = {0} is invalid. Skipp value",args[i+1]);
                            i++;
                        }
                        else
                            Console.WriteLine("--br option is invalid. Are you missing a parameter?");
                        break;
                    case "--pn":   // Set Port Name. --pn /dev/ttyUSB3; Will set the serialPortName to /dev/ttyUSB3
                        if( i+1 < args.Length )
                        {
                            if (log)
                                {
                                    Console.WriteLine("--pn option and value = {0}",args[i+1]);
                                    Console.WriteLine("--pn option changed value uCP.HANOODeviceData.serialPortName from {0} to {1}",uCP.HANOODeviceData.serialPortName,args[i+1]);
                                }
                            uCP.HANOODeviceData.serialPortName = args[i+1];
                            i++;
                        }
                        else
                            Console.WriteLine("--pn option is invalid. Are you missing a parameter?");
                        break;
                    case "--HANApiEndPoint":   // Set API Endpoint Name. --HANApiEndPoint https://han.slettebakk.com/api/<list>
                        if( i+1 < args.Length )
                        {
                            if (log)
                                {
                                    Console.WriteLine("--HANApiEndPoint option and value = {0}",args[i+1]);
                                    Console.WriteLine("--HANApiEndPoint option changed value uCP.HANOODeviceData.HANApiEndPoint from {0} to {1}",uCP.HANOODefaultParameters.HANApiEndPoint,args[i+1]);
                                }
                            uCP.HANOODefaultParameters.HANApiEndPoint = args[i+1];
                            i++;
                        }
                        else
                            Console.WriteLine("--HANApiEndPoint option is invalid. Are you missing a parameter?");
                        break;
                    case "--l": // Set log level "false" or "true"
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if ( bool.Parse(args[i+1]) )
                                    {
                                        Console.WriteLine("--l option and value = {0}",args[i+1]);
                                        Console.WriteLine("--l option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.Log,args[i+1]);
                                    }
                                uCP.HANOODefaultParameters.Log = bool.Parse( args[i+1] );
                                i++;
                            }
                            else
                                if (log) Console.WriteLine("--l option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--l option is invalid. Are you missing a parameter?");
                        break;
                    case "--lDLMS": // Set DLMS log level "false" or "true"
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lDLMS option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lDLMS option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.LogDLMS,args[i+1]);
                                }
                                uCP.HANOODefaultParameters.LogDLMS = bool.Parse( args[i+1] );
                                i++;
                            }
                            else
                                if (log) Console.WriteLine("--lDLMS option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lDLMS option is invalid. Are you missing a parameter?");
                        break;
                    case "--lCOSEM": // Set COSEM log level "false" or "true"
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lCOSEM option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lCOSEM option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.LogCOSEM,args[i+1]);
                                }
                                uCP.HANOODefaultParameters.LogCOSEM = bool.Parse( args[i+1] );
                                i++;
                            }
                            else
                                if (log) Console.WriteLine("--lCOSEM option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lCOSEM option is invalid. Are you missing a parameter?");
                        break;
                    case "--lOBIS": // Set OBIS log level "false" or "true"
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lOBIS option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lOBIS option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.LogOBIS, flag);
                                }
                                uCP.HANOODefaultParameters.LogOBIS = flag;
                                i++;
                            }
                            else
                                if (log) Console.WriteLine("--lOBIS option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lOBIS option is invalid. Are you missing a parameter?");
                        break;
                    case "--lCRC": // Set OBIS log level "false" or "true"
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lCRC option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lCRC option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.LogCRC,flag);
                                }
                                uCP.HANOODefaultParameters.LogCRC = flag;
                                i++;
                            }
                            else
                                if (log) Console.WriteLine("--lCRC option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lCRC option is invalid. Are you missing a parameter?");
                        break;
                    case "--lJson": // Set OBIS log level "false" or "true"
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lJson option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lJson option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.LogJson,flag);
                                }
                                uCP.HANOODefaultParameters.LogJson = flag;
                                i++;
                            }
                            else
                                Console.WriteLine("--lJson option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lJson option is invalid. Are you missing a parameter?");
                        break;
                    case "--lJsonToApi": // Set jsonToApi "false" or "true"; True will send data to HANDate endpoint
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lJsonToApi option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lJsonToApi option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.LogJson,flag);
                                }
                                uCP.HANOODefaultParameters.lJsonToApi = flag;
                                i++;
                            }
                            else
                                Console.WriteLine("--lJsonToApi option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lJsonToApi option is invalid. Are you missing a parameter?");
                        break;
                    case "--lList1": // Set List1 logging "false" or "true"; True will also send data to HANDate endpoint
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lList1 option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lList1 option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.lList1,flag);
                                }
                                uCP.HANOODefaultParameters.lList1 = flag;
                                i++;
                            }
                            else
                                Console.WriteLine("--lList1 option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lList1 option is invalid. Are you missing a parameter?");
                        break;
                    case "--lList2": // Set List2 logging "false" or "true"; True will send data to HANDate endpoint
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lList2 option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lList2 option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.lList2,flag);
                                }
                                uCP.HANOODefaultParameters.lList2 = flag;
                                i++;
                            }
                            else
                                Console.WriteLine("--lList2 option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lList2 option is invalid. Are you missing a parameter?");
                        break;
                    case "--lList3": // Set List3 logging "false" or "true"; True will send data to HANDate endpoint
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lList3 option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lList3 option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.lList3,flag);
                                }
                                uCP.HANOODefaultParameters.lList3 = flag;
                                i++;
                            }
                            else
                                Console.WriteLine("--lList3 option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lList3 option is invalid. Are you missing a parameter?");
                        break;
                    case "--lJsonCompressed": // Set OBIS log level "false" or "true"
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lJsonCompressed option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lJsonCompressed option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.LogJsonCompressed,flag);
                                }
                                uCP.HANOODefaultParameters.LogJsonCompressed = flag;
                                i++;
                            }
                            else
                                Console.WriteLine("--lJsonCompressed option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lJsonCompressed option is invalid. Are you missing a parameter?");
                        break;
                    case "--lApi": // Set API log level "false" or "true"
                        if( i+1 < args.Length )
                        {
                            if ( Boolean.TryParse(args[i+1], out flag) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--lApi option and value = {0}",args[i+1]);
                                    Console.WriteLine("--lApi option changed current log status from {0} to {1}",uCP.HANOODefaultParameters.lApi,flag);
                                }
                                uCP.HANOODefaultParameters.lApi = flag;
                                i++;
                            }
                            else
                                Console.WriteLine("--lApi option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--lApi option is invalid. Are you missing a parameter?");
                        break;
                    case "--delay": // Set delay between HAN reads; --delay 10000; Delay 10000 ms = 10 second
                        if( i+1 < args.Length )
                        {
                            if ( int.TryParse(args[i+1], out value) )
                            {
                                if (uCP.HANOODefaultParameters.Log)
                                {
                                    Console.WriteLine("--delay option and value = {0}",args[i+1]);
                                    Console.WriteLine("--delay option changed current log status from {0} to {1}",uCP.HANOODeviceData.delayBetweenReads,value);
                                }
                                uCP.HANOODeviceData.delayBetweenReads = value;
                                i++;
                            }
                            else
                                if (log) Console.WriteLine("--delay option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
                        else
                            Console.WriteLine("--delay option is invalid. Are you missing a parameter?");
                        break;
                    default:
                        Console.WriteLine("\n-----------------\nUnknown/invalid command: {0}\n-----------------",args[i]);
                        break;
                }
            }
        }

        public void help()
        {
            Console.WriteLine("\n--------   " +
                               "Parameters is stored in JSON fille \'" + jSONfileName + "\'" +
                               "  --------");
            Console.WriteLine("For help :" + 
                              "\n--h => display help (this overview)" +
                              "\n--l => Log various values. More for debugging" +
                              "\n--p => display standard parameters from \'" + jSONfileName + "\'" +
                              "\n--delay \'<int>\' => delay <int> milliseconds between HAN port read" +
                              "\nYou may override some parameters from command line:" +
                              "\n--pn \'PortName\'\n--br \'BaudRate\'" +
                              "\n--lDLMS \'<bool>\' => log DLMS data to tty" +
                              "\n--lCOSEM \'<bool>\' => log COSEM data to tty" +
                              "\n--lOBIS \'<bool>\' => log OBIS data block to tty" +
                              "\n--lCRC \'<bool>\' => log CRC result to tty" +
                              "\n--lJson \'<bool>\' => log Json data to tty" +
                              "\n--lJsonCompressed \'<bool>\' => log Json data to tty" +
                              "\n--HANApiEndPoint \'<string>\' => text <string> to HAN API endpoint" +
                              "\n--lJsonToApi \'<bool>\' => send json data to HAN endpoint and database (" + uCP.HANOODefaultParameters.HANApiEndPoint + ")" +
                              "\n--lList1 \'<bool>\' => send List1 json data to HAN endpoint and database (" + uCP.HANOODefaultParameters.HANApiEndPoint + ")" +
                              "\n--lList2 \'<bool>\' => send List2 json data to HAN endpoint and database (" + uCP.HANOODefaultParameters.HANApiEndPoint + ")" +
                              "\n--lList3 \'<bool>\' => send List3 json data to HAN endpoint and database (" + uCP.HANOODefaultParameters.HANApiEndPoint + ")" +
                              "\n\t-------------------\n");
        }
    }
}