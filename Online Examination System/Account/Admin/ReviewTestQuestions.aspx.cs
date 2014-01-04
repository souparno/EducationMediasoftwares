using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Account_Admin_ReviewTestQuestions : System.Web.UI.Page
{
    string _connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        _connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        if (!IsPostBack)
        {
            fill_ddl_test();
            fill_ddl_subject();
        }
        else
        {
            string subject_id = ddl_subject.SelectedValue;
            string test_id = ddl_test.SelectedValue;
            ViewState["_subject_id"] = subject_id;
            ViewState["_test_id"] = test_id;
        }
    }


    private void fill_ddl_test()
    {
        SqlConnection con = new SqlConnection(_connection);
        string sql = "select test_id,test_name from oes_test_master;";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddl_test.DataSource = dt;
        ddl_test.DataTextField = "test_name";
        ddl_test.DataValueField = "test_id";
        ddl_test.DataBind();

    }

    private void fill_ddl_subject()
    {
        SqlConnection con = new SqlConnection(_connection);
        string sql = "select subject_id,subject_name from oes_subject_master;";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddl_subject.DataSource = dt;
        ddl_subject.DataTextField = "subject_name";
        ddl_subject.DataValueField = "subject_id";
        ddl_subject.DataBind();
    }




    protected void btn_view_questions_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(_connection);
        string sql = "select question_id,question_no,question,image_present,question_img_name,answer1,answer2,answer3,answer4,answer5,pos_marks,neg_marks,correct_option from oes_question_master where subject_id='' and test_id='1';";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        da.Fill(dt);



        grd_questions.DataSource = dt;
        grd_questions.DataBind();


    }
}