using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Bridge.DataModel;
using Bridge.Utilities;

namespace Bridge
{
         public interface IClientBridge
        {
             // Conifiguration support
             void SetDataPath(string dataPath);
             void SetServiceXMLExe(string exePath);
             void SetMethod(string method);
             string GetMethod();

             // item list support
             int GetItemListCount();
             int GetItemListItemNum(int index);
             string GetItemListDescription(int index);
             double GetItemListAmount(int index);
             string GetItemListImageFileName(int index);
             int GetItemListCategory(int index);

             // new item list
             void ClearNewItemList();
             void ResetItemList();
             void AddItemListItem(int itemNum,
                                 string description,
                                 double amount,
                                 string imageFileName,
                                 int category);

             // get message
             int HasMessage(string lastDateTime,
                           string location);
             string GetMessageToName();
             string GetMessageFromName();
             string GetMessageText();
             string GetMessageLocation();
             string GetMessageTime();


             // testing
             void TestException();
        }

        public class Client : IClientBridge
        {
            private static readonly ILog LOG = LogManager.GetLogger(typeof(Client));
            private List<Item> ItemList = null;
            private bool UseServiceXML = false;
            private string DataPath = "";
            private string ServiceXMLExe = "";
            private string Method = "GetItemList";
            private Message CurrentMessage = null;

            public Client()
            {
                Support.Initialize();

            }

            // Conifiguration support
            public void SetDataPath(string dataPath)
            {
                DataPath = dataPath;
            }

            public void SetServiceXMLExe(string exePath)
            {
                if (exePath.Length > 0)
                {
                    UseServiceXML = true;
                    ServiceXMLExe = exePath;
                }
                else
                    UseServiceXML = false;
            }

            public void SetMethod(string method)
            {
                Method = method;
                LOG.Info( "Setting Method=" + method );
            }

            public string GetMethod()
            {
                return Method;
            }


            // item list support
            public int GetItemListCount()
            {
                if (ItemList == null)
                    ItemList = GetItemListItems();

                if (ItemList == null)
                {
                    LOG.Warn("No items found in list");
                    return 0;
                }
                else
                    return ItemList.Count();
            }

            public int GetItemListItemNum(int index)
            {
                 if (ItemList == null)
                     ItemList = GetItemListItems();

                 if (ItemList == null)
                 {
                     LOG.Warn("No items found in list");
                     return 0;
                 }
                 else
                 {
                     if (ItemList.Count > 0)
                         return ItemList[index].ItemNum;
                     else
                     {
                         LOG.Warn("ItemList return Empty");
                         return 0;
                     }
                 }
            }

            public string GetItemListDescription(int index)
            {
                if (ItemList == null)
                    ItemList = GetItemListItems();

                if (ItemList == null)
                {
                    LOG.Warn("No items found in list");
                    return "";
                }
                else
                {
                    if (ItemList.Count > 0)
                        return ItemList[index].Description;
                    else
                    {
                        LOG.Warn("ItemList return Empty");
                        return "";
                    }
                }
            }

            public double GetItemListAmount(int index)
            {
                if (ItemList == null)
                    ItemList = GetItemListItems();

                if (ItemList == null)
                {
                    LOG.Warn("No items found in list");
                    return 0.0;
                }
                else
                {
                    if (ItemList.Count > 0)
                        return ItemList[index].Amount;
                    else
                    {
                        LOG.Warn("ItemList ret urn Empty");
                        return 0.0;
                    }
                }
            }

            public string GetItemListImageFileName(int index)
            {
                if (ItemList == null)
                    ItemList = GetItemListItems();

                if (ItemList == null)
                {
                    LOG.Warn("No items found in list");
                    return "";
                }
                else
                {
                    if (ItemList.Count > 0)
                        return ItemList[index].ImageFileName;
                    else
                    {
                        LOG.Warn("ItemList return Empty");
                        return "";
                    }
                }
            }

            public int GetItemListCategory(int index)
            {
                if (ItemList == null)
                    ItemList = GetItemListItems();

                if (ItemList == null)
                {
                    LOG.Warn("No items found in list");
                    return 0;
                }
                else
                {
                    if (ItemList.Count > 0)
                        return ItemList[index].Category;
                    else
                    {
                        LOG.Warn("ItemList return Empty");
                        return 0;
                    }
                }
            }

             // new item list
             public void ClearNewItemList()
             {
                if (ItemList != null)
                  ItemList = null;
                ItemList = new List<Item>();
                   
             }

