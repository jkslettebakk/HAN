
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

            if (args.Length > 0 ) OOuCP.getHANOptions( args, OOuCP );
            // Prepare Reading DLMS data
            OO_HAN_Read_DLMS dlmsRead = new OO_HAN_Read_DLMS();

            // Get/load user configuration parameters
            
            // OOuCP.displayParameters(OOuCP);

            // Check and modify configuration from command line
            // OOuCP.getHANOptions( args, uCPcontent ); // Modify config data by command line parameters, if any

            dlmsRead.OO_HAN_Read_DLMS_Data( OOuCP ); // start reading DLMS data

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