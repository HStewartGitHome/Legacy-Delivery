// Process.cpp : implementation file
//

#include "stdafx.h"
#include "Order.h"
#include "process.h"
#include "lognet.h"
#include "clientnet.h"

CItemList *CProcess::m_pItemList = NULL;
BOOL	   CProcess::m_bServiceConfigurationNotInitialized = TRUE;
BOOL       CProcess::m_bUseServiceExe = FALSE;
CString    CProcess::m_DataPath = _T("");
CString    CProcess::m_GetServiceXMLExe = _T("");
BOOL	   CProcess::m_bFirstBridgeCall = TRUE;
CString    CProcess::m_Location = _T("");
CString    CProcess::m_TimeMessage = _T("");

// CProcess object


CProcess::CProcess()
{
}

CProcess::~CProcess()
{
	if ( m_pItemList )
		delete ( m_pItemList );
}

CItemList *CProcess::LoadItemList( BOOL bAllowRemote )
{                  
  CItem *pItem;
  long   lIndex, lCount, lID, lCategory;
  double dblAmount;
  CString strDescription, str;

 

  if ( m_pItemList == NULL )
  {
      // just hard code for now
      m_pItemList = new CItemList();

	  if ( m_bFirstBridgeCall )
	  {
		  m_bFirstBridgeCall = FALSE;

		  InitializeServiceConfigurations();
		
          str.Format( _T("Setting DataPath=[%s] and GetServiceXMLExe=[%s]"),
					 m_DataPath, m_GetServiceXMLExe );
		  CLogNet::LogInfo( str, _T("CProcess::LoadItemList") );

		  CClientNet::SetDataPath( m_DataPath );
		  CClientNet::SetServiceXMLExe( m_GetServiceXMLExe );
	  }

	  if (!bAllowRemote)
		    CClientNet::SetServiceXMLExe( _T("") );

      lCount = CClientNet::GetItemListCount();
      str.Format( _T("ItemList Count=%ld"), lCount );
      CLogNet::LogInfo( str, _T("CProcess::LoadItemList") );
      for (lIndex=0;lIndex<lCount; lIndex++ )
      {
          lID = CClientNet::GetItemListItemNum( lIndex );          
          dblAmount = CClientNet::GetItemListAmount( lIndex );          
          CClientNet::GetItemListDescription( lIndex, strDescription );
		  lCategory = CClientNet::GetItemListCategory( lIndex );
          
          if ( lID > 0 )
          {
			  str.Format( _T("Adding Index: %3ld ItemNum: %5ld Description: [%s] Amount: $%5.2f Category: %ld"),
                          lIndex+1, lID, strDescription, dblAmount, lCategory );
              CLogNet::LogInfo( str, _T("CProcess::LoadItemList") );

              pItem = new CItem( lID, strDescription );
              if ( pItem )
              {          
                  pItem->m_dblAmount = dblAmount;
				  pItem->m_lCategory = lCategory;
                  m_pItemList->m_Items.Add( pItem );
              }  
          }
          else
          {
			  str.Format( _T("Skipping Index: %3ld ItemNum: %5ld Description: [%s] Amount: $%5.2f Category: %ld"),
                          lIndex+1, lID, strDescription, dblAmount, lCategory );
              CLogNet::LogWarn( str, _T("CProcess::LoadItemList") );
          }

      }

	   if (!bAllowRemote && !m_bFirstBridgeCall)
     {
          CLogNet::LogInfo( _T("Calling ServiceXMLExe"), _T("CProcess::LoadItemList") );
		    CClientNet::SetServiceXMLExe( m_GetServiceXMLExe );
     }
  }

  return m_pItemList;
}

