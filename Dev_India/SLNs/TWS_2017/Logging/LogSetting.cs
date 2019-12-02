using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Logging
{
    public class LogSetting
    {
        [XmlElement("MAXSizeMb")]
        public int MAXSizeMb
        { get; set; }
        [XmlElement("FlagIn")]
        public bool FlagIn
        { get; set; }
        [XmlElement("FlagOut")]
        public bool FlagOut
        { get; set; }
        [XmlElement("FlagInfo")]
        public bool FlagInfo
        { get; set; }
        [XmlElement("FlagError")]
        public bool FlagError
        { get; set; }
        [XmlElement("FlagInOut")]
        public bool FlagInOut
        { get; set; }
        [XmlElement("FlagLog")]
        public bool FlagLog
        { get; set; }
    }
}
