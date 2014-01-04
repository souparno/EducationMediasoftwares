﻿<%@ Page Language="C#" AutoEventWireup="true" %>

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
<%@ Import Namespace="BLL"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Css/Common/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <div class="container">
  <div class="header"><a href="#"><img src="../images/Ascent logo.png" alt="Insert Logo Here" name="Insert_logo" width="180" height="90" id="Insert_logo" style="background: #C6D580; display:block;" /></a> 
    <!-- end .header --></div>
          <div  class="Menu">
         <ul>
          <li><a href="../Home.aspx">Home</a></li>
          <li><a href="#"  >Download</a></li>
          <li><a href="Register.aspx" >Register</a></li>
          <li><a href="Login.aspx"   >Login</a></li>
          <li><a href="AboutUs.aspx" >About Us</a></li>
         </ul>
          <!-- end .Menu -->
         </div>
  <div class="content">
      <table class="box-table-a">
		<caption>Courses Present</caption>
		<thead>
			<tr>
				<th scope="col">Course Name</th>
				<th scope="col">Price</th>
				<th scope="col">Action</th>
		    </tr>
		</thead>
		<tbody>
	   <%
	        
           Class_BLL obj0 = new Class_BLL();
           DataTable dt = obj0.Get_Courses_For_Download();
           foreach (DataRow dr in dt.Rows)
           {
               Response.Write("<tr>");
               Response.Write("<td>" + dr["CourseName"] + "</td>");
               Response.Write("<td>" + dr["Price"] + "</td>");
               Response.Write("<td><a href='register_2.aspx?CourseId="+ dr["CourseId"]+"&CourseName="+dr["CourseName"] +"&Price="+ dr["price"]+"'>Download</a></td>");
               Response.Write("</tr>");
           }
	    
	             
	   %>
	   </tbody>
	</table>
	
	
	   <table class="box-table-a">
		<caption>Subjects Present</caption>
		<thead>
			<tr>
				<th scope="col">Subject Name</th>
				<th scope="col">Price</th>
				<th scope="col">Action</th>
		    </tr>
		</thead>
		<tbody>
	   <%
	        
           Class_BLL obj1 = new Class_BLL();
           DataTable dt1 = obj1.Get_Subjects_For_Download();
           foreach (DataRow dr in dt1.Rows)
           {
               Response.Write("<tr>");
               Response.Write("<td>" + dr["SubjectName"] + "</td>");
               Response.Write("<td>" + dr["Price"] + "</td>");
               Response.Write("<td><a href='register_2.aspx?SubjectId=" + dr["SubjectId"] + "&SubjectName="+ dr["SubjectName"]+"&Price=" + dr["price"] + "'>Download</a></td>");
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
