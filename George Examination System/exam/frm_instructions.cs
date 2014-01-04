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
    public partial class frm_instructions : Form
    {
        public frm_instructions()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //frm_select_exam_2 frm_select_exam_2 = new frm_select_exam_2();
            //frm_select_exam_2.StartPosition = FormStartPosition.CenterScreen;
            //frm_select_exam_2.Show();

             

            this.Hide();
            frm_select_exam frm_select_exam = new frm_select_exam();
            frm_select_exam.StartPosition = FormStartPosition.CenterScreen;
            frm_select_exam.Show();

        }
    }
}
