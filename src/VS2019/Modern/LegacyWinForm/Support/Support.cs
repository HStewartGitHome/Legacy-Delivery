
using LegacyWinForm.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LegacyWinForm.Support
{
    public class Support
    { 
       public string RouterDir { get; set; }
        public bool FirstTimeRouterDir { get; set; }
        public string ServiceEXE { get; set; }

        public Support()
        {
            FirstTimeRouterDir = true;

            try
            {
                ServiceEXE = GetSettingValue("ServiceEXE");
            }
            catch( Exception e )
            {
                Trace.TraceError("Exception getting setting for ServiceEXE", e);
            }

        }

        

        public Order NewOrder()
        {
            Order order = new Order();
            order.OrderNum = GetNewOrderNum();
           
            return order;
        }
      
        public void SaveOrder(Order order)
        {
            int ContextNum = GetContextNum();
            RouterDir = GetRouterDir();

            // save order to router
            Context context = new Context();
            context.Location = "LEGACY_WINFORM";
            context.Time = DateTime.Now.ToString();
            context.InOrder = order;
            context.Method = "SAVEROUTER";
            context.RequestType = 1;
            context.ID = GetContextID(order.OrderNum, ContextNum, "SAVEROUTER");


            string fileName = RouterDir + "\\" + context.ID + ".XML";

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Context));
            serializer.SerializeToFile(xmlSerializer, context, fileName);


        }

        public void SaveMessage( Message message )
        {
            int ContextNum = GetContextNum();
            int OrderNum = GetNewOrderNum();
            RouterDir = GetRouterDir();

            Context context = new Context();
            context.Location = "LEGACY_WINFORM";
            context.Time = DateTime.Now.ToString();
            context.Method = "SENDMESSAGE";
            context.MessageObj = message;
            context.RequestType = 2;
            context.ID = GetContextID(OrderNum, ContextNum, context.Method);

            string fileName = RouterDir + "\\" + context.ID + ".XML";

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Context));
            serializer.SerializeToFile(xmlSerializer, context, fileName);
        }
        

        public int GetNewOrderNum()
        {
            int OrderNum = 0;
            int NextOrderNum;
            string value;

            // get last order number for configuration
            value = GetSettingValue("NextOrderNum");
            OrderNum = Convert.ToInt32(value);
            NextOrderNum = OrderNum + 1;
            value = NextOrderNum.ToString();
            SetSettingValue("NextOrderNum", value);

            return OrderNum;
        }

        public int GetContextNum()
        {
            int ContextNum = 0;
            int NextContextNum;
            string value;

            // get last order number for configuration
            value = GetSettingValue("NextContextNum");
            ContextNum = Convert.ToInt32(value);
            NextContextNum = ContextNum + 1;
            value = NextContextNum.ToString();
            SetSettingValue("NextContextNum", value);

            return ContextNum;
        }

        public int GetustomerNum()
        {
            int CustomerNum = 0;
            int NextCustomerNum;
            string value;

            // get last Customer number for configuration
            value = GetSettingValue("NextCustomerNum");
            CustomerNum = Convert.ToInt32(value);
            NextCustomerNum = CustomerNum + 1;
            value = NextCustomerNum.ToString();
            SetSettingValue("NextCustomerNum", value);

            return CustomerNum;
        }

        public string GetRouterDir()
        {
            if ( FirstTimeRouterDir == true )
            {
                RouterDir = GetSettingValue("RouterDir");

                var dirOutgoing = RouterDir + @"\Outgoing";
                var dirErrors = RouterDir + @"\Errors";
                if (!Directory.Exists(dirOutgoing))
                    Directory.CreateDirectory(dirOutgoing);
                if (!Directory.Exists(dirErrors))
                    Directory.CreateDirectory(dirErrors);

                FirstTimeRouterDir = false;
            }

            return RouterDir;
        }

        public string GetContextID( int orderNum, int contextID, string msg )
        {
            string str = orderNum.ToString() + "_" + contextID.ToString() + "_" + msg;
            return str;
        }


        public int GetTaxPercent()
        {
            int value = 0;
            string str;

            try
            {
                str = GetSettingValue("TaxPercent");
                value = Convert.ToInt32(str);
            }
            catch(Exception e)
            {
                Trace.TraceError("Exception getting TaxPercent", e);
            }

            return value;
        }


        
        public List<Item> GetItems()
        {
            List<Item> ItemList = new List<Item>();
            string FileName = "itemlist.xml";
            string Method = "GETITEMLIST";

            ExecuteServiceXML(ServiceEXE, Method, FileName);

            string StrPath = ".\\..\\data\\" + FileName;

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Item>));
            ItemList = (List<Item>)serializer.DeserializeFile(xmlSerializer, StrPath);

            return ItemList;
   
        }

        public void SeedItemList()
        {
            string FileName = "itemlist.xml";
            string Method = "SEETITEMLIST";
            string StrPath = ".\\..\\data\\" + FileName;
            List<Item> ItemList = new List<Item>();

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Item>));
            ItemList = (List<Item>)serializer.DeserializeFile(xmlSerializer, StrPath);

            ExecuteServiceXML(ServiceEXE, Method, FileName);
        }
       


        public  void ExecuteServiceXML(string serviceExe,
                                       string method,
                                       string fileName)
        {
            Start(serviceExe, "/" + method + " /" + fileName);
        }

        public  bool Start(string fileName, string arguments)
        {
            bool result = false;
            string currentDir, dir;


            currentDir = Directory.GetCurrentDirectory();

            try
            {
                dir = Path.GetDirectoryName(fileName);
                if ((dir != null) && (dir.Length > 0))
                {
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
                Trace.TraceError("Exception executing start process=" + fileName, e);
            }

            Directory.SetCurrentDirectory(currentDir);
            return result;
        }


        private string GetSettingValue(string paramName)
        {
            return String.Format(ConfigurationManager.AppSettings[paramName]);
        }

        private void SetSettingValue(string paramName, string value)
        {
            ConfigurationManager.AppSettings[paramName] = value;
        }

    }
}
