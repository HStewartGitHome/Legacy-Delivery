using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using log4net.Config;
using log4net;
using System.IO;

namespace Bridge
{
    public class Support
    {
        private static readonly ILog LOG = LogManager.GetLogger(typeof(Support));

        static bool NotInitialized = true;

        public static void Initialize()
        {
            if (NotInitialized == true)
            {
                XmlConfigurator.Configure();

                log4net.GlobalContext.Properties["processid"] = Process.GetCurrentProcess().Id;
                NotInitialized = false;

                LOG.Info("Log4Net initialized");
                
            }
        }

        public static void ExecuteServiceXML(string serviceExe,
                                             string method,
                                             string fileName)
        {
            LOG.Info("Executing " + serviceExe + " for method=" + method + " for fileName=" + fileName);


            Start(serviceExe, "/" + method + " /" + fileName);
        }

        public static bool Start(string fileName, string arguments)
        {
            bool result = false;
            string currentDir, dir;


            currentDir = Directory.GetCurrentDirectory();

            try
            {
                LOG.Info("Executing Start Process=" + fileName);

                dir = Path.GetDirectoryName(fileName);
                LOG.Info("CurrentDirectory=" + currentDir + " dir=" + dir);
                if ((dir != null) && (dir.Length > 0))
                {
                    LOG.Info("Setting current directory=" + dir + " from " + currentDir);
                    Directory.SetCurrentDirectory(dir);
                }

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = fileName;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = arguments;

                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }

                result = true;
            }
            catch (Exception e)
            {
                LOG.Error("Exception executing start process=" + fileName, e);
            }

            Directory.SetCurrentDirectory(currentDir);
            return result;
        }
                                              
    }
}
