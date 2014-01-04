using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Account_Admin_TakeATest : System.Web.UI.Page
{
    string _connection;
    protected void Page_Load(object sender, EventArgs e)
    {
        _connection = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(_connection);
            string sql = "select test_id,test_name from oes_test_master where test_active='1';";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);


            string user_id = Convert.ToString(Session["UserId"]);


            if (dt.Rows.Count > 0)
            {

                String test_id = Convert.ToString(dt.Rows[0]["test_id"]);

                sql = "select COUNT(*) from oes_answer_sheet where usr_id='" + user_id + "' and test_id='" + test_id + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                int test_given = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();



                if (test_given > 0)
                {
                    lbl_test.Text = "NO TEST AVAILABLE";
                    btn_start_test.Visible = false;
                }
                else
                {
                    lbl_test.Text = "TEST AVAILABLE :- " + dt.Rows[0]["test_name"];
                    btn_start_test.Visible = true;
                }
            }
            else
            {
                lbl_test.Text = "NO TEST AVAILABLE";
                btn_start_test.Visible = false;
            }


        }

    }
    protected void btn_start_test_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Test/Test.aspx");
    }
}