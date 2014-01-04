<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
     <link href="../Css/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <link href="../Css/Menu.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div  class="container">
        <div class="Header">
            <a href="#">
            <img src="../images/Ascent logo.png" class="Image"  alt="Insert Logo Here" name="Insert_logo" id="Insert_logo" />
            </a> 
      <%--End Of The Div Header--%>
       </div>
        <div  class="Menu">
         <ul>
          <li><a href="../Home.aspx" >Home</a></li>
          <li><a href="FreeDemo.aspx">Free Demo</a></li>
          <li><a href="Register.aspx">Register</a></li>
          <li><a href="Login.aspx" >Login</a></li>
          <li><a href="#" >About Us</a></li>
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
