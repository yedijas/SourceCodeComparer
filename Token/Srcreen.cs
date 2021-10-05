using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Token
{
    public partial class Srcreen : Form
    {
        int i = 0;
        int similarityInt = 0;
        double similarityDouble = 0.0;
        Controller con;
        bool tampilToken = false;
        public Srcreen(Controller c)
        {
            this.con = c;
            
            InitializeComponent();
            lblSimilarity.Width=0;
            this.textBoxDir.AppendText("Klik textbox untuk memilih folder...");
        }

        //Mengatur property FileDialog
        private void setFileDialog(String _filter,String _title)
        {
            fileDialog.Filter = _filter;
            fileDialog.Title = _title;
            fileDialog.FilterIndex = 1;
            fileDialog.FileName = "";
            
        }

        //Method untuk menampilkan data ke text box yang ada pada Screen
        public void ShowToScreen()
        {
            rtbTokenA.Lines = con.GetAllSourceCodesToShow(con.ListToken1).ToArray();
            rtbTokenB.Lines = con.GetAllSourceCodesToShow(con.ListToken2).ToArray();
            // get similar
            List<TokenUnit> exListSatu = con.ExtractSimilar(0);
            List<TokenUnit> exListDua = con.ExtractSimilar(1);
            // Highlight similar
            HighlightAllA(con.GetAllSourceCodes(exListSatu), Color.LimeGreen);
            HighlightAllB(con.GetAllSourceCodes(exListDua), Color.LimeGreen);
            // Identifier Renaming
            HighlightAllA(con.GetAllIdentifierRenamingSourceCode(exListSatu), Color.Yellow);
            HighlightAllB(con.GetAllIdentifierRenamingSourceCode(exListDua), Color.Yellow);
            // Control Replacement
            HighlightAllA(con.GetAllControlReplacementsSourceCode(exListSatu), Color.SkyBlue);
            HighlightAllB(con.GetAllControlReplacementsSourceCode(exListDua), Color.SkyBlue);
        }

        private void ShowToScreenToken()
        {
            rtbTokenA.Lines = con.GetAllTokenToShow(con.ListToken1).ToArray();
            rtbTokenB.Lines = con.GetAllTokenToShow(con.ListToken2).ToArray();
            // get similar
            List<TokenUnit> exListSatu = con.ExtractSimilar(0);
            List<TokenUnit> exListDua = con.ExtractSimilar(1);
            // Highlight similar
            HighlightAllA(con.GetAllToken(exListSatu), Color.LimeGreen);
            HighlightAllB(con.GetAllToken(exListDua), Color.LimeGreen);
            // Identifier Renaming
            HighlightAllA(con.GetAllIdentifierRenamingToken(exListSatu), Color.Yellow);
            HighlightAllB(con.GetAllIdentifierRenamingToken(exListDua), Color.Yellow);
            // Control Replacement
            HighlightAllA(con.GetAllControlReplacementsToken(exListSatu), Color.SkyBlue);
            HighlightAllB(con.GetAllControlReplacementsToken(exListDua), Color.SkyBlue);
        }

        private bool CheckValidDir()
        {
            return textBoxDir.Text.Contains(":\\") &&
                !textBoxDir.Text.Equals("");
        }
        
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CheckValidDir())
            {
                try
                {
                    showFiles();
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    MessageBox.Show("Insert a valid Directory!");
                }
            }
            else
            {
                MessageBox.Show("Insert a valid Directory!");
            }
        }
        /// <summary>
        /// menampilkan representasi source code
        /// </summary>
        public void showFiles()
        {
            if (!textBoxDir.Text.Equals(""))
            {
                List<String> FileA = Directory.GetFiles(textBoxDir.Text).ToList<String>();
                List<String> FileB = Directory.GetFiles(textBoxDir.Text).ToList<String>();
                FileA.RemoveAll(NotASourceCode);
                FileB.RemoveAll(NotASourceCode);
                
                cmbFileA.DataSource = FileA;
                cmbFileB.DataSource = FileB;
                if (FileA.Count <= 0)
                {
                    MessageBox.Show("Fila Java Tidak ada di Folder ini");
                }
            }
            else
            {
                return;
            }
        }

        private bool NotASourceCode(String s)
        {
            bool result = false;
            
                result = !Path.GetExtension(s).Equals(".java");
            
            
            return result;
        }

        private void textBoxDir_Click(object sender, EventArgs e)
        {
            if (this.textBoxDir.Text.Equals("Klik textbox untuk memilih folder..."))
            {
                this.textBoxDir.Clear();
            }
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDir.Text = folderBrowserDialog.SelectedPath;

            }
        }

        private void Srcreen_Load(object sender, EventArgs e)
        {

        }

        private void rtbTokenA_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCek_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbFileA.SelectedItem.ToString().Equals("") || cmbFileB.SelectedItem.ToString().Equals(""))
                {
                    MessageBox.Show("Nama File tidak boleh kosong", "Error");
                }
                else
                {
                    if (cmbFileA.SelectedItem.ToString().Equals(cmbFileB.SelectedItem.ToString())) {
                        MessageBox.Show("File yang Anda bandingkan sama", "File Sama");
                    }
                    else
                    {
                        con.ClearListToken();
                        this.rtbTokenA.Clear();
                        this.rtbTokenB.Clear();

                        lblPersen.Text = " 0 %";
                        con.ListToken1 = con.ProsesFile(cmbFileA.SelectedItem.ToString());
                        con.ListToken2 = con.ProsesFile(cmbFileB.SelectedItem.ToString());
                        similarityDouble = con.getSimilarity();
                        similarityInt = (int)similarityDouble;
                        ShowToScreen();
                        i = 0;

                        lblSimilarity.Width = 0;
                        if (similarityDouble != 0.0)
                        {
                            timer.Enabled = true;
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nama File tidak boleh kosong", "Error");
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            
            int panjang = lblLong.Width;
            int inc = panjang / 100;
            String sim = "";
            sim = similarityDouble.ToString();
            if (i <= similarityInt)
            {
                lblSimilarity.Width = lblSimilarity.Width + inc;
                lblSimilarity.BackColor = Color.FromArgb(i+100, 100-i, 0);
                lblPersen.Text = " " + i + " %";
                i++;
            }
            else {
                if (sim.IndexOf(".")>=0)
                {
                    string sim1 = sim.Substring(sim.IndexOf("."));
                    if (sim1.Length > 3)
                    {
                        lblPersen.Text = " " + sim.Substring(0, sim.IndexOf(".") + 3) + " %";
                    }
                    else {
                        lblPersen.Text = " " + sim + " %";
                    }
                }
                
                timer.Dispose();
                timer.Enabled = false;

            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help hp = new Help();
            hp.Show();
        }

        private void textBoxDir_TextChanged(object sender, EventArgs e)
        {

        }

        public void HighlightAllA(List<String> lst, Color c)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Length >= 0)
                {
                    int startindex = 0;
                    int searchlength = lst[i].Length;
                    while ((startindex < rtbTokenA.TextLength)
                        && 0 <= (startindex = rtbTokenA.Find(lst[i],
                        startindex, RichTextBoxFinds.MatchCase)))
                    {
                        rtbTokenA.SelectionBackColor = c;
                        startindex += searchlength;
                    }
                }
            }
        }

        public void HighlightAllB(List<String> lst, Color c)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Length >= 0)
                {
                    int startindex = 0;
                    int searchlength = lst[i].Length;
                    while ((startindex < rtbTokenB.TextLength)
                        && 0 <= (startindex = rtbTokenB.Find(lst[i],
                        startindex, RichTextBoxFinds.MatchCase)))
                    {
                        rtbTokenB.SelectionBackColor = c;
                        startindex += searchlength;
                    }
                }
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            rtbTokenA.BackColor = Color.WhiteSmoke;
            rtbTokenB.BackColor = Color.WhiteSmoke;
            rtbTokenA.Clear();
            rtbTokenB.Clear();
            if (tampilToken == false)
            {
                tampilToken = true;
                btnShow.Image = Image.FromFile("E:\\Kuliah\\Semester VI\\TAII\\Aplikasi\\Token\\Gambar\\code.png");
                ShowToScreenToken();
            }
            else if(tampilToken == true){
                ShowToScreen();
                btnShow.Image = Image.FromFile("E:\\Kuliah\\Semester VI\\TAII\\Aplikasi\\Token\\Gambar\\token.png");
                tampilToken = false;
            }
        }

        private void penggunaanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
