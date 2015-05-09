<%@ Page Title="Schedule" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="Team11.Schedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="Styles/Schedule.css"/>  
   
    <script type="text/javascript" language="javascript">
     $(document).ready(function () {

            //Semester
            if ($("#MainContent_RadioButtonListFilterSemester_0").is(":checked")) {
                $("#MainContent_RadioButtonListFilterSemester_0").parent().addClass("btn btn-danger");
            };
            if ($("#MainContent_RadioButtonListFilterSemester_1").is(":checked")) {
                $("#MainContent_RadioButtonListFilterSemester_1").parent().addClass("btn btn-danger");
            };
            if ($("#MainContent_RadioButtonListFilterSemester_2").is(":checked")) {
                $("#MainContent_RadioButtonListFilterSemester_2").parent().addClass("btn btn-danger");
            };

            //Status
            if ($("#MainContent_RadioButtonListFilterStatus_0").is(":checked")) {
                $("#MainContent_RadioButtonListFilterStatus_0").parent().addClass("btn btn-danger");
            };
            if ($("#MainContent_RadioButtonListFilterStatus_1").is(":checked")) {
                $("#MainContent_RadioButtonListFilterStatus_1").parent().addClass("btn btn-danger");
            };
            if ($("#MainContent_RadioButtonListFilterStatus_2").is(":checked")) {
                $("#MainContent_RadioButtonListFilterStatus_2").parent().addClass("btn btn-danger");
            };

         //Semester
            if ($("#MainContent_RadioButtonListFilterSemester_0").is(":checked")) {
                $("#MainContent_RadioButtonListFilterSemester_0").parent().addClass("btn btn-danger");
            };
            if ($("#MainContent_RadioButtonListFilterSemester_1").is(":checked")) {
                $("#MainContent_RadioButtonListFilterSemester_1").parent().addClass("btn btn-danger");
            };
            if ($("#MainContent_RadioButtonListFilterSemester_2").is(":checked")) {
                $("#MainContent_RadioButtonListFilterSemester_2").parent().addClass("btn btn-danger");
            };

         //Status
            if ($("#MainContent_RadioButtonListFilterStatusStaff_0").is(":checked")) {
                $("#MainContent_RadioButtonListFilterStatusStaff_0").parent().addClass("btn btn-danger");
            };
            if ($("#MainContent_RadioButtonListFilterStatusStaff_1").is(":checked")) {
                $("#MainContent_RadioButtonListFilterStatusStaff_1").parent().addClass("btn btn-danger");
            };
            if ($("#MainContent_RadioButtonListFilterStatusStaff_2").is(":checked")) {
                $("#MainContent_RadioButtonListFilterStatusStaff_2").parent().addClass("btn btn-danger");
            };

    });
    </script>


</asp:Content>

<%-- Page Title Content --%>
<asp:Content ID="TitlesContent" runat="server" ContentPlaceHolderID="TitleContent">
    <h1>Schedule</h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="canister none">
    <div class="canistertitle none">
        <h2>Schedule By Module / Staff</h2>
    </div>
    <div class="canistercontainer none">
        <div class="row">
            <div class="text-center col-md-12 col-sm-12 none">
                <asp:RadioButtonList class="center none" ID="RadioButtonScheduleView" runat="server" AutoPostBack="true" onselectedindexchanged="RadioButtonScheduleView_SelectedIndexChanged" RepeatDirection="Horizontal">
                        <asp:ListItem class="none btn btn-primary">Module</asp:ListItem>
                        <asp:ListItem class="none btn btn-primary">Staff</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
</div>
    
