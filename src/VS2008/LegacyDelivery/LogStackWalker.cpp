#include "stdafx.h"
#include "lognet.h"

#include "LogStackWalker.h"



CLogStackWalker::CLogStackWalker()
{
  this->m_options = (int)StackWalkOptions::RetrieveSymbol;
}

CLogStackWalker::~CLogStackWalker()
{
}

void CLogStackWalker::OnOutput(LPCSTR buffer)
{
   CString strMsg;
   CharStringToCString( (char *)buffer, 512, strMsg );

   strMsg.Replace( _T("\n"), _T("") );

   CLogNet::LogInfo( strMsg, _T("CALLSTACK") );
}

void CLogStackWalker::CharStringToCString( char    *lpChar, 
                                           long     lMaxSize,
                                           CString &str )
{
   long lIndex;
   
   str.Empty();               
   for ( lIndex=0; lIndex<lMaxSize ; lIndex++ )
   {	
	   if( ! isascii( lpChar[lIndex] ) )
	   {
		   str += _T('\0');
		   break;
	   }	
	   str += ( TCHAR ) lpChar[lIndex];
   }
} 
