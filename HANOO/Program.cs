
namespace HAN_OO
{
    public class Program
    {

        public static void Main(string[] args)
        {

            // Initiate objects config data in JSON file 
            OOUserConfigurationParameters OOuCP = new OOUserConfigurationParameters();

            // User Configuration Parameters
            if (args.Length > 0 ) OOuCP.getHANOptions( args, OOuCP );
            if ( OOuCP.uCP.HANOODefaultParameters.help ) OOuCP.help();

            // Prepare Reading DLMS data
            OO_HAN_Read_DLMS dlmsRead = new OO_HAN_Read_DLMS();

            dlmsRead.OO_HAN_Read_DLMS_Data( OOuCP ); // start reading DLMS data

            // Stopping this app will probably be by "Ctrl-c" or in a memory crash
            // and will probably never "execute" this line

        }
    }
}