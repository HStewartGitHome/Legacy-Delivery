
#include <stdafx.h>
#include "clientnet.h"
#include "lognet.h"

#import "..\..\..\build\binDebug\bridge.tlb" raw_interfaces_only

Bridge::IClientBridgePtr spClient = NULL;
BOOL CClientNet:: m_bNotInitialized = TRUE;

CClientNet::CClientNet()
{
}



CClientNet::~CClientNet()
{
  Shutdown();

}

void CClientNet::Shutdown()
{
   spClient = NULL;

}

long CClientNet::GetItemListCount()
{
  long lCount = 0;
  long *plCount = &lCount;
  CString str;

  CreateCOMInstances();
  if ( spClient == NULL )
	  CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetItemListCount") );
  else
  {
      HRESULT hResult = spClient->GetItemListCount( plCount );
	  str.Format( _T("GetItemListCount return Count=%ld and hResult=^ld"), lCount, hResult );
	  CLogNet::LogInfo( str, _T("CClientNet::GetItemListCount") );
  }  
 
  return lCount;

}

long CClientNet::GetItemListItemNum( long lIndex )
{
  long lValue = 0;

  CreateCOMInstances();
  if ( spClient == NULL )
        CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetItemListItemNum") );
  else
  {
      HRESULT hResult = spClient->GetItemListItemNum( lIndex, &lValue );
  }  

  return lValue;

}

BOOL CClientNet::GetItemListDescription( long     lIndex, 
                                         CString &strDescription )
{
  BOOL bResult = FALSE;
  BSTR bstrDescription;
  CString str;
  
  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetItemListDescription") );
  else
  {
      bstrDescription = ::SysAllocString( _T("space for a long description in the XML required by COM interface") );
      HRESULT hResult = spClient->GetItemListDescription( lIndex, &bstrDescription );
      str.Format( _T("HRESULT=%ld"), hResult );
      CLogNet::LogInfo( str, _T("CClientNet::GetItemListDescription") );
      _bstr_t b(bstrDescription);
      strDescription = (LPCTSTR)bstrDescription;
      ::SysFreeString( bstrDescription );
      bResult = TRUE; 
  }  

  return bResult;

}

double CClientNet::GetItemListAmount( long lIndex )
{
  double dblValue = 0.0;

  CreateCOMInstances();
  if ( spClient == NULL )
        CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetItemListAmount") );
  else
  {
      HRESULT hResult = spClient->GetItemListAmount( lIndex, &dblValue );
  }  


  return dblValue;

}

long CClientNet::GetItemListCategory( long lIndex )
{
  long lValue = 0;

  CreateCOMInstances();
  if ( spClient == NULL )
        CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetItemListCategory") );
  else
  {
      HRESULT hResult = spClient->GetItemListCategory( lIndex, &lValue );
  }  

  return lValue;

}

void CClientNet::ClearNewItemList()
{
  CreateCOMInstances();
  if ( spClient == NULL )
        CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::ClearNewItemList") );
  else
  {
      HRESULT hResult = spClient->ClearNewItemList();
  }  

}

void CClientNet::ResetItemList()
{
  CreateCOMInstances();
  if ( spClient == NULL )
        CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::ResetItemList") );
  else
  {
      HRESULT hResult = spClient->ResetItemList();
  }  

}


void CClientNet::AddItemListItem( long    lItemID,
                                  CString strDescription,
                                  double  dblAmount,
								  CString strImageFileName,
								  long    lCategory)
{
  BSTR bstrDescription, bstrImageFileName;
  CString str;
  
  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::AddItemListItem") );
  else
  {
      bstrDescription =  strDescription.AllocSysString();  
      bstrImageFileName =  strImageFileName.AllocSysString();  
      HRESULT hResult = spClient->AddItemListItem( lItemID, bstrDescription, dblAmount, bstrImageFileName, lCategory );
      str.Format( _T("HRESULT=%ld"), hResult );
  }  

}




void CClientNet::SetDataPath( LPCTSTR lpPath )
{
  CString str;
  BSTR bstr;

  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::SetDataPath") );
  else
  {
	  str = lpPath;
      bstr = str.AllocSysString();     

	  spClient->SetDataPath( bstr ); 
  }
}

void CClientNet::SetServiceXMLExe( LPCTSTR lpPath )
{
  CString str;
  BSTR bstr;

  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::SetServiceXMLExe") );
  else
  {
	  str = lpPath;
      bstr = str.AllocSysString();     

	  spClient->SetServiceXMLExe( bstr ); 
  }
}

void CClientNet::SetMethod( LPCTSTR lpMethod )
{
  CString str;
  BSTR bstr;

  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::SetMethod") );
  else
  {
	  str = lpMethod;
      bstr = str.AllocSysString();     

	  spClient->SetMethod( bstr ); 
  }
}

BOOL CClientNet::GetMethod( CString &strMethod )
{
  BOOL bResult = FALSE;
  BSTR bstrMethod;
  CString str;
  
  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetMethod") );
  else
  {
      bstrMethod = ::SysAllocString( _T("space for a long description in the XML required by COM interface") );
      HRESULT hResult = spClient->GetMethod(  &bstrMethod );
      str.Format( _T("HRESULT=%ld"), hResult );
      CLogNet::LogInfo( str, _T("CClientNet::GetMethod") );
      _bstr_t b(bstrMethod);
      strMethod = (LPCTSTR)bstrMethod;
      ::SysFreeString( bstrMethod );
      bResult = TRUE; 
  }  

  return bResult;

}


void CClientNet::CreateCOMInstances()
{
  if ( m_bNotInitialized )
  {
      CoInitialize( NULL );
	    spClient.CreateInstance(__uuidof( Bridge::Client ) );
      m_bNotInitialized = FALSE;
  }
}

