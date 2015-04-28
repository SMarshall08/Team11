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


        <%--Delete Pool Room --%>
       <asp:TextBox ID="filterPool" runat="server" placeholder="Type to filter list."
         onkeyup = "FilterItems(this.value)"></asp:TextBox> 
        <asp:DropDownList id="poolDropDownList" runat="server"> 
            
        </asp:DropDownList>
        <asp:Button id="removePoolRoom" Text="Remove Pool Room" runat="server" />
        <br />



         <%--Add Pool Room --%>
        <asp:TextBox ID="filterRoom" runat="server" placeholder="Type to filter list."
         onkeyup = "FilterItems2(this.value)"></asp:TextBox>  
        <asp:DropDownList id="roomDropDownList" runat="server"> 
            
        </asp:DropDownList> 
        <asp:Button id="addPoolRoom" Text="Add Pool Room" runat="server" /><br />




        <%--Edit Room Facilities --%>
        <asp:TextBox ID="filterEditFacilities" runat="server" placeholder="Type to filter list."
         onkeyup = "FilterItems3(this.value)"></asp:TextBox>  
        <asp:DropDownList id="editFacilitiesList" runat="server"> 
            
        </asp:DropDownList> 
        
        <asp:Button id="editFacilities" Text="Edit the facilities for this room" runat="server" /><br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="acceptRequests" Text="Click here to view and respond to booking requests." runat="server" />
        
        
    </div>
    <div id="scriptDiv" runat="server"> <%--This div is where script tags will be placed. --%>

    </div>
    
    <script type = "text/javascript">//This script tag is for filtering drop down lists for both pool rooms and non-pool rooms.
        var ddlText, ddlValue, ddl, ddlText2, ddlValue2, ddl2, ddlText3, ddlValue3, ddl3;
    function CacheItems() {
        ddlText = new Array();
        ddlValue = new Array();
        ddl = document.getElementById("<%=poolDropDownList.ClientID %>");
        
        for (var i = 0; i < ddl.options.length; i++) {
            ddlText[ddlText.length] = ddl.options[i].text;
            ddlValue[ddlValue.length] = ddl.options[i].value;
        }
        ddlText2 = new Array();
        ddlValue2 = new Array();
        ddl2 = document.getElementById("<%=roomDropDownList.ClientID %>");

        for (var i = 0; i < ddl2.options.length; i++) {
            ddlText2[ddlText2.length] = ddl2.options[i].text;
            ddlValue2[ddlValue2.length] = ddl2.options[i].value;
        }
        ddlText3 = new Array();
        ddlValue3 = new Array();
        ddl3 = document.getElementById("<%=editFacilitiesList.ClientID %>");

        for (var i = 0; i < ddl3.options.length; i++) {
            ddlText3[ddlText3.length] = ddl3.options[i].text;
            ddlValue3[ddlValue3.length] = ddl3.options[i].value;
        }

    }
        
   
    function FilterItems(value) {
        ddl.options.length = 0;
        for (var i = 0; i < ddlText.length; i++) {
            if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                AddItem(ddlText[i], ddlValue[i]);
            }
        }
        
    }
   
    function AddItem(text, value) {
        var opt = document.createElement("option");
        opt.text = text;
        opt.value = value;
        ddl.options.add(opt);
    }

    
   
    

    function FilterItems2(value) {
        ddl2.options.length = 0;
        for (var i = 0; i < ddlText2.length; i++) {
            if (ddlText2[i].toLowerCase().indexOf(value) != -1) {
                AddItem2(ddlText2[i], ddlValue2[i]);
            }
        }

    }

    function AddItem2(text, value) {
        var opt2 = document.createElement("option");
        opt2.text = text;
        opt2.value = value;
        ddl2.options.add(opt2);
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
