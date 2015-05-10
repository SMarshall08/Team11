<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateRequest.aspx.cs" Inherits="Team11.CreateRequest" MaintainScrollPositionOnPostback = "true"%>

<%-- Create Request Header Content --%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function ()
        {   // for each radio and checkbox determine if it is checked and if it is then add the "btn" and "btn-danger" classes to the parent
            $.each(["#MainContent_RadioButtonListRoomType_0",
                   "#MainContent_RadioButtonListRoomType_1",
                   "#MainContent_RadioButtonListRoomType_2",
                   "#MainContent_RadioButtonListArrangement_0",
                   "#MainContent_RadioButtonListArrangement_1",
                   "#MainContent_RadioButtonListArrangement_2",
                   "#MainContent_RadioButtonListWheelchair_0",
                   "#MainContent_RadioButtonListWheelchair_1",
                   "#MainContent_CheckBoxWB",
                   "#MainContent_CheckBoxCB",
                   "#MainContent_RadioButtonListProjector_0",
                   "#MainContent_RadioButtonListProjector_1",
                   "#MainContent_RadioButtonListProjector_2",
                   "#MainContent_RadioButtonListVisualiser_0",
                   "#MainContent_RadioButtonListVisualiser_1",
                   "#MainContent_RadioButtonListComputer_0",
                   "#MainContent_RadioButtonListComputer_1",
                   "#MainContent_Week1",
                   "#MainContent_Week2",
                   "#MainContent_Week3",
                   "#MainContent_Week4",
                   "#MainContent_Week5",
                   "#MainContent_Week6",
                   "#MainContent_Week7",
                   "#MainContent_Week8",
                   "#MainContent_Week9",
                   "#MainContent_Week10",
                   "#MainContent_Week11",
                   "#MainContent_Week12",
                   "#MainContent_Week13",
                   "#MainContent_Week14",
                   "#MainContent_Week15",
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
                   "#MainContent_RadioButtonListSemester_1",
                   "#MainContent_RadioButtonListSemester_1",
                                ],
                function(index, controlRef)
                {
                    if ($(controlRef).is(":checked"))
                        $(controlRef).parent().addClass("btn btn-danger");
                }
                );

        });
                
    </script>
    <!-- Create Request CSS -->
    <link rel="Stylesheet" type="text/css" href="Styles/CreateRequest.css" />
  
</asp:Content>

<%-- Page Title Content --%>
<asp:Content ID="TitlesContent" runat="server" ContentPlaceHolderID="TitleContent">
    <h1><asp:Label runat="server" ID="countdownLabel2"></asp:Label></h1>
</asp:Content>