<div id="divByModule" runat="server" visible="false">   
<div id="SearchBar" runat="server">

     <div class="canister">
        <div class="canistertitle">
            <h2>Filter Results</h2>
        </div>
        <div class="canistercontainer">
            <div class="row">
                <div class="text-center center col-md-4 col-sm-4">
                    <h2>Module</h2>
                </div>
               
                <div class="text-center center col-md-4 col-sm-4">
                    <h2>Semester</h2>
                </div>
                <div class="text-center center col-md-4 col-sm-4">
                    <h2>Status</h2>
                </div>
            </div>
            <div class="row">
                <div class="text-center center col-md-4 col-sm-4">
                    <asp:DropDownList class="form-control" ID="DropDownListFilterModule" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListFilterModule_SelectedIndexChanged" >
                    </asp:DropDownList>
                </div>
                
                <div class="text-center center col-md-4 col-sm-4">
                    <asp:RadioButtonList class="center" ID="RadioButtonListFilterSemester" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="RadioButtonListFilterSemester_SelectedIndexChanged">
                        <asp:ListItem value="0" class="btn btn-primary" Selected="True">All</asp:ListItem>
                        <asp:ListItem value="1" class="btn btn-primary">1</asp:ListItem>
                        <asp:ListItem value="2" class="btn btn-primary">2</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="text-center center col-md-4 col-sm-4">
                    <asp:RadioButtonList class="center" ID="RadioButtonListFilterStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="RadioButtonListFilterStatus_SelectedIndexChanged">
                        <asp:ListItem value="0" class="btn btn-primary" Selected="True">ALL</asp:ListItem>
                        <asp:ListItem value="1" class="btn btn-primary">Pending</asp:ListItem>
                        <asp:ListItem value="2" class="btn btn-primary">Accepted</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="text-center col-md-3 col-sm-3">
                    <h3>Park</h3>
                </div>
                <div class="text-center col-md-3 col-sm-3">
                    <h3>Week</h3>
                </div>
                
                 <div class="text-center col-md-3 col-sm-3">
                    <h3>Part</h3>
                </div>
                <div class="text-center col-md-3 col-sm-3">
                    <h3>Year</h3>
                </div>
            </div>
            <div class="row">
                <div class="text-center col-md-3 col-sm-3">
                     <asp:DropDownList class="form-control" ID="DropDownListFilterPark" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterPark_SelectedIndexChanged">
                        <asp:ListItem Value="0">Any</asp:ListItem>
                        <asp:ListItem Value="1">Central</asp:ListItem>
                        <asp:ListItem Value="2">East</asp:ListItem>
                        <asp:ListItem Value="3">West</asp:ListItem>
                    </asp:DropDownList>
         
                </div>
                <div class="text-center col-md-3 col-sm-3">
                     <asp:DropDownList class="form-control" ID="DropDownListFilterWeek" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterWeek_SelectedIndexChanged">
                        
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        
                    </asp:DropDownList>
                </div>
                 <div class="text-center col-md-3 col-sm-3">
                     <asp:DropDownList class="form-control" ID="DropDownListFilterPart" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterPart_SelectedIndexChanged">
                        <asp:ListItem Value="0">Any</asp:ListItem>
                        <asp:ListItem Value="1">A</asp:ListItem>
                        <asp:ListItem Value="2">B</asp:ListItem>
                        <asp:ListItem Value="3">C</asp:ListItem>
                     </asp:DropDownList>

                </div>
                <div class="text-center col-md-3 col-sm-3">
                     <asp:DropDownList class="form-control" ID="DropDownListFilterYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterYear_SelectedIndexChanged">
                        <asp:ListItem Value="0">Any</asp:ListItem>
                         <asp:ListItem Value="2014">2013/2014</asp:ListItem>
                        <asp:ListItem Value="2013">2012/2013</asp:ListItem>
                        
                     </asp:DropDownList>
                     <asp:Button ID="Button1" runat="server" onclick="ButtonRefreshSearch_Click" Text="Search" Visible="False" />

                </div>

            </div>
        </div>
    </div>
        </div>
    
        <div>
            
            <div runat="server" id="ViewTable" style="margin-top: 0px"></div>
            

        </div>
    </div>

    <div id="divByStaff" runat="server" visible="false">   
