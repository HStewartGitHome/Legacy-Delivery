// Order.cpp : implementation file
//

#include "stdafx.h"
#include "Order.h"
#include "LogNet.h"

// CItem object

CItem::CItem()
  :   m_lItemNum( 0 ),     
      m_dblAmount( 0.00 ),
      m_lQty( 1 )
{
}

CItem::CItem( long     lItemNum,
              LPCTSTR  lpDesc )
  :   m_lItemNum( lItemNum ),     
      m_dblAmount( 0.00 ),
      m_lQty( 1 ),
	  m_lCategory( 0 )
{
  m_strDescription = lpDesc;
}

CItem::~CItem()
{
}

void CItem::Duplicate( CItem *pObj )
{
  if ( pObj )
  {
      m_lItemNum= pObj->m_lItemNum;
      m_strDescription = pObj->m_strDescription;
      m_dblAmount = pObj->m_dblAmount;
      m_lQty = pObj->m_lQty;
	  m_strImageFileName = pObj->m_strImageFileName;
	  m_lCategory = pObj->m_lCategory;
  }
}

void  CItem::MakeListString( CString &str, 
							 long    lType )
{
  CString strOut,strUse;
  CString strPad = _T("                                                                 ");

  strOut = m_strDescription + strPad;
  if ( lType == PICKLIST )
  {
	 strUse = strOut.Left(56);
	 str.Format( _T("%56s     $%5.2f"),
					strUse, m_dblAmount );
  }
  else
  {
	  strUse = strOut.Left(56);
	  str.Format( _T("%3ld    %56s   $%5.2f"),
				  m_lQty, strUse, m_dblAmount );
  }
}

// CItemList object

CItemList::CItemList()
{
}

CItemList::~CItemList()
{
  ClearItems();
}

void CItemList::ClearItems()
{
  long lIndex, lCount;
  CItem *pItem;

  lCount = m_Items.GetSize();
  for ( lIndex=0; lIndex<lCount; lIndex++ )
  {
      pItem = (CItem*)m_Items.GetAt( lIndex );
      if ( pItem )
          delete ( pItem );
  }
}

void CItemList::Duplicate( CItemList *pObj )
{
  long lIndex, lCount;
  CItem *pItem, *pNewItem;

  if ( pObj )
  {
      ClearItems();

      lCount = pObj->m_Items.GetSize();
      for ( lIndex=0; lIndex<lCount; lIndex++ )
      {
          pItem = (CItem*)pObj->m_Items.GetAt( lIndex );
          if ( pItem )
          {
              pNewItem = new CItem();
			    pNewItem->Duplicate( pItem );
              m_Items.Add( pNewItem );
          }
      }

  }
}

CItem *CItemList::GetItem( LPCTSTR lpText,
				           long    lType )
{
	CItem *pRetItem = NULL;
	CItem *pItem;
	long lIndex, lCount;
	CString strCheck = lpText;
	CString str;

	lCount = m_Items.GetSize();
     for ( lIndex=0; lIndex<lCount; lIndex++ )
     {
          pItem = (CItem*)m_Items.GetAt( lIndex );
          if ( pItem )
          {
			  pItem->MakeListString( str, lType );
			  if ( strCheck.CollateNoCase( str ) == 0 )
			  {
				  pRetItem = pItem;
				  lIndex = lCount;
			  }
		  }
	  }

	  return pRetItem;
}

// COrder object


CCustomer::CCustomer( long lCustomerNum )
  : m_lCustomerNum( lCustomerNum )
{
}

CCustomer::~CCustomer()
{
}

void CCustomer::Duplicate( CCustomer *pObj )
{
   if ( pObj )
   {
	   m_lCustomerNum = pObj->m_lCustomerNum;
	   m_Name = pObj->m_Name;
       m_Address = pObj->m_Address;
	   m_City = pObj->m_City;
	   m_State = pObj->m_State;
	   m_Zip = pObj->m_Zip;
   }
}


// COrder object


COrder::COrder( long lOrderNum )
  : m_lOrderNum( lOrderNum ),
    m_lDestination( DEST_LEGACY_ORDER ),
    m_dblBeforeTax( 0.00 ),
    m_dblTax( 0.00 ),
    m_dblShipping( 0.00 ),
    m_dblTip( 0.00 ),
    m_dblTotal( 0.00 ),
	m_pCustomer( NULL )
{
}

COrder::~COrder()
{
	if ( m_pCustomer )
       delete ( m_pCustomer );
}

void COrder::Duplicate( COrder *pObj )
{
   if ( pObj )
   {
       m_ItemList.Duplicate( &pObj->m_ItemList );
       m_lOrderNum = pObj->m_lOrderNum;
	   m_lDestination = pObj->m_lDestination;
       m_dblBeforeTax = pObj->m_dblBeforeTax;
       m_dblTax = pObj->m_dblTax;
       m_dblShipping = pObj->m_dblShipping;
       m_dblTip = pObj->m_dblTip;
       m_dblTotal = pObj->m_dblTotal;

	   // duplicate InOrder 
       if ( m_pCustomer )
          delete ( m_pCustomer );
       if ( pObj->m_pCustomer )
       {
          m_pCustomer = new CCustomer();
          m_pCustomer->Duplicate( pObj->m_pCustomer );
       }
       else
          m_pCustomer = NULL;
   }
}

double  COrder::CalculateTotal()
{
	m_dblTotal = m_dblBeforeTax + m_dblTax + m_dblShipping + m_dblTip;
	

	return m_dblTotal;
}

// CMessage
CMessage::CMessage( long lMessageType  )
  : m_lMessageType( lMessageType )
{
}

CMessage::~CMessage()
{
}

void CMessage::Duplicate( CMessage *pObj )
{

   if ( pObj )
   {
	   m_lMessageType = pObj->m_lMessageType;
	   m_ToName = pObj->m_ToName;
	   m_FromName = pObj->m_FromName;
       m_MessageText = pObj->m_MessageText;
   }
}


// CContext

CContext::CContext()
: m_pInOrder( NULL ),
  m_pMessage( NULL ),
  m_lRequestType( REQTYPE_ORDER )
{
}

CContext::~CContext()
{
	if ( m_pInOrder )
		delete ( m_pInOrder );
	if ( m_pMessage )
		delete ( m_pMessage );
}	

void CContext::Duplicate( CContext *pObj )
{
  if ( pObj )
  {
      m_Method = pObj->m_Method;
      m_ID = pObj->m_ID;
	  m_lRequestType = pObj->m_lRequestType;
	  m_Location = pObj->m_Location;
	  m_Time = pObj->m_Time;
 
      // duplicate InOrder 
      if ( m_pInOrder )
          delete ( m_pInOrder );
      if ( pObj->m_pInOrder )
      {
          m_pInOrder = new COrder();
          m_pInOrder->Duplicate( pObj->m_pInOrder );
      }
      else
         m_pInOrder = NULL;

	  // duplicate Message
      if ( m_pMessage )
          delete ( m_pMessage );
      if ( pObj->m_pInOrder )
      {
          m_pMessage = new CMessage();
          m_pMessage->Duplicate( pObj->m_pMessage );
      }
      else
         m_pMessage = NULL;
  }
}
