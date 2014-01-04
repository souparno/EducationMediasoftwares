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
    <link href="../Css/Common/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
<form action="Login.aspx" method="post"  >
<div class="container">
  <div class="header"><a href="#"><img src="../images/Ascent logo.png" alt="Insert Logo Here" name="Insert_logo" width="180" height="90" id="Insert_logo" style="background: #C6D580; display:block;" /></a> 
    <!-- end .header --></div>
          <div  class="Menu">
         <ul>
          <li><a href="../Home.aspx">Home</a></li>
          <li><a href="Download.aspx"   >Download</a></li>
          <li><a href="Register.aspx" >Register</a></li>
          <li><a href="#"  >Login</a></li>
          <li><a href="AboutUs.aspx" >About Us</a></li>
         </ul>
          <!-- end .Menu -->
         </div>
  <div class="content">
    <div class="payment" >
   	<fieldset>
		<legend>Your details</legend>
		<ol>
			<li>
				<label for="name">User Name</label>
				<input id="Text1" name="txt_UserName" type="text" placeholder="User Name" required autofocus>
			</li>
			<li>
				<label for="phone">Password</label>
				<input id="Tel1" name="txt_password" type="tel" placeholder="Password" required>
			</li>
		</ol>
	</fieldset>

	<fieldset>
		<button type="submit" name="btn_Submit">LogIn</button>
	</fieldset>
   </div>
    <!-- end .content --></div>
  <div class="footer">
    <!-- end .footer --></div>
  <!-- end .container --></div>
  </form>
</body>
<%
    if (Request["btn_submit"] != null)
    {
        string UserName = Request["txt_UserName"];
        string Password = Request["txt_password"];

        Class_BLL obj = new Class_BLL();
        DataTable dt = obj.user_present(UserName, Password);
        
        if(dt.Rows.Count>0)
        {
            if ((dt.Rows[0]["UserRole"]).Equals("USER"))
            {
                Session["UserId"] = dt.Rows[0]["UserId"];
                Session["UserName"] = UserName;
                Response.Redirect("~/user/Add_To_Cart.aspx");
            }
            else if ((dt.Rows[0]["UserRole"]).Equals("ADMIN"))
            {
                Session["UserId"] = dt.Rows[0]["UserId"];
                Session["UserName"] = UserName;
                Response.Redirect("~/admin/CreateCourses.aspx");
            }
        }
  }  
%>
</html>
