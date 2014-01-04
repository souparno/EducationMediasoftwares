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
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    
     <link href="../Css/Admin/StyleSheet.css" rel="Stylesheet" type="text/css" />
         
</head>
<body>
    <form id="form1" runat="server">
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
      <li><a href="add_to_cart.aspx">Downloads</a></li>
      <li><a href="Requisition.aspx" >Requisition Sent</a></li>
      <li><a href="#">Inbox</a></li>
      <li><a href="Cart.aspx">Cart</a></li>
    </ul>

    <!-- end .sidebar1 --></div>
  <div class="content">
   <table class="box-table-a">
		<caption>Inbox</caption>
		<thead>
			<tr>
				<th scope="col">Course Name</th>
				<th scope="col">Subject Name</th>
				<th scope="col">Download</th>
		    </tr>
		</thead>
		<tbody>
	   <%
	        
           Class_BLL obj0 = new Class_BLL();
           int UserId = Convert.ToInt32(Session["UserId"]);
           DataTable dt = obj0.Get_UserRequisitionCourses_Inbox(UserId);
           foreach (DataRow dr in dt.Rows)
           {
               Response.Write("<tr>");
               Response.Write("<td>" + dr["CourseName"] + "</td>");
               Response.Write("<td>" + dr["SubjectName"] + "</td>");
               Response.Write("<td><a href='pdf_page.aspx?filename="+ dr["FileName"]+"'>DownLoad</a></td>");
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
