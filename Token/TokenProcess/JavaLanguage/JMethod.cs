using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Token.TokenProcess.JavaLanguage
{
    class JMethod:Tokenization
    {
        //Attribute
        private String strCode;
        private List<TokenUnit> hasilToken;
  

        //Constructor berparameter
        public JMethod(String _strCode)
        {
            strCode = _strCode;
            hasilToken = new List<TokenUnit>();
        }

        //override method dari interface Token++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Method untuk mencek apakah keword yang akan diubah menjadi token
        //ada pada source code
        public bool IsExist()
        {
            if (this.getIndexString(this.strCode, "(")>=0||this.getIndexString(this.strCode, ")")>=0)
            {
                int pointer1 = this.getIndexString(this.strCode, "(");
                int pointer2 = this.getIndexString(this.strCode, ")");
                int pointer3 = this.getIndexString(this.strCode, "{");
                int pointer4 = this.getIndexString(this.strCode, "=");
                if (((pointer1 < pointer2) && (pointer1 < pointer3)) && (pointer2 < pointer3)&&(pointer4 < 0))
                //if(pointer1>=0)
                {
                    
                    return true;
                }
                else {
                    return false;
                }
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
            this.strCode = this.TokenMethod(strCode);
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

        //Method untuk mengubah keyword while yang ada menjadi token======================================================================
        public String TokenMethod(String _strCode)
        {
            int pointer1 = -1;
            int pointer2 = -1;
            int pointer3 = -1;
            string rep = "";
            string retType="";
            try
            {
                    _strCode = _strCode.TrimStart(' ');
                    pointer1 = _strCode.IndexOf(" ");
                    pointer3 = _strCode.IndexOf("(");
                    if (pointer1 >= 0 && pointer1<pointer3)
                    {
                        rep = _strCode.Substring(0, pointer1);
                        retType = rep;
                        _strCode = _strCode.Substring(pointer1 + 1);
                        
                        pointer3 = _strCode.IndexOf("(");
                        pointer2 = _strCode.IndexOf("{", pointer3 + 1);
                        if (pointer3 >= 0)
                        {
                            if (pointer3 < pointer2)
                            {
                                string name = _strCode;
                                _strCode = _strCode.Substring(pointer2+1);
                                
                                hasilToken.Add(new TokenUnit(rep+" "+name, "METHOD_" + retType));
                            }
                        }
                    }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return _strCode;
        }
    }
}
