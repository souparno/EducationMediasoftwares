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
    public partial class frm_master_Group : Form
    {

        static class_Application ob;
        string group_code;
        

        public frm_master_Group()
        {
            InitializeComponent();
        }


       

        private void frm_sub_group_Load(object sender, EventArgs e)
        {
           ob = new class_Application();
           fill_grid();

       }

        private void fill_grid()
        {
            textBox2.Text = "";
            class_Application.flag = 1;

            string s = "select group_code,group_name from group_master";
            DataSet ds = ob.fill_data_set(s);
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;

            dataGridView1.Columns[0].HeaderText = "GROUP CODE";
            dataGridView1.Columns[1].HeaderText = "GROUP NAME";

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
        }

        //--save button click event---------------------------------->
        private void button2_Click(object sender, EventArgs e)
        {

            if (class_Application.flag == 1 && ! textBox2.Text.Equals(""))
            {
                string s = textBox2.Text.Substring(0, 1);
                string query = "SELECT top 1 group_code FROM group_master WHERE group_name Like '" + s + "%' order by group_code desc;";
                ob.getcode(s, query);
                group_code = class_Application.code;
                s = null;
                s = "insert into group_master(group_code,group_name) values('" + group_code.ToUpper() + "','" + textBox2.Text.ToUpper() + "')";
                ob.execute_non_query(s);
                
            }
            else if (class_Application.flag == 2 && !textBox2.Text.Equals(""))
            {
                string s = "update group_master set group_name = '"+ textBox2.Text.ToUpper() +"' where group_code='" + group_code + "'";
                ob.execute_non_query(s);

            }
            fill_grid();
           
        }
        //--button click event for the new button--------------------->
        private void btnNew_Click(object sender, EventArgs e)
        {
            fill_grid();
        }
        //--button click event for the delete button------------------>
        private void button3_Click(object sender, EventArgs e)
        {
            string s = "delete from group_master where group_code='"+ group_code +"'";
            ob.execute_non_query(s);
            fill_grid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            class_Application.flag = 2;
            int row = dataGridView1.CurrentCell.RowIndex;
            group_code  = Convert.ToString(dataGridView1.Rows[row].Cells[0].Value);
            textBox2.Text = Convert.ToString(dataGridView1.Rows[row].Cells[1].Value);
        }


    }
}
