#undef DEBUG
#undef DLSMDEBUG
#undef COSEMDEBUG
#undef OBISDEBUG
#define JSONDEBUG
#define COSEMSTRUCTURE

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HAN_Crc16Class
{
        class Crc16Class
        {
            private const ushort polynomial = 0x8408;
            private static ushort[] table = new ushort[256];
            static Crc16Class() // Initiate CRC Class Object table (table[256]) 
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
#if CRCCHECK
                for ( int i = 0; i < 256; i++ )
                {
                    if ( (i % (writeWidth - 10)) == 0 ) Console.WriteLine();
                    Console.Write("{0:X2} ",table[i]);
                }
                Console.WriteLine();
#endif
            }

            public ushort ComputeChecksum(List<byte> data, int start, int length)
            {
                ushort fcs = 0xffff;
                var index = 0x0000;
                try
                {
                    for (int i = start; i < (start + length); i++)
                    {
                        if ( i == start ) Console.WriteLine("ComputeCheck first byte = {0}",data[i]);
                        if ( i == (start + length - 1) ) Console.WriteLine("ComputeCheck last byte = {0}",data[i]);
                        index = (fcs ^ data[i]) & 0xff;
                        fcs = (ushort)((fcs >> 8) ^ table[index]);
                    }
                    fcs ^= 0xffff;
#if CRCCHECK                    
                    Console.WriteLine("ComputeChecksum processed - start={0}, length={1}:\nFirst={2:X2}, last={3:X2}",start,length,data[start],data[start + length]);
                    for (int i = start; i < (start + length); i++ )
                    {
                        if ( (i % writeWidth) == 0 ) Console.WriteLine(); 
                        Console.Write("{0:X2} ",data[i]);
                    }
                    Console.WriteLine("\nReturning fcs={0:X2}",(ushort) fcs);
#endif
                    return fcs;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in ComputeChecksum:\nex error=\n{0}",ex);
                    Console.WriteLine("Input was - start={0}, length={1}",start,length);
                    return 0;
                }
            }
        }
}

