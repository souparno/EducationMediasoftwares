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
    public partial class frm_QuestionAddEdit : Form
    {
        public frm_QuestionAddEdit()
        {
            InitializeComponent();
        }


        public DataGridView DataGridView1
        {
            get { return dataGridView1; }
            set { dataGridView1 = value; }


        }

        string s;
        private static class_Application ob;
        string exam_code;
        string test_code;


        private void frm_QuestionAddEdit_Load(object sender, EventArgs e)
        {   s = null;
            ob = new class_Application();
            s = "select exam_code,exam_name from exam_master";
            ob.fill_combo_box(comboBox1, s, "exam_name");

   
        }

        //--selected index change event for the exam name combobox-->
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)comboBox1.SelectedItem;
            exam_code = Convert.ToString(dr["exam_code"]);
            s = "select test_code,test_name from test_master where exam_code='" + exam_code + "'";
            ob.fill_combo_box(comboBox2, s, "test_name");

        }
        
        //--selected index change exant for the test name combobox-->
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataRowView dr=(DataRowView)comboBox2.SelectedItem;
            test_code=Convert.ToString(dr["test_code"]);

            dataGridView1.Columns.Clear();
            DataGridViewCheckBoxColumn ob1 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add("sub_code", "SUBJECT CODE");
            dataGridView1.Columns.Add("sub_name", "SUBJECT NAME");
            dataGridView1.Columns.Add(ob1);
            dataGridView1.Columns[2].HeaderText = "QUESTION PRESENT";
            dataGridView1.Columns[2].Name = "is_present";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[2].ReadOnly = true;



            s = "select sub_code,sub_name from subject_master where exam_code='" + exam_code + "'";
            DataSet ds = ob.fill_data_set(s);
            dataGridView1.Rows.Add(ds.Tables[0].Rows.Count);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {   dataGridView1.Rows[i].Cells["sub_code"].Value = ds.Tables[0].Rows[i]["sub_code"];
                dataGridView1.Rows[i].Cells["sub_name"].Value = ds.Tables[0].Rows[i]["sub_name"];
                s = null;
                s = "select iif ( count(*),1,0)  from questions_master where exam_code='" + exam_code + "' and test_code='" + test_code + "' and sub_code='" + Convert.ToString(ds.Tables[0].Rows[i]["sub_code"]) + "';";
                dataGridView1.Rows[i].Cells["is_present"].Value = Convert.ToInt32(ob.execute_scalar(s));
            }
            

        }

        //---double click event for data grid view1--->
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             class_Application.frm_master_question = new frm_master_question();
             class_Application.frm_master_question.StartPosition = FormStartPosition.CenterScreen;
             class_Application.frm_master_question.SubjectName.Text = Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["sub_name"].Value);
             class_Application.frm_master_question.SubjectCode = Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["sub_code"].Value); ;
             class_Application.frm_master_question.TestCode = test_code;
             class_Application.frm_master_question.ExamCode = exam_code;
             class_Application.frm_master_question.Show();

        }

    }
}
