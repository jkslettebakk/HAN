#undef DEBUG
#undef DLSMDEBUG
#undef COSEMDEBUG
#undef OBISDEBUG
#define JSONDEBUG
#define COSEMSTRUCTURE

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HAN_OBIS;

namespace HAN_COSEM
{
        class COSEMClass
        {
            List<byte> cOSEMHeadingData = new List<byte>();
            List<byte> cOSEMData = new List<byte>();
            int cosemHeadingDataLength = 9;
            int cosemDataLength;
            int dLMSstartCosemBlock = 9;
            byte[] LLC = new byte[3]; // LLC (Logic Link Control) frame header, E6 E7 00
            byte[] UIFrameHeade = new byte[6]; // (00 00 00 00)
            public byte[] cOSEMtypeobject = new byte[2]; // Object or Array
            public string tabLength = "\t";
            obisCodesClass obisCode = new obisCodesClass();

            public void cOSEMInitialize()
            {
                cOSEMtypeobject[0] = 0x02; // initialize to two
                cOSEMtypeobject[1] = 0x00; // initiated to zero
#if OBISDEBUG
                obisCode.showObisValues();
#endif
            }

            void cOSEMHeading()
            {
                LLC[0] = cOSEMHeadingData[0]; // E6 (Aidon values? General meetering values?)
                LLC[1] = cOSEMHeadingData[1]; // E7
                LLC[2] = cOSEMHeadingData[2]; // 00
                UIFrameHeade[0] = cOSEMHeadingData[3]; // 0F
                UIFrameHeade[1] = cOSEMHeadingData[4]; // 40
                UIFrameHeade[2] = cOSEMHeadingData[5]; // 00
                UIFrameHeade[3] = cOSEMHeadingData[6]; // 00
                UIFrameHeade[4] = cOSEMHeadingData[7]; // 00
                UIFrameHeade[5] = cOSEMHeadingData[8]; // 00
            }



            string cOSEMObisJsonCode(List<byte> data, int start, int length)
            {
                string jsonString = ",\n\'obis code\' : \'";
                string cosemTemp = "";
#if DEBUG
                    Console.WriteLine("cOSEMObisCode input: start={0},length={1}",start,length);
                    for ( int k = start; k < (start + length + 1); k++)  Console.Write("{0:X2}.",cOSEMData[k]);
                    Console.WriteLine();
                    Console.Write("Obis Code: ");
#endif

                cosemTemp = obisCode.oBISCode(data, start);
                jsonString += cosemTemp + "\',\n";

#if DEBUG
                    Console.WriteLine($"Obis found? Return = {0}",cosemTemp);
#endif
                return jsonString;
            }

