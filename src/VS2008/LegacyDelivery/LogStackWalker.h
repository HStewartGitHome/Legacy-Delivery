#include "stackwalker.h"

class CLogStackWalker : public StackWalker
{
   public:
      CLogStackWalker();
      virtual ~CLogStackWalker();


   protected:
      virtual void OnOutput(LPCSTR szText);

      void          CharStringToCString( char    *lpChar, 
                                         long     lMaxSize,
                                         CString &str );
};

