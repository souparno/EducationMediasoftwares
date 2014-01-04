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
    public partial class Frm_master_exam : Form
    {
        String s;
        String Exam_code;
        DataSet ds;
        static class_Application ob;
        int row;
        string query;

        public Frm_master_exam()
        {
            InitializeComponent();
        }


        //-----form load event---->
        private void Frm_test_type_Load(object sender, EventArgs e)
        {

            ob = new class_Application();
            fill_grid();
        }


        //---function for filling up the datagridview-->
        public void  fill_grid()
        {
            row = 0;
            textBox2.Text = null;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            class_Application.flag = 1;
            ds = new DataSet();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("Examcode", "Exam Code");
            dataGridView1.Columns.Add("ExamName", "Exam Name");
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk);
            dataGridView1.Columns[2].Name = "Skip";
            dataGridView1.Columns[2].HeaderText = "SKIP";
            DataGridViewCheckBoxColumn chk2 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk2);
            dataGridView1.Columns[3].Name = "Negetive";
            dataGridView1.Columns[3].HeaderText = "NEGETIVE";

                                   
                s = null;
                s = "select exam_code,exam_name,can_skip,negetive_marking from exam_master;";
                //--loadingthe datagrid view with the dataset--->
                ds = ob.fill_data_set(s);

                int i = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.Rows.Add(ds.Tables[0].Rows.Count);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dataGridView1.Rows[i].Cells["ExamCode"].Value = dr["exam_code"];
                        dataGridView1.Rows[i].Cells["ExamName"].Value = dr["exam_name"];
                        dataGridView1.Rows[i].Cells["Skip"].Value = Convert.ToInt32(dr["can_skip"]);
                        dataGridView1.Rows[i].Cells["Negetive"].Value = Convert.ToInt32(dr["negetive_marking"]);
                        i++;
                    }

                   
                }
                //---assigning the default cell fill type property for the datagridveiw1--->
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
                dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
                dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
                dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
          
          }



   
        

 
        //click event for the datagrid view1-->
        private void dataGridView1_Click(object sender, EventArgs e)
        {   class_Application.flag = 2;
            row = dataGridView1.CurrentCell.RowIndex;
            Exam_code  = Convert.ToString(dataGridView1.Rows[row].Cells[0].Value);
            textBox2.Text = Convert.ToString(dataGridView1.Rows[row].Cells[1].Value);
            checkBox1.Checked = Convert.ToBoolean(dataGridView1.Rows[row].Cells[2].Value);
            checkBox2.Checked = Convert.ToBoolean(dataGridView1.Rows[row].Cells[3].Value);
       }




        //---button click event for the new button--->
        private void btnNew_Click(object sender, EventArgs e)
        {  fill_grid();
        }
        //--button click event for the save button-->
        private void button1_Click(object sender, EventArgs e)
        {
           if (class_Application.flag == 1 && textBox2.Text !="" )
            {
                s = null;
                s = "insert into exam_master(exam_code,exam_name,can_skip,negetive_marking) values('" + Exam_code.ToUpper() + "','" + textBox2.Text.ToUpper() + "'," + Convert.ToInt32(checkBox1.Checked) + ",'" + Convert.ToInt32(checkBox2.Checked) + "');";
                ob.execute_non_query(s);
                fill_grid();
            } //---condition for updating the exam master----->
            else if(class_Application.flag==2 && textBox2.Text!=null )
            {
                s = null;
                s = "update exam_master set exam_name='" + textBox2.Text.ToUpper() + "',can_skip=" + Convert.ToInt32(checkBox1.Checked) + ",negetive_marking=" + Convert.ToInt32(checkBox2.Checked) + " where exam_code='" + Exam_code.ToUpper() + "'";
                ob.execute_non_query(s);
                fill_grid();
           }
       }  //---code for the delete button-->
        private void button3_Click(object sender, EventArgs e)
        {           s = null;
                    s = "delete from exam_master where exam_code='" + Convert.ToString(dataGridView1.Rows[row].Cells[0].Value) + "';";
                    ob.execute_non_query(s);
                    fill_grid();
         }
     
     
       

   


        //text change event for the exam name adding text box-->
        private void textBox2_TextChanged(object sender, EventArgs e)
        {      if (class_Application.flag == 1 && textBox2.Text.Length >= 1)
                {   s = textBox2.Text.Substring(0, 1);
                    query = "SELECT top 1 Exam_Code FROM Exam_Master WHERE Exam_Name Like '" + s + "%' order by Exam_Code desc;";
                    ob.getcode(s, query);
                    Exam_code  = class_Application.code;
                }
                else if (class_Application.flag == 1 && textBox2.Text.Length < 1)
                {
                    textBox2.Text = null;
                }
       }
        //---disabeling the user from changing the exam code--->
         private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {  e.Handled = true;
        }

  

    }
}
