using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.IO;
using System.Collections;


namespace exam
{
    public partial class frm_exam : Form
    {
        
        static class_Application ob;
        CurrencyManager cur_manager;

        int time;
        int count;
        DataTable init_datatable;
        DataTable Question;
        DataTable Subject;
        DataTable collection;

        string _testcode;
        string _examcode;
        string _testname;
        string _examname;



        public string ExamCode
        {
            get { return _examcode; }
            set { _examcode = value; }
        }
        public string TestCode
        {
            get { return _testcode; }
            set { _testcode = value; }
        }
        public string TestName
        {
            get { return _testname; }
            set { _testname = value; }
        }
        public string ExamName
        {
            get { return _examname;}
            set { _examname = value; }
        }





        public frm_exam()
        {
            InitializeComponent();
        }

        //--form load event---->
        private void frm_exam_Load(object sender, EventArgs e)
        {

            
            count = 0;
            ob = new class_Application();

            if (class_Application.REVIEW == 0)
            {
                timer1.Enabled = true;
                lbl_timer.Visible = true;

                string s = "SELECT Exam_Master.Exam_Code, Subject_Master.Sub_code, Test_Master.Test_code, Exam_Master.Exam_Name," +
               " Subject_Master.Sub_Name, Test_Master.Test_Name, Questions_Master.Q_No, Questions_Master.Q_Name, " +
               "Questions_Master.Answer1, Questions_Master.Answer2, Questions_Master.Answer3, Questions_Master.Answer4, " +
               "Questions_Master.Answer5, Questions_Master.Answer6, Questions_Master.Answer7, Questions_Master.Answer8, " +
               "Questions_Master.Answer9, Questions_Master.Answer10, Questions_Master.Answer11, Questions_Master.Answer12, " +
               "Questions_Master.Answer13, Questions_Master.Answer14, Questions_Master.Answer15, Questions_Master.Answer16," +
               " Questions_Master.Answer17, Questions_Master.Answer18, Questions_Master.Qname_HasImage,"+
               "Questions_Master.Q_type,Exam_Master.Can_Skip,sub_duration FROM ((Exam_Master INNER JOIN Subject_Master ON " +
               "Exam_Master.Exam_Code = Subject_Master.Exam_code) INNER JOIN Test_Master ON Exam_Master.Exam_Code = " +
               "Test_Master.Exam_code) INNER JOIN Questions_Master ON (Exam_Master.Exam_Code = Questions_Master.Exam_code) AND " +
               "(Test_Master.Test_code = Questions_Master.Test_code) AND (Test_Master.Exam_code = Questions_Master.Exam_code)" +
               " AND (Subject_Master.Exam_code = Questions_Master.Exam_code) AND (Subject_Master.Sub_code = Questions_Master.Sub_code) " +
               "WHERE (((Exam_Master.Exam_Code)='" + ExamCode + "') AND ((Test_Master.Test_code)='" + TestCode + "'));";
                init_datatable = ob.fill_data_table(s);
                collection = new DataTable();
                collection = init_datatable.Clone();
                collection.Columns.Add("option1", typeof(int));
                collection.Columns.Add("option2", typeof(int));
                collection.Columns.Add("option3", typeof(int));
                collection.Columns.Add("option4", typeof(int));
                collection.Columns.Add("option5", typeof(int));
                collection.Columns.Add("option6", typeof(int));
                collection.Columns.Add("option7", typeof(int));
                collection.Columns.Add("option8", typeof(int));
                collection.Columns.Add("option9", typeof(int));
                collection.Columns.Add("option10", typeof(int));
                collection.Columns.Add("option11", typeof(int));
                collection.Columns.Add("option12", typeof(int));
                collection.Columns.Add("option13", typeof(int));
                collection.Columns.Add("option14", typeof(int));
                collection.Columns.Add("option15", typeof(int));
                collection.Columns.Add("option16", typeof(int));
                collection.Columns.Add("option17", typeof(int));
                collection.Columns.Add("option18", typeof(int));
                collection.Columns.Add("essay_answer", typeof(string));



                var query = from p in init_datatable.AsEnumerable()
                            group p by new
                            {
                                SubjectCode = p.Field<string>("Sub_code"),
                                SubjectName = p.Field<string>("Sub_Name"),
                                time = p.Field<int>("sub_duration"),
                                CanSkip = p.Field<int>("Can_Skip")
                            } into grp
                            select new
                            {
                                SubjectCode = grp.Key.SubjectCode,
                                SubjectName = grp.Key.SubjectName,
                                time = grp.Key.time,
                                CanSkip = grp.Key.CanSkip
                            };

                Subject = new DataTable("Subject");
                Subject.Columns.Add("SubjectCode", typeof(string));
                Subject.Columns.Add("SubjectName", typeof(string));
                Subject.Columns.Add("time", typeof(int));
                Subject.Columns.Add("CanSkip", typeof(int));
                foreach (var grp in query)
                {
                    DataRow dr = Subject.NewRow();
                    dr["SubjectCode"] = grp.SubjectCode;
                    dr["SubjectName"] = grp.SubjectName;
                    dr["time"] = grp.time;
                    dr["CanSkip"] = grp.CanSkip;
                    Subject.Rows.Add(dr);
                }

                lbl_exam.Text = ExamName;
                lbl_test.Text = TestName;
                string sub_code = Convert.ToString(Subject.Rows[count]["SubjectCode"]);
                time = Convert.ToInt32(Subject.Rows[count]["time"]) * 60;
                bind_data(sub_code);


               }
            else
            {
                timer1.Enabled = false;
                lbl_timer.Visible = false;

                string s = "SELECT Exam_Master.Exam_Code, Subject_Master.Sub_code, Test_Master.Test_code, Exam_Master.Exam_Name, "+
                           "Subject_Master.Sub_Name, Test_Master.Test_Name, Questions_Master.Q_No, Questions_Master.Q_Name, Questions_Master.Answer1, "+
                           "Questions_Master.Answer2, Questions_Master.Answer3, Questions_Master.Answer4, Questions_Master.Answer5, "+
                           "Questions_Master.Answer6, Questions_Master.Answer7, Questions_Master.Answer8, Questions_Master.Answer9, "+
                           "Questions_Master.Answer10, Questions_Master.Answer11, Questions_Master.Answer12, Questions_Master.Answer13, "+
                           "Questions_Master.Answer14, Questions_Master.Answer15, Questions_Master.Answer16, Questions_Master.Answer17, "+
                           "Questions_Master.Answer18, Questions_Master.Qname_HasImage, Questions_Master.Correct_Option, Questions_Master.Q_type, "+
                           "User_Exam_Dtl.Option1, User_Exam_Dtl.Option2, User_Exam_Dtl.Option3, User_Exam_Dtl.Option4, User_Exam_Dtl.Option5, "+
                           "User_Exam_Dtl.Option6, User_Exam_Dtl.Option7, User_Exam_Dtl.Option8, User_Exam_Dtl.Option9, User_Exam_Dtl.Option10, "+
                           "User_Exam_Dtl.Option11, User_Exam_Dtl.Option12, User_Exam_Dtl.Option13, User_Exam_Dtl.Option14, User_Exam_Dtl.Option15, "+
                           "User_Exam_Dtl.Option16, User_Exam_Dtl.Option17, User_Exam_Dtl.Option18, User_Exam_Dtl.Essay_Answer FROM (((Exam_Master INNER "+
                           "JOIN Test_Master ON Exam_Master.Exam_Code = Test_Master.Exam_code) INNER JOIN Subject_Master ON Exam_Master.Exam_Code = "+
                           "Subject_Master.Exam_code) INNER JOIN Questions_Master ON (Exam_Master.Exam_Code = Questions_Master.Exam_code) AND "+
                           "(Test_Master.Test_code = Questions_Master.Test_code) AND (Test_Master.Exam_code = Questions_Master.Exam_code) AND "+
                           "(Subject_Master.Exam_code = Questions_Master.Exam_code) AND (Subject_Master.Sub_code = Questions_Master.Sub_code)) "+
                           "INNER JOIN User_Exam_Dtl ON (Exam_Master.Exam_Code = User_Exam_Dtl.Exam_code) AND (Questions_Master.Q_No = User_Exam_Dtl.Q_No) "+
                           "AND (Questions_Master.Test_code = User_Exam_Dtl.Test_code) AND (Questions_Master.Sub_code = User_Exam_Dtl.Sub_code) AND "+
                           "(Questions_Master.Exam_code = User_Exam_Dtl.Exam_code) WHERE (((Exam_Master.Exam_Code)='"+ ExamCode +"') AND "+
                           "((Test_Master.Test_code)='"+ TestCode +"') AND ((User_Exam_Dtl.User_ID)='"+ class_Application.user_id +"'));";

                init_datatable = ob.fill_data_table(s);


                var query = from p in init_datatable.AsEnumerable()
                            group p by new
                            {
                                SubjectCode = p.Field<string>("Sub_code"),
                                SubjectName = p.Field<string>("Sub_Name"),
                            } into grp
                            select new
                            {
                                SubjectCode = grp.Key.SubjectCode,
                                SubjectName = grp.Key.SubjectName,
                             
                            };

                Subject = new DataTable("Subject");
                Subject.Columns.Add("SubjectCode", typeof(string));
                Subject.Columns.Add("SubjectName", typeof(string));
                foreach (var grp in query)
                {
                    DataRow dr = Subject.NewRow();
                    dr["SubjectCode"] = grp.SubjectCode;
                    dr["SubjectName"] = grp.SubjectName;
                    Subject.Rows.Add(dr);
                }

                lbl_exam.Text = ExamName;
                lbl_test.Text = TestName;
                string sub_code = Convert.ToString(Subject.Rows[count]["SubjectCode"]);
                bind_data_for_review(sub_code);

            }
        }

