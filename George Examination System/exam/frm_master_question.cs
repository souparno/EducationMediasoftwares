using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;
using System.Data.SqlClient;


namespace exam
{

    public partial class frm_master_question : Form
    {
        
        
        //---declaring the instance variables--->
        OleDbConnection MyConnection;
        DataTable dt;
        DataSet ds;
        string s;
        static class_Application ob;
        
        string exam_code;
        string test_code;
        string subject_code;

        
        public frm_master_question()
        {
            InitializeComponent();
        }

        //--assingning a string property to tthe variable examcode
        public string ExamCode
        {
            get { return exam_code; }
            set { exam_code = value; }
        }
        //--assigning a string property to a varibale testcode
        public string TestCode
        {
            get { return test_code; }
            set { test_code = value; }
        }
        //--assigning a string property to a variable subjectcode
        public string SubjectCode
        {
            get { return subject_code; }
            set { subject_code = value; }
        }
        //--publicising the lable for subject name-->
        public Label SubjectName
        {
            get { return LblSubjectName; }
            set { LblSubjectName = value; }

        }
        //---assigning public property to the datagridview--->
        public DataGridView Questiongrid
        {
            get { return dataGridView1; }
            set { dataGridView1 = value; }
        }





        //----form load event--->
        private void frm_create_questions_Load(object sender, EventArgs e)
        {
           
            ob = new class_Application();
            refresh_me();
      
        }

        private void refresh_me()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "--SELECT A SHEET NAME--";

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();


