using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class Account_Admin_CreateTest2 : System.Web.UI.Page
{
    string _connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        _connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        if (!IsPostBack)
        {
            filltestgrid();
            fillsubjgrid();
        }
    }

    private void filltestgrid()
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(_connection);
        string sql = "select test_id,test_name,test_time_sec,test_active from oes_test_master;";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(dt);
        grd_test.DataSource = dt;
        grd_test.DataBind();
    }

    private void fillsubjgrid()
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(_connection);
        string sql = "select subject_id,subject_name from oes_subject_master;";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(dt);
        grd_subject.DataSource = dt;
        grd_subject.DataBind();
    }



    protected void btn_create_test_Click(object sender, EventArgs e)
    {


        string test_name = Txt_test_name.Text;
        int test_time =  Convert.ToInt32(txt_test_time.Text)*60;
        int test_active =Convert.ToInt32(chk_test_active.Checked);

        string sql = "select ISNULL(max(test_id),0)+1 as 'id' from oes_test_master;";
        SqlConnection con = new SqlConnection(_connection);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        string test_id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();


        if (chk_test_active.Checked)
        {
            sql = "update oes_test_master set test_active='0'";
            con = new SqlConnection(_connection);
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }


        sql = "insert into oes_test_master(test_id,test_name,test_time_sec,test_active) values('"+test_id+"','"+test_name+"','"+ test_time +"','"+test_active +"')";
        con = new SqlConnection(_connection);
        con.Open();
        cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();






        foreach (GridViewRow row in grd_subject.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chk_subject");
            if (chk.Checked)
            {

                sql = "insert into oes_test_subject_master(test_id,subject_id) values('" + test_id + "','" + grd_subject.DataKeys[row.RowIndex].Value + "')";
                con = new SqlConnection(_connection);
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();


            }
        }



        filltestgrid();

        

    }
    protected void grd_test_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd_test.EditIndex = e.NewEditIndex;
        filltestgrid();


        string test_id = Convert.ToString(grd_test.DataKeys[e.NewEditIndex].Value);
        GridViewRow row = (GridViewRow)grd_test.Rows[e.NewEditIndex];
        GridView subjectgrid = (GridView)row.FindControl("grd_edit_test_subject");
        SqlConnection con = new SqlConnection(_connection);
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("select subject_id,subject_name from oes_subject_master order by subject_id;", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        subjectgrid.DataSource = dt;
        subjectgrid.DataBind();
        chk_test_subjects(test_id, subjectgrid);
    }


    private void chk_test_subjects(string test_id, GridView edit_test_subject)
    {
        foreach (GridViewRow row in edit_test_subject.Rows)
        {
            string subject_id = Convert.ToString(edit_test_subject.DataKeys[row.RowIndex].Value);
            string sql = "select COUNT(*) as present from oes_test_subject_master where test_id='" + test_id + "' and subject_id='" + subject_id + "';";
            SqlConnection con = new SqlConnection(_connection);
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int present = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            if (present == 1)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk_sbj_edit");
                chk.Checked = true;
            }

        }
    }


    protected void grd_test_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_test.EditIndex = -1;
        filltestgrid();
    }
    protected void grd_test_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)grd_test.Rows[e.RowIndex];
        TextBox test_name = (TextBox)row.FindControl("txt_test_name_edit");
        TextBox test_time = (TextBox)row.FindControl("txt_test_time_edit");
        CheckBox test_active = (CheckBox)row.FindControl("chk_active_edit");


        string sql = "";
        SqlConnection con;
        SqlCommand cmd;


        if (test_active.Checked)
        {
            sql = "update oes_test_master set test_active='0'";
            con = new SqlConnection(_connection);
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        sql = "update oes_test_master set test_name='" + test_name.Text + "',test_time_sec='"+ Convert.ToInt32(test_time.Text)*60 +"',test_active='"+Convert.ToInt32(test_active.Checked)+"' where test_id='" + grd_test.DataKeys[e.RowIndex].Value + "'";
        con = new SqlConnection(_connection);
        con.Open();
        cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();




        grd_test.EditIndex = -1;
        filltestgrid();
    }

    protected void grd_test_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grd_test.Rows[e.RowIndex];
        string sql = "delete from oes_test_master where test_id='" + grd_test.DataKeys[e.RowIndex].Value + "'";
        SqlConnection con = new SqlConnection(_connection);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();
        filltestgrid();
    }
}