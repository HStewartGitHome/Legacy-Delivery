// TestDlg.cpp : implementation file
//

#include "stdafx.h"
#include "LegacyDelivery.h"
#include "TestDlg.h"
#include "process.h"
#include "LogNet.h"


// CTestD dialog

IMPLEMENT_DYNAMIC(CTestDlg, CDialog)

CTestDlg::CTestDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CTestDlg::IDD, pParent)
{

}

CTestDlg::~CTestDlg()
{
}

void CTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CTestDlg, CDialog)
	ON_BN_CLICKED(IDC_EXCEPTION, &CTestDlg::OnBnClickedException)
	ON_BN_CLICKED(IDC_NETEXCEPTION, &CTestDlg::OnBnClickedNetexception)
	ON_BN_CLICKED(IDC_LOGCALLSTACK, &CTestDlg::OnBnClickedLogcallstack)
END_MESSAGE_MAP()


// CTestDlg message handlers

void CTestDlg::OnBnClickedException()
{
	// TODO: Add your control notification handler code here
	CLogNet::LogInfo( _T("Testing Exception"), _T("CTestDlg::OnBnClickedException") );
	CProcess::TestException();
	CLogNet::LogInfo( _T("Finish testing  Exception"), _T("CTestDlg::OnBnClickedException") );
}

void CTestDlg::OnBnClickedNetexception()
{
	// TODO: Add your control notification handler code here
	CLogNet::LogInfo( _T("Testing .Net Exception"), _T("CTestDlg::OnBnClickedNetException") );
	CProcess::TestNetException();
	CLogNet::LogInfo( _T("Finish esting .Net Exception"), _T("CTestDlg::OnBnClickedNetException") );
}

void CTestDlg::OnBnClickedLogcallstack()
{
	// TODO: Add your control notification handler code here
	CLogNet::LogInfo( _T("Testing call loging call stack"), _T("CTestDlg::OnBnClickedLogcallstack") );
	CProcess::TestCallStack();
	CLogNet::LogInfo( _T("Finish testing call loging call stack"), _T("CTestDlg::OnBnClickedLogcallstack")  );
}
