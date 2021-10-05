using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Token.TokenProcess;
using System.Windows.Forms;
using Token;

namespace Token.TokenProcess.JavaLanguage
{
    class JClass:Tokenization
    {
        //Attribut
        private String strCode;
        private List<TokenUnit> hasilToken;
      

        //Constructor berparameter
        public JClass(String _strCode)
        {
            this.strCode = _strCode;
            hasilToken = new List<TokenUnit>();
        }

        //override method dari interface Token++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Method untuk mencek apakah keword yang akan diubah menjadi token
        //ada pada source code
        public bool IsExist()
        {
            if ((this.strCode.IndexOf("class ") >= 0) || (this.strCode.IndexOf("interface ") >= 0))
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
            this.strCode = this.TokenClass(strCode);
            this.strCode = this.TokenInterface(strCode);
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


        //Method untuk mengubah keyword class yang ada menjadi token======================================================================
        public String TokenClass(String _strCode)
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
                pointer1 = _strCode.IndexOf(" class ");
                pointer2 = _strCode.IndexOf(" ",pointer1+2);
                pointer3 = _strCode.IndexOf("{", pointer2+1);
                
                if (pointer1 >= 0)
                {
                    if (pointer1 < pointer3)
                    {
                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer3 - pointer1+1);
                        _strCode = _strCode.Replace(rep, " ");
                        //Menambahkan token ke dalam variable hasilToken

                        hasilToken.Add(new TokenUnit(rep, "CLASS"));
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

        //Method untuk mengubah keyword interface yang ada menjadi token======================================================================
        public String TokenInterface(String _strCode)
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
                pointer1 = _strCode.IndexOf(" interface ");
                pointer2 = _strCode.IndexOf(" ", pointer1 + 2);
                pointer3 = _strCode.IndexOf("{", pointer2 + 1);
                if (pointer1 >= 0)
                {

                    if (pointer1 < pointer3){
                        exist = true;
                        rep = _strCode.Substring(pointer1, pointer3 - pointer1 + 1);
                        _strCode = _strCode.Replace(rep, " ");
                    }else{
                        exist = false;
                    }
                    //Menambahkan token ke dalam variable hasilToken
                    hasilToken.Add(new TokenUnit(rep, "INTERFACE"));
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
