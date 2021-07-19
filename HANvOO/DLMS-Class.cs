#define DLMSDEBUG
#define CRCDEBUG
#undef CRCCHECK

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HAN_OBIS;
using HAN_COSEM;
using HAN_Crc16Class;

namespace HAN_DLMS
{
    class DLMS
    {
        List<byte> DLMSCOSEMlist = new List<byte>();
        byte frameFormatType { set; get; } // DLMS frame type 3 (iFrame) expected (0b1010/0xA)
        byte frameSegmentBit { set; get; }
        int dLMSframeLength { set; get; }  // DLMS datablock length without 0x7E start and stop flag
        static byte DLMSflag = 0x7E; // start flag indicator for DLMS frames (inclusive electric meeters)
        static byte frameFormatTypeMask = 0b11110000;
        static byte frameSegmentBitMask = 0b00001000;
        byte iFrame = 0b1010;   // value "ten" (10,0xA)
        byte sFrame = 0b0001;   // value "one" (1,0x1)
        byte uFrame = 0b0011;    // value "tre" (3,0x3)
        byte controlFlag = 0;
        byte[] hcs = new byte[2];
        uint dLMSfcs;
        uint dLMSfcsInvert;
        uint dLMShcs;
        byte dLMScontrol;
        bool DLMSStartFlag = false;
        int waitBetweenRead = 1510; // 1.51 second
        string KamstrupDate;
        const byte writeWidth = 30;
#if KAMSTRUP
        waitBetweenRead = 10000; // 10 second

#endif

        public void readDLMSstreamFromHAN(SerialPort sp)
        {
            if (!sp.IsOpen) sp.Open();
            DLMSCOSEMlist.Clear(); // Prepare and start with empthy list
            int HANbufferLength = 0;
            int bytesProcessed = 0;

            try
            {   // now, loop forever reading and prosessing HAN port data

                while (true)
                {
                    HANbufferLength = sp.BytesToRead; // get number of available bytes on HAN device
                    bytesProcessed = 0;  // validate if we have processes all bytes in HAN buffer
                    byte[] byteBuffer = new byte[HANbufferLength]; // preapare HAN data buffer
#if DLMSDEBUG
                    Console.WriteLine("\nReady to read HAN port.\n\tHan buffer length={0}, byteBuffer length={1}, DLMSCOSEMlist length={2}", HANbufferLength,byteBuffer.Length,DLMSCOSEMlist.Count);
#endif
                    for (int i = 0; i < HANbufferLength; i++) byteBuffer[i] = (byte)sp.ReadByte(); // Read all data in HAN device to byteBuffer
#if DLMSDEBUG
                    for( int i=0; i < byteBuffer.Length; i++)
                    {
                        if ( (i % writeWidth ) == 0 ) Console.WriteLine();
                        Console.Write("{0:X2} ",byteBuffer[i]);
                    }
                    Console.WriteLine();
#endif

                    if (byteBuffer.Length != HANbufferLength)
                        Console.WriteLine(" ******************   ERROR reading HAN port   ****************");

                    // Console.WriteLine("\nBytes read from HAN meeter:{0}", HANbufferLength);
                    for (int i = 0; i < HANbufferLength; i++)
                    {
                        // Console.WriteLine("Processing byte nr={0}, value={1:X2} ",i,byteBuffer[i]);
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
                                    DLMSCOSEMlist.Clear(); // Empty old list and start new
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
                                        DLMSCOSEMlist.Clear();
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
                    System.Threading.Thread.Sleep(waitBetweenRead); // Sleep time differs between meetering systems
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
            dLMSframeLength = ((dlmsDataBlock[1] & 0b00000111) << 8) + dlmsDataBlock[2];
#if DLMSDEBUG
                Console.WriteLine("Frame heading data:\nFrameFormatType={0}, frameSegmentBit={1:X2}, dLMSframeLength={2}, dlmsDataBlock.Count={3}", frameFormatType, frameSegmentBit, dLMSframeLength,dlmsDataBlock.Count);
                for( int i = 0; i < dlmsDataBlock.Count; i++ )
                {
                    if ( i % writeWidth == 0 ) Console.WriteLine();
                    Console.Write("{0:X2} ",dlmsDataBlock[i]);
                }
                Console.WriteLine();
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
            int crcCalculatedValue;

            dLMSframePropertyData(dLMSCOSEMData);  // Update heading data
            dLMSControlHscFcs(dLMSCOSEMData[5], dLMSCOSEMData[6], dLMSCOSEMData[7], dLMSCOSEMData[dLMSCOSEMData.Count - 3], dLMSCOSEMData[dLMSCOSEMData.Count - 2]); // Calculate control, hcs and fcs

            crcCalculatedValue = crc.ComputeChecksum(dLMSCOSEMData, 1, dLMSCOSEMData.Count - 4); // typical "7E........ A36E7E", dLMSfcsInvert=6EA3
            if ((dLMSfcsInvert == crcCalculatedValue) || true )
            {

#if DLMSDEBUG
                Console.WriteLine("DLMS datablock:\nList length={0}\nDLMS block length={1},\nValidity (List length and DLMS lengt match)={2}\nFull DLMS data result:", dLMSCOSEMData.Count, dLMSframeLength, (dLMSCOSEMData.Count - 2) == dLMSframeLength && dLMSCOSEMData[0] == dLMSCOSEMData[dLMSframeLength + 1]);
#endif
                if ( dLMSCOSEMData[16] == 0x0C) // KAMSTRUP Clock data found
                {
                    KamstrupDate =  ((dLMSCOSEMData[17] & 0xFF)<<8 | (dLMSCOSEMData[18] & 0xFF)) + "." +
                                    dLMSCOSEMData[19].ToString("00") + "." + dLMSCOSEMData[20].ToString("00") + "T" +
                                    dLMSCOSEMData[22].ToString("00") + ":" + dLMSCOSEMData[23].ToString("00");
                    Console.WriteLine("\t\t**************** KAMSTRUP Time data ****************");
                    Console.WriteLine("\t\t****\t\t{0}\t\t****",KamstrupDate);
                    Console.WriteLine("\t\t****\t\tYear/date = {0} {1}.{2}\t\t****",(dLMSCOSEMData[17]<<8 | dLMSCOSEMData[18]),dLMSCOSEMData[19].ToString("00"),dLMSCOSEMData[20].ToString("00"));
                    Console.WriteLine("\t\t****\t\tTime = {0}:{1}\t\t\t****",dLMSCOSEMData[22].ToString("00"),dLMSCOSEMData[23].ToString("00"));
                    Console.WriteLine("\t\t****************************************************");
                }
                // process COSEM Data Block
                cOSEM.cOSEMDataBlock(dLMSCOSEMData);

            }
            else
            {
                Console.WriteLine("Frame Check failed. Calculated CRC={0:X2}. DLMS data block error. Block =",crcCalculatedValue);
                for (int i = 0; i < dLMSCOSEMData.Count; i++)
                {
                    if ( (i % writeWidth) == 0 ) Console.WriteLine();    
                    Console.Write("{0:X2} ", dLMSCOSEMData[i]);                    
                }
                Console.WriteLine();
            }

            DLMSCOSEMlist.Clear();
            DLMSStartFlag = false;
        }
    }
}
