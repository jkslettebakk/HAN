
namespace HAN_OO
{
    public class Program
    {

        public static void Main(string[] args)
        {
            int HANPortLoops = 0;
            Console.WriteLine("Hello,hello \"DLSM-COSEM-OBIS\" World!");
            // Initiate objects config data in JSON file 
            OOUserConfigurationParameters OOuCP = new OOUserConfigurationParameters();
            // User Configuration Parameters
            // OOUserConfigurationParameters.UserConfigurationParameters uCPcontent = new OOUserConfigurationParameters.UserConfigurationParameters();

            Console.WriteLine("Args[0]={0}",args[0]);
            if ( args[0] == "--d" ) OOuCP.displayParameters(OOuCP.uCP);

            Console.WriteLine("\nPortName = {0}\n", OOuCP.uCP.HANOODeviceData.serialPortName);

            // Prepare Reading DLMS data
            OO_HAN_DLMS_Read dlmsRead = new OO_HAN_DLMS_Read();
            // Declair/Initiate HAN SerialPort object
            SerialPort serialPort = new SerialPort();

            // Get/load user configuration parameters
            // OOuCP = OOuCP.loadConfigFile();
            
            // OOuCP.displayParameters(OOuCP);

            // Check and modify configuration from command line
            // OOuCP.getHANOptions( args, uCPcontent ); // Modify config data by command line parameters, if any

            serialPort = OOuCP.setSerialPort( OOuCP );

            dlmsRead.OO_HAN_DLMS_ReadData( serialPort ); // start reading DLMS data

            //Console.WriteLine("\n\tuCP object:{0}",uCP);
            
            do
            {
                // Read HAN port and analyse for OBIS codes on Power/ProweCosumption/Ampere etc.
                if ( (++HANPortLoops % 100000000) == 0 )
                {
                    Console.WriteLine("Loops done: {0}",HANPortLoops);
                }

            } while (true);
        }
    }
}