BOOL CProcess::SeedItemList()
{
	BOOL bResult = FALSE;
	CString strMethod, str;
  CItem *pItem = NULL;
  long lIndex, lCount;

	try
	{
		  InitializeServiceConfigurations();
		
          str.Format( _T("Setting DataPath=[%s] and GetServiceXMLExe=[%s]"),
					 m_DataPath, m_GetServiceXMLExe );
		  CLogNet::LogInfo( str, _T("CProcess::SeedItemList") );

		  CClientNet::SetDataPath( m_DataPath );
		  CClientNet::SetServiceXMLExe( m_GetServiceXMLExe );

		CClientNet::GetMethod( strMethod );
		CClientNet::SetMethod( _T("GetLocalItemList") );

		CLogNet::LogInfo( _T("Loading local ItemList"), _T("CProcess::SeedItemList") );
		if ( m_pItemList == NULL )
			m_pItemList = LoadItemList();

		CLogNet::LogInfo( _T("Seeding local ItemList"), _T("CProcess::SeedItemList") );

      // Clear Item List
      CClientNet::ClearNewItemList();
      
      // loop tthough each items add items
      lCount = m_pItemList->m_Items.GetSize();


      str.Format( _T("Seeding %ld Item in ItemList"), lCount );
		CLogNet::LogInfo( str, _T("CProcess::SeedItemList") );

      for ( lIndex=0; lIndex<lCount; lIndex++ )
      {
          pItem = (CItem *)m_pItemList->m_Items.GetAt(lIndex);
		    if (pItem)
          {
              str.Format( _T("Adding ItemNum=%ld Description=[%s] Amount=%5.2f ImageFileName=[%s] Category=%ld"),
                          pItem->m_lItemNum,
                          pItem->m_strDescription,
                          pItem->m_dblAmount,
						  pItem->m_strImageFileName,
						  pItem->m_lCategory);
		        CLogNet::LogInfo( str, _T("CProcess::SeedItemList") );
              CClientNet::AddItemListItem( pItem->m_lItemNum,
                                           pItem->m_strDescription,
                                           pItem->m_dblAmount,
										   pItem->m_strImageFileName,
										   pItem->m_lCategory);
               
          }
      }

      if ( lCount == 0 )
		   CLogNet::LogWarn( _T("Nothing to seed!"), _T("CProcess::SeedItemList") );
      else
      {   
		    CClientNet::SetMethod( _T("SeedItemList") );

		    CLogNet::LogInfo( _T("Must Reset so GetItems ServiceXMLExe gets called"), _T("CProcess::SeedItemList") );
		    CClientNet::ResetItemList();

            CLogNet::LogInfo( _T("Calling ServiceXMLExe by getting count"), _T("CProcess::SeedItemList") );
		    CClientNet::GetItemListCount();

          
            CLogNet::LogInfo( _T("Reload Items"), _T("CProcess::SeedItemList") );
		    CClientNet::SetMethod( strMethod );	
            LoadItemList();
      }

	}
	catch (CException *e)
	{
		e->Delete();                                    
		CLogNet::LogError( _T("Exception seeding item list"), _T("CProcess::SeedItemList"));
	}
	catch(...)
	{
		CLogNet::LogError( _T("Unhandle exception seeding item list"), _T("CProcess::SeedItemList"));
	}

	try
	{
		CClientNet::SetMethod( strMethod );	
	}
	catch (CException *e)
	{
		e->Delete();
		CLogNet::LogError( _T("Exception seeding item list (SetMethod)"), _T("CProcess::SeedItemList"));
	}
	catch(...)
	{
		CLogNet::LogError( _T("Unhandle exception seeding item list (SetMethod)"), _T("CProcess::SeedItemList"));
	}


	
	try
	{
		if ( m_pItemList )
			delete ( m_pItemList );
    }
	catch (CException *e)
	{
		e->Delete();
		CLogNet::LogError( _T("Exception deleting item list after Seeding so it gets reloaded)"), _T("CProcess::SeedItemList"));
	}
	catch(...)
	{
		CLogNet::LogError( _T("Unhandle exception deleting item list after Seeding so it gets reloaded)"), _T("CProcess::SeedItemList"));
	}
    m_pItemList = NULL;


	return bResult;
}	

COrder *CProcess::NewOrder()
{

  COrder *pOrder = new COrder( GetNewOrderNum() );

  try
  {
	  if ( pOrder )
			pOrder->m_lOrderNum = GetNewOrderNum();

	  CString str;
	  str.Format( _T("New Order Created with OrderNum = %ld"),
	               pOrder->m_lOrderNum );
	  CLogNet::LogInfo( str, _T("CProcess::NewOrder"));
    }
  	catch (CException *e)
	{
		e->Delete();
		CLogNet::LogInfo( _T("Exception creating new order"), _T("CProcess::NewOrder"));
	}
	catch(...)
	{
		CLogNet::LogInfo( _T("Unhandle exception creating new ordert"), _T("CProcess::NewOrder"));
	}

  return pOrder;

} 

