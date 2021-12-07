
namespace HANOOUserConfigurationParameters
{
    public class OOUserConfigurationParameters
    {

        private string jSONfileName = "HAN-OO-Parameters.json";

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
        }

        public class HANOODefaultParameters
        {
            [JsonPropertyName("Comment")]
            public string Comment { get; set; }

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
            Console.WriteLine("--------------------------------------");
        } 

        public UserConfigurationParameters loadConfigFile()
        {

            // Open and analyse config (HAN-OO.config) file
            // string jSONfileName = "HAN-OO-Parameters.json";
            string jSONString;

            // Read JSON file data to string
            jSONString = File.ReadAllText(jSONfileName);
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
                        help();
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
                                if (log) Console.WriteLine("--lOBIS option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
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
                                if (log) Console.WriteLine("--lJson option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
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
                                if (log) Console.WriteLine("--lJsonCompressed option and value = {0} is invalid. Skipp value",args[i+1]);
                        }
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
                        break;
                    default:
                        Console.WriteLine("\n-----------------\nUnknown/invalid command: {0}\n-----------------",args[i]);
                        break;
                }
            }
        }

        public void help()
        {
            Console.WriteLine("Parameters is stored in JSON fille \'{0}\'",jSONfileName);
            Console.WriteLine("For help :\n" + 
                              "--h => display help\n" + 
                              "--p => display parameters" +
                              "\nYou may change the parameters for:\n--pn \'PortName\'\n--br \'BaudRate\'" +
                              "\n--lDLMS \'<bool>\' => log DLMS data to tty" +
                              "\n--lCOSEM \'<bool>\' => log COSEM data to tty" +
                              "\n--lOBIS \'<bool>\' => log OBIS data block to tty" +
                              "\n--lCRC \'<bool>\' => log CRC result to tty" +
                              "\n--lJson \'<bool>\' => log Json data to tty" +
                              "\n--delay \'<int>\' => delay <int> milliseconds between HAN port read");
        }
    }
}