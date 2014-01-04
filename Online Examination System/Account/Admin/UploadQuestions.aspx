<%@ Page Title="" Language="C#" Debug="true" ValidateRequest="false" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="UploadQuestions.aspx.cs" Inherits="Account_Admin_UploadQuestions" %>
<%@ Register Src="~/Account/Admin/UserControl/Navigation.ascx" TagPrefix="uc" TagName="Navigation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
.fixedsize
{
    width:180px;
}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBar1" Runat="Server">
<uc:Navigation runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">


<div class="row panel">

<table>
<tr>
<td>TEST</td>
<td>
   <asp:DropDownList ID="ddl_test" runat="server" CssClass="fixedsize" AutoPostBack="true">
   <asp:ListItem>--SELECT TEST--</asp:ListItem>
   </asp:DropDownList>
</td>
<td></td>
</tr>
<tr>
<td>SUBJECT</td>
<td>
   <asp:HiddenField ID="hdn_subject_id" runat="server" />
   <asp:DropDownList ID="ddl_subject" runat="server" CssClass="fixedsize" 
       AutoPostBack="true">
   <asp:ListItem>--SELECT SUBJECT--</asp:ListItem>
   </asp:DropDownList>
</td>
<td></td>
</tr>
<tr>
<td><label>UPLOAD QUESTION FILE</label></td>
<td><asp:FileUpload ID="q_upload" runat="server"/></td>
<td><asp:Button ID="btn_upload" runat="server" Text="VIEW EXCEL SHEETS" CssClass="small button fixedsize" onclick="btn_upload_Click" /></td>
</tr>
<tr>
<td><label>SELECT EXCEL SHEET</label></td>
<td>
<asp:DropDownList ID="ddl_excel_sheets" runat="server" CssClass="fixedsize">
<asp:ListItem>--Select Sheet Name--</asp:ListItem>
</asp:DropDownList>
</td>
<td>    <asp:Button ID="btn_viewquestion" runat="server" Text="VIEW QUESTION" onclick="btn_viewquestion_Click" CssClass="small button fixedsize" />
</td>
</tr>
</table>
 
    </div>

   

    <div class="row panel">
    
    <asp:GridView ID="grd_questions" 
    runat="server" 
    AutoGenerateColumns="false" 
    GridLines="None" 
    Width="100%"
    AllowPaging="false"
    ShowHeaderWhenEmpty="true">

    <PagerSettings  Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
        <Columns>
            <asp:TemplateField HeaderText="REVIEW QUESTIONS">
             <ItemTemplate>
             

     <table width="100%">
     <tr>
     <td style="width:5%;">Question No.</td>
     <td>
       <asp:TextBox ID="txt_question_no" runat="server" CssClass="text" Text='<%# Eval("question_no") %>'></asp:TextBox>
     </td>
     </tr>
     <tr>
     <td>Question</td>
     <td>
       <asp:TextBox ID="txt_question" runat="server" TextMode="MultiLine" CssClass="text area" Text='<%# Eval("question") %>'></asp:TextBox>
     </td>
     </tr>
     <tr>
      <td>Question Image</td>
      <td>
          <asp:FileUpload ID="upload_image" runat="server" CssClass="button small" />
      </td>
    </tr>
    <tr>
    <td>Answer1</td>
    <td>
       <asp:TextBox ID="txt_answer1" runat="server" CssClass="text" Text='<%# Eval("answer1") %>'></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>Answer2</td>
    <td>
       <asp:TextBox ID="txt_answer2" runat="server" CssClass="text" Text='<%# Eval("answer2") %>'></asp:TextBox>
    </td>
    </tr> 
    <tr>
    <td>Answer3</td>
    <td>
       <asp:TextBox ID="txt_answer3" runat="server" CssClass="text" Text='<%# Eval("answer3") %>'></asp:TextBox>
    </td>
    </tr> 
    <tr>
    <td>Answer4</td>
    <td>
       <asp:TextBox ID="txt_answer4" runat="server" CssClass="text" Text='<%# Eval("answer4") %>'></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>Answer5</td>
    <td>
       <asp:TextBox ID="txt_answer5" runat="server" CssClass="text" Text='<%# Eval("answer5") %>'></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>Correct Option</td>
    <td>
       <asp:TextBox ID="txt_correct_option" runat="server" CssClass="text" Text='<%# Eval("correct_option") %>'></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>Positive Marks</td>
    <td>
       <asp:TextBox ID="txt_posmarks" runat="server" CssClass="text" Text='<%# Eval("posmarks") %>'></asp:TextBox>
    </td>
    </tr> 
    <tr>
    <td>Negetive Marks</td>
    <td>
       <asp:TextBox ID="txt_negmarks" runat="server" CssClass="text" Text='<%# Eval("negmarks") %>'></asp:TextBox>
    </td>
    </tr>
    </table>
    <hr />
    
    


             </ItemTemplate>
             </asp:TemplateField>
        </Columns>
    </asp:GridView>

        <asp:Button ID="btn_save" runat="server" Text="SAVE" CssClass="small button" 
            onclick="btn_save_Click" />

     </div> 

</asp:Content>

