<%@ Page Title="" Language="C#" MasterPageFile="~/Site3.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Account_Test_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
            .img
            {
                height:200px;
                width:650px;
            }
            
            .fixedwidth
            {
                width:180px;
            }
            
            
.notattempted 
{
    background: none repeat scroll 0 0 #fc0596;
    border-color: #c888ae;
    border-style: solid;
    border-width: 1px;
    padding:1.25em;
    color:White;
    font-weight:bold;
}
            


.attempted 
{
    background: none repeat scroll 0 0 #3ed21c;
    border-color: #3aa322;
    border-style: solid;
    border-width: 1px;
    padding:1.25em;
    color:White;
    font-weight:bold;
}

            
.markforlater 
{
    background: none repeat scroll 0 0 #f1f408;   
    border-color:#acae0c;
    border-style: solid;
    border-width: 1px;
    padding:1.25em;
    color:White;
    font-weight:bold;
}  


       
            
            
    </style>

       <script type="text/javascript">
           var TotalSeconds;

           function CreateTimer(Time) {
               TotalSeconds = Time;
               window.setTimeout("Tick()", 1000);
           }

           function Tick() {
               var hour = parseInt(TotalSeconds / 3600);
               var min_sec = parseInt(TotalSeconds % 3600);
               var min = parseInt(min_sec / 60);
               var sec = parseInt(min_sec % 60);
               document.getElementById("timer").innerHTML = hour + ":" + min + ":" + sec;

               if (TotalSeconds == 0) {
                   document.getElementById("form1").submit();
               }
               else {
                   TotalSeconds -= 1;
                   window.setTimeout("Tick()", 1000);
               }
           }
    </script>


    <script type="text/javascript">

        function uncheck(Elm)
        {
            var ParentRow = Elm.parentNode.parentNode;
            var table = ParentRow.cells[0].childNodes[5];
            var row = table.rows;
            for (var i = 0; i < row.length; i++) {
                var radio_btn = row[i].cells[0].childNodes[0];
                if (radio_btn.checked) radio_btn.checked = false;
            }


            var ParentTableRow = ParentRow.parentNode.parentNode.parentNode.parentNode.parentNode;
            var question_id = ParentRow.cells[0].childNodes[1].value;
            var test_id = ParentTableRow.cells[0].childNodes[1].value;
            var subject_id = ParentTableRow.cells[0].childNodes[3].value;


            var table = document.getElementById("<%= grd_subject_dash.ClientID %>");
            var row = table.rows;
            for (var i = 1; i < row.length; i++) {
                var node = row[i].cells[0].childNodes;
                var dash_test_id = node[3].value;
                var dash_subj_id = node[5].value;

                if (parseInt(dash_subj_id) == parseInt(subject_id) && parseInt(dash_test_id) == parseInt(test_id)) {


                    //--getting the table from inside the div at node 7--->
                    var tbl = node[7].getElementsByTagName("table");
                    if (tbl.item(0) != undefined) {
                        var tbl_new = tbl.item(0);
                        var div_columns = tbl_new.rows[1].cells[0].childNodes[1].getElementsByTagName("div");

                        for (var k = 0; k < div_columns.length; k++) {
                            var itm = div_columns.item(k);
                            var n_node = itm.childNodes;
                            var dash_q_id = n_node[0].value;
                            if (parseInt(dash_q_id) == parseInt(question_id)) {

                                itm.className = "column large-1 notattempted";
                            }
                        }

                    }

                }
            }

        }
    
    
    </script>


    <script type="text/javascript">
        function markforlater(Elm) 
        {
            var ParentRow = Elm.parentNode.parentNode;
            var ParentTableRow = ParentRow.parentNode.parentNode.parentNode.parentNode.parentNode;
            var question_id = ParentRow.cells[0].childNodes[1].value;
            var test_id = ParentTableRow.cells[0].childNodes[1].value;
            var subject_id = ParentTableRow.cells[0].childNodes[3].value;


            var table = document.getElementById("<%= grd_subject_dash.ClientID %>");
            var row = table.rows;
            for (var i = 1; i < row.length; i++) {
                var node = row[i].cells[0].childNodes;
                var dash_test_id = node[3].value;
                var dash_subj_id = node[5].value;

                if (parseInt(dash_subj_id) == parseInt(subject_id) && parseInt(dash_test_id) == parseInt(test_id)) {
               

                //--getting the table from inside the div at node 7--->
                var tbl = node[7].getElementsByTagName("table");
                if (tbl.item(0) != undefined) {
                    var tbl_new =tbl.item(0);
                    var div_columns = tbl_new.rows[1].cells[0].childNodes[1].getElementsByTagName("div");
               
                    for (var k = 0; k < div_columns.length; k++) {
                        var itm = div_columns.item(k);
                        var n_node = itm.childNodes;
                        var dash_q_id = n_node[0].value;
                        if (parseInt(dash_q_id) == parseInt(question_id)) {

                            itm.className = "column large-1 markforlater";
                        }
                                                                 }

                }

            }
            }

        }
    </script>

    <script type="text/javascript">
        function attempted(Elm) {
            var ParentRow = Elm.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
            var ParentTableRow = ParentRow.parentNode.parentNode.parentNode.parentNode.parentNode;
            var question_id = ParentRow.cells[0].childNodes[1].value;
            var test_id = ParentTableRow.cells[0].childNodes[1].value;
            var subject_id = ParentTableRow.cells[0].childNodes[3].value;


            var table = document.getElementById("<%= grd_subject_dash.ClientID %>");
            var row = table.rows;
            for (var i = 1; i < row.length; i++) {
                var node = row[i].cells[0].childNodes;
                var dash_test_id = node[3].value;
                var dash_subj_id = node[5].value;

                if (parseInt(dash_subj_id) == parseInt(subject_id) && parseInt(dash_test_id) == parseInt(test_id)) {


                    //--getting the table from inside the div at node 7--->
                    var tbl = node[7].getElementsByTagName("table");
                    if (tbl.item(0) != undefined) {
                        var tbl_new = tbl.item(0);
                        var div_columns = tbl_new.rows[1].cells[0].childNodes[1].getElementsByTagName("div");

                        for (var k = 0; k < div_columns.length; k++) {
                            var itm = div_columns.item(k);
                            var n_node = itm.childNodes;
                            var dash_q_id = n_node[0].value;
                            if (parseInt(dash_q_id) == parseInt(question_id)) {

                                itm.className = "column large-1 attempted";
                            }
                        }

                    }

                }
            }

        }
    </script>



