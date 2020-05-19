#include "windows.h"
#include <exception>


class win32_exception: public std::exception
{
public:
    typedef const void* Address; // OK on Win32 platform

    static void install_handler();
    virtual const char* what() const { return mWhat; };
    Address where() const { return mWhere; };
    unsigned code() const { return mCode; };
protected:
    win32_exception(const EXCEPTION_RECORD& info);
    static void translate(unsigned code, EXCEPTION_POINTERS* info);
private:
    const char* mWhat;
    Address mWhere;
    unsigned mCode;
};
