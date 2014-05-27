using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace TP.WPF.Properties
{
    [Serializable]
    [DebuggerDisplay("{LogicalChannelId} -> {LocalChannelId}")]
    public class LogicalChannelMapping
    {
        [XmlAttribute]
        public int LogicalChannelId { get; set; }
        [XmlAttribute]
        public int LocalChannelId { get; set; }
    }
}