
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using XMLSupport.Models;
using XMLSupport.Services;

// Note testing logic that actually deals with database have been move to seperate command line 
// test application XMLDataTest so that unit testing does not alter database

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {

            string fileName = "c:\\src\\data\\testitem.xml";
            var dataFactory = new XMLDataFactory();
            XMLSupport.Models.Item item = dataFactory.MakeXMLItem(9001, "test item", 2.99, 1, "test.png", 1000);

            // var xmlServiceFactory = new xmlServiceFactory(_configuration, _logger);
            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XMLSupport.Models.Item));
            bool result = serializer.SerializeToFile(xmlSerializer, item, fileName);
            Assert.IsTrue(result, "Serialize to file successfully return");

            XMLSupport.Models.Item itemReturn = (XMLSupport.Models.Item)serializer.DeserializeFile(xmlSerializer, fileName);
            Assert.IsNotNull(itemReturn, "Item should not be null");
            Assert.AreEqual(itemReturn.ItemNum, 9001, "ItemNum should be 9001");
            Assert.AreEqual(itemReturn.Description, "test item", "Description should be test item");
            Assert.AreEqual(itemReturn.Amount, 2.99, "Amount should be 2.99");
            Assert.AreEqual(itemReturn.Quantity, 1, "Quantity should be 1");
            Assert.AreEqual(itemReturn.ImageFileName, "test.png", "ImageFileName should be test.png");
        }




        [TestMethod]
        public void TestMethod2()
        {
            bool result = false;
            string connectionString = @"Data Source = DESKTOP-BJK75NS\SQLEXPRESS;Database = Modern; Integrated Security = True";
            //string connectionString = @".\SQLEXPRESS;Initial Catalog=ModenDB;Integrated Security=SSPI;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    result = true;
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, "Exception is ", ex.ToString());
                }
            }
            Assert.IsTrue(result, "Failed to open connection");
        }






        [TestMethod]
        public void TestMethod3()
        {
            var dataFactory = new XMLDataFactory();
            Context context = dataFactory.MakeXMLContext();
            XMLSupport.Models.Message message = dataFactory.MakeXMLMessage(2, "Testing", "Unit Test", "This is from Unit Testing");
            context.MessageObj = message;

            string fileName = "c:\\src\\data\\context.xml";
            //var xmlServiceFactory = new xmlServiceFactory(_configuration, _logger);
            var serializer = new Serializer();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Context));
            bool result = serializer.SerializeToFile(xmlSerializer, context, fileName);
            Assert.IsTrue(result, "Serialize to file successfully return");
        }



        [TestMethod]
        public void TestMethod4()
        {
            bool result = false;
            string currentDir, dir;

            string fileName = @"cmd.exe";
            string arguments = @"/c dir";

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
            catch (Exception ex)
            {
                Assert.IsTrue(false, "Exception is ", ex.ToString());
            }

            Directory.SetCurrentDirectory(currentDir);
            Assert.IsTrue(result, "successfully run shell cmd.exe");
        }




    }
}
