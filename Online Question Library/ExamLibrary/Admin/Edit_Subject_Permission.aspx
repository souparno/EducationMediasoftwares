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
    <form id="form1" runat="server" method="post" action="Edit_Subject_Permission.aspx"  >
   <table class="box-table-a">
		<caption>Subject Permission</caption>
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
	   	               	    
               Response.Write("<tr>");
               Response.Write("<td>" + Request.QueryString["requisition_UserName"] + "</td>");
               Response.Write("<td>" + Request.QueryString["requisition_SubjectName"] + "</td>");
               Response.Write("<td>" + Request.QueryString["requisition_Price"] + "</td>");
               if (Convert.ToInt32(Request.QueryString["requisition_Permission"]) == 0)
               {
                   Response.Write("<td><input type='checkbox' name='chk_Subject' /></td>");
               }
               else
               {
                   Response.Write("<td><input type='checkbox' name='chk_Subject' checked='checked'/></td>");
               }
               Response.Write(" <td><input type='submit' name='btn_submit' value='Save' /></td>");
               Response.Write("</tr>");
         
	   %>
	   </tbody>
	</table>
    </form>
</body>
</html>
<%

     if (Request["btn_submit"] == null)
    {
        Session["_update_course_userid"] = Convert.ToInt32(Request.QueryString["requisition_UserId"]);
        Session["_update_course_subjectid"] = Convert.ToInt32(Request.QueryString["requisition_SubjectId"]);
    }
    else if(Request["btn_submit"]!=null)
    {
        string Permission = Request["chk_Subject"] == null ? "off" : "on";
        Class_BLL obj = new Class_BLL();
        int Execute = obj.Update_User_Permission_Subject(Convert.ToInt32(Session["_update_course_userid"]), Convert.ToInt32(Session["_update_course_subjectid"]), Permission.Equals("on") ? 1 : 0);
        if (Execute == 1)
        {
           Response.Redirect("Edit_User_Permisson.aspx?UserId="+ Session["_update_course_userid"]);
        }
        
    }
    
    
    %>