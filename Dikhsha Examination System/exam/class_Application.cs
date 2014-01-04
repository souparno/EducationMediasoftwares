using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;



namespace exam
{  
    class class_Application
    {

        public static string user_id;
        public static string user_name;


        //----declaring the static variables-->
        public static int flag;
        public static string code;
        public static frm_master_question frm_master_question;
        public static frm_QuestionAddEdit frm_QuestionAddEdit;
        public static string exam_code;
        public static string test_code;
        public static frm_mdi_parent parent_form;
        public static DataSet ds_gre_answer;
        public static DataSet ds_gmat_answer;
 



        //---declaring teh non static-instance variables--->
        public OleDbConnection con;
        public DataSet combobox_dataset;
        public int quant_right_answer;
        public int quant_wrong_answer;
        public int verbal_right_answer;
        public int verbal_wrong_answer;
        String return_result;
        DataSet ds;


        static int _review;
        public static int REVIEW
        {
            get {return _review; }
            set { _review = value; }
        }
        //---constructor of the class--->
        public class_Application()
        { con = null;
        }
        //----function for connecting to the database--->
        public void connect()
        {
            //con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\WinExaminationSystem.mdb;Jet OLEDB:Database Password=admin123#");
            con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ Application.StartupPath +"\\WinExaminationSystem.accdb;");
            con.Open();
        }
        //---function for discooneting the database-->
        public void disconnect()
        {
            con.Close();
        }
        //---function for executing the non-query--->
        public void execute_non_query(string s)
        {   this.connect();
            OleDbCommand cmd = new OleDbCommand(s, this.con);
            cmd.ExecuteNonQuery();
            this.disconnect();
         }
        //--function for returning a value queried by the user witht the help of an datareader--->
        public string  execute_scalar(string s)
        {   this.connect();
            OleDbCommand cmd = new OleDbCommand(s, this.con);
            return_result = Convert.ToString(cmd.ExecuteScalar());
            this.disconnect();
            return return_result;
         }

        //--function for filling up a dataset with the help of a data adapter-->
        public DataSet  fill_data_set(string s)
        {   ds = new DataSet();
            this.connect();
            OleDbDataAdapter da = new OleDbDataAdapter(s, this.con);
            da.Fill(ds);
            this.disconnect();
            return ds;
        }
        public DataTable fill_data_table(string s)
        {
            DataTable dt = new DataTable();
            this.connect();
            OleDbDataAdapter da = new OleDbDataAdapter(s, this.con);
            da.Fill(dt);
            this.disconnect();
            return dt;
        }

        //---getting the code--------------------------------->
        public void getcode(string s1,string s2)
        {   string check;
            check = execute_scalar(s2);
            if (check.Equals("") == true)
            {
             code = s1.ToUpper()  + "00001";
            }//---end of the if---->
            else
            {
                int i;
                for (i = 1; i < check.Length; i++)
                { if(check[i] != '0' )
                    {break;
                    }
                }
                int number=Convert.ToInt32(check.Substring(i))+1;
                code = check.Substring(0, i).ToUpper()  + Convert.ToString(number);
                if(code.Length>6){
                    code = check.Substring(0, i-1).ToUpper() + Convert.ToString(number);
                }
           }//-----end of the else--->
      }//---end of the function getcode--->

        //---function for filling a combobox--->
        public void fill_combo_box(ComboBox cmb_box,string s,string dispay_member)
        {   combobox_dataset  = new DataSet();
            combobox_dataset = fill_data_set(s);
            cmb_box.DataSource = combobox_dataset.Tables[0];
            cmb_box.DisplayMember = dispay_member;
                          
        }
   


        
    }
}
