
// LegacyDeliveryDlg.h : header file
//

#pragma once
#include "afxwin.h"
#include "order.h"


// CLegacyDeliveryDlg dialog
class CLegacyDeliveryDlg : public CDialog
{
// Construction
public:
	CLegacyDeliveryDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_LEGACYDELIVERY_DIALOG,
          TID_LEGACY   =  200 };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;
   COrder *m_pOrder;
   CFont   m_FontBig;
   BOOL    m_bModified;
   BOOL    m_bInsideTimer;
   BOOL    m_bAllowTimer;

	
	void RefreshScreen();
	void CreateNewOrder();

	void EnableControls( BOOL bEnabled );
	void AddCustomerInfo();

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnTimer(UINT nIDEvent);
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedNew();
	afx_msg void OnBnClickedLoad();
	afx_msg void OnBnClickedSave();
	CListBox m_OrderList;
	afx_msg void OnBnClickedTest();
	afx_msg void OnBnClickedNewitem();
	CString m_OrderNum;
	CString m_BeforeTax;
	CString m_Tax;
	CString m_Total;
	afx_msg void OnLbnSelchangeOrderlist();
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedSeedlist();
	afx_msg void OnBnClickedNewitem2();
	afx_msg void OnBnClickedSendmsg();
};
