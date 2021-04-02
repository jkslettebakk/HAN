#!/usr/bin/python

import serial, time, sys
from aidon_obis import *

if len (sys.argv) != 2:
	print "Usage: ... <serial_port>"
	sys.exit(0)

def aidon_callback(fields):
	print "fields=", fields

ser = serial.Serial(sys.argv[1], baudrate=2400, bytesize=serial.EIGHTBITS, stopbits=serial.STOPBITS_ONE, parity=serial.PARITY_EVEN, timeout=4)
a = aidon(aidon_callback)

while(1):
	while ser.inWaiting():
		object = ser.read(1)
		print("{0}".format(object.encode("hex")))
		a.decode(object)
	time.sleep(0.01)

