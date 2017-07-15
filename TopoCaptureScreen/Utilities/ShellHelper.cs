using System;
using System.Diagnostics;

namespace TopoCaptureScreen.Utilities
{
    public static class ShellHelper
    {
        public static bool Execute(string cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = String.Concat("/C " + cmd);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            return  p.Start();
        }
    }
}