COrder *CProcess::LoadOrder( long lOrderNum,
                             long lDestination )
{
	  CString str;
	  str.Format( _T("LoadOrder not implemented OrderNum = %ld with Destination = %ld"),
	               lOrderNum, lDestination );
	  CLogNet::LogInfo( str, _T("CProcess::LoadOrder"));
    AfxMessageBox( str );

  return NULL;
}
                           
BOOL   CProcess::SaveOrder( COrder *pOrder,
                            long    lDestination )
{
	CString str;

  if ( pOrder )
  {
	    str.Format( _T("SaveOrder not implemented OrderNum = %ld with Destination = %ld"),
	                 pOrder->m_lOrderNum, lDestination );
	    CLogNet::LogInfo( str, _T("CProcess::SaveOrder"));

      if ( lDestination == SAVE_ROUTER )
          return SaveOrderXML( pOrder, _T("SAVEROUTER"), SAVE_ROUTER );
      else
          return SaveOrderXML( pOrder, _T("SAVEORDER"), SAVE_NORMAL );
  }
  else
  {
	  CLogNet::LogWarn( _T("Order is NULL"), _T("CProcess::SaveOrder"));
	  return FALSE;
  }
} 


BOOL   CProcess:: CalculateTotal( COrder *pOrder )
{
	BOOL bResult = FALSE;
	double dblTotal = 0.00;
	long lTaxPercent = GetTaxPercent();
	CItem *pItem = NULL;
	long lIndex, lCount;
	CString str;

	if ( pOrder )
	{
	  lCount = pOrder->m_ItemList.m_Items.GetSize();
      for ( lIndex=0; lIndex<lCount; lIndex++ )
      {
          pItem = (CItem *)pOrder->m_ItemList.m_Items.GetAt(lIndex);
		  if (pItem)
			  dblTotal += pItem->m_dblAmount * (double)pItem->m_lQty;
	  }



	  pOrder->m_dblBeforeTax = dblTotal;
	  pOrder->m_dblTax = dblTotal * ((double)lTaxPercent / 100.0);
	  pOrder->m_dblTotal = pOrder->CalculateTotal();

      str.Format( _T("Total=%5.2f"), pOrder->m_dblTotal );
	  CLogNet::LogInfo( str, _T("CProcess::CalculateTotal"));
	  bResult = TRUE;
	}


	return bResult;
}


	

BOOL   CProcess::TestException()
{
	BOOL bResult = FALSE;

	try
	{
      // Doing Divide by Zero test
	  CLogNet::LogInfo( _T("Test Divide by Zero exception"), _T("CProcess::TestException"));
      long lValue = 928;
      long lZero = 0;
      lValue = lValue / lZero;

      // Null pointer assignment test
	  CLogNet::LogInfo( _T("Test null pointer assignment exception"), _T("CProcess::TestException"));
      long *plLong = NULL;
      *plLong = lValue;

	  bResult = TRUE;
	}
	catch (CException *e)
	{
		e->Delete();
		CLogNet::LogInfo( _T("Exception occur in test"), _T("CProcess::TestException"));
	}
	catch(...)
	{
		CLogNet::LogInfo( _T("Unhandle exception occur in test"), _T("CProcess::TestException"));
	}

	return bResult;
}

BOOL   CProcess::LogTestCallStack( LPCTSTR lpMsg )
{
    CString str;
    str.Format( _T("Testing Logging Stack with Message [%s]"), lpMsg );
	  CLogNet::LogInfo( str, _T("CProcess::LogTestCallStack"));

    LogCallStack( str );  

	return TRUE;
}



BOOL   CProcess::TestCallStack()
{
	  CLogNet::LogInfo( _T("Test Logging Call Stack manually"), _T("CProcess::TestCallStack"));
    LogTestCallStack( _T("From TestCallStack") );

	return TRUE;
}

