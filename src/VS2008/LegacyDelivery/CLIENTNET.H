#pragma once


class  CClientNet
{
   public:
	   CClientNet();
   	virtual ~CClientNet();

      // ItemList
      static long GetItemListCount();
      static long GetItemListItemNum( long lIndex );
      static BOOL GetItemListDescription( long     lIndex, 
                                          CString &strDescription );
      static double GetItemListAmount( long lIndex );
	  static long GetItemListCategory( long lIndex );

      static void ClearNewItemList();
	  static void ResetItemList();
      static void AddItemListItem( long    lItemID,
                                   CString strDescription,
                                   double  dblAmount,
								   CString strImageFileName,
								   long    lCategory);

      
	  // Configuration
	  static void SetDataPath( LPCTSTR lpPath );
	  static void SetServiceXMLExe( LPCTSTR lpPath );
	  static void SetMethod( LPCTSTR lpMethod );
	  static BOOL GetMethod( CString &strMethod );


	  // Get message
	  static BOOL HasMessage(LPCTSTR lpLastDateTime,
							  LPCTSTR lpLocation);
	  static BOOL GetMessageToName(CString &strName);
	  static BOOL GetMessageFromName(CString &strName);
	  static BOOL GetMessageText(CString &strText);
	  static BOOL GetMessageLocation(CString &strLocation);
	  static BOOL GetMessageTime(CString &strTime);

	  // testing
	  static void TestException();

   protected:
      static void CreateCOMInstances();
	   static void Shutdown();

   private:
      static BOOL  m_bNotInitialized;
   
};



