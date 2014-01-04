using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;




public partial class Account_Admin_CreateQuestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillquestiongrid();
    }



    private void fillquestiongrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("question_id",typeof(int));
        
        DataRow dr = dt.NewRow();
        dr["question_id"] = 1;
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["question_id"] = 2;
        dt.Rows.Add(dr);


        grd_question.DataSource = dt;
        grd_question.DataBind();

    }
}