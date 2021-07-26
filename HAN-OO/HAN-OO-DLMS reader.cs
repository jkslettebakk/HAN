using System.IO.Ports;

namespace HANDLMS
{
    public class OO_HAN_DLMS_Read
    {
            public List<byte> DLMSblock = new List<byte>();
            public List<byte> COSEMblock = new List<byte>();
            // static SerialPort serialPort;

        public class DLMSdataBlock
        {
        }

        public void OO_HAN_DLMS_Read()
        {
            if ( HANDevice = KAMSTRUP )
                serialPort.Parity = Parity.None; // 0=None, 1=Odd, 2=Even, 3=Mark, 4=Space
        }
/*
        public bool OO_HAN_DLMS_Read( SerialPort port)
        {

        }

        public List<byte> OO_HAN_DLMS_Read( SerialPort port )
        {

        } */
    }
}