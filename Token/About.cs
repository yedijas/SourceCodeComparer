using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Token
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_UnLoad(object sender, EventArgs e)
        {
           
            
        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        
    }
}
