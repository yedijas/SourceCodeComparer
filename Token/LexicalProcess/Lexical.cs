using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Token.LexicalProcess
{
    public interface Lexical
    {
        String Remove();
        void SetStrCode(String _strcode);
        bool IsExist();
    }
}
