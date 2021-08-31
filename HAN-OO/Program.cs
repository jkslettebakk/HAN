
namespace HAN_OO
{
    public class Program
    {

        public static void Main(string[] args)
        {
            int HANPortLoops = 0;
            Console.WriteLine("Hello \"DLSM-COSEM-OBIS\" World!");
            // Initiate objects config data in JSON file 
            OOUserConfigurationParameters OOuCP = new OOUserConfigurationParameters();
            // User Configuration Parameters
            OOUserConfigurationParameters.UserConfigurationParameters uCPcontent = new OOUserConfigurationParameters.UserConfigurationParameters();
            // Prepare Reading DLMS data
            OO_HAN_DLMS_Read dlmsRead = new OO_HAN_DLMS_Read();
            // Declair/Initiate HAN SerialPort object
            SerialPort serialPort = new SerialPort();

            // Get/load user configuration parameters
            uCPcontent = OOuCP.loadConfigFile();
            
            OOuCP.displayParameters(uCPcontent);

            // Check and modify configuration from command line
            OOuCP.getHANOptions( args, uCPcontent ); // Modify config data by command line parameters, if any

            serialPort = OOuCP.setSerialPort( OOuCP );

            Console.WriteLine("PortName = {0}", uCPcontent.HANOODeviceData.serialPortName);

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