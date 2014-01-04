using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace exam
{
    public partial class frm_start : Form
    {
        public frm_start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_mdi_parent frm_mdi_parent = new frm_mdi_parent();
            frm_mdi_parent.Show();
            this.Hide();
        }
    }
}
