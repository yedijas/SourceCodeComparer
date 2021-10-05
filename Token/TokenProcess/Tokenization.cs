using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Token.TokenProcess
{
    public interface Tokenization
    {
        List<TokenUnit> GetToken();
        String GetStrCode();
        void SetStrCode(String _strcode);
        bool IsExist();
       
    }
}
