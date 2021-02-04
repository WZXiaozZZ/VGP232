using System;
using System.Collections.Generic;
using System.Text;

namespace WeaponLib
{
    interface ICsvSerializable
    {
        bool LoadCSV(string path);
        bool SaveAsCSV(string path);
    }
}
