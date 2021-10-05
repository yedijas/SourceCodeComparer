using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Token.Rules
{
    public class TransRules
    {
        //Attribut
        private String strCode;

        

        //Constructor
        public TransRules(String _strCode)
        {
            this.strCode = _strCode;
        }

        //setter getter
        public String StrCode
        {
            get { return strCode; }
            set { strCode = value; }
        }

        public bool IsExist()
        {
            if ((this.getIndexString(this.strCode, "import") >= 0) || ((this.getIndexString(this.strCode, "=") >= 0)
                && (this.getIndexString(this.strCode, "{") >= 0) && (this.getIndexString(this.strCode, "}") >= 0)) || ((this.getIndexString(this.strCode, "protected") >= 0)||
                (this.getIndexString(this.strCode, "private") >= 0) || (this.getIndexString(this.strCode, "public") >= 0)) || (this.getIndexString(this.strCode, "static") >= 0) || (this.getIndexString(this.strCode, "if") >= 0) ||
                (this.getIndexString(this.strCode, "for") >= 0) || (this.getIndexString(this.strCode, "while") >= 0) || (this.getIndexString(this.strCode, "do") >= 0)
                || (this.getIndexString(this.strCode, "else") >= 0))
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public string doTransRules()
        {
            strCode = this.removePackage(strCode);
            strCode = this.removeInitializationListV1(strCode);
            strCode = this.removeInitializationListV2(strCode);
            strCode = this.RemoveAccessibility(strCode);
            strCode = this.ConvertToCompoundV1(strCode,"if");
            strCode = this.ConvertToCompoundV1(strCode, "for");
            strCode = this.ConvertToCompoundV1(strCode, "while");
            strCode = this.ConvertToCompoundV2(strCode, "do");
            return strCode;
        }
        //RJ1 = > Remove Package Name
        private String removePackage(string _strCode)
        {
            if (getIndexString(_strCode, "import") >= 0)
            {
                int pointer1 = getIndexString(_strCode, ";");
                int pointer2 = getIndexString(_strCode, "import");
                //MessageBox.Show(_strCode);
                _strCode = _strCode.Substring(0, pointer1 + 1);
                string pack = _strCode.Substring(pointer2 + 6).Trim();

                int pointer3 = pack.IndexOf(".");
                string firstChar = "";
                while ((pointer3 >= 0))
                {
                    firstChar = pack.Substring(pointer3 + 1, 1);

                    pack = pack.Substring(pointer3 + 1);
                    pointer3 = pack.IndexOf(".");
                    if (Check(firstChar))
                    {
                        _strCode = "import " + pack;
                        pointer3 = -1;
                    }

                }

            }
            return _strCode;
        }

        //RJ3 => remove Initialization List Versi1
        private string removeInitializationListV1(string _strCode)
        {
            int pointer1 = getIndexString(_strCode,"=");
            int pointer2 = getIndexString(_strCode, "{");
            int pointer3 = getIndexString(_strCode, "}");
            int pointer4 = getIndexString(_strCode, ";");
            if ((pointer1 >= 0) && (pointer2 >= 0) && (pointer3 >= 0) && (pointer4 >= 0))
            {
                if ((pointer1 < pointer2) && (pointer2 < pointer3) && (pointer1 < pointer3)&&(pointer3<pointer4)&&(pointer2<pointer4))
                {
                    
                    return _strCode = _strCode.Remove(pointer2 + 1, pointer3 - pointer2 - 1);
                }
                else {
                    return _strCode;
                }
            }
            else {
                return _strCode;
            }
            
        }

        //RJ3 => remove Initialization List Versi2
        private string removeInitializationListV2(string _strCode)
        {
            int pointer1 = getIndexString(_strCode, "]");
            int pointer2 = getIndexString(_strCode, "{");
            int pointer3 = getIndexString(_strCode, "}");
            int pointer4 = getIndexString(_strCode, ";");
            if ((pointer1 >= 0) && (pointer2 >= 0) && (pointer3 >= 0) && (pointer4 >= 0))
            {
                if ((pointer1 < pointer2) && (pointer2 < pointer3) && (pointer1 < pointer3) && (pointer3 < pointer4) && (pointer2 < pointer4))
                {
                    return _strCode = _strCode.Remove(pointer2 + 1, pointer3 - pointer2 - 1);
                }
                else
                {
                    return _strCode;
                }
            }
            else
            {
                return _strCode;
            }

        }

        //RJ5 => Remove accesibility 
        private string RemoveAccessibility(string _strCode)
        {
            int pointer1 = getIndexString(_strCode, "protected");
            int pointer2 = getIndexString(_strCode, "private");
            int pointer3 = getIndexString(_strCode, "public");
            
            string acces = "";
            if (pointer1 >= 0)
            {
                acces = "protected";
            }
            else if (pointer2 >= 0)
            {
                acces = "private";
            }
            else if (pointer3 >= 0)
            {
                acces = "public";
            }

            if (acces.Equals("") == false)
            {
                _strCode = _strCode.Replace(acces, "");
            }
            int pointer4 = getIndexString(_strCode, "static");
            acces = "";
            if (pointer4 >= 0)
            {
                acces = "static";
            }
            if (acces.Equals("")==false) {
                _strCode = _strCode.Replace(acces, "");
            }
            

            return _strCode;
        }

        //RJ6 => Convert to Compound Block versi 1
        private string ConvertToCompoundV1(string _strCode,string _ctr)
        {
            int pointer1 = getIndexString(_strCode, _ctr);
            int pointer2 = getIndexString(_strCode, "{");
            int pointer3 = getIndexString(_strCode, ";");
            
            if (pointer1 >= 0)
            {
                if ((pointer2 < 0 && pointer3>0)||(pointer2>pointer3))
                {
                    char[] arrStr = _strCode.ToCharArray();
                    int i = 0;
                    int index = -1;
                    int bracket = 0;
                    bool exist = false;
                    while (i < arrStr.Length)
                    {
                        
                        if (arrStr[i].Equals('('))
                        {
                            bracket++;
                            exist = true;
                        }
                        if (arrStr[i].Equals(')'))
                        {
                            bracket--;
                            exist = true;
                        }
                        if (bracket == 0 && exist==true)
                        {
                            index = i;
                            bracket = arrStr.Length;
                        }
                        i++;
                    }
                    string str = _strCode.Substring(0, index+1);
                    if (pointer1 - index > 2)
                    {
                        _strCode = str + "{" + _strCode.Substring(index + 1) + "}";
                        //MessageBox.Show(_strCode + ":" + str);
                    }
                }
            }
            return _strCode;
        }

        //RJ6 => Convert to Compound Block versi 2
        private string ConvertToCompoundV2(string _strCode, string _ctr)
        {
            int pointer1 = getIndexString(_strCode, _ctr);
            int pointer2 = getIndexString(_strCode, "{");
            int pointer3 = getIndexString(_strCode, ";");
            if (pointer1 >= 0)
            {
                if ((pointer2 < 0 && pointer3 > 0) || (pointer2 > pointer3))
                {
                    
                        _strCode = _ctr + "{" + _strCode.Substring(pointer1 + _ctr.Length + 1) + "}";
                }
            }
            return _strCode;
        }


        private bool Check(string _str)
        {
            if ((_str.Equals("A")) || (_str.Equals("B")) || (_str.Equals("C")) || (_str.Equals("D")) || (_str.Equals("E")) || (_str.Equals("F"))
                || (_str.Equals("G")) || (_str.Equals("H")) || (_str.Equals("I")) || (_str.Equals("J")) || (_str.Equals("K")) || (_str.Equals("L"))
                || (_str.Equals("M")) || (_str.Equals("N")) || (_str.Equals("O")) || (_str.Equals("P")) || (_str.Equals("Q")) || (_str.Equals("R"))
                || (_str.Equals("S")) || (_str.Equals("T")) || (_str.Equals("U")) || (_str.Equals("V")) || (_str.Equals("W")) || (_str.Equals("X"))
                || (_str.Equals("Y")) || (_str.Equals("Z")))
            {
                return true;
            }
            else {
                return false;
            }
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
