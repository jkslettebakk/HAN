import serial
import codecs
import sys

ser = serial.Serial(
    port='/dev/ttyUSB0',
    baudrate=2400,
    parity=serial.PARITY_EVEN,
    stopbits=serial.STOPBITS_ONE,
    bytesize=serial.EIGHTBITS,
    timeout=4)
print("Connected to: " + ser.portstr)

while True:
    bytes = ser.read(4096)
    if bytes:
        print('\nGot %d bytes:' % len(bytes))
        bytes = ('%02x' % int(codecs.encode(bytes, 'hex'), 16)).upper()
        bytes = ' '.join(bytes[i:i+2] for i in range(0, len(bytes), 2))
        print(bytes)
    else:
        print('Got nothing')
