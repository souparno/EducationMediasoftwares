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
    public partial class frm_insert_Image : Form
    {


        int _ROW;
        int _COLUMN;
  
        public int ROW
        {
            get { return _ROW;}
            set { _ROW = value; }
        }
        public int COLUMN
        {
            get { return _COLUMN; }
            set { _COLUMN = value; }
        }
        public TextBox QuestionImage
        {
            get { return txt_question; }
            set { txt_question = value; }
        }
       



        public frm_insert_Image()
        {InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            fdlg.Filter = "All files (*.*)|*.*|JPEG files (*.jpg)|*.jpg|PNG files(*.png)|*.png";
            if (fdlg.ShowDialog() == DialogResult.OK)
            {  txt_question.Text = fdlg.FileName;
            }

        }
       
          

        //button click event to put the image back to the grid-->
        private void button7_Click(object sender, EventArgs e)
        {     
                class_Application.frm_master_question.Questiongrid.Rows[ROW].Cells[1].Value = txt_question.Text;
                class_Application.frm_master_question.Questiongrid.Rows[ROW].Cells[20].Value = 1;
                this.Close();

        }

       

      




    }
}