</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="leftPanel1" Runat="Server">
    
    <asp:HiddenField ID="hdn_user_id" runat="server" />
    <asp:HiddenField ID="hdn_usr_role" runat="server" />
    
    <h3 class="large-12" id="timer" style="text-align:center;"></h3>

    

    <hr />
    <asp:GridView ID="grd_subject_dash" 
    runat="server"
    GridLines="None"
    AutoGenerateColumns="False"
    Width="100%">
    <RowStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField>
              <ItemTemplate>
                  




                  

                  <h3><asp:Label ID="Label1" runat="server" Text='<%# Eval("subject_name") %>'></asp:Label></h3>
                  <asp:HiddenField ID="hdn_test_id_dash" runat="server" Value='<%# Eval("test_id") %>' />
                  <asp:HiddenField ID="hdn_subj_id_dash" runat="server" Value='<%# Eval("subject_id") %>' />


                   
                  <asp:GridView ID="grd_question_dash" 
                  runat="server"
                  GridLines="None"
                  Width="100%"
                  AutoGenerateColumns="false">
                  <RowStyle HorizontalAlign="Left" />
                   <Columns>
                     <asp:TemplateField>
                        <ItemTemplate>

                             <div class='row large-12'>
                                <%# Eval("content") %>
                             </div>
                       
                       </ItemTemplate>
                     </asp:TemplateField>
                   </Columns>
                  </asp:GridView>
                 

                <hr />

              </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>




