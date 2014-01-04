<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateQuestion.aspx.cs" Inherits="Account_Admin_CreateQuestion" %>
<%@ Register TagPrefix="uc" TagName="menu" Src="~/Account/Admin/UserControl/Navigation.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
.textarea
{
    overflow: auto; 
    width: 740px; 
    line-height: 30px; 
    font-size: 18px; 
    color: #333333; 
    padding-left: 10px; 
    padding-top: 5px; 
    height: 175px; 
    -moz-border-radius: 5px; 
    -webkit-vorder-radius: 5px;
}

.textbox
{
    overflow: auto; 
    width: 740px; 
    line-height: 30px; 
    font-size: 18px; 
    color: #333333; 
    padding-left: 10px; 
    padding-top: 5px; 
    -moz-border-radius: 5px; 
    -webkit-vorder-radius: 5px;
}


.BlueH1 {
    color: #22719C;
    float: left;
    font-size: 24px;
    font-weight: 400;
    line-height: 25px;
    margin-bottom: 6px;
    text-align: left;
    width: 100%;
}


</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationContent" Runat="Server">
<uc:menu runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" Runat="Server">




<h1>Upload Questions</h1>
<asp:FileUpload ID="FileUpload1" runat="server"/>

<asp:GridView ID="grd_question" runat="server"
AutoGenerateColumns="False" 
CellPadding="4" 
ForeColor="#333333" 
GridLines="None"  
AllowPaging="true"
PageSize="1">

    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
             
             

             <table>
             <tr>
             <td><asp:Label ID="Label1" runat="server" Text='<%# Eval("question_id") %>'></asp:Label></td>
             <td><asp:TextBox ID="TextBox2" runat="server" CssClass="textarea" TextMode="MultiLine"></asp:TextBox></td>
             </tr>
             <tr>
             <td>
                 <asp:RadioButton ID="RadioButton1" runat="server" />
             </td>
             <td>
                 <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox"></asp:TextBox>
             </td>
             </tr>
             <tr>
             <td>
                 <asp:RadioButton ID="RadioButton2" runat="server" />
             </td>
             <td>
                 <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox"></asp:TextBox>
             </td>
             </tr>
             <tr>
             <td>
                 <asp:RadioButton ID="RadioButton3" runat="server" />
             </td>
             <td>
                 <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox"></asp:TextBox>
             </td>
             </tr>
             <tr>
             <td>
                 <asp:RadioButton ID="RadioButton4" runat="server" />
             </td>
             <td>
                 <asp:TextBox ID="TextBox5" runat="server" CssClass="textbox"></asp:TextBox>
             </td>
             </tr>
             </table>
             

            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    
   


    <RowStyle BackColor="#E3EAEB" />




</asp:GridView>












</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent2" Runat="Server">
</asp:Content>

