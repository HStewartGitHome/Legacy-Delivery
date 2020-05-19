
#include <stdafx.h>
#include "LogNet.h"
#include "logstackwalker.h"
#include "win32_exception.h"

#import "..\..\..\build\binDebug\bridge.tlb" raw_interfaces_only

Bridge::INetLogPtr spLog = NULL;
BOOL CLogNet:: m_bNotInitialized = TRUE;

CLogNet::CLogNet()
{
}



CLogNet::~CLogNet()
{
  Shutdown();

}

void CLogNet::Shutdown()
{
   spLog = NULL;

}


long CLogNet::LogDebug( LPCTSTR lpMsg, LPCTSTR lpFunction )
{                             
    return LogDotNet( LOG_DEBUG, lpMsg, lpFunction );
}

long CLogNet::LogInfo( LPCTSTR lpMsg, LPCTSTR lpFunction )
{
    return LogDotNet( LOG_INFO, lpMsg, lpFunction );
}

long CLogNet::LogWarn( LPCTSTR lpMsg, LPCTSTR lpFunction )
{
    return LogDotNet( LOG_WARN, lpMsg, lpFunction );
}

long CLogNet::LogError( LPCTSTR lpMsg, LPCTSTR lpFunction )
{
    return LogDotNet( LOG_ERROR, lpMsg, lpFunction );
}

long CLogNet::LogFatal( LPCTSTR lpMsg, LPCTSTR lpFunction )
{
    return LogDotNet( LOG_FATAL, lpMsg, lpFunction );
}

long CLogNet::LogDotNet( long lType, LPCTSTR lpMsg, LPCTSTR lpFunction )
{
	long lResult = 1;
    CreateCOMInstances();

    BSTR bstrFunction, bstrMsg;
    CString str;


    str = lpFunction;
    bstrFunction = str.AllocSysString();          
    str = lpMsg;
    bstrMsg = str.AllocSysString();          
    long lRt = 0;


    if ( spLog == NULL )
    {
       TRACE(_T("spLog is NULL in CLogNet::LogDotNet") );
       lResult = -1;
    }
    else
    {
       switch ( lType )
       {  
          case LOG_DEBUG:
              spLog->LogDebug( bstrMsg, bstrFunction, &lRt );  
              break;
          case LOG_INFO:
              spLog->LogInfo( bstrMsg, bstrFunction, &lRt );  
              break;
          case LOG_WARN:
              spLog->LogWarn( bstrMsg, bstrFunction, &lRt );  
              break;
          case LOG_FATAL:
              spLog->LogFatal( bstrMsg, bstrFunction, &lRt );  
              break;
          case LOG_ERROR:
          default:
              spLog->LogError( bstrMsg, bstrFunction, &lRt );  
              break;
       }
	     lResult = 0;
    }

    SysFreeString( bstrFunction );
    SysFreeString( bstrMsg );

    return lResult;
}

void CLogNet::LogRawCallStack( LPCTSTR lpMsg, 
                               long    lLineNo,
                               LPCTSTR lpFile )
{
  CString strMsg;
                                      
  strMsg.Format( _T("***** Start  CALLSTACK for [%s] from Line %ld of File [%s] *****"), lpMsg, lLineNo, lpFile );
  LogError( strMsg, _T("CALLSTACK") );

  CLogStackWalker sw;
  sw.ShowCallstack();

  strMsg.Format( _T("***** Finish CALLSTACK for [%s] from Line %ld of File [%s] *****"), lpMsg, lLineNo, lpFile );
  LogError( strMsg, _T("CALLSTACK") );

} 


void CLogNet::LogExceptionStack ( LPCTSTR        lpMsg,  
                                  const CONTEXT *context )
{
  CLogStackWalker sw;
  
  if ( context )
      sw.ShowCallstack(  GetCurrentThread(), context );
  else
      sw.ShowCallstack();
  LogError( lpMsg, _T("CALLSTACK") );

}


void CLogNet::Initialize()
{
  win32_exception::install_handler();

} 



void CLogNet::CreateCOMInstances()
{
  if ( m_bNotInitialized )
  {
      CoInitialize( NULL );
	    spLog.CreateInstance(__uuidof( Bridge::NetLog ) );
      m_bNotInitialized = FALSE;
  }
}