        private void bind_data_for_review(string subject_code)
        {
            var query = from p in init_datatable.AsEnumerable()
                        where p.Field<string>("Exam_Code").Equals(ExamCode)
                        && p.Field<string>("Test_Code").Equals(TestCode)
                        && p.Field<string>("Sub_Code").Equals(subject_code)
                        select new
                        {
                            ExamCode = p.Field<string>("Exam_Code"),
                            SubjectCode = p.Field<string>("Sub_code"),
                            TestCode = p.Field<string>("Test_code"),
                            ExamName = p.Field<string>("Exam_name"),
                            SubjectName = p.Field<string>("sub_name"),
                            TestName = p.Field<string>("test_name"),
                            QuestionNo = p.Field<int>("Q_No"),
                            QuestionName = p.Field<string>("Q_Name"),
                            Answer1 = p.Field<string>("Answer1"),
                            Answer2 = p.Field<string>("Answer2"),
                            Answer3 = p.Field<string>("Answer3"),
                            Answer4 = p.Field<string>("Answer4"),
                            Answer5 = p.Field<string>("Answer5"),
                            Answer6 = p.Field<string>("Answer6"),
                            Answer7 = p.Field<string>("Answer7"),
                            Answer8 = p.Field<string>("Answer8"),
                            Answer9 = p.Field<string>("Answer9"),
                            Answer10 = p.Field<string>("Answer10"),
                            Answer11 = p.Field<string>("Answer11"),
                            Answer12 = p.Field<string>("Answer12"),
                            Answer13 = p.Field<string>("Answer13"),
                            Answer14 = p.Field<string>("Answer14"),
                            Answer15 = p.Field<string>("Answer15"),
                            Answer16 = p.Field<string>("Answer16"),
                            Answer17 = p.Field<string>("Answer17"),
                            Answer18 = p.Field<string>("Answer18"),
                            Option1 = p.Field<int>("Option1"),
                            Option2 = p.Field<int>("Option2"),
                            Option3 = p.Field<int>("Option3"),
                            Option4 = p.Field<int>("Option4"),
                            Option5 = p.Field<int>("Option5"),
                            Option6 = p.Field<int>("Option6"),
                            Option7 = p.Field<int>("Option7"),
                            Option8 = p.Field<int>("Option8"),
                            Option9 = p.Field<int>("Option9"),
                            Option10 = p.Field<int>("Option10"),
                            Option11 = p.Field<int>("Option11"),
                            Option12 = p.Field<int>("Option12"),
                            Option13 = p.Field<int>("Option13"),
                            Option14 = p.Field<int>("Option14"),
                            Option15 = p.Field<int>("Option15"),
                            Option16 = p.Field<int>("Option16"),
                            Option17 = p.Field<int>("Option17"),
                            Option18 = p.Field<int>("Option18"),
                            QuestionHasImage = p.Field<int>("Qname_HasImage"),
                            QuestionType = p.Field<string>("Q_type"),
                            EssayAns = p.Field<string>("Essay_Answer"),
                            CorrectOption = p.Field<string>("Correct_Option")
                       };

            Question = new DataTable("Question");
            Question = init_datatable.Clone();
            foreach (var grp in query)
            {
                DataRow dr = Question.NewRow();
                dr["Exam_Code"] = grp.ExamCode;
                dr["Sub_code"] = grp.SubjectCode;
                dr["Test_code"] = grp.TestCode;
                dr["Exam_Name"] = grp.ExamName;
                dr["Sub_Name"] = grp.SubjectName;
                dr["Test_Name"] = grp.TestName;
                dr["Q_No"] = grp.QuestionNo;
                dr["Q_Name"] = grp.QuestionName;
                dr["Answer1"] = grp.Answer1;
                dr["Answer2"] = grp.Answer2;
                dr["Answer3"] = grp.Answer3;
                dr["Answer4"] = grp.Answer4;
                dr["Answer5"] = grp.Answer5;
                dr["Answer6"] = grp.Answer6;
                dr["Answer7"] = grp.Answer7;
                dr["Answer8"] = grp.Answer8;
                dr["Answer9"] = grp.Answer9;
                dr["Answer10"] = grp.Answer10;
                dr["Answer11"] = grp.Answer11;
                dr["Answer12"] = grp.Answer12;
                dr["Answer13"] = grp.Answer13;
                dr["Answer14"] = grp.Answer14;
                dr["Answer15"] = grp.Answer15;
                dr["Answer16"] = grp.Answer16;
                dr["Answer17"] = grp.Answer17;
                dr["Answer18"] = grp.Answer18;
                dr["Qname_HasImage"] = grp.QuestionHasImage;
                dr["Q_type"] = grp.QuestionType;
                dr["option1"] = grp.Option1;
                dr["option2"] = grp.Option2;
                dr["option3"] = grp.Option3;
                dr["option4"] = grp.Option4;
                dr["option5"] = grp.Option5;
                dr["option6"] = grp.Option6;
                dr["option7"] = grp.Option7;
                dr["option8"] = grp.Option8;
                dr["option9"] = grp.Option9;
                dr["option10"] = grp.Option10;
                dr["option11"] = grp.Option11;
                dr["option12"] = grp.Option12;
                dr["option13"] = grp.Option13;
                dr["option14"] = grp.Option14;
                dr["option15"] = grp.Option15;
                dr["option16"] = grp.Option16;
                dr["option17"] = grp.Option18;
                dr["option18"] = grp.Option18;
                dr["essay_answer"] = grp.EssayAns;
                dr["Correct_Option"] = grp.CorrectOption;
                Question.Rows.Add(dr);
            }


            Binding objBinding = null;


            objBinding = new Binding("Text", Question, "sub_name");
            lbl_sub.DataBindings.Clear();
            lbl_sub.DataBindings.Add(objBinding);


            objBinding = new Binding("Text", Question, "sub_name");
            lbl_sub.DataBindings.Clear();
            lbl_sub.DataBindings.Add(objBinding);


            objBinding = new Binding("Text", Question, "q_no");
            lblQuestionNo.DataBindings.Clear();
            lblQuestionNo.DataBindings.Add(objBinding);


            //---object binding the essay answer field of question_dataset.Tables[0] with the tesxt box-->
            objBinding = new Binding("Text", Question, "essay_answer");
            richTxtAns.DataBindings.Clear();
            richTxtAns.DataBindings.Add(objBinding);
            //---object binding the essay answer field with the normal text box--->
            objBinding = new Binding("Text", Question, "essay_answer");
            txtAns.DataBindings.Clear();
            txtAns.DataBindings.Add(objBinding);

            //---object binding to groupbox1radio buttons--------------------------->
            objBinding = new Binding("Checked", Question, "Option1");
            rbAns1.DataBindings.Clear();
            rbAns1.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option2");
            rbAns2.DataBindings.Clear();
            rbAns2.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option3");
            rbAns3.DataBindings.Clear();
            rbAns3.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option4");
            rbAns4.DataBindings.Clear();
            rbAns4.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option5");
            rbAns5.DataBindings.Clear();
            rbAns5.DataBindings.Add(objBinding);


            objBinding = new Binding("Checked", Question, "Option6");
            rbAns6.DataBindings.Clear();
            rbAns6.DataBindings.Add(objBinding);

            //--object binding to group box2 of the radio buttons--->

            objBinding = new Binding("Checked", Question, "Option7");
            rbAns7.DataBindings.Clear();
            rbAns7.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option8");
            rbAns8.DataBindings.Clear();
            rbAns8.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option9");
            rbAns9.DataBindings.Clear();
            rbAns9.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option10");
            rbAns10.DataBindings.Clear();
            rbAns10.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option11");
            rbAns11.DataBindings.Clear();
            rbAns11.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option12");
            rbAns12.DataBindings.Clear();
            rbAns12.DataBindings.Add(objBinding);

            //--object binding to group box4 radio buttons--->

            objBinding = new Binding("Checked", Question, "Option13");
            rbAns13.DataBindings.Clear();
            rbAns13.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option14");
            rbAns14.DataBindings.Clear();
            rbAns14.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option15");
            rbAns15.DataBindings.Clear();
            rbAns15.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option16");
            rbAns16.DataBindings.Clear();
            rbAns16.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option17");
            rbAns17.DataBindings.Clear();
            rbAns17.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option18");
            rbAns18.DataBindings.Clear();
            rbAns18.DataBindings.Add(objBinding);

            //--object binding to group box3 check boxes--->

            objBinding = new Binding("Checked", Question, "Option1");
            chkAns1.DataBindings.Clear();
            chkAns1.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option2");
            chkAns2.DataBindings.Clear();
            chkAns2.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option3");
            chkAns3.DataBindings.Clear();
            chkAns3.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option4");
            chkAns4.DataBindings.Clear();
            chkAns4.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option5");
            chkAns5.DataBindings.Clear();
            chkAns5.DataBindings.Add(objBinding);


            objBinding = new Binding("Checked", Question, "Option6");
            chkAns6.DataBindings.Clear();
            chkAns6.DataBindings.Add(objBinding);

            //--object binding to the group box1 lables-->

            objBinding = new Binding("Text", Question, "Answer1");
            lblAns1.DataBindings.Clear();
            lblAns1.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer2");
            lblAns2.DataBindings.Clear();
            lblAns2.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer3");
            lblAns3.DataBindings.Clear();
            lblAns3.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer4");
            lblAns4.DataBindings.Clear();
            lblAns4.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer5");
            lblAns5.DataBindings.Clear();
            lblAns5.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer6");
            lblAns6.DataBindings.Clear();
            lblAns6.DataBindings.Add(objBinding);

            //---object binding to th group box2 lables--->

            objBinding = new Binding("Text", Question, "Answer7");
            lblAns7.DataBindings.Clear();
            lblAns7.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer8");
            lblAns8.DataBindings.Clear();
            lblAns8.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer9");
            lblAns9.DataBindings.Clear();
            lblAns9.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer10");
            lblAns10.DataBindings.Clear();
            lblAns10.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer11");
            lblAns11.DataBindings.Clear();
            lblAns11.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer12");
            lblAns12.DataBindings.Clear();
            lblAns12.DataBindings.Add(objBinding);

            //--object binding to the group box 4 lables--->

            objBinding = new Binding("Text", Question, "Answer13");
            lblAns13.DataBindings.Clear();
            lblAns13.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer14");
            lblAns14.DataBindings.Clear();
            lblAns14.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer15");
            lblAns15.DataBindings.Clear();
            lblAns15.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer16");
            lblAns16.DataBindings.Clear();
            lblAns16.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer17");
            lblAns17.DataBindings.Clear();
            lblAns17.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer18");
            lblAns18.DataBindings.Clear();
            lblAns18.DataBindings.Add(objBinding);

            //---object bindings to the group box 3 lables--->

            objBinding = new Binding("Text", Question, "Answer1");
            lblchkAns1.DataBindings.Clear();
            lblchkAns1.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer2");
            lblchkAns2.DataBindings.Clear();
            lblchkAns2.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer3");
            lblchkAns3.DataBindings.Clear();
            lblchkAns3.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer4");
            lblchkAns4.DataBindings.Clear();
            lblchkAns4.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer5");
            lblchkAns5.DataBindings.Clear();
            lblchkAns5.DataBindings.Add(objBinding);


            objBinding = new Binding("Text", Question, "Answer6");
            lblchkAns6.DataBindings.Clear();
            lblchkAns6.DataBindings.Add(objBinding);



            //---putting the object binding in a currency manager-->
            cur_manager = (CurrencyManager)this.BindingContext[Question];
            //--decidign which panel to be put to visible--->
            check_question_type();
            //---checking if the answer is correct or wrong--->
            check_correct();
            //---clearing the buttons form the flow lay panel-->
            FLPanel1.Controls.Clear();
            //---creating dynamic buttons-->
            Dynamic_Button_Creation();

        }



