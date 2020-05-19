#pragma once
#include "afxwin.h"

class CItem;
class CItemList;

// CNewItemDlg dialog

class CNewItemDlg : public CDialog
{
	DECLARE_DYNAMIC(CNewItemDlg)

public:
	CNewItemDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~CNewItemDlg();

   CItem *GetItem();
   void   SetControls();

// Dialog Data
	enum { IDD = IDD_NEWITEM };

	CItem     *m_pItem;
	CItemList *m_pItemList;
	CFont   m_FontCombo;

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	CString  m_ItemCombo;
	CString  m_QtyCombo;
};
