using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Account_Admin_RegisterStudents : System.Web.UI.Page
{
    string _connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        _connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        if (!IsPostBack)
        {
            fillstudentgrid();
        }

    }


    private void fillstudentgrid()
    {
        DataTable dt = new DataTable();
        string sql = "select usr_id,usr_name,usr_contact,usr_code,usr_password from oes_user_master where usr_type='2';";
        SqlConnection con = new SqlConnection(_connection);
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        da.Fill(dt);

        grd_students.DataSource = dt;
        grd_students.DataBind();

    }


    protected void btn_register_Click(object sender, EventArgs e)
    {
        string name = txt_name.Text;
        string contact = txt_contact.Text;
        string password = txt_password.Text;


        string sql = "select ISNULL(max(usr_id),0)+1 as 'id' from oes_user_master;";
        SqlConnection con = new SqlConnection(_connection);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        string id = Convert.ToString(cmd.ExecuteScalar());
        con.Close();


        sql = "insert into oes_user_master(usr_id,usr_name,usr_contact,usr_code,usr_password,usr_type) values('"+id+"','"+name+"','"+contact+"','"+generate_user_code()+"','"+password+"','2')";
        con.Open();
        cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();

        fillstudentgrid();

    }
    protected void grd_students_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd_students.EditIndex = e.NewEditIndex;
        fillstudentgrid();
    }

    protected void grd_students_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)grd_students.Rows[e.RowIndex];
        TextBox student_name = (TextBox)row.FindControl("txt_usr_name");
        TextBox student_contact = (TextBox)row.FindControl("txt_usr_contact");
        TextBox student_email = (TextBox)row.FindControl("txt_usr_email");
        TextBox student_pass = (TextBox)row.FindControl("txt_usr_pass");


        string sql = "update oes_user_master set usr_name='" + student_name.Text + "',usr_contact='"+student_contact.Text+"',usr_email='"+student_email.Text+"',usr_password='"+student_pass.Text+"' where usr_id='" + grd_students.DataKeys[e.RowIndex].Value + "'";
        SqlConnection con = new SqlConnection(_connection);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();
        grd_students.EditIndex = -1;
        fillstudentgrid();
    }
    protected void grd_students_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd_students.EditIndex = -1;
        fillstudentgrid();
    }
    protected void grd_students_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)grd_students.Rows[e.RowIndex];
        string sql = "delete from oes_user_master where usr_id='" + grd_students.DataKeys[e.RowIndex].Value + "'";
        SqlConnection con = new SqlConnection(_connection);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        cmd.ExecuteNonQuery();
        con.Close();
        fillstudentgrid();
    }


    private string generate_user_code()
    {
        string user_code = "";
        string code = "";
        SqlConnection con = new SqlConnection(_connection);
        string sql = "select usr_code from oes_user_master order by usr_code desc;";
        SqlCommand cmd = new SqlCommand(sql, con);
        con.Open();
        code = Convert.ToString(cmd.ExecuteScalar());
        con.Close();



        if (code == null || code=="")
        {
            user_code = "U0000001";
        }else{
                int i;
                for (i = 1; i < code.Length; i++)
                {
                    if (code[i] != '0')
                    {
                        break;
                    }
                }
                int number = Convert.ToInt32(code.Substring(i)) + 1;
                user_code = code.Substring(0, i).ToUpper() + Convert.ToString(number);
                if (user_code.Length > 8)
                {
                    user_code = code.Substring(0, i - 1).ToUpper() + Convert.ToString(number);
                }
        }

        return user_code;

    }


}