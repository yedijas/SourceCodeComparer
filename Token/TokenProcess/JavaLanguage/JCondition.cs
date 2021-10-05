using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Token.TokenProcess.JavaLanguage
{
    class JCondition:Tokenization
    {
        //Attribute
        private String strCode;
        private List<TokenUnit> hasilToken;
        

        //Constructor berparameter
        public JCondition(String _strCode)
        {
            strCode = _strCode;
            hasilToken = new List<TokenUnit>();
        }

        //override method dari interface Token++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Method untuk mencek apakah keword yang akan diubah menjadi token
        //ada pada source code
        public bool IsExist()
        {
            if ((this.getIndexString(this.strCode, "if") >= 0) || (this.getIndexString(this.strCode, "switch")>=0)
                || (this.getIndexString(this.strCode, "case") >= 0) || (this.getIndexString(this.strCode, "else") >= 0)
                || (this.getIndexString(this.strCode, "default") >= 0))
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
            //MessageBox.Show(strCode);
            this.strCode = this.TokenIf(strCode, "else if");
            this.strCode = this.TokenIf(strCode,"if");
            this.strCode = this.TokenElse(strCode);
            this.strCode = this.TokenCase(strCode);
            this.strCode = this.TokenDefault(strCode);
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

    
        
        //Method untuk mengubah keyword if yang ada menjadi token======================================================================
        public String TokenIf(String _strCode,String _str)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            int pointer3 = 0;
            string rep = "";
            do
            {
                //mencek apakah ada whitespace pada karakter pertama, whitespcae akan ditambah jika tidak ada
                if (_strCode.IndexOf(" ") != 0)
                {
                    _strCode = " " + _strCode;
                }
                
                pointer1 = _strCode.IndexOf(" "+_str+" ");
                
                pointer2 = _strCode.IndexOf("(", pointer1 + 2);
                if (pointer1 == -1)
                {
                    pointer1 = _strCode.IndexOf(" "+_str);
                    if (_str.Equals("if"))
                    {
                        //MessageBox.Show(_strCode);
                        if ((pointer2 != pointer1 + 3) && (pointer2 != pointer1 + 4))
                        {

                            pointer1 = -1;
                        }
                    }
                    else if (_str.Equals("else if"))
                    {
                        
                        if ((pointer2 != pointer1 + 7) && (pointer2 != pointer1 + 8))
                        {

                            pointer1 = -1;
                        }
                    }
                }
                
                pointer3 = _strCode.IndexOf("{", pointer2 + 1);
                if (pointer3 >= 0)
                {
                    string subStr = _strCode.Substring(0, pointer3);
                }

                if (pointer1 >= 0)
                {
                    if (pointer1 < pointer3)
                    {
                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer3 - pointer1+1);
                        _strCode = _strCode.Replace(rep, " ");

                        hasilToken.Add(new TokenUnit(rep, "CONDITION"));
                    }
                    else
                    {
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

        //Method untuk mengubah keyword case yang ada menjadi token======================================================================
        public String TokenCase(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";
            if (_strCode.IndexOf("case ") >= 0)
            {
                do
                {
                    //mencek apakah ada whitespace pada karakter pertama, whitespcae akan ditambah jika tidak ada
                    if (_strCode.IndexOf(" ") != 0)
                    {
                        _strCode = " " + _strCode;
                    }

                    pointer1 = _strCode.IndexOf(" case ");
                    pointer2 = _strCode.IndexOf(":", pointer1 + 2);
                    if ((pointer1 >= 0) && (pointer1 < pointer2))
                    {

                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");

                        //Menambahkan token ke dalam variable hasilToken
                        hasilToken.Add(new TokenUnit(rep, "CONDITION"));
                    }
                    else
                    {
                        exist = false;
                    }
            
                }
                while (exist == true);
            }
            return _strCode;
        }

        //Method untuk mengubah keyword else yang ada menjadi token======================================================================
        public String TokenElse(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";
            if (_strCode.IndexOf("else") >= 0)
            {
                do
                {

                    _strCode = _strCode.Replace("  ", " ");
                    pointer1 = getIndexString(_strCode, "else{");
                    if (pointer1 < 0)
                    {
                        pointer1 = getIndexString(_strCode, "else {");
                    }
                    pointer2 = _strCode.IndexOf("{", pointer1 + 2);
                    if ((pointer1 >= 0) && (pointer1 < pointer2))
                    {
                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");
                        //Menambahkan token ke dalam variable hasilToken

                        hasilToken.Add(new TokenUnit(rep, "CONDITION"));
                    }
                    else
                    {
                        exist = false;
                    }
            
                }
                while (exist == true);
            }
            return _strCode;
        }

        //Method untuk mengubah keyword default yang ada menjadi token======================================================================
        public String TokenDefault(String _strCode)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            string rep = "";
            if (_strCode.IndexOf("default") >= 0)
            {
                do
                {

                    _strCode = _strCode.Replace("  "," ");
                    pointer1 = getIndexString(_strCode, "default:") ;
                    if (pointer1 < 0)
                    {
                        pointer1 = getIndexString(_strCode, "default :");
                    }
                    pointer2 = _strCode.IndexOf(":", pointer1 + 2);
                    if ((pointer1 >= 0) && (pointer1 < pointer2))
                    {

                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer2 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");

                        //Menambahkan token ke dalam variable hasilToken

                        hasilToken.Add(new TokenUnit(rep, "CONDITION"));
                    }
                    else
                    {
                        exist = false;
                    }
                }
                while (exist == true);
            }
            return _strCode;
        }

        //Method untuk mengetahui letak(index) dari suatu string x di string _str
        private int getIndexString(String _str, String x)
        {
            int hasil = -1;
            int pointer1 = -1;
            int pointer2 = -1;
            int pointer3 = -1;
            bool exist = false;
            int len = _str.Length;
            int i = 1;
            do
            {
                pointer1 = _str.IndexOf(x);
                pointer2 = _str.Substring(0, (pointer1 + 1)).LastIndexOf("\"");
                pointer3 = _str.IndexOf("\"", pointer2 + 1);
                if (((pointer2 < pointer1) && (pointer3 > pointer1) && ((pointer2 >= 0) || (pointer1 < 0))) && (pointer3 > pointer2))
                {

                    hasil = -1;
                }
                else if (pointer1 >= 0)
                {
                    hasil = pointer1;
                }
                else
                {
                    hasil = -1;
                }
                String _str2 = _str.Substring(pointer1 + 1);
                if (_str2.IndexOf(x) >= 0)
                {
                    exist = true;
                    _str = _str2;
                }
                else
                {
                    exist = false;
                }
                i++;
            } while (exist == true && i <= len);
            return hasil;
        }

    }
}
