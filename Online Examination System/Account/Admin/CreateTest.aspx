<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="CreateTest.aspx.cs" Inherits="Account_Admin_CreateTest2" %>
<%@ Register Src="~/Account/Admin/UserControl/Navigation.ascx" TagPrefix="uc" TagName="Navigation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBar1" Runat="Server">
<uc:Navigation ID="Navigation1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">

<div class="row panel">
<h3>FILL IN TO CREATE TEST</h3>
 <table width="100%">
  <tr>
   <td style="width:15%">TEST NAME:</td>
   <td><asp:TextBox ID="Txt_test_name" runat="server" CssClass="text"></asp:TextBox></td>
  </tr>
  <tr>
  <td>TEST TIME(min)</td>
  <td>
      <asp:TextBox ID="txt_test_time" runat="server" CssClass="text"></asp:TextBox></td>
  </tr>
 </table>
 <hr />
    <asp:GridView ID="grd_subject" 
    runat="server"
    GridLines="None"
    ShowHeaderWhenEmpty="True"
    AutoGenerateColumns="False"
    Width="100%"
    DataKeyNames="subject_id">
    <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField HeaderText="SUBJECT NAME">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("subject_name") %>'></asp:Label>
                   <hr />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CHECK">
                <ItemTemplate>
                    <asp:CheckBox ID="chk_subject" runat="server" />
                    <hr />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

 
 <table>
   <tr>
     <td style="width:15%">TEST ACTIVE</td>
     <td><asp:CheckBox ID="chk_test_active" runat="server" /></td>
   </tr>
 </table>
  
  <hr />
 <asp:Button ID="btn_create_test" runat="server" Text="CREATE TEST" 
        CssClass="small button" onclick="btn_create_test_Click" />
</div>


<div class="row panel">
    <asp:GridView ID="grd_test" 
    runat="server"
    GridLines="None"
    Width="100%"
    AutoGenerateColumns="False"
    ShowHeaderWhenEmpty="true"
    DataKeyNames="test_id" onrowcancelingedit="grd_test_RowCancelingEdit" 
        onrowediting="grd_test_RowEditing" onrowupdating="grd_test_RowUpdating" 
        onrowdeleting="grd_test_RowDeleting">

    <RowStyle HorizontalAlign="Center" />

        <Columns>
            <asp:TemplateField HeaderText="TEST ID">
                <EditItemTemplate>
                     <asp:Label ID="lbl_test_id_edit" runat="server" Text='<%# Eval("test_id") %>'></asp:Label>


                   <asp:GridView ID="grd_edit_test_subject" runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="subject_id"
                    GridLines="None"
                    Width="100%">

                    <RowStyle HorizontalAlign="Center" />

                        <Columns>
                            <asp:TemplateField HeaderText="CHK">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_sbj_edit" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SUBJECT NAME">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("subject_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>





                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_test_id" runat="server" Text='<%# Eval("test_id") %>'></asp:Label>
                <hr />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TEST NAME">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_test_name_edit" runat="server" Text='<%# Eval("test_name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_test_name" runat="server" Text='<%# Eval("test_name") %>'></asp:Label>
                <hr />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TEST TIME(MIN)">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_test_time_edit" runat="server" Text='<%# Convert.ToInt32(Eval("test_time_sec"))/60 %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_test_time" runat="server" Text='<%#  Convert.ToInt32(Eval("test_time_sec"))/60 %>'></asp:Label>
                <hr />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TEST ACTIVE">
                <EditItemTemplate>
                    <asp:CheckBox ID="chk_active_edit" runat="server" Checked='<%# Convert.ToBoolean(Eval("test_active")) %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chk_active" runat="server"  Checked='<%# Convert.ToBoolean(Eval("test_active")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:CommandField ShowEditButton="true" HeaderText="EDIT" />
                <asp:CommandField ShowDeleteButton="true" HeaderText="DELETE" />

        </Columns>
        
    </asp:GridView>
</div>


</asp:Content>