        //--funtion for creating a datset and binding the datas with the controlls-->
        private void bind_data(string subject_code)
        {       
                   

            var query= from p in init_datatable.AsEnumerable()
                       where p.Field<string>("Exam_Code").Equals(ExamCode) 
                       && p.Field<string>("Test_Code").Equals(TestCode) 
                       && p.Field<string>("Sub_Code").Equals(subject_code)
                       select new
                       {
                           ExamCode=p.Field<string>("Exam_Code"),
                           SubjectCode=p.Field<string>("Sub_code"),
                           TestCode=p.Field<string>("Test_code"),
                           ExamName=p.Field<string>("Exam_name"),
                           SubjectName=p.Field<string>("sub_name"),
                           TestName=p.Field<string>("test_name"),
                           QuestionNo = p.Field<int>("Q_No"),
                           QuestionName = p.Field<string>("Q_Name"),
                           Answer1 = p.Field<string>("Answer1"),
                           Answer2 = p.Field<string>("Answer2"),
                           Answer3 = p.Field<string>("Answer3"),
                           Answer4 = p.Field<string>("Answer4"),
                           Answer5 = p.Field<string>("Answer5"),
                           Answer6 = p.Field<string>("Answer6"),
                           Answer7 = p.Field<string>("Answer7"),
                           Answer8 = p.Field<string>("Answer8"),
                           Answer9 = p.Field<string>("Answer9"),
                           Answer10 = p.Field<string>("Answer10"),
                           Answer11 = p.Field<string>("Answer11"),
                           Answer12 = p.Field<string>("Answer12"),
                           Answer13 = p.Field<string>("Answer13"),
                           Answer14 = p.Field<string>("Answer14"),
                           Answer15 = p.Field<string>("Answer15"),
                           Answer16 = p.Field<string>("Answer16"),
                           Answer17 = p.Field<string>("Answer17"),
                           Answer18 = p.Field<string>("Answer18"),
                           QuestionHasImage = p.Field<int>("Qname_HasImage"),
                           QuestionType = p.Field<string>("Q_type"),
                           CanSkip = p.Field<int>("Can_Skip")
                        };


            Question = new DataTable("Question");
            Question = init_datatable.Clone();
            Question.Columns.Add("option1", typeof(int));
            Question.Columns.Add("option2", typeof(int));
            Question.Columns.Add("option3", typeof(int));
            Question.Columns.Add("option4", typeof(int));
            Question.Columns.Add("option5", typeof(int));
            Question.Columns.Add("option6", typeof(int));
            Question.Columns.Add("option7", typeof(int));
            Question.Columns.Add("option8", typeof(int));
            Question.Columns.Add("option9", typeof(int));
            Question.Columns.Add("option10", typeof(int));
            Question.Columns.Add("option11", typeof(int));
            Question.Columns.Add("option12", typeof(int));
            Question.Columns.Add("option13", typeof(int));
            Question.Columns.Add("option14", typeof(int));
            Question.Columns.Add("option15", typeof(int));
            Question.Columns.Add("option16", typeof(int));
            Question.Columns.Add("option17", typeof(int));
            Question.Columns.Add("option18", typeof(int));
            Question.Columns.Add("essay_answer", typeof(string));
            
            foreach (var grp in query)
            {   DataRow dr = Question.NewRow();
                dr["Exam_Code"] = grp.ExamCode;
                dr["Sub_code"] = grp.SubjectCode;
                dr["Test_code"] = grp.TestCode;
                dr["Exam_Name"] = grp.ExamName;
                dr["Sub_Name"] = grp.SubjectName;
                dr["Test_Name"] = grp.TestName;
                dr["Q_No"] = grp.QuestionNo;
                dr["Q_Name"] = grp.QuestionName;
                dr["Answer1"] = grp.Answer1;
                dr["Answer2"] = grp.Answer2;
                dr["Answer3"] = grp.Answer3;
                dr["Answer4"] = grp.Answer4;
                dr["Answer5"] = grp.Answer5;
                dr["Answer6"] = grp.Answer6;
                dr["Answer7"] = grp.Answer7;
                dr["Answer8"] = grp.Answer8;
                dr["Answer9"] = grp.Answer9;
                dr["Answer10"] = grp.Answer10;
                dr["Answer11"] = grp.Answer11;
                dr["Answer12"] = grp.Answer12;
                dr["Answer13"] = grp.Answer13;
                dr["Answer14"] = grp.Answer14;
                dr["Answer15"] = grp.Answer15;
                dr["Answer16"] = grp.Answer16;
                dr["Answer17"] = grp.Answer17;
                dr["Answer18"] = grp.Answer18;
                dr["Qname_HasImage"] = grp.QuestionHasImage;
                dr["Q_type"] = grp.QuestionType;
                dr["Can_Skip"] = grp.CanSkip;
                dr["option1"] = 0;
                dr["option2"] = 0;
                dr["option3"] = 0;
                dr["option4"] = 0;
                dr["option5"] = 0;
                dr["option6"] = 0;
                dr["option7"] = 0;
                dr["option8"] = 0;
                dr["option9"] = 0;
                dr["option10"] = 0;
                dr["option11"] = 0;
                dr["option12"] = 0;
                dr["option13"] = 0;
                dr["option14"] = 0;
                dr["option15"] = 0;
                dr["option16"] = 0;
                dr["option17"] = 0;
                dr["option18"] = 0;
                dr["essay_answer"] = "";
                Question.Rows.Add(dr);
            }

           


            Binding objBinding = null;


            objBinding = new Binding("Text", Question, "sub_name");
            lbl_sub.DataBindings.Clear();
            lbl_sub.DataBindings.Add(objBinding);


            objBinding = new Binding("Text", Question, "sub_name");
            lbl_sub.DataBindings.Clear();
            lbl_sub.DataBindings.Add(objBinding);


            objBinding = new Binding("Text", Question, "q_no");
            lblQuestionNo.DataBindings.Clear();
            lblQuestionNo.DataBindings.Add(objBinding);

                               
            //---object binding the essay answer field of question_dataset.Tables[0] with the tesxt box-->
            objBinding = new Binding("Text", Question, "essay_answer");
            richTxtAns.DataBindings.Clear();
            richTxtAns.DataBindings.Add(objBinding);
            //---object binding the essay answer field with the normal text box--->
            objBinding = new Binding("Text", Question, "essay_answer");
            txtAns.DataBindings.Clear();
            txtAns.DataBindings.Add(objBinding);

            //---object binding to groupbox1radio buttons--------------------------->
            objBinding = new Binding("Checked", Question, "Option1");
            rbAns1.DataBindings.Clear();
            rbAns1.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option2");
            rbAns2.DataBindings.Clear();
            rbAns2.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option3");
            rbAns3.DataBindings.Clear();
            rbAns3.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option4");
            rbAns4.DataBindings.Clear();
            rbAns4.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option5");
            rbAns5.DataBindings.Clear();
            rbAns5.DataBindings.Add(objBinding);
         

            objBinding = new Binding("Checked", Question, "Option6");
            rbAns6.DataBindings.Clear();
            rbAns6.DataBindings.Add(objBinding);

            //--object binding to group box2 of the radio buttons--->

            objBinding = new Binding("Checked", Question, "Option7");
            rbAns7.DataBindings.Clear();
            rbAns7.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option8");
            rbAns8.DataBindings.Clear();
            rbAns8.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option9");
            rbAns9.DataBindings.Clear();
            rbAns9.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option10");
            rbAns10.DataBindings.Clear();
            rbAns10.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option11");
            rbAns11.DataBindings.Clear();
            rbAns11.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option12");
            rbAns12.DataBindings.Clear();
            rbAns12.DataBindings.Add(objBinding);
            
            //--object binding to group box4 radio buttons--->
            
            objBinding = new Binding("Checked", Question, "Option13");
            rbAns13.DataBindings.Clear();
            rbAns13.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option14");
            rbAns14.DataBindings.Clear();
            rbAns14.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option15");
            rbAns15.DataBindings.Clear();
            rbAns15.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option16");
            rbAns16.DataBindings.Clear();
            rbAns16.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option17");
            rbAns17.DataBindings.Clear();
            rbAns17.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option18");
            rbAns18.DataBindings.Clear();
            rbAns18.DataBindings.Add(objBinding);

            //--object binding to group box3 check boxes--->

            objBinding = new Binding("Checked", Question, "Option1");
            chkAns1.DataBindings.Clear();
            chkAns1.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option2");
            chkAns2.DataBindings.Clear();
            chkAns2.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option3");
            chkAns3.DataBindings.Clear();
            chkAns3.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option4");
            chkAns4.DataBindings.Clear();
            chkAns4.DataBindings.Add(objBinding);

            objBinding = new Binding("Checked", Question, "Option5");
            chkAns5.DataBindings.Clear();
            chkAns5.DataBindings.Add(objBinding);


            objBinding = new Binding("Checked", Question, "Option6");
            chkAns6.DataBindings.Clear();
            chkAns6.DataBindings.Add(objBinding);

            //--object binding to the group box1 lables-->

            objBinding = new Binding("Text", Question, "Answer1");
            lblAns1.DataBindings.Clear();
            lblAns1.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer2");
            lblAns2.DataBindings.Clear();
            lblAns2.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer3");
            lblAns3.DataBindings.Clear();
            lblAns3.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer4");
            lblAns4.DataBindings.Clear();
            lblAns4.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer5");
            lblAns5.DataBindings.Clear();
            lblAns5.DataBindings.Add(objBinding);
            
            objBinding = new Binding("Text", Question, "Answer6");
            lblAns6.DataBindings.Clear();
            lblAns6.DataBindings.Add(objBinding);

            //---object binding to th group box2 lables--->

            objBinding = new Binding("Text", Question, "Answer7");
            lblAns7.DataBindings.Clear();
            lblAns7.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer8");
            lblAns8.DataBindings.Clear();
            lblAns8.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer9");
            lblAns9.DataBindings.Clear();
            lblAns9.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer10");
            lblAns10.DataBindings.Clear();
            lblAns10.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer11");
            lblAns11.DataBindings.Clear();
            lblAns11.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer12");
            lblAns12.DataBindings.Clear();
            lblAns12.DataBindings.Add(objBinding);

            //--object binding to the group box 4 lables--->

            objBinding = new Binding("Text", Question, "Answer13");
            lblAns13.DataBindings.Clear();
            lblAns13.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer14");
            lblAns14.DataBindings.Clear();
            lblAns14.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer15");
            lblAns15.DataBindings.Clear();
            lblAns15.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer16");
            lblAns16.DataBindings.Clear();
            lblAns16.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer17");
            lblAns17.DataBindings.Clear();
            lblAns17.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer18");
            lblAns18.DataBindings.Clear();
            lblAns18.DataBindings.Add(objBinding);
            
            //---object bindings to the group box 3 lables--->

            objBinding = new Binding("Text", Question, "Answer1");
            lblchkAns1.DataBindings.Clear();
            lblchkAns1.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer2");
            lblchkAns2.DataBindings.Clear();
            lblchkAns2.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer3");
            lblchkAns3.DataBindings.Clear();
            lblchkAns3.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer4");
            lblchkAns4.DataBindings.Clear();
            lblchkAns4.DataBindings.Add(objBinding);

            objBinding = new Binding("Text", Question, "Answer5");
            lblchkAns5.DataBindings.Clear();
            lblchkAns5.DataBindings.Add(objBinding);


            objBinding = new Binding("Text", Question, "Answer6");
            lblchkAns6.DataBindings.Clear();
            lblchkAns6.DataBindings.Add(objBinding);

       

            //---putting the object binding in a currency manager-->
            cur_manager = (CurrencyManager)this.BindingContext[Question];
            //--decidign which panel to be put to visible--->
            check_question_type();
            //---clearing the buttons form the flow lay panel-->
            FLPanel1.Controls.Clear();
            //---creating dynamic buttons->
            Dynamic_Button_Creation();
        }


