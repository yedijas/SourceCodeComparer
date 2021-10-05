using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Token.TokenProcess;
using Token.TokenProcess.JavaLanguage;
using Token.LexicalProcess;
using Token.LexicalProcess.JavaLanguage;
using Token.Rules;

namespace Token
{
    public class Controller
    {
        //Attribut
        List<TokenUnit> listToken1;
        List<TokenUnit> listToken2;
        List<Koordinat> letakSama;
        private Srcreen screenApp;
        private SourceFile sourceF;
        /// <summary>
        /// konstruktor tanpa parameter
        /// </summary>
        public Controller()
        {
            screenApp = new Srcreen(this);
            listToken1 = new List<TokenUnit>();
            listToken2 = new List<TokenUnit>();
            letakSama = new List<Koordinat>();
        }
        /// <summary>
        /// menampilkan GUI
        /// </summary>
        public void Start()
        {
            Application.Run(screenApp);
        }

        public List<TokenUnit> ListToken1
        {
            get { return listToken1; }
            set { listToken1 = value; }
        }


        public List<TokenUnit> ListToken2
        {
            get { return listToken2; }
            set { listToken2 = value; }
        }
        /// <summary>
        /// memproses source code
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_txtBox"></param>
        /// <returns></returns>
        public List<TokenUnit> ProsesFile(String _path)
        {
            List<TokenUnit> hasil = new List<TokenUnit>();
            sourceF = new SourceFile(_path);
            String strCodeLine = "";
            //Instansiasi class yang akan melakukan proses tokenisasi
            Lexical jLexical = new JLexical(strCodeLine);
            //Lexical jRemoveKeyword = new JRemoveKeyword(strCodeLine);
            Tokenization jVariable = new JVariable(strCodeLine);
            Tokenization jClass = new JClass(strCodeLine);
            
            Tokenization jCondition = new JCondition(strCodeLine);
            Tokenization jLooping = new JLooping(strCodeLine);
            Tokenization jMethod = new JMethod(strCodeLine);
            TransRules jRules = new TransRules(strCodeLine);

            #region proses tokenisasi
            int i = 0;
            string prev = "";
            bool exist = false;
            bool lanjutBaca = true;
            while (sourceF.IsLast() == false)
            {
                //Mengambil 1 baris dari sourcefile
                strCodeLine = prev;
                if (lanjutBaca==true)
                {
                    strCodeLine = strCodeLine+" "+sourceF.ReadOneLine().ToString();
                }
                
                prev = "";
                if ((this.existString(strCodeLine, "/*"))&&(exist==false)) {
                    exist = true;
                    //MessageBox.Show("/*"+strCodeLine);
                }
                else if ((this.existString(strCodeLine, "*/"))&&(exist == true))
                {
                    exist = false;
                    //MessageBox.Show("*/" + strCodeLine);
                }
                else if(exist==false)
                {
                    
                    int pointerKolon = -1;
                    int pointerKurawal = -1;
                    int pointerFor = -1;
                    if (this.existString(strCodeLine, "for"))
                    {
                        pointerFor = strCodeLine.IndexOf("for");
                        
                    }
                    else
                    {


                        if (this.existString(strCodeLine, ";"))
                        {
                            pointerKolon = strCodeLine.IndexOf(";");
                        }
                        else
                        {
                            pointerKolon = -1;
                        }
                    }

                    
                        if (this.existString(strCodeLine, "{"))
                        {
                            pointerKurawal = strCodeLine.IndexOf("{");
                        }
                        else
                        {
                            pointerKurawal = -1;
                        }
                    
                    //pemrosesan lexical

                    jLexical.SetStrCode(strCodeLine);
                    if (jLexical.IsExist())
                    {

                        strCodeLine = jLexical.Remove();
                    }

                    if ((pointerKurawal == -1) && (strCodeLine.IndexOf("}")>1))
                    {
                        
                        
                        pointerKurawal = strCodeLine.IndexOf("}");
                    }
                    if (((pointerKolon >= 0) || (pointerKurawal >= 0))||(this.existString(strCodeLine,"//")))
                    {
                        jRules.StrCode = strCodeLine;
                        if (jRules.IsExist())
                        {
                            strCodeLine = jRules.doTransRules();
                        }
                        jClass.SetStrCode(strCodeLine);
                        if (jClass.IsExist() == true)
                        {
                            jClass.SetStrCode(strCodeLine);
                            hasil.AddRange(jClass.GetToken());
                            strCodeLine = jClass.GetStrCode();
                        }
                        strCodeLine = this.removeBracket(strCodeLine);
                        
                        jCondition.SetStrCode(strCodeLine);
                        if (jCondition.IsExist() == true)
                        {
                            jCondition.SetStrCode(strCodeLine);
                            hasil.AddRange(jCondition.GetToken());
                            strCodeLine = jCondition.GetStrCode();
                        }
                        jLooping.SetStrCode(strCodeLine);
                        if (jLooping.IsExist() == true)
                        {
                            jLooping.SetStrCode(strCodeLine);
                            hasil.AddRange(jLooping.GetToken());
                            strCodeLine = jLooping.GetStrCode();
                        }
                        jMethod.SetStrCode(strCodeLine);
                        if (jMethod.IsExist() == true)
                        {

                            jMethod.SetStrCode(strCodeLine);
                            hasil.AddRange(jMethod.GetToken());
                            strCodeLine = jMethod.GetStrCode();
                        }
                        jVariable.SetStrCode(strCodeLine);
                        if (jVariable.IsExist() == true)
                        {
                            jVariable.SetStrCode(strCodeLine);
                            hasil.AddRange(jVariable.GetToken());
                            strCodeLine = jVariable.GetStrCode();
                        }
                        jLexical.SetStrCode(strCodeLine);
                        if (jLexical.IsExist())
                        {
                            
                            strCodeLine = jLexical.Remove();
                        }
                        if (existString(strCodeLine, "}"))
                        {
                            int pointer1 = strCodeLine.IndexOf("}");
                            strCodeLine = strCodeLine.Remove(pointer1, 1);
                        }
                        strCodeLine=strCodeLine.Replace("  ", " ");
                        if (strCodeLine.Equals("") || strCodeLine.Equals(" ") || strCodeLine.Equals("  "))
                        { }
                        else
                        {
                            strCodeLine=strCodeLine.TrimEnd();
                            strCodeLine=strCodeLine.TrimStart();
                            if (strCodeLine.Length > 2)
                            {
                                hasil.Add(new TokenUnit(strCodeLine, strCodeLine));
                            }
                        }
                    }
                    else
                    {

                        prev = prev+" " + strCodeLine;

                    }
                }
                i++;
            }
            sourceF.CloseFile();
            #endregion

            return hasil;

        }

