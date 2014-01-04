using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;
using System.IO;

namespace DAL
{
    public class Class_DAL
    {
        string ConnectionString;

        private DataTable _CourseMaster=new DataTable();
        private DataTable _SubjectMaster = new DataTable();
        private DataTable _RequestCourseMaster = new DataTable();
        private DataTable _RequestSubjectMaster = new DataTable();
        private DataTable _FileMaster = new DataTable();
        private DataTable _FileRelationMaster = new DataTable();
        private DataTable _PasswordMaster = new DataTable();
        private DataTable _UserMaster = new DataTable();
        private DataTable _UserRoleMaster = new DataTable();
        private DataTable _UserRequisitionCourses = new DataTable();
        private DataTable _UserRequisitionSubject = new DataTable();


        public DataTable CourseMaster
        {
            get { return _CourseMaster;}
        }
        public DataTable SubjectMaster
        {
            get { return _SubjectMaster; }
        }
        public DataTable FileMaster
        {
            get { return _FileMaster; }
        }
        public DataTable FileRelationMaster
        {
            get { return _FileRelationMaster; }
        }
        public DataTable PasswordMaster
        {
            get { return _PasswordMaster; }
        }
        public DataTable UserMaster
        {
            get { return _UserMaster; }
        }
        public DataTable UserRoleMaster
        {
            get { return _UserRoleMaster; }
        }
        public DataTable UserRequsitionCourses
        {
            get
            {
                return _UserRequisitionCourses;
            }
        }
        public DataTable UserRequisitionSubject
        {
            get
            {
                return _UserRequisitionSubject;
            }
        }


        public Class_DAL()
        {

            //ConnectionString = "Data Source=BONNIE-PC;Initial Catalog=OnlineExamDB;Integrated Security=True";
            ConnectionString = "Data Source=COMPUTER5;Initial Catalog=OnlineExamDB;User ID=sa;Password=admin123#";
            
            _Get_CourseMaster();
            _Get_SubjectMaster();
            _Get_FileMaster();
            _Get_FileRelationMaster();
            _Get_PasswordMaster();
            _Get_UserMaster();
            _Get_UserRoleMaster();
            _Get_UserRequisitionCourses();
            _Get_UsreRequisitionSubject();

        }