long       CProcess::GetNewOrderNum()
{
  
  long lOrderNum  = (long)::GetPrivateProfileInt(_T("GENERAL"), _T("OrderNum"), 10001, _T("..\\data\\Legacy.ini") );
  long lNewID = lOrderNum + 1;
  if ( lNewID > 99999 )
      lNewID = 10001;


  CString str;
  str.Format( _T("%ld"), lNewID );
  ::WritePrivateProfileString(_T("GENERAL"), _T("OrderNum"), str, _T("..\\data\\Legacy.ini") );

	return lOrderNum;

}

long       CProcess::GetNewContextID()
{                                                                 
  long lID  = (long)::GetPrivateProfileInt(_T("GENERAL"), _T("CONTEXTID"), 1, _T("..\\data\\Legacy.ini") );
  long lNewID = lID + 1;
  if ( lNewID > 99999 )
      lNewID = 1;

  CString str;
  str.Format( _T("%ld"), lNewID );
  ::WritePrivateProfileString(_T("GENERAL"), _T("CONTEXTID"), str, _T("..\\data\\Legacy.ini") );

	return lID;

}

long       CProcess::GetNewCustomerNum()
{                                                                 
  long lID  = (long)::GetPrivateProfileInt(_T("GENERAL"), _T("CustomerNum"), 1001, _T("..\\data\\Legacy.ini") );
  long lNewID = lID + 1;
  if ( lNewID > 99999 )
      lNewID = 1001;

  CString str;
  str.Format( _T("%ld"), lNewID );
  ::WritePrivateProfileString(_T("GENERAL"), _T("CustomerNum"), str, _T("..\\data\\Legacy.ini") );

  return lID;

}

long       CProcess::GetTaxPercent()
{                                 
  // simple value for demostration
  long lPercent  = (long)::GetPrivateProfileInt(_T("GENERAL"), _T("TAXPERCENT"), 6, _T("..\\data\\Legacy.ini") );

  return lPercent;

}



void CProcess::CreateContextID( COrder   *pOrder,
                                LPCTSTR lpMethod,  
                                CString  &strID )
{
  long lContextID = GetNewContextID();
  long lOrderNum = 0;

  if ( pOrder )
      lOrderNum = pOrder->m_lOrderNum;
  else 
	  lOrderNum = GetNewOrderNum();

  strID.Format( _T("%05ld_%05ld_%s"), 
                lOrderNum, lContextID, lpMethod );    

}
      


BOOL  CProcess::SaveOrderXML( COrder *pOrder,
                              LPCTSTR lpMethod,
                              long    lDestination )
{
  BOOL bResult = FALSE;
  CContext *pContext = new CContext();
  
  try
  {
	if ( pContext )
	{
      pContext->m_Method = lpMethod;
	  pContext->m_Location = m_Location;
	  pContext->m_Time = GetCurrentTimeMessage();
      pContext->m_pInOrder = new COrder();
      pContext->m_pInOrder->Duplicate( pOrder );
      
      bResult = SaveContextXML( pContext, lDestination );
      delete ( pContext );      
	}
   }
	catch (CException *e)
	{
		e->Delete();
		CLogNet::LogInfo( _T("Exception occur in test"), _T("CProcess::SaveOrderXML"));
	}
	catch(...)
	{
		CLogNet::LogInfo( _T("Unhandle exception occur in test"), _T("CProcess::SaveOrderXML"));
	}
  return bResult;
}


