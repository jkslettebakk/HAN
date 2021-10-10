
namespace HANOOUserConfigurationParameters
{
    public class OOUserConfigurationParameters
    {

        public string jSONfileName = "HAN-OO-Parameters.json";

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
        }

        public class HANOOParameters
        {
            [JsonPropertyName("Comment")]
            public string Comment { get; set; }

            [JsonPropertyName("DefaultParameterFile")]
            public string DefaultParameterFile { get; set; }

            [JsonPropertyName("-h")]
            public string H { get; set; }

            [JsonPropertyName("-l")]
            public string L { get; set; }
        }

        public class HANOODefaultParameters
        {
            [JsonPropertyName("Comment")]
            public string Comment { get; set; }

            [JsonPropertyName("Log")]
            public bool Log { get; set; }

            [JsonPropertyName("LogUserConfig")]
            public bool LogUserConfig { get; set; }

            [JsonPropertyName("LogHAN")]
            public bool LogHAN { get; set; }

            [JsonPropertyName("LogDLMS")]
            public bool LogDLMS { get; set; }

            [JsonPropertyName("LogCOSEM")]
            public bool LogCOSEM { get; set; }

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

        public OOUserConfigurationParameters()
        {
            Console.WriteLine("Initialising/preparing \"OOUserConfigurationParameters\" objects and user data.");
            // displayParameters( uCP );
        }

        public void displayParameters( UserConfigurationParameters parameters )
        {
            Console.WriteLine("userConfigurationParameters.Comment: {0}",parameters.Comment);
            Console.WriteLine("**************************************************");
            Console.WriteLine("parameters.HANOODetails.Comment: {0}",parameters.HANOODetails.Comment);
            Console.WriteLine("parameters.HANOODetails.Programmer: {0}",parameters.HANOODetails.Programmer);
            Console.WriteLine("parameters.HANOODetails.Phone: {0}",parameters.HANOODetails.Phone);
            Console.WriteLine("parameters.HANOODetails.Street: {0}",parameters.HANOODetails.Street);
            Console.WriteLine("parameters.HANOODetails.PostNumber & details.HANOODetails.PostName: {0} {1}", parameters.HANOODetails.PostNumber, parameters.HANOODetails.PostName );
            Console.WriteLine("**************************************************");
            Console.WriteLine("parameters.HANOODeviceData.Comment: {0}",parameters.HANOODeviceData.Comment);
            Console.WriteLine("parameters.HANOODeviceData.HANDeviceName: {0}",parameters.HANOODeviceData.HANDeviceName);
            Console.WriteLine("parameters.HANOODeviceData.serialPortName: {0}",parameters.HANOODeviceData.serialPortName);
            Console.WriteLine("parameters.HANOODeviceData.serialPortBaud: {0}",parameters.HANOODeviceData.serialPortBaud);
            Console.WriteLine("parameters.HANOODeviceData.serialPortParity: {0}",parameters.HANOODeviceData.serialPortParity);
            Console.WriteLine("parameters.HANOODeviceData.serialPortDataBits: {0}",parameters.HANOODeviceData.serialPortDataBits);
            Console.WriteLine("parameters.HANOODeviceData.serialPortStopBits: {0}",parameters.HANOODeviceData.serialPortStopBits);

            Console.WriteLine("**************************************************");
            Console.WriteLine("parameters.HANOODefaultParameters.Comment: {0}",parameters.HANOODefaultParameters.Comment);
            Console.WriteLine("parameters.HANOODefaultParameters.Log: {0}",parameters.HANOODefaultParameters.Log);
            Console.WriteLine("parameters.HANOODefaultParameters.LogUserConfig: {0}",parameters.HANOODefaultParameters.LogUserConfig);
            Console.WriteLine("parameters.HANOODefaultParameters.LogHAN: {0}",parameters.HANOODefaultParameters.LogHAN);
            Console.WriteLine("parameters.HANOODefaultParameters.LogDLMS: {0}",parameters.HANOODefaultParameters.LogDLMS);
            Console.WriteLine("parameters.HANOODefaultParameters.LogCOSEM: {0}",parameters.HANOODefaultParameters.LogCOSEM);
            Console.WriteLine("**************************************************");
            Console.WriteLine("parameters.HANOOParameters.Comment: {0}",parameters.HANOOParameters.Comment);
            Console.WriteLine("parameters.HANOOParameters.DefaultParameterFile: {0}",parameters.HANOOParameters.DefaultParameterFile);
            Console.WriteLine("parameters.HANOOParameters.H: {0}",parameters.HANOOParameters.H);
            Console.WriteLine("parameters.HANOOParameters.L: {0}",parameters.HANOOParameters.L);
        } 

        public UserConfigurationParameters loadConfigFile()
        {

            // Open and analyse config (HAN-OO.config) file
            // string jSONfileName = "HAN-OO-Parameters.json";
            string jSONString;

            Console.WriteLine("****    In analyseConfigFile    ****");

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

        public SerialPort setSerialPort( OOUserConfigurationParameters uCP )
        {
            SerialPort sp = new SerialPort();
            return sp;
            
        }

        public void getHANOptions( string[] args, OOUserConfigurationParameters.UserConfigurationParameters uCPcontent )
        {
            // analyse command line input
            Console.WriteLine("****    In getHANOptions    ****\nargs.Length = {0}", args.Length);
            foreach( var arg in args)
                Console.WriteLine("{0}",arg);
            // modifying/override JSON parameters from commandline
            // String management
            // -p "/dev/ttyUSB0" -> -p=Paramerer "PortName", PortName="/dev/ttyUSB0"
            for ( int i = 0; i < args.Length; i++)
            {
                switch ( args[i] )
                {
                    case "-h":
                        help();
                        break;
                    case "-pn":
                        Console.WriteLine("-pn option and value = {0}",args[i+1]);
                        uCPcontent.HANOODeviceData.serialPortName = args[i+1];
                        i++;
                        break;
                    default:
                        Console.WriteLine("Unknown/invalid command: {0}",args[i]);
                        break;
                }
            }
        }

        public void help()
        {
            Console.WriteLine("Parameters is stored in JSON fille \'{0}\'",jSONfileName);
            Console.WriteLine("For help :\n-h (display help)\nYou may change the parameters for:\n-pn \'PortName\'\n-br \'BaudRate\'");
        }
    }
}