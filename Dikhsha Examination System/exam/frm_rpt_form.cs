using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;


namespace exam
{
    public partial class frm_rpt_form : Form
    {
        //--declarignt he instance variables--->
        static class_Application ob;
        string s;
        DataSet ds;
        ReportDocument rpt;

       



        public frm_rpt_form()
        { InitializeComponent();
        }



        public CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReport
        {
            get { return crystalReportViewer1; }
            set { crystalReportViewer1 = value; }
        }


        private void frm_rpt_form_Load(object sender, EventArgs e)
        {
            //DataSet ds_checking;
            //DataTable dt_check;
            //DataRow data_row;
            //ds_checking = new ds_checking();
            //dt_check = ds_checking.Tables["dt_check"];

            //s = null;
            //ds = new DataSet();
            //ob = new class_Application();
            //ob.quant_right_answer = 0;
            //ob.quant_wrong_answer = 0;
            //ob.verbal_right_answer = 0;
            //ob.verbal_wrong_answer = 0;



            //s = "SELECT Exam_Master.Exam_Name, Subject_Master.sub_group, Test_Master.Test_Name, User_Exam_Dtl.Q_No, User_Exam_Dtl.Correct_Option, User_Access.User_ID, User_Access.User_Name, Exam_Master.Exam_Code, Subject_Master.Sub_code, Test_Master.Test_code,is_attempted FROM User_Access INNER JOIN (((Exam_Master INNER JOIN User_Exam_Dtl ON Exam_Master.Exam_Code = User_Exam_Dtl.Exam_code) INNER JOIN Subject_Master ON (Subject_Master.Exam_code = User_Exam_Dtl.Exam_code) AND (Subject_Master.Sub_code = User_Exam_Dtl.Sub_code) AND (Exam_Master.Exam_Code = Subject_Master.Exam_code)) INNER JOIN Test_Master ON (Test_Master.Test_code = User_Exam_Dtl.Test_code) AND (Test_Master.Exam_code = User_Exam_Dtl.Exam_code) AND (Exam_Master.Exam_Code = Test_Master.Exam_code)) ON User_Access.User_ID = User_Exam_Dtl.User_ID WHERE (((User_Access.User_ID)='"+ class_Application.user_id +"')) and  sub_group<>'other';";
            //ds = ob.fill_data_set(s);
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{   int right_answer = 0;
            //    int wrong_answer = 0;
            //    int attempt_flg = 0;
            //    int q_type = 0;
            //    int flag = 1;

            //    string[] new_array = Convert.ToString(dr["correct_option"]).Split(',');
              
            //    //---start fo checking for the answers by indivisual questions--->
            //    foreach (string a in new_array)
            //    {
            //        attempt_flg = Convert.ToInt32(ob.execute_scalar("select is_attempted from user_exam_dtl where q_no="+ dr["q_no"]+" and exam_code='"+ Convert.ToString(dr["exam_code"]) +"' and test_code='"+ Convert.ToString(dr["test_code"])+"' and sub_code='"+ Convert.ToString(dr["sub_code"])+"'"));
            //        q_type = Convert.ToInt32(ob.execute_scalar("select q_type from user_exam_dtl where q_no=" + dr["q_no"] + " and exam_code='" + Convert.ToString(dr["exam_code"]) + "' and test_code='" + Convert.ToString(dr["test_code"]) + "' and sub_code='" + Convert.ToString(dr["sub_code"]) + "'"));

            //        if (q_type != 2 && q_type!=5)
            //        {
            //            s = "select count(*) from user_exam_dtl where option" + a + " =1 and q_no=" + dr["q_no"] + " and exam_code='" + Convert.ToString(dr["exam_code"]) + "' and test_code='" + Convert.ToString(dr["test_code"]) + "' and sub_code='" + Convert.ToString(dr["sub_code"]) + "' and is_attempted=1 and q_type<>2";
            //            int count = Convert.ToInt32(ob.execute_scalar(s));
            //            if (count < 1)
            //            {
            //                flag = 0;
            //            }
            //        }
            //    }//--end of checking if the answer for that question was correct or wrong-->
            //    if (flag == 1 && attempt_flg==1)
            //    {right_answer = 1;
            //    }
            //    else if(flag==0 && attempt_flg==1)
            //    {wrong_answer = 1;
            //    }


            

            //    data_row = dt_check.NewRow();
            //    data_row["exam_name"] = dr["Exam_Name"];
            //    data_row["test_name"] = dr["test_name"];
            //    data_row["sub_group"] = dr["sub_group"];
            //    data_row["correct_answer"] = right_answer;
            //    data_row["wrong_answer"] = wrong_answer;
            //    data_row["user_id"] = dr["user_id"];
            //    data_row["user_name"] = dr["user_name"];
            //    data_row["test_code"]=dr["test_code"];
            //    data_row["exam_code"] = dr["exam_code"];
            //    data_row["attempted"]=dr["is_attempted"];
            //    ds_checking.Tables["dt_check"].Rows.Add(data_row);
            // }

            
            
            ////--query for grouping the dataset by exam name and sub_group name-->
            //var query = from p in ds_checking.Tables["dt_check"].AsEnumerable()
            //            group p by new { 
            //                exam_name = p.Field<string>("Exam_name"), 
            //                sub_group = p.Field<string>("sub_group"),
            //                test_name = p.Field<string>("test_name"),
            //                user_id =  p.Field<string>("user_id"),
            //                user_name = p.Field<string>("user_name"),
            //                exam_code=p.Field<string>("exam_code"),
            //                test_code=p.Field<string>("test_code")
            //            } into grp
            //            select new
            //            {   exam_code=grp.Key.exam_code,
            //                test_code=grp.Key.test_code,
            //                user_id=grp.Key.user_id,
            //                user_name=grp.Key.user_name,
            //                test_name=grp.Key.test_name,
            //                exam_name=grp.Key.exam_name,
            //                sub_group=grp.Key.sub_group,
            //                sum_right_ans = grp.Sum(r => r.Field<Int32>("correct_answer")),
            //                sum_wrong_ans = grp.Sum(r => r.Field<Int32>("wrong_answer")),
            //                sum_attempted = grp.Sum(r => r.Field<Int32>("attempted"))
            //            };




        

            //DataSet ds_score = new ds_score();
            //DataTable dt_score= ds_score.Tables["dt_score"];
            //DataRow dt_row;


            //foreach (var grp in query)
            //{   
            //    dt_row=dt_score.NewRow();

            //    //---getting the number of questions for that perticular exam and test---->
            //    string no_of_questions = null;
            //    s = null;
            //    s = "select count(*) from questions_master where exam_code='" + Convert.ToString(grp.exam_code) + "' and test_code='" + Convert.ToString(grp.test_code) + "'";
            //    no_of_questions = ob.execute_scalar(s);


            //    dt_row["user_id"] = Convert.ToString(grp.user_id);
            //    dt_row["user_name"] = Convert.ToString(grp.user_name);
            //    dt_row["exam_name"] = Convert.ToString(grp.exam_name);
            //    dt_row["test_name"] = Convert.ToString(grp.test_name);
            //    dt_row["sub_group"] = Convert.ToString(grp.sub_group);
            //    dt_row["no_of_questions"] = no_of_questions;
            //    dt_row["no_of_correct_ans"] = Convert.ToString(grp.sum_right_ans);
            //    dt_row["no_of_wrong_ans"] = Convert.ToString(grp.sum_wrong_ans);
            //    dt_row["attempted"] = Convert.ToString(grp.sum_attempted);

            //    if (Convert.ToString(grp.exam_name).Equals("GRE"))
            //    {   
            //        dt_row["raw_score"] = Convert.ToString(grp.sum_right_ans);
            //        //---query to fetch the percentile for the raw score if the subject group is quant---->
            //        if (Convert.ToString(grp.sub_group).Equals("QUANT"))
            //        {
            //            var percentile = from p in class_Application.ds_gre_answer.Tables["dt_answer_check"].AsEnumerable()
            //                             where p.Field<int>(0).Equals(Convert.ToInt32(grp.sum_right_ans))
            //                             select new
            //                             {
            //                                 quant_scaled_score = p.Field<int>(2)
            //                             };
            //            foreach (var q in percentile)
            //            { dt_row["percentile"] = Convert.ToString(q.quant_scaled_score);
            //            }
            //        }
            //        //---query to fetch the percentile for the raw score if the subject group is vgerbal-->
            //        else if (Convert.ToString(grp.sub_group).Equals("VERBAL"))
            //        {
            //            var percentile = from p in class_Application.ds_gre_answer.Tables["dt_answer_check"].AsEnumerable()
            //                             where p.Field<int>(0).Equals(Convert.ToInt32(grp.sum_right_ans))
            //                             select new
            //                             {
            //                                 quant_scaled_score = p.Field<int>(1)
            //                             };
            //            foreach (var q in percentile)
            //            { dt_row["percentile"] = Convert.ToString(q.quant_scaled_score);
            //            }

                      

            //        }
            //    }//--end of marks checking for gre exam-->




            //    else if (Convert.ToString(grp.exam_name).Equals("GMAT"))
            //    {
            //        dt_row["raw_score"] = Convert.ToString((int)Math.Round(grp.sum_right_ans-0.25*grp.sum_wrong_ans));
            //        s = null;
            //        s = "select raw_score,scaled_score,percentile,";


            //    }//---end of marks checking for gmat exam--->




            //    ds_score.Tables["dt_score"].Rows.Add(dt_row);
            //    }

        



            ////--referencignt th ecrystal report to the dataset-->
            //rpt = new rpt_score();
            //rpt.SetDataSource(ds_score);
            //crystalReportViewer1.ReportSource = rpt;
            //crystalReportViewer1.Refresh();


        }


      


    }
}
