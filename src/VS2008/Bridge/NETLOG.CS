using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using log4net;
using log4net.Config;


namespace Bridge
{
    public interface INetLog
    {
        int LogDebug(string Msg, string Function);
        int LogInfo(string Msg, string Function);
        int LogWarn(string Msg, string Function);
        int LogError(string Msg, string Function);
        int LogFatal(string Msg, string Function);
    }


    public class NetLog : INetLog
    {
        private static readonly ILog LOG = LogManager.GetLogger(typeof(NetLog));

        public NetLog()
        {
            Support.Initialize();

        }

        public int LogDebug(string Msg, string Function)
        {
            string Log = Function + ": " + Msg;
            LOG.Debug(Log);

            return 1;
        }

        public int LogInfo(string Msg, string Function)
        {
            string Log = Function + ": " + Msg;
            LOG.Info(Log);

            return 1;
        }

        public int LogWarn(string Msg, string Function)
        {
            string Log = Function + ": " + Msg;
            LOG.Warn(Log);

            return 1;
        }


        public int LogError(string Msg, string Function)
        {
            string Log = Function + ": " + Msg;
            LOG.Error(Log);

            return 1;
        }


        public int LogFatal(string Msg, string Function)
        {
            string Log = Function + ": " + Msg;
            LOG.Fatal(Log);

            return 1;
        }
    }
}
