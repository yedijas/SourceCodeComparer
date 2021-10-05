using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Token.TokenProcess;

namespace Token.TokenProcess.JavaLanguage
{
    public class JVariable : Tokenization
    {
        //Attribut
        private String strCode;
        private List<TokenUnit> hasilToken;
 

        //Constructor berparameter
        public JVariable(String _strCode)
        {
            this.strCode = _strCode;
            hasilToken = new List<TokenUnit>();
        }

        //override method dari interface Token++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Method untuk mencek apakah keword yang akan diubah menjadi token
        //ada pada source code
        public bool IsExist()
        {
            if ((this.strCode.IndexOf("int ") >= 0) || (this.strCode.IndexOf("boolean ") >= 0) || (this.strCode.IndexOf("long ") >= 0) ||
                (this.strCode.IndexOf("byte ") >= 0) || (this.strCode.IndexOf("float ") >= 0) || (this.strCode.IndexOf("char ") >= 0) ||
                (this.strCode.IndexOf("short ") >= 0) || (this.strCode.IndexOf("double ") >= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //override method dari interface Token++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public List<TokenUnit> GetToken()
        {
            hasilToken.Clear();
            if ((strCode.IndexOf(";")) != (strCode.LastIndexOf(";")))
            {
                String[] arrStrCode = strCode.Split(';');
                foreach (string item in arrStrCode)
                {
                    this.strCode = this.TokenInt(item + ";");
                    this.strCode = this.TokenBoolean(strCode);
                    this.strCode = this.TokenLong(strCode);
                    this.strCode = this.TokenByte(strCode);
                    this.strCode = this.TokenFloat(strCode);
                    this.strCode = this.TokenChar(strCode);
                    this.strCode = this.TokenDouble(strCode);
                    this.strCode = this.TokenShort(strCode);
                  
                }
            }
            else
            {

                this.strCode = this.TokenInt(strCode);
                this.strCode = this.TokenBoolean(strCode);
                this.strCode = this.TokenLong(strCode);
                this.strCode = this.TokenByte(strCode);
                this.strCode = this.TokenFloat(strCode);
                this.strCode = this.TokenChar(strCode);
                this.strCode = this.TokenDouble(strCode);
                this.strCode = this.TokenShort(strCode);
            }
            return this.hasilToken;
        }


        //override method dari interface Token++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public String GetStrCode()
        {

            return this.strCode;
        }

        //override method dari interface Token++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetStrCode(String _strcode)
        {
            strCode = _strcode;
        }

     

        //check ada kurung

        private bool IsExistBracket(string _str,string _find)
        { 
            int pointer1=_str.IndexOf(_find);
            int pointer2=_str.IndexOf("(");
            int pointer3=_str.LastIndexOf(")");
            if ((pointer2 < pointer1) && (pointer2 < pointer3))
            {
                return true;
            }
            else {
                return false;
            }
            
        }

        //Method untuk mengubah keyword int yang ada menjadi token======================================================================
        public String TokenInt(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";
            if (_strCode.IndexOf(" ") != 0)
            {
                _strCode = " " + _strCode;
            }
            if (_strCode.IndexOf(" int ") >= 0 && IsExistBracket(_strCode, "int ") == false && _strCode.IndexOf(";") >= 0)
            {
                _strCode = _strCode.Replace(",", "; int ");
            }
            do
            {
                
                pointer1 = _strCode.IndexOf(" int ");

                if (pointer1 >= 0)
                {
                    
                    

                    pointer2 = _strCode.IndexOf(";");
                    if (pointer1 < pointer2)
                    {
                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                       
                        _strCode = _strCode.Replace(rep, " ");
                        //Menambahkan token ke dalam variable hasilToken

                        hasilToken.Add(new TokenUnit(rep, "INT"));
                    }
                    else {
                        exist = false;
                    }
                    
                }
                else
                {
                    exist = false;
                }

            }
            while (exist == true);
            return _strCode;
        }



        //Method untuk mengubah keyword boolean yang ada menjadi token======================================================================
        public String TokenBoolean(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";

            if (_strCode.IndexOf(" ") != 0)
            {
                _strCode = " " + _strCode;
            }
            if (_strCode.IndexOf(" boolean ") >= 0 && IsExistBracket(_strCode, "boolean ") == false && _strCode.IndexOf(";") >= 0)
            {
                _strCode = _strCode.Replace(",", "; boolean ");
            }
            do
            {
                //mencek apakah ada whitespace pada karakterk pertama, whitespcae akan dihapus jika ada
                pointer1 = _strCode.IndexOf(" boolean ");

                if (pointer1 >= 0)
                {

                    

                    pointer2 = _strCode.IndexOf(";");
                    if (pointer1 < pointer2)
                    {
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");
                        hasilToken.Add(new TokenUnit(rep, "BOOLEAN"));
                        exist = true;
                    }
                    else {
                        exist = false;
                    }

                    
                }
                else
                {
                    exist = false;
                }

            }
            while (exist == true);
            return _strCode;
        }

        //Method untuk mengubah keyword long yang ada menjadi token======================================================================
        public String TokenLong(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";

            if (_strCode.IndexOf(" ") != 0)
            {
                _strCode = " " + _strCode;
            }
            if (_strCode.IndexOf(" long ") >= 0 && IsExistBracket(_strCode, "long ") == false && _strCode.IndexOf(";") >= 0)
            {
                _strCode = _strCode.Replace(",", "; long ");
            }
            do
            {
                //mencek apakah ada whitespace pada karakterk pertama, whitespcae akan dihapus jika ada
                pointer1 = _strCode.IndexOf(" long ");

                if (pointer1 >= 0)
                {

                    

                    pointer2 = _strCode.IndexOf(";");
                    if (pointer1 < pointer2)
                    {
                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");

                        //Menambahkan token ke dalam variable hasilToken
                        hasilToken.Add(new TokenUnit(rep, "LONG"));
                    }
                    else {
                        exist = false;
                    }

                    
                }
                else
                {
                    exist = false;
                }

              
            }
            while (exist == true);
            return _strCode;
        }

        //Method untuk mengubah keyword byte yang ada menjadi token======================================================================
        public String TokenByte(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";

            if (_strCode.IndexOf(" ") != 0)
            {
                _strCode = " " + _strCode;
            }
            if (_strCode.IndexOf(" byte ") >= 0 && IsExistBracket(_strCode, "byte ") == false && _strCode.IndexOf(";") >= 0)
            {
                _strCode = _strCode.Replace(",", "; byte ");
            }
            do
            {
                //mencek apakah ada whitespace pada karakterk pertama, whitespcae akan dihapus jika ada
                pointer1 = _strCode.IndexOf(" byte ");

                if (pointer1 >= 0)
                {

                    exist = true;

                    pointer2 = _strCode.IndexOf(";");
                    if (pointer1 < pointer2)
                    {
                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");

                        //Menambahkan token ke dalam variable hasilToken
                        hasilToken.Add(new TokenUnit(rep, "BYTE"));
                    }
                    else {
                        exist = false;
                    }

                    
                }
                else
                {
                    exist = false;
                }

            }
            while (exist == true);
            return _strCode;
        }

        //Method untuk mengubah keyword float yang ada menjadi token======================================================================
        public String TokenFloat(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";

            if (_strCode.IndexOf(" ") != 0)
            {
                _strCode = " " + _strCode;
            }
            if (_strCode.IndexOf(" float ") >= 0 && IsExistBracket(_strCode, "float ") == false && _strCode.IndexOf(";") >= 0)
            {
                _strCode = _strCode.Replace(",", "; float ");
            }
            do
            {
                //mencek apakah ada whitespace pada karakter pertama, whitespcae akan dihapus jika ada
                pointer1 = _strCode.IndexOf(" float ");

                if (pointer1 >= 0)
                {

                    exist = true;

                    pointer2 = _strCode.IndexOf(";");
                    if (pointer1 < pointer2)
                    {
                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");

                        //Menambahkan token ke dalam variable hasilToken
                        hasilToken.Add(new TokenUnit(rep, "FLOAT"));
                    }
                    else {
                        exist = false;
                    }

                    
                }
                else
                {
                    exist = false;
                }

            
            }
            while (exist == true);
            return _strCode;
        }

        //Method untuk mengubah keyword char yang ada menjadi token======================================================================
        public String TokenChar(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";

            if (_strCode.IndexOf(" ") != 0)
            {
                _strCode = " " + _strCode;
            }
            if (_strCode.IndexOf(" char ") >= 0 && IsExistBracket(_strCode, "char ") == false && _strCode.IndexOf(";") >= 0)
            {
                _strCode = _strCode.Replace(",", "; char ");
            }
            do
            {
                //mencek apakah ada whitespace pada karakterk pertama, whitespcae akan dihapus jika ada
                pointer1 = _strCode.IndexOf(" char ");

                if (pointer1 >= 0)
                {

                    exist = true;

                    pointer2 = _strCode.IndexOf(";");
                    if (pointer1 < pointer2)
                    {
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");
                        exist = true;

                        //Menambahkan token ke dalam variable hasilToken
                        hasilToken.Add(new TokenUnit(rep, "CHAR"));
                    }
                    else {
                        exist = false;
                    }

                    
                }
                else
                {
                    exist = false;
                }

            
            }
            while (exist == true);
            return _strCode;
        }

        //Method untuk mengubah keyword short yang ada menjadi token======================================================================
        public String TokenShort(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";

            if (_strCode.IndexOf(" ") != 0)
            {
                _strCode = " " + _strCode;
            }
            if (_strCode.IndexOf(" short ") >= 0 && IsExistBracket(_strCode, "short ") == false && _strCode.IndexOf(";") >= 0)
            {
                _strCode = _strCode.Replace(",", "; short ");
            }
            do
            {
                //mencek apakah ada whitespace pada karakter pertama, whitespcae akan dihapus jika ada
                pointer1 = _strCode.IndexOf(" short ");

                if (pointer1 >= 0)
                {

                    exist = true;

                    pointer2 = _strCode.IndexOf(";");
                    if (pointer1 < pointer2)
                    {
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");
                        exist = true;

                        //Menambahkan token ke dalam variable hasilToken
                        hasilToken.Add(new TokenUnit(rep, "SHORT"));
                    }
                    else {
                        exist = false;
                    }

                    
                }
                else
                {
                    exist = false;
                }

              
            }
            while (exist == true);
            return _strCode;
        }

        //Method untuk mengubah keyword double yang ada menjadi token======================================================================
        public String TokenDouble(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";

            if (_strCode.IndexOf(" ") != 0)
            {
                _strCode = " " + _strCode;
            }
            if (_strCode.IndexOf(" double ") >= 0 && IsExistBracket(_strCode, "double ") == false && _strCode.IndexOf(";") >= 0)
            {
                _strCode = _strCode.Replace(",", "; double ");
            }
            do
            {
                //mencek apakah ada whitespace pada karakterk pertama, whitespcae akan dihapus jika ada
                pointer1 = _strCode.IndexOf(" double ");

                if (pointer1 >= 0)
                {

                    exist = true;

                    pointer2 = _strCode.IndexOf(";");
                    if (pointer1 < pointer2)
                    {
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");
                        exist = true;

                        //Menambahkan token ke dalam variable hasilToken
                        hasilToken.Add(new TokenUnit(rep, "DOUBLE"));
                    }
                    else {
                        exist = false;
                    }

                    
                }
                else
                {
                    exist = false;
                }
               
            }
            while (exist == true);
            return _strCode;
        }

    }
}