            public void cOSEMDataBlock(List<byte> dLMScOSEMData)
            {
                cOSEMHeadingData.Clear(); // Prepare new COSEM data heading
                cOSEMData.Clear(); // Prepare new COSEM data
                for (int i = dLMSstartCosemBlock; i < dLMSstartCosemBlock + cosemHeadingDataLength; i++)  // extract COSEM heading from DLMS block
                    cOSEMHeadingData.Add(dLMScOSEMData[i]);  // get COSEMClass data extracted out of DLMS data block
                for (int i = dLMSstartCosemBlock + cosemHeadingDataLength; i < dLMScOSEMData.Count - 3; i++)  // extract COSEM block from DLMS block
                    cOSEMData.Add(dLMScOSEMData[i]);  // get COSEMClass data extracted out of DLMS data block
                // cosemHeadingDataLength = cOSEMHeadingData.Count;
                cosemDataLength = cOSEMData.Count;
                cOSEMHeading();
                string structJson = "";
                int cosemBlockStart = 0;
#if KAMSTRUP
                cosemBlockStart = 23;
#endif


                if (cOSEMData[cosemBlockStart] == 0x01) // Array of x (cOSEMData[1]) elements (lines)
                {
#if COSEMSTRUCTURE
                    Console.WriteLine("New COSEM Data block.");
                    for (int i = 0; i < cosemDataLength; i++) // for debug purposes
                    {
                        Console.Write("{0:X2} ", cOSEMData[i]);
                    }
                    Console.WriteLine("\nKey values in block: Array type={0:X2} with {1:X2} array objects and length = {2}", cOSEMData[0], cOSEMData[1], cOSEMData.Count);
#endif
                    int cOSEMBufferPointer = 1; // Prepare handling of COSEM data block from Meeter

                    for (int i = 0; i < cOSEMData[1]; i++) // Loop trough all Array objects (normally 1 or 12 for Aidon; 01 0C)
                    {

                        cOSEMBufferPointer++; // Increase counters to start on "sctructure" elements
                        Console.WriteLine("Loop = {0}, cOSEMBufferPointer={1}", i, cOSEMBufferPointer);

                        string currentCOSEMString = "";

                        switch (cOSEMData[cOSEMBufferPointer]) // switch on Structure (0x02) or .... (0x03) and prepare for length (++)
                        {
                            case 0x02: // Structure with 2 elements (COSEM Code + data Text about the Mettering system)
                            case 0x03: // Structure with Metering data (3 elements; 1=obis, 2=value/data, 3=UoM)
                                cOSEMBufferPointer++;
                                /* Sample structure 0x02 
                                01 0C // 01 12 = 01=Array of 12 elements
                                      0202 // Structure of 02 elements
                                           0906 // Octet of 0x06 elements
                                                01 01 00 02 81 FF // OBIS list version identifyer 
                                           0A0B // Visible string mark (0x0A) of 0x0B elements
                                                41 49 44 4F 4E 5F 56 30 30 30 31 // "A I D O N _ V 0 0 0 1"
                                      0202 // Struct (02) of 02 elements
                                           0906 // Octet (0x09) of 0x06 elements
                                                00 00 60 01 00 FF // Metering point ID
                                           0A10 // Visible string (= 10) of 0x10 (16) elements
                                                37 33 35 39 39 39 32 39 30 35 34 37 38 33 36 32 // "7 3 5 9 ......"
                                      ........
                                */
                                int numStructures = cOSEMData[cOSEMBufferPointer++];
                                for ( int structures = 0; structures < numStructures; structures++)
                                {
                                    int k; // used in loops
                                    switch (cOSEMData[cOSEMBufferPointer])
                                    {
                                        case 0x09: // octet string, OBIS code (e.g 01.00.01.07.00.FF = Active Power Q1+Q4)
                                            cOSEMBufferPointer++;
                                            Console.WriteLine("In 0x09 octet string in case(0x02). cOSEMBufferPointer = {0}", cOSEMBufferPointer);
                                            currentCOSEMString = obisCode.oBISCode(cOSEMData, cOSEMBufferPointer + 1);
                                            cOSEMBufferPointer +=6+1; // COSEM code = 6, +1 for next object
                                            Console.WriteLine("OBIS Code={0}",currentCOSEMString);
                                            break;
                                        case 0x0A: // Visible string
                                            cOSEMBufferPointer++;
                                            if (cOSEMBufferPointer >= cosemDataLength) break; // increase counter to start on Structure elements
                                            Console.WriteLine("In 0x0A visible text. cOSEMBufferPointer = {0}", cOSEMBufferPointer);
                                            string localText = "";
    #if DEBUG
                                                Console.Write("Visible string: ");
    #endif
    #if COSEMSTRUCTURE
                                            Console.Write("\t\t{0:X2} {1:X2}", cOSEMData[cOSEMBufferPointer - 1], cOSEMData[cOSEMBufferPointer]);
    #endif
                                            for (k = (cOSEMBufferPointer + 1); k < (cOSEMBufferPointer + 1) + (cOSEMData[cOSEMBufferPointer]); k++)
                                            {
                                                localText += (char)cOSEMData[k];
    #if DEBUG || COSEMSTRUCTURE
                                                if (k == (cOSEMBufferPointer + 1)) Console.Write("\t");
                                                Console.Write("{0}", (char)cOSEMData[k]);
    #endif
                                            }
    #if DEBUG || COSEMSTRUCTURE
                                            Console.WriteLine();
    #endif
                                            // structJson += "\'\n}";
                                            cOSEMBufferPointer += cOSEMData[cOSEMBufferPointer];
                                            Console.WriteLine("COSEM Code = {0}", currentCOSEMString);
                                            switch (currentCOSEMString)
                                            {
                                                case "1.1.0.2.129.255":
                                                    structJson += ",\n\'List version identifier\' : \'" + localText + "\'";
                                                    break;
                                                case "0.0.96.1.0.255":
                                                    structJson += ",\n\'Model serial number\' : \'" + localText + "\'";
                                                    break;
                                                case "0.0.96.1.7.255":
                                                    structJson += ",\n\'Model type\' : \'" + localText + "\'";
                                                    break;
                                                default:
                                                    Console.WriteLine("Obis code not implemented? - {0}", localText);
                                                    break;
                                            }
                                            Console.WriteLine("debug: {0}", structJson);
                                            break;
                                        case 0x06: // Metering data power (Aidon=)
                                            cOSEMBufferPointer++;
                                            if (cOSEMBufferPointer >= cosemDataLength) break; // increase counter to start on Structure elements
                                            Console.WriteLine("In 0x06 - meetering data power. cOSEMBufferPointer = {0}", cOSEMBufferPointer);
    #if DEBUG
                                                Console.Write("Data type unsigned32 : ");
    #endif
                                            int unsignedLength = 10;
    #if COSEMSTRUCTURE
                                            Console.Write("\t\t{0:X2} {1:X2}", cOSEMData[cOSEMBufferPointer - 1], cOSEMData[cOSEMBufferPointer]);
    #endif
                                            structJson += ",\n\'Metering Power \' : \'";
                                            for (k = (cOSEMBufferPointer); k < (cOSEMBufferPointer) + unsignedLength; k++)
                                            {
                                                structJson += cOSEMData[k];
    #if DEBUG
                                                Console.Write("{0:X2}", cOSEMData[k]);
    #endif
                                            }
    #if DEBUG
                                            Console.WriteLine();
    #endif
                                            structJson += "\'";
                                            cOSEMBufferPointer += 11; // Last element used
                                            Console.WriteLine("Power loop done (k={0}).", k);
                                            break;
                                        case 0x10: // Meetering data current L1 & L3
                                            cOSEMBufferPointer++;
                                            if (cOSEMBufferPointer >= cosemDataLength) break; // increase counter to start on Structure elements
                                            Console.WriteLine("In 0x10 - meetering data current L1&L3. cOSEMBufferPointer = {0}", cOSEMBufferPointer);
    #if DEBUG
                                                Console.Write("Data type Current L1 : ");
    #endif
                                            unsignedLength = 10;
                                            structJson += ",\n\'Metering current \' : \'";
                                            for (k = (cOSEMBufferPointer + 1); k < (cOSEMBufferPointer + 1) + unsignedLength; k++)
                                            {
                                                structJson += cOSEMData[k];
    #if DEBUG
                                                Console.Write("{0:X2}", cOSEMData[k]);
    #endif
                                            }
    #if DEBUG
                                                Console.WriteLine();
    #endif
                                            structJson += "\'";
                                            cOSEMBufferPointer += 6; // Last element used
                                            break;
                                        case 0x12: // Meetering data Phase VL1 & VL2 & VL3
                                            cOSEMBufferPointer++;
                                            if (cOSEMBufferPointer >= cosemDataLength) break; // increase counter to start on Structure elements
                                            Console.WriteLine("In 0x12 - meetering data phase VL1&VL3. cOSEMBufferPointer = {0}", cOSEMBufferPointer);
    #if DEBUG
                                                Console.Write("Data type Phase VL1 & VL2 & VL3 : ");
    #endif
                                            unsignedLength = 8;
                                            structJson += ",\n\'Metering Phase \' : \'";
                                            for (k = (cOSEMBufferPointer + 1); k < (cOSEMBufferPointer + 1) + unsignedLength; k++)
                                            {
                                                structJson += cOSEMData[k];
    #if DEBUG
                                                Console.Write("{0:X2}", cOSEMData[k]);
    #endif
                                            }
    #if DEBUG
                                                Console.WriteLine();
    #endif
                                            structJson += "\'";
                                            cOSEMBufferPointer += 6; // Last element used
                                            break;
                                        case 0x16: // Metering KV UoM
                                            cOSEMBufferPointer++;
                                            if (cOSEMBufferPointer >= cosemDataLength) break; // increase counter to start on Structure elements
                                            Console.WriteLine("In 0x16 - meetering data ....... cOSEMBufferPointer = {0}", cOSEMBufferPointer);
    #if DEBUG
                                                Console.Write("Data type ......... : ");
    #endif
                                            structJson += "\'";
                                            cOSEMBufferPointer += 1; // Last element used
                                            break;
                                        default:
                                            if (cOSEMBufferPointer < cosemDataLength) cOSEMBufferPointer++;
                                            Console.WriteLine("{0}Inner Switch. Value not handled yet. Value = {1:X2} in location {2}", tabLength, cOSEMData[cOSEMBufferPointer], cOSEMBufferPointer);
                                            Console.WriteLine("Outer 0x09,0x0A loop. Value = {0:X2} in location {1} not handled yet", cOSEMData[cOSEMBufferPointer - 1], (cOSEMBufferPointer - 1));
                                            break;
                                        }
                                }
                                    structJson += "\n}";
#if COSEMDEBUG
                                        Console.WriteLine("{0}",structJson);
#endif
#if DEBUG || COSEMDEBUG
                                    Console.WriteLine("Structure 0x02 done. Result:\n{0}",structJson);
#endif
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Unknown type ({0:X2}) ***************** with ******************* {1:X2} elements", cOSEMData[9], cOSEMData[10]);
                }
            }

            public void cOSEMDebug(string leadingText, List<byte> byteList, bool eol)
            {
                Console.WriteLine(leadingText);
                foreach (byte b in byteList) Console.Write("{0:X2} ", b);
                if (eol) Console.WriteLine();
            }

        }
}