<div id="SearchBarStaff" runat="server">

     <div class="canister">
        <div class="canistertitle">
            <h2>Filter Results</h2>
        </div>
        <div class="canistercontainer">
            <div class="row">
                <div class="text-center center col-md-4 col-sm-4">
                    <h2>Staff</h2>
                </div>
               
                <div class="text-center center col-md-4 col-sm-4">
                    <h2>Semester</h2>
                </div>
                <div class="text-center center col-md-4 col-sm-4">
                    <h2>Status</h2>
                </div>
            </div>
            <div class="row">
                <div class="text-center center col-md-4 col-sm-4">
                    <asp:DropDownList class="form-control" ID="DropDownListFilterStaff" runat="server" AutoPostBack="true" onselectedindexchanged="DropDownListFilterStaff_SelectedIndexChanged" >
                    </asp:DropDownList>
                </div>
                
                <div class="text-center center col-md-4 col-sm-4">
                    <asp:RadioButtonList class="center" ID="RadioButtonListFilterSemesterStaff" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="RadioButtonListFilterSemesterStaff_SelectedIndexChanged">
                        <asp:ListItem value="0" class="btn btn-primary" Selected="True">All</asp:ListItem>
                        <asp:ListItem value="1" class="btn btn-primary">1</asp:ListItem>
                        <asp:ListItem value="2" class="btn btn-primary">2</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="text-center center col-md-4 col-sm-4">
                    <asp:RadioButtonList class="center" ID="RadioButtonListFilterStatusStaff" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" onselectedindexchanged="RadioButtonListFilterStatusStaff_SelectedIndexChanged">
                        <asp:ListItem value="0" class="btn btn-primary" Selected="True">ALL</asp:ListItem>
                        <asp:ListItem value="1" class="btn btn-primary">Pending</asp:ListItem>
                        <asp:ListItem value="2" class="btn btn-primary">Accepted</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="text-center col-md-3 col-sm-3">
                    <h3>Park</h3>
                </div>
                <div class="text-center col-md-3 col-sm-3">
                    <h3>Week</h3>
                </div>
                
                 <div class="text-center col-md-3 col-sm-3">
                    <h3>Part</h3>
                </div>
                <div class="text-center col-md-3 col-sm-3">
                    <h3>Year</h3>
                </div>
            </div>
            <div class="row">
                <div class="text-center col-md-3 col-sm-3">
                     <asp:DropDownList class="form-control" ID="DropDownListFilterParkStaff" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterParkStaff_SelectedIndexChanged">
                        <asp:ListItem Value="0">Any</asp:ListItem>
                        <asp:ListItem Value="1">Central</asp:ListItem>
                        <asp:ListItem Value="2">East</asp:ListItem>
                        <asp:ListItem Value="3">West</asp:ListItem>
                    </asp:DropDownList>
         
                </div>
                <div class="text-center col-md-3 col-sm-3">
                     <asp:DropDownList class="form-control" ID="DropDownListFilterWeekStaff" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterWeekStaff_SelectedIndexChanged">
                        
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        
                    </asp:DropDownList>
                </div>
                 <div class="text-center col-md-3 col-sm-3">
                     <asp:DropDownList class="form-control" ID="DropDownListFilterPartStaff" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterPartStaff_SelectedIndexChanged">
                        <asp:ListItem Value="0">Any</asp:ListItem>
                        <asp:ListItem Value="1">A</asp:ListItem>
                        <asp:ListItem Value="2">B</asp:ListItem>
                        <asp:ListItem Value="3">C</asp:ListItem>
                     </asp:DropDownList>

                </div>
                <div class="text-center col-md-3 col-sm-3">
                     <asp:DropDownList class="form-control" ID="DropDownListFilterYearStaff" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListFilterYearStaff_SelectedIndexChanged">
                        <asp:ListItem Value="0">Any</asp:ListItem>
                         <asp:ListItem Value="2014">2013/2014</asp:ListItem>
                        <asp:ListItem Value="2013">2012/2013</asp:ListItem>
                        
                     </asp:DropDownList>
                     <asp:Button ID="Button2" runat="server" onclick="ButtonRefreshSearch_Click" Text="Search" Visible="False" />

                </div>

            </div>
        </div>
    </div>
        </div>
    
        <div>
            
            <div runat="server" id="ViewTableStaff" style="margin-top: 0px"></div>
            

        </div>
    </div>

    </asp:Content>
