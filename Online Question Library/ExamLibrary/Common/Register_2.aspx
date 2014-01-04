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
    
<link href="../Css/Common/StyleSheet.css" type="text/css" rel="Stylesheet" />
    
</head>
<body>
   <form  runat="server" method="post" action="Register_2.aspx"   >
   
   <div class="payment" >
   	<fieldset>
		<legend>Your details</legend>
		<ol>
			<li>
				<label for="name">User Name</label>
				<input id="Text1" name="txt_UserName" type="text" placeholder="User Name" required autofocus>
			</li>
			<li>
				<label for="name">First Name</label>
				<input id="name" name="txt_FirstName" type="text" placeholder="First Name" required autofocus>
			</li>
				<li>
				<label for="name">Last Name</label>
				<input id="Text2" name="txt_LastName" type="text" placeholder="Last Name" required autofocus>
			</li>
			<li>
				<label for="email">Email</label>
				<input id="email" name="txt_Email" type="email" placeholder="example@domain.com" required>
			</li>
			<li>
				<label for="phone">Phone</label>
				<input id="phone" name="phone" type="tel" placeholder="Eg. +447500000000" required>
			</li>
				<li>
				<label for="phone">Password</label>
				<input id="Tel1" name="txt_password" type="tel" placeholder="Password" required>
			</li>
		</ol>
	</fieldset>

	<fieldset>
		<button type="submit" name="btn_Submit">Buy it!</button>
	</fieldset>
   </div>
   
   

</form>
</body>
<%
     
    if (Request["btn_Submit"] == null)
    {
       
        if (Request.QueryString["CourseId"] != null)
        {
            int CourseId = Convert.ToInt32(Request.QueryString["CourseId"]);
            string CourseName = Request.QueryString["CourseName"];
            Double price = Convert.ToDouble(Request.QueryString["Price"]);
            Session["Path"] = "~/User/cart.aspx?CourseId=" + CourseId + "&CourseName=" + CourseName + "&Price=" + price;
        }
        else if (Request.QueryString["SubjectId"] != null)
        {
            int SubjectId = Convert.ToInt32(Request.QueryString["SubjectId"]);
            string SubjectName = Request.QueryString["SubjectName"];
            Double price = Convert.ToDouble(Request.QueryString["Price"]);
            Session["Path"] = "~/User/cart.aspx?SubjectId=" + SubjectId + "&SubjectName=" + SubjectName + "&Price=" + price;
        }
    }   
    else if (Request["btn_Submit"] != null)
    {
        string UserName = Convert.ToString(Request["txt_UserName"]);
        string FirstName = Convert.ToString(Request["txt_FirstName"]);
        string LastName = Convert.ToString(Request["txt_LastName"]);
        string Password = Convert.ToString(Request["txt_password"]);
        string Email = Convert.ToString(Request["txt_Email"]);
        Class_BLL obj = new Class_BLL();
        int UserId=obj.UserMaster_Insert(FirstName, LastName, UserName, Email, Password);
        if (UserId != 0)
        {
           Session["UserName"] = UserName;
           Session["UserId"] = UserId;
           Response.Redirect(Convert.ToString(Session["Path"]));
             
        }
        
    }
  
    
%>

</html>
