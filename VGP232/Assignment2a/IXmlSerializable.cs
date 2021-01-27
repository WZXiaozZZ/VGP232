using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2a
{
    interface IXmlSerializable
    {
        bool LoadXML(string filename);
        bool SaveAsXML(string filename);
    }
}