</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="RightPanel" Runat="Server">
  
  
  

  <div style="height:700px;overflow:auto;">

  <asp:GridView ID="grd_test_subjects" 
       runat="server"
       AutoGenerateColumns="false" 
       GridLines="None" 
       Width="100%"
       AllowPaging="false"
       ShowHeaderWhenEmpty="true">
       
       <RowStyle HorizontalAlign="Left" />

       <Columns>
         <asp:TemplateField>
           <ItemTemplate>
             
             <asp:HiddenField ID="hdn_test_id" runat="server" Value='<%# Eval("test_id") %>' />
             <asp:HiddenField ID="hdn_subj_id" runat="server" Value='<%# Eval("subject_id") %>' />
             <h3><asp:Label ID="lbl_subject_name" runat="server" Text='<%# Eval("subject_name") %>'></asp:Label></h3>
             <hr />

             
               <asp:GridView ID="grd_sbj_question" 
               runat="server"
               GridLines="None"
               AutoGenerateColumns="false"
               Width="100%">
               <RowStyle HorizontalAlign="Left" />
                 <Columns>
                    <asp:TemplateField>
                       <ItemTemplate>


                        <asp:HiddenField ID="hdn_q_id" runat="server" Value='<%# Eval("question_id") %>' />

                       <div class="row large-12">
                       <div class="panel">
                       <asp:Image ID="Image1" runat="server" CssClass="img" 
                           ImageUrl='<%# Eval("question_img_name","q_images/{0}") %>' 
                           Visible='<%# Convert.ToBoolean(Eval("image_present")) %>' />
                       <div class="clear"></div>
                       <br />
                     <asp:Label ID="lbl_question_no" runat="server" Text='<%# Eval("question_no") %>'></asp:Label>
                     <label>.</label>
                     <asp:Label ID="lbl_question" runat="server" Text='<%# Eval("question") %>'></asp:Label>
                 </div>
               </div>
              <table width="100%" id="tbl">
               <tr>
                  <td style="width:2%"><input type="radio" id="radbtn_ans1"  name='<%# Eval("question_id") %>' onclick="attempted(this)" runat="server" /></td>
                  <td style="width:985%"><asp:Label ID="lbl_ans1" runat="server" Text='<%# Eval("answer1") %>'></asp:Label></td>
               </tr>
               <tr>
                  <td><input type="radio" id="radbtn_ans2"  name='<%# Eval("question_id") %>' onclick="attempted(this)" runat="server"/></td>
                  <td><asp:Label ID="lbl_ans2" runat="server" Text='<%# Eval("answer2") %>'></asp:Label></td>
               </tr>
               <tr>
                  <td><input type="radio" id="radbtn_ans3"  name='<%# Eval("question_id") %>' onclick="attempted(this)" runat="server"/></td>
                  <td><asp:Label ID="lbl_ans3" runat="server" Text='<%# Eval("answer3") %>'></asp:Label></td>
               </tr>
               <tr>
                  <td><input type="radio" id="radbtn_ans4"  name='<%# Eval("question_id") %>' onclick="attempted(this)" runat="server"/></td>
                  <td><asp:Label ID="lbl_ans4" runat="server" Text='<%# Eval("answer4") %>'></asp:Label></td>
               </tr>
               <tr>
                  <td><input type="radio" id="radbtn_ans5"  name='<%# Eval("question_id") %>' onclick="attempted(this)" runat="server"/></td>
                  <td><asp:Label ID="lbl_ans5" runat="server" Text='<%# Eval("answer5") %>'></asp:Label></td>
               </tr>
              </table>

              <input type="button" class="small button" onclick="uncheck(this)" value="UNCHECK" />
              <input type="button" class="small button" onclick="markforlater(this)" value="MARK FOR LATER" />

                       </ItemTemplate>

                       



                     </asp:TemplateField>
                  </Columns>
               </asp:GridView>


               
               
           </ItemTemplate>
         </asp:TemplateField>
      </Columns>
    </asp:GridView>

    <hr />

      <asp:Button ID="btn_submit" runat="server" Text="SUBMIT" 
          CssClass="small button" onclick="btn_submit_Click" />




</div>
</asp:Content>

