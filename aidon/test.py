import serial, string

output = " "
ser = serial.Serial('/dev/ttyUSB0', 2400, 8, 'E', 1, timeout=1)
while True:
  print "----"
  while output != "":
    output = ser.readline()
    print output
  output = " "
