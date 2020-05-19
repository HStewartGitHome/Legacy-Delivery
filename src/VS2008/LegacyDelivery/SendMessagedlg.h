#pragma once


// CSendMessage dialog

class CSendMessageDlg : public CDialog
{
	DECLARE_DYNAMIC(CSendMessageDlg)

public:
	CSendMessageDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CSendMessageDlg();

// Dialog Data
	enum { IDD = IDD_SENDMESSAGE };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedSendmessage();
	CString m_ToName;
	CString m_Message;
	CString m_Location;
	CString m_DateTime;
   BOOL    m_bReadOnly; 
   CString m_FromName;
};
