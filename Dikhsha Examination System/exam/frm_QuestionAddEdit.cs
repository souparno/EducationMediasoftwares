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


        DataTable dt;

        private void frm_QuestionAddEdit_Load(object sender, EventArgs e)
        {   s = null;
            ob = new class_Application();
            
            s = "SELECT Exam_Master.Exam_Code, Test_Master.Test_code, Subject_Master.Sub_code, Exam_Master.Exam_Name, " +
                "Test_Master.Test_Name, Subject_Master.Sub_Name FROM (Exam_Master INNER JOIN Test_Master ON " +
                "Exam_Master.Exam_Code = Test_Master.Exam_code) INNER JOIN Subject_Master ON Exam_Master.Exam_Code = " +
                "Subject_Master.Exam_code;";

            dt = new DataTable();
            dt = ob.fill_data_table(s);

            var query = from p in dt.AsEnumerable()
                        group p by new
                        {   ExamCode = p.Field<string>("Exam_Code"),
                            ExamName = p.Field<string>("Exam_Name")
                        } into grp
                        select new
                        {   ExamCode = grp.Key.ExamCode,
                            ExamName = grp.Key.ExamName
                        };

            DataTable ExamTable = new DataTable("Exam_Master");
            ExamTable.Columns.Add("ExamCode", typeof(string));
            ExamTable.Columns.Add("ExamName", typeof(string));

            foreach (var grp in query)
            {   DataRow dr = ExamTable.NewRow();
                dr["ExamCode"] = grp.ExamCode;
                dr["ExamName"] = grp.ExamName;
                ExamTable.Rows.Add(dr);
            }

           
            comboBox1.DataSource = ExamTable;
            comboBox1.DisplayMember = "ExamName";
             
        }

        //--selected index change event for the exam name combobox-->
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            exam_code = Convert.ToString(((DataRowView)comboBox1.SelectedValue)["ExamCode"]);
            var query = from p in dt.AsEnumerable()
                        where p.Field<string>("Exam_Code").Equals(exam_code)
                        group p by new
                        {
                            TestCode = p.Field<string>("Test_Code"),
                            TestName = p.Field<string>("Test_Name")
                        } into grp
                        select new
                        {
                            TestCode = grp.Key.TestCode,
                            TestName = grp.Key.TestName 
                        };


            DataTable TestTable = new DataTable("test_master");
            TestTable.Columns.Add("TestCode", typeof(string));
            TestTable.Columns.Add("TestName", typeof(string));

            foreach (var grp in query)
            {
                DataRow row = TestTable.NewRow();
                row["TestCode"] = grp.TestCode;
                row["TestName"] = grp.TestName;
                TestTable.Rows.Add(row);
            }
            comboBox2.DataSource = TestTable;
            comboBox2.DisplayMember = "TestName";
            
       }
        
        //--selected index change exant for the test name combobox-->
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            test_code = Convert.ToString(((DataRowView)comboBox2.SelectedValue)["TestCode"]);
            
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
            
            var query = from p in dt.AsEnumerable()
                        where p.Field<string>("Exam_Code").Equals(exam_code)
                        && p.Field<string>("Test_Code").Equals(test_code)
                        group p by new
                        {
                            SubCode = p.Field<string>("Sub_Code"),
                            SubName = p.Field<string>("Sub_Name")
                        } into grp
                        select new
                        {
                            SubCode=grp.Key.SubCode,
                            SubName=grp.Key.SubName 
                        };


            DataTable subject_table = new DataTable();
            subject_table.Columns.Add("SubCode", typeof(string));
            subject_table.Columns.Add("SubName",typeof(string));
            foreach (var grp in query)
            {
                DataRow dr = subject_table.NewRow();
                dr["SubCode"] = grp.SubCode;
                dr["SubName"] = grp.SubName;
                subject_table.Rows.Add(dr);
            }


            dataGridView1.Rows.Add(subject_table.Rows.Count);
            for (int i = 0; i < subject_table.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["sub_code"].Value = subject_table.Rows[i]["SubCode"];
                dataGridView1.Rows[i].Cells["sub_name"].Value = subject_table.Rows[i]["SubName"];
                s = null;
                s = "select iif ( count(*),1,0)  from questions_master where exam_code='" + exam_code + "' and test_code='" + test_code + "' and sub_code='" + Convert.ToString(subject_table.Rows[i]["SubCode"]) + "';";
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
