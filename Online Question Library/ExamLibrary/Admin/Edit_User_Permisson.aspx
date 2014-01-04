<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Import Namespace="System"%>
<%@ Import Namespace="System.Collections"  %>
<%@ Import Namespace="System.Configuration"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System.Linq"  %>
<%@ Import Namespace="System.Web"  %>
<%@ Import Namespace="System.Web.Security"  %>
<%@ Import Namespace="System.Web.UI"  %>
<%@ Import Namespace="System.Web.UI.HtmlControls"  %>
<%@ Import Namespace="System.Web.UI.WebControls"  %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts"  %>
<%@ Import Namespace="System.Xml.Linq"  %>
<%@ Import Namespace="System.IO"  %>
<%@ Import Namespace="BLL"%>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
        <link href="../Css/Admin/StyleSheet.css" rel="Stylesheet" type="text/css" />
      
</head>
<body>
  <form id="form1" runat="server" action="Edit_User_Permisson.aspx" method="post">
 
   	
   <table class="box-table-a" name="course_table">
		<caption>Courses Bought</caption>
		<thead>
			<tr>
			    <th scope="col">UserName</th>
				<th scope="col">Course Name</th>
				<th scope="col">Price</th>
			    <th scope="col">Permission</th>
			     <th scope="col">Action</th>
			</tr>
		</thead>
		<tbody>
	   <%	    
           Class_BLL obj = new Class_BLL();
	       int UserId= Convert.ToInt32(Request.QueryString["UserId"]);   
           DataTable Course_Requisition = obj.Get_UserRequisitionCourses(UserId);

           foreach (DataRow dr in Course_Requisition.Rows)
           {
               Response.Write("<tr>");
               Response.Write("<td>" + dr["UserName"] + "</td>");
               Response.Write("<td>" + dr["CourseName"] + "</td>");
               Response.Write("<td>" + dr["Price"] + "</td>");
                   if (Convert.ToInt32(dr["Permission"]) == 0)
                       {
                           Response.Write("<td><input type='checkbox' name='chk_Course' onclick='return false;' /></td>");
                       }
                   else
                       {
                           Response.Write("<td><input type='checkbox' name='chk_Course' checked='checked' onclick='return false;'/></td>");
                       }
               Response.Write(" <td><a href='edit_course_permission.aspx?requisition_UserId=" + dr["UserId"] + 
                   "&requisition_UserName=" + dr["UserName"] + "&requisition_CourseId=" + dr["CourseId"] + 
                   "&requisition_CourseName=" + dr["CourseName"] + "&requisition_Permission=" + dr["Permission"] + 
                   "&requisition_Price=" + dr["Price"] + "'>Edit</a></td>");
               Response.Write("</tr>");
           }
	   %>
	   </tbody>
	</table>
	

	
	
	   <table class="box-table-a" name="subject_table">
		<caption>Subjects Bought</caption>
		<thead>
			<tr>
			    <th scope="col">UserName</th>
				<th scope="col">Subject Name</th>
				<th scope="col">Price</th>
				<th scope="col">Permission</th>
			    <th scope="col">Action</th>
			</tr>
		</thead>
		<tbody>
	   <%
	    
           DataTable SubjectRequisition = obj.Get_UserRequisitionSubject(UserId);
           foreach (DataRow dr in SubjectRequisition.Rows)
           {
               Response.Write("<tr>");
               Response.Write("<td>" + dr["UserName"] + "</td>");
               Response.Write("<td>" + dr["SubjectName"] + "</td>");
               Response.Write("<td>" + dr["Price"] + "</td>");

               if (Convert.ToInt32(dr["Permission"]) == 0)
               {
                   Response.Write("<td><input type='checkbox' name='chk_Subject' onclick='return false;' /></td>");
               }
               else
               {
                   Response.Write("<td><input type='checkbox' name='chk_Subject' checked='checked' onclick='return false;'/></td>");
               }
               Response.Write(" <td><a href='edit_subject_permission.aspx?requisition_UserId=" + dr["UserId"] +
                   "&requisition_UserName=" + dr["UserName"] + "&requisition_SubjectId=" + dr["SubjectId"] +
                   "&requisition_SubjectName=" + dr["SubjectName"] + "&requisition_Permission=" + dr["Permission"] +
                   "&requisition_Price=" + dr["Price"] + "'>Edit</a></td>");
               Response.Write("</tr>");
           }
	             
	   %>
	   </tbody>
	</table>
	<a href="Inbox.aspx">Cancel</a>
    </form>
</body>
</html>

