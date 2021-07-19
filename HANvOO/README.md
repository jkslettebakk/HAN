
Rewrite HAN Reader to better use the OBIS object code structure

Class & Object structure based upon:
*   COSEM   - COmpanion Specification for Energy Metering (code for text, numbers etc in meetering systems)
*   DLMS    - Device Language Message Specification
*   OBIS    - Object Identification System - Energy Meetering system (Electricity/Gas/Water etc) objects

Read more here:    https://en.wikipedia.org/wiki/IEC_62056

CRC16   - Cyclic Redundance Ceck of data recieved from the HAN (Home Area Netwotk/Metering system) port 

19.07.21 -

Updated to .NET 6 beta

Aim to rewrite the OBIS class & Objects
1)  Use OBIS code object structure for object property data management (Power Reading etc)
2)  Rewrite DLMS/COSEM 