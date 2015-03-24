<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="lol.aspx.cs" Inherits="Team11.Facilities" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

   <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AdminConnectionString %>" 
        SelectCommand="SELECT * FROM [User] WHERE ([userID] = @userID)"></asp:SqlDataSource>


    
    <div>
        <h3 align="center">
        Edit Facilities
        </h3>
    

        <asp:Label ID="labelID" runat="server"></asp:Label>
        
        <a href="javascript: history.go(-1)">return to the admin menu.</a>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="labelID2" runat="server"></asp:Label>
    </div>
    


    </asp:Content>
