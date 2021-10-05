using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Token
{
    public class TokenUnit
    {
        public String TokenShape { get; set; }
        public String SourceCode { get; set; }

        public TokenUnit(String _source, String _token)
        {
            TokenShape = _token;
            SourceCode = _source;
        }

        public TokenUnit()
        {
            TokenShape = "";
            SourceCode = "";
        }
    }
}
