<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <table width="100%">
          <tr>
            <td>ID</td>
            <td><asp:TextBox ID="txt_user_name" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
           <td>PASSWORD</td>
           <td><asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox></td>
          </tr>
          <tr>
           <td><asp:Button ID="btn_login" runat="server" Text="LOGIN" CssClass="button small" 
                   onclick="btn_login_Click" /></td>
           <td></td>
          </tr>
         </table>
</asp:Content>

