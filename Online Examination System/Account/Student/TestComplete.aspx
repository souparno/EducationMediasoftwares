<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="TestComplete.aspx.cs" Inherits="Account_Admin_TestComplete" %>
<%@ Register Src="~/Account/Student/UserControl/Navigation.ascx" TagPrefix="uc" TagName="Navigation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideBar1" Runat="Server">
<uc:Navigation runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" Runat="Server">
  <h1>TEST COMPLETED SUCCESSFULLY</h1>
</asp:Content>