        private string removeBracket(string _str)
        {
            int pointer=_str.IndexOf("}");
            
            if ((pointer == 1) || (pointer == 0))
            {
                _str = _str.Substring(pointer + 1);
            }
            return _str;
        }

        public String[] TokenUnitListToTokenArray(List<TokenUnit> toConvert)
        {
            List<String> temp = new List<String>();
            for (int i = 0; i < toConvert.Count; i++)
            {
                temp.Add(toConvert[i].TokenShape);
            }
            return temp.ToArray();
        }
        /// <summary>
        /// mengambil bagian yang dinyatakan sama dari proses perbandingan.
        /// </summary>
        /// <param name="toExtract">list token hasil tokenisasi</param>
        /// <param name="id">0 = list token pertama, 1 = list token ke dua</param>
        /// <returns>list token yang telah diekstrak</returns>
        public List<TokenUnit> ExtractSimilar(int id)
        {
            List<TokenUnit> temp = new List<TokenUnit>();
            for (int i = 0; i < letakSama.Count; i++)
            {
                if (id == 0)
                {
                    Console.Out.WriteLine(ListToken1[letakSama[i].X].SourceCode);
                    temp.Add(ListToken1[letakSama[i].X]);
                }
                else if (id == 1)
                {
                    Console.Out.WriteLine(ListToken2[letakSama[i].Y].SourceCode);
                    temp.Add(ListToken2[letakSama[i].Y]);
                }
            }
            return temp;
        }

        public List<String> GetAllControlReplacementsSourceCode(List<TokenUnit> toExtract)
        {
            List<String> temp = new List<String>();
            for (int i = 0; i < toExtract.Count; i++)
            {
                if (toExtract[i].TokenShape.Equals("CONDITION") ||
                    toExtract[i].TokenShape.Equals("LOOP"))
                {
                    temp.Add(toExtract[i].SourceCode);
                }
            }
            return temp;
        }

        public List<String> GetAllControlReplacementsToken(List<TokenUnit> toExtract)
        {
            List<String> temp = new List<String>();
            for (int i = 0; i < toExtract.Count; i++)
            {
                if (toExtract[i].TokenShape.Equals("CONDITION") ||
                    toExtract[i].TokenShape.Equals("LOOP"))
                {
                    temp.Add(toExtract[i].TokenShape);
                }
            }
            return temp;
        }

