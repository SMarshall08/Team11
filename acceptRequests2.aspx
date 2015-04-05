<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="acceptRequests.aspx.cs" Inherits="Team11.acceptRequests2" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">



    
    <asp:Label id="referenceLabel" runat="server" /><br /><br /><br />
    <div id="tableDiv" runat="server">
    
    </div>

    <br /><br /><br /><br /><br />
    <asp:Button ID="acceptRequestedRoom" Text="Accept request" runat="server" />
    <asp:Button ID="changeRequestedRoom" Text="Accept request but assign a different room" runat="server" />
    <asp:Button ID="rejectRequestedRoom" Text="Reject request" runat="server" />

    
    </asp:Content>
