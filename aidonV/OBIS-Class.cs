#define OBISDEBUG


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Slettebakk_OBIS
{
    class obisCodesClass
    {
        struct obisCodesStruct
        {
            public byte A { get; set; }
            public byte B { get; set; }
            public byte C { get; set; }
            public byte D { get; set; }
            public byte E { get; set; }
            public byte F { get; set; }
            public string UoM { get; set; }
            public string obis { get; set; }
        }

        //a obis struct list
        List<obisCodesStruct> legalObisCodes_Aidon = new List<obisCodesStruct>{
                new obisCodesStruct {A=1, B=0, C=1,  D=7, E=0,   F=255, obis= "1.0.1.7.0.255",  UoM= "kW"},
                new obisCodesStruct {A=1, B=1, C= 0, D=2, E=129, F=255, obis= "1.1.0.2.129.255",UoM= "(Vendor version)"},
                new obisCodesStruct {A=0, B=0, C=96, D=1, E=  0, F=255, obis= "0.0.96.1.0.255", UoM= "(Model serial number)"},
                new obisCodesStruct {A=0, B=0, C=96, D=1, E=  7, F=255, obis= "0.0.96.1.7.255", UoM= "(Model type)"},
                new obisCodesStruct {A=1, B=0, C= 2, D=7, E=  0, F=255, obis= "1.0.2.7.0.255",  UoM= "kW"},
                new obisCodesStruct {A=1, B=0, C= 3, D=7, E=  0, F=255, obis= "1.0.3.7.0.255",  UoM= "kVAr"},
                new obisCodesStruct {A=1, B=0, C= 4, D=7, E=  0, F=255, obis= "1.0.4.7.0.255",  UoM= "kVAr"},
                new obisCodesStruct {A=1, B=0, C=31, D=7, E=  0, F=255, obis= "1.0.31.7.0.255", UoM= "A"},
                new obisCodesStruct {A=1, B=0, C=51, D=7, E=  0, F=255, obis= "1.0.51.7.0.255", UoM=  "A"},
                new obisCodesStruct {A=1, B=0, C=71, D=7, E=  0, F=255, obis= "1.0.71.7.0.255", UoM=  "A"},
                new obisCodesStruct {A=1, B=0, C=32, D=7, E=  0, F=255, obis= "1.0.32.7.0.255", UoM= "V"},
                new obisCodesStruct {A=1, B=0, C=52, D=7, E=  0, F=255, obis= "1.0.52.7.0.255", UoM= "V"},
                new obisCodesStruct {A=1, B=0, C=72, D=7, E=  0, F=255, obis= "1.0.72.7.0.255", UoM= "V"},
                new obisCodesStruct {A=0, B=0, C= 1, D=0, E=  0, F=255, obis= "0.0.1.0.0.255",  UoM= ""},
                new obisCodesStruct {A=1, B=0, C= 1, D=8, E=  0, F=255, obis= "1.0.1.8.0.255",  UoM= "kWh"},
                new obisCodesStruct {A=1, B=0, C= 2, D=8, E=  0, F=255, obis= "1.0.2.8.0.255",  UoM= "kWh"},
                new obisCodesStruct {A=1, B=0, C= 3, D=8, E=  0, F=255, obis= "1.0.3.8.0.255",  UoM= "kVArh"},
                new obisCodesStruct {A=1, B=0, C= 4, D=8, E=  0, F=255, obis= "1.0.4.8.0.255",  UoM= "kVArh"}
            };

        public void showObisValues()
        {
            for (int i = 0; i < legalObisCodes_Aidon.Count; i++) Console.WriteLine("Struct obis={0:x2}.{1:x2}.{2:x2}.{3:x2}.{4:x2}.{5:x2} ({6}) with UoM={7}", legalObisCodes_Aidon[i].A, legalObisCodes_Aidon[i].B, legalObisCodes_Aidon[i].C, legalObisCodes_Aidon[i].D, legalObisCodes_Aidon[i].E, legalObisCodes_Aidon[i].F, legalObisCodes_Aidon[i].obis, legalObisCodes_Aidon[i].UoM);
        }

        public int isObisFound(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            for (int i = 0; i < legalObisCodes_Aidon.Count; i++)
            {
                if (legalObisCodes_Aidon[i].A == a && legalObisCodes_Aidon[i].B == b && legalObisCodes_Aidon[i].C == c && legalObisCodes_Aidon[i].D == d && legalObisCodes_Aidon[i].E == e && legalObisCodes_Aidon[i].F == f)
                    return i;
            }
            return -1;
        }

        public string UoMObisCode(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            int location = isObisFound(a, b, c, d, e, f);
            if (location > -1)
                return legalObisCodes_Aidon[location].UoM;
            return "Error in UoM";
        }

        public string oBISCode(List<byte> data, int start)
        {
            string cosem = "";
            int obisLength = 6;
            for (int k = start; k < (start + obisLength - 1); k++)
            {
                cosem += (int)(data[k]) + ".";
#if OBISDEBUG
                        Console.Write("{0:X2}.",data[k]);
#endif
            }
            cosem += (int)data[start + obisLength - 1];
#if OBISDEBUG
                Console.WriteLine("{0:X2}.",data[(start + obisLength - 1)]);
#endif
            return cosem;

        }


    }
}