        public List<String> GetAllIdentifierRenamingSourceCode(List<TokenUnit> toExtract)
        {
            List<String> temp = new List<String>();
            for (int i = 0; i < toExtract.Count; i++)
            {
                if (toExtract[i].TokenShape.Equals("CLASS") ||
                    toExtract[i].TokenShape.Equals("INT") ||
                    toExtract[i].TokenShape.Equals("BOOLEAN") ||
                    toExtract[i].TokenShape.Equals("LONG") ||
                    toExtract[i].TokenShape.Equals("BYTE") ||
                    toExtract[i].TokenShape.Equals("FLOAT") ||
                    toExtract[i].TokenShape.Equals("CHAR") ||
                    toExtract[i].TokenShape.Equals("SHORT") ||
                    toExtract[i].TokenShape.Equals("DOUBLE")||
                    toExtract[i].TokenShape.IndexOf("METHOD")>=0)
                {
                    temp.Add(toExtract[i].SourceCode);
                }
            }
            return temp;
        }

        public List<String> GetAllIdentifierRenamingToken(List<TokenUnit> toExtract)
        {
            List<String> temp = new List<String>();
            for (int i = 0; i < toExtract.Count; i++)
            {
                if (toExtract[i].TokenShape.Equals("CLASS") ||
                    toExtract[i].TokenShape.StartsWith("INT")||
                    toExtract[i].TokenShape.Equals("BOOLEAN") ||
                    toExtract[i].TokenShape.Equals("LONG") ||
                    toExtract[i].TokenShape.Equals("BYTE") ||
                    toExtract[i].TokenShape.Equals("FLOAT") ||
                    toExtract[i].TokenShape.Equals("CHAR") ||
                    toExtract[i].TokenShape.Equals("SHORT") ||
                    toExtract[i].TokenShape.Equals("DOUBLE") ||
                    toExtract[i].TokenShape.IndexOf("METHOD") >= 0)
                {
                    temp.Add(toExtract[i].TokenShape);
                }
            }
            return temp;
        }

        public List<String> GetAllToken(List<TokenUnit> toExtract)
        {
            List<String> temp = new List<String>();
            for (int i = 0; i < toExtract.Count; i++)
            {
                temp.Add(toExtract[i].TokenShape);
            }
            return temp;
        }

        public List<String> GetAllSourceCodes(List<TokenUnit> toExtract)
        {
            List<String> temp = new List<String>();
            for (int i = 0; i < toExtract.Count; i++)
            {
                temp.Add(toExtract[i].SourceCode);
            }
            return temp;
        }

        public List<String> GetAllTokenToShow(List<TokenUnit> toExtract)
        {
            List<String> temp = new List<String>();
            for (int i = 0; i < toExtract.Count; i++)
            {
                temp.Add(i + 1 + ". " + toExtract[i].TokenShape);
            }
            return temp;
        }

        public List<String> GetAllSourceCodesToShow(List<TokenUnit> toExtract)
        {
            List<String> temp = new List<String>();
            for (int i = 0; i < toExtract.Count; i++)
            {
                temp.Add(i + 1 + ". " + toExtract[i].SourceCode);
            }
            return temp;
        }

        public double getSimilarity()
        {
            SmithWaterman sw = new SmithWaterman(TokenUnitListToTokenArray(listToken1), TokenUnitListToTokenArray(listToken2));
            sw.CalculateScore();
            letakSama = sw.TraceBack();
            return ((sw.MaxScore / sw.MaxGain) * 100);
        }

        private bool existString(String _str,String x)
        {
            bool hasil = false;
            int pointer1 = -1;
            int pointer2 = -1;
            int pointer3 = -1;
            bool exist=false;
            int len=_str.Length;
            int i=1;
            do{
                pointer1 = _str.IndexOf(x);
                pointer2 = _str.Substring(0,(pointer1+1)).LastIndexOf("\"");
                pointer3 = _str.IndexOf("\"",pointer2+1);
                if (((pointer2 < pointer1) && (pointer3 > pointer1) && ((pointer2 >= 0) || (pointer1 < 0)))&&(pointer3>pointer2))
                {
                    
                    hasil= false; 
                }
                else if (pointer1 >= 0)
                {
                    hasil= true;
                }
                else {
                    hasil= false;
                }
                String _str2 = _str.Substring(pointer1 + 1); 
                if (_str2.IndexOf(x) >= 0)
                {
                    exist = true;
                    _str = _str2;
                }
                else {
                    exist = false;
                }
                i++;
            }while(exist==true && i<=len);
            return hasil;
        }

        public void ClearListToken()
        {
            this.listToken1.Clear();
            this.listToken2.Clear();
            this.letakSama.Clear();
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Controller con = new Controller();
            con.Start();
        }
    }
}
