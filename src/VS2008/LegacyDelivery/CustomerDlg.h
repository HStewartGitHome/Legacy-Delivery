#pragma once


// CCustomerDlg dialog

class COrder;

class CCustomerDlg : public CDialog
{
	DECLARE_DYNAMIC(CCustomerDlg)

public:
	CCustomerDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CCustomerDlg();

	
    void GetCustomerDetails( COrder *pOrder );

// Dialog Data
	enum { IDD = IDD_CUSTOMER };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	CString m_Name;
	CString m_Address;
	CString m_City;
	CString m_State;
	CString m_Zip;
};
