# HAN

Home Area Network - a dotnet core (5.0) c# project aiming to

a) Read DLMS data fram HAN port and compleatly decififer and pull out a DLMS data block (0x7E..............0x7E)

b) Pull out a COSEM block from the "data" part of the DLMS data block

c) Interpreate OBIS objects and treat their data

d) send json block of data per OBIS object to the database

Typical DLMS block (Kamfa):

7E A0 E2 2B 21 13 23 9A E6 E7 00 0F 00 00 00 00 0C 07 E5 04 02 05 17 1A 1E FF 80 00 00 02
19 0A 0E 4B 61 6D 73 74 72 75 70 5F 56 30 30 30 31 09 06 01 01 00 00 05 FF 0A 10 35 37 30
36 35 36 37 32 38 30 32 31 32 37 33 35 09 06 01 01 60 01 01 FF 0A 12 36 38 34 31 31 32 31
42 4E 32 34 33 31 30 31 30 34 30 09 06 01 01 01 07 00 FF 06 00 00 08 AD 09 06 01 01 02 07
00 FF 06 00 00 00 00 09 06 01 01 03 07 00 FF 06 00 00 01 58 09 06 01 01 04 07 00 FF 06 00
00 00 00 09 06 01 01 1F 07 00 FF 06 00 00 03 67 09 06 01 01 33 07 00 FF 06 00 00 02 7E
