<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Team11.Manage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="Styles/Manage.css" />

        <script type="text/javascript" language="javascript">
            $(document).ready(function () {
                // for each radio and checkbox determine if it is checked and if it is then add the "btn" and "btn-danger" classes to the parent
                //Scott Marshall
                $.each([
                      "#MainContent_RadioButtonListView_0",
                      "#MainContent_RadioButtonListView_1",
                      "#MainContent_RadioButtonListPark_0",
                      "#MainContent_RadioButtonListPark_1",
                      "#MainContent_RadioButtonListPark_2",
                      "#MainContent_CheckBoxM1",
                      "#MainContent_CheckBoxM2",
                      "#MainContent_CheckBoxM3",
                      "#MainContent_CheckBoxM4",
                      "#MainContent_CheckBoxM5",
                      "#MainContent_CheckBoxM6",
                      "#MainContent_CheckBoxM7",
                      "#MainContent_CheckBoxM8",
                      "#MainContent_CheckBoxM9",
                      "#MainContent_CheckBoxT1",
                      "#MainContent_CheckBoxT2",
                      "#MainContent_CheckBoxT3",
                      "#MainContent_CheckBoxT4",
                      "#MainContent_CheckBoxT5",
                      "#MainContent_CheckBoxT6",
                      "#MainContent_CheckBoxT7",
                      "#MainContent_CheckBoxT8",
                      "#MainContent_CheckBoxT9",
                      "#MainContent_CheckBoxW1",
                      "#MainContent_CheckBoxW2",
                      "#MainContent_CheckBoxW3",
                      "#MainContent_CheckBoxW4",
                      "#MainContent_CheckBoxW5",
                      "#MainContent_CheckBoxW6",
                      "#MainContent_CheckBoxW7",
                      "#MainContent_CheckBoxW8",
                      "#MainContent_CheckBoxW9",
                      "#MainContent_CheckBoxJ1",
                      "#MainContent_CheckBoxJ2",
                      "#MainContent_CheckBoxJ3",
                      "#MainContent_CheckBoxJ4",
                      "#MainContent_CheckBoxJ5",
                      "#MainContent_CheckBoxJ6",
                      "#MainContent_CheckBoxJ7",
                      "#MainContent_CheckBoxJ8",
                      "#MainContent_CheckBoxJ9",
                      "#MainContent_CheckBoxF1",
                      "#MainContent_CheckBoxF2",
                      "#MainContent_CheckBoxF3",
                      "#MainContent_CheckBoxF4",
                      "#MainContent_CheckBoxF5",
                      "#MainContent_CheckBoxF6",
                      "#MainContent_CheckBoxF7",
                      "#MainContent_CheckBoxF8",
                      "#MainContent_CheckBoxF9",
                      "#MainContent_RadioButtonList1_0",
                      "#MainContent_RadioButtonList1_1",
                      "#MainContent_RadioButtonList1_2",
                      "#MainContent_CheckBoxM10",
                      "#MainContent_CheckBoxM11",
                      "#MainContent_CheckBoxM12",
                      "#MainContent_CheckBoxM13",
                      "#MainContent_CheckBoxM14",
                      "#MainContent_CheckBoxM15",
                      "#MainContent_CheckBoxM16",
                      "#MainContent_CheckBoxM17",
                      "#MainContent_CheckBoxM18",
                      "#MainContent_CheckBoxT10",
                      "#MainContent_CheckBoxT11",
                      "#MainContent_CheckBoxT12",
                      "#MainContent_CheckBoxT13",
                      "#MainContent_CheckBoxT14",
                      "#MainContent_CheckBoxT15",
                      "#MainContent_CheckBoxT16",
                      "#MainContent_CheckBoxT17",
                      "#MainContent_CheckBoxT18",
                      "#MainContent_CheckBoxW10",
                      "#MainContent_CheckBoxW11",
                      "#MainContent_CheckBoxW12",
                      "#MainContent_CheckBoxW13",
                      "#MainContent_CheckBoxW14",
                      "#MainContent_CheckBoxW15",
                      "#MainContent_CheckBoxW16",
                      "#MainContent_CheckBoxW17",
                      "#MainContent_CheckBoxW18",
                      "#MainContent_CheckBoxJ10",
                      "#MainContent_CheckBoxJ11",
                      "#MainContent_CheckBoxJ12",
                      "#MainContent_CheckBoxJ13",
                      "#MainContent_CheckBoxJ14",
                      "#MainContent_CheckBoxJ15",
                      "#MainContent_CheckBoxJ16",
                      "#MainContent_CheckBoxJ17",
                      "#MainContent_CheckBoxJ18",
                      "#MainContent_CheckBoxF10",
                      "#MainContent_CheckBoxF11",
                      "#MainContent_CheckBoxF11",
                      "#MainContent_CheckBoxF12",
                      "#MainContent_CheckBoxF13",
                      "#MainContent_CheckBoxF14",
                      "#MainContent_CheckBoxF15",
                      "#MainContent_CheckBoxF16",
                      "#MainContent_CheckBoxF17",
                      "#MainContent_CheckBoxF18",
                ],
                    function (index, controlAva) {
                        if ($(controlAva).is(":checked"))
                            $(controlAva).parent().addClass("btn btn-danger");
                    }
                    );


            });
    </script>
</asp:Content>

