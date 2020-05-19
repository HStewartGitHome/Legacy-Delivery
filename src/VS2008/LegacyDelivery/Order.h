#pragma once

// CItem object

class CItem : public CObject
{
   public:
      CItem();
      CItem( long     lItemNum,
             LPCTSTR  lpDesc );
      virtual  ~CItem();

	  const enum { PICKLIST, DETAILLIST };

      void     MakeListString( CString &str,
							   long    lType = PICKLIST );
      void     Duplicate( CItem *pObj );


      long     m_lItemNum;
      CString  m_strDescription;
      double   m_dblAmount;
      long     m_lQty;
      CString  m_strImageFileName;
	  long     m_lCategory;
      
};


// CItemList object

class CItemList : public CObject
{
   public:
      CItemList();
      virtual ~CItemList();

      void         ClearItems();
      void         Duplicate( CItemList *pObj );

	  CItem		  *GetItem( LPCTSTR lpText,
				            long    lType = CItem::PICKLIST );


      CObArray m_Items;
      
};

// CCustomer object

class CCustomer : public CObject
{
   public:
      CCustomer( long lCustomerNum  = 0 );
	   virtual ~CCustomer();

      void         Duplicate( CCustomer *pObj );

	  
      long        m_lCustomerNum;
	  CString     m_Name;
      CString     m_Address;
	  CString     m_City;
	  CString     m_State;
	  CString     m_Zip;

      
};


// COrder object

class COrder : public CObject
{
   public:
	   const enum { DEST_LEGACY_ORDER  = 1,
					DEST_LEGACY_ITEMS  = 2,
					DEST_MODERN  = 2 };


      COrder( long lOrderNum  = 0 );
	  virtual ~COrder();

      void         Duplicate( COrder *pObj );

	  double       CalculateTotal();

	  
      long        m_lOrderNum;
	  long        m_lDestination;
      double      m_dblBeforeTax;
      double      m_dblTax;
      double      m_dblShipping;
      double      m_dblTip;
      double      m_dblTotal;
      CItemList   m_ItemList;
      CCustomer  *m_pCustomer;
};

// CMessage object

class CMessage : public CObject
{
   public:
	   const enum { MSGTYPE_SEND = 1,
					MSGTYPE_RECEIVE = 2 };

      CMessage(long lMessageType = MSGTYPE_SEND );
	   virtual ~CMessage();

      void         Duplicate( CMessage *pObj );

	  long		  m_lMessageType;	 
	  CString     m_ToName;
	  CString     m_FromName;
      CString     m_MessageText;      
};

// Context

class CContext : public CObject
{
  public:
	  const enum { REQTYPE_ORDER		= 1,
				   REQUEST_MESSAGE		= 2,
				   REQUEST_CHECKMESSAGE = 3};

	  CContext();
     virtual ~CContext();

     void         Duplicate( CContext *pObj );


	  CString		m_Method;
	  CString       m_ID;
	  long          m_lRequestType;
	  CString		m_Location;
	  CString       m_Time;
	  COrder       *m_pInOrder;
	  CMessage     *m_pMessage;

};
