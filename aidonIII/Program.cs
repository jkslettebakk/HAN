using System;
using System.IO.Ports;
using System.Text;
using System.Collections.Generic;
using System.Collections;


namespace aidon
{

    ///<summary>
    ///DPA RX class
    ///</summary>
    ///<remarks>
    ///This class handles RX performance of DPA Protol
    ///</remarks>
    public class DPA_RX
    {

        public enum DPA_RX_STATE
        {
            DPA_RX_NOERR,       // default state
            DPA_RX_OK,          // OK, message parsed
            DPA_RX_FE,          // Frame error
            DPA_RX_CRCERR       // CRC error
        }

        UInt16 NADR;             // Node address
        byte PNUM;               // Peripheral number
        byte PCMD;               // Peripheral command
        UInt16 HWPID;            // Hardware profile ID
        byte[] Data;             // DPA Data
        byte DLEN;               // DPA Data length
        byte ErrN;               // Error number
        byte DpaValue;

        private bool HDLC_CE;           // control escape flag
        private byte CRC;               // HDLC CRC
        ArrayList Buffer;        // Input buffer

        const byte HDLC_FLAG_SEQUENCE = 0x7e;    // Flag sequence constant
        const byte HDLC_CONTROL_ESCAPE = 0x7d;   // Control escape constant
        const byte HDLC_ESCAPE_BIT = 0x20;       // Escape bit constant
        const byte HDLC_MIN_LEN = 0x0B;          // Minimum length of response
        const byte HDLC_MAX_LEN = 0x80;          // Maximum length of buffer


        // Constructor
        public DPA_RX()
        {
            NADR = 0x0000;              // Reset Node address
            PNUM = 0x00;                // Reset peripheral number
            PCMD = 0x00;                // Reset peripheral command
            HWPID = 0xFFFF;             // All HWPIDs
            Data = new byte[55];        // New DPA data array
            DLEN = 0;                   // Reset Data length
            ErrN = 0;
            DpaValue = 0;
            

            CRC = 0x00;                 // Reset CRC          
            Buffer = new ArrayList();
        }


        // This method try to parse DPA message against incomming character
        public DPA_RX_STATE DPA_RX_Parse(byte character)
        {
            DPA_RX_STATE ret_val = DPA_RX_STATE.DPA_RX_NOERR;
            // Console.WriteLine("DPA_RX Buffer length:{0}",Buffer.Count);

            if (character == HDLC_FLAG_SEQUENCE)        // flag sequence
            {
                // first Flag sequnce
                if (Buffer.Count == 0)
                {
                    Buffer.Add((byte)character);
                    HDLC_CE = false;
                }
                else
                {
                    // It is error state - too short message
                    if (Buffer.Count < (HDLC_MIN_LEN-1))
                    {
                        // Maybe it is start of new frame...
                        Console.WriteLine("\nNew frame? Print old first:");
                        foreach ( byte b in Buffer ) Console.Write("{0:x2}",b);
                        Buffer.Clear(); Buffer.Add(character);
                        return DPA_RX_STATE.DPA_RX_FE;
                    }
                    // Correct length
                    else
                    {
                        // Check CRC                        
                        Buffer.RemoveAt(0); // remove first Flag sequnce                       
                        byte crc = DPA_UTILS.CalcCRC(Buffer);
                        // CRC is OK
                        if (crc == 0)
                        {
                            byte[] tmpBuffer = new byte[Buffer.Count];
                            Buffer.CopyTo(tmpBuffer);                   
                            NADR = tmpBuffer[1]; 
                            NADR = (UInt16)(NADR<<8);          // NADH.high8
                            NADR |= tmpBuffer[0];              // NADR.low8
                            PNUM = tmpBuffer[2];               // PNUM
                            PCMD = tmpBuffer[3];               // PCMD
                            HWPID = tmpBuffer[5]; 
                            HWPID = (UInt16)(HWPID << 8);       // HWPID.high8
                            HWPID |= tmpBuffer[4];              // HWPID.low8                            
                            ErrN = tmpBuffer[6];
                            DpaValue = tmpBuffer[7];
                            CRC = tmpBuffer[8];
                            Console.WriteLine("\nCRC ok.\nTømmer buffer:");
                            foreach ( byte b in Buffer ) Console.Write("{0:x2}",b);
                            Buffer.Clear(); 
                            return DPA_RX_STATE.DPA_RX_OK;
                        }
                        // CRC is not OK
                        else
                        {
                            // Maybe it is start of new frame...
                            Console.WriteLine("\nNew frame?:");
                            foreach ( byte b in Buffer ) Console.Write("{0:x2}",b);
                            Buffer.Clear(); Buffer.Add((byte)0x7E);
                            return DPA_RX_STATE.DPA_RX_CRCERR;
                        }
                    }
                }

            }
            else // if another character received
            {
                // if it is not the first character and length is within borders
                if ((Buffer.Count > 0) && (Buffer.Count < HDLC_MAX_LEN))
                {
                    // if Control Esape received
                    if (character == HDLC_CONTROL_ESCAPE)
                        HDLC_CE = true;
                    else
                    {   // else insert character
                        if (HDLC_CE == false)
                            Buffer.Add((byte)character);
                        else
                        {
                            HDLC_CE = false;
                            Buffer.Add((byte)character ^ (byte)HDLC_ESCAPE_BIT);
                        }
                    }
                }
            }

            return ret_val;
        }

    }

