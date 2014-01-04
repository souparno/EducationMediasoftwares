using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;


public partial class Account_Test_Default : System.Web.UI.Page
{
    string _connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        _connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        if (!IsPostBack)
        {
            hdn_user_id.Value=Convert.ToString(Session["UserId"]);
            hdn_usr_role.Value = Convert.ToString(Session["UserRole"]);
            filltestsubject();
            filltestgrid();

        }
        else
        {
            submit_answer();
        }
    }

    private void filltestgrid()
    {
        SqlConnection con = new SqlConnection(_connection);
        string sql = "";

        foreach (GridViewRow row in grd_test_subjects.Rows)
        {
            string test_id = ((HiddenField)row.FindControl("hdn_test_id")).Value;
            string subject_id = ((HiddenField)row.FindControl("hdn_subj_id")).Value;
            GridView grd = (GridView)row.FindControl("grd_sbj_question");
            
           sql = "select subject_id,test_id,question_id,question_no,question,image_present,"+
                "question_img_name,answer1,answer2,answer3,answer4,answer5,pos_marks,neg_marks "+
                "from oes_question_master where test_id='"+test_id+"' and subject_id='"+subject_id+"';";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dt);
            grd.DataSource = dt;
            grd.DataBind();
        }



        foreach (GridViewRow row in grd_subject_dash.Rows)
        {
            string test_id = ((HiddenField)row.FindControl("hdn_test_id_dash")).Value;
            string subject_id = ((HiddenField)row.FindControl("hdn_subj_id_dash")).Value;
            GridView grd = (GridView)row.FindControl("grd_question_dash");
            sql = "select question_id,question_no from oes_question_master where test_id='" + test_id + "' and subject_id='" + subject_id + "';";
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dt);
            
            
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("content", typeof(string));
            DataRow dr = dt1.NewRow();
            string content = "";
            foreach(DataRow drow in dt.Rows){
                content = content + "<div class='column large-1 notattempted'>" +
                                    "<input type='hidden' value='" + drow["question_id"] + "'/>" +
                                    "<label style='font-size:smaller;text-align:center;'>" + drow["question_no"] + "</label>" +
                                "</div>";
            
            }


            dr["content"] = content;
            dt1.Rows.Add(dr);


            grd.DataSource = dt1;
            grd.DataBind();



        }

         sql = "select test_time_sec from oes_test_master where test_active='1';";
         SqlCommand cmd = new SqlCommand(sql, con);
         con.Open();
         string time = Convert.ToString(cmd.ExecuteScalar());
         con.Close();

         //time = "20";

         ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:CreateTimer(" + time + ");", true);
    }

    private void filltestsubject()
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(_connection);
        string sql = "select oes_subject_master.subject_id,subject_name,oes_test_master.test_id from oes_test_master " +
            "join oes_test_subject_master on oes_test_master.test_id=oes_test_subject_master.test_id "+
            "join oes_subject_master on oes_subject_master.subject_id =oes_test_subject_master.subject_id where test_active='1'";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(dt);

        grd_subject_dash.DataSource = dt;
        grd_subject_dash.DataBind();


        grd_test_subjects.DataSource = dt;
        grd_test_subjects.DataBind();
    }

    private void submit_answer()
    {

        string user_id = hdn_user_id.Value;

        foreach (GridViewRow row in grd_test_subjects.Rows)
        {
            string test_id = ((HiddenField)row.FindControl("hdn_test_id")).Value;
            string subject_id = ((HiddenField)row.FindControl("hdn_subj_id")).Value;
            GridView grd = (GridView)row.FindControl("grd_sbj_question");
            

            foreach (GridViewRow gv_row in grd.Rows)
            {
                string question_id = ((HiddenField)gv_row.FindControl("hdn_q_id")).Value;

                
                int ans1_ckecked = Convert.ToInt32(((HtmlInputRadioButton)gv_row.FindControl("radbtn_ans1")).Checked);
                int ans2_ckecked = Convert.ToInt32(((HtmlInputRadioButton)gv_row.FindControl("radbtn_ans2")).Checked);
                int ans3_ckecked = Convert.ToInt32(((HtmlInputRadioButton)gv_row.FindControl("radbtn_ans3")).Checked);
                int ans4_ckecked = Convert.ToInt32(((HtmlInputRadioButton)gv_row.FindControl("radbtn_ans4")).Checked);
                int ans5_ckecked = Convert.ToInt32(((HtmlInputRadioButton)gv_row.FindControl("radbtn_ans5")).Checked);

                int answer = 0;
                if (ans1_ckecked == 1) answer = 1;
                else if (ans2_ckecked == 1) answer = 2;
                else if (ans3_ckecked == 1) answer = 3;
                else if (ans4_ckecked == 1) answer = 4;
                else if (ans5_ckecked == 1) answer = 5;



                string sql = "insert into oes_answer_sheet(usr_id,test_id,subject_id,answer,question_id) " +
                "values('" + user_id + "','" + test_id + "','" + subject_id + "','" + answer + "','" + question_id + "')";

                SqlConnection con = new SqlConnection(_connection);
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }


            
        }

        string user_role = hdn_usr_role.Value;

        if(hdn_usr_role.Value == "ADMIN")  Response.Redirect("../Admin/TestComplete.aspx");
        else if (hdn_usr_role.Value == "STUDENT") Response.Redirect("../Student/TestComplete.aspx");

    }



    protected void btn_submit_Click(object sender, EventArgs e)
    {
       
    }
}