        private void check_question_type()
        {
            if (Convert.ToInt32(Question.Rows[cur_manager.Position]["qname_hasimage"]) == 0)
            {
                flowLayoutPanel1.Visible = false;
                lblQuestion.Visible = true;
                lblQuestion.Text = Convert.ToString(Question.Rows[cur_manager.Position]["q_name"]);
                lblQuestion.Location = new Point(132, 121);
                lblQuestion.Size = new Size(973, 328);
            }
            if (Convert.ToInt32(Question.Rows[cur_manager.Position]["qname_hasimage"]) == 1)
            {
                lblQuestion.Visible = false;
                flowLayoutPanel1.Visible = true;
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\image_path\\" + Question.Rows[cur_manager.Position]["q_name"]);
                flowLayoutPanel1.Location = new Point(132, 121);
                flowLayoutPanel1.Size = new Size(973, 328);
            }


            switch (Convert.ToInt32(Question.Rows[cur_manager.Position]["q_type"]))
            {
                case 1:
                   
                    groupBox1.Visible = true;
                    groupBox5.Visible = false;
                    richTxtAns.Visible = false;
                    groupBox3.Visible = false;
                    groupBox2.Visible = false;
                    groupBox4.Visible = false;
                    
                    groupBox1.Location = new Point(3, 10);
                    groupBox1.Size = new Size(952, 198);

                    if (class_Application.REVIEW == 1)
                    {
                        lblAns1RightImg.Location = new Point(836, 26);
                        lblAns2RightImg.Location = new Point(836, 55);
                        lblAns3RightImg.Location = new Point(836, 84);
                        lblAns4RightImg.Location = new Point(836, 114);
                        lblAns5RightImg.Location = new Point(836, 141);
                        lblAns6RightImg.Location = new Point(836, 174);

                        lblAns1WrngImg.Location = new Point(892, 26);
                        lblAns2WrngImg.Location = new Point(892, 55);
                        lblAns3WrngImg.Location = new Point(892, 81);
                        lblAns4WrngImg.Location = new Point(892, 108);
                        lblAns5WrngImg.Location = new Point(892, 140);
                        lblAns6WrngImg.Location = new Point(892, 173);

                    }
                    else
                    {
                        lblAns1RightImg.Visible = false;
                        lblAns2RightImg.Visible = false;
                        lblAns3RightImg.Visible = false;
                        lblAns4RightImg.Visible = false;
                        lblAns5RightImg.Visible = false;
                        lblAns6RightImg.Visible = false;


                        lblAns1WrngImg.Visible = false;
                        lblAns2WrngImg.Visible = false;
                        lblAns3WrngImg.Visible = false;
                        lblAns4WrngImg.Visible = false;
                        lblAns5WrngImg.Visible = false;
                        lblAns6WrngImg.Visible = false;
                      

                    }
                    
                    break;
                case 2:
                 
                    groupBox1.Visible = false;
                    groupBox5.Visible = false;
                    richTxtAns.Visible = false;
                    groupBox3.Visible = true;
                    groupBox2.Visible = false; 
                    groupBox4.Visible = false;
                    
                    groupBox3.Location = new Point(3, 10);
                    groupBox3.Size = new Size(952, 198);

                    if (class_Application.REVIEW == 1)
                    {
                        
                    }
                    else
                    {
                        lblChkAns1RgtImg.Visible = false;
                        lblChkAns2RgtImg.Visible = false;
                        lblChkAns3RgtImg.Visible = false;
                        lblChkAns4RgtImg.Visible = false;
                        lblChkAns5RgtImg.Visible = false;
                        lblChkAns6RgtImg.Visible = false;

                        lblChkAns1WrngImg.Visible = false;
                        lblChkAns2WrngImg.Visible = false;
                        lblChkAns3WrngImg.Visible = false;
                        lblChkAns4WrngImg.Visible = false;
                        lblChkAns5WrngImg.Visible = false;
                        lblChkAns6WrngImg.Visible = false;
                    }


                   
                    
                    break;
                case 3:

                    groupBox5.Visible = true;
                    groupBox1.Visible = false;
                    richTxtAns.Visible = false;
                    groupBox3.Visible = false;
                    groupBox2.Visible = false;
                    groupBox4.Visible = false;

                    if (class_Application.REVIEW == 1)
                    {
                        TxtAnsRightImg.Visible = true;
                        TxtAnsWrngImg.Visible = true;
                    }
                    else
                    {
                        TxtAnsRightImg.Visible = false;
                        TxtAnsWrngImg.Visible = false;
                    }

  
                    break;
                case 4:

                    groupBox1.Visible = true;
                    groupBox5.Visible = false;
                    richTxtAns.Visible = false;
                    groupBox3.Visible = false;
                    groupBox2.Visible = true;
                    groupBox4.Visible = false;
                    
                    
                    groupBox1.Location = new Point(3, 3);
                    groupBox1.Size = new Size(478, 200);
                    groupBox2.Location = new Point(483, 3);
                    groupBox2.Size = new Size(478, 200);



                    if (class_Application.REVIEW == 1)
                    {
                        lblAns1RightImg.Location = new Point(356, 27);
                        lblAns2RightImg.Location = new Point(356, 56);
                        lblAns3RightImg.Location = new Point(356, 85);
                        lblAns4RightImg.Location = new Point(356, 115);
                        lblAns5RightImg.Location = new Point(356, 142);
                        lblAns6RightImg.Location = new Point(356, 175);
                        lblAns7RightImg.Location = new Point(365, 26);
                        lblAns8RightImg.Location = new Point(365, 57);
                        lblAns9RightImg.Location = new Point(365, 86);
                        lblAns10RightImg.Location = new Point(365, 118);
                        lblAns11RightImg.Location = new Point(365, 143);
                        lblAns12RightImg.Location = new Point(365, 173);

                       
                        lblAns1WrngImg.Location = new Point(406, 28);
                        lblAns2WrngImg.Location = new Point(406, 56);
                        lblAns3WrngImg.Location = new Point(406, 83);
                        lblAns4WrngImg.Location = new Point(406, 112);
                        lblAns5WrngImg.Location = new Point(406, 142);
                        lblAns6WrngImg.Location = new Point(406, 175);
                        lblAns7WrngImg.Location = new Point(428, 26);
                        lblAns8WrngImg.Location = new Point(428, 57);
                        lblAns9WrngImg.Location = new Point(428, 86);
                        lblAns10WrngImg.Location = new Point(428, 118);
                        lblAns11WrngImg.Location = new Point(428, 143);
                        lblAns12WrngImg.Location = new Point(428, 173);



                    }
                    else
                    {
                        lblAns1RightImg.Visible = false;
                        lblAns2RightImg.Visible = false;
                        lblAns3RightImg.Visible = false;
                        lblAns4RightImg.Visible = false;
                        lblAns5RightImg.Visible = false;
                        lblAns6RightImg.Visible = false;
                        lblAns7RightImg.Visible = false;
                        lblAns8RightImg.Visible = false;
                        lblAns9RightImg.Visible = false;
                        lblAns10RightImg.Visible = false;
                        lblAns11RightImg.Visible = false;
                        lblAns12RightImg.Visible = false;

                        lblAns1WrngImg.Visible = false;
                        lblAns2WrngImg.Visible = false;
                        lblAns3WrngImg.Visible = false;
                        lblAns4WrngImg.Visible = false;
                        lblAns5WrngImg.Visible = false;
                        lblAns6WrngImg.Visible = false;
                        lblAns7WrngImg.Visible = false;
                        lblAns8WrngImg.Visible = false;
                        lblAns9WrngImg.Visible = false;
                        lblAns10WrngImg.Visible = false;
                        lblAns11WrngImg.Visible = false;
                        lblAns12WrngImg.Visible = false;
   
                    }


                    break;
                case 5:

                    groupBox1.Visible = true;
                    groupBox5.Visible = false;
                    richTxtAns.Visible = false;
                    groupBox3.Visible = false;
                    groupBox2.Visible = true;
                    groupBox4.Visible = true;
                 

                    groupBox1.Location = new Point(5, 9);
                    groupBox1.Size = new Size(305, 192);
                    groupBox2.Location = new Point(331, 9);
                    groupBox2.Size = new Size(305, 192);
                    groupBox4.Location = new Point(656, 9);
                    groupBox4.Size = new Size(305, 192);



                    if (class_Application.REVIEW == 1)
                    {
                        lblAns1RightImg.Location = new Point(207, 24);
                        lblAns2RightImg.Location = new Point(207, 53);
                        lblAns3RightImg.Location = new Point(207, 82);
                        lblAns4RightImg.Location = new Point(207, 112);
                        lblAns5RightImg.Location = new Point(207, 139);
                        lblAns6RightImg.Location = new Point(207, 172);
                        lblAns7RightImg.Location = new Point(189, 21);
                        lblAns8RightImg.Location = new Point(189, 52);
                        lblAns9RightImg.Location = new Point(189, 81);
                        lblAns10RightImg.Location = new Point(189, 113);
                        lblAns11RightImg.Location = new Point(189, 138);
                        lblAns12RightImg.Location = new Point(189, 168);


                        lblAns1WrngImg.Location = new Point(255, 25);
                        lblAns2WrngImg.Location = new Point(257, 53);
                        lblAns3WrngImg.Location = new Point(257, 80);
                        lblAns4WrngImg.Location = new Point(257, 109);
                        lblAns5WrngImg.Location = new Point(257, 139);
                        lblAns6WrngImg.Location = new Point(257, 172);
                        lblAns7WrngImg.Location = new Point(252, 21);
                        lblAns8WrngImg.Location = new Point(252, 52);
                        lblAns9WrngImg.Location = new Point(252, 81);
                        lblAns10WrngImg.Location = new Point(252, 113);
                        lblAns11WrngImg.Location = new Point(252, 138);
                        lblAns12WrngImg.Location = new Point(252, 168);


                    }
                    else
                    {
                        lblAns1RightImg.Visible = false;
                        lblAns2RightImg.Visible = false;
                        lblAns3RightImg.Visible = false;
                        lblAns4RightImg.Visible = false;
                        lblAns5RightImg.Visible = false;
                        lblAns6RightImg.Visible = false;
                        lblAns7RightImg.Visible = false;
                        lblAns8RightImg.Visible = false;
                        lblAns9RightImg.Visible = false;
                        lblAns10RightImg.Visible = false;
                        lblAns11RightImg.Visible = false;
                        lblAns12RightImg.Visible = false;
                        lblAns13RightImg.Visible = false;
                        lblAns14RightImg.Visible = false;
                        lblAns15RightImg.Visible = false;
                        lblAns16RightImg.Visible = false;
                        lblAns17RightImg.Visible = false;
                        lblAns18RightImg.Visible = false;

                        lblAns1WrngImg.Visible = false;
                        lblAns2WrngImg.Visible = false;
                        lblAns3WrngImg.Visible = false;
                        lblAns4WrngImg.Visible = false;
                        lblAns5WrngImg.Visible = false;
                        lblAns6WrngImg.Visible = false;
                        lblAns7WrngImg.Visible = false;
                        lblAns8WrngImg.Visible = false;
                        lblAns9WrngImg.Visible = false;
                        lblAns10WrngImg.Visible = false;
                        lblAns11WrngImg.Visible = false;
                        lblAns12WrngImg.Visible = false;
                        lblAns13WrngImg.Visible = false;
                        lblAns14WrngImg.Visible = false;
                        lblAns15WrngImg.Visible = false;
                        lblAns16WrngImg.Visible = false;
                        lblAns17WrngImg.Visible = false;
                        lblAns18WrngImg.Visible = false;
                    }
                    break;

                case 6:

                    groupBox1.Visible = false;
                    groupBox5.Visible = false;
                    richTxtAns.Visible = true;
                    groupBox3.Visible = false;
                    groupBox2.Visible = false;
                    groupBox4.Visible = false;
                
                    richTxtAns.Location = new Point(3, 10);
                    richTxtAns.Size = new Size(952, 198);
                    break;

            }



        }

  



