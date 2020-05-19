// SendMessage.cpp : implementation file
//

#include "stdafx.h"
#include "LegacyDelivery.h"
#include "SendMessagedlg.h"
#include "LogNet.h"
#include "Process.h"



// CSendMessageDlg dialog
IMPLEMENT_DYNAMIC(CSendMessageDlg, CDialog)

CSendMessageDlg::CSendMessageDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSendMessageDlg::IDD, pParent)
	, m_ToName(_T(""))
	, m_Message(_T(""))
	, m_Location(_T(""))
	, m_DateTime(_T(""))
  , m_bReadOnly( FALSE )
  , m_FromName(_T(""))
{

}

CSendMessageDlg::~CSendMessageDlg()
{
}

void CSendMessageDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_TONAME, m_ToName);
	DDX_Text(pDX, IDC_MESSAGE, m_Message);
	DDX_Text(pDX, IDC_LOCATION, m_Location);
	DDX_Text(pDX, IDC_DATETIME, m_DateTime);
	DDX_Text(pDX, IDC_FROMNAME, m_FromName);
}


BEGIN_MESSAGE_MAP(CSendMessageDlg, CDialog)
	ON_BN_CLICKED(IDSENDMESSAGE, &CSendMessageDlg::OnBnClickedSendmessage)
END_MESSAGE_MAP()


// CSendMessage message handlers

void CSendMessageDlg::OnBnClickedSendmessage()
{
	// TODO: Add your control notification handler code here
	OnOK();
}
