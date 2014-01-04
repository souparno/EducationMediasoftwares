<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="System"%>
<%@ Import Namespace="System.Collections"  %>
<%@ Import Namespace="System.Configuration"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System.Linq"  %>
<%@ Import Namespace="System.Web"  %>
<%@ Import Namespace="System.Web.Security"  %>
<%@ Import Namespace="System.Web.UI"  %>
<%@ Import Namespace="System.Web.UI.HtmlControls"  %>
<%@ Import Namespace="System.Web.UI.WebControls"  %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts"  %>
<%@ Import Namespace="System.Xml.Linq"  %>
<%@ Import Namespace="System.IO"  %>
<%@ Import Namespace="BLL"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Css/Admin/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server" action="Create_Question_Papers.aspx" method="post" enctype="multipart/form-data"  >
  <div class="container">
  <div class="header"><a href="#"><img src="../images/Ascent logo.png" alt="Insert Logo Here" name="Insert_logo" width="180" height="90" id="Insert_logo" style="background: #C6D580; display:block;" /></a> 
    <!-- end .header --></div>
  <div  class="Menu">
         <ul>
          <li><a href="#">Home</a></li>
          <li><a href="#">Free Demo</a></li>
          <li><a href="#">Register</a></li>
          <li><a href="LogOut.aspx" >Log Out</a></li>
          <li><a href="#">About Us</a></li>
         </ul>
         <!--End Of The Div Menu-->
         </div>
  <div class="sidebar1">
    <ul class="nav">
      <li><a href="CreateCourses.aspx" >Create Course</a></li>
      <li><a href="CreateSubjects.aspx" >Create Subject</a></li>
      <li><a href="#">Import Questions</a></li>
      <li><a href="Inbox.aspx" >Inbox</a></li>
    </ul>

    <!-- end .sidebar1 --></div>
  <div class="content">
   <table class="box-table-a">
		<caption>Files Present</caption>
		<thead>
			<tr>
				<th scope="col">File Name</th>
				<th scope="col">Course Name</th>
				<th scope="col">Subject Name</th>
				<th scope="col">Creation Time</th>
				<th scope="col">Action</th>
				<th scope="col"></th>
			</tr>
		</thead>
		<tbody>
	   <%
	        
           Class_BLL obj0 = new Class_BLL();
           DataTable dt = obj0.GET_Files();
           foreach (DataRow dr in dt.Rows)
           {
               Response.Write("<tr>");
               Response.Write("<td>" + dr["FileName"] + "</td>");
               Response.Write("<td>" + dr["CourseName"] + "</td>");
               Response.Write("<td>" + dr["SubjectName"] + "</td>");
               Response.Write("<td>" + dr["CreationTime"] + "</td>");
               Response.Write("<td><a href='del_files.aspx?file_name="+ dr["filename"]+"&file_id="+ dr["fileid"]+"'>del</a> | <a href='#'>edit</a> | </td>");
               Response.Write("<td><a href='pdf_page.aspx?filename="+ dr["FileName"]+"'>View</a></td>");
               Response.Write("</tr>");
           }
	    
	             
	   %>
	   </tbody>
	</table>
    <table class="box-table-a">
		<caption>Upload Files</caption>
		<thead>
			<tr>
				<th scope="col">Course Name</th>
				<th scope="col">Subject Name</th>
				<th scope="col">Select File</th>
				<th scope="col">Price</th>
				<th scope="col">Actions</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td>
				<select name="course_select">
				<%
                    Class_BLL obj1 = new Class_BLL();
                    DataTable dt1 = obj1.GET_Courses();
                    foreach (DataRow dr in dt1.Rows)
                    {
                        Response.Write("<option value='"+ dr["course_id"]+"'>"+ dr["course_name"]+"</option>");
                    }
	            %>
			    </select>
				</td> 
				<td>
				<select name="Subject_Select">
				<%
                    Class_BLL obj2 = new Class_BLL();
                    DataTable dt2 = obj2.GET_Subjets();
                    foreach (DataRow dr in dt2.Rows)
                    {
                        Response.Write("<option value='" + dr["subject_id"] + "'>" + dr["subject_name"] + "</option>");
                    }		    	
			    %>
				</select>
				</td>
				<td>
				<input type="file" name="file_uploader" />
				</td>
				<td>
				<input type="text" name="txt_price" />
				</td>
				<td><button type="submit" name="btn_submit">Upload</button></td>
			</tr>
		</tbody>
	</table>
    <!-- end .content --></div>
  <div class="footer">

    <!-- end .footer --></div>
  <!-- end .container --></div>
    </form>
</body>
<%
   
    if (Request["btn_submit"] != null)
    {
        int CourseCode = Convert.ToInt32(Request["course_select"]);
        int SubjectCode = Convert.ToInt32(Request["Subject_Select"]);
        Double price = Convert.ToDouble(Request["txt_price"]);
        
        HttpPostedFile file = Request.Files["file_uploader"];
        String file_path=Path.GetFullPath("Files/");
        String file_name = file.FileName;
        try
        {
            file.SaveAs(file_path + file_name);
            Class_BLL obj3 = new Class_BLL();
            obj3.FileMaster_Insert(CourseCode, SubjectCode, file_name, price);
            Response.Redirect("Create_Question_Papers.aspx");
        }
        catch (Exception ex) { }
     }
%>

</html>
