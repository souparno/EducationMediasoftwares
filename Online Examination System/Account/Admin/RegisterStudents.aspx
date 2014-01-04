<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="RegisterStudents.aspx.cs" Inherits="Account_Admin_RegisterStudents" %>
<%@ Register Src="~/Account/Admin/UserControl/Navigation.ascx" TagPrefix="uc" TagName="Navigation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBar1" Runat="Server">
<uc:Navigation ID="Navigation1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">

<div class="row panel">
<h3>FILL IN TO REGISTER STUDENT</h3>
 <table width="100%">
  <tr>
   <td style="width:5%">NAME:</td>
   <td><asp:TextBox ID="txt_name" runat="server" CssClass="text"></asp:TextBox></td>
  </tr>
  <tr>
   <td>CONTACT</td>
   <td><asp:TextBox ID="txt_contact" runat="server" CssClass="text"></asp:TextBox></td>
  </tr>
  <tr>
   <td>PASSWORD</td>
   <td><asp:TextBox ID="txt_password" runat="server" CssClass="text"></asp:TextBox></td>  
  </tr>
 </table> 
  <hr />
    <asp:Button ID="btn_register" runat="server" Text="REGISTER" 
        CssClass="small button" onclick="btn_register_Click"/>
</div>


<div class="row panel">
    <asp:GridView ID="grd_students" runat="server" 
    AutoGenerateColumns="False"
    Width="100%"
    GridLines="None"
    DataKeyNames="usr_id" 
    onrowediting="grd_students_RowEditing" 
        onrowupdating="grd_students_RowUpdating" 
        onrowcancelingedit="grd_students_RowCancelingEdit" 
        onrowdeleting="grd_students_RowDeleting"
        ShowHeaderWhenEmpty="true">

        <RowStyle HorizontalAlign="Center" />
        <EditRowStyle Width="100%" />

        <Columns>
            <asp:TemplateField HeaderText="STUDENT ID">
                <EditItemTemplate>
                    <asp:Label ID="lbl_usr_id_edit" runat="server" Text='<%# Eval("usr_id") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_usr_id" runat="server" Text='<%# Eval("usr_id") %>'></asp:Label>
                    <hr />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="STUDENT NAME">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_usr_name" runat="server" Text='<%# Eval("usr_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_usr_name" runat="server" Text='<%# Eval("usr_name") %>'></asp:Label>
                    <hr />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CONTACT">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_usr_contact" runat="server" Text='<%# Eval("usr_contact") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_usr_contact" runat="server" Text='<%# Eval("usr_contact") %>'></asp:Label>
                    <hr />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="USER CODE">
                <EditItemTemplate>
                    <asp:Label ID="lbl_usr_code_edit" runat="server" Text='<%# Eval("usr_code") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_usr_code" runat="server" Text='<%# Eval("usr_code") %>'></asp:Label>
                    <hr />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PASSWORD">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_usr_pass" runat="server" Text='<%# Eval("usr_password") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_usr_pass" runat="server" Text='<%# Eval("usr_password") %>'></asp:Label>
                    <hr />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="true" HeaderText="EDIT" />
            <asp:CommandField ShowDeleteButton="true" HeaderText="DELETE" />
        </Columns>
    </asp:GridView>
</div>


</asp:Content>