// Message support


BOOL  CClientNet::HasMessage(LPCTSTR lpLastDateTime,
							 LPCTSTR lpLocation)
{
    BOOL bResult = FALSE;
	long lResult = 1;
    CreateCOMInstances();

    BSTR bstrTime, bstrLocation;
    CString str;


    str = lpLastDateTime;
    bstrTime = str.AllocSysString();          
    str = lpLocation;
    bstrLocation = str.AllocSysString();          
    BOOL bReturn = FALSE;

	CreateCOMInstances();
	if ( spClient == NULL )
		CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::HasMessage") );
	else
    {
		str.Format( _T("Calling HasMessage with LastDateTime=%s Location=%s"), lpLastDateTime, lpLocation );
		CLogNet::LogInfo( str, _T("CClientNet::HasMessage") );
		spClient->HasMessage( bstrTime, bstrLocation, &lResult );  
	    if ( lResult > 0 )
			bResult = TRUE;
		 str.Format( _T("lResult=%ld"), lResult );
         CLogNet::LogInfo( str, _T("CClientNet::HasMessage") );
    }

    SysFreeString( bstrTime );
    SysFreeString( bstrLocation );

    return bResult;
}

BOOL   CClientNet::GetMessageToName(CString &strName)
{
  BOOL bResult = FALSE;
  BSTR bstr;
  CString str;
  
  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetMessageToName") );
  else
  {
      bstr = ::SysAllocString( _T("space for a long description in the XML required by COM interface") );
      HRESULT hResult = spClient->GetMessageToName(  &bstr );
      str.Format( _T("HRESULT=%ld"), hResult );
      CLogNet::LogInfo( str, _T("CClientNet::GetMessageToName") );
      _bstr_t b(bstr);
      strName = (LPCTSTR)bstr;
      ::SysFreeString( bstr );
      bResult = TRUE; 
  }  

  return bResult;

}

BOOL   CClientNet::GetMessageFromName(CString &strName)
{
  BOOL bResult = FALSE;
  BSTR bstr;
  CString str;
  
  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetMessageFromName") );
  else
  {
      bstr = ::SysAllocString( _T("space for a long description in the XML required by COM interface") );
      HRESULT hResult = spClient->GetMessageFromName(  &bstr );
      str.Format( _T("HRESULT=%ld"), hResult );
      CLogNet::LogInfo( str, _T("CClientNet::GetMessageFromName") );
      _bstr_t b(bstr);
      strName = (LPCTSTR)bstr;
      ::SysFreeString( bstr );
      bResult = TRUE; 
  }  

  return bResult;

}

BOOL CClientNet::GetMessageText(CString &strText)
{
  BOOL bResult = FALSE;
  BSTR bstr;
  CString str;
  
  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetMessageText") );
  else
  {
      bstr = ::SysAllocString( _T("space for a long description in the XML required by COM interface") );
      HRESULT hResult = spClient->GetMessageText(  &bstr );
      str.Format( _T("HRESULT=%ld"), hResult );
      CLogNet::LogInfo( str, _T("CClientNet::GetMessageText") );
      _bstr_t b(bstr);
      strText = (LPCTSTR)bstr;
      ::SysFreeString( bstr );
      bResult = TRUE; 
  }  

  return bResult;

}

BOOL CClientNet::GetMessageLocation(CString &strLocation)
{
  BOOL bResult = FALSE;
  BSTR bstr;
  CString str;
  
  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetMessageLocation") );
  else
  {
      bstr = ::SysAllocString( _T("space for a long description in the XML required by COM interface") );
      HRESULT hResult = spClient->GetMessageLocation(  &bstr );
      str.Format( _T("HRESULT=%ld"), hResult );
      CLogNet::LogInfo( str, _T("CClientNet::GetMessageLocation") );
      _bstr_t b(bstr);
      strLocation = (LPCTSTR)bstr;
      ::SysFreeString( bstr );
      bResult = TRUE; 
  }  

  return bResult;

}

BOOL CClientNet::GetMessageTime(CString &strTime)
{
  BOOL bResult = FALSE;
  BSTR bstr;
  CString str;
  
  CreateCOMInstances();
  if ( spClient == NULL )
       CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::GetMessageTime") );
  else
  {
      bstr = ::SysAllocString( _T("space for a long description in the XML required by COM interface") );
      HRESULT hResult = spClient->GetMessageTime(  &bstr );
      str.Format( _T("HRESULT=%ld"), hResult );
      CLogNet::LogInfo( str, _T("CClientNet::GetMessageTime") );
      _bstr_t b(bstr);
      strTime = (LPCTSTR)bstr;
      ::SysFreeString( bstr );
      bResult = TRUE; 
  }  

  return bResult;

}


void CClientNet::TestException()
{
	try
	{
		  CreateCOMInstances();
		  if ( spClient == NULL )
				CLogNet::LogWarn( _T("spClient is null"), _T("CClientNet::TestException") );
		  else
		  {
			  	CLogNet::LogInfo( _T("Calling Bridge for Test Exception"), _T("CClientNet::TestException") );
				HRESULT hResult = spClient->TestException();
				CLogNet::LogInfo( _T("Finish calling Bridge for Test Exception"), _T("CClientNet::TestException") );
		  }
	}
	catch ( CException *e )
	{
		e->Delete();
		CLogNet::LogError( _T("Exception occur in trying call Bridge for net exception test"), _T("CClientNet::TestExceptio") );
	}
	catch ( ... )
	{
		CLogNet::LogError( _T("Unhandled exception occur in trying call Bridge for net exception test"), _T("CClientNet::TestExceptio") );
	}
}