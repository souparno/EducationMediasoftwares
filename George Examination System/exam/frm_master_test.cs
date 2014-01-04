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
    public partial class frm_master_test : Form
    {

      
        String s;
        string exam_code;
        DataSet ds;
        static class_Application ob;
        int row;
        string query;
        string test_code;




        public frm_master_test()
        {
            InitializeComponent();
        }

        private void frm_create_test_Load(object sender, EventArgs e)
        {
            s=null;
            ob=new class_Application();
            s = "select exam_code,exam_name from exam_master;";
            ob.fill_combo_box(comboBox1, s, "exam_name");
         }



        private void fill_grid()
        {
            row = 0;
            textBox2.Text = null;
            textBox3.Text = null;
            class_Application.flag = 1;
            ds = new DataSet();

            
            //--filling the datagridview--->
            s = null;
            s = "select test_code,test_name,test_duration from test_master where exam_code='"+ exam_code +"';";
            //--loadingthe datagrid view with the dataset--->
            ds = ob.fill_data_set(s);
            dataGridView1.DataSource = ds.Tables[0];
            //---assigning the default cell fill type property for the datagridveiw1--->
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[0].HeaderText = "Test Code";
            dataGridView1.Columns[1].HeaderText = "Test Name";
            dataGridView1.Columns[2].HeaderText = "Test Duration";
        }



   

     
        //--code for not accepting any entry in the test number allocation text box-->
        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        //--text change event for the test name text box--->
        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (class_Application.flag == 1 && textBox2.Text.Length >= 1)
            {
                s = textBox2.Text.Substring(0, 1);
                query = "SELECT top 1 test_code FROM test_master WHERE test_name Like '" + s + "%' order by test_code desc;";
                ob.getcode(s, query);
                test_code = class_Application.code;
            }
            else if (class_Application.flag == 1 && textBox2.Text.Length < 1)
            {
                test_code = null;
                textBox2.Text = null;
                textBox3.Text = null;
            }
        }


        //----selected index change for the combobox-------->
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataRowView dr=(DataRowView)comboBox1.SelectedItem;
            exam_code = Convert.ToString(dr["exam_code"]);
            fill_grid();

        }



        //---button click event for the save button--->
        private void button1_Click(object sender, EventArgs e)
        {
            if (class_Application.flag == 1 && textBox2.Text != null && textBox3.Text != null)
            {
                s = null;
                s = "insert into test_master(exam_code,test_code,test_name,test_duration) values('" + exam_code.ToUpper() + "','" + test_code.ToUpper() + "','" + textBox2.Text.ToUpper() + "','"+ textBox3.Text.ToUpper()+"');";
                ob.execute_non_query(s);
                fill_grid();

            }
            else if (class_Application.flag == 2 && textBox2.Text != null && textBox3.Text !=null)
            {
                s = null;
                s = "update test_master set test_name='"+ textBox2.Text.ToUpper() +"',test_duration ='"+ textBox3.Text.ToUpper() +"' where exam_code='"+ exam_code.ToUpper() +"' and test_code='"+ test_code.ToUpper() +"'";
                ob.execute_non_query(s);
                fill_grid();
            }


            if(textBox2.Text==null ){
                MessageBox.Show("Please fill in a test name");
            }
            if (textBox3.Text == null)
            {
                MessageBox.Show("Please fill in the time in minutes");
            }

        }
        //---butotn click event for the new butotn-->
        private void btnNew_Click(object sender, EventArgs e)
        {
            fill_grid();
        }
        //--button click event for the delete button-->
        private void button3_Click(object sender, EventArgs e)
        {   s = null;
            s = "delete from test_master where exam_code='"+exam_code +"' and test_code='"+ test_code.ToUpper() +"'";
            ob.execute_non_query(s);
            fill_grid();
         }





        //--datagridview1 click event-------------->
        private void dataGridView1_Click(object sender, EventArgs e)
        {   class_Application.flag = 2;
            row = dataGridView1.CurrentCell.RowIndex;
            test_code = Convert.ToString(dataGridView1.Rows[row].Cells[0].Value);
            textBox2.Text = Convert.ToString(dataGridView1.Rows[row].Cells[1].Value);
            textBox3.Text = Convert.ToString(dataGridView1.Rows[row].Cells[2].Value);
        }
     

     

    

     
    }
}
