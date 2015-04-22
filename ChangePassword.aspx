<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChangePassword.aspx.cs" Inherits="Team11.ChangePassword" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <!-- ChangePassword CSS -->
    <link rel="Stylesheet" type="text/css" href="Styles/ChangePassword.css" />
        
</asp:Content>



<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">
    <h1>Change Password</h1>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">

<!--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AdminConnectionString %>" 
        SelectCommand="SELECT * FROM [User] WHERE ([userID] = @userID)"></asp:SqlDataSource>-->

        
   <!-- <div id="content2" runat="server"> <%--This div is where buttons, text forms, and other text will be placed. --%>
        <!--<p>Test!</p>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>-->
      <!--  <asp:Label ID="Label2" runat="server" Text="Current Password:"></asp:Label>
        
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="New Password:"></asp:Label>
       
        <br />
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <br />
    </div>  -->
    
   



    <div class="canister">
        <div class="canistertitle">
            <h2>Change Password</h2>
        </div>
        <div class="canistercontainer">
            <div class="row">
                <div class="text-center col-md-4 col-sm-4">
                    <h3>Current Password:</h3>
                </div>
                <div class="text-center col-md-8 col-sm-8">
                   <asp:TextBox ID="CurrentPassTextBox" runat="server" Width="130px" ></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="text-center col-md-4 col-sm-4">
                    <h3>New Password:</h3>
                </div>
                <div class="text-center col-md-8 col-sm-8">
                   <asp:TextBox ID="NewPassTextBox" runat="server" Width="130px" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="text-center col-md-4 col-sm-4">
                    <h3>Confirm New Password:</h3>
                </div>
                <div class="text-center col-md-8 col-sm-8">
                    <asp:TextBox ID="NewPassTextBox2" runat="server" Width="130px" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="text-center col-md-4 col-sm-4">
                    <h3>Enter New Hint:</h3>
                </div>
                <div class="text-center col-md-8 col-sm-8">
                    <asp:TextBox ID="HintTextBox" runat="server" Width="60%" TextMode="MultiLine" ></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="text-center col-md-6 col-sm-6 col-md-offset-3 col-sm-offset-3">
                    <asp:Button class="btn btn-success btn-block" ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" />
                </div>
            </div>
        </div>
        <asp:Label class="center errormessage" style="color:red;" ID="ErrorLabel" Text="" runat="server"></asp:Label>
    </div>
    


</asp:Content>

