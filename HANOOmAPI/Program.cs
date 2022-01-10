
// using Microsoft.AspNetCore.Http;

// Using this guide
// https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient

namespace HAN_OO
{
    public class Program
    {
        /*
        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            // var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            var stringTask = client.GetStringAsync("https://han.slettebakk.com/api/List3/number");

            var msg = await stringTask;
            Console.WriteLine(msg);
        }
        */

        // private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
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