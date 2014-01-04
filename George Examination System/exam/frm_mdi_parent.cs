using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;


using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;



namespace exam
{
    public partial class frm_mdi_parent : Form
    {

        static class_Application ob;

        public frm_mdi_parent()
        {
            InitializeComponent();
        }


        public void disable_menu()
        {        
            this.masterToolStripMenuItem.Visible = false;
            this.viewToolStripMenuItem.Visible = false;
            this.tOOLSToolStripMenuItem.Visible = false;
            this.lOGOFFUSERToolStripMenuItem.Visible = false;
        }

        public void enable_admin_menu()
        {
            this.masterToolStripMenuItem.Visible = true;
            this.viewToolStripMenuItem.Visible = true;
            this.tOOLSToolStripMenuItem.Visible = true;
            this.cREATEUSERToolStripMenuItem.Visible = true;
       
            this.lOGOFFUSERToolStripMenuItem.Visible = true;
        }

        public void enable_user_menu()
        {
            this.masterToolStripMenuItem.Visible = false;
            this.viewToolStripMenuItem.Visible = true;
            this.tOOLSToolStripMenuItem.Visible = true;
            this.cREATEUSERToolStripMenuItem.Visible = false;
       
            this.lOGOFFUSERToolStripMenuItem.Visible = true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //--instantiating the parent form-->
            class_Application.parent_form = this;
            //--instantiating the object ob of the class application--->
            ob = new class_Application();
            frm_login frm_login = new frm_login();
            frm_login.MdiParent  = this;
            frm_login.StartPosition = FormStartPosition.CenterScreen;
            frm_login.Show();
            
        }




        private void createQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {   frm_QuestionAddEdit frm_QuestionAddEdit = new frm_QuestionAddEdit();
            frm_QuestionAddEdit.StartPosition = FormStartPosition.CenterScreen;
            class_Application.frm_QuestionAddEdit = frm_QuestionAddEdit;
            frm_QuestionAddEdit.Show();


        }




        //---opening the test master--->
        private void createTestMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_master_test frm_create_question = new frm_master_test();
            frm_create_question.MdiParent = this;
            frm_create_question.StartPosition = FormStartPosition.CenterScreen;
            frm_create_question.Show();
        }

