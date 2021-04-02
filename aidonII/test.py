import serial
import codecs
import sys

ser = serial.Serial(
    port='/dev/ttyUSB0',
    baudrate=2400,
    parity=serial.PARITY_NONE,
    stopbits=serial.STOPBITS_ONE,
    bytesize=serial.EIGHTBITS,
    timeout=4)
print("Connected to: " + ser.portstr)

while True:
    bytes = ser.read(1024)
    # print("Raw data from Aidon:\n{0}".format(bytes))
    if bytes:
        print('Got %d bytes:' % len(bytes))
        # print("Raw data from Aidon with content:\n{0}".format(bytes))
        bytes = ('%02x' % int(codecs.encode(bytes, 'hex'), 16))
        # print("After bytes encoding:\n{0}".format(bytes))
        bytes = ' '.join(bytes[i:i+2] for i in range(0, len(bytes), 2))
        # print("After space:\n{0}".format(bytes))
        if (bytes[0:2] == "7E" or bytes[0:2] == "7e"):         # Test frame startflag (Would have loved using C# and Case statement)
            print("Framekey match! Key = {0}".format(bytes[0:2]))
        else:
            print("No match. bytes[0:2]={0}, 7E={1}".format(bytes[0:2],b'\x7E'))
    else:
        print('Got nothing')