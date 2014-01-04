<%@ Page Language="C#" AutoEventWireup="true" %>

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

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
       <link href="../Css/Admin/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server" action="Inbox.aspx" method="post">
  <div class="container">
  <div class="header"><a href="#"><img src="../images/Ascent logo.png" alt="Insert Logo Here" name="Insert_logo" width="180" height="90" id="Insert_logo" style="background: #C6D580; display:block;" /></a> 
    <!-- end .header --></div>
  <div  class="Menu">
         <ul>
          <li><a href="#">Home</a></li>
          <li><a href="#">Free Demo</a></li>
          <li><a href="#">Register</a></li>
          <li><a href="LogOut.aspx" >Log Out</a></li>
          <li><a href="#">About Us</a></li>
         </ul>
         <!--End Of The Div Menu-->
         </div>
  <div class="sidebar1">
    <ul class="nav">
      <li><a href="CreateCourses.aspx" >Create Course</a></li>
      <li><a href="CreateSubjects.aspx" >Create Subject</a></li>
      <li><a href="Create_Question_Papers.aspx" >Import Questions</a></li>
      <li><a href="#">Inbox</a></li>
     </ul>

    <!-- end .sidebar1 --></div>
  <div class="content">
   <table class="box-table-a">
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
	           DataTable Course_Requisition = obj.Get_UserRequisitionCourses();
    	                
               foreach (DataRow dr in Course_Requisition.Rows)
               {
                   Response.Write("<tr>");
                   Response.Write("<td>" + dr["UserName"] + "</td>");
                   Response.Write("<td>" + dr["CourseName"] + "</td>");
                   Response.Write("<td>" + dr["Price"] + "</td>");
                   if (Convert.ToInt32(dr["Permission"]) == 0)
                       {
                           Response.Write("<td><input type='checkbox' name='chk_Course' onclick='return false;'/></td>");
                       }
                   else
                       {
                           Response.Write("<td><input type='checkbox' name='chk_Course' onclick='return false;' checked='checked'/></td>");
                       } 
                   Response.Write("<td><a href='Edit_User_Permisson.aspx?UserId=" + dr["UserId"] + "'>Edit</a></td>");
                   Response.Write("</tr>");
               }
        
	             
	   %>
	   
	   </tbody>
	</table>
	
	   
	   <table class="box-table-a">
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
	    
               DataTable SubjectRequisition = obj.Get_UserRequisitionSubject();
               foreach (DataRow dr in SubjectRequisition.Rows)
               {
                   Response.Write("<tr>");
                   Response.Write("<td>" + dr["UserName"] + "</td>");
                   Response.Write("<td>" + dr["SubjectName"] + "</td>");
                   Response.Write("<td>" + dr["Price"] + "</td>");
                   
                   if (Convert.ToInt32(dr["Permission"]) == 0)
                       {
                           Response.Write("<td><input type='checkbox' name='chk_Subject' onclick='return false;'/></td>");
                       }
                   else
                       {
                           Response.Write("<td><input type='checkbox' name='chk_Subject' onclick='return false;'  checked='checked'/></td>");
                       }
                   Response.Write("<td><a href='Edit_User_Permisson.aspx?UserId="+ dr["UserId"]+"'>Edit</a></td>");
                   Response.Write("</tr>");
               }

        %>
	   </tbody>
	</table>
   
    <!-- end .content --></div>
    <div class="footer">

    <!-- end .footer --></div>
  <!-- end .container --></div>
    </form>
</body>
</html>