             // Reset item list
             public void ResetItemList()
             {
                 if (ItemList != null)
                     ItemList = null;


             }

             public void AddItemListItem(int itemNum,
                                         string description,
                                         double amount,
                                         string imageFileName,
                                         int category )
             {
                if (ItemList == null)
                  ClearNewItemList();
      
                Item item = new Item();
                item.ItemNum = itemNum;
                item.Description = description;
                item.Amount = amount;
                item.Quantity = 1;
                item.ImageFileName = imageFileName;
                item.Category = category;
             }




             public List<Item> GetItemListItems()
            {
                string fileName = "itemlist.xml";
                List<Item> itemData = new List<Item>();

                // get data from server if configured
                LOG.Info("Handling Method= " + Method + " for " + fileName);
                ExecuteServiceXML(fileName, Method);

                // read from xml
                string strPath = ".\\..\\data\\" + fileName;
                LOG.Info("Getting items from [" + strPath + "]");

               itemData =  ( List<Item>)Serializer.DeserializeFile( strPath, typeof(List<Item>) );
               LOG.Info("Handle " + itemData.ToString() + " Items");

                return itemData;
            }

             // get message
             public int HasMessage(string lastDateTime,
                                   string location)
             {
                 LOG.Info("HasMessage with LastDateTime=" + lastDateTime + " Location=" + location);
                 if (GetMessage(lastDateTime, location))
                     return 1;
                 else
                     return 0;

             }

             public string GetMessageToName()
             {
                 if (CurrentMessage == null)
                     return "";
                 else
                     return CurrentMessage.ToName;

             }


             public string GetMessageFromName()
             {
                 if (CurrentMessage == null)
                     return "";
                 else
                     return CurrentMessage.FromName;

             }




             public string GetMessageText()
             {
                 if (CurrentMessage == null)
                     return "";
                 else
                     return CurrentMessage.MessageText;

             }

             public string GetMessageLocation()
             {  
                if (CurrentMessage == null)
                     return "";
                 else
                     return CurrentMessage.Location;

             }

             public string GetMessageTime()
             {
                 if (CurrentMessage == null)
                     return "";
                 else
                     return CurrentMessage.Time;

             }


             private bool GetMessage(string lastDateTime,
                                      string location)
             {
                 string fileName = "GetMessage.xml";
                 Method = "GetMessage";
                 Message message = new Message();
                 Message returnMessage = null;
                 message.Location = location;
                 message.Time = lastDateTime;

                 try
                 {
                     // first serialize the message
                     string strPath = ".\\..\\data\\" + fileName;
                     LOG.Info("Serializing Message from [" + strPath + "] with Location [" + location + "] and LastDateTime= [" + lastDateTime.ToString());
                     Serializer.SerializeToFile(message, fileName);


                     // get data from server if configured
                     LOG.Info("Handling Method= " + Method + " for " + fileName + " after serialization");
                     ExecuteServiceXML(fileName, Method);

                     // read from xml
                     strPath = ".\\..\\data\\" + fileName;
                     LOG.Info("Getting Message from [" + strPath + "]");

                     returnMessage = (Message)Serializer.DeserializeFile(strPath, typeof(Message));
                     if (returnMessage == null)
                     {
                         LOG.Warn("Return message not return");
                         CurrentMessage = null;
                     }
                     else
                     {
                         LOG.Info("Return message found ");
                         CurrentMessage = returnMessage;
                     }


                     if (CurrentMessage == null)
                         return false;
                     else
                         return true;
                 }
                 catch (Exception ex)
                 {
                     LOG.Error("Exception executing method = " + Method, ex);
                 }
                 return false;
             }

            // testing
             public void TestException()
             {
                 try
                 {
                           // Doing Divide by Zero test
	                LOG.Info("Test Divide by Zero exception");
                    long lValue = 928;
                    long lZero = 0;
                    lValue = lValue / lZero;
                 }
                 catch (Exception ex)
                 {
                     LOG.Error("Exception Occured in with Test Exception", ex);
                 }
             }

            // support

            private void ExecuteServiceXML(string fileName,
                                           string method )
            {
                if (UseServiceXML == true)
                {
                    LOG.Info("Execute ServiceXML Method= " + method + " for fileName = " + fileName);
                    string strPath = DataPath + "\\" + fileName;
                    Support.ExecuteServiceXML(ServiceXMLExe, method, strPath);
                }
                else
                    LOG.Warn("Not using ServiceXML");
            }
        }

}
