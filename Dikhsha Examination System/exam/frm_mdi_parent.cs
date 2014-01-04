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
            this.aDMININSTRUCTIONToolStripMenuItem.Visible = true;
        }

        public void enable_user_menu()
        {
            this.masterToolStripMenuItem.Visible = false;
            this.viewToolStripMenuItem.Visible = true;
            this.tOOLSToolStripMenuItem.Visible = true;
            this.cREATEUSERToolStripMenuItem.Visible = false;
            this.lOGOFFUSERToolStripMenuItem.Visible = true;
            this.aDMININSTRUCTIONToolStripMenuItem.Visible = false;
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
        {   class_Application.frm_QuestionAddEdit = new frm_QuestionAddEdit();
            class_Application.frm_QuestionAddEdit.MdiParent = this;
            class_Application.frm_QuestionAddEdit.StartPosition = FormStartPosition.CenterScreen;
            class_Application.frm_QuestionAddEdit.Show();
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
        //---opening the section master
        private void cREATESECTIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_master_Group frm_sub_group = new frm_master_Group();
            frm_sub_group.MdiParent = this;
            frm_sub_group.StartPosition = FormStartPosition.CenterScreen;
            frm_sub_group.Show();
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
            class_Application.REVIEW = 0;
            frm_select_exam frm_select_exam = new frm_select_exam();
            frm_select_exam.MdiParent = this;
            frm_select_exam.StartPosition = FormStartPosition.CenterScreen;
            frm_select_exam.Show();
        }

        //---toolbar fro exam review--->
        private void rEVIEWEXAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            class_Application.REVIEW = 1;
            frm_select_exam frm_select_exam = new frm_select_exam();
            frm_select_exam.MdiParent = this;
            frm_select_exam.StartPosition = FormStartPosition.CenterScreen;
            frm_select_exam.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(class_Application.ds_gmat_answer.Tables["gmat"].Rows[0][0]));

        }

        private void aDMININSTRUCTIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_admin_instruction frm_admin_instruction = new frm_admin_instruction();
            frm_admin_instruction.MdiParent = this;
            frm_admin_instruction.StartPosition = FormStartPosition.CenterScreen;
            frm_admin_instruction.Show();
        }

        //---tool strip menu for getting the exam summery-->
        private void eXAMSUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_rpt_form frm_rpt_form = new frm_rpt_form();
            DataSet rpt_dataset = populate_score();

            ReportDocument rpt = new rpt_score_summery();
            rpt.SetDataSource(rpt_dataset);

            frm_rpt_form.CrystalReport.ReportSource = rpt;
            frm_rpt_form.CrystalReport.Refresh();

            frm_rpt_form.Show();
        }

        //---tool strip menu for getting the exam details--->
        private void eXAMDETAILSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_rpt_form frm_rpt_form = new frm_rpt_form();
            DataSet rpt_dataset = populate_score();

            ReportDocument rpt = new rpt_score_details();
            rpt.SetDataSource(rpt_dataset);

            frm_rpt_form.CrystalReport.ReportSource = rpt;
            frm_rpt_form.CrystalReport.Refresh();

            frm_rpt_form.Show();
        }


        private DataSet populate_score()
        {


            string UserId = class_Application.user_id;
            DataSet rpt_dataset = new rpt_student_result();
            DataSet dataset = new DataSet();
                       




       string s = "SELECT User_Exam_Dtl.User_ID, User_Exam_Dtl.Exam_code, User_Exam_Dtl.Test_code, User_Exam_Dtl.Sub_code, User_Exam_Dtl.User_name, "+
             "Exam_Master.Exam_Name, Test_Master.Test_Name, Subject_Master.Sub_Name, User_Exam_Dtl.Q_No, User_Exam_Dtl.Option1, User_Exam_Dtl.Option2, "+
             "User_Exam_Dtl.Option3, User_Exam_Dtl.Option4, User_Exam_Dtl.Option5, Questions_Master.Correct_Option, option1+option2+option3+option4+"+
             "option5+option6+option7+option8+option9+option10+option11+option12+option13+option14+option15+option16+option17+option18 AS Is_Attempted, "+
             "User_Exam_Dtl.Option6, User_Exam_Dtl.Option7, User_Exam_Dtl.Option8, User_Exam_Dtl.Option9, User_Exam_Dtl.Option10, User_Exam_Dtl.Option11, "+
             "User_Exam_Dtl.Option12, User_Exam_Dtl.Option13, User_Exam_Dtl.Option14, User_Exam_Dtl.Option15, User_Exam_Dtl.Option16, "+
             "User_Exam_Dtl.Option17, User_Exam_Dtl.Option18, User_Exam_Dtl.Essay_Answer, Questions_Master.Q_type, Exam_Master.Negetive_marking, "+
             "Group_Master.Group_Name, Group_Master.Group_Code FROM Questions_Master INNER JOIN (Group_Master INNER JOIN (((User_Exam_Dtl INNER JOIN "+
             "Exam_Master ON User_Exam_Dtl.Exam_code = Exam_Master.Exam_Code) INNER JOIN Test_Master ON (Exam_Master.Exam_Code = Test_Master.Exam_code) AND "+
             "(User_Exam_Dtl.Exam_code = Test_Master.Exam_code) AND (User_Exam_Dtl.Test_code = Test_Master.Test_code)) INNER JOIN Subject_Master ON "+
             "(Exam_Master.Exam_Code = Subject_Master.Exam_code) AND (User_Exam_Dtl.Sub_code = Subject_Master.Sub_code) AND (User_Exam_Dtl.Exam_code = "+
             "Subject_Master.Exam_code)) ON Group_Master.Group_Code = Subject_Master.sub_group) ON (Exam_Master.Exam_Code = Questions_Master.Exam_code) AND "+
             "(Test_Master.Test_code = Questions_Master.Test_code) AND (Test_Master.Exam_code = Questions_Master.Exam_code) AND "+
             "(Subject_Master.Exam_code = Questions_Master.Exam_code) AND (Subject_Master.Sub_code = Questions_Master.Sub_code) AND "+
             "(Questions_Master.Q_No = User_Exam_Dtl.Q_No) AND (Questions_Master.Test_code = User_Exam_Dtl.Test_code) AND (Questions_Master.Sub_code = "+
             "User_Exam_Dtl.Sub_code) AND (Questions_Master.Exam_code = User_Exam_Dtl.Exam_code) WHERE (((User_Exam_Dtl.User_ID) In ('"+ UserId +"')));";


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

                    int flag = 0;
                    if (Convert.ToInt32(dr["Q_type"]) == 1 || Convert.ToInt32(dr["Q_type"]) == 2 || 
                        Convert.ToInt32(dr["Q_type"]) == 4 || Convert.ToInt32(dr["Q_type"]) == 5)
                    {

                        ArrayList arr_list = new ArrayList();
                        for (int i = 1; i <= 18; i++)
                        {
                            if (Convert.ToInt32(dr["option" + i]) == 1)
                            {
                                arr_list.Add(Convert.ToString(i));
                            }
                        }
                        string[] ticked = (string[])arr_list.ToArray(typeof(string));
                        string[] new_array = Convert.ToString(dr["correct_option"]).Split(',');

                        if (ticked.Length == new_array.Length)
                        {
                            for (int i = 0; i < ticked.Length; i++)
                            {
                                if (ticked[i].Equals(new_array[i])) flag = 1;
                                else flag = 0; break;
                            }

                        }
                        else if(ticked.Length != new_array.Length)
                        {
                            flag = 0;
                        }
                    }
                    else if (Convert.ToInt32(dr["Q_type"]) == 3)
                    {
                        if (Convert.ToString(dr["correct_option"]).Equals(Convert.ToString(dr["Essay_Answer"]))) flag = 1;

                        else flag = 0;
                    }

                    if (flag == 1 && Convert.ToInt32(dr["Is_Attempted"]) >= 1)
                    {
                        dr["correct_ans"] = 1;
                        dr["wrong_ans"] = 0;
                    }
                    else if (flag == 0 && Convert.ToInt32(dr["Is_Attempted"]) >= 1)
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
                                test_name = p.Field<string>(6),
                                group_name=p.Field<string>(32),
                                group_code=p.Field<string>(33),
                                has_negetive_marking = p.Field<int>(31)
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
                                has_negetive_marking = grp.Key.has_negetive_marking, 
                                group_name=grp.Key.group_name,
                                group_code=grp.Key.group_code,
                                sum_correct_option = grp.Sum(r => r.Field<int>("correct_ans")),
                                sum_wrong_option = grp.Sum(r => r.Field<int>("wrong_ans"))
                            };


                DataRow rpt_row;
                foreach (var grp in query)
                {
                    rpt_row = rpt_dataset.Tables["dt_student_result_summery"].NewRow();
                    rpt_row["no_of_ques"] = grp.no_of_ques;
                    rpt_row["user_id"] = grp.user_id;
                    rpt_row["user_name"] = grp.user_name;
                    rpt_row["exam_no"] = grp.exam_no;
                    rpt_row["test_no"] = grp.test_no;
                    rpt_row["test_name"] = grp.test_name;
                    rpt_row["exam_name"] = grp.exam_name;
                    rpt_row["sum_correct_ans"] = grp.sum_correct_option;
                    rpt_row["sum_wrong_ans"] = grp.sum_wrong_option;
                    rpt_row["has_negetive_marking"] = Convert.ToBoolean(grp.has_negetive_marking);
                    rpt_row["group_code"] = grp.group_code;
                    rpt_row["group_name"] = grp.group_name;
                    rpt_dataset.Tables["dt_student_result_summery"].Rows.Add(rpt_row);
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
                                 sub_name = p.Field<string>(7),
                                  group_name=p.Field<string>(32),
                                group_code=p.Field<string>(33),
                                 has_negetive_marking = p.Field<int>(31)
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
                                 has_negetive_marking = grp.Key.has_negetive_marking,
                                 group_name = grp.Key.group_name,
                                 group_code = grp.Key.group_code,
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
                    rpt_row2["group_code"] = grp.group_code;
                    rpt_row2["group_name"] = grp.group_name;
                    rpt_row2["has_negetive_marking"] = Convert.ToBoolean(grp.has_negetive_marking);
                    rpt_dataset.Tables["dt_student_result_details"].Rows.Add(rpt_row2);
                }
            }
            return rpt_dataset;
        }

        private void cHANGEPASSWORDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

   

      


    


    }
}