        //--button click event for the next button-->
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (class_Application.REVIEW == 0)
            {
                if (Convert.ToInt32(Subject.Rows[count]["CanSkip"]) == 1)
                {
                    cur_manager.Position += 1;
                    check_question_type();
                }
                else if (Convert.ToInt32(Subject.Rows[count]["CanSkip"]) == 0)
                {
                    if (Convert.ToInt32(Question.Rows[cur_manager.Position]["option1"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option2"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option3"]) == 1 ||
                         Convert.ToInt32(Question.Rows[cur_manager.Position]["option4"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option5"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option6"]) == 1 ||
                         Convert.ToInt32(Question.Rows[cur_manager.Position]["option7"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option8"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option9"]) == 1 ||
                         Convert.ToInt32(Question.Rows[cur_manager.Position]["option10"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option11"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option12"]) == 1 ||
                         Convert.ToInt32(Question.Rows[cur_manager.Position]["option13"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option14"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option15"]) == 1 ||
                         Convert.ToInt32(Question.Rows[cur_manager.Position]["option16"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option17"]) == 1 || Convert.ToInt32(Question.Rows[cur_manager.Position]["option18"]) == 1 ||
                         txtAns.Text != "" || richTxtAns.Text != "")
                    {
                        cur_manager.Position += 1;
                        check_question_type();
                    }
                }
            }
            else
            {
                cur_manager.Position += 1;
                check_question_type();
                check_correct();
            }
         
        
        }

        //--button click event for the previous button--->
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            cur_manager.Position -= 1;
            check_question_type();
            if(class_Application.REVIEW==1){
                check_correct();
            }

        }


        
        //---submit button click event-->
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (class_Application.REVIEW == 1)
            {
                count++;
                string SubjectCode = Convert.ToString(Subject.Rows[count][0]);
                bind_data_for_review(SubjectCode);
               
            }
            else submit();
        }