BOOL  CProcess::SaveContextXML( CContext *pContext,
                                long      lDestination )
{
  BOOL bResult = FALSE;
  CStringArray Array;
  CString strID, str;
  TCHAR ch = 39;
  long lIndex, lCount;
  CItem *pItem;
  long lOrderNum = 0;

  
  CString strIndent = _T("                      ");

  // For legacy code, I did not waist time and just write the 
  // contents in .net serialize format for demostration of technique
  // for Router.  It is recommend this xml to be similar to fields in XMLOrder and Context.
  

  if ( pContext )
  {
	  if ( pContext->m_lRequestType != CContext::REQUEST_CHECKMESSAGE )
	  {
		CreateContextID( pContext->m_pInOrder, pContext->m_Method, strID );
		pContext->m_ID = strID;
	  }
	  else
		  strID = pContext->m_ID;

	  if ( pContext->m_pInOrder )
		  lOrderNum = pContext->m_pInOrder->m_lOrderNum;
	  

	  str.Format( _T("Saving Context XML for OrderNum=%ld ID=%s Method=%s"),
				 lOrderNum,
				 strID,
				 pContext->m_Method );
	  CLogNet::LogInfo( str, _T("CProcess::SaveContextXML"));


      str.Format( _T("<?xml version=%c1.0%c encoding=%cutf-8%c?>"), 
                  ch, ch, ch, ch );
      Array.Add( str );
      str.Format( _T("<Context xmlns:xsi=%chttp://www.w3.org/2001/XMLSchema-instance%c xmlns:xsd=%chttp://www.w3.org/2001/XMLSchema%c>"),
                  ch, ch, ch, ch );
      Array.Add( str );
     
      str.Format( _T("%s<Method>%s</Method>"), 
                  strIndent.Left(2),     
                  pContext->m_Method );
      Array.Add( str );
      str.Format( _T("%s<ID>%s</ID>"), 
                  strIndent.Left(2),     
                  strID );
      Array.Add( str );

	  str.Format( _T("%s<RequestType>%ld</RequestType>"), 
                  strIndent.Left(2),     
				  pContext->m_lRequestType );
      Array.Add( str );
	  str.Format( _T("%s<Location>%s</Location>"), 
                  strIndent.Left(2),     
                  pContext->m_Location );
      Array.Add( str );
	   str.Format( _T("%s<Time>%s</Time>"), 
                  strIndent.Left(2),     
                  pContext->m_Time );
      Array.Add( str );

 

	  // in order
      str.Format( _T("%s<InOrder>"),
                 strIndent.Left(2) );
      Array.Add( str );

      if ( pContext->m_pInOrder )
	  {
		// Order content 
		str.Format( _T("%s<OrderNum>%ld</OrderNum>"),
			          strIndent.Left(4),     
				      pContext->m_pInOrder->m_lOrderNum );
		Array.Add( str );
		str.Format( _T("%s<Destination>%ld</Destination>"),
			          strIndent.Left(4),     
				      pContext->m_pInOrder->m_lDestination );
		Array.Add( str );
		str.Format( _T("%s<BeforeTax>%5.2f</BeforeTax>"),
			          strIndent.Left(4),     
				      pContext->m_pInOrder->m_dblBeforeTax );
		Array.Add( str );
		str.Format( _T("%s<Tax>%5.2f</Tax>"),
			          strIndent.Left(4),     
				      pContext->m_pInOrder->m_dblTax );
		Array.Add( str );
		str.Format( _T("%s<Shipping>%5.2f</Shipping>"),
			          strIndent.Left(4),     
				      pContext->m_pInOrder->m_dblShipping );
		Array.Add( str );
		str.Format( _T("%s<Tip>%5.2f</Tip>"),
			          strIndent.Left(4),     
				      pContext->m_pInOrder->m_dblTip );
		Array.Add( str );
		str.Format( _T("%s<Total>%5.2f</Total>"),
			          strIndent.Left(4),     
				      pContext->m_pInOrder->m_dblTotal );
		Array.Add( str );

		// handle items
		str.Format( _T("%s<Items>"),
			         strIndent.Left(4) );
		Array.Add( str );

		// loop though each item
		lCount = pContext->m_pInOrder->m_ItemList.m_Items.GetSize();
		for ( lIndex=0; lIndex<lCount; lIndex++ )
		{
			  pItem = (CItem *)pContext->m_pInOrder->m_ItemList.m_Items.GetAt(lIndex);
			if ( pItem )
			{
				str.Format( _T("%s<Item>"),
				          strIndent.Left(6) );
				Array.Add( str );

				str.Format( _T("%s<ItemNum>%ld</ItemNum>"),
				          strIndent.Left(8),     
					      pItem->m_lItemNum );
				Array.Add( str );
				str.Format( _T("%s<Description>%s</Description>"),
                          strIndent.Left(8),     
                          pItem->m_strDescription );
				Array.Add( str );
				str.Format( _T("%s<Amount>%5.2f</Amount>"),
					      strIndent.Left(8),     
                          pItem->m_dblAmount );
				Array.Add( str );
				str.Format( _T("%s<Quantity>%ld</Quantity>"),
						  strIndent.Left(8),     
                          pItem->m_lQty );
				Array.Add( str );
				str.Format( _T("%s<Category>%ld</Category>"),
						  strIndent.Left(8),     
                          pItem->m_lCategory );
				Array.Add( str );

				str.Format( _T("%s</Item>"),
				          strIndent.Left(6) );
				Array.Add( str );
			}
		}

		str.Format( _T("%s</Items>"),
			         strIndent.Left(4) );
		Array.Add( str );
	 }

          // in order
      str.Format( _T("%s<CustomerObj>"),
                 strIndent.Left(4) );
      Array.Add( str );

	  if ( pContext->m_pInOrder && ( pContext->m_pInOrder->m_pCustomer ) )
	  {
		  str.Format( _T("%s<CustomerNum>%ld</CustomerNum>"),
                      strIndent.Left(6),     
                      pContext->m_pInOrder->m_pCustomer->m_lCustomerNum );
	      Array.Add( str );
		  str.Format( _T("%s<Name>%s</Name>"),
                      strIndent.Left(6),     
                      pContext->m_pInOrder->m_pCustomer->m_Name );
	      Array.Add( str );
		   str.Format( _T("%s<Address>%s</Address>"),
                      strIndent.Left(6),     
                      pContext->m_pInOrder->m_pCustomer->m_Address );
	      Array.Add( str );
		  str.Format( _T("%s<City>%s</City>"),
                      strIndent.Left(6),     
                      pContext->m_pInOrder->m_pCustomer->m_City );
	      Array.Add( str );
		  str.Format( _T("%s<State>%s</State>"),
                      strIndent.Left(6),     
                      pContext->m_pInOrder->m_pCustomer->m_State );
	      Array.Add( str );
		  str.Format( _T("%s<Zip>%s</Zip>"),
                      strIndent.Left(6),     
                      pContext->m_pInOrder->m_pCustomer->m_Zip );
	      Array.Add( str );
	  }

	  str.Format( _T("%s</CustomerObj>"),
                 strIndent.Left(4) );
      Array.Add( str );

	 str.Format( _T("%s</InOrder>"),
		         strIndent.Left(2) );
	 Array.Add( str );
	  
	 // Message - as .net MessageObj
	 str.Format( _T("%s<MessageObj>"),
		         strIndent.Left(2) );
	 Array.Add( str );


	 if ( pContext->m_pMessage )
	 {
		 str.Format( _T("%s<MessageType>%ld</MessageType>"),
		   		      strIndent.Left(4),     
					  pContext->m_pMessage->m_lMessageType );
		 Array.Add( str );
		 str.Format( _T("%s<ToName>%s</ToName>"),
		   		      strIndent.Left(4),     
                      pContext->m_pMessage->m_ToName );
		 Array.Add( str );
		 		 str.Format( _T("%s<FromName>%s</FromName>"),
		   		      strIndent.Left(4),     
                      pContext->m_pMessage->m_FromName );
		 Array.Add( str );
		 str.Format( _T("%s<MessageText>%s</MessageText>"),
		   		      strIndent.Left(4),     
                      pContext->m_pMessage->m_MessageText );
		 Array.Add( str );
	 }

    str.Format( _T("%s</MessageObj>"),
	           strIndent.Left(2) );
	 Array.Add( str );


 
      str = _T("</Context>");
      Array.Add( str );
          
      CString strFile;
      if ( lDestination == SAVE_ROUTER )
          strFile.Format( _T("..\\data\\Router\\%s.xml"), strID );    
      else                                
          strFile.Format( _T("..\\data\\%s.xml"), strID );    

      bResult = SaveFile( Array, strFile );      
  }

  return bResult;
}



