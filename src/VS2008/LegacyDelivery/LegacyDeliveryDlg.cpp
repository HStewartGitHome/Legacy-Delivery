
// LegacyDeliveryDlg.cpp : implementation file
//

#include "stdafx.h"
#include "LegacyDelivery.h"
#include "LegacyDeliveryDlg.h"
#include "NewItemDlg.h"
#include "TestDlg.h"
#include "Process.h"
#include "LogNet.h"
#include "savedlg.h"
#include "customerdlg.h"
#include "sendmessagedlg.h"
#include "clientnet.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CLegacyDeliveryDlg dialog




CLegacyDeliveryDlg::CLegacyDeliveryDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CLegacyDeliveryDlg::IDD, pParent)
	, m_bModified ( FALSE )
	, m_BeforeTax(_T(""))
	, m_Tax(_T(""))
	, m_Total(_T(""))
	, m_bInsideTimer( FALSE )
	, m_bAllowTimer( FALSE )
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	m_pOrder = NULL;
	

}

void CLegacyDeliveryDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_ORDERLIST, m_OrderList);
	DDX_Text(pDX, IDC_OrderNum, m_OrderNum);
	DDX_Text(pDX, IDC_BEFORETAX, m_BeforeTax);
	DDX_Text(pDX, IDC_TAX, m_Tax);
	DDX_Text(pDX, IDC_TOTAL, m_Total);
}

BEGIN_MESSAGE_MAP(CLegacyDeliveryDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
  ON_WM_TIMER()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDNEW, &CLegacyDeliveryDlg::OnBnClickedNew)
	ON_BN_CLICKED(IDLOAD, &CLegacyDeliveryDlg::OnBnClickedLoad)
	ON_BN_CLICKED(IDSAVE, &CLegacyDeliveryDlg::OnBnClickedSave)
	ON_BN_CLICKED(IDTEST, &CLegacyDeliveryDlg::OnBnClickedTest)
	ON_BN_CLICKED(IDNEWITEM, &CLegacyDeliveryDlg::OnBnClickedNewitem)
	ON_LBN_SELCHANGE(IDC_ORDERLIST, &CLegacyDeliveryDlg::OnLbnSelchangeOrderlist)
	ON_BN_CLICKED(IDOK, &CLegacyDeliveryDlg::OnBnClickedOk)
	ON_BN_CLICKED(IDSEEDLIST, &CLegacyDeliveryDlg::OnBnClickedSeedlist)
	ON_BN_CLICKED(IDSENDMSG, &CLegacyDeliveryDlg::OnBnClickedSendmsg)
END_MESSAGE_MAP()


// CLegacyDeliveryDlg message handlers

BOOL CLegacyDeliveryDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	LOGFONT logFont;
	memset(&logFont, 0, sizeof(LOGFONT));
	logFont.lfHeight = 20;
	//logFont.lfWeight = FW_BOLD;
	lstrcpy(logFont.lfFaceName, _T("Courier New") );
	m_FontBig.CreateFontIndirect(&logFont);

	CListBox *pListBox = (CListBox*) GetDlgItem(IDC_ORDERLIST);
	if ( pListBox )
      pListBox->SetFont(&m_FontBig);

   

	CLogNet::Initialize();
                                                                                              
	long lCheckTimer = (long)::GetPrivateProfileInt(_T("GENERAL"), _T("TIMERCHECK"), 10*1000, _T("..\\data\\Legacy.ini") );
  if ( lCheckTimer > 0 )
      SetTimer(TID_LEGACY, lCheckTimer, NULL);



    CLogNet::LogInfo( _T("Creating new Order"), _T("CLegacyDeliveryDlg::OnInitDialog") );
    CreateNewOrder();

	long lFlag = (long)::GetPrivateProfileInt(_T("GENERAL"), _T("ADDCUSTOMERATSTART"), 0, _T("..\\data\\Legacy.ini") );
	if ( lFlag )
	{
		CLogNet::LogInfo( _T("Adding Customer"), _T("CLegacyDeliveryDlg::OnInitDialog") );
		AddCustomerInfo();
	}

	m_bAllowTimer = TRUE;
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CLegacyDeliveryDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CLegacyDeliveryDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CLegacyDeliveryDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CLegacyDeliveryDlg::OnBnClickedNew()
{
   m_bAllowTimer = FALSE;
   if ( m_bModified )
   {
	   int nResult = AfxMessageBox(_T("Do you want to save Order?"), MB_YESNO|MB_ICONQUESTION);

	   if(nResult == IDYES)
	   {
	      CLogNet::LogInfo( _T("Do you want to save Order? (YES)"), _T("CLegacyDeliveryDlg::OnBnClickedNew") );
		  OnBnClickedSave();
	   }
	   else
		  CLogNet::LogInfo( _T("Do you want to save Order? (NO)"), _T("CLegacyDeliveryDlg::OnBnClickedNew") );
   }

   CLogNet::LogInfo( _T("Creating new Order"), _T("CLegacyDeliveryDlg::OnBnClickedNew") );
   CreateNewOrder();
   CLogNet::LogInfo( _T("Adding Customer"), _T("CLegacyDeliveryDlg::OnBnClickedNew") );
   AddCustomerInfo();
   m_bAllowTimer = TRUE;
}

