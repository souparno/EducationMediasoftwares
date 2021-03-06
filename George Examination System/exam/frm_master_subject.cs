﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace exam
{
    public partial class frm_master_subject : Form
    {

        String s;
        string exam_code;
        DataSet ds;
        static class_Application ob;
        int row;
        string query;
        string  sub_number;
        string sub_code;


        public frm_master_subject()
        {
            InitializeComponent();
        }

        private void frm_subject_master_Load(object sender, EventArgs e)
        {
            exam_code = null;
            ob = new class_Application();
            //---filling the combobox1-->
            s = "select exam_code,exam_name from exam_master;";
            ob.fill_combo_box(comboBox1, s, "exam_name");



        }

        //---function for filling the datagrid view-->
        public void fill_grid()
        {


            comboBox2.Items.Clear();            
            comboBox2.Items.Add("QUANT");
            comboBox2.Items.Add("VERBAL");
            comboBox2.Items.Add("OTHER");
            comboBox2.Text = "select a group name";


            row = 0;
            textBox2.Text = null;
            class_Application.flag = 1;
            ds = new DataSet();

            //--filling the datagridview--->
            s = null;
            s = "select sub_code,sub_no,sub_name,sub_group from subject_master where exam_code='" + exam_code + "';";
            //--loadingthe datagrid view with the dataset--->
            ds = ob.fill_data_set(s);
            dataGridView1.DataSource = ds.Tables[0];
            //---assigning the default cell fill type property for the datagridveiw1--->
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[0].HeaderText = "subject Code";
            dataGridView1.Columns[1].HeaderText = "subject no.";
            dataGridView1.Columns[2].HeaderText = "subject Name";
            dataGridView1.Columns[3].HeaderText = "subject Group";
        }



        //---click event for the datagrid view1--->
        private void dataGridView1_Click(object sender, EventArgs e)
        {   class_Application.flag = 2;
            row = dataGridView1.CurrentCell.RowIndex;
            sub_code  = Convert.ToString(dataGridView1.Rows[row].Cells[0].Value);
            textBox2.Text = Convert.ToString(dataGridView1.Rows[row].Cells[2].Value);
            sub_number = Convert.ToString(dataGridView1.Rows[row].Cells[1].Value);
         }





        //---button click event for the new button--->
        private void btnNew_Click(object sender, EventArgs e)
        { fill_grid();
        }
        //---button click event for the save button-->
        private void button1_Click(object sender, EventArgs e)
        {           if (class_Application.flag == 1 && textBox2.Text != "" )
                    {   //---getting the subject number for the subject to be inserted--->
                        s = null;
                        s = "SELECT IIf( IsNull (Max (sub_no) ) ,0 , Max(sub_No) )+1 AS subject_number FROM subject_master where exam_code='"+ exam_code +"';";
                        sub_number = ob.execute_scalar(s);
                        //--inserting the new subject--->
                        s = null;
                        s = "insert into subject_master(sub_code,sub_name,exam_code,sub_no,sub_group) values('" + sub_code.ToUpper() + "','" + textBox2.Text.ToUpper() + "','"+ exam_code +"','"+ sub_number +"','"+ comboBox2.Text.ToUpper() +"');";
                        ob.execute_non_query(s);
                        fill_grid();

                    }
                    else if (class_Application.flag == 2 && textBox2.Text != null )
                    {
                        s = null;
                        s = "update subject_master set sub_name='" + textBox2.Text.ToUpper() + "',sub_group='"+ comboBox2.Text.ToUpper() +"' where sub_no='"+sub_number +"' and sub_code='" + sub_code.ToUpper() + "' and exam_code='"+ exam_code +"';";
                        ob.execute_non_query(s);
                        fill_grid();
                    }

            if(textBox2.Text==null ){
                MessageBox.Show("Please Enter a Subject Name");
            }

       
        }
        //--delete button code---->
        private void button3_Click(object sender, EventArgs e)
        {
            s = null;
            s = "delete from subject_master where sub_code='" + Convert.ToString(dataGridView1.Rows[row].Cells[0].Value) + "';";
            ob.execute_non_query(s);
            fill_grid();
        }
        


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)comboBox1.SelectedItem;
            exam_code=Convert.ToString(dr["exam_code"]);
            fill_grid();
        }




        //--------text change event for the text box 2---->
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (class_Application.flag == 1 && textBox2.Text.Length >= 1)
            {
                s = textBox2.Text.Substring(0, 1);
                query = "SELECT top 1 sub_code FROM subject_master WHERE sub_name Like '" + s + "%' order by sub_code desc;";
                ob.getcode(s, query);
                sub_code = class_Application.code;


            }
            else if (class_Application.flag == 1 && textBox2.Text.Length < 1)
            {               
                textBox2.Text = null;
            }
        }
        //---disabeling the user from changing the subject code--->
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        { e.Handled = true;
        }

      





    }
}
