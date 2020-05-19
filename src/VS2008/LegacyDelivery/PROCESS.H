#pragma once


class COrder;
class CContext;
class CItemList;

// CProcess object

class CProcess : public CObject
{
   public:
   CProcess();
   virtual ~CProcess();

   const enum { SAVE_ROUTER      = 0,
                SAVE_NORMAL      = 1 };
   
   static CItemList *LoadItemList( BOOL bAllowRemote = TRUE );
   static BOOL      SeedItemList();
   static COrder    *NewOrder();
   static COrder    *LoadOrder( long lOrderNum,
                                long lDestination = SAVE_NORMAL );
   static BOOL       SaveOrder( COrder *pOrder,
                                long    lDestination = SAVE_NORMAL );

   static BOOL       CalculateTotal( COrder *pOrder );

   // testing
   static BOOL       TestException();
   static BOOL       LogTestCallStack( LPCTSTR lpMsg );
   static BOOL       TestCallStack(); 
   static void		 TestNetException();


   // support
   static long       GetNewOrderNum();
   static long       GetNewContextID();
   static long       GetNewCustomerNum();
   static long       GetTaxPercent();
   static void       CreateContextID( COrder   *pOrder,
                                      LPCTSTR lpMethod,
                                      CString  &strID );
   static BOOL       SaveOrderXML( COrder *pOrder,
                                   LPCTSTR lpMethod,
                                   long    lDestination = SAVE_NORMAL );
   static BOOL       SaveContextXML( CContext *pContext,
                                     long      lDestination = SAVE_NORMAL );
   static BOOL       SaveFile( CStringArray &Array,
                               LPCTSTR       lpFile );
   static long       SaveXMLFile( CString strXML,
                                  CString strFile );
   static BOOL		 SaveSendMessage( LPCTSTR lpToName,
									  LPCTSTR lpFromName,
									  LPCTSTR lpMessage,
									  LPCTSTR lpTime,
								      long    lRequestType = 0 );

   static BOOL	     GetIniString( LPCTSTR	   lpSection,  
			    		  	       LPCTSTR	   lpOption, 
							       CString    &strValue );
   static void		 InitializeServiceConfigurations();
   static CString   &GetLocation();
   static CString   &GetCurrentTimeMessage();


   
   // Get message
   static BOOL HasMessage(LPCTSTR lpLastDateTime,
						  LPCTSTR lpLocation);
   static BOOL GetMessageToName(CString &strName);
   static BOOL GetMessageFromName(CString &strName);
   static BOOL GetMessageText(CString &strText);
   static BOOL GetMessageLocation(CString &strLocation);
   static BOOL GetMessageTime(CString &strTime);



   
   static CItemList *m_pItemList;
   static BOOL	     m_bServiceConfigurationNotInitialized;
   static BOOL       m_bUseServiceExe;
   static CString    m_DataPath;
   static CString    m_GetServiceXMLExe;
   static BOOL	     m_bFirstBridgeCall;
   static CString    m_Location;
   static CString    m_TimeMessage;

};