BOOL   CProcess::SaveFile( CStringArray &Array,
                           LPCTSTR       lpFile )
{
  BOOL bResult = FALSE;
  CString strXML, str;
  long lIndex, lCount;

  lCount = Array.GetSize();
  for (lIndex=0; lIndex<lCount; lIndex++ )
  {
      str = Array.GetAt( lIndex ) + _T("\n");
      strXML += str;
  }          
 
  str.Format( _T("Saving XML [%s] to File [%s]"),
		      strXML, lpFile );
   CLogNet::LogInfo( str, _T("CProcess::SaveFile"));
  
  if ( SaveXMLFile( strXML, lpFile ) == 0 )
      bResult = TRUE;

  return bResult;
} 

long CProcess::SaveXMLFile( CString strXML,
                            CString strFile )
{
   long lRet=0;    
   int tlen=strXML.GetLength();
   char  *pmbbuf   = (char *)malloc(tlen);
      HANDLE hFile;
   DWORD dWritten=0;
   BOOL retry=TRUE;
   CString str;

   
   if ( strFile.GetLength() == 0 )
      return ( -3 ); 

   str.Format( _T("Save XML to File [%s]"),
	           strFile );
   CLogNet::LogInfo( str, _T("CProcess::SaveXMLFile"));


   int i=wcstombs(pmbbuf,strXML.GetBuffer(0),tlen);

   hFile=CreateFile(strFile,GENERIC_WRITE,0,NULL,CREATE_ALWAYS,FILE_ATTRIBUTE_NORMAL|FILE_FLAG_WRITE_THROUGH,NULL);
   if (hFile!=INVALID_HANDLE_VALUE)
   {
      if (!WriteFile(hFile,pmbbuf,tlen,&dWritten,NULL))
      {
         long lErrCode=GetLastError();	
         lRet=-2;    

		 str.Format( _T("Error Saving XML file with LastError=%ld"),
				     lErrCode );
		 CLogNet::LogInfo( str, _T("CProcess::SaveXMLFile"));
      }
      CloseHandle(hFile);
   }
   free (pmbbuf);

   return lRet;

}

