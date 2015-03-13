<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Availibility.aspx.cs" Inherits="Team11.Availibility" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="Styles/CreateRequest.css" />
    <link rel="Stylesheet" type="text/css" href="Styles/Availability.css"/>    
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function ()
        {
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
                function(index,controlAva)
                {
                    if($(controlAva).is(":checked"))
                        $(controlAva).parent().addClass("btn btn-danger");
                }
                );
            

        });
    </script>


</asp:Content>

<%-- Page Title Content --%>
<asp:Content ID="TitlesContent" runat="server" ContentPlaceHolderID="TitleContent">
    <h1>Ad-Hoc</h1>
</asp:Content>

<%-- Body Content --%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="canister none">
        <div class="canistertitle none">
            <h2>Ad-Hoc by Room / Date</h2>
        </div>
        <div class="canistercontainer none">
            <div class="row">
                <div class="text-center col-md-12 col-sm-12 none">
                    <asp:RadioButtonList class="center none" ID="RadioButtonListView" runat="server" AutoPostBack="true" onselectedindexchanged="RadioButtonListView_SelectedIndexChanged" RepeatDirection="Horizontal">
                        <asp:ListItem class="none btn btn-primary">By Room</asp:ListItem>
                        <asp:ListItem class="none btn btn-primary">By Date</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div><!-- ./row -->
    </div><!-- ./canister -->

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ParkConnectionString %>" 
        SelectCommand="SELECT [parkName] FROM [Park]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:myConnectionString %>" 
        SelectCommand="SELECT [moduleTitle] FROM [Module]"></asp:SqlDataSource>
                                                                 
    <!-- BY ROOM -->
    <div id="divByRoom" runat="server" visible="false">

        <div class="canister">
            <div class="canistertitle">
                <h2>Room Selection</h2>
            </div>
            <div class="canistercontainer">
                <div class="row">
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Park</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Building</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Room</h3>
                    </div>
                </div><!-- ./row -->

                <div class="row">
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:RadioButtonList class="center" ID="RadioButtonListPark" runat="server" AutoPostBack="true" onselectedindexchanged="RadioButtonListPark_SelectedIndexChanged" RepeatDirection="Horizontal">
                            <asp:ListItem class="btn btn-primary">Central</asp:ListItem>
                            <asp:ListItem class="btn btn-primary">East</asp:ListItem>
                            <asp:ListItem class="btn btn-primary">West</asp:ListItem>
                    </asp:RadioButtonList>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:DropDownList class="form-control" ID="DropDownListBuilding" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListBuilding_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:DropDownList class="form-control" ID="DropDownListRooms" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListRooms_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div><!-- ./row -->

                <div class="row">
                    <div class="text-center center col-md-12 col-sm-12">
                        <h3>Week</h3>
                    </div>
                </div>

                <div class="row">
                    <div class="text-center center col-md-12 col-sm-12">
                         <asp:DropDownList class="form-control" ID="DropDownListWeekNumber" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListWeekNumber_SelectedIndexChanged">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->

        
       


            <div class="canister">
                <div class="canistertitle">
                    <h2>Day and Period Selection</h2>
                </div>
                <div class="canistercontainer">
                    <div class="row">
                        <div class="text-center center col-md-12 col-sm-12">
                            <table width="800px" height="180px" class="center">
                                <tr>
                                    <td>
                                        <asp:Button class="btn btn-warning btn-block" ID="ButtonClearPeriods" runat="server" onclick="ButtonClearPeriods_Click" Text="Clear Periods" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod1" runat="server" onclick="ButtonPeriod1_Click" Text="Period 1" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod2" runat="server" onclick="ButtonPeriod2_Click" Text="Period 2" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod3" runat="server" onclick="ButtonPeriod3_Click" Text="Period 3" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod4" runat="server" onclick="ButtonPeriod4_Click" Text="Period 4" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod5" runat="server" onclick="ButtonPeriod5_Click" Text="Period 5" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod6" runat="server" onclick="ButtonPeriod6_Click" Text="Period 6" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod7" runat="server" onclick="ButtonPeriod7_Click" Text="Period 7" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod8" runat="server" onclick="ButtonPeriod8_Click" Text="Period 8" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod9" runat="server" onclick="ButtonPeriod9_Click" Text="Period 9" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button style="height:40px;" class="btn btn-success btn-block" ID="ButtonMonday" runat="server" onclick="ButtonMonday_Click" Text="Monday" />
                                    </td>
                                    <td>
                                        <asp:CheckBox  class="btn btn-primary btn-block" ID="CheckBoxM1" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button  class="fullheight btn btn-info btn-block" ID="LabelM1" runat="server" Text="Label" Visible="False" onclick="LabelM1_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button  class="fullheight btn btn-info btn-block" ID="LabelM2" runat="server" Text="Label" Visible="False" onclick="LabelM2_Click"></asp:Button>
                                        <asp:CheckBox  class="btn btn-primary btn-block" ID="CheckBoxM2" Text="x" runat="server"  autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox  class="btn btn-primary btn-block" ID="CheckBoxM3" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button  class="fullheight btn btn-info btn-block" ID="LabelM3" runat="server" Text="Label" Visible="False" onclick="LabelM3_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox  class="btn btn-primary btn-block" ID="CheckBoxM4" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button  class="btn btn-info btn-block" ID="LabelM4" runat="server" Text="Label" Visible="False" onclick="LabelM4_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox  class="btn btn-primary btn-block" ID="CheckBoxM5" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button  class="btn btn-info btn-block" ID="LabelM5" runat="server" Text="Label" Visible="False" onclick="LabelM5_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox  class="btn btn-primary btn-block" ID="CheckBoxM6" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button  class="btn btn-info btn-block" ID="LabelM6" runat="server" Text="Label" Visible="False" onclick="LabelM6_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox  class="btn btn-primary btn-block" ID="CheckBoxM7" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button  class="btn btn-info btn-block" ID="LabelM7" runat="server" Text="Label" Visible="False" onclick="LabelM7_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox  class="btn btn-primary btn-block" ID="CheckBoxM8" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button  class="btn btn-info btn-block" ID="LabelM8" runat="server" Text="Label" Visible="False" onclick="LabelM8_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxM9" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelM9" runat="server" Text="Label" Visible="False" onclick="LabelM9_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button style="height:40px;" class="btn btn-success btn-block" ID="ButtonTuesday" runat="server" onclick="ButtonTuesday_Click" Text="Tuesday" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxT1" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelT1" runat="server" Text="Label" Visible="False" onclick="LabelT1_Click" ></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxT2" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelT2" runat="server" Text="Label" Visible="False" onclick="LabelT2_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxT3" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelT3" runat="server" Text="Label" Visible="False" onclick="LabelT3_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxT4" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelT4" runat="server" Text="Label" Visible="False" onclick="LabelT4_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxT5" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelT5" runat="server" Text="Label" Visible="False" onclick="LabelT5_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxT6" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelT6" runat="server" Text="Label" Visible="False" onclick="LabelT6_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxT7" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelT7" runat="server" Text="Label" Visible="False" onclick="LabelT7_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxT8" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelT8" runat="server" Text="Label" Visible="False" onclick="LabelT8_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxT9" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelT9" runat="server" Text="Label" Visible="False" onclick="LabelT9_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button style="height:40px;" class="btn btn-success btn-block" ID="ButtonWednesday" runat="server" onclick="ButtonWednesday_Click" Text="Wednesday" />
                                    </td>
                                    <td>
                                        <asp:Button class="btn btn-info btn-block" ID="LabelW1" runat="server" Text="Label" Visible="False" onclick="LabelW1_Click"></asp:Button>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxW1" Text="x" runat="server"  autopostback="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxW2" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelW2" runat="server" Text="Label" Visible="False" onclick="LabelW2_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxW3" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelW3" runat="server" Text="Label" Visible="False" onclick="LabelW3_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxW4" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelW4" runat="server" Text="Label" Visible="False" onclick="LabelW4_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxW5" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelW5" runat="server" Text="Label" Visible="False" onclick="LabelW5_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxW6" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelW6" runat="server" Text="Label" Visible="False" onclick="LabelW6_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxW7" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelW7" runat="server" Text="Label" Visible="False" onclick="LabelW7_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxW8" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelW8" runat="server" Text="Label" Visible="False" onclick="LabelW8_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxW9" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelW9" runat="server" Text="Label" Visible="False" onclick="LabelW9_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button style="height:40px;" class="btn btn-success btn-block" ID="ButtonThursday" runat="server" onclick="ButtonThursday_Click" Text="Thursday" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxJ1" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelJ1" runat="server" Text="Label" Visible="False" onclick="LabelJ1_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxJ2" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelJ2" runat="server" Text="Label" Visible="False" onclick="LabelJ2_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxJ3" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelJ3" runat="server" Text="Label" Visible="False" onclick="LabelJ3_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxJ4" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelJ4" runat="server" Text="Label" Visible="False" onclick="LabelJ4_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxJ5" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelJ5" runat="server" Text="Label" Visible="False" onclick="LabelJ5_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxJ6" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelJ6" runat="server" Text="Label" Visible="False" onclick="LabelJ6_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxJ7" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelJ7" runat="server" Text="Label" Visible="False" onclick="LabelJ7_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxJ8" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelJ8" runat="server" Text="Label" Visible="False" onclick="LabelJ8_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxJ9" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelJ9" runat="server" Text="Label" Visible="False" onclick="LabelJ9_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button style="height:40px;" class="btn btn-success btn-block" ID="ButtonFriday" runat="server" onclick="ButtonFriday_Click" Text="Friday" />
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxF1" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelF1" runat="server" Text="Label" Visible="False" onclick="LabelF1_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxF2" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelF2" runat="server" Text="Label" Visible="False" onclick="LabelF2_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxF3" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelF3" runat="server" Text="Label" Visible="False" onclick="LabelF3_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxF4" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelF4" runat="server" Text="Label" Visible="False" onclick="LabelF4_Click" style="height: 26px"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxF5" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelF5" runat="server" Text="Label" Visible="False" onclick="LabelF5_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxF6" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelF6" runat="server" Text="Label" Visible="False" onclick="LabelF6_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxF7" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelF7" runat="server" Text="Label" Visible="False" onclick="LabelF7_Click" style="width: 47px"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxF8" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelF8" runat="server" Text="Label" Visible="False" onclick="LabelF8_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:CheckBox class="btn btn-primary btn-block" ID="CheckBoxF9" Text="x" runat="server"  autopostback="true" />
                                        <asp:Button class="btn btn-info btn-block" ID="LabelF9" runat="server" Text="Label" Visible="False" onclick="LabelF9_Click"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <asp:Button style="margin-top:5px;" class="btn btn-success btn-block" ID="ButtonBookByRoom" runat="server" Text="Book" onclick="ButtonBookByRoom_Click" />
                                    </td>
                                    
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div><!-- ./canister -->
                
            
        <br />
        <br />
        
        
        
        <div id="requestDetails" runat="server" style="height: 191px" visible="false">
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Details</h2>
            </div>
            <div class="canistercontainer details">
                <div class="row">
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Reference</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Department</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Module Code</h3>
                    </div>
                   
                </div><!-- ./row -->
                <div class="row">
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="requestidLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="departmentLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="modulecodeLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    
                </div><!-- ./row -->
                <div class="row">
                     <div class="text-center center col-md-4 col-sm-4">
                        <h3>Module Title</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Semester</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Year</h3>
                    </div>
                </div>
                <div class="row">
                     <div class="text-center center col-md-4 col-sm-4">
                       <asp:Label ID="moduletitleLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="semesterLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="yearLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Day</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Period</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Week(s)</h3>
                    </div>
                </div>
                <div class="row">
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="dayLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="periodLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="weeksLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Park</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Building</h3>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <h3>Room</h3>
                    </div>
                    
                </div>
                <div class="row">
                    <div class="text-center center col-md-4 col-sm-4">
                         <asp:Label ID="parkLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="buildingLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="text-center center col-md-4 col-sm-4">
                        <asp:Label ID="bookedroomLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                    
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12">
                            <asp:Button style="margin-top:10px;" class="btn btn-block btn-success" ID="ButtonHideDetails" runat="server" OnClick="ButtonHideDetails_Click" Text="Hide" />
                    </div>
                    
                </div>
                </div><!-- ./canister -->
            </div><!-- ./canistercontainer -->
        </div>

    
    <div style="display:none">
        <asp:Label ID="altroomLabel" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="facilitiesLabel" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="statusLabel" runat="server" Text="Label"></asp:Label>
   </div>
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    </div><!-- ./Adhoc by room -->



        
        
                <div ID="divBookingByRoom" runat="server" visible="false">

                <div style="margin-top:-200px;" class="canister">
                    <div class="canistertitle">
                        <h2>Booking</h2>
                    </div>
                    <div class="canistercontainer">
                        <div class="row">
                            <div class="text-center center col-md-3 col-sm-3">
                                <h3>Module</h3>
                            </div>
                            <div class="text-center center col-md-3 col-sm-3">
                                <h3>Day(s) and Period(s)</h3>
                            </div>
                            <div class="text-center center col-md-3 col-sm-3">
                                <h3>Week</h3>
                            </div>
                            <div class="text-center center col-md-3 col-sm-3">
                                <h3>Room</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="text-center center col-md-3 col-sm-3">
                                <asp:DropDownList class="form-control" ID="DropDownListModuleByRoom" runat="server" DataSourceID="SqlDataSource2" DataTextField="moduleTitle" DataValueField="moduleTitle">
                                </asp:DropDownList>
                            </div>
                            <div class="text-center center col-md-3 col-sm-3">
                                <asp:Label ID="LabelPeriod" runat="server"></asp:Label>
                            </div>
                            <div class="text-center center col-md-3 col-sm-3">
                                <asp:Label ID="LabelWeek" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="text-center center col-md-3 col-sm-3">
                                <asp:Label ID="LabelRoom" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="center col-md-6 col-sm-6">
                                <asp:Button style="margin-top:10px;" class="btn btn-success btn-block" ID="ButtonConfirm" runat="server" OnClick="ButtonConfirm_Click" Text="Confirm" />
                            </div>
                            <div class="col-md-6 col-sm-6">
                            <asp:Button style="margin-top:10px;" class="btn btn-warning btn-block" ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                                    
                            </div>
                            <asp:Label ID="LabelError" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                   
                    
                </div>
           
                
      
    
    <!-- BY Date -->
    
    <div id="divByDate" runat="server" visible="false">
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Ad-Hoc by Date</h2>
            </div>
            <div class="canistercontainer">
                <div class="row">
                    <div class="text-center center col-md-12 col-sm-12">
                        <h3>Week Required</h3>
                    </div>
                </div>
                <div class="row">
                    <div class="text-center center col-md-12 col-sm-12">
                        <asp:DropDownList class="form-control" ID="DropDownListWeeks" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div><!-- ./row -->
                <div class="row">
                    <div class="col-md-12 col-sm-12 text-center center">
                        
                                <table width="700px" class="center" style="margin-top:10px;">
                                    <tr>
                                        <td>
                                            <asp:Button class="btn btn-warning btn-block" ID="ButtonClearPeriods0" runat="server" onclick="ButtonClearPeriods_Click1" Text="Clear Periods" />
                                        </td>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod10" runat="server" onclick="ButtonPeriod1_Click1" Text="Period 1" />
                                        </td>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod11" runat="server" onclick="ButtonPeriod2_Click1" Text="Period 2" />
                                        </td>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod12" runat="server" onclick="ButtonPeriod3_Click1" Text="Period 3" />
                                        </td>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod13" runat="server" onclick="ButtonPeriod4_Click1" Text="Period 4" />
                                        </td>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod14" runat="server" onclick="ButtonPeriod5_Click1" Text="Period 5" />
                                        </td>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod15" runat="server" onclick="ButtonPeriod6_Click1" Text="Period 6" />
                                        </td>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod16" runat="server" onclick="ButtonPeriod7_Click1" Text="Period 7" />
                                        </td>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod17" runat="server" onclick="ButtonPeriod8_Click1" Text="Period 8" />
                                        </td>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonPeriod18" runat="server" onclick="ButtonPeriod9_Click1" Text="Period 9" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonMonday0" runat="server" onclick="ButtonMonday_Click1" Text="Monday" />
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxM10" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxM11" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxM12" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxM13" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxM14" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxM15" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxM16" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxM17" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxM18" runat="server" autopostback="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonTuesday0" runat="server" onclick="ButtonTuesday_Click1" Text="Tuesday" />
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxT10" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxT11" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxT12" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxT13" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxT14" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxT15" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxT16" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxT17" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxT18" runat="server" autopostback="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonWednesday0" runat="server" onclick="ButtonWednesday_Click1" Text="Wednesday" />
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxW10" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxW11" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxW12" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxW13" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxW14" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxW15" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxW16" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxW17" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxW18" runat="server" autopostback="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonThursday0" runat="server" onclick="ButtonThursday_Click1" Text="Thursday" />
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxJ10" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxJ11" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxJ12" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxJ13" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxJ14" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxJ15" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxJ16" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxJ17" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxJ18" runat="server" autopostback="true"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button class="btn btn-success btn-block" ID="ButtonFriday0" runat="server" onclick="ButtonFriday_Click1" Text="Friday" />
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxF10" runat="server" autopostback="true" />
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxF11" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxF12" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxF13" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxF14" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxF15" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxF16" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxF17" runat="server" autopostback="true"/>
                                        </td>
                                        <td>
                                            <asp:CheckBox class="btn btn-primary btn-block" Text="x" ID="CheckBoxF18" runat="server" autopostback="true"/>
                                        </td>
                                    </tr>
                                </table>
                           
                    </div>
                </div>
                <div class="row">
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:Button class="btn btn-success btn-block" style="margin-top:10px;" ID="ButtonFindRooms" runat="server" Text="Find Rooms" onclick="ButtonFindRooms_Click" />
                    </div>
                    <div class="text-center center col-md-6 col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                    <asp:DropDownList class="form-control" style="margin-top:10px;" ID="DropDownListRoomsByDate" runat="server">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <div class="text-center center col-md-12 col-sm-12">
                        <asp:DropDownList style="margin-top:10px;" class="form-control" ID="DropDownListModulesByDate" runat="server" DataSourceID="SqlDataSource2" DataTextField="moduleTitle" DataValueField="moduleTitle">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="text-center center col-md-12 col-sm-12">
                        <asp:Button style="margin-top:10px;" class="btn btn-success btn-block" ID="BookByDate" runat="server" Text="Book" onclick="ButtonBookByDate_Click" />
                    </div>
                </div>


            </div>
        </div>
        
        
           
        <br />
        
        <br />
        <br />
        
        <br />
        
        <br />
        
        <br />
    </div>
</asp:Content>
