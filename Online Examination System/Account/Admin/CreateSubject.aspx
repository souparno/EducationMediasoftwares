<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="CreateSubject.aspx.cs" Inherits="Account_Admin_CreateSubject2" %>
<%@ Register Src="~/Account/Admin/UserControl/Navigation.ascx" TagPrefix="uc" TagName="Navigation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBar1" Runat="Server">
<uc:Navigation ID="Navigation1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">

<div class="row panel">
<h3>FILL IN TO CREATE SUBJECT</h3>
 <table width="100%">
  <tr>
   <td style="width:15%">SUBJECT NAME:</td>
   <td><asp:TextBox ID="Txt_subject_name" runat="server" CssClass="text"></asp:TextBox></td>
  </tr>
 </table> 
  <hr />
    <asp:Button ID="btn_create_subject" runat="server" Text="CREATE SUBJECT" 
        CssClass="small button" OnClick="btn_create_subject_Click" />
</div>

<div class="row panel">

 <asp:GridView ID="grd_subject" runat="server" AutoGenerateColumns="False" 
         DataKeyNames="subject_id" 
         onrowdeleting="grd_subject_RowDeleting" 
         onrowediting="grd_subject_RowEditing" 
         onrowcancelingedit="grd_subject_RowCancelingEdit" 
         onrowupdating="grd_subject_RowUpdating"
         Width="100%"
         ShowHeaderWhenEmpty="true"
         GridLines="None">

         <RowStyle HorizontalAlign="Center" />

        <Columns>
               <asp:TemplateField HeaderText="SUBJECT ID">
                   <EditItemTemplate>
                       <asp:Label ID="lbl_subject_id_edit" runat="server" Text='<%# Eval("subject_id") %>'></asp:Label>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:Label ID="lbl_subject_id" runat="server" Text='<%# Eval("subject_id") %>'></asp:Label>
                           <hr />
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="SUBJECT NAME">
                   <EditItemTemplate>
                       <asp:TextBox ID="txt_subject_name_edit" runat="server" Text='<%# Eval("subject_name") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:Label ID="lbl_subject_name" runat="server" Text='<%# Eval("subject_name") %>'></asp:Label>
                        <hr />
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:CommandField ShowEditButton="true" HeaderText="EDIT" />
               <asp:CommandField ShowDeleteButton="true" HeaderText="DELETE" />

              

        </Columns>
    </asp:GridView>

</div>

</asp:Content>

