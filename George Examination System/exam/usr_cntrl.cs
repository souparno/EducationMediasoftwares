using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace exam
{
    public partial class usr_cntrl : UserControl
    {
        public usr_cntrl()
        {
            InitializeComponent();
        }

      
      
        public Label Question_Label
        {
            get { return lblQuestion; }
            set { lblQuestion  = value; }
        }
        public Label question_number
        {
            get { return lbl_Qno; }
            set { lbl_Qno = value; }

        }
        public Label section_code
        {
            get { return lbl_sub_code; }
            set { lbl_sub_code = value; }
        }
        public Label CorrectOption
        {
            get { return lbl_corretct_option; }
            set { lbl_corretct_option = value; }
        }
      
        public GroupBox GroupBox2
        {
            get { return lblWrong5; }
            set {lblWrong5=value; }
        }
        public  Label  ImageAns1{
            get { return lblImageAns1; }
            set { lblImageAns1 = value; }
        }
        public Label ImageAns2
        {
            get { return lblImageAns2; }
            set { lblImageAns2 = value; }
        }
        public Label ImageAns3
        {
            get { return lblImageAns3; }
            set { lblImageAns3 = value; }
        }
        public Label ImageAns4
        {
            get { return lblImageAns4; }
            set { lblImageAns4 = value; }
        }
        public Label ImageAns5
        {
            get { return lblImageAns5; }
            set { lblImageAns5 = value; }
        }
        public RadioButton RadioButton6
        {
            get{return radioButton1;}
            set{radioButton1=value;}
        }
        public RadioButton RadioButton7
        {
            get{return radioButton2;}
            set{radioButton2=value;}
        }
        public RadioButton RadioButton8
        {
            get{return radioButton3;}
            set{radioButton3=value;}
        }
        public RadioButton RadioButton9
        {
            get{return radioButton4;}
            set{radioButton4=value;}
        }
        public RadioButton RadioButton10
        {
            get{return radioButton5;}
            set{radioButton5=value;}
        }
        public Label CorrectImage1
        {
            get { return lblCorrect1; }
            set { lblCorrect1 = value; }
        }
        public Label CorrectImage2
        {
            get { return lblcorrect2; }
            set { lblcorrect2 = value; }
        }
        public Label CorrectImage3
        {
            get { return lblcorrect3; }
            set { lblcorrect3 = value; }
        }
        public Label CorrectImage4
        {
            get { return lblcorrect4; }
            set { lblcorrect4 = value; }
        }
        public Label CorrectImage5
        {
            get { return lblcorerct5; }
            set { lblcorerct5 = value; }
        }
        public Label WrongImage1
        {
            get {return lblWrong1; }
            set{lblWrong1=value;}
        }
        public Label WrongImage2
        {
            get { return lblWrong2; }
            set { WrongImage2 = value; }
        }
        public Label WrongImage3
        {
            get { return lblWrong3; }
            set { WrongImage3 = value; }
        }
        public Label WrongImage4
        {
            get { return lblWrong4; }
            set { lblWrong4 = value; }
        }
        public Label WrongImage5
        {
            get { return labelWrong5; }
            set { labelWrong5  = value;}
        }

        public PictureBox Picture
        {
            get { return pictureBox1; }
            set { pictureBox1 = value; }
        }

        public FlowLayoutPanel Flpanel
        {
            get { return flowLayoutPanel1; }
            set { flowLayoutPanel1 = value; }
        }


        private void usr_cntrl_Load(object sender, EventArgs e)
        {

        }
  
        

        

    }
}
