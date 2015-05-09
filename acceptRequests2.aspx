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
        <asp:TextBox ID="filterRooms" runat="server" placeholder="Type to filter list (room one)."
         onkeyup = "FilterItems3(this.value)"></asp:TextBox>  <asp:DropDownList id="listOfRooms" runat="server"> 
            
        </asp:DropDownList>

        <asp:TextBox ID="filterRooms2" runat="server" placeholder="Type to filter list (room two)."
         onkeyup = "FilterItems4(this.value)"></asp:TextBox>  <asp:DropDownList id="listOfRooms2" runat="server"> 
            
        </asp:DropDownList>

        <asp:TextBox ID="filterRooms3" runat="server" placeholder="Type to filter list (room three)."
         onkeyup = "FilterItems5(this.value)"></asp:TextBox>  <asp:DropDownList id="listOfRooms3" runat="server"> 
            
        </asp:DropDownList>

        <asp:TextBox ID="filterRooms4" runat="server" placeholder="Type to filter list (room four)."
         onkeyup = "FilterItems6(this.value)"></asp:TextBox>  <asp:DropDownList id="listOfRooms4" runat="server"> 
            
        </asp:DropDownList>
    </div>
    <script type = "text/javascript">//This script tag is for filtering drop down lists for both pool rooms and non-pool rooms.
        var ddlText3, ddlValue3, ddl3, ddlText4, ddlValue4, ddl4, ddlText5, ddlValue5, ddl5, ddlText6, ddlValue6, ddl6;
        function CacheItems() {

            ddlText3 = new Array();
            ddlValue3 = new Array();
            ddl3 = document.getElementById("<%=listOfRooms.ClientID %>");
            ddlText4 = new Array();
            ddlValue4 = new Array();
            ddl4 = document.getElementById("<%=listOfRooms2.ClientID %>");
            ddlText5 = new Array();
            ddlValue5 = new Array();
            ddl5 = document.getElementById("<%=listOfRooms3.ClientID %>");
            ddlText6 = new Array();
            ddlValue6 = new Array();
            ddl6 = document.getElementById("<%=listOfRooms4.ClientID %>");
        for (var i = 0; i < ddl3.options.length; i++) {
            ddlText3[ddlText3.length] = ddl3.options[i].text;
            ddlValue3[ddlValue3.length] = ddl3.options[i].value;
        }
        for (var i = 0; i < ddl4.options.length; i++) {
            ddlText4[ddlText4.length] = ddl4.options[i].text;
            ddlValue4[ddlValue4.length] = ddl4.options[i].value;
        }
        for (var i = 0; i < ddl5.options.length; i++) {
            ddlText5[ddlText5.length] = ddl5.options[i].text;
            ddlValue5[ddlValue5.length] = ddl5.options[i].value;
        }
        for (var i = 0; i < ddl6.options.length; i++) {
            ddlText6[ddlText3.length] = ddl6.options[i].text;
            ddlValue6[ddlValue3.length] = ddl6.options[i].value;
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

    function FilterItems4(value) {
        ddl4.options.length = 0;
        for (var i = 0; i < ddlText4.length; i++) {
            if (ddlText4[i].toLowerCase().indexOf(value) != -1) {
                AddItem4(ddlText4[i], ddlValue4[i]);
            }
        }

    }

    function AddItem4(text, value) {
        var opt4 = document.createElement("option");
        opt4.text = text;
        opt4.value = value;
        ddl4.options.add(opt4);
    }

    function FilterItems5(value) {
        ddl5.options.length = 0;
        for (var i = 0; i < ddlText5.length; i++) {
            if (ddlText5[i].toLowerCase().indexOf(value) != -1) {
                AddItem5(ddlText5[i], ddlValue5[i]);
            }
        }

    }

    function AddItem5(text, value) {
        var opt5 = document.createElement("option");
        opt5.text = text;
        opt5.value = value;
        ddl5.options.add(opt5);
    }
    window.onload = CacheItems;
</script>
    </asp:Content>