<%-- MAIN BODY CONTENT --%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <!-- Module Details -->
            <div class="canister =">
            
                <div class="canistertitle">
                    <h2>Module Details</h2>
                </div>
                <div class="clearfix"></div>

                <div class="row modulerow">
                    <div class="text-center col-md-3 col-sm-3 department">
                        <h3 style="margin-top:-0px;">Department</h3>
                    </div>
                    <div class="text-center col-md-3 col-sm-3 part">
                        <h3 style="margin-top:0px;">Part</h3>
                    </div>
                    <div class="text-center col-md-3 col-sm-3 module" >
                        <h3 style="margin-top:0px;">Module</h3>
                    </div>
                    <div class="text-center col-md-3 col-sm-3 capacity">
                        <h3 style="margin-top:-0px;">Capacity</h3>
                    </div>
                </div><!-- ./row -->

                <div class="row modulerow">
                    <div class="text-center col-md-3 col-sm-3 department">
                        <label id="deptName" class="form-control" runat="server" style="font-size:12px;"></label>
                    </div>
                    <div class="text-center col-md-3 col-sm-3 part">
                        <asp:DropDownList class="form-control" ID="DropDownListPart" runat="server" OnSelectedIndexChanged="DropDownListPart_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="0">Any</asp:ListItem>
                            <asp:ListItem Value="1">A</asp:ListItem>
                            <asp:ListItem Value="2">B</asp:ListItem>
                            <asp:ListItem Value="3">C</asp:ListItem>
                            <asp:ListItem Value="4">D</asp:ListItem>
                            <asp:ListItem Value="5">P</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="text-center col-md-3 col-sm-3 module">
                        <asp:DropDownList class="form-control" ID="DropDownListModules" runat="server" OnSelectedIndexChanged="DropDownListModules_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    
                    <div class="text-center col-md-3 col-sm-3 capacity">
                        <asp:TextBox class="form-control" ID="TextBoxCapacity" runat="server" AutoPostBack="True" ontextchanged="TextBoxCapacity_TextChanged" ></asp:TextBox>
                    </div>
                </div><!-- ./row -->
            
            </div><!-- ./canister -->
            
            <div class="clearfix"></div>
            
            <!-- Facilities -->
            <div class="canister">
                
                <div class="canistertitle">
                    <h2>Facility Options</h2>
                </div>

                <div class="canistercontainer">
                    
                    <div class="row">
                        <div class="text-center col-md-4 col-sm-4">
                            <h3 style="margin-top:-0px;">Room Type:</h3>
                        </div>
                        <div class="text-center col-md-4 col-sm-4">
                            <h3 style="margin-top:-0px;">Arrangement:</h3>
                        </div>
                        <div class="text-center col-md-4 col-sm-4">
                            <h3 style="margin-top:-0px;">WheelChair Access:</h3>
                        </div>
                    </div><!-- ./row -->
                    
                    

                    <div class="row">

                        <div class="text-center col-md-4 col-sm-4">
                            <asp:RadioButtonList ID="RadioButtonListRoomType" runat="server" AutoPostBack="True" onselectedindexchanged="RadioButtonListRoomType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem class="btn btn-primary moveright">Lecture</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg">Seminar</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg" Selected="True">Either</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="text-center col-md-4 col-sm-4">
                            <asp:RadioButtonList ID="RadioButtonListArrangement" runat="server" AutoPostBack="True" onselectedindexchanged="RadioButtonListArrangement_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem class="btn btn-primary moverightarrange">Tiered</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg">Flat</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg" Selected="True">Either</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="text-center col-md-4 col-sm-4">
                            <asp:RadioButtonList ID="RadioButtonListWheelchair" runat="server" AutoPostBack="True" onselectedindexchanged="RadioButtonListWheelchair_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem class="btn btn-primary moverightarrange">Yes</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg" Selected="True">Either</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div><!-- ./row -->

                    <div class="clearfix"></div>

                    <div class="row">
                        <div class="text-center col-md-4 col-sm-4">
                            <h3 style="margin-top:4px;">Board Type(s)</h3>
                        </div>
                        <div class="text-center col-md-4 col-sm-4">
                            <h3 style="margin-top:4px;">Data Projector(s)</h3>
                        </div>
                        <div class="text-center col-md-4 col-sm-4">
                            <h3 style="margin-top:4px;">Visualiser</h3>
                        </div>
                    </div><!-- ./row -->

                    <div class="clearfix"></div>

                     <div class="row">
                        <div class="text-center col-md-4 col-sm-4">
                            <asp:CheckBox class="btn btn-primary" ID="CheckBoxWB" runat="server" Text="White Board"   autopostback="true"/>
                            <asp:CheckBox class="btn btn-primary leftmarg" ID="CheckBoxCB" runat="server" Text="Chalk Board" autopostback="true" />
                        </div>
                        <div class="text-center col-md-4 col-sm-4">
                            <asp:RadioButtonList ID="RadioButtonListProjector" runat="server" AutoPostBack="True" onselectedindexchanged="RadioButtonListProjector_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem class="btn btn-primary">Data Projector</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg">Double Projector</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg" Selected="True">Either</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="text-center col-md-4 col-sm-4">
                            <asp:RadioButtonList ID="RadioButtonListVisualiser" runat="server" AutoPostBack="True" onselectedindexchanged="RadioButtonListVisualiser_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem class="btn btn-primary moverightarrange">Yes</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg" Selected="True">Either</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div><!-- ./row -->

                    <div class="clearfix"></div>

                    <div class="row">
                        <div class="text-center col-md-4 col-sm-4">
                            <h3 style="margin-top:4px;">Fixed Computer</h3>
                        </div>
                        <div class="text-center col-md-8 col-sm-8">
                            <h3 style="margin-top:4px;">Additional Requirements</h3>
                        </div>
                    </div><!-- ./row -->

                    <div class="clearfix"></div>

                   <div class="row">
                        <div class="text-center col-md-4 col-sm-4">
                            <asp:RadioButtonList ID="RadioButtonListComputer" runat="server" AutoPostBack="True" onselectedindexchanged="RadioButtonListComputer_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem class="btn btn-primary moverightarrange">Yes</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg" Selected="True">Either</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="text-center col-md-8 col-sm-8">
                            <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                        </div>
                    </div><!-- ./row -->
                </div><!-- ./canister container -->
            </div><!-- ./canister -->

            <div class="clearfix"></div>

            <div class="canister">
                <div class="canistertitle">
                    <h2>Room Selection</h2>
                </div>

                <div class="canistercontainer">
                    <div class="row">
                        <div class="text-center col-md-3 col-sm-3">
                            <h3 style="margin-top:-0px;">Park</h3>
                        </div>
                        <div class="text-center col-md-3 col-sm-3">
                            <h3 style="margin-top:-0px;">Building</h3>
                        </div>
                        <div class="text-center col-md-3 col-sm-3">
                            <h3 style="margin-top:-0px;">Room to book</h3>
                        </div>
                        <div class="text-center col-md-3 col-sm-3">
                            <h3 style="margin-top:-0px;">Alternate Rooms</h3>
                        </div>
                    </div><!-- ./row -->

                    <div class="clearfix"></div>
                    
                    
                    
                    <div class="row">
                        <div class="text-center col-md-3 col-sm-3">
                            <asp:RadioButtonList ID="RadioButtonListParks" runat="server" AutoPostBack="True" onselectedindexchanged="RadioButtonListParks_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem class="btn btn-primary">Central</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarglittle">East</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarglittle">West</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="text-center col-md-3 col-sm-3">
                            <asp:DropDownList class="form-control" ID="DropDownListBuildings" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListBuildings_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="text-center col-md-3 col-sm-3">
                            <!-- DROP DOWN FOR ROOM BOOKING -->                    
                           
                            <asp:DropDownList class="form-control" ID="DropDownListRooms" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListRooms_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="text-center col-md-3 col-sm-3">
                            <!-- DROPDOWN FOR ALTERNATE ROOM BOOKING -->
                            <asp:DropDownList class="form-control" ID="DropDownListRoomsAlt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListRoomsAlt_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div><!-- ./row -->
                    
                    <div class="clearfix"></div>

                     <!--<asp:UpdatePanel ID="UpdatePanel100" runat="server">
                                                    <ContentTemplate> -->
                    <div class="row">
                        <div class="text-right col-md-3 col-sm-3 col-md-offset-6 col-sm-offset-6">
                            <!-- Book Room 1 label -->
                            <asp:Label ID="LabelRoom1" runat="server" Text="None"></asp:Label>
                            <!-- Book Room 1 Delete Button-->
                            <asp:Button class="btn btn-success moveleft" ID="ButtonDeleteRoom1" runat="server" Text="Delete" onclick="ButtonDeleteRoom1_Click" />
                                   </br>                     
                            <!-- Book Room 2 label -->
                            <asp:Label ID="LabelRoom2" runat="server" Text="None"></asp:Label>
                            <!-- Book Room 2 Delete Button -->
                            <asp:Button ID="ButtonDeleteRoom2" runat="server" 
                                class="btn btn-success moveleft" onclick="ButtonDeleteRoom2_Click" 
                                Text="Delete" />
                            <br />
                            <!-- Book Room 3 label -->
                            <asp:Label ID="LabelRoom3" runat="server" Text="None"></asp:Label>
                            <!-- Book Room 3 Delete Button-->
                            <asp:Button style="margin-right:24px;" ID="ButtonDeleteRoom3" runat="server" 
                                class="btn btn-success moveleft" onclick="ButtonDeleteRoom3_Click" 
                                Text="Delete" />
                            
                        </div>
                        <div class="text-right col-md-3 col-sm-3">
                            <!-- Alt Room 1 label -->
                            <asp:Label ID="LabelRoomAlt1" runat="server" Text="None"></asp:Label>
                            <!-- Alt Room 1 Delete button -->
                            <asp:Button class="btn btn-success moveleft" ID="ButtonDeleteRoomAlt1" runat="server" Text="Delete" onclick="ButtonDeleteRoomAlt1_Click" />
                            </br>
                            <!-- Alt Room 2 label -->
                            <asp:Label ID="LabelRoomAlt2" runat="server" Text="None"></asp:Label>
                            <!-- Alt Room 2 Delete Button -->
                            <asp:Button ID="ButtonDeleteRoomAlt2" runat="server" 
                                class="btn btn-success moveleft" onclick="ButtonDeleteRoomAlt2_Click" 
                                Text="Delete" />
                            </br>
                            <!-- Alt Room 3 label -->
                            <asp:Label ID="LabelRoomAlt3" runat="server" Text="None"></asp:Label>
                            <!-- Alt Room 3 Delete Button -->
                            <asp:Button style="margin-right:24px;" ID="ButtonDeleteRoomAlt3" runat="server" 
                                class="btn btn-success moveleft" onclick="ButtonDeleteRoomAlt3_Click" 
                                Text="Delete" />
                            
                        </div>
                    </div><!-- ./row -->
                </div><!-- ./canistercontainer -->
            </div><!-- ./canister -->
           <!--</ContentTemplate>
                            </asp:UpdatePanel> -->

            <div class="canister">
                <div class="canistertitle">
                    <h2>Times</h2>
                </div>
                <div class="canistercontainer">
                    <div class="row">
                        <div class="text-center col-md-12 col-sm-12">
                            <div class="text-center col-md-12 col-sm-12">
                            <asp:RadioButtonList CssClass="center" ID="RadioButtonListSemester" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem class="btn btn-primary " Enabled="False">Semester 1</asp:ListItem>
                                <asp:ListItem class="btn btn-primary leftmarg" Selected="True">Semester 2</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        </div>
                    </div><!-- ./row -->
                    
                    <div class="clearfix"></div>

                    <div class="row">
                        
                    </div><!-- ./row -->

                    <div class="clearfix"></div>

                    <div class="row">
                        <div class="text-center col-md-12 col-sm-12">
                            <h3>Week(s)</h3>
                        </div>
                    </div>
                    
                    <div class="clearfix"></div>
             
                    <div class="row">
                        <div class="text-center col-md-12 col-sm-12">
                            <!--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate> -->
                                    <asp:CheckBox class="btn btn-primary" ID="Week1" text="1" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week2" text="2" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week3" text="3" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week4" text="4" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week5" text="5" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week6" text="6" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week7" text="7" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week8" text="8" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week9" text="9" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week10" text="10" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week11" text="11" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week12" text="12" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week13" text="13" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week14" text="14" runat="server" AutoPostBack="true" />
                                    <asp:CheckBox class="btn btn-primary" ID="Week15" text="15" runat="server" AutoPostBack="true" />
                                    
                               <!-- </ContentTemplate>
                            </asp:UpdatePanel> -->
                        </div><!-- ./col -->
                    </div><!-- ./row -->
                    <div class="clearfix"></div>

                    <div class="row">
                        <div class="text-center col-md-12 col-sm-12">
                            <asp:Button class="btn btn-success topmarg" ID="All" runat="server" onclick="All_Click" Text="All" />
                            <asp:Button class="btn btn-success topmarg" ID="Twelve" runat="server" onclick="Twelve_Click" Text="1-12" />
                            <asp:Button class="btn btn-success topmarg" ID="Odd" runat="server" onclick="Odd_Click" Text="Odd" />
                            <asp:Button class="btn btn-success topmarg" ID="Even" runat="server" onclick="Even_Click" Text="Even" />
                            <asp:Button class="btn btn-warning topmarg" ID="Clear" runat="server" onclick="Clear_Click" Text="Clear All" />
                        </div>
                    </div><!-- ./row -->

                    <div class="clearfix"></div>

                    <div class="row">
                        <div class="text-center col-md-12 col-sm-12">
                            <h3>Day(s) and Period(s)</h3>
                        </div>
                    </div><!-- ./row -->

                    <div class="clearfix"></div>

                    <div class="row">
                        <div class="text-center col-md-12 col-sm-12">
                            <!-- Periods and Times Table -->
                            <table class="center">
                                <tr>
                                    <td class="style2">
                                        <asp:Button class="btn btn-period btn-warning" ID="ButtonClearPeriods" runat="server" onclick="ButtonClearPeriods_Click" Text="Clear Periods" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-period btn-success" ID="ButtonPeriod1" runat="server" Text="Period 1" onclick="ButtonPeriod1_Click" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-period btn-success" ID="ButtonPeriod2" runat="server" Text="Period 2" onclick="ButtonPeriod2_Click" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-period btn-success" ID="ButtonPeriod3" runat="server" Text="Period 3" onclick="ButtonPeriod3_Click" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-period btn-success" ID="ButtonPeriod4" runat="server" Text="Period 4" onclick="ButtonPeriod4_Click" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-period btn-success" ID="ButtonPeriod5" runat="server" Text="Period 5" onclick="ButtonPeriod5_Click" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-period btn-success" ID="ButtonPeriod6" runat="server" Text="Period 6" onclick="ButtonPeriod6_Click" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-period btn-success" ID="ButtonPeriod7" runat="server" Text="Period 7" onclick="ButtonPeriod7_Click" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-period btn-success" ID="ButtonPeriod8" runat="server" Text="Period 8" onclick="ButtonPeriod8_Click" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-period btn-success" ID="ButtonPeriod9" runat="server" Text="Period 9" onclick="ButtonPeriod9_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonMonday" runat="server" Text="Monday" onclick="ButtonMonday_Click" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxM1" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxM2" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxM3" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxM4" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxM5" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxM6" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxM7" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxM8" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxM9" runat="server" autopostback="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonTuesday" runat="server" Text="Tuesday" onclick="ButtonTuesday_Click" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxT1" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxT2" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxT3" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxT4" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxT5" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxT6" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxT7" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxT8" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxT9" runat="server" autopostback="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonWednesday" runat="server" Text="Wednesday" onclick="ButtonWednesday_Click" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxW1" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxW2" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxW3" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxW4" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxW5" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxW6" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxW7" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxW8" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxW9" runat="server" autopostback="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonThursday" runat="server" Text="Thursday" onclick="ButtonThursday_Click" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxJ1" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxJ2" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxJ3" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxJ4" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxJ5" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxJ6" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxJ7" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxJ8" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxJ9" runat="server" autopostback="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonFriday" runat="server" Text="Friday" onclick="ButtonFriday_Click" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxF1" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxF2" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxF3" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxF4" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxF5" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxF6" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxF7" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxF8" runat="server" autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" text="x" ID="CheckBoxF9" runat="server" autopostback="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td> </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                     <asp:Button class="btn btn-success btn-block topmarg" ID="Button1" runat="server" onclick="Button1_Click" Text="Submit Request" />
                       
                                    </td>
                                    <td colspan="5">
                                     <asp:Button class="btn btn-warning btn-block topmarg" ID="ButtonClearAll" runat="server" onclick="ButtonClearAll_Click" Text="Clear All" />
                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="errorMessage" runat="server" Text="" />
                                    </td>
                                </tr>
                            </table>
                        </div><!-- ./col -->
                    </div><!-- ./row -->

                    <div class="clearfix"></div>
                    </br>
                    <div class="row">
                        <div class="text-center col-md-6 col-sm-6">
                            </div>
                        <div class="text-center col-md-6 col-sm-6">
                            </div>
                    </div><!-- ./row -->
                </div><!-- ./canistercontainer -->
            </div><!-- ./canister -->


</asp:Content>