    ///<summary>
    ///DPA TX class
    ///</summary>
    ///<remarks>
    ///This class handles TX performance of DPA Protol
    ///</remarks>
    public class DPA_TX
    {
        UInt16 NADR;             // Node address
        byte PNUM;               // Peripheral number
        byte PCMD;               // Peripheral command
        UInt16 HWPID;            // Hardware profile ID
        byte[] Data;             // DPA Data
        byte DLEN;               // DPA Data length
        
        private byte CRC;               // HDLC CRC
        byte[] Buffer;           // Output buffer

        // Constructor
        public DPA_TX()
        {
            NADR = 0x0000;              // Reset Node address
            PNUM = 0x00;                // Reset peripheral number
            PCMD = 0x00;                // Reset peripheral command
            HWPID = 0xFFFF;             // All HWPIDs
            Data = new byte[55];        // New DPA data array
            DLEN = 0;                   // Reset Data length

            CRC = 0x00;                 // Reset CRC            
        }
        
        // This method fill compile DPA message to HDLC
        void BuildHDLC()
        {
            byte temp;
            ArrayList tmpBuffer = new ArrayList();
            ArrayList tmpBuffer2 = new ArrayList();

            // Clear buffer
            tmpBuffer.Clear();
            
            // Add NADR
            temp = (byte)NADR;
            tmpBuffer.Add(temp);
            temp = (byte)(NADR >> 8);
            tmpBuffer.Add(temp);

            // Add PNUM
            tmpBuffer.Add(PNUM);

            // Add PCMD
            tmpBuffer.Add(PCMD);

            // Add HWPID
            temp = (byte)HWPID;
            tmpBuffer.Add(temp);
            temp = (byte)(HWPID >> 8);
            tmpBuffer.Add(temp);

            // Add Data
            for (byte i = 0; i < DLEN; i++)
                tmpBuffer.Add(Data[i]);

            // Add CRC
            temp = DPA_UTILS.CalcCRC(tmpBuffer);
            tmpBuffer.Add(temp);

            // Make a HDLC frame
            tmpBuffer2.Clear();
            tmpBuffer2.Add((byte)0x7E);    // Flag sequence
            foreach (byte tmp in tmpBuffer)
            {
                temp = tmp;
                // If there is Flag sequence or Control escape
                if ((temp == (byte)0x7D) || (temp == (byte)0x7E))
                {
                    tmpBuffer2.Add((byte)0x7D);
                    tmpBuffer2.Add((byte)(temp ^ 0x20));
                }
                else
                {
                    tmpBuffer2.Add((byte)temp);
                }
            }
            tmpBuffer2.Add((byte)0x7E);    // Flag sequence

            // Copy result to Buffer
            Buffer = new byte[tmpBuffer2.Count];
            tmpBuffer2.CopyTo(Buffer);
        }        

    }


    ///<summary>
    ///DPA UTILS class
    ///</summary>
    ///<remarks>
    ///This class helps with CRC and many other things in future
    ///</remarks>
    public class DPA_UTILS
    {

