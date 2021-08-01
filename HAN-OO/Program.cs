
namespace HAN_OO
{
    class Program
    {

        public static async Task Main(string[] args)
        {
            int HANPortLoops = 0;
            OOUserConfigurationParameters uCP = new OOUserConfigurationParameters(); // Initiate objects config data in JSON file 
            OO_HAN_DLMS_Read dlmsRead = new OO_HAN_DLMS_Read();
            
            Console.WriteLine("Hello \"DLSM-COSEM-OBIS\" World!");
            // fetch parameters like -HAN-Device <name> (e.g. AIDON, KAMSTRUP)
            // han-oo --help to se all
            //
            await uCP.analyseConfigFile();  // Reading config JSON file
            uCP.getHANOptions( args ); // Modify config data by command line parameters, if any
            dlmsRead.OO_HAN_DLMS_ReadData(); // start reading DLMS data
            
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