using System;
using System.Xml;

namespace TopoCaptureScreen.Utilities
{
    public static class XMLHelper
    {
        public static string GetAttributeValue(XmlNode xml,string attributeName)
        {
            string returnValue = String.Empty;
            if (xml != null)
            {
                XmlAttribute _att = xml.Attributes[attributeName];
                if (_att != null)
                {
                    returnValue = _att.Value;
                }
            }
            return returnValue;
        }
        public static string GetAttributeValue(XmlNode xml,string xQuery,string attributeName)
        {
            string returnValue = String.Empty;
            XmlNode _node = xml.SelectSingleNode(xQuery);
            if (_node != null)
            {
                XmlAttribute _att = _node.Attributes[attributeName];
                if (_att != null)
                {
                    returnValue = _att.Value;
                }
            }
            return returnValue;
        }
    }
}
