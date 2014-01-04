using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


using System.IO;

using DAL;

namespace BLL
{
    public class Class_BLL
    {
        public DataTable GET_Subjets()
        {
            Class_DAL obj = new Class_DAL();
            return obj.SubjectMaster;
        }
        public DataTable GET_Courses()
        {
            Class_DAL obj = new Class_DAL();
            return obj.CourseMaster;
        }
        public DataTable GET_Files()
        {
            Class_DAL obj = new Class_DAL();

            DataTable dt1= obj.FileMaster;
            DataTable dt2 = obj.FileRelationMaster;
            DataTable dt3 = obj.CourseMaster;
            DataTable dt4 = obj.SubjectMaster;


            DataTable dt = new DataTable();
            dt.Columns.Add("FileId", typeof(int));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("CourseId", typeof(int));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("SubjectId", typeof(int));
            dt.Columns.Add("SubjectName", typeof(string));
            dt.Columns.Add("CreationTime", typeof(string));


            
            DirectoryInfo diFiles = new DirectoryInfo(Path.GetFullPath("Files/"));
            foreach (var file in diFiles.GetFiles("*"))
            {
                var query = from table1 in dt1.AsEnumerable()
                            where table1.Field<string>("File_Name") == file.Name
                            join table2 in
                                (
                                    from table5 in dt4.AsEnumerable()
                                    join table6 in
                                        (
                                            from table3 in dt2.AsEnumerable()
                                            join table4 in dt3.AsEnumerable()
                                            on table3.Field<int>("course_id") equals table4.Field<int>("course_id")
                                            select new
                                            {
                                                FileId = table3.Field<int>("file_id"),
                                                CourseId = table3.Field<int>("course_id"),
                                                SubjectId = table3.Field<int>("subject_id"),
                                                CourseName = table4.Field<string>("Course_Name")
                                            }
                                            ).AsEnumerable()
                                    on table5.Field<int>("subject_id") equals table6.SubjectId
                                    select new
                                    {
                                        FileId = table6.FileId,
                                        CourseId = table6.CourseId,
                                        SubjectId = table6.SubjectId,
                                        CourseName = table6.CourseName,
                                        SubjectName = table5.Field<string>("subject_name")
                                    }
                                    ).AsEnumerable()
                            on table1.Field<int>("file_id") equals table2.FileId into table
                            from p in table.DefaultIfEmpty()
                            select new
                            {
                                FileId = table1.Field<int>("file_id"),
                                FileName = file.Name,
                                CourseId = p == null ? 0 : p.CourseId,
                                CourseName = p == null ? "" : p.CourseName,
                                SubjectId = p == null ? 0 : p.SubjectId,
                                SubejctName = p == null ? "" : p.SubjectName,
                                CreationTime = file.CreationTime.ToString("dd/MM/yyyy"),
                            };



                foreach (var grp in query)
                {
                    DataRow dr = dt.NewRow();
                    dr["FileId"] = grp.FileId;
                    dr["FileName"] = grp.FileName;
                    dr["CourseId"] = grp.CourseId;
                    dr["CourseName"] = grp.CourseName;
                    dr["SubjectId"] = grp.SubjectId;
                    dr["SubjectName"] = grp.SubejctName;
                    dr["CreationTime"] = grp.CreationTime;
                    dt.Rows.Add(dr);
                }


            }

            return dt;
        }
        public DataTable Get_Courses_For_Download()
        {

            Class_DAL obj = new Class_DAL();
            
            DataTable dtFileMaster = obj.FileMaster;
            DataTable dtFileRelationMaster = obj.FileRelationMaster;
            DataTable dtCourses = obj.CourseMaster;




            var query = from table0 in( 
                        from table1 in dtCourses.AsEnumerable()
                        join table2 in
                            (
                                from p in dtFileMaster.AsEnumerable()
                                join q in dtFileRelationMaster.AsEnumerable()
                                on p.Field<int>("file_id") equals q.Field<int>("file_id")
                                select new
                                {
                                    FileId=p.Field<int>("File_Id"),
                                    CourseId = q.Field<int>("course_id"),
                                    Price=p.Field<Decimal>("price")
                                }

                             ).AsEnumerable()
                        on table1.Field<int>("course_id") equals table2.CourseId
                        select new
                        {
                            CourseId = table2.CourseId,
                            CourseName = table1.Field<string>("Course_Name"),
                            Price = table2.Price 
                        }
                        ).AsEnumerable()
                        group table0 by new
                        {
                            CourseId=table0.CourseId,
                            CourseName=table0.CourseName 
                        }into grp
                         select new
                        {
                             CourseId=grp.Key.CourseId,
                             CourseName=grp.Key.CourseName,
                             Price = grp.Sum(r=> r.Price)
                        };



            DataTable dt = new DataTable();
            dt.Columns.Add("CourseId", typeof(int));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("Price", typeof(float));
            foreach (var grp in query)
            {
                DataRow dr = dt.NewRow();
                dr["CourseId"] = grp.CourseId;
                dr["CourseName"] = grp.CourseName;
                dr["Price"] = grp.Price;
                dt.Rows.Add(dr);
            }
            return dt;
       
        }
        public DataTable Get_Subjects_For_Download()
        {
            Class_DAL obj = new Class_DAL();

            DataTable dtFileMaster = obj.FileMaster;
            DataTable dtFileRelationMaster = obj.FileRelationMaster;
            DataTable dtSubjects = obj.SubjectMaster;


            var query = from table0 in
                            (
                                from table1 in dtSubjects.AsEnumerable()
                                join table2 in
                                    (
                                        from p in dtFileMaster.AsEnumerable()
                                        join q in dtFileRelationMaster.AsEnumerable()
                                        on p.Field<int>("file_id") equals q.Field<int>("file_id")
                                        select new
                                        {
                                            FileId = p.Field<int>("File_Id"),
                                            SubjectId = q.Field<int>("subject_id"),
                                            Price = p.Field<Decimal>("price")
                                        }

                                     ).AsEnumerable()
                                on table1.Field<int>("subject_id") equals table2.SubjectId
                                select new
                                {
                                    SubjectId = table2.SubjectId, 
                                    SubjectName = table1.Field<string>("Subject_Name"),
                                    Price = table2.Price
                                }
                                ).AsEnumerable()
                        group table0 by new
                        {
                            SubjectId = table0.SubjectId,
                            SubjectName = table0.SubjectName 
                        } into grp
                        select new
                        {
                            SubjectId = grp.Key.SubjectId,
                            SubjectName = grp.Key.SubjectName,
                            Price = grp.Sum(r => r.Price)
                        };



            DataTable dt = new DataTable();
            dt.Columns.Add("SubjectId", typeof(int));
            dt.Columns.Add("SubjectName", typeof(string));
            dt.Columns.Add("Price", typeof(float));
            foreach (var grp in query)
            {
                DataRow dr = dt.NewRow();
                dr["SubjectId"] = grp.SubjectId;
                dr["SubjectName"] = grp.SubjectName;
                dr["Price"] = grp.Price;
                dt.Rows.Add(dr);
            }
            return dt;
       
        }
        public DataTable Get_UserRequisitionCourses()
        {
            Class_DAL obj = new Class_DAL();
            DataTable UserCourseRequisition = obj.UserRequsitionCourses;
            DataTable UserMaster = obj.UserMaster;
            DataTable CourseMaster = obj.CourseMaster;

            var query = from table0 in
                            (
                                from table1 in
                                    (
                                        from p in UserCourseRequisition.AsEnumerable()
                                        select new
                                        {
                                            UserId = p.Field<int>("User_Id"),
                                            CourseId = p.Field<int>("Course_id"),
                                            Price = p.Field<decimal>("Price"),
                                            Permission = p.Field<int>("Permission")
                                        }).AsEnumerable()
                                join table2 in UserMaster.AsEnumerable()
                                on table1.UserId equals table2.Field<int>("User_id")
                                select new
                                {
                                    UserId = table1.UserId,
                                    UserName = table2.Field<string>("user_name"),
                                    CourseId = table1.CourseId,
                                    Price = table1.Price,
                                    Permission = table1.Permission
                                }
                                ).AsEnumerable()
                        join table4 in CourseMaster.AsEnumerable()
                        on table0.CourseId equals table4.Field<int>("course_id")
                        select new
                        {
                            UserId = table0.UserId,
                            UserName = table0.UserName,
                            CourseId = table0.CourseId,
                            CourseName = table4.Field<string>("course_name"),
                            Price = table0.Price,
                            Permission = table0.Permission
                        };

            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("CourseId", typeof(int));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("Price", typeof(int));
            dt.Columns.Add("Permission", typeof(int));

            foreach (var grp in query)
            {
                DataRow dr = dt.NewRow();
                dr["UserId"] = grp.UserId;
                dr["UserName"] = grp.UserName;
                dr["CourseId"] = grp.CourseId;
                dr["CourseName"] = grp.CourseName;
                dr["Price"] = grp.Price;
                dr["Permission"] = grp.Permission;
                dt.Rows.Add(dr);
            }

            return dt;
        }
        public DataTable Get_UserRequisitionSubject()
        {
            Class_DAL obj = new Class_DAL();
            DataTable UserSubjectRequisition = obj.UserRequisitionSubject;
            DataTable UserMaster = obj.UserMaster;
            DataTable SubjectMaster = obj.SubjectMaster;

            var query = from table0 in
                            (
                                from table1 in
                                    (
                                        from p in UserSubjectRequisition.AsEnumerable()
                                        select new
                                        {
                                            UserId = p.Field<int>("User_Id"),
                                            SubjectId = p.Field<int>("subject_id"),
                                            Price = p.Field<decimal>("Price"),
                                            Permission = p.Field<int>("Permission")
                                        }).AsEnumerable()
                                join table2 in UserMaster.AsEnumerable()
                                on table1.UserId equals table2.Field<int>("User_id")
                                select new
                                {
                                    UserId = table1.UserId,
                                    UserName = table2.Field<string>("user_name"),
                                    SubjectId = table1.SubjectId,
                                    Price = table1.Price,
                                    Permission = table1.Permission
                                }
                                ).AsEnumerable()
                        join table4 in SubjectMaster.AsEnumerable()
                        on table0.SubjectId equals table4.Field<int>("subject_id")
                        select new
                        {
                            UserId = table0.UserId,
                            UserName = table0.UserName,
                            SubjectId = table0.SubjectId,
                            SubejctName = table4.Field<string>("subject_name"),
                            Price = table0.Price,
                            Permission = table0.Permission
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("SubjectId", typeof(int));
            dt.Columns.Add("SubjectName", typeof(string));
            dt.Columns.Add("Price", typeof(int));
            dt.Columns.Add("Permission", typeof(int));

            foreach (var grp in query)
            {
                DataRow dr = dt.NewRow();
                dr["UserId"] = grp.UserId;
                dr["UserName"] = grp.UserName;
                dr["SubjectId"] = grp.SubjectId;
                dr["SubjectName"] = grp.SubejctName;
                dr["Price"] = grp.Price;
                dr["Permission"] = grp.Permission;
                dt.Rows.Add(dr);
            }

            return dt;
        }
        public DataTable Get_UserRequisitionCourses(int UserId)
        {
            Class_DAL obj = new Class_DAL();
            DataTable UserCourseRequisition = obj.UserRequsitionCourses;
            DataTable UserMaster=obj.UserMaster;
            DataTable CourseMaster=obj.CourseMaster;

            var query = from table0 in
                            (
                                from table1 in
                                    (
                                        from p in UserCourseRequisition.AsEnumerable()
                                        where p.Field<int>("user_id") == UserId
                                        select new
                                        {
                                            UserId = p.Field<int>("User_Id"),
                                            CourseId = p.Field<int>("Course_id"),
                                            Price = p.Field<decimal>("Price"),
                                            Permission=p.Field<int>("Permission")
                                        }).AsEnumerable()
                                join table2 in UserMaster.AsEnumerable()
                                on table1.UserId equals table2.Field<int>("User_id")
                                select new
                                {
                                    UserId = table1.UserId,
                                    UserName = table2.Field<string>("user_name"),
                                    CourseId = table1.CourseId,
                                    Price = table1.Price,
                                    Permission=table1.Permission 
                                }
                                ).AsEnumerable()
                        join table4 in CourseMaster.AsEnumerable()
                        on table0.CourseId equals table4.Field<int>("course_id")
                        select new
                        {
                           UserId=table0.UserId,
                           UserName=table0.UserName,
                           CourseId=table0.CourseId,
                           CourseName=table4.Field<string>("course_name"),
                           Price=table0.Price, 
                           Permission=table0.Permission 
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("UserId",typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("CourseId",typeof(int));
            dt.Columns.Add("CourseName",typeof(string));
            dt.Columns.Add("Price",typeof(int));
            dt.Columns.Add("Permission",typeof(int));

            foreach (var grp in query)
            {
                DataRow dr = dt.NewRow();
                dr["UserId"]=grp.UserId;
                dr["UserName"] = grp.UserName;
                dr["CourseId"] = grp.CourseId;
                dr["CourseName"] = grp.CourseName;
                dr["Price"] = grp.Price;
                dr["Permission"] = grp.Permission; 
                dt.Rows.Add(dr);
            }
                       
            return dt;
        }
        public DataTable Get_UserRequisitionSubject(int UserId)
        {
            Class_DAL obj = new Class_DAL();
            DataTable UserSubjectRequisition = obj.UserRequisitionSubject;
            DataTable UserMaster = obj.UserMaster;
            DataTable SubjectMaster = obj.SubjectMaster;

            var query = from table0 in
                            (
                                from table1 in
                                    (
                                        from p in UserSubjectRequisition.AsEnumerable()
                                        where p.Field<int>("user_id") == UserId
                                        select new
                                        {
                                            UserId = p.Field<int>("User_Id"),
                                            SubjectId = p.Field<int>("subject_id"),
                                            Price = p.Field<decimal>("Price"),
                                            Permission=p.Field<int>("Permission")
                                        }).AsEnumerable()
                                join table2 in UserMaster.AsEnumerable()
                                on table1.UserId equals table2.Field<int>("User_id")
                                select new
                                {
                                    UserId = table1.UserId,
                                    UserName = table2.Field<string>("user_name"),
                                    SubjectId = table1.SubjectId,
                                    Price = table1.Price,
                                    Permission=table1.Permission 
                                }
                                ).AsEnumerable()
                        join table4 in SubjectMaster.AsEnumerable()
                        on table0.SubjectId equals table4.Field<int>("subject_id")
                        select new
                        {
                            UserId = table0.UserId,
                            UserName = table0.UserName,
                            SubjectId = table0.SubjectId,
                            SubejctName = table4.Field<string>("subject_name"),
                            Price = table0.Price,
                            Permission=table0.Permission 
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("SubjectId", typeof(int));
            dt.Columns.Add("SubjectName", typeof(string));
            dt.Columns.Add("Price", typeof(int));
            dt.Columns.Add("Permission",typeof(int));

            foreach (var grp in query)
            {
                DataRow dr = dt.NewRow();
                dr["UserId"] = grp.UserId;
                dr["UserName"] = grp.UserName;
                dr["SubjectId"] = grp.SubjectId;
                dr["SubjectName"] = grp.SubejctName;
                dr["Price"] = grp.Price;
                dr["Permission"] = grp.Permission; 
                dt.Rows.Add(dr);
            }

            return dt;
        }
        public DataTable Get_UserRequisitionCourses_Inbox(int UserId)
        {
            Class_DAL obj = new Class_DAL();
            DataTable UserCourseRequisition = obj.UserRequsitionCourses;
            DataTable UserSubjectRequisition = obj.UserRequisitionSubject;
            DataTable UserMaster = obj.UserMaster;
            DataTable CourseMaster = obj.CourseMaster;
            DataTable SubjectMaster=obj.SubjectMaster;
            DataTable FileRelationMaster=obj.FileRelationMaster;
            DataTable FileMaster = obj.FileMaster;

            var query = (from table6 in
                             (
                                 from table5 in
                                     (
                                         from course_details in
                                             (from table0 in
                                                  (
                                                      from table1 in
                                                          (
                                                              from p in UserSubjectRequisition.AsEnumerable()
                                                              where p.Field<int>("User_Id") == UserId
                                                              && p.Field<int>("Permission") == 1
                                                              select new
                                                              {
                                                                  UserId = p.Field<int>("User_Id"),
                                                                  SubjectId = p.Field<int>("subject_id")
                                                              }).AsEnumerable()
                                                      join table2 in UserMaster.AsEnumerable()
                                                      on table1.UserId equals table2.Field<int>("User_id")
                                                      select new
                                                      {
                                                          UserId = table1.UserId,
                                                          UserName = table2.Field<string>("user_name"),
                                                          SubjectId = table1.SubjectId
                                                       }
                                                      ).AsEnumerable()
                                              join table4 in SubjectMaster.AsEnumerable()
                                              on table0.SubjectId equals table4.Field<int>("subject_id")
                                              select new
                                              {
                                                  UserId = table0.UserId,
                                                  UserName = table0.UserName,
                                                  SubjectId = table0.SubjectId,
                                                  SubjectName = table4.Field<string>("subject_name")
                                              }
                                         ).AsEnumerable()
                                         join dt_table in FileRelationMaster.AsEnumerable()
                                         on course_details.SubjectId equals dt_table.Field<int>("subject_id")
                                         select new
                                         {
                                             UserId = course_details.UserId,
                                             UserName = course_details.UserName,
                                             SubjectId = course_details.SubjectId,
                                             SubjectName = course_details.SubjectName,
                                             CourseId = dt_table.Field<int>("Course_id"),
                                             FileId = dt_table.Field<int>("file_id")
                                         }).AsEnumerable()
                                 join course_table in CourseMaster.AsEnumerable()
                                 on table5.CourseId equals course_table.Field<int>("course_id")
                                 select new
                                 {
                                     UserId = table5.UserId,
                                     UserName = table5.UserName,
                                     CourseId = table5.CourseId,
                                     CourseName = course_table.Field<string>("course_name"),
                                     SubjectId = table5.SubjectId,
                                     FileId = table5.FileId,
                                     SubjectName = table5.SubjectName 
                                 }).AsEnumerable()
                         join fmaster in FileMaster.AsEnumerable()
                         on table6.FileId equals fmaster.Field<int>("file_id")
                         select new
                         {
                             UserId = table6.UserId,
                             UserName = table6.UserName,
                             CourseId = table6.CourseId,
                             CourseName = table6.CourseName,
                             SubjectId = table6.SubjectId,
                             SubjectName = table6.SubjectName,
                             FileId = table6.FileId,
                             FileName = fmaster.Field<string>("file_name"),
                           }).Union
                        (
                        from table6 in
                            (
                                from table5 in
                                    (
                                        from course_details in
                                            (from table0 in
                                                 (
                                                     from table1 in
                                                         (
                                                             from p in UserCourseRequisition.AsEnumerable()
                                                             where p.Field<int>("User_Id") == UserId
                                                             && p.Field<int>("Permission") == 1
                                                             select new
                                                             {
                                                                 UserId = p.Field<int>("User_Id"),
                                                                 CourseId = p.Field<int>("Course_id")
                                                              }).AsEnumerable()
                                                     join table2 in UserMaster.AsEnumerable()
                                                     on table1.UserId equals table2.Field<int>("User_id")
                                                     select new
                                                     {
                                                         UserId = table1.UserId,
                                                         UserName = table2.Field<string>("user_name"),
                                                         CourseId = table1.CourseId
                                                     }
                                                     ).AsEnumerable()
                                             join table4 in CourseMaster.AsEnumerable()
                                             on table0.CourseId equals table4.Field<int>("course_id")
                                             select new
                                             {
                                                 UserId = table0.UserId,
                                                 UserName = table0.UserName,
                                                 CourseId = table0.CourseId,
                                                 CourseName = table4.Field<string>("course_name")
                                             }
                                        ).AsEnumerable()
                                        join dt_table in FileRelationMaster.AsEnumerable()
                                        on course_details.CourseId equals dt_table.Field<int>("course_id")
                                        select new
                                        {
                                            UserId = course_details.UserId,
                                            UserName = course_details.UserName,
                                            CourseId = course_details.CourseId,
                                            CourseName = course_details.CourseName,
                                            SubjectId = dt_table.Field<int>("subject_id"),
                                            FileId = dt_table.Field<int>("file_id")
                                        }).AsEnumerable()
                                join subject_table in SubjectMaster.AsEnumerable()
                                on table5.SubjectId equals subject_table.Field<int>("subject_id")
                                select new
                                {
                                    UserId = table5.UserId,
                                    UserName = table5.UserName,
                                    CourseId = table5.CourseId,
                                    CourseName = table5.CourseName,
                                    SubjectId = table5.SubjectId,
                                    FileId = table5.FileId,
                                    SubjectName = subject_table.Field<string>("subject_name")
                                }).AsEnumerable()
                        join fmaster in FileMaster.AsEnumerable()
                        on table6.FileId equals fmaster.Field<int>("file_id")
                        select new
                        {
                            UserId = table6.UserId,
                            UserName = table6.UserName,
                            CourseId = table6.CourseId,
                            CourseName = table6.CourseName,
                            SubjectId = table6.SubjectId,
                            SubjectName = table6.SubjectName,
                            FileId = table6.FileId,
                            FileName = fmaster.Field<string>("file_name"),
                        }
                        );


            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(int));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("CourseId", typeof(int));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("SubjectId", typeof(int));
            dt.Columns.Add("SubjectName", typeof(string));
            dt.Columns.Add("FileId", typeof(int));
            dt.Columns.Add("FileName", typeof(string));

            foreach (var grp in query)
            {
                DataRow dr = dt.NewRow();
                dr["UserId"] = grp.UserId;
                dr["UserName"] = grp.UserName;
                dr["CourseId"] = grp.CourseId;
                dr["CourseName"] = grp.CourseName;
                dr["SubjectId"] = grp.SubjectId;
                dr["SubjectName"] = grp.SubjectName;
                dr["FileId"] = grp.FileId;
                dr["FileName"] = grp.FileName;
                dt.Rows.Add(dr);
            }

            return dt;
        }
        
        public DataTable user_present(string user_name, string password)
        {
            Class_DAL obj = new Class_DAL();

            DataTable user_master = obj.UserMaster;
            DataTable role_master = obj.UserRoleMaster;
            DataTable password_master = obj.PasswordMaster;

            var query = from table1 in user_master.AsEnumerable()
                        join table2 in
                            (
                                from x in role_master.AsEnumerable()
                                join y in password_master.AsEnumerable()
                                on x.Field<int>("user_id") equals y.Field<int>("user_id")
                                select new
                                {
                                    user_id = x.Field<int>("user_id"),
                                    user_role = x.Field<string>("user_role"),
                                    password = y.Field<string>("user_password")
                                }
                                ).AsEnumerable()
                        on table1.Field<int>("user_id") equals table2.user_id
                        where
                        table1.Field<string>("user_name") == user_name
                        && table2.password == password
                        select new
                        {
                            User_id = table2.user_id,
                            user_role = table2.user_role,
                            password = table2.password,
                            user_name = table1.Field<string>("user_name"),
                            first_name = table1.Field<string>("first_name"),
                            last_name = table1.Field<string>("last_name")
                        };



            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(int));
            dt.Columns.Add("UserRole", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            foreach (var grp in query)
            {
                DataRow dr = dt.NewRow();
                dr["UserId"] = grp.User_id;
                dr["UserRole"] = grp.user_role;
                dr["Password"] = grp.password;
                dr["UserName"] = grp.user_name;
                dr["FirstName"] = grp.first_name;
                dr["LastName"] = grp.last_name;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public int UserMaster_Insert(string FirstName, string LastName, string UserName, string Email,string Password)
        {
            Class_DAL obj = new Class_DAL();
            int Execute1 = 0;
            int Execute2 = 0;
            int Execute3 = 0;
            int UserId = 0;
            DataTable user_master = obj.UserMaster;
            DataTable role_master = obj.UserRoleMaster;
            DataTable password_master = obj.PasswordMaster;

            var query1 = from table1 in user_master.AsEnumerable()
                        join table2 in
                            (
                                from x in role_master.AsEnumerable()
                                join y in password_master.AsEnumerable()
                                on x.Field<int>("user_id") equals y.Field<int>("user_id")
                                select new
                                {
                                    user_id = x.Field<int>("user_id"),
                                    user_role = x.Field<string>("user_role"),
                                    password = y.Field<string>("user_password")
                                }
                                ).AsEnumerable()
                        on table1.Field<int>("user_id") equals table2.user_id
                        where
                        table1.Field<string>("user_name") == UserName
                        select new
                        {
                            User_id = table2.user_id,
                            user_role = table2.user_role,
                            password = table2.password,
                            user_name = table1.Field<string>("user_name"),
                            first_name = table1.Field<string>("first_name"),
                            last_name = table1.Field<string>("last_name")
                        };

            if (query1.Count() == 0)
            {



                var query2 = from p in user_master.AsEnumerable()
                            select new
                            {
                                UserId = p.Field<int>("user_id")
                            };

                
                if (query2.Count() > 0)
                {
                    UserId = (from grp in query2 select UserId = grp.UserId).Max() + 1;
                }
                else
                {
                    UserId = 1;
                }

                 Execute1=obj.UserMaster_Insert(UserId, FirstName, LastName, UserName, Email);
                 Execute2 = obj.UserRoleMastre_Insert(UserId, "USER");
                 Execute3 = obj.PasswordMaster_Insert(UserId, Password);
               
            }

            if (Execute1 != 0 && Execute2 != 0 && Execute3 != 0)
            {
                return UserId;
            }
            else
            {
                return 0;
            }
        }
        public int CourseMaster_insert(string CourseName)
        {
            Class_DAL obj = new Class_DAL();
            int Execute = 0;
            DataTable course_master = obj.CourseMaster;
            var query = from p in course_master.AsEnumerable()
                        select new
                        {
                            CourseId = p.Field<int>("course_id")
                        };

            if (query.Count() > 0)
            {
                int CourseId = (from grp in query select CourseId = grp.CourseId).Max() + 1;
                Execute=obj.CourseMaster_Insert( CourseId,CourseName );
            }
            else
            {
                 Execute=obj.CourseMaster_Insert( 1,CourseName );
            }
            return Execute;
        }
        public int SubjectMaster_insert(string SubjectName)
        {
            Class_DAL obj = new Class_DAL();
            int Execute = 0;
            DataTable Subject_Master = obj.SubjectMaster;
            var query = from p in Subject_Master.AsEnumerable()
                        select new
                        {
                            SubjectId = p.Field<int>("subject_id")
                        };

            if (query.Count() > 0)
            {
                int SubjectId = (from grp in query select SubjectId = grp.SubjectId).Max() + 1;
                Execute = obj.SubjectMaster_Insert(SubjectId, SubjectName);
            }
            else
            {
                Execute = obj.SubjectMaster_Insert(1, SubjectName);
            }
            return Execute;
            
        }
        public int FileMaster_Insert(int CourseId, int SubjectId, string FileName, double Price)
        {
            Class_DAL obj = new Class_DAL();
            DataTable dt = obj.FileMaster;
            var query1 = from p in dt.AsEnumerable()
                         select new
                         {
                             FileId = p.Field<int>("file_id")
                         };

            int FileId = 0;
            if (query1.Count() > 0)
            {
                FileId = (from grp in query1 select FileId = grp.FileId).Max() + 1;
            }
            else
            {
                FileId = 1;
            }
            int Execute1 = obj.FileMaster_Insert(FileId, FileName, Price);
            int Execute2 = obj.FileRelationMaster_Insert(FileId, CourseId, SubjectId);

            if (Execute1 != 0 && Execute2 != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int UserRequisitionCourse_Insert(int UserId, int CourseId, double Price,int Permission)
        {
            Class_DAL obj = new Class_DAL();
            int Execute=obj.UserRequisitionCourses_Insert(UserId, CourseId, Price,Permission);
            return Execute;
        }
        public int UserRequisitionSubject_Insert(int UserId, int SubjectId, double price,int Permission)
        {
            Class_DAL obj = new Class_DAL();
            int Execute = obj.UserRequisitionSubject_Insert(UserId, SubjectId, price,Permission);
            return Execute;
        }

        public DataTable cancel_course_cart(int CourseId,DataTable Course_Cart)
        {
            var query = from p in Course_Cart.AsEnumerable()
                        where p.Field<int>("CourseId") != CourseId
                        select new
                        {
                            UserId = p.Field<int>("UserId"),
                            CourseId = p.Field<int>("CourseId"),
                            CourseName = p.Field<String>("CourseName"),
                            Price = p.Field<Double>("Price")
                        };

            DataTable _Course_Cart = new DataTable();

            _Course_Cart.Columns.Add("UserId", typeof(int));
            _Course_Cart.Columns.Add("CourseId", typeof(int));
            _Course_Cart.Columns.Add("CourseName", typeof(string));
            _Course_Cart.Columns.Add("Price", typeof(double));

            foreach (var grp in query)
            {
                DataRow dr = _Course_Cart.NewRow();
                dr["UserId"] = grp.UserId;
                dr["CourseId"] = grp.CourseId;
                dr["CourseName"] = grp.CourseName;
                dr["Price"] = grp.Price;
                _Course_Cart.Rows.Add(dr);

            }
            return _Course_Cart;
        }
        public DataTable cancel_subject_cart(int SubjectId, DataTable Subject_cart)
        {
            var query = from p in Subject_cart.AsEnumerable()
                        where p.Field<int>("SubjectId") != SubjectId
                        select new
                        {
                            UserId = p.Field<int>("UserId"),
                            SubjectId = p.Field<int>("SubjectId"),
                            SubjectName = p.Field<String>("SubjectName"),
                            Price = p.Field<Double>("Price")
                        };

            DataTable _Subject_Cart = new DataTable();

            _Subject_Cart.Columns.Add("UserId", typeof(int));
            _Subject_Cart.Columns.Add("SubjectId", typeof(int));
            _Subject_Cart.Columns.Add("SubjectName", typeof(string));
            _Subject_Cart.Columns.Add("Price", typeof(double));

            foreach (var grp in query)
            {
                DataRow dr = _Subject_Cart.NewRow();
                dr["UserId"] = grp.UserId;
                dr["SubjectId"] = grp.SubjectId;
                dr["SubjectName"] = grp.SubjectName;
                dr["Price"] = grp.Price;
                _Subject_Cart.Rows.Add(dr);

            }
            return _Subject_Cart;
        }

        public int Update_User_Permission_Courses(int UserId, int CourseId, int Permission)
        {
            Class_DAL obj = new Class_DAL();
            int Execute=obj.Update_User_Permission_Courses(UserId, CourseId, Permission);
            return Execute;
        }
        public int Update_User_Permission_Subject(int UserId, int SubjectId, int Permission)
        {
            Class_DAL obj = new Class_DAL();
            int Execute = obj.Update_User_Permission_Subject(UserId, SubjectId, Permission);
            return Execute;
        }


        public int delete_course(int course_id)
        {
            Class_DAL obj = new Class_DAL();
            int Execute = obj.del_course(course_id);
            return Execute;
        }
        public int delete_subject(int subject_id)
        {
            Class_DAL obj = new Class_DAL();
            int Execute = obj.del_subject(subject_id);
            return Execute;
        }
        public int delete_files(int file_id, string file_name)
        {
            Class_DAL obj = new Class_DAL();
            int execute=obj.del_files(file_id);
            if (execute == 1)
            {
                //DirectoryInfo diFiles = new DirectoryInfo(Path.GetFullPath("Files/" + file_name));
                //diFiles.Delete();
            }
            return execute;
        }
    }
}