        private void _Get_CourseMaster()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select course_id,course_name from course_master", con);
            da.Fill(_CourseMaster);
        }
        private void _Get_SubjectMaster()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select subject_id,subject_name from subject_master", con);
            da.Fill(_SubjectMaster);
        }
        private void _Get_FileMaster()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select file_id,file_name,price from file_master", con);
            da.Fill(_FileMaster);
        }
        private void _Get_FileRelationMaster()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select course_id,subject_id,file_id from file_relation_master", con);
            da.Fill(_FileRelationMaster);
        }
        private void _Get_PasswordMaster()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select user_id,user_password from password_master", con);
            da.Fill(_PasswordMaster);
        }
        private void _Get_UserMaster()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select user_id,first_name,last_name,user_name from user_master", con);
            da.Fill(_UserMaster);
        }
        private void _Get_UserRoleMaster()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select user_id,user_role from user_role_master", con);
            da.Fill(_UserRoleMaster);
        }
        private void _Get_UserRequisitionCourses()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select user_id,course_id,price,Permission from user_requisition_courses", con);
            da.Fill(_UserRequisitionCourses);
        }
        private void _Get_UsreRequisitionSubject()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select user_id,subject_id,price,Permission from user_requisition_subject", con);
            da.Fill(_UserRequisitionSubject);
        }


        public int UserMaster_Insert(int UserId,string FirstName,string LastName,string UserName,string Email)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            String s = "Insert into User_Master(user_id,first_name,last_name,user_name,Email_id) "+
                "values('"+ UserId +"','"+ FirstName +"','"+ LastName +"','"+ UserName +"','"+ Email +"')";
            con.Open();
            SqlCommand cmd = new SqlCommand(s, con);
            int count= cmd.ExecuteNonQuery();
            con.Close();
            return count;
        }
        public int UserRoleMastre_Insert(int UserId, string UserRole)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "insert into user_role_master(user_id,user_role) values('" + UserId + "','" + UserRole + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(s, con);
            int count = cmd.ExecuteNonQuery();
            con.Close();
            return count;
        }
        public int PasswordMaster_Insert(int UserId, string password)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "insert into password_master(user_id,user_password) values('" + UserId + "','" + password + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(s, con);
            int count = cmd.ExecuteNonQuery();
            con.Close();
            return count;
        }
        public int CourseMaster_Insert(int CourseId,String CourseName)
        {

            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "insert into course_master(course_id,course_name) values('"+ CourseId +"','"+ CourseName +"')";
            con.Open();
            SqlCommand cmd = new SqlCommand(s, con);
            int execute = cmd.ExecuteNonQuery();
            con.Close();
            return execute;
        }
        public int SubjectMaster_Insert(int SubjectId, String SubjectName)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "Insert into subject_master(subject_id,subject_name) values('"+ SubjectId +"','"+ SubjectName +"')";
            SqlCommand cmd = new SqlCommand(s,con);
            con.Open();
            int Execute = cmd.ExecuteNonQuery();
            con.Close();
            return Execute;
        }
        public int FileMaster_Insert(int FileId,string FileName,double Price)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "insert into file_master(file_id,file_name,price) values('"+ FileId +"','"+ FileName +"','"+ Price +"')";
            SqlCommand cmd = new SqlCommand(s,con);
            con.Open();
            int Execute = cmd.ExecuteNonQuery();
            con.Close();
            return Execute;

        }
        public int FileRelationMaster_Insert(int FileId, int CourseId, int SubjectId)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "insert into file_relation_master(file_id,course_id,subject_id) values('"+ FileId +"','"+ CourseId +"','"+ SubjectId +"')";
            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            int Execute = cmd.ExecuteNonQuery();
            con.Close();
            return Execute;
        }
        public int UserRequisitionCourses_Insert(int UserId,int CourseId,double price,int Permission)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "insert into user_requisition_courses(User_Id,Course_Id,price,Permission) values('"+ UserId +"','"+ CourseId +"','"+ price +"','"+ Permission +"')";
            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            int Execute=cmd.ExecuteNonQuery();
            con.Close();
            return Execute;

        }
        public int UserRequisitionSubject_Insert(int UserId,int SubjectId,double price,int Permission)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "insert into user_requisition_subject(User_Id,subject_Id,price,Permission) values('" + UserId + "','" + SubjectId + "','" + price + "','"+ Permission +"')";
            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            int Execute = cmd.ExecuteNonQuery();
            con.Close();
            return Execute;
        }


        public int Update_User_Permission_Courses(int UserId,int CourseId,int Permission)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "update User_requisition_Courses set Permission='"+ Permission 
                +"'where User_id='"+ UserId +"' and course_id='"+ CourseId +"'";
            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            int Execute = cmd.ExecuteNonQuery();
            con.Close();
            return Execute;
        }
        public int Update_User_Permission_Subject(int UserId, int SubjectId, int Permission)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "update User_requisition_subject set Permission='" + Permission
                + "'where User_id='" + UserId + "' and subject_id='" + SubjectId + "'";
            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            int Execute = cmd.ExecuteNonQuery();
            con.Close();
            return Execute;
        }


        public int del_course(int course_id)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "delete from course_master where course_id='" + course_id + "'";
            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            int Execute = cmd.ExecuteNonQuery();
            con.Close();
            return Execute;
        }
        public int del_subject(int subject_id)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s = "delete from subject_master where subject_id='" + subject_id + "'";
            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            int Execute = cmd.ExecuteNonQuery();
            con.Close();
            return Execute;
        }
        public int del_files(int file_id)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string s1 = "delete from file_master where file_id='" + file_id + "'";
            SqlCommand cmd1 = new SqlCommand(s1, con);
            string s2 = "delete from file_relation_master where file_id='" + file_id + "'";
            SqlCommand cmd2 = new SqlCommand(s2, con);
            con.Open();
            int Execute1 = cmd1.ExecuteNonQuery();
            int Execute2 = cmd2.ExecuteNonQuery();
            con.Close();
            if (Execute1 == 1 && Execute2 == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }



}
