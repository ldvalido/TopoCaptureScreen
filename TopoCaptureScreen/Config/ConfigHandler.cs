using System;
using System.IO;
using System.Reflection;
using System.Xml;
using TopoCaptureScreen.Utilities;

namespace TopoCaptureScreen.Config
{
    public class ConfigHandler
    {
        private const string resourceManifiest = "TopoCaptureScreen.paramCapture.xml";
        private XmlDocument configXml=new XmlDocument();

        #region Singleton
        private static ConfigHandler _instance;
        public static ConfigHandler instance
        {
            get { return _instance; }
            set { _instance = value; }
        }

        private ConfigHandler() 
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream str = asm.GetManifestResourceStream(resourceManifiest);
            configXml.Load(str);
        }
        static ConfigHandler()
        {
            _instance = new ConfigHandler();
        }
        #endregion

        #region Public Events
        public void Initialize()
        {
            string tmpValue;
            XmlNode _lst = configXml.SelectSingleNode("/params");
            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='tempFolder']", "value");
            if (!String.IsNullOrEmpty(tmpValue))
            {
                _tempfolder = tmpValue;
            }

            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='imageExtension']", "value");
            if (!String.IsNullOrEmpty(tmpValue))
            {
                _imageExtension = tmpValue;
            }

            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='packageExtension']", "value");
            if (!String.IsNullOrEmpty(tmpValue))
            {
                _packageExtension= tmpValue;
            }

            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='ftpHost']", "value");
            if (!String.IsNullOrEmpty(tmpValue))
            {
                _ftpHost = tmpValue;
            }

            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='ftpUserName']", "value");
            if (!String.IsNullOrEmpty(tmpValue))
            {
                _ftpUserName = tmpValue;
            }

            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='ftpUserPassword']", "value");
            if (!String.IsNullOrEmpty(tmpValue))
            {
                _ftpUserPassword = tmpValue;
            }

            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='stmtFile']", "value");
            if (!String.IsNullOrEmpty(tmpValue))
            {
                _stmtFile = tmpValue;
            }
            
            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='screenTimer']", "value");
            int? tmpInt = NumberHelper.secureIntParse(tmpValue);
            if (tmpInt != null)
            {
                _screenTimer = (int)tmpInt;
            }

            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='maximmiumValue']", "value");
            tmpInt = NumberHelper.secureIntParse(tmpValue);
            if (tmpInt != null)
            {
                _maximmiumValue = (int)tmpInt;
            }

            tmpValue = XMLHelper.GetAttributeValue(_lst, "/params/param[@name='ftpSenderTimer']", "value");
            tmpInt = NumberHelper.secureIntParse(tmpValue);
            if (tmpInt != null)
            {
                _ftpSenderTimer = (int)tmpInt;
            }
        }
        #endregion

        #region Private Fields

        private string _systemTempFolder = Path.GetTempPath();
        private string _tempfolder = @"C:\Temp\";
        private string _imageExtension = "*.jpg";
        private string _packageExtension = "*.zip";
        private int _screenTimer = 15000;
        private int _maximmiumValue = 1000000;
        private int _ftpSenderTimer = 600000;
        private string _ftpHost = String.Empty;
        private string _ftpUserName = String.Empty;
        private string _ftpUserPassword = String.Empty;
        private string _stmtFile = String.Empty;
        #endregion

        #region Public Properties
        public int screenTimer
        {
            get { return _screenTimer; }
            set { _screenTimer = value; }
        }

        public string tempfolder
        {
            get { return _tempfolder; }
            set { _tempfolder = value; }
        }

        public string imageExtension
        {
            get { return _imageExtension; }
            set { _imageExtension = value; }
        }

        public string packageExtension
        {
            get { return _packageExtension; }
            set { _packageExtension = value; }
        }

        public string systemTempFolder
        {
            get { return _systemTempFolder; }
            set { _systemTempFolder = value; }
        }

        public int maximmiumValue
        {
            get { return _maximmiumValue; }
            set { _maximmiumValue = value; }
        }

        public int ftpSenderTimer
        {
            get { return _ftpSenderTimer; }
            set { _ftpSenderTimer = value; }
        }

        public string ftpHost
        {
            get { return _ftpHost; }
            set { _ftpHost = value; }
        }

        public string ftpUserName
        {
            get { return _ftpUserName; }
            set { _ftpUserName = value; }
        }

        public string ftpUserPassword
        {
            get { return _ftpUserPassword; }
            set { _ftpUserPassword = value; }
        }

        public string stmtFile
        {
            get { return _stmtFile; }
            set { _stmtFile = value; }
        }

        #endregion
        
    }
}