<%-- Page Title Content --%>
<asp:Content ID="TitlesContent" runat="server" ContentPlaceHolderID="TitleContent">
    <h1>Manage</h1>
</asp:Content>


<%-- Body Content --%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="canister none">
        <div class="canistertitle none">
            <h2>Manage as Depratment / Central</h2>
        </div>
        <div class="canistercontainer none">
            <div class="row">
                <div class="text-center col-md-12 col-sm-12 none">
                    <asp:RadioButtonList class="center none" ID="RadioButtonListView" runat="server" AutoPostBack="true" onselectedindexchanged="RadioButtonListView_SelectedIndexChanged" RepeatDirection="Horizontal">
                        <asp:ListItem class="none btn btn-primary">Depratment</asp:ListItem>
                        <asp:ListItem class="none btn btn-primary">Central</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div><!-- ./row -->
    </div><!-- ./canister -->


    <!-- BY Depratment --><!-- BY Depratment --><!-- BY Depratment -->
    <!-- BY Depratment --><!-- BY Depratment --><!-- BY Depratment -->
    <!-- BY Depratment --><!-- BY Depratment --><!-- BY Depratment -->
    <div id="divByDepartment" runat="server" visible="false">
        <div class="canister">


            <div class="canistertitle">
                <h2>Add/Remove Private Room</h2>
            </div>

            <div class="canistercontainer">
                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Available Rooms</h3>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Private Rooms</h3>
                    </div>
                </div><!-- ./row -->


                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:DropDownList class="form-control" ID="DropDownListRooms" runat="server" AutoPostBack="true" >
                        </asp:DropDownList>
                        <br /> 
                        <asp:CheckBoxList class="center" ID="MakePrivate" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListMakePrivate_SelectedIndexChanged">
                            <asp:ListItem class="btn btn-primary">Make Room Private</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:DropDownList class="form-control" ID="DropDownListPrivateRooms" runat="server" AutoPostBack="true" >
                        </asp:DropDownList>
                        <br />
                        <asp:CheckBoxList class="center" ID="RemovePrivate" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListRemovePrivate_SelectedIndexChanged">
                            <asp:ListItem class="btn btn-primary">Remove Room from Private</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div><!-- ./row -->

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        
                    </div>
                </div><!-- ./row -->

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        
                    </div>
                </div><!-- ./row -->

            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->
    </div><!-- ./BY Depratment -->


    <!-- BY Central --><!-- BY Central --><!-- BY Central -->
    <!-- BY Central --><!-- BY Central --><!-- BY Central -->
    <!-- BY Central --><!-- BY Central --><!-- BY Central -->
    <!-- BY Central --><!-- BY Central --><!-- BY Central -->

    <!--Add/Delete Facility-->
    <!--Add/Delete Facility-->
    <!--Add/Delete Facility-->
    <div id="divByCentralFacility" runat="server" visible="false">
        
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Add/Delete Facility</h2>
            </div>
            <div class="canistercontainer">

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Enter Facility to Add</h3>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Choose Facility to Delete</h3>
                    </div>
                </div><!-- ./row -->

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        
                        <%--<asp:TextBox id="facilityText" runat="server" />--%>
                        <asp:TextBox class="form-control" ID="facilityText" runat="server" AutoPostBack="True" Text="" ></asp:TextBox>
                        <asp:CheckBoxList class="center" ID="addFacility" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListaddFacility_SelectedIndexChanged">
                            <asp:ListItem class="btn btn-primary">Add Facility</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:DropDownList class="form-control" ID="facilityList" runat="server" AutoPostBack="true">
                            <%--Facilities will be taken from db and placed into this tag. --%>
                        </asp:DropDownList>
                        <asp:CheckBoxList class="center" ID="deleteFacility" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListdeleteFacility_SelectedIndexChanged">
                            <asp:ListItem class="btn btn-primary">Delete Facility</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div><!-- ./row -->

            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->        
    </div><!-- ./ByCentralFacility -->


    <!--Add/Delete Pool Rooms-->
    <!--Add/Delete Pool Rooms-->
    <!--Add/Delete Pool Rooms-->
    <div id="divByCentralPoolRoom" runat="server" visible="false">
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Add/Delete Pool Rooms</h2>
            </div>
            <div class="canistercontainer">

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Choose Room to Delete</h3>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Choose Room to Add</h3>
                    </div>
                </div><!-- ./row -->
                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:TextBox class="form-control" ID="filterPool" runat="server" AutoPostBack="True" Text="" placeholder="Type to filter list..." onkeyup = "FilterItems(this.value)"></asp:TextBox>
                        <asp:DropDownList class="form-control" ID="poolDropDownList" runat="server" AutoPostBack="true">
                            <%--Pool rooms will be taken from db and placed into this tag. --%>
                        </asp:DropDownList>
                        <asp:CheckBoxList class="center" ID="removePoolRoom" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" ><%--onselectedindexchanged="CheckBoxListdeleteFacility_SelectedIndexChanged"--%>
                            <asp:ListItem class="btn btn-primary">Delete Pool Room</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        
                        <%--<asp:TextBox id="facilityText" runat="server" />--%>
                        
                    </div>
                </div><!-- ./row -->


            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->        
    </div><!-- ./ByCentralPoolRoom -->

    <div id="scriptDiv" runat="server"> 
        <%--This div is where script tags will be placed. --%>
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
        }

        //filter the pool rooms
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

        window.onload = CacheItems;
    </script>
</asp:Content>