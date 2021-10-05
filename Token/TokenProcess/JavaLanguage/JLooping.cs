using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Token.TokenProcess.JavaLanguage
{
    class JLooping:Tokenization
    {
        //Attribute
        private String strCode;
        private List<TokenUnit> hasilToken;
    

        //Constructor berparameter
        public JLooping(String _strCode)
        {
            strCode = _strCode;
            hasilToken = new List<TokenUnit>();
        }

        //override method dari interface Token++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Method untuk mencek apakah keword yang akan diubah menjadi token
        //ada pada source code
        public bool IsExist()
        {
            if ((this.getIndexString(this.strCode, "while") >= 0) || (this.getIndexString(this.strCode, "do") >= 0)
                || (this.getIndexString(this.strCode, "for") >= 0))
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
            this.strCode = strCode.Replace("{", " { ");
            this.strCode = strCode.Replace("}", " } ");
            this.strCode = this.TokenLooping(strCode,"while");
            this.strCode = this.TokenLooping(strCode,"for");
            this.strCode = this.TokenLooping(strCode, "do");
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

        //Method untuk mengubah keyword while dan for yang ada menjadi token======================================================================
        public String TokenLooping(String _strCode,String _strLoop)
        {
            bool exist = true;
            int pointer1 = 0;
            int pointer2 = 0;
            int pointer3 = 0;
            string rep = "";

            do
            {
                //mencek apakah ada whitespace pada karakterk pertama, whitespcae akan dihapus jika ada
                if (_strCode.IndexOf(" ") != 0)
                {
                    _strCode = " " + _strCode;
                }
                _strCode = _strCode.Replace("("," ( ");
                _strCode = _strCode.Replace("  "," ");
                pointer1 = _strCode.IndexOf(" "+_strLoop+" ");
                pointer2 = _strCode.IndexOf("(", pointer1 + 2);
                if (pointer1 == -1)
                {
                    pointer1 = _strCode.IndexOf(" "+_strLoop);
                    if (_strLoop.Equals("for") && (pointer2 == pointer1 + 3 || pointer2 == pointer1 + 4))
                    {
                        pointer1 = _strCode.IndexOf(" " + _strLoop);
                    }
                    else {
                        pointer1 = -1;
                    }
                    if (_strLoop.Equals("while") && (pointer2 == pointer1 + 5 || pointer2 == pointer1 + 6))
                    {
                        pointer1 = _strCode.IndexOf(" " + _strLoop);
                    }
                    else
                    {
                        pointer1 = -1;
                    }
                }
                pointer3 = _strCode.IndexOf("{", pointer2 + 1);
                if (pointer3 >= 0)
                {
                    if (pointer1 >= 0)
                    {
                        if (pointer1 < pointer3)
                        {
                            exist = true;
                            rep = _strCode.Substring(pointer1, pointer3 - pointer1 + 1);
                            _strCode = _strCode.Replace(rep, " ");

                            //Menambahkan token ke dalam variable hasilToken

                            hasilToken.Add(new TokenUnit(rep, "LOOP"));
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
                else {
                    
                    pointer3 = _strCode.IndexOf(";");
                    if (pointer1 >= 0 && pointer2 > 6 && pointer2<pointer3)
                    {
                        string repl = _strCode.Substring(pointer1, pointer3 - pointer1 + 1);
                        hasilToken.Add(new TokenUnit(repl, repl));
                        _strCode=_strCode.Remove(pointer1, pointer3 - pointer1 + 1);

                    }
                    exist = false;
                }
            
            }
            while (exist == true);
            return _strCode;
        }

        
    }
}
