<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReviewTestQuestions.aspx.cs" Inherits="Account_Admin_ReviewTestQuestions" %>
<%@ Register Src="~/Account/Admin/UserControl/Navigation.ascx" TagPrefix="uc" TagName="navigation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/Site5.css"  rel="Stylesheet" type="text/css"/>
    <style type="text/css">
    .fixedsize
{
    width:180px;
}
/* TEXT AREA AND TEXT BOX
--------------------------------------------------------------*/

.text
{
    -moz-box-sizing: border-box;
    background-color: white;
    border: 1px solid #CCCCCC;
    box-shadow: 0 1px 2px rgba(0, 0, 0, 0.1) inset;
    color: rgba(0, 0, 0, 0.75);
    display: block;
    font-family: inherit;
    font-size: 0.875em;
    margin: 0 0 1em;
    padding: 0.5em;
    transition: box-shadow 0.45s ease 0s, border-color 0.45s ease-in-out 0s;
    width: 100%;
    min-height: 30px;
}


.area
{
        height: 175px; 
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
       <uc:navigation runat="server" />
      </div>
      <div class="column large-8">
      <div class="row large-7">
           <asp:DropDownList ID="ddl_test" runat="server" AutoPostBack="true" CssClass="fixedsize">
            <asp:ListItem>SELECT TEST</asp:ListItem>
           </asp:DropDownList>
       </div>
       <div class="row large-7">
           <asp:DropDownList ID="ddl_subject" runat="server" AutoPostBack="true" CssClass="fixedsize">
            <asp:ListItem Selected="True">SELECT SUBJECT</asp:ListItem>
           </asp:DropDownList>
       </div>

       <div class="row large-7">
          <asp:Button ID="btn_view_questions" runat="server" Text="VIEW QUESTIONS" 
               CssClass="small button" onclick="btn_view_questions_Click"/>
       </div>

       <div class="row large-12">
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

       </div>

      </div>
     </div>
    </div>
    </form>
</body>
</html>
