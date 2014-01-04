using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Account_Admin_CreateSubject2 : System.Web.UI.Page
{
    string _connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        _connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        if (!IsPostBack)
        {
            fillsubjectgrid();
        }

    }


    private void fillsubjectgrid()
    {
        SqlConnection con = new SqlConnection(_connection);
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("select subject_id,subject_name from oes_subject_master order by subject_id;", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        grd_subject.DataSource = dt;
        grd_subject.DataBind();
    }


    protected void grd_subject_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grd_subject.Rows[e.RowIndex];
        string sql = "delete from oes_subject_master where subject_id='" + grd_subject.DataKeys[e.RowIndex].Value + "'";
        SqlConnection con = new SqlConnection(_connection);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();
        fillsubjectgrid();
    }

    protected void grd_subject_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd_subject.EditIndex = e.NewEditIndex;
        fillsubjectgrid();
    }

    protected void grd_subject_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)grd_subject.Rows[e.RowIndex];
        TextBox subject_name = (TextBox)row.FindControl("txt_subject_name_edit");
        string sql = "update oes_subject_master set subject_name='" + subject_name.Text + "' where subject_id='" + grd_subject.DataKeys[e.RowIndex].Value + "'";
        SqlConnection con = new SqlConnection(_connection);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();
        grd_subject.EditIndex = -1;
        fillsubjectgrid();
    }
    protected void grd_subject_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_subject.EditIndex = -1;
        fillsubjectgrid();
    }
    protected void btn_create_subject_Click(object sender, EventArgs e)
    {
        string sql = "select ISNULL(max(subject_id),0)+1 as 'id' from oes_subject_master;";
        SqlConnection con = new SqlConnection(_connection);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        string subject_id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();

        string subject_name = Txt_subject_name.Text;
        sql = "insert into oes_subject_master(subject_id,subject_name) values('" + subject_id + "','" + subject_name + "')";
        con.Open();
        cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();

        fillsubjectgrid();
    }
}