<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="lol.aspx.cs" Inherits="Team11.Facilities" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

   <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AdminConnectionString %>" 
        SelectCommand="SELECT * FROM [User] WHERE ([userID] = @userID)"></asp:SqlDataSource>


    
    <di>
        <h3 align="center">
        Edit Facilities
        </h3>
    

        <asp:Label ID="labelID" runat="server"></asp:Label>
        <a href="javascript: history.go(-1)">return to the admin menu.</a>
    </div>
    


    </asp:Content>