void CLegacyDeliveryDlg::AddCustomerInfo()
{
	long lResult;

	if ( m_pOrder )
	{
	    CLogNet::LogInfo( _T("Adding Customer Details"), _T("CLegacyDeliveryDlg:AddCustomerInfo") );
		CCustomerDlg *pCustomerDlg = new CCustomerDlg();
	    if ( pCustomerDlg )
	    {
			// dialog will modify order
			
		    lResult = pCustomerDlg->DoModal();
			if ( lResult == IDOK )
				pCustomerDlg->GetCustomerDetails( m_pOrder );
		    delete ( pCustomerDlg );
      }
	}
}

void CLegacyDeliveryDlg::EnableControls( BOOL bEnabled )
{
	CString str;
	if ( bEnabled )
		CLogNet::LogInfo( _T("Enabling controls on screen"), _T("CLegacyDeliveryDlg::EnableControls") );
	else
		CLogNet::LogInfo( _T("Disabling controls on screen"), _T("CLegacyDeliveryDlg::EnableControls") );

	 ((CButton*)GetDlgItem(IDSAVE))->EnableWindow( bEnabled );
     ((CButton*)GetDlgItem(IDSEEDLIST))->EnableWindow( !bEnabled );
}

void CLegacyDeliveryDlg::CreateNewOrder()
{
   m_bAllowTimer = FALSE;
   try
   {

		// TODO: Add your control notification handler code here
		CLogNet::LogInfo( _T("Creating New Order"), _T("CLegacyDeliveryDlg::NewOrder") );
		if ( m_pOrder )
			delete( m_pOrder );
		m_pOrder = CProcess::NewOrder();
		m_bModified = FALSE;
		EnableControls( FALSE );
		RefreshScreen();
   }
    	catch (CException *e)
	{
		e->Delete();
		CLogNet::LogInfo( _T("Exception creating new order"), _T("CLegacyDeliveryDlg::NewOrder"));
	}
	catch(...)
	{                                                               
		CLogNet::LogInfo( _T("Unhandle exception creating new order"), _T("CLegacyDeliveryDlg::NewOrder"));
	}
	m_bAllowTimer = TRUE;
}

void CLegacyDeliveryDlg::OnBnClickedLoad()
{
	// TODO: Add your control notification handler code here
  m_bAllowTimer = FALSE;
  CreateNewOrder();
  COrder *pOrder = CProcess::LoadOrder( NULL, CProcess::SAVE_NORMAL );
  if ( pOrder )
  {
      m_pOrder = pOrder;
      RefreshScreen();
  }
  else
      CreateNewOrder();
  m_bAllowTimer = TRUE;
}

void CLegacyDeliveryDlg::OnBnClickedSave()
{
	// TODO: Add your control notification handler code here
  m_bAllowTimer = FALSE;
  long lResult = 0;
  CString str;
  

  if ( m_pOrder )
  {
      str.Format( _T("Saving OrderNum = %ld"), m_pOrder->m_lOrderNum );
	    CLogNet::LogInfo( str, _T("CLegacyDeliveryDlg::OnBnClickedSave") );

	    CSaveDlg *pSaveDlg = new CSaveDlg();
	    if ( pSaveDlg )
	    {
			pSaveDlg->SetOrder( m_pOrder );
		    lResult = pSaveDlg->DoModal();
		    delete ( pSaveDlg );
      }
	}
	CreateNewOrder();
	m_bAllowTimer = TRUE;
}


void CLegacyDeliveryDlg::OnBnClickedTest()
{
	// TODO: Add your control notification handler code here
	m_bAllowTimer = FALSE;
	long lResult = 0;

	CLogNet::LogInfo( _T("Showing Test Dialog"), _T("CLegacyDeliveryDlg::OnBnClickedTest") );
	CTestDlg *pTestDlg = new CTestDlg();
	if ( pTestDlg )
	{
		lResult = pTestDlg->DoModal();
		delete ( pTestDlg );
	}
	m_bAllowTimer = TRUE;
}

