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
    <style type="text/css">
        .auto-style2 {
            height: 24px;
            margin-bottom: 1px;
        }
    </style>
</asp:Content>

<%-- Page Title Content --%>
<asp:Content ID="TitlesContent" runat="server" ContentPlaceHolderID="TitleContent">
    <h1>Manage</h1>
</asp:Content>


<%-- Body Content --%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="canister none">
        <div class="canistertitle none">
            <h2>Manage as Department / Central</h2>
        </div>
        <div class="canistercontainer none">
            <div class="row">
                <div class="text-center col-md-12 col-sm-12 none">
                    <asp:RadioButtonList class="center none" ID="RadioButtonListView" runat="server" AutoPostBack="true" onselectedindexchanged="RadioButtonListView_SelectedIndexChanged" RepeatDirection="Horizontal">
                        <asp:ListItem class="none btn btn-primary">Department</asp:ListItem>
                        <asp:ListItem class="none btn btn-primary">Central</asp:ListItem>
                        
                    </asp:RadioButtonList>
                    <br />
                        <asp:Label id="isUserAdminLabel" runat="server"></asp:Label>
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
    
       <div id="divByAddLecturer" runat="server" visible="false">
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Add Staff</h2>
            </div>
            <div class="canistercontainer">

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Choose Staff to Add to Department</h3>
                    </div>
                </div>

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">

                        <asp:DropDownList class="form-control" ID="DropDownListChooseStaffDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListChooseStaffDept_SelectedIndexChanged"></asp:DropDownList>
                        <!--<asp:DropDownList class="form-control" ID="DropDownListChooseStaffModule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListChooseStaffModule_SelectedIndexChanged"></asp:DropDownList>-->
                        <asp:TextBox class="form-control" ID="TextBoxFirstName" runat="server" AutoPostBack="True" Text="" placeholder="Enter First Name..." OnTextChanged="TextBoxFirstName_TextChanged"></asp:TextBox>
                        <asp:TextBox class="form-control" ID="TextBoxLastName" runat="server" AutoPostBack="True" Text="" placeholder="Enter First Name..." OnTextChanged="TextBoxLastName_TextChanged"></asp:TextBox>



                        <asp:CheckBoxList class="center" ID="CheckBoxListAddNewStaff" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListAddNewStaff_SelectedIndexChanged">
                            <asp:ListItem class="btn btn-primary">Add Staff</asp:ListItem>
                        </asp:CheckBoxList>

                        
                    </div>
                 </div>

            </div>
        </div>
               
    </div>


    <div id="divByCentralModuleStaff" runat="server" visible="false">
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Add/Delete Staff from Modules</h2>
            </div>
            <div class="canistercontainer">

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Choose Staff to Add to Module</h3>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Choose Staff to Delete from Module</h3>
                    </div>
                </div>

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:DropDownList class="form-control" ID="DropDownListFilterAddDeptStaff" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterAddDeptStaff_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList class="form-control" ID="DropDownListFilterAddStaffDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterAddStaffDept_SelectedIndexChanged"></asp:DropDownList>     
                        <asp:DropDownList class="form-control" ID="DropDownListFilterAddModuleStaff" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterAddModuleStaff_SelectedIndexChanged"></asp:DropDownList>



                        <asp:CheckBoxList class="center" ID="CheckBoxListAddStaff" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListAddStaff_SelectedIndexChanged">
                            <asp:ListItem class="btn btn-primary">Add Staff</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:DropDownList class="form-control" ID="DropDownListFilterDeleteDeptStaff" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterDeleteDeptStaff_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList class="form-control" ID="DropDownListFilterDeleteStaffDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterDeleteStaffDept_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList class="form-control" ID="DropDownListFilterDeleteModuleStaff" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterDeleteModuleStaff_SelectedIndexChanged"></asp:DropDownList>


                        <asp:CheckBoxList class="center" ID="CheckBoxListDeleteStaff" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListDeleteStaff_SelectedIndexChanged" CssClass="auto-style2">
                            <asp:ListItem class="btn btn-primary">Delete Staff</asp:ListItem>
                        </asp:CheckBoxList>
                        <asp:TextBox ID="StaffError" align="left" runat="server" OnTextChanged="StaffError_TextChanged"></asp:TextBox>
                    </div>
                </div>

            </div>
        </div>
               
    </div>
    
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
                        
                        <%-- Page Title Content --%>
                        <asp:TextBox class="form-control" ID="facilityText" runat="server" AutoPostBack="True" Text="" placeholder="Enter Facility..."></asp:TextBox>
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
                <h2>Add/Remove Pool Rooms</h2>
            </div>
            <div class="canistercontainer">

                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Choose Room to Add</h3>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Choose Room to Remove</h3>
                    </div>
                </div><!-- ./row -->
                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:TextBox class="form-control" ID="filterRoom" runat="server" AutoPostBack="False" Text="" placeholder="Type to filter list..." onkeyup = "FilterItems2(this.value)" ></asp:TextBox>
                        <asp:DropDownList class="form-control" ID="roomDropDownList" runat="server" AutoPostBack="true">
                            <%--Non-Pool rooms will be taken from db and placed into this tag. --%>
                        </asp:DropDownList>
                        <asp:CheckBoxList class="center" ID="addPoolRoom" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListaddPoolRoom_SelectedIndexChanged" >
                            <asp:ListItem class="btn btn-primary">Add Pool Room</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:TextBox class="form-control" ID="filterPool" runat="server" AutoPostBack="False" Text="" placeholder="Type to filter list..." onkeyup = "FilterItems(this.value)"></asp:TextBox>
                        <asp:DropDownList class="form-control" ID="poolDropDownList" runat="server" AutoPostBack="True">
                            <%--Pool rooms will be taken from db and placed into this tag. --%>
                        </asp:DropDownList>
                        <asp:CheckBoxList class="center" ID="removePoolRoom" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListremovePoolRoom_SelectedIndexChanged" >
                            <asp:ListItem class="btn btn-primary">Remove Pool Room</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div><!-- ./row -->


            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->        
    </div><!-- ./ByCentralPoolRoom -->

    <!--Edit Room Facilitys-->
    <!--Edit Room Facilitys-->
    <!--Edit Room Facilitys-->
    <div id="divByCentralEditRoom" runat="server" visible="false">
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Edit Room Facility</h2>
            </div>
            <div class="canistercontainer">

                <div class="row">
                    <div class="text-center center col-md-3 col-sm-3">
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <h3>Choose Room to Edit</h3>
                    </div>
                </div><!-- ./row -->

                <div class="row">
                    <div class="text-center center col-md-3 col-sm-3">
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:TextBox class="form-control" ID="filterEditFacilities" runat="server" AutoPostBack="False" Text="" placeholder="Type to filter list..."  onkeyup = "FilterItems3(this.value)"></asp:TextBox>
                        <asp:DropDownList class="form-control" ID="editFacilitiesList" runat="server" AutoPostBack="true">
                            <%--Non-Pool rooms will be taken from db and placed into this tag. --%>
                        </asp:DropDownList>
                        <asp:CheckBoxList class="center" ID="editFacilities" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListeditFacilities_SelectedIndexChanged" >
                            <asp:ListItem class="btn btn-primary">Edit Facility</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div class="text-center center col-md-3 col-sm-3">
                    </div>
                </div><!-- ./row -->
                

            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->        
    </div><!-- ./ByCentralPoolRoom -->

    <!--Respond to Booking Requests-->
    <!--Respond to Booking Requests-->
    <!--Respond to Booking Requests-->
    <div id="divByCentralRespond" runat="server" visible="false">
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Respond to Booking Requests</h2>
            </div>
            <div class="canistercontainer">

                <div class="row">
                    <div class="text-center center col-md-3 col-sm-3">
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:CheckBoxList class="center" ID="respondButton" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" onselectedindexchanged="CheckBoxListRespond_SelectedIndexChanged" >
                            <asp:ListItem class="btn btn-primary">Click here to respond to requests</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div><!-- ./row -->

                

            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->        
    </div><!-- ./ByCentralPoolRoom -->
    <div id="divByCentralRounds" runat="server" visible="false">
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Rounds<asp:Label ID="roundsLabel" runat="server"></asp:Label></h2>
            </div>
            <div class="canistercontainer">

                <div class="row">
                    <div class="text-center center col-md-3 col-sm-3">
                        
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        
                        <asp:DropDownList id="Rounds"
                     runat="server">

                  <asp:ListItem Selected="True" Value="1"> 1 </asp:ListItem>
                  <asp:ListItem Value="2"> 2 </asp:ListItem>
                  <asp:ListItem Value="3"> 3 </asp:ListItem>
                  <asp:ListItem Value="4"> 4 </asp:ListItem>
                  <asp:ListItem Value="5"> 5 </asp:ListItem>

               </asp:DropDownList>
                        <asp:Button id="roundButton" Text="Manually change round" runat="server" />
                       <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                        <asp:Calendar id="CalendarRound" runat="server" AutoPostback="false" ></asp:Calendar> 
                               </ContentTemplate> 
                                </asp:UpdatePanel>                      
                        
                        <br /><asp:Button id="roundDateButton" Text="Set a date for the round to progress" runat="server" />
                       
                    </div>
                </div><!-- ./row -->

                <div>

                </div>

            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->        
    </div><!-- ./ByCentralPoolRoom -->

    <div id="scriptDiv" runat="server"> 
        <%-- Body Content --%>
    </div>
   

    <script type = "text/javascript">//This script tag is for filtering drop down lists for both pool rooms and non-pool rooms.
        var ddlText, ddlValue, ddl, ddlText2, ddlValue2, ddl2, ddlText3, ddlValue3, ddl3;
        function CacheItems() {
            //ddl
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=poolDropDownList.ClientID %>");
            
            for (var i = 0; i < ddl.options.length; i++) {
                ddlText[ddlText.length] = ddl.options[i].text;
                ddlValue[ddlValue.length] = ddl.options[i].value;
            }
            //dd2
            ddlText2 = new Array();
            ddlValue2 = new Array();
            ddl2 = document.getElementById("<%=roomDropDownList.ClientID %>");

            for (var i = 0; i < ddl2.options.length; i++) {
                ddlText2[ddlText2.length] = ddl2.options[i].text;
                ddlValue2[ddlValue2.length] = ddl2.options[i].value;
            }
            //dd3
            ddlText3 = new Array();
            ddlValue3 = new Array();
            ddl3 = document.getElementById("<%=editFacilitiesList.ClientID %>");

            for (var i = 0; i < ddl3.options.length; i++) {
                ddlText3[ddlText3.length] = ddl3.options[i].text;
                ddlValue3[ddlValue3.length] = ddl3.options[i].value;
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

        //filter the non-pool rooms
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
        //filter the edit facilites
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