        private void check_correct()
        {
            if (Convert.ToInt32(Question.Rows[cur_manager.Position]["q_type"]) != 3)
            {
                String[] Correct_Option = Convert.ToString(Question.Rows[cur_manager.Position]["Correct_Option"]).Split(',');
                ArrayList other_options = new ArrayList();

                for (int count = 1; count <= 18; count++)
                {
                    var query = from p in Correct_Option.AsEnumerable()
                                where p.Equals(Convert.ToString(count))
                                select p;
                    if (query.Count() == 0)
                    {
                        other_options.Add(Convert.ToString(count));
                    }
                }

                string[] _other_options = (string[])other_options.ToArray(typeof(string));

                for (int i = 0; i < Correct_Option.Length; i++)
                {
                    switch (Convert.ToInt32(Correct_Option[i]))
                    {
                        case 1:
                            lblAns1RightImg.Visible = true;
                            lblChkAns1RgtImg.Visible = true;
                            break;
                        case 2:
                            lblAns2RightImg.Visible = true;
                            lblChkAns2RgtImg.Visible = true;
                            break;
                        case 3:
                            lblAns3RightImg.Visible = true;
                            lblChkAns3RgtImg.Visible = true;
                            break;
                        case 4:
                            lblAns4RightImg.Visible = true;
                            lblChkAns4RgtImg.Visible = true;
                            break;
                        case 5:
                            lblAns5RightImg.Visible = true;
                            lblChkAns5RgtImg.Visible = true;
                            break;
                        case 6:
                            lblAns6RightImg.Visible = true;
                            lblChkAns6RgtImg.Visible = true;
                            break;
                        case 7:
                            lblAns7RightImg.Visible = true;
                            break;
                        case 8:
                            lblAns8RightImg.Visible = true;
                            break;
                        case 9:
                            lblAns9RightImg.Visible = true;
                            break;
                        case 10:
                            lblAns10RightImg.Visible = true;
                            break;
                        case 11:
                            lblAns11RightImg.Visible = true;
                            break;
                        case 12:
                            lblAns12RightImg.Visible = true;
                            break;
                        case 13:
                            lblAns13RightImg.Visible = true;
                            break;
                        case 14:
                            lblAns14RightImg.Visible = true;
                            break;
                        case 15:
                            lblAns15RightImg.Visible = true;
                            break;
                        case 16:
                            lblAns16RightImg.Visible = true;
                            break;
                        case 17:
                            lblAns17RightImg.Visible = true;
                            break;
                        case 18:
                            lblAns18RightImg.Visible = true;
                            break;
                    }
                }


                for (int i = 0; i < _other_options.Length; i++)
                {
                    switch (Convert.ToInt32(_other_options[i]))
                    {
                        case 1:
                            lblAns1RightImg.Visible = false;
                            lblChkAns1RgtImg.Visible = false;
                            break;
                        case 2:
                            lblAns2RightImg.Visible = false;
                            lblChkAns2RgtImg.Visible = false;
                            break;
                        case 3:
                            lblAns3RightImg.Visible = false;
                            lblChkAns3RgtImg.Visible = false;
                            break;
                        case 4:
                            lblAns4RightImg.Visible = false;
                            lblChkAns4RgtImg.Visible = false;
                            break;
                        case 5:
                            lblAns5RightImg.Visible = false;
                            lblChkAns5RgtImg.Visible = false;
                            break;
                        case 6:
                            lblAns6RightImg.Visible = false;
                            lblChkAns6RgtImg.Visible = false;
                            break;
                        case 7:
                            lblAns7RightImg.Visible = false;
                            break;
                        case 8:
                            lblAns8RightImg.Visible = false;
                            break;
                        case 9:
                            lblAns9RightImg.Visible = false;
                            break;
                        case 10:
                            lblAns10RightImg.Visible = false;
                            break;
                        case 11:
                            lblAns11RightImg.Visible = false;
                            break;
                        case 12:
                            lblAns12RightImg.Visible = false;
                            break;
                        case 13:
                            lblAns13RightImg.Visible = false;
                            break;
                        case 14:
                            lblAns14RightImg.Visible = false;
                            break;
                        case 15:
                            lblAns15RightImg.Visible = false;
                            break;
                        case 16:
                            lblAns16RightImg.Visible = false;
                            break;
                        case 17:
                            lblAns17RightImg.Visible = false;
                            break;
                        case 18:
                            lblAns18RightImg.Visible = false;
                            break;
                    }
                }



                ArrayList chk = new ArrayList();
                if (rbAns1.Checked == true || chkAns1.Checked == true) chk.Add("1");
                if (rbAns2.Checked == true || chkAns2.Checked == true) chk.Add("2");
                if (rbAns3.Checked == true || chkAns3.Checked == true) chk.Add("3");
                if (rbAns4.Checked == true || chkAns4.Checked == true) chk.Add("4");
                if (rbAns5.Checked == true || chkAns5.Checked == true) chk.Add("5");
                if (rbAns6.Checked == true || chkAns6.Checked == true) chk.Add("6");
                if (rbAns7.Checked == true) chk.Add("7");
                if (rbAns8.Checked == true) chk.Add("8");
                if (rbAns9.Checked == true) chk.Add("9");
                if (rbAns10.Checked == true) chk.Add("10");
                if (rbAns11.Checked == true) chk.Add("11");
                if (rbAns12.Checked == true) chk.Add("12");
                if (rbAns13.Checked == true) chk.Add("13");
                if (rbAns14.Checked == true) chk.Add("14");
                if (rbAns15.Checked == true) chk.Add("15");
                if (rbAns16.Checked == true) chk.Add("16");
                if (rbAns17.Checked == true) chk.Add("17");
                if (rbAns18.Checked == true) chk.Add("18");
                string[] _chk = (string[])chk.ToArray(typeof(string));


                ArrayList unticked = new ArrayList();
                for (int count = 1; count <= 18; count++)
                {
                    var query2 = from p in _chk.AsEnumerable()
                                 where p.Equals(Convert.ToString(count))
                                 select p;
                    if (query2.Count() == 0)
                    {
                        unticked.Add(Convert.ToString(count));
                    }
                }
                string[] _unticked = (string[])unticked.ToArray(typeof(string));

                for (int i = 0; i < _chk.Length; i++)
                {
                    switch (Convert.ToInt32(_chk[i]))
                    {
                        case 1:
                            lblAns1WrngImg.Visible = true;
                            lblChkAns1WrngImg.Visible = true;
                            if (lblAns1RightImg.Visible == true || lblChkAns1RgtImg.Visible == true)
                            {
                                lblAns1WrngImg.Visible = false;
                                lblChkAns1WrngImg.Visible = false;
                            }
                            break;
                        case 2:
                            lblAns2WrngImg.Visible = true;
                            lblChkAns2WrngImg.Visible = true;
                            if (lblAns2RightImg.Visible == true || lblChkAns2RgtImg.Visible == true)
                            {
                                lblAns2WrngImg.Visible = false;
                                lblChkAns2WrngImg.Visible = false;
                            }
                            break;
                        case 3:
                            lblAns3WrngImg.Visible = true;
                            lblChkAns3WrngImg.Visible = true;
                            if (lblAns3RightImg.Visible == true || lblChkAns3RgtImg.Visible == true)
                            {
                                lblAns3WrngImg.Visible = false;
                                lblChkAns3WrngImg.Visible = false;
                            }
                            break;
                        case 4:
                            lblAns4WrngImg.Visible = true;
                            lblChkAns4WrngImg.Visible = true;
                            if (lblAns4RightImg.Visible == true || lblChkAns4RgtImg.Visible == true)
                            {
                                lblAns4WrngImg.Visible = false;
                                lblChkAns4WrngImg.Visible = false;
                            }
                            break;
                        case 5:
                            lblAns5WrngImg.Visible = true;
                            lblChkAns5WrngImg.Visible = true;
                            if (lblAns5RightImg.Visible == true || lblChkAns5RgtImg.Visible == true)
                            {
                                lblAns5WrngImg.Visible = false;
                                lblChkAns5WrngImg.Visible = false;
                            }
                            break;
                        case 6:
                            lblAns6WrngImg.Visible = true;
                            lblChkAns6WrngImg.Visible = true;
                            if (lblAns6RightImg.Visible == true || lblChkAns6RgtImg.Visible == true)
                            {
                                lblAns6WrngImg.Visible = false;
                                lblChkAns6WrngImg.Visible = false;
                            }
                            break;
                        case 7:
                            lblAns7WrngImg.Visible = true;
                            if (lblAns7RightImg.Visible == true) lblAns7WrngImg.Visible = false;
                            break;
                        case 8:
                            lblAns8WrngImg.Visible = true;
                            if (lblAns8RightImg.Visible == true) lblAns8WrngImg.Visible = false;
                            break;
                        case 9:
                            lblAns9WrngImg.Visible = true;
                            if (lblAns9RightImg.Visible == true) lblAns9WrngImg.Visible = false;
                            break;
                        case 10:
                            lblAns10WrngImg.Visible = true;
                            if (lblAns10RightImg.Visible == true) lblAns10WrngImg.Visible = false;
                            break;
                        case 11:
                            lblAns11WrngImg.Visible = true;
                            if (lblAns11RightImg.Visible == true) lblAns11WrngImg.Visible = false;
                            break;
                        case 12:
                            lblAns12WrngImg.Visible = true;
                            if (lblAns12RightImg.Visible == true) lblAns12WrngImg.Visible = false;
                            break;
                        case 13:
                            lblAns13WrngImg.Visible = true;
                            if (lblAns13RightImg.Visible == true) lblAns13WrngImg.Visible = false;
                            break;
                        case 14:
                            lblAns14WrngImg.Visible = true;
                            if (lblAns14RightImg.Visible == true) lblAns14WrngImg.Visible = false;
                            break;
                        case 15:
                            lblAns15WrngImg.Visible = true;
                            if (lblAns15RightImg.Visible == true) lblAns15WrngImg.Visible = false;
                            break;
                        case 16:
                            lblAns16WrngImg.Visible = true;
                            if (lblAns16RightImg.Visible == true) lblAns16WrngImg.Visible = false;
                            break;
                        case 17:
                            lblAns17WrngImg.Visible = true;
                            if (lblAns17RightImg.Visible == true) lblAns17WrngImg.Visible = false;
                            break;
                        case 18:
                            lblAns18WrngImg.Visible = true;
                            if (lblAns18RightImg.Visible == true) lblAns18WrngImg.Visible = false;
                            break;
                    }
                }



                for (int i = 0; i < _unticked.Length; i++)
                {
                    switch (Convert.ToInt32(_unticked[i]))
                    {
                        case 1:
                            lblAns1WrngImg.Visible = false;
                            lblChkAns1WrngImg.Visible = false;
                            break;
                        case 2:
                            lblAns2WrngImg.Visible = false;
                            lblChkAns2WrngImg.Visible = false;
                            break;
                        case 3:
                            lblAns3WrngImg.Visible = false;
                            lblChkAns3WrngImg.Visible = false;
                            break;
                        case 4:
                            lblAns4WrngImg.Visible = false;
                            lblChkAns4WrngImg.Visible = false;
                            break;
                        case 5:
                            lblAns5WrngImg.Visible = false;
                            lblChkAns5WrngImg.Visible = false;
                            break;
                        case 6:
                            lblAns6WrngImg.Visible = false;
                            lblChkAns6WrngImg.Visible = false;
                            break;
                        case 7:
                            lblAns7WrngImg.Visible = false;
                            break;
                        case 8:
                            lblAns8WrngImg.Visible = false;
                            break;
                        case 9:
                            lblAns9WrngImg.Visible = false;
                            break;
                        case 10:
                            lblAns10WrngImg.Visible = false;
                            break;
                        case 11:
                            lblAns11WrngImg.Visible = false;
                            break;
                        case 12:
                            lblAns12WrngImg.Visible = false;
                            break;
                        case 13:
                            lblAns13WrngImg.Visible = false;
                            break;
                        case 14:
                            lblAns14WrngImg.Visible = false;
                            break;
                        case 15:
                            lblAns15WrngImg.Visible = false;
                            break;
                        case 16:
                            lblAns16WrngImg.Visible = false;
                            break;
                        case 17:
                            lblAns17WrngImg.Visible = false;
                            break;
                        case 18:
                            lblAns18WrngImg.Visible = false;
                            break;
                    }
                }
            }
            else if (Convert.ToInt32(Question.Rows[cur_manager.Position]["q_type"]) == 3)
            {
                String Cor_Option = Convert.ToString(Question.Rows[cur_manager.Position]["Correct_Option"]);
                String Given_Option = Convert.ToString(Question.Rows[cur_manager.Position]["Essay_Answer"]);

                TxtAnsRightImg.Visible = false;
                TxtAnsWrngImg.Visible = false;

                if (Cor_Option.Equals(Given_Option) && Given_Option!="")
                {
                    TxtAnsRightImg.Visible = true;
                    TxtAnsWrngImg.Visible = false;

                }
                else if(!Cor_Option.Equals(Given_Option) && Given_Option!="")
                {
                    TxtAnsRightImg.Visible = false;
                    TxtAnsWrngImg.Visible = true;
                }

            }

            
        }

