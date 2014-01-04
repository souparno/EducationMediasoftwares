<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TakeATest.aspx.cs" Inherits="Account_Admin_TakeATest" %>
<%@ Register Src="~/Account/Admin/UserControl/Navigation.ascx" TagPrefix="uc" TagName="Navigation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/Site5.css"  rel="Stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
      <div class="header">
        <h1>Online Examination Syatem</h1>
      </div>
      <div class="content">
      <div class="column large-4">
       <uc:Navigation runat="server" />
      </div>
      <div class="column large-8">
      <div class="row large-12">
        <h2><asp:Label ID="lbl_test" runat="server" Text=""></asp:Label></h2>
      </div>
        <div class="row large-6">
         <div class="column large-12">
          <asp:Button ID="btn_start_test" runat="server" Text="START TEST" 
                 CssClass="button small" onclick="btn_start_test_Click" />
          </div>
        </div>
      </div>
      </div>    
    </div>
    </form>
</body>
</html>
