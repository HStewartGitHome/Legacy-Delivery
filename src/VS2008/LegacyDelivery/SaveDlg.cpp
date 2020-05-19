// SaveDlg.cpp : implementation file
//

#include "stdafx.h"
#include "LegacyDelivery.h"
#include "SaveDlg.h"
#include "lognet.h"
#include "process.h"


// SaveDlg dialog

IMPLEMENT_DYNAMIC(CSaveDlg, CDialog)


CSaveDlg::CSaveDlg( CWnd* pParent /*=NULL*/ )
	: CDialog(CSaveDlg::IDD, pParent),
    m_pOrder( NULL )
{

}

CSaveDlg::~CSaveDlg()
{
}

void CSaveDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CSaveDlg, CDialog)
	ON_BN_CLICKED(IDC_SAVEROUTER, &CSaveDlg::OnBnClickedSaverouter)
	ON_BN_CLICKED(IDC_SAVELOCAL, &CSaveDlg::OnBnClickedSavelocal)
END_MESSAGE_MAP()


// SaveDlg message handlers

void CSaveDlg::OnBnClickedSaverouter()
{
	// TODO: Add your control notification handler code here
  CLogNet::LogInfo( _T("Saving Order to Router"), _T("CSaveDlg::OnBnClickedSaverouter")  );
  CProcess::SaveOrder( m_pOrder, CProcess::SAVE_ROUTER );

  AfxMessageBox( _T("Order saved for router") );

  
  OnOK();
}

void CSaveDlg::OnBnClickedSavelocal()
{
	// TODO: Add your control notification handler code here

  	int nResult = AfxMessageBox(_T("Do you want to save local Order?"), MB_YESNO|MB_ICONQUESTION);

	if(nResult == IDYES)
	{
		nResult = AfxMessageBox(_T("Loading local Order is current not supported, Are you sure?"), MB_YESNO|MB_ICONQUESTION);
		if(nResult == IDYES)
		{	
			CLogNet::LogInfo( _T("Saving Order to local"), _T("CSaveDlg::OnBnClickedSavelocal")  );

			CProcess::SaveOrder( m_pOrder, CProcess::SAVE_NORMAL );

			AfxMessageBox( _T("Order saved to local storage") );
			OnOK();
		}
	} 
}


void CSaveDlg::SetOrder( COrder *pOrder )
{
	m_pOrder = pOrder;
}
