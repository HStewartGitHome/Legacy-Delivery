#pragma once


// CSaveDlg dialog

class COrder;

class CSaveDlg : public CDialog
{
	DECLARE_DYNAMIC(CSaveDlg)

public:

	CSaveDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CSaveDlg();

	void SetOrder( COrder *pOrder );

// Dialog Data
	enum { IDD = IDD_SAVE };

   COrder *m_pOrder;

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support


	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedSaverouter();
	afx_msg void OnBnClickedSavelocal();
};