BOOL	CProcess::SaveSendMessage( LPCTSTR lpToName,
								   LPCTSTR lpFromName,
				  				   LPCTSTR lpMessage,
								   LPCTSTR lpTime,
								   long    lRequestType )
{
	BOOL bResult = FALSE;
  CContext *pContext = new CContext();
  CMessage *pMessage = new CMessage();
 
  try
  {
     InitializeServiceConfigurations();
		
	if ( pContext )
	{

	  pContext->m_Location = m_Location;
      pContext->m_pInOrder = NULL;
	  if ( lpTime == NULL )
		pContext->m_Time = GetCurrentTimeMessage();
	  else
	    pContext->m_Time = lpTime;
	  
	  if (lRequestType == CContext::REQUEST_CHECKMESSAGE )
	  {
		    pContext->m_lRequestType = lRequestType;
			pContext->m_ID = _T("11111_11111_") + pContext->m_Method;
			pContext->m_Method = _T("CHECKMESSAGE");
	  }
	  else
	  {
		    pContext->m_lRequestType = CContext::REQUEST_MESSAGE;
			pContext->m_Method = _T("SENDMESSAGE");
	  }
      
	  if ( pMessage )
	  {
		  pMessage->m_ToName = lpToName;
		  pMessage->m_FromName = lpFromName;
		  pMessage->m_MessageText = lpMessage;
		  pContext->m_pMessage = pMessage;
	  }
      
	  bResult = SaveContextXML( pContext, SAVE_ROUTER );
      delete ( pContext );      
	}
   }
	catch (CException *e)
	{
		e->Delete();
		CLogNet::LogInfo( _T("Exception occur sending message"), _T("CProcess::SaveSendMessage"));
	}
	catch(...)
	{
		CLogNet::LogInfo( _T("Unhandle exception occur sending message"), _T("CProcess::SaveSendMesage"));
	}
	return bResult;
}

BOOL CProcess::GetIniString( LPCTSTR	lpSection,  
			    		     LPCTSTR	lpOption, 
							 CString    &strValue )
{
   DWORD Result;
   TCHAR szOption[255];
   BOOL bResult = FALSE;
   CString strDefault = _T("@@@@");
   CString strSection = lpSection;
   CString strOption = lpOption;
   
   strValue = _T("");
   Result = GetPrivateProfileString(strSection,
                                    strOption,
                                    strDefault,
                                    szOption,
                                    255,
                                    _T("..\\data\\Legacy.ini"));

   if (Result)
   {
      CString strTemp = szOption;
	  if ((strTemp.CompareNoCase(strDefault)) != 0 )
	  {
		strValue = strTemp;
	    bResult = TRUE;
      }
   }

   return bResult;
}

