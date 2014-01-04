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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <%
        if (Page.Request.QueryString["filename"] != null)
        {
            string value = Page.Request.QueryString["filename"];
            string pdfpath = Server.MapPath("~/files/" + value);
            byte[] pdfData = System.IO.File.ReadAllBytes(pdfpath);
            Response.ContentType = "application/pdf";
            Response.WriteFile(pdfpath);
        }
    %>
       
    
    
    </div>
    </form>
</body>
</html>
