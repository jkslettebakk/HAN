using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HANOOUserConfigurationParameters;

namespace HAN_OO
{

    class Program
    {

        public static async Task Main(string[] args)
        {
            int HANPortLoops = 0;
            OOUserConfigurationParameters uCP = new OOUserConfigurationParameters();
            
            Console.WriteLine("Hello \"DLSM-COSEM-OBIS\" World!");
            // fetch parameters like -HAN-Device <name> (e.g. AIDON, KAMSTRUP)
            // han-oo --help to se all
            //
            await uCP.analyseConfigFile();
            uCP.getHANOptions( args );
            
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