        // return IQRF DPA CRC
        public static byte CalcCRC(ArrayList buffer)
        {
            byte crc = 0xFF;

            foreach (byte val in buffer)
            {
                byte value = val;
                for (int bitLoop = 8; bitLoop != 0; --bitLoop, value >>= 1)
                    if (((crc ^ value) & 0x01) != 0)
                        crc = (byte)((crc >> 1) ^ 0x8C);
                    else
                        crc >>= 1;
            }
            return crc;
        }

    }

    public struct arguments
    {
        public void ArgumentData( string[] args )
        {
            for ( int i = 0; i < args.Length; i++)
            {
                var argument = args[i].ToUpper();
                if ( argument == "-V" )
                {
                    verbose = true;
                }
                else if ( argument == "-R" )
                {
                    raw = true;
                } else if ( argument == "-S" )
                {
                    Console.WriteLine("-s not implemented. value added = {0}",args[i+1]);
                    i++;
                }
                else
                {
                    Console.WriteLine("Error in parameters {0}. Argument unknown.",argument);
                }
            }
        }
        bool verbose {
            get { return verbose; }
            set { verbose = false; }
            }
        bool raw {
            get { return raw; }
            set { raw = false;}
            }
    }

    class Program
    {
        static SerialPort serialPort;
        static void Main(string[] args)
        {
            serialPort = new SerialPort("/dev/ttyUSB0");
            serialPort.BaudRate = 2400;
            serialPort.Parity = Parity.None; // 0=None, 1=Odd, 2=Even, 3=Mark, 4=Space
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One; // 0=None, 1=One, 2=Two, 3=OnePointFive
            byte[] bytes;
            string byteString;
            arguments a;
            string[] ports = SerialPort.GetPortNames();
            int right = 0;
            int bytesLength;
            DPA_RX dpa_rx = new DPA_RX();
            DPA_TX dpa_tx = new DPA_TX();

            if (args.Length > 0 ) a.ArgumentData( args );

            foreach ( var port in ports )
            {
                Console.WriteLine("Port: {0}", port );
            }

            serialPort.Open();
            // byteString = serialPort.ReadLine();
            try
            {
                while ( true )
                {
                    byteString = serialPort.ReadLine();
                    bytes = Encoding.ASCII.GetBytes(byteString);
                    bytesLength = byteString.Length;
                    // Console.WriteLine("{0:x}",byteString);
                    Console.WriteLine("\n***** New data block *****\n******* {0} bytes ******\n",bytesLength);

                    // for ( int i = 0; i < bytesLength; i++ )
                    foreach( byte b in bytes )
                    {
                        dpa_rx.DPA_RX_Parse((byte) b);

                    /*
                        if ( bytes[i] == 0x7E )
                            {
                                int j = 4;
                                Console.WriteLine("\nHit 0x7E - next byte value and bits:{0:x2}, {1}",bytes[i+1],PadNibble(bytes[i+1]));
                                Console.WriteLine("Frame Format DLSM/COSEM value {0} and bits:{1}",(bytes[i+1]>>4),PadNibble((byte) unchecked(bytes[i+1]>>4)));
                                Console.WriteLine("Next byte bit {0} has value {1}",j, IsBitSet(bytes[i+1],j));

                                if ( (bytes[i+1] & 0xA) == 0xA )
                                {
                                    Console.WriteLine("\nStart of information block. Lengde = {0}",bytes[i+1]);
                                    right = -1;
                                }
                                else
                                {
                                    // end of block or data?
                                    Console.WriteLine("end of block?");
                                }
                            }
                        right++;
                        if ( right >= 20 )
                        {
                            Console.WriteLine();
                            right = 0;
                        }
                        Console.Write(" {0:x2}",bytes[i]);
                    */

                    }
                    //Console.WriteLine();
                }
            }
            catch ( System.Exception ex )
            {
                Console.WriteLine("Exit:\n{0}",ex);
                serialPort.Close();
            }
            serialPort.Close();

            Console.WriteLine("Hello World!, bye, bye");
            
        bool IsBitSet(byte b, int pos)
            {
                return ((b >> pos) & 1) != 0;
            }
        static string PadNibble(byte b)
            {
                return Int32.Parse(Convert.ToString(b, 2)).ToString("0000 0000");
            }
        }
    }
}
