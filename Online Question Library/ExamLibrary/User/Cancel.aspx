<%@ Page Language="C#" AutoEventWireup="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
<%@ Import Namespace="BLL"%>

<%  
    
    
    if (Request.QueryString["type"].Equals("CourseCart"))
    {
        DataTable Course_Cart = (DataTable)Session["Courses"];
        int CourseId = Convert.ToInt32(Request.QueryString["CourseId"]);
        Class_BLL obj = new Class_BLL();
        Session["Courses"] = obj.cancel_course_cart(CourseId, Course_Cart);
    }
    else if (Request.QueryString["type"].Equals("SubjectCart"))
    {
        DataTable Subject_cart = (DataTable)Session["Subjects"];
        int SubjectId = Convert.ToInt32(Request.QueryString["SubjectId"]);
        Class_BLL obj = new Class_BLL();
        Session["Subjects"] = obj.cancel_subject_cart(SubjectId, Subject_cart);
    }
    Response.Redirect("cart.aspx"); 
    
%>


