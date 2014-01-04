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
<%@ Import Namespace="BLL"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="Css/Menu.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div  class="container">
        <div class="Header">
          <a href="#">
          <img src="images/Ascent logo.png"class="Image" alt="Insert Logo Here" name="Insert_logo" id="Insert_logo" />
          </a>
       
       <%--End Of The Div Header--%>
       </div>
        <div  class="Menu">
         <ul>
          <li><a href="#">Home</a></li>
          <li><a href="Common/Download.aspx" >Download</a></li>
          <li><a href="Common/Register.aspx"  >Register</a></li>
          <li><a href="Common/Login.aspx" >Login</a></li>
          <li><a href="Common/AboutUs.aspx">About Us</a></li>
         </ul>
         <%--End Of The Div Menu--%>
         </div>
        <div class="content">
           <%-- End Of The Division Content --%>
      </div>
      <div class="footer">
        <%-- End Of The Division footer --%>
        </div>
      <%-- End Of The Division Container --%>
      </div> 
    </form>
</body>
</html>
