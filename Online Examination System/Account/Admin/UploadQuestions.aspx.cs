using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Data.SqlClient;



public partial class Account_Admin_UploadQuestions : System.Web.UI.Page
{

    string _connection;

    protected void Page_Load(object sender, EventArgs e)
    {
        _connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        if (!IsPostBack)
        {
            fill_question_grid();
            fill_ddl_test();
            fill_ddl_subject();
        }

        else
        {

            fill_ddl_subject();
        }


    }

  


    private void fill_ddl_test()
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(_connection);
        string sql = "select test_id,test_name from oes_test_master;";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(dt);
        ddl_test.DataSource = dt;
        ddl_test.DataValueField = "test_id";
        ddl_test.DataTextField = "test_name";
        ddl_test.DataBind();
        
    }

    private void fill_ddl_subject()
    {

        string subject_id = ddl_subject.SelectedValue;
        
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(_connection);
            string sql = "select oes_subject_master.subject_id,subject_name " +
                "from oes_test_subject_master join oes_subject_master on " +
                "oes_subject_master.subject_id =oes_test_subject_master.subject_id " +
                "where test_id='" + ddl_test.SelectedValue + "';";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dt);
            


            try{
               ddl_subject.DataSource = dt;
               ddl_subject.DataValueField = "subject_id";
               ddl_subject.DataTextField = "subject_name";
               ddl_subject.SelectedValue = subject_id;
               ddl_subject.DataBind();
            }catch(Exception e) {};
            
            
            
            
            ViewState["test_id"] = ddl_test.SelectedValue;
            ViewState["subject_id"] = ddl_subject.SelectedValue;
    }


    private void fill_question_grid()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("question_no", typeof(string));
        dt.Columns.Add("question", typeof(string));
        dt.Columns.Add("answer1", typeof(string));
        dt.Columns.Add("answer2", typeof(string));
        dt.Columns.Add("answer3", typeof(string));
        dt.Columns.Add("answer4", typeof(string));
        dt.Columns.Add("answer5", typeof(string));
        dt.Columns.Add("correct_option", typeof(string));
        dt.Columns.Add("posmarks", typeof(string));
        dt.Columns.Add("negmarks", typeof(string));

        DataRow row = dt.NewRow();
        row["question_no"] = "";
        row["question"] = "Your Questions Will Be Shown As Soon As You Upload The Excel Sheet";
        row["answer1"] = "";
        row["answer2"] = "";
        row["answer3"] = "";
        row["answer4"] = "";
        row["answer5"] = "";
        row["correct_option"] = "";
        row["posmarks"] = "";
        row["negmarks"] = "";
        dt.Rows.Add(row);

        grd_questions.DataSource = dt;
        grd_questions.DataBind();
        
    }



    protected void btn_upload_Click(object sender, EventArgs e)
    {

        string connectionString = "";
        if (q_upload.HasFile)
        {
            string fileName = Path.GetFileName(q_upload.PostedFile.FileName);
            string fileExtension = Path.GetExtension(q_upload.PostedFile.FileName);
            string fileLocation = Server.MapPath("~/App_Data/" + fileName);
            q_upload.SaveAs(fileLocation);
            if (fileExtension == ".xls")
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                  fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (fileExtension == ".xlsx")
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                  fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            ViewState["connectionstring"] = connectionString;

            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
            DataTable dtExcelRecords = new DataTable();
            con.Open();
            DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            ddl_excel_sheets.DataSource = dtExcelSheetName;
            ddl_excel_sheets.DataValueField = "TABLE_NAME";
            ddl_excel_sheets.DataTextField = "TABLE_NAME";
            ddl_excel_sheets.DataBind();
            con.Close();
            
            
            
        }

    }

    protected void btn_viewquestion_Click(object sender, EventArgs e)
    {
        DataTable dtExcelRecords = GetExcelTableRecords();
        grd_questions.DataSource = dtExcelRecords;
        grd_questions.DataBind();
    }




    private DataTable GetExcelTableRecords()
    {
        string connectionString = Convert.ToString(ViewState["connectionstring"]);
        string getExcelSheetName = ddl_excel_sheets.SelectedValue;
        OleDbConnection con = new OleDbConnection(connectionString);
        OleDbCommand cmd = new OleDbCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = con;
        OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
        DataTable dtExcelRecords = new DataTable();
        cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
        dAdapter.SelectCommand = cmd;
        dAdapter.Fill(dtExcelRecords);
       



        DataTable dt = new DataTable();
        dt.Columns.Add("question_no", typeof(string));
        dt.Columns.Add("question",typeof(string));
        dt.Columns.Add("answer1",typeof(string));
        dt.Columns.Add("answer2",typeof(string));
        dt.Columns.Add("answer3", typeof(string));
        dt.Columns.Add("answer4",typeof(string));
        dt.Columns.Add("answer5", typeof(string));
        dt.Columns.Add("correct_option", typeof(string));
        dt.Columns.Add("posmarks",typeof(string));
        dt.Columns.Add("negmarks", typeof(string));

        foreach (DataRow dr in dtExcelRecords.Rows)
        {
            try
            {
                int question_n0 = Convert.ToInt32(dr[0]);
                if (question_n0 != 0)
                {

                    DataRow row = dt.NewRow();
                    row["question_no"] = dr[0];
                    row["question"] = dr[1];
                    row["answer1"] = dr[2];
                    row["answer2"] = dr[3];
                    row["answer3"] = dr[4];
                    row["answer4"] = dr[5];
                    row["answer5"] = dr[6];
                    row["posmarks"] = dr[7];
                    row["negmarks"] = dr[8];
                    row["correct_option"] = dr[9];
                    dt.Rows.Add(row);
                }
            }catch(Exception){};
        }
        return dt;

    }

    protected void btn_save_Click(object sender, EventArgs e)
    {

        
        string test_id =    Convert.ToString(ViewState["test_id"]);
        string subject_id = Convert.ToString(ViewState["subject_id"]);

        SqlConnection con = new SqlConnection(_connection);

        try
        {


            foreach (GridViewRow row in grd_questions.Rows)
            {
                string question_no = ((TextBox)row.FindControl("txt_question_no")).Text;
                string question = ((TextBox)row.FindControl("txt_question")).Text;
                string answer1 = ((TextBox)row.FindControl("txt_answer1")).Text;
                string answer2 = ((TextBox)row.FindControl("txt_answer2")).Text;
                string answer3 = ((TextBox)row.FindControl("txt_answer3")).Text;
                string answer4 = ((TextBox)row.FindControl("txt_answer4")).Text;
                string answer5 = ((TextBox)row.FindControl("txt_answer5")).Text;
                string pos_marks = ((TextBox)row.FindControl("txt_posmarks")).Text;
                string neg_marks = ((TextBox)row.FindControl("txt_negmarks")).Text;
                string correct_option = ((TextBox)row.FindControl("txt_correct_option")).Text;


                string image_name = "";
                int image_present = 0;
                FileUpload img = (FileUpload)row.FindControl("upload_image");
                if (img.HasFile)
                {
                    string path = Server.MapPath("..//Test//q_images//");
                    img.SaveAs(path + img.FileName);
                    image_name = img.FileName;
                    image_present = 1;

                }


                string sql = "select ISNULL(max(question_id),0)+1 as 'id' from oes_question_master;";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                string question_id = Convert.ToString(cmd.ExecuteScalar());
                con.Close();


                sql = "INSERT INTO [oes].[dbo].[oes_question_master] ([subject_id],[test_id],[question_id],[question],[question_img_name],[answer1],[answer2],[answer3],[answer4],[answer5],[pos_marks],[neg_marks],[question_no],[image_present],[correct_option])" +
                    "VALUES ('" + subject_id + "','" + test_id + "','" + question_id + "','" + question.Replace("'", "''") + "','" + image_name + "','" + answer1.Replace("'", "''") + "','" + answer2.Replace("'", "''") + "','" + answer3.Replace("'", "''") + "','" + answer4.Replace("'", "''") + "','" + answer5.Replace("'", "''") + "','" + pos_marks + "','" + neg_marks + "','" + question_no + "','" + image_present + "','" + correct_option + "');";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            fill_question_grid();
            ClientScript.RegisterStartupScript(typeof(Page), "SymbolError", "<script typr='text/javascript'>alert('Question has been uploaded Successfully')</script>");

        }
        catch(SqlException ex) {

            con.Close();
            ClientScript.RegisterStartupScript(typeof(Page), "SymbolError", "<script typr='text/javascript'>alert('could not upload questions :-"+ex+"')</script>");
        }
    }
    protected void ddl_subject_TextChanged(object sender, EventArgs e)
    {
        ViewState["subject_id"] = ddl_subject.SelectedValue;
    }
}