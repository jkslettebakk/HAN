
namespace HANDLMS
{
    public class OO_HAN_Read_DLMS
    {

        int HANBufferLength = 0;
        static byte DLMSflag = 0x7E; // start flag indicator for DLMS frames (inclusive electric meeters)
        static byte frameFormatTypeMask = 0b11110000;
        // static byte frameSegmentBitMask = 0b00001000;
        static byte iFrame = 0b1010;   // value "ten" (10,0xA)
        // static byte sFrame = 0b0001;   // value "one" (1,0x1)
        // static byte uFrame = 0b0011;    // value "tre" (3,0x3)
        bool DLMS_start = false;
        List<byte> DLMSBlock = new List<byte>();
        COSEM.HAN_OO_COSEM Cosem = new COSEM.HAN_OO_COSEM();

        
        public void OO_HAN_Read_DLMS_Data( OOUserConfigurationParameters OOuCP )
         {
            // Declair/Initiate HAN SerialPort object
            SerialPort serialPort = new SerialPort();

            serialPort = OOuCP.setSerialPortParameters( OOuCP );

            Console.WriteLine("Reading port {0}",serialPort.PortName);

            serialPort.Open();

            while( serialPort.IsOpen)
            {
                HANBufferLength = serialPort.BytesToRead;
                byte[] HANData = new byte[HANBufferLength];
                if( OOuCP.uCP.HANOODefaultParameters.LogDLMS) Console.Write("Bytes to read = {0}",HANBufferLength);
                serialPort.Read(HANData, 0, HANBufferLength);
                if( OOuCP.uCP.HANOODefaultParameters.LogDLMS) Console.WriteLine(", bytes read {0}",HANData.Length);
                if( OOuCP.uCP.HANOODefaultParameters.LogDLMS) 
                {
                    for (int i = 0; i < HANBufferLength; i++) Console.Write("{0:x2} ",HANData[i]);
                    Console.WriteLine();
                }
                if ( HANBufferLength > 0 )
                {
                    // Collecting HAN blocks to a DLMS block
                    if ( (HANBufferLength >=2) && (HANData[0] == DLMSflag) && ( (HANData[1] & frameFormatTypeMask) >> 4) == iFrame  )
                    {
                        // Start of DLMS block
                        if (OOuCP.uCP.HANOODefaultParameters.LogDLMS) Console.WriteLine("Start of DLMS block.");
                        DLMS_start = true;
                        foreach ( byte b in HANData ) DLMSBlock.Add(b);

                        if ( (HANData[HANBufferLength-1] == DLMSflag) )
                        {
                            // complete DLMS block in one read
                            // take care of COSEM block
                            if (OOuCP.uCP.HANOODefaultParameters.LogDLMS) Console.WriteLine("DLMS block complete.");
                            DLMS_start = false;
                            Cosem.HANOOCOSEMBlock( DLMSBlock.ToArray(), OOuCP );
                            DLMSBlock.Clear();
                        }
                    }
                    else if ( DLMS_start )
                    {
                        // HAN data block without start, but could contain end
                        // add to DLMS block
                        foreach ( byte b in HANData ) DLMSBlock.Add(b);
                        if (OOuCP.uCP.HANOODefaultParameters.LogDLMS) Console.WriteLine("DLMS block continue.");
                        if ( (HANData[HANBufferLength-1] == DLMSflag) )
                        {
                            // take care of COSEM block
                            if (OOuCP.uCP.HANOODefaultParameters.LogDLMS) Console.WriteLine("DLMS block complete.");
                            DLMS_start = false;
                            Cosem.HANOOCOSEMBlock( DLMSBlock.ToArray(), OOuCP );
                            DLMSBlock.Clear();
                        }
                    } 
                    else if ( DLMS_start && (HANData[HANBufferLength-1] == DLMSflag) )
                    {
                        // HAN data block complete
                        // take care of COSEM block
                        foreach ( byte b in HANData ) DLMSBlock.Add(b);
                        if (OOuCP.uCP.HANOODefaultParameters.LogDLMS) Console.WriteLine("DLMS block complete.");
                        DLMS_start = false;
                        Cosem.HANOOCOSEMBlock( DLMSBlock.ToArray(), OOuCP );
                        DLMSBlock.Clear();
                    }
                }

                Thread.Sleep(OOuCP.uCP.HANOODeviceData.delayBetweenReads);
            }

        }

    }
}