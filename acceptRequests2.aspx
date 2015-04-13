<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="acceptRequests.aspx.cs" Inherits="Team11.acceptRequests2" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">



    
    <asp:Label id="referenceLabel" runat="server" /><br /><br /><br />
    <div id="tableDiv" runat="server">
    
    </div>
    
    <div id="scriptDiv" runat="server"></div>
    <div id="buttonDiv" runat="server">
    <br /><br /><br /><br /><br />
    <asp:Button ID="acceptRequestedRoom" Text="Accept request" runat="server" />
    
    <asp:Button ID="rejectRequestedRoom" Text="Reject request" runat="server" /><br /><br />

        <asp:Button ID="changeRequestedRoom" Text="Accept request but assign a different room" runat="server" />
        <asp:TextBox ID="filterRooms" runat="server" placeholder="Type to filter list."
         onkeyup = "FilterItems3(this.value)"></asp:TextBox>  <asp:DropDownList id="listOfRooms" runat="server"> 
            
        </asp:DropDownList>
    </div>
    <script type = "text/javascript">//This script tag is for filtering drop down lists for both pool rooms and non-pool rooms.
        var ddlText3, ddlValue3, ddl3;
        function CacheItems() {

            ddlText3 = new Array();
            ddlValue3 = new Array();
            ddl3 = document.getElementById("<%=listOfRooms.ClientID %>");

        for (var i = 0; i < ddl3.options.length; i++) {
            ddlText3[ddlText3.length] = ddl3.options[i].text;
            ddlValue3[ddlValue3.length] = ddl3.options[i].value;
        }

    }



    function FilterItems3(value) {
        ddl3.options.length = 0;
        for (var i = 0; i < ddlText3.length; i++) {
            if (ddlText3[i].toLowerCase().indexOf(value) != -1) {
                AddItem3(ddlText3[i], ddlValue3[i]);
            }
        }

    }

    function AddItem3(text, value) {
        var opt3 = document.createElement("option");
        opt3.text = text;
        opt3.value = value;
        ddl3.options.add(opt3);
    }
    window.onload = CacheItems;
</script>
    </asp:Content>
