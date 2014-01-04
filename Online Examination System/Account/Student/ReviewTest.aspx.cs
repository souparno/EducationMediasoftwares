using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;


public partial class Account_Admin_ReviewTest : System.Web.UI.Page
{
    string _connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        _connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        if (!IsPostBack)
        {
            fill_ddl_test();
            hdn_user_id.Value = Convert.ToString(Session["UserId"]);
        }
        else
        {
            string test_id = ddl_test_name.SelectedValue;
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
        ddl_test_name.DataSource = dt;
        ddl_test_name.DataTextField = "test_name";
        ddl_test_name.DataValueField = "test_id";
        ddl_test_name.DataBind();

    }


   


    protected void btnview_result_Click(object sender, EventArgs e)
    {


        string user_id = Convert.ToString(hdn_user_id.Value);
        string test_id = Convert.ToString(ViewState["_test_id"]);


        SqlConnection con = new SqlConnection(_connection);
        string sql = "select usr_id,test_id,subject_id,usr_name,"+
            "test_name,subject_name,SUM(Marks) as 'Marks' "+
            "from(select oes_user_master.usr_id,oes_test_master.test_id,"+
            "oes_subject_master.subject_id,usr_name,test_name,"+
            "subject_name,pos_marks as 'Marks' from oes_answer_sheet join "+
            "oes_user_master on oes_user_master.usr_id=oes_answer_sheet.usr_id "+
            "join oes_test_master on oes_test_master.test_id=oes_answer_sheet.test_id "+
            "join oes_subject_master on oes_subject_master.subject_id=oes_answer_sheet.subject_id "+
            "join oes_question_master on oes_question_master.test_id=oes_answer_sheet.test_id and"+
            " oes_question_master.subject_id=oes_answer_sheet.subject_id and "+
            "oes_question_master.question_id=oes_answer_sheet.question_id where "+
            "correct_option=answer union all select oes_user_master.usr_id,oes_test_master.test_id,"+
            "oes_subject_master.subject_id,usr_name,test_name,subject_name,(-1)*neg_marks as 'Marks' "+
            "from oes_answer_sheet join oes_user_master on oes_user_master.usr_id=oes_answer_sheet.usr_id "+
            "join oes_test_master on oes_test_master.test_id=oes_answer_sheet.test_id join "+
            "oes_subject_master on oes_subject_master.subject_id=oes_answer_sheet.subject_id join "+
            "oes_question_master on oes_question_master.test_id=oes_answer_sheet.test_id and "+
            "oes_question_master.subject_id=oes_answer_sheet.subject_id and "+
            "oes_question_master.question_id=oes_answer_sheet.question_id where correct_option<>answer and answer<>0 ) "+
            "as x where usr_id='"+user_id+"' and test_id='"+test_id+"' group by usr_id,test_id,subject_id,usr_name,test_name,subject_name";

       

        //result ds = new result();
        //SqlDataAdapter da = new SqlDataAdapter(sql, con);
        //da.Fill(ds,"dt_result");


        //ReportDocument reportdocument = new ReportDocument();
        //reportdocument.Load(Server.MapPath("../Test/Result_1.rpt"));
        //reportdocument.SetDataSource(ds);

        //CrystalReportViewer_result.ReportSource = reportdocument;
        //CrystalReportViewer_result.DataBind();
        //CrystalReportViewer_result.RefreshReport();


        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(dt);

        grd_review_test.DataSource = dt;
        grd_review_test.DataBind();


        sql = "select usr_id,test_id,usr_name," +
            "test_name,SUM(Marks) as 'Marks' " +
            "from(select oes_user_master.usr_id,oes_test_master.test_id," +
            "oes_subject_master.subject_id,usr_name,test_name," +
            "subject_name,pos_marks as 'Marks' from oes_answer_sheet join " +
            "oes_user_master on oes_user_master.usr_id=oes_answer_sheet.usr_id " +
            "join oes_test_master on oes_test_master.test_id=oes_answer_sheet.test_id " +
            "join oes_subject_master on oes_subject_master.subject_id=oes_answer_sheet.subject_id " +
            "join oes_question_master on oes_question_master.test_id=oes_answer_sheet.test_id and" +
            " oes_question_master.subject_id=oes_answer_sheet.subject_id and " +
            "oes_question_master.question_id=oes_answer_sheet.question_id where " +
            "correct_option=answer union all select oes_user_master.usr_id,oes_test_master.test_id," +
            "oes_subject_master.subject_id,usr_name,test_name,subject_name,(-1)*neg_marks as 'Marks' " +
            "from oes_answer_sheet join oes_user_master on oes_user_master.usr_id=oes_answer_sheet.usr_id " +
            "join oes_test_master on oes_test_master.test_id=oes_answer_sheet.test_id join " +
            "oes_subject_master on oes_subject_master.subject_id=oes_answer_sheet.subject_id join " +
            "oes_question_master on oes_question_master.test_id=oes_answer_sheet.test_id and " +
            "oes_question_master.subject_id=oes_answer_sheet.subject_id and " +
            "oes_question_master.question_id=oes_answer_sheet.question_id where correct_option<>answer and answer<>0 ) " +
            "as x where usr_id='" + user_id + "' and test_id='" + test_id + 
            "' group by usr_id,test_id,usr_name,test_name";

        DataTable dt1 = new DataTable();
        da = new SqlDataAdapter(sql, con);
        da.Fill(dt1);
        if (dt1.Rows.Count > 0) lbl_tot_marks.Text = "TOTAL MARKS: " + Convert.ToString(dt1.Rows[0]["Marks"]);
        else lbl_tot_marks.Text = "";

    }
}