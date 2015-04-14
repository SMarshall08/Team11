<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Team11.Manage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="Styles/CreateRequest.css" />
    <link rel="Stylesheet" type="text/css" href="Styles/Availability.css"/>    

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


    <!-- BY Depratment -->
    <div id="divByRoom" runat="server" visible="false">

        <div class="canister">
            <div class="canistertitle">
                <h2>Depratment</h2>
            </div>
            <div class="canistercontainer">

            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->
    </div>


    <!-- BY Central -->
    
    <div id="divByDate" runat="server" visible="false">
        
        <div class="canister">
            <div class="canistertitle">
                <h2>Central</h2>
            </div>
            <div class="canistercontainer">

            </div><!-- ./canistercontainer -->
        </div><!-- ./canister -->        
    </div>
</asp:Content>