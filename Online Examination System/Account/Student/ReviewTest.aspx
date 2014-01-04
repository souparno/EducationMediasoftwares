<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReviewTest.aspx.cs" Inherits="Account_Admin_ReviewTest" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Account/Student/UserControl/Navigation.ascx" TagPrefix="uc" TagName="Navigation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/Site5.css" rel="Stylesheet" type="text/css" />

<style type="text/css">
    .fixedsize
{
    width:180px;
}
</style>





</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
      <div class="header">
       <h1>Online Examination System</h1>
      </div>
      <div class="content">
       <div class="column large-4">
        <uc:Navigation runat="server" />
       </div>
       <div class="column large-8">

       <div class="row large-7">

           <asp:HiddenField ID="hdn_user_id" runat="server" />

           <asp:DropDownList ID="ddl_test_name" runat="server" AutoPostBack="true" CssClass="fixedsize">
            <asp:ListItem>SELECT TEST</asp:ListItem>
           </asp:DropDownList>
       </div>

       <div class="row large-7">
          <asp:Button ID="btnview_result" runat="server" Text="VIEW RESULT" 
               CssClass="small button" onclick="btnview_result_Click" />
       </div>

       <div class="row large-12">
           <%--<CR:CrystalReportViewer ID="CrystalReportViewer_result" runat="server" AutoDataBind="true" />--%>

           <asp:GridView ID="grd_review_test" 
           runat="server"
           GridLines="None"
           AutoGenerateColumns="false"
           Width="100%">

           <RowStyle HorizontalAlign="Center" />

           <Columns>
            <asp:TemplateField>

            <ItemTemplate>
              <div class="row large-12">
                <div class="column large-6"><asp:Label ID="lbl_subject_name" runat="server" 
                        Text='<%# Eval("subject_name") %>'></asp:Label></div>
                <div class="column large-6"><asp:Label ID="lbl_marks" runat="server" 
                        Text='<%# Eval("Marks") %>'></asp:Label></div>
              </div>
            </ItemTemplate>


            </asp:TemplateField>
           </Columns>
           
           </asp:GridView>

           <hr />
           <h3><asp:Label ID="lbl_tot_marks" runat="server" Text=""></asp:Label></h3>
           <hr />
           


       </div>

        
       </div>
      </div>
    </div>
    </form>
</body>
</html>
