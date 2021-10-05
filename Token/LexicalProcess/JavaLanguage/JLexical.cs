using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Token.LexicalProcess;
using System.Windows.Forms;
namespace Token.LexicalProcess.JavaLanguage
{
    public class JLexical : Lexical
    {
        //Attribut
        private String strCode;

        //Constructor berparameter
        public JLexical(String _strCode)
        {
            strCode = _strCode;
        }

        //Setter dan Getter
        public String StrCode
        {
            get { return strCode; }
            set { strCode = value; }
        }

        //override method dari interface Lexical++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetStrCode(String _strcode)
        {
            this.strCode = _strcode;
        }

        //override method dari interface Lexical++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Method untuk menghapus yang berhubungan dengan lexical
        public String Remove()
        {
            String hasil = "";
            hasil = this.RemoveSpace(this.strCode);
            hasil = this.RemoveTab(hasil);
            hasil = this.RemoveComment(hasil);
            //hasil = this.RemoveComment2(hasil);
            
            return hasil;
        }

        //override method dari interface Token++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //Method untuk mencek apakah token ada pada string
        public bool IsExist()
        {
            if ((this.strCode.IndexOf("  ") >= 0) || (this.strCode.IndexOf("//") >= 0) || (this.strCode.IndexOf("/*") >= 0) ||
                (this.strCode.IndexOf("*/") >= 0) || (this.strCode.IndexOf("\t") >= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Method untuk menghapus white space yang ada pada source code======================================================================
        private string RemoveSpace(string _strCode)
        {
            bool exist = true;
            exist = true;
            do
            {
                _strCode = _strCode.Replace("  ", " ");
                if (_strCode.IndexOf("  ") >= 0)
                {
                    exist = true;
                }
                else
                {
                    exist = false;
                }
            }
            while (exist == true);
            return _strCode;
        }

        //Method untuk menghapus komentar yang merupakan lanjutan dari komentar sebelumnya======================================================================
        private string RemoveCommentCont(string _strCode)
        {

            return "";
        }

        //Method untuk menghapus tab yang ada pada source code======================================================================
        private string RemoveTab(string _strCode)
        {
            bool exist = true;
            exist = true;
            do
            {
                _strCode = _strCode.Replace("\t", "");
                if (_strCode.IndexOf("\t") >= 0)
                {
                    exist = true;
                }
                else
                {
                    exist = false;
                }
            }
            while (exist == true);
            return _strCode;
        }

        //Method untuk menghapus komentar yang ada pada source code======================================================================
        private string RemoveComment(string _strCode)
        {
            int pointer1 = _strCode.IndexOf("//");
            int pointer2 = _strCode.Substring(0,(pointer1+1)).LastIndexOf("\"");
            int pointer3 = _strCode.IndexOf("\"",pointer2+1);
            string rep = "";
            //MessageBox.Show(pointer1 + ":" + pointer2 + ":" + pointer3+":"+_strCode);
            if (((pointer2 < pointer1) && (pointer3 > pointer1)&&(pointer2>=0))||(pointer1<0))
            {
                
            }
            else if(pointer1>=0){
                //MessageBox.Show(pointer1 + ":" + pointer2 + ":" + pointer3 + ":" + _strCode);
                rep = _strCode.Substring(pointer1);
                //MessageBox.Show(rep);
                _strCode = _strCode.Replace(rep, "");
            }
            return _strCode;
        }

        

    }
}