void CProcess::InitializeServiceConfigurations()
{
	CString str, strValue;

	if ( m_bServiceConfigurationNotInitialized )
	{
		m_bServiceConfigurationNotInitialized = FALSE;

		long lFlag = (long)::GetPrivateProfileInt(_T("CLIENT"), _T("USEGETSERVICEXML"), 1, _T("..\\data\\Legacy.ini") );
		if ( lFlag > 0 )
		{
			m_bUseServiceExe = TRUE;
			if ( m_bUseServiceExe )
			{
				CLogNet::LogInfo( _T("Using ServiceEXE"), _T("CProcess::InitializeServiceConfigurations"));

				if ( GetIniString( _T("CLIENT"), _T("DATAPATH"), str ) )
				{
					m_DataPath = str;
					str.Format( _T("DATAPATH=%s"), m_DataPath );
					CLogNet::LogInfo( str, _T("CProcess::InitializeServiceConfigurations"));
				}

				if ( GetIniString( _T("CLIENT"), _T("GETSERVICEXML"), str ) )
				{
					m_GetServiceXMLExe = str;
					str.Format( _T("GETSERVICEXML=%s"), m_GetServiceXMLExe );
					CLogNet::LogInfo( str, _T("CProcess::InitializeServiceConfigurations"));
				}


				
				if ( GetIniString( _T("CLIENT"), _T("LOCATION"), str ) )
					m_Location = str;
				else
					m_Location = _T("LEGACY-CLIENT");

				str.Format( _T("LOCATION=%s"), m_Location );
				CLogNet::LogInfo( str, _T("CProcess::InitializeServiceConfigurations"));

			}
			else
				CLogNet::LogInfo( _T("Not using ServiceEXE"), _T("CProcess::InitializeServiceConfigurations"));
		}
	}
}

CString &CProcess::GetLocation()
{
	InitializeServiceConfigurations();
	return m_Location;
}

CString &CProcess::GetCurrentTimeMessage()
{
	COleDateTime CurrentTime = COleDateTime::GetCurrentTime();
	m_TimeMessage = CurrentTime.Format();
	return m_TimeMessage;
}



void CProcess::TestNetException()
{
	try
	{
		CLogNet::LogInfo( _T("Calling Bridge for Test Exception"), _T("CProcess::TestNetException") );
		CClientNet::TestException();
		CLogNet::LogWarn( _T("Finish calling Bridge for Test Exception"), _T("CProcess::TestNetException") );
	}
	catch ( CException *e )
	{
		e->Delete();
		CLogNet::LogError( _T("Exception occur in trying call Bridge for net exception test"), _T("CProcess::TestNetExceptio") );
	}
	catch ( ... )
	{
		CLogNet::LogError( _T("Unhandled exception occur in trying call Bridge for net exception test"), _T("CProcess::TestNetExceptio") );
	}
}


BOOL  CProcess::HasMessage(LPCTSTR lpLastDateTime,
			  			  LPCTSTR lpLocation)
{
	BOOL bResult = FALSE;
	CString strMethod;
	InitializeServiceConfigurations();

    CClientNet::SetDataPath( m_DataPath );
    CClientNet::SetServiceXMLExe( m_GetServiceXMLExe );

	CClientNet::GetMethod( strMethod );
	CClientNet::SetMethod( _T("GetMessage") );

    bResult = CClientNet::HasMessage(lpLastDateTime, lpLocation);
	CClientNet::SetMethod( strMethod );
	return bResult;

}

BOOL   CProcess::GetMessageToName(CString &strName)
{
  return CClientNet::GetMessageToName(strName);

}


BOOL   CProcess::GetMessageFromName(CString &strName)
{
  return CClientNet::GetMessageFromName(strName);

}

BOOL CProcess::GetMessageText(CString &strText)
{
  return CClientNet::GetMessageText(strText);

}

BOOL CProcess::GetMessageLocation(CString &strLocation)
{
  return CClientNet::GetMessageLocation(strLocation);

}

BOOL CProcess::GetMessageTime(CString &strTime)
{
	return CClientNet::GetMessageTime( strTime );

}
