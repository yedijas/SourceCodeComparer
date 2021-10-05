using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Token
{
    public class SourceFile
    {
        //Atribut
        String pathFile;


        FileStream fs;
        TextReader reader;

        //Constructor berparameter
        public SourceFile(String _path)
        {
            pathFile = _path;
            fs = File.OpenRead(pathFile);
            reader = new StreamReader(fs);
        }

        //Setter Getter
        public String PathFile
        {
            get { return pathFile; }
            set { pathFile = value; }
        }

        //Method untuk mencek apakah file sudah habis dibaca
        public bool IsLast()
        {
            if (reader.Peek() > -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Mengembalikan 1 baris file yang dibaca
        public String ReadOneLine()
        {
            return this.reader.ReadLine();
        }

        //Method untuk menutup file yang sudah dibuka
        public void CloseFile()
        {
            fs.Close();
        }
    }
}
