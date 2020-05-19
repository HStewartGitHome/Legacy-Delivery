// customerDlg.cpp : implementation file
//

#include "stdafx.h"
#include "LegacyDelivery.h"
#include "customerDlg.h"
#include "order.h"
#include "lognet.h"
#include "process.h"



// CCustomerDlg dialog

IMPLEMENT_DYNAMIC(CCustomerDlg, CDialog)

CCustomerDlg::CCustomerDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CCustomerDlg::IDD, pParent)
	, m_Name(_T(""))
	, m_Address(_T(""))
	, m_City(_T(""))
	, m_State(_T(""))
	, m_Zip(_T(""))
{

}

CCustomerDlg::~CCustomerDlg()
{
}

void CCustomerDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_NAME, m_Name);
	DDX_Text(pDX, IDC_ADDRESS, m_Address);
	DDX_Text(pDX, IDC_CITY, m_City);
	DDX_Text(pDX, IDC_STATE, m_State);
	DDX_Text(pDX, IDC_ZIP, m_Zip);
}


BEGIN_MESSAGE_MAP(CCustomerDlg, CDialog)
	ON_BN_CLICKED(IDOK, &CCustomerDlg::OnBnClickedOk)
END_MESSAGE_MAP()


void CCustomerDlg::GetCustomerDetails( COrder *pOrder )
{
	if ( pOrder )
	{
		CLogNet::LogInfo( _T("Adding Customer Details"), _T("CustomerDlg::GetCustomerDetails") );
		if ( pOrder->m_pCustomer )
			delete (pOrder->m_pCustomer);
		pOrder->m_pCustomer = new CCustomer();
		if ( pOrder->m_pCustomer )
		{
			pOrder->m_pCustomer->m_lCustomerNum = CProcess::GetNewCustomerNum();
			pOrder->m_pCustomer->m_Name = m_Name;
			pOrder->m_pCustomer->m_Address = m_Address;
			pOrder->m_pCustomer->m_City = m_City;
			pOrder->m_pCustomer->m_State = m_State;
			pOrder->m_pCustomer->m_Zip = m_Zip;
		}
	}
}


// CCustomerDlg message handlers

void CCustomerDlg::OnBnClickedOk()
{
	// TODO: Add your control notification handler code here

	OnOK();
}
