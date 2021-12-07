# HAN \#(Home Automation Network)

This is a dotnet core (3.1) c# project aiming to

a) Read DLMS data fram HAN port and compleatly decififer and pull out a DLMS data block (0x7E..............0x7E)

b) Pull out a COSEM block from the "data" part of the DLMS data block

c) Interpreate OBIS objects and treat their data

d) send json block of data per OBIS object to the database

Typical DLMS data blocks -
Kamstrup block:

7E A0 E2 2B 21 13 23 9A E6 E7 00 0F 00 00 00 00 0C 07 E5 07 13 01 10 15 0A FF 80 00 00 02
19 0A 0E 4B 61 6D 73 74 72 75 70 5F 56 30 30 30 31 09 06 01 01 00 00 05 FF 0A 10 35 37 30
36 35 36 37 32 38 30 32 31 32 37 33 35 09 06 01 01 60 01 01 FF 0A 12 36 38 34 31 31 32 31
42 4E 32 34 33 31 30 31 30 34 30 09 06 01 01 01 07 00 FF 06 00 00 05 47 09 06 01 01 02 07
00 FF 06 00 00 00 00 09 06 01 01 03 07 00 FF 06 00 00 01 78 09 06 01 01 04 07 00 FF 06 00
00 00 00 09 06 01 01 1F 07 00 FF 06 00 00 02 6B 09 06 01 01 33 07 00 FF 06 00 00 01 EE 09
06 01 01 47 07 00 FF 06 00 00 00 8C 09 06 01 01 20 07 00 FF 12 00 E6 09 06 01 01 34 07 00
FF 12 00 E5 09 06 01 01 48 07 00 FF 12 00 E7 08 1D 7E

Kamstrup COSEM block look like this (data inside the DLMS block):

0C 07 E5 07 13 01 10 15 0A FF 80 00 00 02
19 0A 0E 4B 61 6D 73 74 72 75 70 5F 56 30 30 30 31 09 06 01 01 00 00 05 FF 0A 10 35 37 30
36 35 36 37 32 38 30 32 31 32 37 33 35 09 06 01 01 60 01 01 FF 0A 12 36 38 34 31 31 32 31
42 4E 32 34 33 31 30 31 30 34 30 09 06 01 01 01 07 00 FF 06 00 00 05 47 09 06 01 01 02 07
00 FF 06 00 00 00 00 09 06 01 01 03 07 00 FF 06 00 00 01 78 09 06 01 01 04 07 00 FF 06 00
00 00 00 09 06 01 01 1F 07 00 FF 06 00 00 02 6B 09 06 01 01 33 07 00 FF 06 00 00 01 EE 09
06 01 01 47 07 00 FF 06 00 00 00 8C 09 06 01 01 20 07 00 FF 12 00 E6 09 06 01 01 34 07 00
FF 12 00 E5 09 06 01 01 48 07 00 FF 12 00 E7

Aidon DLMS block:

7e a1 0b 41 08 83 13 fa 7c e6  e7 00 0f 40 00 00 00 00 01 0c  02 02 09 06 01 01 00 02 81 ff  0a 0b 41 49 44 4f 4e 5f 56 30
30 30 31 02 02 09 06 00 00 60  01 00 ff 0a 10 37 33 35 39 39  39 32 39 30 35 34 37 38 33 36  32 02 02 09 06 00 00 60 01 07
ff 0a 04 36 35 32 35 02 03 09  06 01 00 01 07 00 ff 06 00 00  20 83 02 02 0f 00 16 1b 02 03  09 06 01 00 02 07 00 ff 06
00 00 00 00 02 02 0f 00 16 1b  02 03 09 06 01 00 03 07 00 ff  06 00 00 00 00 02 02 0f 00 16  1d 02 03 09 06 01 00 04 07 00
ff 06 00 00 01 ef 02 02 0f 00  16 1d 02 03 09 06 01 00 1f 07  00 ff 10 00 86 02 02 0f ff 16  21 02 03 09 06 01 00 47 07 00
ff 10 00 c0 02 02 0f ff 16 21  02 03 09 06 01 00 20 07 00 ff  12 08 fe 02 02 0f ff 16 23 02  03 09 06 01 00 34 07 00 ff 12
09 06 02 02 0f ff 16 23 02 03  09 06 01 00 48 07 00 ff 12 08  fa 02 02 0f ff 16 23 b5 14 7e

For Aidon the OBIS block look like this (data inside the DLMS block above):

01 0C 02 02 09 06 01 01 00 02  81 FF 0A 0B 41 49 44 4F 4E 5F  56 30 30 30 31 02 02 09 06 00  00 60 01 00 FF 0A 10 37 33 35
39 39 39 32 39 30 35 34 37 38  33 36 32 02 02 09 06 00 00 60  01 07 FF 0A 04 36 35 32 35 02  03 09 06 01 00 01 07 00 FF 06
00 00 20 83 02 02 0F 00 16 1B  02 03 09 06 01 00 02 07 00 FF  06 00 00 00 00 02 02 0F 00 16  1B 02 03 09 06 01 00 03 07 00
FF 06 00 00 00 00 02 02 0F 00  16 1D 02 03 09 06 01 00 04 07  00 FF 06 00 00 01 EF 02 02 0F  00 16 1D 02 03 09 06 01 00 1F
07 00 FF 10 00 86 02 02 0F FF  16 21 02 03 09 06 01 00 47 07  00 FF 10 00 C0 02 02 0F FF 16  21 02 03 09 06 01 00 20 07 00
FF 12 08 FE 02 02 0F FF 16 23  02 03 09 06 01 00 34 07 00 FF  12 09 06 02 02 0F FF 16 23 02  03 09 06 01 00 48 07 00 FF 12
08 FA 02 02 0F FF 16 23
