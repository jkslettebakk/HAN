#undef OBISDEBUG


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HAN_OBIS
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
            public string objectName { set; get; }
            public string HAN_Vendor { get; set; }
        }

        //a obis struct list
        List<obisCodesStruct> legalObisCodes = new List<obisCodesStruct>{
                new obisCodesStruct {A=1, B=0, C=1,  D=7, E=0,   F=255, obis= "1.0.1.7.0.255",  UoM= "kW",      objectName="Q1+Q4",              HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=1, C= 0, D=2, E=129, F=255, obis= "1.1.0.2.129.255",UoM= "' '",     objectName="Version identifier", HAN_Vendor ="Aidon & Kamstrup"},
                new obisCodesStruct {A=0, B=0, C=96, D=1, E=  0, F=255, obis= "0.0.96.1.0.255", UoM= "' '",     objectName="Model ID",           HAN_Vendor ="Aidon"}, 
                new obisCodesStruct {A=0, B=0, C=96, D=1, E=  7, F=255, obis= "0.0.96.1.7.255", UoM= "' '",     objectName="Model type",         HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C= 2, D=7, E=  0, F=255, obis= "1.0.2.7.0.255",  UoM= "kW",      objectName="Q2+Q3",              HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C= 3, D=7, E=  0, F=255, obis= "1.0.3.7.0.255",  UoM= "kVAr",    objectName="Q1+Q2",              HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C= 4, D=7, E=  0, F=255, obis= "1.0.4.7.0.255",  UoM= "kVAr",    objectName="Q3+Q4",              HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C=31, D=7, E=  0, F=255, obis= "1.0.31.7.0.255", UoM= "A",       objectName="IL1",                HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C=51, D=7, E=  0, F=255, obis= "1.0.51.7.0.255", UoM= "A",       objectName="IL2",                HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C=71, D=7, E=  0, F=255, obis= "1.0.71.7.0.255", UoM= "A",       objectName="IL3",                HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C=32, D=7, E=  0, F=255, obis= "1.0.32.7.0.255", UoM= "V",       objectName="UL1",                HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C=52, D=7, E=  0, F=255, obis= "1.0.52.7.0.255", UoM= "V",       objectName="UL2",                HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C=72, D=7, E=  0, F=255, obis= "1.0.72.7.0.255", UoM= "V",       objectName="UL3",                HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=0, B=0, C= 1, D=0, E=  0, F=255, obis= "0.0.1.0.0.255",  UoM= "' '",     objectName="Clock and Date",     HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C= 1, D=8, E=  0, F=255, obis= "1.0.1.8.0.255",  UoM= "kWh",     objectName="Q1+Q4",              HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C= 2, D=8, E=  0, F=255, obis= "1.0.2.8.0.255",  UoM= "kWh",     objectName="Q2+Q3",              HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C= 3, D=8, E=  0, F=255, obis= "1.0.3.8.0.255",  UoM= "kVArh",   objectName="Q1+Q2",              HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=0, C= 4, D=8, E=  0, F=255, obis= "1.0.4.8.0.255",  UoM= "kVArh",   objectName="Q3+Q4",              HAN_Vendor ="Aidon"},
                new obisCodesStruct {A=1, B=1, C= 0, D=0, E=  5, F=255, obis= "1.1.0.0.5.255",  UoM= "' '",     objectName="GS1 number",         HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C= 1, D=7, E=  0, F=255, obis= "1.1.1.7.0.255",  UoM= "W",       objectName="P14",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C= 2, D=7, E=  0, F=255, obis= "1.1.2.7.0.255",  UoM= "W",       objectName="P23",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C=31, D=7, E=  0, F=255, obis= "1.1.31.7.0.255", UoM= "A",       objectName="IL1",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C=51, D=7, E=  0, F=255, obis= "1.1.51.7.0.255", UoM= "A",       objectName="IL2",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C=71, D=7, E=  0, F=255, obis= "1.1.71.7.0.255", UoM= "A",       objectName="IL3",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C=32, D=7, E=  0, F=255, obis= "1.1.32.7.0.255", UoM= "V",       objectName="UL1",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C=52, D=7, E=  0, F=255, obis= "1.1.52.7.0.255", UoM= "V",       objectName="UL2",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C=72, D=7, E=  0, F=255, obis= "1.1.72.7.0.255", UoM= "V",       objectName="UL3",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=0, B=1, C= 1, D=0, E=  0, F=255, obis= "0.1.1.0.0.255",  UoM= "' '",     objectName="RTC",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C= 1, D=8, E=  0, F=255, obis= "1.1.1.8.0.255",  UoM= "Wh",      objectName="A14",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C= 2, D=8, E=  0, F=255, obis= "1.1.2.8.0.255",  UoM= "Wh",      objectName="A23",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C=96, D=1, E=  1, F=255, obis= "1.1.96.1.1.255", UoM= "' '",     objectName="Meter type",         HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C= 3, D=7, E=  0, F=255, obis= "1.1.3.7.0.255",  UoM= "var",     objectName="Q12",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C= 4, D=7, E=  0, F=255, obis= "1.1.4.7.0.255",  UoM= "var",     objectName="Q34",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C= 3, D=8, E=  0, F=255, obis= "1.1.3.8.0.255",  UoM= "varh",    objectName="R12",                HAN_Vendor ="Kamstrup"},
                new obisCodesStruct {A=1, B=1, C= 4, D=8, E=  0, F=255, obis= "1.1.4.8.0.255",  UoM= "varh",    objectName="R34",                HAN_Vendor ="Kamstrup"}

            };

        public void showObisValues()
        {
            for (int i = 0; i < legalObisCodes.Count; i++)
                    Console.WriteLine("Struct obis={0:x2}.{1:x2}.{2:x2}.{3:x2}.{4:x2}.{5:x2} ({6,-15}), UoM={7,-5}, objectName={8,-18}, vendor={9}", 
                    legalObisCodes[i].A, 
                    legalObisCodes[i].B, 
                    legalObisCodes[i].C, 
                    legalObisCodes[i].D, 
                    legalObisCodes[i].E, 
                    legalObisCodes[i].F, 
                    legalObisCodes[i].obis, 
                    legalObisCodes[i].UoM,
                    legalObisCodes[i].objectName,
                    legalObisCodes[i].HAN_Vendor);
        }

        public int isObisFound(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            for (int i = 0; i < legalObisCodes.Count; i++)
            {
                if (legalObisCodes[i].A == a && legalObisCodes[i].B == b && legalObisCodes[i].C == c && legalObisCodes[i].D == d && legalObisCodes[i].E == e && legalObisCodes[i].F == f)
                    return i;
            }
            return -1;
        }

        public string UoMObisCode(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            int location = isObisFound(a, b, c, d, e, f);
            if (location > -1)
                return legalObisCodes[location].UoM;
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
