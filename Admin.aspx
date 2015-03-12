<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Team11.Admin" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AdminConnectionString %>" 
        SelectCommand="SELECT * FROM [User] WHERE ([userID] = @userID)"></asp:SqlDataSource>
    <h2 align="center">
        Admin Menu
    </h2>
    
    <div id="areYouAdmin" runat="server"> <%--This div is where buttons, text forms, and other text will be placed. --%>
        <asp:TextBox id="facilityText" runat="server" />
        
    </div>
    <div id="scriptDiv" runat="server"> <%--This div is where script tags will be placed. --%>

    </div>






</asp:Content>
