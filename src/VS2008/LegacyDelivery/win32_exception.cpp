#include "stdafx.h"
#include "win32_exception.h"
#include "eh.h"
#include "LogNet.h"

void win32_exception::install_handler()
{
    _set_se_translator(win32_exception::translate);
}

void win32_exception::translate(unsigned code, EXCEPTION_POINTERS* info)
{
    CString strWhat, strMsg;

    // Windows guarantees that *(info->ExceptionRecord) is valid
    switch (code) 
    {
      case EXCEPTION_ACCESS_VIOLATION:
        strWhat.Format(_T("%ld: Access violation"), code );
        break;
      case EXCEPTION_FLT_DIVIDE_BY_ZERO:
      case EXCEPTION_INT_DIVIDE_BY_ZERO:
        strWhat.Format(_T("%ld: Division by zero"), code );
        break;
      default:
        strWhat.Format(_T("%ld: win32 exception"), code );
        break;
    }
    strMsg.Format( _T("Exception occur in SE_handler: %s"), strWhat );
    CLogNet::LogExceptionStack( strMsg, info->ContextRecord );    

    AfxThrowUserException();
}

win32_exception::win32_exception(const EXCEPTION_RECORD& info)
: mWhat("Win32 exception"), mWhere(info.ExceptionAddress), mCode(info.ExceptionCode)
{
    switch (info.ExceptionCode) {
    case EXCEPTION_ACCESS_VIOLATION:
        mWhat = "Access violation";
        break;
    case EXCEPTION_FLT_DIVIDE_BY_ZERO:
    case EXCEPTION_INT_DIVIDE_BY_ZERO:
        mWhat = "Division by zero";
        break;
    }
}





