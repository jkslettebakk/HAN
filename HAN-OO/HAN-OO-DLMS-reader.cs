
namespace HANDLMS
{
    public class OO_HAN_DLMS_Read
    {
        public SerialPort serialPort = new SerialPort();
        public OOUserConfigurationParameters ucpPort = new OOUserConfigurationParameters();
        public OOUserConfigurationParameters.UserConfigurationParameters ucp = new OOUserConfigurationParameters.UserConfigurationParameters();
        public byte[] HANData = new byte[100];

        public void OO_HAN_DLMS_Open()
        {
            Console.WriteLine("Opening port:{0}",ucp.HANOODeviceData.serialPortName);
            serialPort.Open();
        }

        public OO_HAN_DLMS_Read()
        {   // HANOODeviceData.HANDeviceName
            Console.WriteLine("Initialising/preparing \"HANDLMS\" objects and port reading");
        }

        public void OO_HAN_DLMS_ReadData( )
        {
            ucpPort.getPortParameters();
            Console.WriteLine("Prepare reading HAN port:{0}",ucp.HANOODeviceData.serialPortName);
            serialPort.PortName = ucp.HANOODeviceData.serialPortName;
            serialPort.BaudRate = ucp.HANOODeviceData.serialPortBaud;
            serialPort.Parity = ucp.HANOODeviceData.serialPortParity;
            serialPort.DataBits = ucp.HANOODeviceData.serialPortDataBits;
            serialPort.StopBits = ucp.HANOODeviceData.serialPortStopBits;
            OO_HAN_DLMS_Open();
        }

        public class DLMSdataBlock
        {
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