void CLegacyDeliveryDlg::OnBnClickedNewitem()
{
	// TODO: Add your control notification handler code here
  	m_bAllowTimer = FALSE;
    long lResult = 0;
	CItem *pItem = NULL;
    CString str;

  if ( m_pOrder && ( m_pOrder->m_pCustomer == NULL ))
  {
	  str.Format( _T("Adding Customer details for OrderNum = %ld"), m_pOrder->m_lOrderNum );
	  CLogNet::LogInfo( str, _T("CLegacyDeliveryDlg::OnBnClickedNewitem") );
	  AddCustomerInfo();
  }

  str.Format( _T("Getting New Item for OrderNum = %ld"), m_pOrder->m_lOrderNum );
	CLogNet::LogInfo( str, _T("CLegacyDeliveryDlg::OnBnClickedNewitem") );
	CNewItemDlg *pNewItemDlg = new CNewItemDlg();
	if ( pNewItemDlg )
	{
		lResult = pNewItemDlg->DoModal();
		pItem = pNewItemDlg->GetItem();
		delete ( pNewItemDlg );
	}

	if ( pItem && m_pOrder )
	{
		if ( pItem->m_lItemNum > 0 )
		{
			m_bModified = TRUE;
			EnableControls( TRUE );
			m_pOrder->m_ItemList.m_Items.Add( pItem );
			CProcess::CalculateTotal( m_pOrder );
			RefreshScreen();
		}
	}
	m_bAllowTimer = TRUE;    

}

void CLegacyDeliveryDlg::RefreshScreen()
{
	long lIndex, lCount;
	CItem *pItem;
	CString str;

	CLogNet::LogInfo( _T("Refreshing Screen"), _T("CLegacyDeliveryDlg::RefreshScreen") );
	CListBox *pListBox = (CListBox*) GetDlgItem(IDC_ORDERLIST);
	if ( pListBox )
	{
		pListBox->ResetContent();
		if ( m_pOrder )
		{
			lCount = m_pOrder->m_ItemList.m_Items.GetSize();
			for (lIndex=0; lIndex<lCount; lIndex++ )
			{
				pItem = (CItem *)m_pOrder->m_ItemList.m_Items.GetAt( lIndex );
				if ( pItem )
				{
					pItem->MakeListString( str, CItem::DETAILLIST );
					pListBox->AddString( str );
					pListBox->SetItemData( lIndex, (DWORD_PTR)pItem );
				}

			}
		}

		m_OrderNum.Format( _T("%ld"), m_pOrder->m_lOrderNum );
		m_BeforeTax.Format( _T("$%5.2f"), m_pOrder->m_dblBeforeTax );
		m_Tax.Format( _T("$%5.2f"), m_pOrder->m_dblTax );
		m_Total.Format( _T("$%5.2f"), m_pOrder->m_dblTotal );;
		UpdateData( false );
	}
}

void CLegacyDeliveryDlg::OnLbnSelchangeOrderlist()
{
	// TODO: Add your control notification handler code here
}

void CLegacyDeliveryDlg::OnBnClickedOk()
{
	// TODO: Add your control notification handler code here
		m_bAllowTimer = FALSE;
    if ( m_bModified )
   {
	   int nResult = AfxMessageBox(_T("Do you want to save Order?"), MB_YESNO|MB_ICONQUESTION);

	   if(nResult == IDYES)
	   {
	      CLogNet::LogInfo( _T("Do you want to save Order? (YES)"), _T("CLegacyDeliveryDlg::OnBnClickedOk") );
		  OnBnClickedSave();
	   }
	   else
		  CLogNet::LogInfo( _T("Do you want to save Order? (NO)"), _T("CLegacyDeliveryDlg::OnBnClickedOk") );
   }
	OnOK();
}

void CLegacyDeliveryDlg::OnBnClickedSeedlist()
{
	// TODO: Add your control notification handler code here
	m_bAllowTimer = FALSE;
	int nResult = AfxMessageBox(_T("Do you wish to send local ItemList to Server?"), MB_YESNO|MB_ICONQUESTION);

	if(nResult == IDYES)
	{
		CLogNet::LogInfo( _T("Send Local XML to Server"), _T("CLegacyDeliveryDlg::OnBnClickedSendList") );
		CProcess::SeedItemList();
		CLogNet::LogInfo( _T("Local XML sent to Server"), _T("CLegacyDeliveryDlg::OnBnClickedSendList") );
		AfxMessageBox( _T("Local XML sent to Server") );
	}   
	m_bAllowTimer = TRUE;
}


