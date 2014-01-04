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
    public partial class frm_create_user : Form
    {


        String s;
    
        DataSet ds;
        static class_Application ob;
        int row;
        string query;
        String user_id;



        string user_role;

        public frm_create_user()
        {
            InitializeComponent();
        }



        private void frm_create_user_Load(object sender, EventArgs e)
        {

            ob = new class_Application();
            fill_grid();
        }


        public void fill_grid()
        {
            row = 0;
            textBox1.Text = null;
            textBox2.Text = null;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            class_Application.flag = 1;
            ds = new DataSet();
            
            s = null;
            s = "select user_id,user_name,user_password,user_role,is_active from user_access;";
            ds = ob.fill_data_set(s);
                        
         
            dataGridView1.Columns.Clear();
            DataGridViewCheckBoxColumn ob1=new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add("user_id","USER ID");
            dataGridView1.Columns.Add("user_name","USER NAME");
            dataGridView1.Columns.Add("password","PASSWORD");
            dataGridView1.Columns.Add("user_role","USER ROLE");
            dataGridView1.Columns.Add(ob1);
            dataGridView1.Columns[4].HeaderText = "ACTIVE";
            dataGridView1.Columns[4].Name = "is_active";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[4].ReadOnly = true;

            dataGridView1.Rows.Add(ds.Tables[0].Rows.Count);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {   dataGridView1.Rows[i].Cells["user_id"].Value = ds.Tables[0].Rows[i]["user_id"];
                dataGridView1.Rows[i].Cells["user_name"].Value = ds.Tables[0].Rows[i]["user_name"];
                dataGridView1.Rows[i].Cells["password"].Value = ds.Tables[0].Rows[i]["user_password"];
                dataGridView1.Rows[i].Cells["user_role"].Value = ds.Tables[0].Rows[i]["user_role"];
                dataGridView1.Rows[i].Cells["is_active"].Value = ds.Tables[0].Rows[i]["is_active"];
                
            }






        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            class_Application.flag = 2;
            row = dataGridView1.CurrentCell.RowIndex;
            user_id = Convert.ToString(dataGridView1.Rows[row].Cells["user_id"].Value);
            textBox2.Text = Convert.ToString(dataGridView1.Rows[row].Cells["user_name"].Value);
            textBox1.Text = Convert.ToString(dataGridView1.Rows[row].Cells["password"].Value);
            checkBox1.Checked=Convert.ToBoolean(dataGridView1.Rows[row].Cells["is_active"].Value);
            if (Convert.ToString(dataGridView1.Rows[row].Cells["user_role"].Value).Equals("ADMIN"))
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {         
            fill_grid(); ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (class_Application.user_id == user_id)
            {
                MessageBox.Show("YOU CANNOT DELETE YOUR OWN ACCOUNT");
            }
            else
            {
                if (Convert.ToString(dataGridView1.Rows[row].Cells["user_id"].Value).Equals("A00001"))
                {
                    MessageBox.Show("System Admin cannot be deleted");
                }
                else
                {
                    s = " delete from user_access where user_id='" + user_id + "';";

                    ob.execute_non_query(s);

                }
            }
            fill_grid();

        }

        private void button1_Click(object sender, EventArgs e)
        {

                if(checkBox2.Checked==true){
                user_role = "ADMIN";}
                else if(checkBox2.Checked==false){
                user_role = "USER";}


            if( class_Application.flag==1 && textBox1.Text !=null && textBox2.Text !=null){

            s = null;
            s = textBox2.Text.Substring(0, 1);
            
            query = null;
            query = "SELECT top 1 Exam_Code FROM Exam_Master WHERE Exam_Name Like '" + s + "%' order by Exam_Code desc;";
            ob.getcode(s, query);
            user_id = class_Application.code;
                   
            
        

            s = "insert into user_access(user_id,user_name,user_role,is_active,user_password) values('" + user_id + "','" + textBox2.Text.ToUpper() + "','" + user_role + "'," + Convert.ToString(Convert.ToInt32(checkBox1.Checked)) + ",'"+ textBox1.Text +"');";
       
            }
            else if (class_Application.flag==2 && textBox1.Text !=null && textBox2.Text !=null && user_id!="A00001")
            {
                s = "update user_access set user_name='"+ textBox2.Text.ToUpper() +"',user_password='"+ textBox1.Text +"',is_active=" + Convert.ToString(Convert.ToInt32(checkBox1.Checked)) + ",user_role='"+ user_role +"' where user_id='"+ user_id +"'";

            }
            else if (class_Application.flag == 2 && textBox1.Text != null && textBox2.Text != null && user_id == "A00001")
            {
                s = "update user_access set user_password='" + textBox1.Text + "' where user_id='" + user_id + "'";

            }

            ob.execute_non_query(s);

            fill_grid();



        }

  






    }
}
