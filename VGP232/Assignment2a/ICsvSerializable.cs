using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2a
{
    interface ICsvSerializable
    {
        bool LoadCSV(string path);
        bool SaveAsCSV(string path);
    }
}
