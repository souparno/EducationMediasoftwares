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
    public partial class frm_select_exam : Form
    {

        string s;
        static class_Application ob;
        
        String TestCode;
        String ExamCode;
        String ExamName;
        String TestName;
        int Duration;

        DataSet ds;


        public frm_select_exam()
        {InitializeComponent();
        }


        //---form load event-------->
        private void frm_select_exam_Load(object sender, EventArgs e)
        {
            ob = new class_Application();
            s = null;
            
            
             s="SELECT Test_Master.Exam_code, Test_Master.Test_code, Exam_Master.Exam_Name, Test_Master.Test_Name,"+
              " Test_Master.Test_Duration FROM (Exam_Master INNER JOIN Test_Master ON"+
              " Exam_Master.Exam_Code = Test_Master.Exam_code) INNER JOIN Questions_Master "+
              "ON (Exam_Master.Exam_Code = Questions_Master.Exam_code) AND (Test_Master.Test_code = Questions_Master.Test_code)"+
              " AND (Test_Master.Exam_code = Questions_Master.Exam_code);";
             ds = ob.fill_data_set(s);

             var query = from p in ds.Tables[0].AsEnumerable()
                         group p by new
                         {
                             ExamCode=p.Field<string>(0),
                             TestCode=p.Field<string>(1),
                             ExamNmae=p.Field<string>(2),
                             TestName=p.Field<string>(3),
                             Duration=p.Field<int>(4)
                         } into grp
                         select new
                         {
                             ExamCode = grp.Key.ExamCode,
                             TestCode = grp.Key.TestCode,
                             ExamNmae = grp.Key.ExamNmae,
                             TestName = grp.Key.TestName,
                             Duration = grp.Key.Duration 
                         };


          
             DataTable dt_table = new DataTable();
             dt_table.Columns.Add("ExamCode", typeof(string));
             dt_table.Columns.Add("TestCode", typeof(string));
             dt_table.Columns.Add("ExamName", typeof(string));
             dt_table.Columns.Add("TestName", typeof(string));
             dt_table.Columns.Add("Duration", typeof(int));
             DataRow combo_row;

             foreach (var grp in query)
             {   combo_row = dt_table.NewRow();
                 combo_row["ExamCode"] = grp.ExamCode;
                 combo_row["TestCode"] = grp.TestCode;
                 combo_row["ExamName"] = grp.ExamNmae;
                 combo_row["TestName"] = grp.TestName;
                 combo_row["Duration"] = grp.Duration;
                 dt_table.Rows.Add(combo_row);
            }

          
            comboBox1.DataSource = dt_table;
            comboBox1.DisplayMember = "ExamName";
            
         }


        //---select index change for the exam_name combobox--->
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {  
            DataRowView dr = (DataRowView)comboBox1.SelectedItem;
            ExamCode = Convert.ToString(dr["ExamCode"]);
            
            var query = from p in ds.Tables[0].AsEnumerable()
                        where p.Field<string>(0).Equals(ExamCode)
                        group p by new{
                            TestCode = p.Field<string>(1),
                            TestName = p.Field<string>(3)
                        } into grp
                        select new
                        {
                            TestCode=grp.Key.TestCode,
                            TestName=grp.Key.TestName 
                        };


            DataTable dt_table = new DataTable();
            dt_table.Columns.Add("TestCode", typeof(string));
            dt_table.Columns.Add("TestName", typeof(string));
            DataRow combo_row;

            foreach (var grp in query)
            {
                combo_row = dt_table.NewRow();
                combo_row["TestCode"] = grp.TestCode;
                combo_row["TestName"] = grp.TestName;
                dt_table.Rows.Add(combo_row);
            }

            comboBox3.DataSource = dt_table;
            comboBox3.DisplayMember = "TestName";
     
        }



        //--select index change for the test name combobox-->
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        { 
            DataRowView dr = (DataRowView)comboBox3.SelectedItem;
            TestCode = Convert.ToString(dr["TestCode"]);
            var query = from p in ds.Tables[0].AsEnumerable()
                        where p.Field<string>(0).Equals(ExamCode) && p.Field<string>(1).Equals(TestCode)
                        group p by new
                        {
                            ExamCode = p.Field<string>(0),
                            TestCode = p.Field<string>(1),
                            ExamNmae = p.Field<string>(2),
                            TestName = p.Field<string>(3),
                            Duration = p.Field<int>(4)
                        } into grp
                        select new
                        {
                            ExamCode = grp.Key.ExamCode,
                            TestCode = grp.Key.TestCode,
                            ExamNmae = grp.Key.ExamNmae,
                            TestName = grp.Key.TestName,
                            Duration = grp.Key.Duration 
                        };

                foreach(var grp in query)
                {
                    ExamCode = grp.ExamCode;
                    TestCode = grp.TestCode;
                    ExamName = grp.ExamNmae;
                    TestName = grp.TestName;
                    Duration = grp.Duration;
                }

        }


        //---button click event for take exam--->
        private void button1_Click(object sender, EventArgs e)
        {             
              

                frm_exam frm_exam = new frm_exam();
                frm_exam.TestCode = TestCode;
                frm_exam.ExamCode = ExamCode;
                frm_exam.TestName = TestName;
                frm_exam.ExamName = ExamName;
                frm_exam.Duration = Duration;
                frm_exam.StartPosition = FormStartPosition.CenterScreen;
                frm_exam.Show();

    
        }
    }
}