        private void submit()
        {
            //--refreshing the form--->
            count++;
            if (count < Subject.Rows.Count)
            {


                foreach (DataRow dr in Question.Rows)
                {
                    DataRow collection_row = collection.NewRow();
                    collection_row["Exam_Code"] = dr["Exam_Code"];
                    collection_row["Sub_code"] = dr["Sub_code"];
                    collection_row["Test_code"] = dr["Test_code"];
                    collection_row["Exam_Name"] = dr["Exam_Name"];
                    collection_row["Sub_Name"] = dr["Sub_Name"];
                    collection_row["Test_Name"] = dr["Test_Name"];
                    collection_row["Q_No"] = dr["Q_No"];
                    collection_row["option1"] = dr["option1"];
                    collection_row["option2"] = dr["option2"];
                    collection_row["option3"] = dr["option3"];
                    collection_row["option4"] = dr["option4"];
                    collection_row["option5"] = dr["option5"];
                    collection_row["option6"] = dr["option6"];
                    collection_row["option7"] = dr["option7"];
                    collection_row["option8"] = dr["option8"];
                    collection_row["option9"] = dr["option9"];
                    collection_row["option10"] = dr["option10"];
                    collection_row["option11"] = dr["option11"];
                    collection_row["option12"] = dr["option12"];
                    collection_row["option13"] = dr["option13"];
                    collection_row["option14"] = dr["option14"];
                    collection_row["option15"] = dr["option15"];
                    collection_row["option16"] = dr["option16"];
                    collection_row["option17"] = dr["option17"];
                    collection_row["option18"] = dr["option18"];
                    collection_row["essay_answer"] = dr["essay_answer"];
                    collection.Rows.Add(collection_row);
                }

                //--binding the data with a new subject-->
                string SubjectCode = Convert.ToString(Subject.Rows[count][0]);
                time = Convert.ToInt32(Subject.Rows[count][2]) * 60;
                bind_data(SubjectCode);
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                foreach (DataRow dr in Question.Rows)
                {
                    DataRow collection_row = collection.NewRow();
                    collection_row["Exam_Code"] = dr["Exam_Code"];
                    collection_row["Sub_code"] = dr["Sub_code"];
                    collection_row["Test_code"] = dr["Test_code"];
                    collection_row["Exam_Name"] = dr["Exam_Name"];
                    collection_row["Sub_Name"] = dr["Sub_Name"];
                    collection_row["Test_Name"] = dr["Test_Name"];
                    collection_row["Q_No"] = dr["Q_No"];
                    collection_row["option1"] = dr["option1"];
                    collection_row["option2"] = dr["option2"];
                    collection_row["option3"] = dr["option3"];
                    collection_row["option4"] = dr["option4"];
                    collection_row["option5"] = dr["option5"];
                    collection_row["option6"] = dr["option6"];
                    collection_row["option7"] = dr["option7"];
                    collection_row["option8"] = dr["option8"];
                    collection_row["option9"] = dr["option9"];
                    collection_row["option10"] = dr["option10"];
                    collection_row["option11"] = dr["option11"];
                    collection_row["option12"] = dr["option12"];
                    collection_row["option13"] = dr["option13"];
                    collection_row["option14"] = dr["option14"];
                    collection_row["option15"] = dr["option15"];
                    collection_row["option16"] = dr["option16"];
                    collection_row["option17"] = dr["option17"];
                    collection_row["option18"] = dr["option18"];
                    collection_row["essay_answer"] = dr["essay_answer"];
                    collection.Rows.Add(collection_row);
                }
                string s = "delete from user_exam_dtl where user_id='" + class_Application.user_id + "' and exam_code='" + ExamCode + "' and test_code='" + TestCode + "'";
                ob.execute_non_query(s);
                foreach (DataRow dr in collection.Rows)
                {
                    s = null;
                    s = "insert into user_exam_dtl(user_id,exam_code,sub_code,test_code,q_no,option1,option2," +
                        "option3,option4,option5,option6,option7,option8,option9,option10,option11,option12,option13" +
                        ",option14,option15,option16,option17,option18,essay_answer,user_name)" + 
                        "values('" + class_Application.user_id + "','" + ExamCode +
                        "','" + dr["sub_code"] + "','" + TestCode + "','" + dr["Q_No"] + "','" +
                        dr["option1"] + "'," + dr["option2"] + ",'" + dr["option3"] + "','" + dr["option4"] + "','" +
                        dr["option5"] + "','" + dr["option6"] + "','" + dr["option7"] + "','" + dr["option8"] + "','" +
                        dr["option9"] + "','" + dr["option10"] + "','" + dr["option11"] + "','" + dr["option12"] + "','" +
                        dr["option13"] + "','" + dr["option14"] + "','" + dr["option15"] + "','" + dr["option16"] + "','" + dr["option17"] + "','" +
                        dr["option18"] + "','" + dr["essay_answer"] + "','"+ class_Application.user_name +"')";
                    ob.execute_non_query(s);
                }

                MessageBox.Show("Exam Over");
                this.Close();
            }
        }







        //---function for creating the dynamic buttons-->
        private void Dynamic_Button_Creation()
        {

            foreach (DataRow dr in Question.Rows)
            {
                Button obj_button = new Button();
                obj_button.Name = "btn_"+Convert.ToString(dr["q_no"]);
                obj_button.Text= Convert.ToString(dr["q_no"]);
                obj_button.Size = new System.Drawing.Size(30, 23);
                obj_button.Click += new EventHandler(button_click);
                FLPanel1.Controls.Add(obj_button);
          }
       }

        //---adding click events to the dyanmic buttons created--->
        private void button_click(object sender,EventArgs e)
        {   Button button = (Button)sender;
            cur_manager.Position = Convert.ToInt32(button.Text.ToString())-1;
            check_question_type();

        }


        //timer tick event--->
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                timer1.Stop();
                timer1.Enabled = false;
                MessageBox.Show("section over");
                submit();
            }

            int hour = time / 3600;
            int minute = (time % 3600) / 60;
            int second = (time % 3600) % 60;
            lbl_timer.Text  = Convert.ToString(hour) + ":" + Convert.ToString(minute) + ":" + Convert.ToString(second);
            time --;

        }

       

   

       
     

   

   



    }
}
