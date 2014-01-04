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
    public partial class frm_exam : Form
    {
        public frm_exam()
        {
            InitializeComponent();
        }

        //---declaring the object of the type class-->
        static class_Application ob;
        //---declaring a object of type tab control-->
        private TabControl tabControl1;
        // Declares tabPage1 as a TabPage type------->
        private System.Windows.Forms.TabPage tabPage1;
     
        
        
        usr_cntrl[] obj_UsrCntrl;
        int count;
        DataSet question_dataset;




        String _ExamName;
        String _TestName;
        String _ExamCode;
        String _TestCode;
        int _Duration;
        int seconds;
     

        public string ExamName
        {
            get { return _ExamName; }
            set{_ExamName=value;}
        }
        public string ExamCode
        {
            get{return _ExamCode;}
            set { _ExamCode = value; } 
        }
        public string TestName
        {
            get { return _TestName; }
            set { _TestName = value; }
        }
        public string TestCode
        {
            get { return _TestCode; }
            set { _TestCode = value; }
        }
        public int Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
       
      


       

        private void frm_experimemnt_Load(object sender, EventArgs e)
        {

            seconds = Duration * 60;
            ob = new class_Application();
            count = 0;
            MyTabs();

        }


        private void MyTabs()
        {
            
            if (class_Application.review_flag == 1)
            {
                string s = null;
                s = "select count(*) from user_exam_dtl where exam_code='" + ExamCode + "' and test_code='" + TestCode + "' and user_id='" + class_Application.user_id + "'";
                if (Convert.ToInt32(ob.execute_scalar(s)) > 0)
                {
                    MessageBox.Show("You Have Already Given The Test", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }



                s = "SELECT sub_group,Sub_Name,questions_master.sub_code,Q_No,Q_Name,Answer1,Answer2,Answer3,Answer4,Answer5," +
                    "correct_option,qname_hasimage,ans1_hasimage,ans2_hasimage,ans3_hasimage,ans4_hasimage,ans5_hasimage FROM Subject_Master INNER JOIN Questions_Master ON " +
                    "Subject_Master.Sub_code = Questions_Master.Sub_code WHERE Questions_Master.Exam_code='" + ExamCode +
                    "'  AND Questions_Master.Test_code='" + TestCode + "' order by Questions_Master.Exam_code," +
                    "questions_master.sub_code,q_no;";

                question_dataset = ob.fill_data_set(s);
                var query = from p in question_dataset.Tables[0].AsEnumerable()
                            group p by new
                            {
                                subjectname = p.Field<string>(1),
                                subjectcode = p.Field<string>(2)

                            } into grp
                            select new
                            {
                                subjectname = grp.Key.subjectname,
                                subjectcode = grp.Key.subjectcode
                            };

                obj_UsrCntrl = new usr_cntrl[question_dataset.Tables[0].Rows.Count];
                //--creating an instance of the tabcontroller-->
                tabControl1 = new TabControl();
                foreach (var grp in query)
                {   //Invokes the TabPage() constructor to create the tabPage1. 
                    tabPage1 = new System.Windows.Forms.TabPage();
                    tabPage1.Text = grp.subjectname;
                    FlowLayoutPanel FlPanel = new FlowLayoutPanel();
                    FlPanel = bind_data(grp.subjectcode);
                    tabPage1.Controls.Add(FlPanel);
                    tabControl1.Controls.AddRange(new Control[] { this.tabPage1 });
                }

                Button ObjButton = new Button();
                ObjButton.Size = new Size(75, 23);
                ObjButton.Location = new Point(362, 536);
                ObjButton.Text = "SUBMIT";
                ObjButton.Click += new EventHandler(ButtonClick);
                Controls.AddRange(new Control[] { ObjButton });

            }


            else if (class_Application.review_flag == 0)
            {
                string s = null;
                s = "SELECT Exam_Master.Exam_Code, Test_Master.Test_code, Subject_Master.Sub_code, "+
                    "Exam_Master.Exam_Name, Test_Master.Test_Name, Subject_Master.Sub_Name, Questions_Master.Q_No,"+
                    " Questions_Master.Q_Name, Questions_Master.Answer1, Questions_Master.Answer2, Questions_Master.Answer3,"+
                    " Questions_Master.Answer4, Questions_Master.Answer5, Questions_Master.Correct_Option,"+
                    " Questions_Master.QName_HasImage, User_Exam_Dtl.Option1, User_Exam_Dtl.Option2, User_Exam_Dtl.Option3,"+
                    " User_Exam_Dtl.Option4, User_Exam_Dtl.Option5, User_Exam_Dtl.Is_Attempted,Questions_Master.Ans1_HasImage,"+
                    "Questions_Master.Ans2_HasImage,Questions_Master.Ans3_HasImage,Questions_Master.Ans4_HasImage,"+
                    "Questions_Master.Ans5_HasImage"+
                    " FROM" +
                    " (((Exam_Master INNER JOIN Questions_Master ON Exam_Master.Exam_Code = Questions_Master.Exam_code) "+
                    "INNER JOIN Subject_Master ON (Subject_Master.Exam_code = Questions_Master.Exam_code) AND "+
                    "(Subject_Master.Sub_code = Questions_Master.Sub_code) AND "+
                    "(Exam_Master.Exam_Code = Subject_Master.Exam_code)) INNER JOIN Test_Master ON "+
                    "(Test_Master.Test_code = Questions_Master.Test_code) AND "+
                    "(Test_Master.Exam_code = Questions_Master.Exam_code) AND (Exam_Master.Exam_Code = Test_Master.Exam_code)) "+
                    "INNER JOIN User_Exam_Dtl ON (Questions_Master.Q_No = User_Exam_Dtl.Q_No) AND (Questions_Master.Test_code "+
                    "= User_Exam_Dtl.Test_code) AND (Questions_Master.Sub_code = User_Exam_Dtl.Sub_code) AND "+
                    "(Questions_Master.Exam_code = User_Exam_Dtl.Exam_code) WHERE (((User_Exam_Dtl.User_ID)='" + class_Application.user_id + "'));";




                question_dataset = ob.fill_data_set(s);
                var query = from p in question_dataset.Tables[0].AsEnumerable()
                            group p by new
                            {
                                subjectname = p.Field<string>(5),
                                subjectcode = p.Field<string>(2)

                            } into grp
                            select new
                            {
                                subjectname = grp.Key.subjectname,
                                subjectcode = grp.Key.subjectcode
                            };

                obj_UsrCntrl = new usr_cntrl[question_dataset.Tables[0].Rows.Count];
                //--creating an instance of the tabcontroller-->
                tabControl1 = new TabControl();
                foreach (var grp in query)
                {   //Invokes the TabPage() constructor to create the tabPage1. 
                    tabPage1 = new System.Windows.Forms.TabPage();
                    tabPage1.Text = grp.subjectname;
                    FlowLayoutPanel FlPanel = new FlowLayoutPanel();
                    FlPanel = binddata_for_review(grp.subjectcode);
                    tabPage1.Controls.Add(FlPanel);
                    tabControl1.Controls.AddRange(new Control[] { this.tabPage1 });
                }

            }



   
          

            lblExam.Text = ExamName;
            lblTest.Text = TestName;
            //---definign the tabcontrol location-->
            tabControl1.Location = new Point(12,68);
            //--definig the tabcontrol size--->
            tabControl1.Size = new Size(865, 462);
            //---this code is for adding the tab control and the button to the form-->
            Controls.AddRange(new Control[] { tabControl1 });
            //---declares the size of the form-->
            ClientSize = new Size(897, 597);
        }



        private FlowLayoutPanel bind_data(string SubjectCode)
        {
            var query = from p in question_dataset.Tables[0].AsEnumerable()
                        where p.Field<string>(2).Equals(SubjectCode)
                        select new
                        {
                            SubjectGroup = p.Field<string>(0),
                            SubjectName = p.Field<string>(1),
                            SubjectCode = p.Field<string>(2),
                            QuestionNo = p.Field<int>(3),
                            QuestionName = p.Field<string>(4),
                            Answer1 = p.Field<string>(5),
                            Answer2 = p.Field<string>(6),
                            Answer3 = p.Field<string>(7),
                            Answer4 = p.Field<string>(8),
                            Answer5 = p.Field<string>(9),
                            CorrectOption = p.Field<int>(10),
                            Qname_HasImage = p.Field<int>(11),
                            Ans1_HasImage = p.Field<int>(12),
                            Ans2_HasImage = p.Field<int>(13),
                            Ans3_HasImage = p.Field<int>(14),
                            Ans4_HasImage = p.Field<int>(15),
                            Ans5_HasImage = p.Field<int>(16)
                        };


            FlowLayoutPanel FlPanel = new FlowLayoutPanel();
            foreach (var grp in query)
            {
                obj_UsrCntrl[count] = new usr_cntrl();
               
                    obj_UsrCntrl[count].CorrectImage1.Visible = false;
                    obj_UsrCntrl[count].CorrectImage2.Visible = false;
                    obj_UsrCntrl[count].CorrectImage3.Visible = false;
                    obj_UsrCntrl[count].CorrectImage4.Visible = false;
                    obj_UsrCntrl[count].CorrectImage5.Visible = false;
                    obj_UsrCntrl[count].WrongImage1.Visible = false;
                    obj_UsrCntrl[count].WrongImage2.Visible = false;
                    obj_UsrCntrl[count].WrongImage3.Visible = false;
                    obj_UsrCntrl[count].WrongImage4.Visible = false;
                    obj_UsrCntrl[count].WrongImage5.Visible = false;


                    if (grp.Qname_HasImage == 1)
                    {
                        obj_UsrCntrl[count].Picture.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.QuestionName);
                        obj_UsrCntrl[count].Flpanel.Visible = true;
                        obj_UsrCntrl[count].Question_Label.Visible = false;
                        obj_UsrCntrl[count].Flpanel.Size = new Size(793, 196);
                        obj_UsrCntrl[count].Flpanel.Location = new Point(29, 40);
                    }
                    if (grp.Ans1_HasImage == 1)
                    {
                        obj_UsrCntrl[count].ImageAns1.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer1);
                    }
                    if (grp.Ans2_HasImage == 1)
                    {
                        obj_UsrCntrl[count].ImageAns2.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer2);
                    }
                    if (grp.Ans3_HasImage == 1)
                    {
                        obj_UsrCntrl[count].ImageAns3.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer3);

                    }
                    if (grp.Ans4_HasImage == 1)
                    {
                        obj_UsrCntrl[count].ImageAns4.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer4);

                    }
                    if (grp.Ans5_HasImage == 1)
                    {
                        obj_UsrCntrl[count].ImageAns5.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer5);

                    }
                    if (grp.Qname_HasImage == 0)
                    {
                        obj_UsrCntrl[count].Question_Label.Text = grp.QuestionName;
                        obj_UsrCntrl[count].Flpanel.Visible = false;
                        obj_UsrCntrl[count].Question_Label.Visible = true;
                        obj_UsrCntrl[count].Question_Label.Size = new Size(793, 196);
                        obj_UsrCntrl[count].Question_Label.Location = new Point(29, 40);
                    }
                    if (grp.Ans1_HasImage == 0)
                    {
                        obj_UsrCntrl[count].ImageAns1.Text = grp.Answer1;
                    }
                    if (grp.Ans2_HasImage == 0)
                    {
                        obj_UsrCntrl[count].ImageAns2.Text = grp.Answer2;
                    }
                    if (grp.Ans3_HasImage == 0)
                    {
                        obj_UsrCntrl[count].ImageAns3.Text = grp.Answer3;

                    }
                    if (grp.Ans4_HasImage == 0)
                    {
                        obj_UsrCntrl[count].ImageAns4.Text = grp.Answer4;

                    }
                    if (grp.Ans5_HasImage == 0)
                    {
                        obj_UsrCntrl[count].ImageAns5.Text = grp.Answer5;

                    }
                        
                obj_UsrCntrl[count].question_number.Text = Convert.ToString(grp.QuestionNo);
                obj_UsrCntrl[count].CorrectOption.Text = Convert.ToString(grp.CorrectOption);
                obj_UsrCntrl[count].CorrectOption.Visible = false;
                obj_UsrCntrl[count].section_code.Text = grp.SubjectCode;
                obj_UsrCntrl[count].section_code.Visible = false;
                obj_UsrCntrl[count].Dock = System.Windows.Forms.DockStyle.Top;
                FlPanel.Controls.Add(obj_UsrCntrl[count]);
                count++;

            }


            FlPanel.AutoScroll = true;
            FlPanel.Size = new Size(856, 462);
            return FlPanel;
        }




        private FlowLayoutPanel binddata_for_review(string SubjectCode)
        {

            var query = from p in question_dataset.Tables[0].AsEnumerable()
                        where p.Field<string>(2).Equals(SubjectCode)
                        select new
                        {
                            ExamCode = p.Field<string>(0),
                            TestCode = p.Field<string>(1),
                            SubjectCode = p.Field<string>(2),
                            ExamName = p.Field<string>(3),
                            TestName = p.Field<string>(4),
                            SubjectName = p.Field<string>(5),
                            QuestionNo = p.Field<int>(6),
                            QuestionName = p.Field<string>(7),
                            Answer1 = p.Field<string>(8),
                            Answer2 = p.Field<string>(9),
                            Answer3 = p.Field<string>(10),
                            Answer4 = p.Field<string>(11),
                            Answer5 = p.Field<string>(12),
                            CorrectOption = p.Field<int>(13),
                            Qname_HasImage = p.Field<int>(14),
                            Option1 = p.Field<int>(15),
                            Option2 = p.Field<int>(16),
                            Option3 = p.Field<int>(17),
                            Option4 = p.Field<int>(18),
                            Option5 = p.Field<int>(19),
                            IsAttempted = p.Field<int>(20),
                            Ans1_HasImage = p.Field<int>(21),
                            Ans2_HasImage = p.Field<int>(22),
                            Ans3_HasImage = p.Field<int>(23),
                            Ans4_HasImage = p.Field<int>(24),
                            Ans5_HasImage = p.Field<int>(25)
                        };


            FlowLayoutPanel FlPanel = new FlowLayoutPanel();

            foreach (var grp in query)
            {
                obj_UsrCntrl[count] = new usr_cntrl();


                if (grp.Qname_HasImage == 1)
                {
                    obj_UsrCntrl[count].Picture.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.QuestionName);
                    obj_UsrCntrl[count].Flpanel.Visible = true;
                    obj_UsrCntrl[count].Question_Label.Visible = false;
                    obj_UsrCntrl[count].Flpanel.Size = new Size(793, 196);
                    obj_UsrCntrl[count].Flpanel.Location = new Point(29, 40);
                }
                if (grp.Ans1_HasImage == 1)
                {
                    obj_UsrCntrl[count].ImageAns1.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer1);
                }
                if (grp.Ans2_HasImage == 1)
                {
                    obj_UsrCntrl[count].ImageAns2.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer2);
                }
                if (grp.Ans3_HasImage == 1)
                {
                    obj_UsrCntrl[count].ImageAns3.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer3);
                }
                if (grp.Ans4_HasImage == 1)
                {
                    obj_UsrCntrl[count].ImageAns4.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer4);
                }
                if (grp.Ans5_HasImage == 1)
                {
                    obj_UsrCntrl[count].ImageAns5.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + grp.Answer5);
                }
                if (grp.Qname_HasImage == 0)
                {
                    obj_UsrCntrl[count].Question_Label.Text = grp.QuestionName;
                    obj_UsrCntrl[count].Flpanel.Visible = false;
                    obj_UsrCntrl[count].Question_Label.Visible = true;
                    obj_UsrCntrl[count].Question_Label.Size = new Size(793, 196);
                    obj_UsrCntrl[count].Question_Label.Location = new Point(29, 40);
                }
                if (grp.Ans1_HasImage == 0)
                {
                    obj_UsrCntrl[count].ImageAns1.Text = grp.Answer1;
                }
                if (grp.Ans2_HasImage == 0)
                {
                    obj_UsrCntrl[count].ImageAns2.Text = grp.Answer2;
                }
                if (grp.Ans3_HasImage == 0)
                {
                    obj_UsrCntrl[count].ImageAns3.Text = grp.Answer3;
                }
                if (grp.Ans4_HasImage == 0)
                {
                    obj_UsrCntrl[count].ImageAns4.Text = grp.Answer4;
                }
                if (grp.Ans5_HasImage == 0)
                {
                    obj_UsrCntrl[count].ImageAns5.Text = grp.Answer5;
                }



                    obj_UsrCntrl[count].RadioButton6.Checked = Convert.ToBoolean(grp.Option1);
                    obj_UsrCntrl[count].RadioButton7.Checked = Convert.ToBoolean(grp.Option2);
                    obj_UsrCntrl[count].RadioButton8.Checked = Convert.ToBoolean(grp.Option3);
                    obj_UsrCntrl[count].RadioButton9.Checked = Convert.ToBoolean(grp.Option4);
                    obj_UsrCntrl[count].RadioButton10.Checked = Convert.ToBoolean(grp.Option5);
                    obj_UsrCntrl[count].RadioButton6.Enabled = false;
                    obj_UsrCntrl[count].RadioButton7.Enabled = false;
                    obj_UsrCntrl[count].RadioButton8.Enabled = false;
                    obj_UsrCntrl[count].RadioButton9.Enabled = false;
                    obj_UsrCntrl[count].RadioButton10.Enabled = false;


                    if (obj_UsrCntrl[count].RadioButton6.Checked)
                    {
                        obj_UsrCntrl[count].WrongImage1.Visible = true;
                        obj_UsrCntrl[count].WrongImage2.Visible = false;
                        obj_UsrCntrl[count].WrongImage3.Visible = false;
                        obj_UsrCntrl[count].WrongImage4.Visible = false;
                        obj_UsrCntrl[count].WrongImage5.Visible = false;
                    }
                    else if (obj_UsrCntrl[count].RadioButton7.Checked)
                    {
                        obj_UsrCntrl[count].WrongImage1.Visible = false;
                        obj_UsrCntrl[count].WrongImage2.Visible = true;
                        obj_UsrCntrl[count].WrongImage3.Visible = false;
                        obj_UsrCntrl[count].WrongImage4.Visible = false;
                        obj_UsrCntrl[count].WrongImage5.Visible = false;
                    }
                    else if (obj_UsrCntrl[count].RadioButton8.Checked)
                    {
                        obj_UsrCntrl[count].WrongImage1.Visible = false;
                        obj_UsrCntrl[count].WrongImage2.Visible = false;
                        obj_UsrCntrl[count].WrongImage3.Visible = true;
                        obj_UsrCntrl[count].WrongImage4.Visible = false;
                        obj_UsrCntrl[count].WrongImage5.Visible = false;
                    }
                    else if (obj_UsrCntrl[count].RadioButton9.Checked)
                    {
                        obj_UsrCntrl[count].WrongImage1.Visible = false;
                        obj_UsrCntrl[count].WrongImage2.Visible = false;
                        obj_UsrCntrl[count].WrongImage3.Visible = false;
                        obj_UsrCntrl[count].WrongImage4.Visible = true;
                        obj_UsrCntrl[count].WrongImage5.Visible = false;
                    }
                    else if (obj_UsrCntrl[count].RadioButton10.Checked)
                    {
                        obj_UsrCntrl[count].WrongImage1.Visible = false;
                        obj_UsrCntrl[count].WrongImage2.Visible = false;
                        obj_UsrCntrl[count].WrongImage3.Visible = false;
                        obj_UsrCntrl[count].WrongImage4.Visible = false;
                        obj_UsrCntrl[count].WrongImage5.Visible = true;
                    }
                    else
                    {
                        obj_UsrCntrl[count].WrongImage1.Visible = false;
                        obj_UsrCntrl[count].WrongImage2.Visible = false;
                        obj_UsrCntrl[count].WrongImage3.Visible = false;
                        obj_UsrCntrl[count].WrongImage4.Visible = false;
                        obj_UsrCntrl[count].WrongImage5.Visible = false;
                    }


                    switch (grp.CorrectOption)
                    {
                        case 1:
                            obj_UsrCntrl[count].CorrectImage1.Visible = true;
                            obj_UsrCntrl[count].CorrectImage2.Visible = false;
                            obj_UsrCntrl[count].CorrectImage3.Visible = false;
                            obj_UsrCntrl[count].CorrectImage4.Visible = false;
                            obj_UsrCntrl[count].CorrectImage5.Visible = false;
                            if (obj_UsrCntrl[count].RadioButton6.Checked)
                            {
                                obj_UsrCntrl[count].WrongImage1.Visible = false;
                                obj_UsrCntrl[count].WrongImage2.Visible = false;
                                obj_UsrCntrl[count].WrongImage3.Visible = false;
                                obj_UsrCntrl[count].WrongImage4.Visible = false;
                                obj_UsrCntrl[count].WrongImage5.Visible = false;
                            }
                            break;
                        case 2:
                            obj_UsrCntrl[count].CorrectImage1.Visible = false;
                            obj_UsrCntrl[count].CorrectImage2.Visible = true;
                            obj_UsrCntrl[count].CorrectImage3.Visible = false;
                            obj_UsrCntrl[count].CorrectImage4.Visible = false;
                            obj_UsrCntrl[count].CorrectImage5.Visible = false;
                            if (obj_UsrCntrl[count].RadioButton7.Checked)
                            {
                                obj_UsrCntrl[count].WrongImage1.Visible = false;
                                obj_UsrCntrl[count].WrongImage2.Visible = false;
                                obj_UsrCntrl[count].WrongImage3.Visible = false;
                                obj_UsrCntrl[count].WrongImage4.Visible = false;
                                obj_UsrCntrl[count].WrongImage5.Visible = false;
                            }
                            break;
                        case 3:
                            obj_UsrCntrl[count].CorrectImage1.Visible = false;
                            obj_UsrCntrl[count].CorrectImage2.Visible = false;
                            obj_UsrCntrl[count].CorrectImage3.Visible = true;
                            obj_UsrCntrl[count].CorrectImage4.Visible = false;
                            obj_UsrCntrl[count].CorrectImage5.Visible = false;
                            if (obj_UsrCntrl[count].RadioButton8.Checked)
                            {
                                obj_UsrCntrl[count].WrongImage1.Visible = false;
                                obj_UsrCntrl[count].WrongImage2.Visible = false;
                                obj_UsrCntrl[count].WrongImage3.Visible = false;
                                obj_UsrCntrl[count].WrongImage4.Visible = false;
                                obj_UsrCntrl[count].WrongImage5.Visible = false;
                            }
                            break;
                        case 4:
                            obj_UsrCntrl[count].CorrectImage1.Visible = false;
                            obj_UsrCntrl[count].CorrectImage2.Visible = false;
                            obj_UsrCntrl[count].CorrectImage3.Visible = false;
                            obj_UsrCntrl[count].CorrectImage4.Visible = true;
                            obj_UsrCntrl[count].CorrectImage5.Visible = false;
                            if (obj_UsrCntrl[count].RadioButton9.Checked)
                            {
                                obj_UsrCntrl[count].WrongImage1.Visible = false;
                                obj_UsrCntrl[count].WrongImage2.Visible = false;
                                obj_UsrCntrl[count].WrongImage3.Visible = false;
                                obj_UsrCntrl[count].WrongImage4.Visible = false;
                                obj_UsrCntrl[count].WrongImage5.Visible = false;
                            }
                            break;
                        case 5:
                            obj_UsrCntrl[count].CorrectImage1.Visible = false;
                            obj_UsrCntrl[count].CorrectImage2.Visible = false;
                            obj_UsrCntrl[count].CorrectImage3.Visible = false;
                            obj_UsrCntrl[count].CorrectImage4.Visible = false;
                            obj_UsrCntrl[count].CorrectImage5.Visible = true;
                            if (obj_UsrCntrl[count].RadioButton10.Checked)
                            {
                                obj_UsrCntrl[count].WrongImage1.Visible = false;
                                obj_UsrCntrl[count].WrongImage2.Visible = false;
                                obj_UsrCntrl[count].WrongImage3.Visible = false;
                                obj_UsrCntrl[count].WrongImage4.Visible = false;
                                obj_UsrCntrl[count].WrongImage5.Visible = false;
                            }
                            break;
                    }


                obj_UsrCntrl[count].question_number.Text = Convert.ToString(grp.QuestionNo);
                obj_UsrCntrl[count].CorrectOption.Text = Convert.ToString(grp.CorrectOption);
                obj_UsrCntrl[count].CorrectOption.Visible = false;
                obj_UsrCntrl[count].section_code.Text = grp.SubjectCode;
                obj_UsrCntrl[count].section_code.Visible = false;
                obj_UsrCntrl[count].Dock = System.Windows.Forms.DockStyle.Top;
                FlPanel.Controls.Add(obj_UsrCntrl[count]);
                count++;

            }


            FlPanel.AutoScroll = true;
            FlPanel.Size = new Size(856, 462);
            return FlPanel;
        }





        //---adding click events to the submit button created--->
        private void ButtonClick(object sender, EventArgs e)
        {
            submit();
        }


        private void submit()
        {
            string s = "delete from user_exam_dtl where test_code='" + TestCode + "' and exam_code='" + ExamCode + "' and user_id='" + class_Application.user_id + "' and user_name='" + class_Application.user_name + "'";
            ob.execute_non_query(s);
            for (int i = 0; i < obj_UsrCntrl.Length; i++)
            {
                s = null;
                int is_attempted = 0;
                if (obj_UsrCntrl[i].RadioButton6.Checked || obj_UsrCntrl[i].RadioButton7.Checked || obj_UsrCntrl[i].RadioButton8.Checked || obj_UsrCntrl[i].RadioButton9.Checked || obj_UsrCntrl[i].RadioButton10.Checked)
                {
                    is_attempted = 1;
                }

                s = "insert into user_exam_dtl(user_id,user_name,exam_code,test_code,sub_code,q_no,"+
                    "option1,option2,option3,option4,option5,correct_option,is_attempted) values('" +
                class_Application.user_id + "','" + class_Application.user_name + "','" + ExamCode + "','" + TestCode + "','" +
                obj_UsrCntrl[i].section_code.Text + "'," + obj_UsrCntrl[i].question_number.Text + "," +
                Convert.ToInt32(obj_UsrCntrl[i].RadioButton6.Checked) + "," +
                Convert.ToInt32(obj_UsrCntrl[i].RadioButton7.Checked) + "," +
                Convert.ToInt32(obj_UsrCntrl[i].RadioButton8.Checked) + "," +
                Convert.ToInt32(obj_UsrCntrl[i].RadioButton9.Checked) + "," +
                Convert.ToInt32(obj_UsrCntrl[i].RadioButton10.Checked) + "," + 
                obj_UsrCntrl[i].CorrectOption.Text + ",'" + is_attempted + "');";
                ob.execute_non_query(s);
            }

            MessageBox.Show("Exam Completed");
            this.Close();
        }





        //--timer tick event-->
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (class_Application.review_flag == 1)
            {
                if (seconds == 0)
                {
                    submit();
                }

                int hour = seconds / 3600;
                int minute = (seconds % 3600) / 60;
                int count_second = (seconds % 3600) % 60;
                label6.Text = Convert.ToString(hour) + ":" + Convert.ToString(minute) + ":" + Convert.ToString(count_second);
                seconds--;
            }
            else if(class_Application.review_flag==0)
            {
                label1.Visible = false;
                label6.Visible = false;
            }
        }



    }
}
