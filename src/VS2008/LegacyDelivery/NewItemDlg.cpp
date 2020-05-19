// NewItemDlg.cpp : implementation file
//

#include "stdafx.h"
#include "LegacyDelivery.h"
#include "NewItemDlg.h"
#include "order.h"
#include "process.h"
#include "lognet.h"


// CNewItemDlg dialog

IMPLEMENT_DYNAMIC(CNewItemDlg, CDialog)

CNewItemDlg::CNewItemDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CNewItemDlg::IDD, pParent)
{
   m_pItem = new CItem();
   m_pItemList = NULL;
}

CNewItemDlg::~CNewItemDlg()
{
  if ( m_pItem )
      delete ( m_pItem );
  m_pItem = NULL;
  m_pItemList = NULL;
}

void CNewItemDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_ITEMCOMBO, m_ItemCombo);
	DDX_Text(pDX, IDC_QTYCOMBO, m_QtyCombo);
}


CItem *CNewItemDlg::GetItem()
{
	long lQty = 0;
	CItem *pItem;
	CItem *pNewItem = NULL;
	

	CString  str;
	str.Format( _T("Item=[%s] Qty=[%s]"), m_ItemCombo, m_QtyCombo );
	CLogNet::LogInfo( str, _T("CNewItemDlg::GetItem") );
	//AfxMessageBox( str );

	if ( m_pItemList )
	{
		pItem = m_pItemList->GetItem( m_ItemCombo );
		if ( pItem )
		{
			pNewItem = new CItem();
			pNewItem->Duplicate( pItem );
			pNewItem->m_lQty = _ttol(m_QtyCombo);
		}
	}


	return pNewItem;
}

void   CNewItemDlg::SetControls()
{
    long lIndex, lCount;
    CItem *pItem;
    CString str;

		LOGFONT logFont;
	memset(&logFont, 0, sizeof(LOGFONT));
	logFont.lfHeight = 14;
	//logFont.lfWeight = FW_BOLD;
	lstrcpy(logFont.lfFaceName,  _T("Courier New") );
	m_FontCombo.CreateFontIndirect(&logFont);

	CComboBox *pComboBox = (CComboBox*) GetDlgItem(IDC_ITEMCOMBO);
	if ( pComboBox )
      pComboBox->SetFont(&m_FontCombo);

    // Load items
    m_pItemList = CProcess::LoadItemList();
    if ( m_pItemList )
    {
      lCount = m_pItemList->m_Items.GetSize();
      for ( lIndex=0; lIndex<lCount; lIndex++ )
      {
          pItem = (CItem*)m_pItemList->m_Items.GetAt( lIndex );
          if ( pItem )
          {
              pItem->MakeListString( str );
              ((CComboBox*)GetDlgItem(IDC_ITEMCOMBO))->AddString( str );
              ((CComboBox*)GetDlgItem(IDC_ITEMCOMBO))->SetItemData( lIndex, (DWORD_PTR)pItem );
              
          }
      }
    }
	((CComboBox*)GetDlgItem(IDC_ITEMCOMBO))->SetCurSel( 0 );

    // for qty just use 1 - 5
    ((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->AddString( _T("1") );
	((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->SetItemData( 0, (DWORD_PTR)1 );
    ((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->AddString( _T("2") );
	((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->SetItemData( 0, (DWORD_PTR)2 );
    ((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->AddString( _T("3") );
	((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->SetItemData( 0, (DWORD_PTR)3 );
    ((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->AddString( _T("4") );
	((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->SetItemData( 0, (DWORD_PTR)4 );
    ((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->AddString( _T("5") );
    ((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->SetItemData( 0, (DWORD_PTR)5 );
    ((CComboBox*)GetDlgItem(IDC_QTYCOMBO))->SetCurSel( 0 );
}



BEGIN_MESSAGE_MAP(CNewItemDlg, CDialog)
END_MESSAGE_MAP()


// CNewItemDlg message handlers

BOOL CNewItemDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// TODO:  Add extra initialization here
	SetControls();

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}
