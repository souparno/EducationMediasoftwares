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
<%@ Import Namespace="BLL"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Css/Admin/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" method="post" action="Requisition.aspx" >
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
      <li><a href="Add_To_Cart.aspx">Downloads</a></li>
      <li><a href="#">Requisition Sent</a></li>
      <li><a href="Inbox.aspx">Inbox</a></li>
      <li><a href="Cart.aspx" >Cart</a></li>
    </ul>

    <!-- end .sidebar1 --></div>
  <div class="content">
   <table class="box-table-a">
		<caption>Courses Bought</caption>
		<thead>
			<tr>
				<th scope="col">Course Name</th>
				<th scope="col">Price</th>
			</tr>
		</thead>
		<tbody>
	   <%
	    
               Class_BLL obj = new Class_BLL();
	           int UserId=Convert.ToInt32(Session["UserId"]);
               DataTable Course_Requisition = obj.Get_UserRequisitionCourses(UserId);
    	                
               foreach (DataRow dr in Course_Requisition.Rows)
               {
                   Response.Write("<tr>");
                   Response.Write("<td>" + dr["CourseName"] + "</td>");
                   Response.Write("<td>" + dr["Price"] + "</td>");
                   Response.Write("</tr>");
               }
        
	             
	   %>
	   
	   </tbody>
	</table>
	
	
	   <table class="box-table-a">
		<caption>Subjects Bought</caption>
		<thead>
			<tr>
				<th scope="col">Subject Name</th>
				<th scope="col">Price</th>
		    </tr>
		</thead>
		<tbody>
	   <%
	    
               DataTable SubjectRequisition = obj.Get_UserRequisitionSubject(UserId);
               foreach (DataRow dr in SubjectRequisition.Rows)
               {
                   Response.Write("<tr>");
                   Response.Write("<td>" + dr["SubjectName"] + "</td>");
                   Response.Write("<td>" + dr["Price"] + "</td>");
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
