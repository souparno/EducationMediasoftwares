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
    <form id="form1" runat="server" method="post" action="Cart.aspx" >
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
      <li><a href="Requisition.aspx" >Requisition Sent</a></li>
      <li><a href="Inbox.aspx">Inbox</a></li>
      <li><a href="#">Cart</a></li>
    </ul>

    <!-- end .sidebar1 --></div>
  <div class="content">
   <table class="box-table-a">
		<caption>Courses Bought</caption>
		<thead>
			<tr>
				<th scope="col">Course Name</th>
				<th scope="col">Price</th>
				<th scope="col">Action</th>
		    </tr>
		</thead>
		<tbody>
	   <%
	    
	          
	   
           DataTable Course_Cart = (DataTable)Session["Courses"];
           if (Course_Cart == null)
           {
               Course_Cart = new DataTable();
               Course_Cart.Columns.Add("UserId", typeof(int));
               Course_Cart.Columns.Add("CourseId", typeof(int));
               Course_Cart.Columns.Add("CourseName", typeof(string));
               Course_Cart.Columns.Add("Price", typeof(double));
           }
           if (Request.QueryString["CourseId"] != null)
           {
               DataRow dr = Course_Cart.NewRow();
               dr["UserId"] = Convert.ToInt32(Session["UserId"]);
               dr["CourseId"] = Convert.ToInt32(Request.QueryString["CourseId"]);
               dr["CourseName"] = Convert.ToString(Request.QueryString["CourseName"]);
               dr["Price"] = Convert.ToDouble(Request.QueryString["Price"]);
               Course_Cart.Rows.Add(dr);
               Session["Courses"] = Course_Cart;
           }


           if (((DataTable)Session["Courses"]) != null)
           {
               foreach (DataRow dr in ((DataTable)Session["Courses"]).Rows)
               {
                   Response.Write("<tr>");
                   Response.Write("<td>" + dr["CourseName"] + "</td>");
                   Response.Write("<td>" + dr["Price"] + "</td>");
                   Response.Write("<td><a href='Cancel.aspx?type=CourseCart&CourseId="+ dr["CourseId"]+"'>Cancel</a></td>");
                   Response.Write("</tr>");
               }
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
				<th scope="col">Action</th>
		    </tr>
		</thead>
		<tbody>
	   <%
	    
           DataTable Subject_Cart = (DataTable)Session["Subjects"];
           if (Subject_Cart == null)
           {
                   Subject_Cart = new DataTable();
                   Subject_Cart.Columns.Add("UserId", typeof(int));
                   Subject_Cart.Columns.Add("SubjectId", typeof(int));
                   Subject_Cart.Columns.Add("SubjectName", typeof(string));
                   Subject_Cart.Columns.Add("Price", typeof(double));
           }
           if (Request.QueryString["SubjectId"] != null)
           {
                   DataRow dr = Subject_Cart.NewRow();
                   dr["UserId"] = Convert.ToInt32(Session["UserId"]);
                   dr["SubjectId"] = Request.QueryString["SubjectId"];
                   dr["SubjectName"] = Request.QueryString["SubjectName"];
                   dr["Price"] = Request.QueryString["Price"];
                   Subject_Cart.Rows.Add(dr);
                   Session["Subjects"] = Subject_Cart;
           }
	       if(((DataTable)Session["Subjects"])!=null)
           {
               foreach (DataRow dr in ((DataTable)Session["Subjects"]).Rows)
               {
                   Response.Write("<tr>");
                   Response.Write("<td>" + dr["SubjectName"] + "</td>");
                   Response.Write("<td>" + dr["Price"] + "</td>");
                   Response.Write("<td><a href='Cancel.aspx?type=SubjectCart&SubjectId=" + dr["SubjectId"] + "'>Cancel</a></td>");
                   Response.Write("</tr>");
               }
           }
	             
	   %>
	   </tbody>
	</table>
   <a href="Add_To_Cart.aspx">Buy More</a> 
  <input type="submit" name="btn_submit" value="Send Requisition" />
     <!-- end .content --></div>
  <div class="footer">

    <!-- end .footer --></div>
  <!-- end .container --></div>
    </form>
</body>
</html>
<%
    if (Request["btn_submit"] != null)
    {
        DataTable Course_Cart = (DataTable)Session["Courses"];
        DataTable Subject_Cart = (DataTable)Session["Subjects"];
        Class_BLL obj = new Class_BLL();

        if (Course_Cart != null)
        {
            foreach (DataRow dr in Course_Cart.Rows)
            {
                int UserId = Convert.ToInt32(dr["UserId"]);
                int CourseId = Convert.ToInt32(dr["CourseId"]);
                double Price = Convert.ToDouble(dr["Price"]);
                obj.UserRequisitionCourse_Insert(UserId, CourseId, Price, 0);
            }
        }
        if (Subject_Cart != null)
        {
            foreach (DataRow dr in Subject_Cart.Rows)
            {
                int UserId = Convert.ToInt32(dr["UserId"]);
                int SubjectId = Convert.ToInt32(dr["SubjectId"]);
                double Price = Convert.ToDouble(dr["Price"]);
                obj.UserRequisitionSubject_Insert(UserId, SubjectId, Price, 0);
            }
        }
        if (Session["Courses"] != null)
        {
            Session.Remove("Courses");
        }
        if (Session["Subjects"] != null)
        {
            Session.Remove("Subjects");
        }
        Response.Redirect("Requisition.aspx");
    }
%>