#pragma once

#define LogCallStack( lpMsg ) CLogNet::LogRawCallStack( lpMsg, __LINE__, CString(__FILE__) )


class  CLogNet
{
   public:
	   CLogNet();
   	virtual ~CLogNet();

      const enum { LOG_DEBUG = 1,
                   LOG_INFO  = 2,
                   LOG_WARN  = 3,
                   LOG_ERROR = 4,
                   LOG_FATAL = 5 };

      // log4net wrappers
      static long LogDebug( LPCTSTR lpMsg, LPCTSTR lpFunction );
      static long LogInfo( LPCTSTR lpMsg, LPCTSTR lpFunction );
      static long LogWarn( LPCTSTR lpMsg, LPCTSTR lpFunction );
      static long LogError( LPCTSTR lpMsg, LPCTSTR lpFunction );
      static long LogFatal( LPCTSTR lpMsg, LPCTSTR lpFunction );

      // log4net common routines
      static long LogDotNet( long lType, LPCTSTR lpMsg, LPCTSTR lpFunction );

      // Call Stack logging
      static void LogRawCallStack( LPCTSTR lpMsg, 
                                   long    lLineNo,
                                   LPCTSTR lpFile );
      static void LogExceptionStack ( LPCTSTR        lpMsg,  
                                      const CONTEXT *context );

      // initialization
      static void Initialize();


   protected:
      static void CreateCOMInstances();
	  static void Shutdown();

   private:
      static BOOL  m_bNotInitialized;
   
};



