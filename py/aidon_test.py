#!/usr/bin/python

import serial, time, sys
from aidon_obis import *

if len (sys.argv) != 2:
	print ("Usage: ... <serial_port>")
	sys.exit(0)

def aidon_callback(fields):
	print (fields)

ser = serial.Serial(sys.argv[1], 2400, timeout=0.05, xonxoff=1, parity=serial.PARITY_EVEN, stopbits=serial.STOPBITS_ONE)
a = aidon(aidon_callback)

while(1):
    while ser.inWaiting():
        byte = ser.readline(1)
        a.decode(byte)
        print("In ser.inWaitin()", byte)
    time.sleep(0.01)
