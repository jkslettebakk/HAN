
// using Microsoft.AspNetCore.Http;

// Using this guide
// https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient

namespace HAN_OO
{
    public class Program
    {

        static async Task Main(string[] args) // made async Task to be able to use Async GET and POST
        {
            //Caal API endpoint....
            // await ProcessRepositories();

            // Initiate objects config data in JSON file 
            OOUserConfigurationParameters OOuCP = new OOUserConfigurationParameters();

            // User Configuration Parameters
            if (args.Length > 0 ) OOuCP.getHANOptions( args, OOuCP );
            if ( OOuCP.uCP.HANOODefaultParameters.help ) OOuCP.help();

            // Prepare Reading DLMS data
            OO_HAN_Read_DLMS dlmsRead = new OO_HAN_Read_DLMS();

            // Introduces async methods for API to endpoint..... Next time think trough this from start..... :-) Big rewrite....
            await OO_HAN_Read_DLMS.OO_HAN_Read_DLMS_Data( OOuCP ); // start reading DLMS data

            // Stopping this app will probably be by "Ctrl-c" or in a memory crash
            // and will probably never "execute" this line
        }
    }
}