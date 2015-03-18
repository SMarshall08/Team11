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
        <asp:Button id="addFacility" Text="Add Facility" runat="server" />
        <br />
        <asp:DropDownList id="facilityList" runat="server"> 
            <%--Facilities will be taken from db and placed into this tag. --%>
        </asp:DropDownList>
        <asp:Button id="deleteFacility" Text="Delete Facility" runat="server" />
        <br />
        <asp:DropDownList id="poolDropDownList" runat="server"> 
            
        </asp:DropDownList><asp:Button id="removePoolRoom" Text="Remove Pool Room" runat="server" /><br />
        <asp:DropDownList id="roomDropDownList" runat="server"> 
            
        </asp:DropDownList> <asp:Button id="addPoolRoom" Text="Add Pool Room" runat="server" />
        
        
    </div>
    <div id="scriptDiv" runat="server"> <%--This div is where script tags will be placed. --%>

    </div>






</asp:Content>
