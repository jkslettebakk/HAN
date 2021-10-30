
namespace HANDLMS
{
    public class OO_HAN_Read_DLMS
    {

        byte[] HANData = new byte[100];

        public void OO_HAN_DLMS_Open()
        {
            Console.WriteLine("Opening port:??");
            serialPort.Open();
        }

        public OO_HAN_Read_DLMS()
        {
            /* if ( ucp.HANOODeviceData.HANDeviceName == "KAMSTRUP" )
                serialPort.Parity = Parity.None; // 0=None, 1=Odd, 2=Even, 3=Mark, 4=Space
                Console.WriteLine("KAMSTRUP parity set to \"Non\""); */
        }

        public class DLMSdataBlock
        {
        }
        
        public void OO_HAN_Read_DLMS_Data( SerialPort port)
        {
            Console.WriteLine("Reading port {0}",port.PortName);

        }

/*
        public bool OO_HAN_DLMS_ReadData( SerialPort port)
        {

        }

        public List<byte> OO_HAN_DLMS_ReadData( SerialPort port )
        {

        }
*/
    }
}