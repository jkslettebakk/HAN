
namespace COSEM
{
    public class HAN_OO_COSEM
    {

        public void HANOOCOSEMBlock(byte[] DLMSBlock, OOUserConfigurationParameters OOuCP )
        {
            HAN_Crc16Class.Crc16 crc = new HAN_Crc16Class.Crc16();
            HAN_OBIS.obisCodesClass obis = new HAN_OBIS.obisCodesClass();
            int errorCRC = 0;
            ushort dLMSfcsInvert;
            ushort dLMSfcs;
            byte[] CRCBlock = new byte[DLMSBlock.Length - 4];
            byte[] oBISBytes = new byte[ DLMSBlock.Length - 21 ];

            int COSEMLength = DLMSBlock.Length;
            if( OOuCP.uCP.HANOODefaultParameters.LogCOSEM)
            {
            Console.WriteLine("-------------------------------------------------------------\n" +
                             "Complete DLMS block. Checking CRC and extracting COSEM data");
            for (int i = 0; i < DLMSBlock.Length; i++)
            {
                if ( i != 0 )
                {
                    if ( (i % 10) == 0 ) Console.Write(" ");
                    if ( (i % 40) == 0 ) Console.WriteLine();
                }
                Console.Write("{0:X2} ",DLMSBlock[i]);
            }
            Console.WriteLine("\n-------------------------------------------------------------");                
            }

            // CRC / crc-25 check
            dLMSfcsInvert = (ushort)((DLMSBlock[COSEMLength-3] << 8) + DLMSBlock[COSEMLength-2]);
            dLMSfcs = (ushort)((DLMSBlock[COSEMLength-2] << 8) + DLMSBlock[COSEMLength-3]);
            for (int i = 0; i < CRCBlock.Length; i++) CRCBlock[i] = DLMSBlock[i+1];
            ushort CRC = crc.ComputeChecksum( CRCBlock, OOuCP.uCP.HANOODefaultParameters.LogCRC );
            if ( OOuCP.uCP.HANOODefaultParameters.LogCRC )
                Console.WriteLine("Calculated crc = {0:X4}, Result should be  {1:X4}",CRC,dLMSfcs);
            if ( (CRC != dLMSfcs) && OOuCP.uCP.HANOODefaultParameters.LogCRC )
            {
                Console.WriteLine("Error in CRC");
                errorCRC++;
                // Considder stopping. Error reading leagal blocks from HAN device
            }

            // all ok, lets dechifer OBIS data and values
            if ( OOuCP.uCP.HANOODefaultParameters.LogCRC ) Console.WriteLine("Valid CRC check");
            for( int i=0; i < oBISBytes.Length; i++ ) oBISBytes[i] = CRCBlock[i+17];

            // obis.showObisValues();
            obis.oBISBlock( oBISBytes, OOuCP );

        }

    }
}