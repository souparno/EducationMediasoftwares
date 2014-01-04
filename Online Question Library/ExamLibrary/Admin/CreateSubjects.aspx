<%@ Page Language="C#" AutoEventWireup="true"%>

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



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Css/Admin/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form action="CreateSubjects.aspx" method="post" >
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
      <li><a href="#">Create Subject</a></li>
      <li><a href="Create_Question_Papers.aspx" >Import Questions</a></li>
      <li><a href="Inbox.aspx" >Inbox</a></li>
    </ul>

    <!-- end .sidebar1 --></div>
  <div class="content">
   <table class="box-table-a">
		<caption>Site Users</caption>
		<thead>
			<tr>
				<th scope="col">Subject Id</th>
				<th scope="col">Subject Name</th>
				<th scope="col">Actions</th>
			</tr>
		</thead>
		<tbody>
	    <%
            Class_BLL obj1 = new Class_BLL();
            DataTable dt = obj1.GET_Subjets();
            foreach (DataRow dr in dt.Rows)
            {
                Response.Write("<tr>");
                Response.Write("<td>"+dr["subject_id"]+"</td>");
                Response.Write("<td>" + dr["subject_name"] + "</td>");
                Response.Write("<td><a href='del_subject.aspx?subject_id=" + dr["subject_id"] + "'>del</a> | <a href='#'>edit</a></td>");
                Response.Write("</tr>"); 
            }
		          
		%>
	   </tbody>

	</table>
    <table class="box-table-a">
		<caption>New User</caption>
		<thead>
			<tr>
				<th scope="col">Subject Name</th>
				<th scope="col">Actions</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td><input type="text" name="txt_SubjectName"/></td> 
				<td><button type="submit" name="btn_submit">Create</button></td>
			</tr>
		</tbody>
	</table>
    <!-- end .content --></div>
  <div class="footer">

    <!-- end .footer --></div>
  <!-- end .container --></div>
  </form>
</body>
<%
    if(Request["btn_submit"]!=null)
    {
        string SubjectName=Convert.ToString(Request["txt_SubjectName"]);
        Class_BLL obj = new Class_BLL();
        int Execute = obj.SubjectMaster_insert(SubjectName);
        if (Execute == 1)
        {
            Response.Redirect("CreateSubjects.aspx");
        }
        
    }
%>

</html>