        //--opening the exam master------------->
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_master_exam frm_exam_master = new Frm_master_exam();
            frm_exam_master.MdiParent = this;
            frm_exam_master.StartPosition = FormStartPosition.CenterScreen;
            frm_exam_master.Show();
        }
        //---opening the subject master---------->
        private void crateSubjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frm_master_subject frm_subject_master = new frm_master_subject();
           frm_subject_master.MdiParent = this;
           frm_subject_master.StartPosition = FormStartPosition.CenterScreen;
           frm_subject_master.Show();
        }

        
        //--opening the exam selection combobox--->
        private void startExamToolStripMenuItem_Click(object sender, EventArgs e)
        {
              class_Application.review_flag = 1;
              frm_instructions frm_instructions = new frm_instructions();
              frm_instructions.StartPosition = FormStartPosition.CenterScreen;
              frm_instructions.Show();
            


     


        }

       

        private void gETRESULTToolStripMenuItem_Click(object sender, EventArgs e)
        {           
           
            //frm_SelectStudent frm_SelectStudent = new frm_SelectStudent();
            //frm_SelectStudent.MdiParent = this;
            //frm_SelectStudent.StartPosition = FormStartPosition.CenterScreen;
            //frm_SelectStudent.Show();

        }

        private void cREATEUSERToolStripMenuItem_Click(object sender, EventArgs e)
        {   frm_create_user frm_create_user = new frm_create_user();
            frm_create_user.MdiParent = this;
            frm_create_user.StartPosition = FormStartPosition.CenterScreen;
            frm_create_user.Show();
        }

        private void lOGOFFUSERToolStripMenuItem_Click(object sender, EventArgs e)
        {   //--instantiating the parent form-->
            class_Application.parent_form = this;
            //--instantiating the object ob of the class application--->
            ob = new class_Application();
            frm_login frm_login = new frm_login();
            frm_login.MdiParent = this;
            frm_login.StartPosition = FormStartPosition.CenterScreen;
            frm_login.Show();
         
        }

        private void cREATEORGANISATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {        
            frm_mater_organisation frm_organisation_details = new frm_mater_organisation();
            frm_organisation_details.StartPosition = FormStartPosition.CenterScreen;
            frm_organisation_details.Show();
        }

        private void rEVIEWEXAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            class_Application.review_flag = 0;
            frm_select_exam frm_select_exam = new frm_select_exam();
            frm_select_exam.StartPosition = FormStartPosition.CenterScreen;
            frm_select_exam.Show();
            

        }


        //--click event handler for exam summeryr--->
        private void eXAMSUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_result_det frm_result = new frm_result_det();
            DataSet rpt_dataset = populate_score();
            ReportDocument rpt = new crSTResult_Sum();
            rpt.SetDataSource(rpt_dataset);
            frm_result.CrystalReport.ReportSource = rpt;
            frm_result.CrystalReport.Refresh();
            frm_result.Show();
        }

        private void eXAMDETAILSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_result_det frm_result = new frm_result_det();
            DataSet rpt_dataset = populate_score();
            ReportDocument rpt = new crSTResult_Det();
            rpt.SetDataSource(rpt_dataset);
            frm_result.CrystalReport.ReportSource = rpt;
            frm_result.CrystalReport.Refresh();
            frm_result.Show();
        }
        //--click event handler for exam details--->


        private DataSet populate_score()
        {


            string UserId = class_Application.user_id;
            DataSet rpt_dataset = new rpt_student_result();
            DataSet dataset = new DataSet();
            string  s = "SELECT User_Exam_Dtl.User_ID, User_Exam_Dtl.Exam_code, User_Exam_Dtl.Test_code, User_Exam_Dtl.Sub_code," +
                    "User_Exam_Dtl.user_name, Exam_Master.Exam_Name, Test_Master.Test_Name, Subject_Master.Sub_Name, User_Exam_Dtl.Q_No," +
                    "User_Exam_Dtl.Option1, User_Exam_Dtl.Option2, User_Exam_Dtl.Option3, User_Exam_Dtl.Option4, User_Exam_Dtl.Option5," +
                    "User_Exam_Dtl.Correct_Option, User_Exam_Dtl.Is_Attempted FROM ((User_Exam_Dtl INNER JOIN Exam_Master ON" +
                    " User_Exam_Dtl.Exam_code = Exam_Master.Exam_Code) INNER JOIN Test_Master ON (Test_Master.Test_code = User_Exam_Dtl.Test_code)" +
                    " AND (Test_Master.Exam_code = User_Exam_Dtl.Exam_code) AND (Exam_Master.Exam_Code = Test_Master.Exam_code)) INNER JOIN Subject_Master" +
                    " ON (Subject_Master.Exam_code = User_Exam_Dtl.Exam_code) AND (Subject_Master.Sub_code = User_Exam_Dtl.Sub_code) AND" +
                    " (Exam_Master.Exam_Code = Subject_Master.Exam_code) WHERE User_Exam_Dtl.User_ID IN('" + UserId + "');";
            dataset = ob.fill_data_set(s);

            if (dataset.Tables.Count > 0)
            {
                dataset.Tables[0].Columns.Add("correct_ans", typeof(int));
                dataset.Tables[0].Columns.Add("wrong_ans", typeof(int));

                //--checking the corret and  the wrong answers per question-->
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    dr["correct_ans"] = 0;
                    dr["wrong_ans"] = 0;
                    string correct_option = Convert.ToString(dr["correct_option"]);
                    if (Convert.ToInt32(dr["option" + correct_option]) == 1 && Convert.ToInt32(dr["Is_Attempted"]) == 1)
                    {
                        dr["correct_ans"] = 1;
                        dr["wrong_ans"] = 0;
                    }
                    else if (Convert.ToInt32(dr["option" + correct_option]) == 0 && Convert.ToInt32(dr["Is_Attempted"]) == 1)
                    {
                        dr["correct_ans"] = 0;
                        dr["wrong_ans"] = 1;
                    }
                }


                var query = from p in dataset.Tables[0].AsEnumerable()
                            group p by new
                            {
                                user_id = p.Field<string>(0),
                                exam_no = p.Field<string>(1),
                                test_no = p.Field<string>(2),
                                user_name = p.Field<string>(4),
                                exam_name = p.Field<string>(5),
                                test_name = p.Field<string>(6)
                            } into grp
                            select new
                            {
                                no_of_ques = grp.Count(),
                                user_id = grp.Key.user_id,
                                exam_no = grp.Key.exam_no,
                                test_no = grp.Key.test_no,
                                user_name = grp.Key.user_name,
                                exam_name = grp.Key.exam_name,
                                test_name = grp.Key.test_name,
                                sum_correct_option = grp.Sum(r => r.Field<int>("correct_ans")),
                                sum_wrong_option = grp.Sum(r => r.Field<int>("wrong_ans"))
                            };


                DataRow rpt_row;
                foreach (var grp in query)
                {
                    rpt_row = rpt_dataset.Tables["dt_student_result"].NewRow();
                    rpt_row["no_of_ques"] = grp.no_of_ques;
                    rpt_row["user_id"] = grp.user_id;
                    rpt_row["user_name"] = grp.user_name;
                    rpt_row["exam_no"] = grp.exam_no;
                    rpt_row["test_no"] = grp.test_no;
                    rpt_row["test_name"] = grp.test_name;
                    rpt_row["exam_name"] = grp.exam_name;
                    rpt_row["sum_correct_ans"] = grp.sum_correct_option;
                    rpt_row["sum_wrong_ans"] = grp.sum_wrong_option;
                    rpt_dataset.Tables["dt_student_result"].Rows.Add(rpt_row);
                }

                var query2 = from p in dataset.Tables[0].AsEnumerable()
                             group p by new
                             {
                                 user_id = p.Field<string>(0),
                                 exam_no = p.Field<string>(1),
                                 test_no = p.Field<string>(2),
                                 sub_no = p.Field<string>(3),
                                 user_name = p.Field<string>(4),
                                 exam_name = p.Field<string>(5),
                                 test_name = p.Field<string>(6),
                                 sub_name = p.Field<string>(7)

                             } into grp
                             select new
                             {
                                 no_of_ques = grp.Count(),
                                 user_id = grp.Key.user_id,
                                 exam_no = grp.Key.exam_no,
                                 test_no = grp.Key.test_no,
                                 sub_no = grp.Key.sub_no,
                                 user_name = grp.Key.user_name,
                                 exam_name = grp.Key.exam_name,
                                 test_name = grp.Key.test_name,
                                 sub_name = grp.Key.sub_name,
                                 sum_correct_option = grp.Sum(r => r.Field<int>("correct_ans")),
                                 sum_wrong_option = grp.Sum(r => r.Field<int>("wrong_ans"))
                             };


                DataRow rpt_row2;
                foreach (var grp in query2)
                {
                    rpt_row2 = rpt_dataset.Tables["dt_student_result_details"].NewRow();
                    rpt_row2["no_of_ques"] = grp.no_of_ques;
                    rpt_row2["user_id"] = grp.user_id;
                    rpt_row2["user_name"] = grp.user_name;
                    rpt_row2["exam_no"] = grp.exam_no;
                    rpt_row2["test_no"] = grp.test_no;
                    rpt_row2["sub_no"] = grp.sub_no;
                    rpt_row2["test_name"] = grp.test_name;
                    rpt_row2["exam_name"] = grp.exam_name;
                    rpt_row2["sub_name"] = grp.sub_name;
                    rpt_row2["sum_correct_ans"] = grp.sum_correct_option;
                    rpt_row2["sum_wrong_ans"] = grp.sum_wrong_option;
                    rpt_dataset.Tables["dt_student_result_details"].Rows.Add(rpt_row2);
                }
            }
            return rpt_dataset;
        }

      




    


    }
}
