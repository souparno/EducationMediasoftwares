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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        DataSet ds;
        static class_Application ob;
        usr_cntrl[] obj_UsrCntrl;
        TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;

        private void Form1_Load(object sender, EventArgs e)
        {
            ob = new class_Application();
            MyTabs();
        }




        private void MyTabs()
        {

            string s = "SELECT Test_Master.Exam_code, Test_Master.Test_code, Exam_Master.Exam_Name," +
                " Test_Master.Test_Name, Test_Master.Test_Duration" +
                " FROM Exam_Master INNER JOIN Test_Master ON Exam_Master.Exam_Code = Test_Master.Exam_code;";
            ds = ob.fill_data_set(s);

            var query = from p in ds.Tables[0].AsEnumerable()
                        group p by new{
                            ExamCode = p.Field<string>(0),
                            ExamName = p.Field<string>(2)
                                       }into grp
                        select new
                        {
                            ExamCode = grp.Key.ExamCode,
                            ExamName = grp.Key.ExamName 
                        };

            obj_UsrCntrl = new usr_cntrl[query.Count()];
            //--creating an instance of the tabcontroller-->
            tabControl1 = new TabControl();
            foreach (var grp in query)
            {   //Invokes the TabPage() constructor to create the tabPage1. 
                tabPage1 = new System.Windows.Forms.TabPage();
                tabPage1.Text = grp.ExamName;
                tabPage1.Name = grp.ExamCode;
                //FlowLayoutPanel FlPanel = bind_data(grp.subjectcode);
                FlowLayoutPanel FlPanel = new FlowLayoutPanel();
                tabPage1.Controls.Add(FlPanel);
                tabControl1.Controls.AddRange(new Control[] { this.tabPage1 });
            }


            //---definign the tabcontrol location-->
            tabControl1.Location = new Point(12, 30);
            //--definig the tabcontrol size--->
            tabControl1.Size = new Size(547, 397);
            //---this code is for adding the tab control and the button to the form-->
            Controls.AddRange(new Control[] { tabControl1 });
              //---declares the size of the form-->
            ClientSize = new Size(579, 483);
        }

        //----putting buttons in the flow lay panel
        private void bind_data(string ExamCode)
        {
            var query = from p in ds.Tables[0].AsEnumerable()
                        where p.Field<string>(2).Equals(ExamCode)
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
                            ImagePath = p.Field<string>(11)
                        };
        }



        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(tabControl1.SelectedTab.Text));
            MessageBox.Show(Convert.ToString(tabControl1.SelectedTab.Name));
        }


    }
}
