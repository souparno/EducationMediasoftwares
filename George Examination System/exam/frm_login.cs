using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace exam
{
    public partial class frm_login : Form
    {
        //---declaring an object o fthe class application---->
        static class_Application ob;
        
        public frm_login()
        {
            InitializeComponent();
        }



        //---button click event for successful login--->
        private void button1_Click(object sender, EventArgs e)
        {
            string s;
            s = null;
            DataSet ds = new DataSet();
            ob = new class_Application();
            s = "SELECT User_Access.User_ID,user_name,user_role FROM User_Access where user_name='"+ textBox1.Text.ToUpper() +"' and user_password='"+ textBox2.Text +"' and is_active=1;";
            ds = ob.fill_data_set(s);

         


  


            //---elaborate login code will go in here----->
            if (ds.Tables[0].Rows.Count>0 && Convert.ToString(ds.Tables[0].Rows[0][2]).Equals("ADMIN"))
            {   class_Application.user_id = Convert.ToString(ds.Tables[0].Rows[0][0]);
                class_Application.user_name = Convert.ToString(ds.Tables[0].Rows[0][1]);
                class_Application.parent_form.enable_admin_menu();
                this.Close();
            }

               

            else if (ds.Tables[0].Rows.Count > 0 && Convert.ToString(ds.Tables[0].Rows[0][2]).Equals("USER"))
            {
                class_Application.user_id = Convert.ToString(ds.Tables[0].Rows[0][0]);
                class_Application.user_name = Convert.ToString(ds.Tables[0].Rows[0][1]);
                class_Application.parent_form.enable_user_menu();
                this.Close();
            }


            else
            {
                MessageBox.Show("Please check your login id or password");
            }
        }

        private void frm_login_Load(object sender, EventArgs e)
        {
            class_Application.parent_form.disable_menu();
        }
    }
}
