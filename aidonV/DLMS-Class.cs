#undef DLMSDEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Slettebakk_OBIS;
using Slettebakk_COSEM;

namespace Slettebakk_DLMS
{
    class DLMS
    {
        public List<byte> DLMSCOSEMlist = new List<byte>();
        public byte frameFormatType { set; get; } // DLMS frame type 3 (iFrame) expected (0b1010/0xA)
        public byte frameSegmentBit { set; get; }
        public int dLMSframeLength { set; get; }  // DLMS datablock length without 0x7E start and stop flag
        public static byte DLMSflag = 0x7E; // start flag indicator for DLMS frames (inclusive electric meeters)
        public static byte frameFormatTypeMask = 0b11110000;
        public static byte frameSegmentBitMask = 0b00001000;
        public byte iFrame = 0b1010;   // value "ten" (10,0xA)
        public byte sFrame = 0b0001;   // value "one" (1,0x1)
        public byte uFrame = 0b0011;    // value "tre" (3,0x3)
        public byte controlFlag = 0;
        public byte[] hcs = new byte[2];
        public uint dLMSfcs;
        public uint dLMSfcsInvert;
        public uint dLMShcs;
        public byte dLMScontrol;
        public bool DLMSStartFlag = false;

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
            }

            public ushort ComputeChecksum(List<byte> data, int start, int length)
            {
                ushort fcs = 0xffff;
                var index = 0x0000;
                try
                {
                    for (int i = start; i < (start + length); i++)
                    {
                        index = (fcs ^ data[i]) & 0xff;
                        fcs = (ushort)((fcs >> 8) ^ table[index]);
                    }
                    fcs ^= 0xffff;
                    return fcs;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in ComputeChecksum:\nex error=\n{0}",ex);
                    return 0;
                }
            }
        }
        public void readDLMSstreamFromHAN(SerialPort sp)
        {
            if (!sp.IsOpen) sp.Open();
            DLMSCOSEMlist.Clear(); // Prepare and start with empthy list
            int HANbufferLength;
            int bytesProcessed;
            COSEMClass cOSEM = new COSEMClass();
            cOSEM.cOSEMInitialize();

            try
            {   // now, loop forever reading and prosessing HAN port data

                while (true)
                {
                    HANbufferLength = sp.BytesToRead; // get number of available bytes on HAN device
                    bytesProcessed = 0;  // validate if we have processes all bytes in HAN buffer
                    byte[] byteBuffer = new byte[HANbufferLength]; // preapare HAN data buffer

                    for (int i = 0; i < HANbufferLength; i++) byteBuffer[i] = (byte)sp.ReadByte(); // Read all data in HAN device to byteBuffer

                    if (byteBuffer.Length != HANbufferLength)
                        Console.WriteLine(" ******************   ERROR reading HAN port   ****************");

                    // Console.WriteLine("\nBytes read from HAN meeter:{0}", HANbufferLength);
                    for (int i = 0; i < HANbufferLength; i++)
                    {
                        if (byteBuffer[i] == DLMSflag)
                        {
                            if (i + 1 < HANbufferLength)
                                if ((frameType(byteBuffer[i + 1]) == iFrame))
                                {
#if DLMSDEBUG
                                    Console.Write("\nNew 0x7E & iFrame block!\n{0:X2} ", byteBuffer[i]);
#endif
                                    // Is old block prosessed?
                                    if (DLMSStartFlag)
                                    {
                                        DLMSCOSEMlist.Add(byteBuffer[i]); // add last 0x7E flag
#if DLMSDEBUG
                                            Console.WriteLine("\n3) Complete block? Add 0x7E");
#endif
                                        DLMSCOSEMCompleteBlock(DLMSCOSEMlist);
                                    }
                                    DLMSCOSEMlist.Add(byteBuffer[i]); // add first 0x7E flag 
                                    DLMSStartFlag = true;
                                }
                                else
                                {
                                    DLMSCOSEMlist.Add(byteBuffer[i]);
                                    // but do we have a start?
                                    if (DLMSStartFlag) // yes, full block
                                    {
#if DLMSDEBUG
                                            // Console.WriteLine("{0:X2}\nComplete block. Process...", byteBuffer[i]);
                                            Console.WriteLine("\n2) Complete block...");
#endif
                                        DLMSCOSEMCompleteBlock(DLMSCOSEMlist);
                                        DLMSStartFlag = false;
                                    }

                                }
                            else
                            {
                                DLMSCOSEMlist.Add(byteBuffer[i]);
                                if (DLMSStartFlag)
                                {
#if DLMSDEBUG
                                        Console.WriteLine("\n1) Complete block. Process...");
#endif
                                    DLMSCOSEMCompleteBlock(DLMSCOSEMlist);
                                    DLMSStartFlag = false;
                                }
                            }
                        }
                        else
                        {
                            DLMSCOSEMlist.Add(byteBuffer[i]);
                            // Console.Write("{0:X2} ", byteBuffer[i]);
                        }
                        bytesProcessed = i + 1;
                    }
                    // Console.WriteLine("");
                    // ready to start first DLMS block
                    // Console.WriteLine("\nBytes prosessed:{0}. All bytes={1}", bytesProcessed, bytesProcessed == HANbufferLength);
                    System.Threading.Thread.Sleep(1510); // Sleep 1.51s
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nAbnormal exit:\n{0}", ex);
                sp.Close();
                sp.Dispose();
                Environment.Exit(-1);
                return;
            }
        }
        public void dLMSframePropertyData(List<byte> dlmsDataBlock)
        {
            // Pull out the the DLMS header data and set the DLMS c# variable
            frameFormatType = (byte)((dlmsDataBlock[1] & frameFormatTypeMask) >> 4);
            frameSegmentBit = (byte)((dlmsDataBlock[1] & frameSegmentBitMask) >> 3);
            dLMSframeLength = ((dlmsDataBlock[1] & 0b00000111) << 8) | dlmsDataBlock[2];
#if DLMSDEBUG
                Console.WriteLine("Frame heading data:\nFrameFormatType={0}, frameSegmentBit={1:X2},dLMSframeLength={2}", frameFormatType, frameSegmentBit, dLMSframeLength);
#endif
        }
        public byte frameType(byte b)
        {
            return (byte)(((b) & frameFormatTypeMask) >> 4);
        }

        public void dLMSControlHscFcs(byte control, byte hcs1, byte hcs2, byte fcs1, byte fcs2) // Frame Check Sequence calculation
        {
            dLMSfcs = (ushort)((fcs1 << 8) + fcs2);
            dLMSfcsInvert = (ushort)((fcs2 << 8) + fcs1);
            dLMShcs = (ushort)((hcs1 << 8) + hcs2);
            dLMScontrol = control;
#if DLMSDEBUG
                Console.WriteLine("control byte={0:X2}, dLMSfcs={1:X4}({1}) and dLMShcs={2:X4}({2}) in Frame Check Sequence block.", dLMScontrol, dLMSfcs, dLMShcs);
                Console.WriteLine("fcs inverted ={0:X4} ({0}))", dLMSfcsInvert);
#endif
        }
        public void DLMSCOSEMCompleteBlock(List<byte> dLMSCOSEMData)
        {
            COSEMClass cOSEM = new COSEMClass();
            Crc16Class crc = new Crc16Class();

            dLMSframePropertyData(dLMSCOSEMData);  // Update heading data
            dLMSControlHscFcs(dLMSCOSEMData[5], dLMSCOSEMData[6], dLMSCOSEMData[7], dLMSCOSEMData[dLMSCOSEMData.Count - 3], dLMSCOSEMData[dLMSCOSEMData.Count - 2]); // Calculate control, hcs and fcs

            if (dLMSfcsInvert == crc.ComputeChecksum(dLMSCOSEMData, 1, dLMSframeLength - 2)) // typical "7E........ A36E7E", dLMSfcsInvert=6EA3
            {

#if DLMSDEBUG
                    Console.WriteLine("DLMS datablock:\nList length={0}\nDLMS block length={1},\nValidity (List length and DLMS lengt match)={2}\nFull DLMS data result:", dLMSCOSEMData.Count, dLMSframeLength, (dLMSCOSEMData.Count - 2) == dLMSframeLength && dLMSCOSEMData[0] == dLMSCOSEMData[dLMSframeLength + 1]);
                    dLMSCOSEMDebug("Frame Check ok! DLMS block complete",dLMSCOSEMData,true);
#endif
                cOSEM.cOSEMDataBlock(dLMSCOSEMData);

            }
            else
                Console.WriteLine("Frame Check failed. DLMS data block error");
            DLMSCOSEMlist.Clear();
            DLMSStartFlag = false;
        }
    }
}