            dataGridView1.Columns.Add("q_no", "QUESTION NO.");
            dataGridView1.Columns.Add("q_name", "Q#");
            DataGridViewCheckBoxColumn chk1 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk1);
            dataGridView1.Columns.Add("ans1", "ANS1");
            DataGridViewCheckBoxColumn chk2 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk2);
            dataGridView1.Columns.Add("ans2", "ANS2");
            DataGridViewCheckBoxColumn chk3 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk3);
            dataGridView1.Columns.Add("ans3", "ANS3");
            DataGridViewCheckBoxColumn chk4 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk4);
            dataGridView1.Columns.Add("ans4", "ANS4");
            DataGridViewCheckBoxColumn chk5 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk5);
            dataGridView1.Columns.Add("ans5", "ANS5");
            DataGridViewCheckBoxColumn chk6 = new DataGridViewCheckBoxColumn();
            dataGridView1.Columns.Add(chk6);
            dataGridView1.Columns.Add("correct_option", "CORRECT OPTION");
            
          
            dataGridView1.Columns[2].HeaderText = "IMAGE PRESENT";
            dataGridView1.Columns[2].Name = "image1_present";
            dataGridView1.Columns[4].HeaderText = "IMAGE PRESENT";
            dataGridView1.Columns[4].Name = "image2_present";
            dataGridView1.Columns[6].HeaderText = "IMAGE PRESENT";
            dataGridView1.Columns[6].Name = "image3_present";
            dataGridView1.Columns[8].HeaderText = "IMAGE PRESENT";
            dataGridView1.Columns[8].Name = "image4_present";
            dataGridView1.Columns[10].HeaderText = "IMAGE PRESENT";
            dataGridView1.Columns[10].Name = "image5_present";
            dataGridView1.Columns[12].HeaderText = "IMAGE PRESENT";
            dataGridView1.Columns[12].Name = "image6_present";

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[6].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[7].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[8].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[9].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[10].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[11].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[12].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[13].SortMode = DataGridViewColumnSortMode.Programmatic;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[8].ReadOnly = true;
            dataGridView1.Columns[10].ReadOnly = true;
            dataGridView1.Columns[12].ReadOnly = true;
            dataGridView1.Columns[13].ReadOnly = true;

            s = "select q_no,q_name,answer1,answer2,answer3,answer4,answer5,correct_option"+
                ",qname_hasimage,ans1_hasimage,ans2_hasimage,ans3_hasimage,ans4_hasimage,ans5_hasimage"+
                " from questions_master where exam_code='" + ExamCode 
                + "' and sub_code='" + SubjectCode 
                + "' and test_code='" + TestCode + "'";
            ds = ob.fill_data_set(s);


            


            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 1)
                {
                    dataGridView1.Rows.Add(ds.Tables[0].Rows.Count - 1);
                }
                else if (ds.Tables[0].Rows.Count == 1)
                {
                    dataGridView1.Rows.Add(ds.Tables[0].Rows.Count);
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["q_no"].Value = ds.Tables[0].Rows[i]["q_no"];
                    dataGridView1.Rows[i].Cells["q_name"].Value = ds.Tables[0].Rows[i]["q_name"];
                    dataGridView1.Rows[i].Cells["image1_present"].Value = Convert.ToInt32(ds.Tables[0].Rows[i]["qname_hasimage"]);
                    dataGridView1.Rows[i].Cells["ans1"].Value = ds.Tables[0].Rows[i]["answer1"];
                    dataGridView1.Rows[i].Cells["image2_present"].Value = Convert.ToInt32(ds.Tables[0].Rows[i]["ans1_hasimage"]);
                    dataGridView1.Rows[i].Cells["ans2"].Value = ds.Tables[0].Rows[i]["answer2"];
                    dataGridView1.Rows[i].Cells["image3_present"].Value = Convert.ToInt32(ds.Tables[0].Rows[i]["ans2_hasimage"]);
                    dataGridView1.Rows[i].Cells["ans3"].Value = ds.Tables[0].Rows[i]["answer3"];
                    dataGridView1.Rows[i].Cells["image4_present"].Value = Convert.ToInt32(ds.Tables[0].Rows[i]["ans3_hasimage"]);
                    dataGridView1.Rows[i].Cells["ans4"].Value = ds.Tables[0].Rows[i]["answer4"];
                    dataGridView1.Rows[i].Cells["image5_present"].Value = Convert.ToInt32(ds.Tables[0].Rows[i]["ans4_hasimage"]);
                    dataGridView1.Rows[i].Cells["ans5"].Value = ds.Tables[0].Rows[i]["answer5"];
                    dataGridView1.Rows[i].Cells["image6_present"].Value = Convert.ToInt32(ds.Tables[0].Rows[i]["ans5_hasimage"]);
                    dataGridView1.Rows[i].Cells["correct_option"].Value = ds.Tables[0].Rows[i]["correct_option"];
                }
            }







        }



        //---button click event to fetch the excel file---->
        private void button2_Click(object sender, EventArgs e)
        {   fdlg.Filter = "All files (*.*)|*.*|Excel files (*.xls)|*.xls|Excel files(*.xlsx)|*.xlsx";
            if (fdlg.ShowDialog() == DialogResult.OK)
            { textBox1.Text  = fdlg.FileName;
            //---start of the switch staement to check for the extension and create the proper connection string--->
               switch (Path.GetExtension(fdlg.FileName)){
                case ".xls":
                    MyConnection = new System.Data.OleDb.OleDbConnection(@"provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + fdlg.FileName + "';Extended Properties=Excel 8.0;");
                    break;
                case ".xlsx":
                    MyConnection = new System.Data.OleDb.OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + fdlg.FileName + "';Extended Properties=Excel 8.0;");
                    break;
            }//---end of the switch statement->
               try
               {
                   MyConnection.Open();
                   dt = MyConnection.GetSchema("tables");
                   MyConnection.Close();
                   //---clearing the combox sheets->
                   comboBox1.Items.Clear();
                   //---adding the sheet names to the combobox----->  
                   foreach (DataRow row in dt.Rows)
                   {
                       string name = (string)row["TABLE_NAME"];
                       String sheetName = name.Replace("'", "");
                       if (sheetName.EndsWith("$"))
                       {
                           sheetName = sheetName.Substring(0, sheetName.Length - 1);
                       }
                       comboBox1.Items.Add(sheetName);
                   }
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
                   
                
             }//---endof the if condition--->
        }//----end of the button press event--->
        

     

        //---text change event for the combbox--->
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {          
                //---clearing the database before filling it--->
                string sQuery=string.Empty;
                string sheetname = comboBox1.Text;
                if (sheetname.Contains("$"))
                {
                   sQuery="select * from [" + comboBox1.Text + "]";
                }
                else
                {
                    sQuery="select * from [" + comboBox1.Text + "$]";
                }

   
                try
                {
                    ds = new DataSet();
                    MyConnection.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter(sQuery, MyConnection);
                    da.Fill(ds);
                    MyConnection.Close();
                    //dataGridView1.DataSource = ds.Tables[0];

                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Clear();
                    if (ds.Tables[0].Rows.Count > 0)
                    {


                        if (ds.Tables[0].Rows.Count > 1)
                        {dataGridView1.Rows.Add(ds.Tables[0].Rows.Count - 1);
                        }

                        else if (ds.Tables[0].Rows.Count == 1)
                        {dataGridView1.Rows.Add(ds.Tables[0].Rows.Count);
                        }

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells["q_no"].Value = ds.Tables[0].Rows[i][0];
                            dataGridView1.Rows[i].Cells["q_name"].Value = ds.Tables[0].Rows[i][1];
                            dataGridView1.Rows[i].Cells["ans1"].Value = ds.Tables[0].Rows[i][2];
                            dataGridView1.Rows[i].Cells["ans2"].Value = ds.Tables[0].Rows[i][3];
                            dataGridView1.Rows[i].Cells["ans3"].Value = ds.Tables[0].Rows[i][4];
                            dataGridView1.Rows[i].Cells["ans4"].Value = ds.Tables[0].Rows[i][5];
                            dataGridView1.Rows[i].Cells["ans5"].Value = ds.Tables[0].Rows[i][6];
                            dataGridView1.Rows[i].Cells["correct_option"].Value = ds.Tables[0].Rows[i][7];
                        }
                    }
           
              
                }
                catch ( OleDbException ex) {
                    MessageBox.Show(ex.Message);
                    MyConnection.Close();
                                         }
                    
            //---assigning the default cell fill type property for the datagridveiw1--->
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
        } //---selected index for the exam comboox-->
  




        //---code behind the save button-->
        private void button1_Click(object sender, EventArgs e)
        {

            //--deleting all the questions of that respective subject before reentering
            s = null;
            s = "delete from questions_master where exam_code='" + ExamCode + "' and test_code='" + TestCode + "' and sub_code='" + SubjectCode + "'";
            ob.execute_non_query(s);


            //try
            //{
                for (int i = 0; i <dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToString(dataGridView1.Rows[i].Cells[0].Value).Replace("'", "''") != "")
                    {

                        String QustionNmae = "";
                        String Answer1 = "";
                        String Answer2 = "";
                        String Answer3 = "";
                        String Answer4 = "";
                        String Answer5 = "";
                       
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image1_present"].Value)==1)
                        {
                            QustionNmae = System.IO.Path.GetFileName(Convert.ToString(dataGridView1.Rows[i].Cells["q_name"].Value));
                            String image_path = Convert.ToString(dataGridView1.Rows[i].Cells["q_name"].Value);
                            System.IO.File.Copy(image_path, Application.StartupPath + "\\image_path\\" + System.IO.Path.GetFileName(image_path), true);
                       
                        }
                        else if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image1_present"].Value) == 0)
                        {
                            QustionNmae = Convert.ToString(dataGridView1.Rows[i].Cells["q_name"].Value).Replace("'", "''");
                        }
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image2_present"].Value) == 1)
                        {
                            Answer1 = System.IO.Path.GetFileName(Convert.ToString(dataGridView1.Rows[i].Cells["ans1"].Value));
                            String image_path = Convert.ToString(dataGridView1.Rows[i].Cells["ans1"].Value);
                            System.IO.File.Copy(image_path, Application.StartupPath + "\\image_path\\" + System.IO.Path.GetFileName(image_path), true);
                        }
                        else if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image2_present"].Value) == 0)
                        {
                            Answer1 = Convert.ToString(dataGridView1.Rows[i].Cells["ans1"].Value).Replace("'", "''");
                        }
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image3_present"].Value) == 1)
                        {
                            Answer2 = System.IO.Path.GetFileName(Convert.ToString(dataGridView1.Rows[i].Cells["ans2"].Value));
                            String image_path = Convert.ToString(dataGridView1.Rows[i].Cells["ans2"].Value);
                            System.IO.File.Copy(image_path, Application.StartupPath + "\\image_path\\" + System.IO.Path.GetFileName(image_path), true);
                        }
                        else if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image3_present"].Value) == 0)
                        {
                            Answer2 = Convert.ToString(dataGridView1.Rows[i].Cells["ans2"].Value).Replace("'", "''");
                        }
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image4_present"].Value) == 1)
                        {
                            Answer3 = System.IO.Path.GetFileName(Convert.ToString(dataGridView1.Rows[i].Cells["ans3"].Value));
                            String image_path = Convert.ToString(dataGridView1.Rows[i].Cells["ans3"].Value);
                            System.IO.File.Copy(image_path, Application.StartupPath + "\\image_path\\" + System.IO.Path.GetFileName(image_path), true);
                        }
                        else if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image4_present"].Value) == 0)
                        {
                            Answer3 = Convert.ToString(dataGridView1.Rows[i].Cells["ans3"].Value).Replace("'", "''");
                        }
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image5_present"].Value) == 1)
                        {
                            Answer4 = System.IO.Path.GetFileName(Convert.ToString(dataGridView1.Rows[i].Cells["ans4"].Value));
                            String image_path = Convert.ToString(dataGridView1.Rows[i].Cells["ans4"].Value);
                            System.IO.File.Copy(image_path, Application.StartupPath + "\\image_path\\" + System.IO.Path.GetFileName(image_path), true);
                        }
                        else if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image5_present"].Value) == 0)
                        {
                            Answer4 = Convert.ToString(dataGridView1.Rows[i].Cells["ans4"].Value).Replace("'", "''");
                        }
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image6_present"].Value) == 1)
                        {
                            Answer5 = System.IO.Path.GetFileName(Convert.ToString(dataGridView1.Rows[i].Cells["ans5"].Value));
                            String image_path = Convert.ToString(dataGridView1.Rows[i].Cells["ans5"].Value);
                            System.IO.File.Copy(image_path, Application.StartupPath + "\\image_path\\" + System.IO.Path.GetFileName(image_path), true);
                        }
                        else if (Convert.ToInt32(dataGridView1.Rows[i].Cells["image6_present"].Value) == 0)
                        {
                            Answer5 = Convert.ToString(dataGridView1.Rows[i].Cells["ans5"].Value).Replace("'", "''");
                        }
                            s = "insert into questions_master(exam_code,sub_code,test_code,q_no,q_name,answer1,answer2,answer3," +
                                               "answer4,answer5,correct_option,qname_hasimage,ans1_hasimage,ans2_hasimage,ans3_hasimage,ans4_hasimage,ans5_hasimage) values('" +
                                               exam_code.Replace("'", "''") + "','" + subject_code.Replace("'", "''") + "','" + test_code.Replace("'", "''") + "','" +
                                               Convert.ToString(dataGridView1.Rows[i].Cells["q_no"].Value).Replace("'", "''") + "','" + //---question no.-->
                                               QustionNmae  + "','" +//----question name-->
                                               Answer1 + "','" +//--answer1--->
                                               Answer2 + "','" +//--answer2--->
                                               Answer3 + "','" +//--answer3--->
                                               Answer4 + "','" +//--answer4--->
                                               Answer5 + "','" +//--answer5--->
                                               Convert.ToString(dataGridView1.Rows[i].Cells["correct_option"].Value).Replace("'", "''") + "'," +
                                               Convert.ToInt32(dataGridView1.Rows[i].Cells["image1_present"].Value) + ","+
                                               Convert.ToInt32(dataGridView1.Rows[i].Cells["image2_present"].Value) + ","+
                                               Convert.ToInt32(dataGridView1.Rows[i].Cells["image3_present"].Value) + ","+
                                               Convert.ToInt32(dataGridView1.Rows[i].Cells["image4_present"].Value) + ","+
                                               Convert.ToInt32(dataGridView1.Rows[i].Cells["image5_present"].Value) + ","+
                                               Convert.ToInt32(dataGridView1.Rows[i].Cells["image5_present"].Value) + ");";
                            ob.execute_non_query(s);

                    

                                              
                    }

                    
                }



            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Questions Could Not Be Created May Be Questions Already Present");
            //}


            refresh();
            this.Close();

     
     }
        //--delete button click exvent--->
        private void button3_Click(object sender, EventArgs e)
        {
             s = null;
             s = "delete from questions_master where exam_code='"+ ExamCode +"' and test_code='"+ TestCode +"' and sub_code='"+ SubjectCode +"'";
             ob.execute_non_query(s);
             refresh();
             refresh_me();
        }


        private void refresh()
        {
            class_Application.frm_QuestionAddEdit.DataGridView1.Columns.Clear();
            DataGridViewCheckBoxColumn ob1 = new DataGridViewCheckBoxColumn();
            class_Application.frm_QuestionAddEdit.DataGridView1.Columns.Add("sub_code", "SUBJECT CODE");
            class_Application.frm_QuestionAddEdit.DataGridView1.Columns.Add("sub_name", "SUBJECT NAME");
            class_Application.frm_QuestionAddEdit.DataGridView1.Columns.Add(ob1);
            class_Application.frm_QuestionAddEdit.DataGridView1.Columns[2].HeaderText = "QUESTION PRESENT";
            class_Application.frm_QuestionAddEdit.DataGridView1.Columns[2].Name = "is_present";
            class_Application.frm_QuestionAddEdit.DataGridView1.RowHeadersVisible = false;
            class_Application.frm_QuestionAddEdit.DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            class_Application.frm_QuestionAddEdit.DataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            class_Application.frm_QuestionAddEdit.DataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            class_Application.frm_QuestionAddEdit.DataGridView1.Columns[2].ReadOnly = true;

            s = "select sub_code,sub_name from subject_master where exam_code='" + ExamCode + "'";
            DataSet ds1 = ob.fill_data_set(s);
            class_Application.frm_QuestionAddEdit.DataGridView1.Rows.Add(ds1.Tables[0].Rows.Count);
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                s = null;
                class_Application.frm_QuestionAddEdit.DataGridView1.Rows[i].Cells["sub_code"].Value = ds1.Tables[0].Rows[i]["sub_code"];
                class_Application.frm_QuestionAddEdit.DataGridView1.Rows[i].Cells["sub_name"].Value = ds1.Tables[0].Rows[i]["sub_name"];
                s = "select iif ( count(*),1,0)  from questions_master where exam_code='" + exam_code + "' and test_code='" + test_code + "' and sub_code='" + Convert.ToString(ds1.Tables[0].Rows[i]["sub_code"]) + "';";
                class_Application.frm_QuestionAddEdit.DataGridView1.Rows[i].Cells["is_present"].Value = Convert.ToInt32(ob.execute_scalar(s));
            }
    }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frm_insert_Image frm_insert_Image = new frm_insert_Image();
            frm_insert_Image.ROW = e.RowIndex;
            frm_insert_Image.COLUMN = e.ColumnIndex;

            //if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
            //{ frm_insert_Image.QuestionImage.Text  =Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            //}


            frm_insert_Image.Show();
        }

           
        
    }
}
