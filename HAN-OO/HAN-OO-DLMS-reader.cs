
namespace HANDLMS
{
    public class OO_HAN_DLMS_Read
    {
        SerialPort serialPort = new SerialPort();
        OOUserConfigurationParameters.UserConfigurationParameters ucp = new OOUserConfigurationParameters.UserConfigurationParameters();
        byte[] HANData = new byte[100];

        public void OO_HAN_DLMS_Open()
        {
            Console.WriteLine("Opening port:{0}",ucp.HANOODeviceData.serialPortName);
            serialPort.Open();
        }

        public OO_HAN_DLMS_Read()
        {
            if ( ucp.HANOODeviceData.HANDeviceName == "KAMSTRUP" )
                serialPort.Parity = Parity.None; // 0=None, 1=Odd, 2=Even, 3=Mark, 4=Space
        }

        public class DLMSdataBlock
        {
        }
        public void OO_HAN_DLMS_ReadData( SerialPort port)
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