void CLegacyDeliveryDlg::OnBnClickedSendmsg()
{
	// TODO: Add your control notification handler code here
	m_bAllowTimer = FALSE;
     long lResult = 0;
	 CString strMsg;
    CString strToName;
	CString strFromName;
	CString strMessage;
	CString strTime;
;
	CLogNet::LogInfo( _T("Showing SendMessage Dialog"), _T("CLegacyDeliveryDlg::OnBnClickedSendMsg") );
	CSendMessageDlg *pMsgDlg = new CSendMessageDlg();
	if ( pMsgDlg )
	{
		pMsgDlg->m_Location = CProcess::GetLocation();
		pMsgDlg->m_DateTime = CProcess::GetCurrentTimeMessage();
		lResult = pMsgDlg->DoModal();
		strToName = pMsgDlg->m_ToName;
		strFromName = pMsgDlg->m_FromName;
		strMessage = pMsgDlg->m_Message;
		strTime = pMsgDlg->m_DateTime;
		delete ( pMsgDlg );
	}

	if ( lResult == IDOK )
	{
		strMsg.Format( _T("ToName=%s"), strToName );
		CLogNet::LogInfo( strMsg, _T("CLegacyDeliveryDlg::OnBnClickedSendMsg") );
		strMsg.Format( _T("FromName=%s"), strFromName );
		CLogNet::LogInfo( strMsg, _T("CLegacyDeliveryDlg::OnBnClickedSendMsg") );
		strMsg.Format( _T("Message=[%s]"), strMessage );
		CLogNet::LogInfo( strMsg, _T("CLegacyDeliveryDlg::OnBnClickedSendMsg") );
		strMsg.Format( _T("Time=[%s]"), strTime );
		CLogNet::LogInfo( strMsg, _T("CLegacyDeliveryDlg::OnBnClickedSendMsg") );

		CProcess::SaveSendMessage( strToName, strFromName, strMessage, strTime );
	}
	m_bAllowTimer = TRUE;
}



void CLegacyDeliveryDlg::OnTimer(UINT nIDEvent)
{
  BOOL bInsideTimer = m_bInsideTimer;
  CString strTime, strLocation, str;
  long lResult;

  try
  {
      if ( !m_bInsideTimer && m_bAllowTimer )
      {
          m_bInsideTimer = TRUE;

 		  if ( !CProcess::GetIniString( _T("CLIENT"), _T("LASTTIMEMESSAGE"), strTime ) )
		  {
			  strTime = CProcess::GetCurrentTimeMessage();
		      ::WritePrivateProfileString(_T("CLIENT"), _T("LASTTIMEMESSAGE"), strTime, _T("..\\data\\Legacy.ini") );
		  }

          
	      str.Format( _T("Check for Message with LASTTIMEMESSAGE=%s"), strTime );
		  CLogNet::LogInfo( str, _T("CLegacyDeliveryDlg::OnTimer") );
          strLocation = CProcess::GetLocation();
          if ( CProcess::HasMessage( strTime, strLocation ) )
          {
                   CProcess::GetMessageTime(strTime);
				    ::WritePrivateProfileString(_T("CLIENT"), _T("LASTTIMEMESSAGE"), strTime, _T("..\\data\\Legacy.ini") );

                   CSendMessageDlg *pMsgDlg = new CSendMessageDlg();
	               if ( pMsgDlg )
	               {
                        pMsgDlg->m_bReadOnly = TRUE;
		                CProcess::GetMessageLocation(pMsgDlg->m_Location);
		                pMsgDlg->m_DateTime = strTime;
                        CProcess::GetMessageToName(pMsgDlg->m_ToName);
						CProcess::GetMessageFromName(pMsgDlg->m_FromName);
                        CProcess::GetMessageText(pMsgDlg->m_Message);   

					    CLogNet::LogInfo( str, _T("CLegacyDeliveryDlg::OnTimer") );
					    str.Format( _T("Displaying Message for ToName=%s and FromName=%s from Location=%s"), pMsgDlg->m_ToName, pMsgDlg->m_FromName, pMsgDlg->m_Location );
					    CLogNet::LogInfo( str, _T("CLegacyDeliveryDlg::OnTimer") );
		                lResult = pMsgDlg->DoModal();
		                delete ( pMsgDlg );
	               }
            
          }
      }
  }
	catch (CException *e)
	{
		e->Delete();
		CLogNet::LogError( _T("Exception in timer message"), _T("CLegacyDeliveryDlg::OnTimer"));
	}
	catch(...)
	{
		CLogNet::LogError( _T("Unhandle exception in timer message"), _T("CLegacyDeliveryDlg::OnTimer"));
	}

  CDialog::OnTimer(nIDEvent);
  m_bInsideTimer = bInsideTimer;  
}
