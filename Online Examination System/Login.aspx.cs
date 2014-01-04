using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    string _connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        _connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        string usr_id = txt_user_name.Text;
        string password = txt_password.Text;

        SqlConnection con = new SqlConnection(_connection);
        string sql = "select user_role,usr_id from oes_user_master join oes_user_role_master on oes_user_master.usr_type=oes_user_role_master.id where usr_code='"+usr_id+"' and usr_password='"+password+"'";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["user_role"]).Equals("ADMIN"))
        {
            Session["UserRole"]=dt.Rows[0]["user_role"];
            Session["UserId"] = dt.Rows[0]["usr_id"];
            Response.Redirect("~/Account/Admin/RegisterStudents.aspx");
        }
        else if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["user_role"]).Equals("STUDENT"))
        {
            Session["UserRole"] = dt.Rows[0]["user_role"];
            Session["UserId"] = dt.Rows[0]["usr_id"];
            Response.Redirect("~/Account/Student/TakeATest.aspx");
        }


    }
}