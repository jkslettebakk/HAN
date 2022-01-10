#define CRCLOG
#undef CRCLOG

namespace HAN_Crc16Class
{
        class Crc16
        {
            private const ushort polynomial = 0x8408;
            private static ushort[] table = new ushort[256];
            private static int writeWidth = 28;
            
            static Crc16( ) // Initiate CRC Class Object table (table[256]) 
            {
                ushort value;
                ushort temp;
                for (ushort i = 0; i < table.Length; ++i)
                {
                    value = 0;
                    temp = i;
                    for (byte j = 0; j < 8; ++j)
                    {
                        if (((value ^ temp) & 0x0001) != 0)
                        {
                            value = (ushort)((value >> 1) ^ polynomial);
                        }
                        else
                        {
                            value >>= 1;
                        }
                        temp >>= 1;
                    }
                    table[i] = value;
                }

#if (CRCLOG)
                Console.WriteLine();
                for ( int i = 0; i < 256; i++ )
                {
                    if ( (i % (writeWidth - 10)) == 0 ) Console.WriteLine();
                    Console.Write("{0:X4} ",table[i]);
                }
                Console.WriteLine();
#endif                
            }

            public ushort ComputeChecksum(byte[] data, bool logCRC )
            {
                ushort fcs = 0xffff;
                byte index = 0x00;
                int start = 0;
                int dataLength = data.Length;

                try
                {
                    fcs = 0xffff;
                    for (int i = start; i < (start + dataLength); i++)
                    {
                        if ( (i == start) && logCRC ) Console.WriteLine("ComputeCheck first byte ({0}) = {1:x}",i,data[i]);
                        if ( (i == (start + dataLength - 1)) && logCRC ) Console.WriteLine("ComputeCheck last byte ({0}) = {1:x}",i,data[i]);
                        // index = (fcs ^ data[i]) & 0xff;
                        index = (byte) (fcs ^ data[i] & 0xff);
                        fcs = (ushort)((fcs >> 8) ^ table[index]);
                    }

                    if( logCRC)
                    {
                        Console.WriteLine("ComputeChecksum processed - start={0}, dataLength={1}:\nFirst byte={2:X2}, last byte={3:X2}",start,dataLength,data[start],data[start + dataLength - 1]);
                        for (int i = start; i < (start + dataLength); i++ )
                        {
                            if ( (i % writeWidth) == 0 ) Console.WriteLine(); 
                            Console.Write("{0:X2} ",data[i]);
                        }
                        Console.WriteLine("\nReturning fcs={0:X2}",(ushort) fcs);
                    }
                        fcs ^= 0xffff;
                        return (ushort) fcs;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in ComputeChecksum:\nex error=\n{0}",ex);
                    return 0;
                }
            }
        }
}

