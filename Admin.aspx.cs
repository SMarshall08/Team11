using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Text.RegularExpressions;

namespace Team11
{
    public partial class Admin : System.Web.UI.Page
    {
        int userID = 0;
        protected void Page_Load(object sender, EventArgs e) {

            string listSQL = "SELECT facilityID,facilityName FROM Facility";

            SqlConnection deleteFacilitiesConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand(listSQL, deleteFacilitiesConnection);
            SqlDataReader reader;

            string poolSQL = "SELECT roomID,roomName FROM Room WHERE pool=1";

            SqlConnection poolConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            SqlCommand poolCmd = new SqlCommand(poolSQL, poolConnection);
            SqlDataReader poolReader;

            string roomSQL = "SELECT roomID,roomName FROM Room WHERE pool=0";

            SqlConnection roomConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            SqlCommand roomCmd = new SqlCommand(roomSQL, roomConnection);
            SqlDataReader roomReader;

            string allRoomsSQL = "SELECT roomID,roomName FROM Room";

            SqlConnection allRoomsConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            SqlCommand allRoomsCmd = new SqlCommand(allRoomsSQL, allRoomsConnection);
            SqlDataReader allReader;

            try
            {
                if (!Page.IsPostBack)
                {

                    facilityList.Items.Clear(); //Clears the drop down list (for deleting facilities). Without this, every time the page is reloaded, the list would get bigger and bigger.

                    ListItem newItem = new ListItem();
                    newItem.Text = "Choose a facility";
                    newItem.Value = "test";
                    facilityList.Items.Add(newItem);

                    deleteFacilitiesConnection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        newItem = new ListItem();
                        newItem.Text = reader["facilityName"].ToString();
                        newItem.Value = reader["facilityID"].ToString();
                        facilityList.Items.Add(newItem);
                    }
                    reader.Close();

                    poolDropDownList.Items.Clear();

                    ListItem newItem2 = new ListItem();
                    newItem2.Text = "Choose a pool room";
                    newItem2.Value = "test";
                    poolDropDownList.Items.Add(newItem2);

                    poolConnection.Open();
                    poolReader = poolCmd.ExecuteReader();
                    while (poolReader.Read())
                    {
                        newItem2 = new ListItem();
                        newItem2.Text = poolReader["roomName"].ToString();
                        newItem2.Value = poolReader["roomID"].ToString();
                        poolDropDownList.Items.Add(newItem2);
                    }
                    poolReader.Close();

                    roomDropDownList.Items.Clear();

                    ListItem newItem3 = new ListItem();
                    newItem3.Text = "Choose a non-pool room";
                    newItem3.Value = "test";
                    roomDropDownList.Items.Add(newItem3);

                    roomConnection.Open();
                    roomReader = roomCmd.ExecuteReader();
                    while (roomReader.Read())
                    {
                        newItem3 = new ListItem();
                        newItem3.Text = roomReader["roomName"].ToString();
                        newItem3.Value = roomReader["roomID"].ToString();
                        roomDropDownList.Items.Add(newItem3);
                    }
                    roomReader.Close();
                    editFacilitiesList.Items.Clear();

                    ListItem newItem4 = new ListItem();
                    newItem4.Text = "Choose a room";
                    newItem4.Value = "test";
                    editFacilitiesList.Items.Add(newItem4);

                    allRoomsConnection.Open();
                    allReader = allRoomsCmd.ExecuteReader();
                    while (allReader.Read())
                    {
                        newItem4 = new ListItem();
                        newItem4.Text = allReader["roomName"].ToString();
                        newItem4.Value = allReader["roomID"].ToString();
                        editFacilitiesList.Items.Add(newItem4);
                    }
                    allReader.Close();
                }
            }
            catch (Exception err)
            {
                //TODO
            }
            finally
            {
                deleteFacilitiesConnection.Close();
            }
            

            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect.Open();
            
            userID = Convert.ToInt32(Request.QueryString["userID"]);
            /*
            string url = Request.Url.Query; ///Gets url
            Match match = Regex.Match(url, @"=(.*)"); ///Gets user ID from url
            string urlID = match.Groups[1].Value;       */
            string adminString = "Select administrator from [User] where userID = " + userID;
            SqlCommand adminCommand = new SqlCommand(adminString, connect);
            string administratorYesNo = adminCommand.ExecuteScalar().ToString();
            string trimmedAdmin = administratorYesNo.Trim();
            bool userIsAdmin = (trimmedAdmin == "yes");


            
           
             
            if (userIsAdmin)
         {        //Below executed if user logged in is an admin.            
             
             
             addFacility.Click += new EventHandler(addFacilityFunction);
             deleteFacility.Click += new EventHandler(deleteFacilityFunction);
             removePoolRoom.Click += new EventHandler(removePoolRoomFunction);
             addPoolRoom.Click += new EventHandler(addPoolRoomFunction);
             editFacilities.Click += new EventHandler(editFacilitiesFunction);
             
           }
           else {
                //Below executed if user logged in is not an admin.

               areYouAdmin.InnerHtml = "You are not logged in as an admin. Please log in to an account with administrator privileges in order to view the admin options.";
                //Because the inner html is set to the above, it rewrites all the html in admin.aspx, rendering the page blank for those who are not admin.
               
                 }

           


            connect.Close();
        
        
        }

        protected void addFacilityFunction(Object sender, EventArgs e) //This is executed when the add facility button is pressed.
        {



            string facility = facilityText.Text.ToString();
            if (facility == "")
            {


                scriptDiv.InnerHtml = "<script>alert(\"You have not entered a facility to add.\")</script>";

            }
            else
            {

                SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                connect2.Open();
                string addFacilityString = "Insert into Facility (facilityName) VALUES ('" + facility + "')";
                SqlCommand addFacilityCommand = new SqlCommand(addFacilityString, connect2);
                addFacilityCommand.ExecuteNonQuery();
                /// scriptDiv.InnerHtml = "<script>alert(\"Facility successfully added!\");window.location.href=\"Admin.aspx\"; </script>";
                ///scriptDiv.InnerHtml = "<script>alert(\"Facility successfully added!\")</script>";
                /// ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Facility successfully added')", true);
                Response.Redirect(Request.RawUrl);
            }


        }
        protected void deleteFacilityFunction(Object sender, EventArgs e) //This is executed when the delete facility button is pressed.
        {



            string selectedFacility = facilityList.SelectedItem.Value;
            if (selectedFacility == "test")
            {


                scriptDiv.InnerHtml = "<script>alert(\"You have not selected a facility to delete.\")</script>";

            }
            else
            {

                SqlConnection connect3 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                connect3.Open();
                string deleteFacilityString = "DELETE FROM Facility WHERE facilityID=" + selectedFacility;
                SqlCommand deleteFacilityCommand = new SqlCommand(deleteFacilityString, connect3);
                deleteFacilityCommand.ExecuteNonQuery();
                /// scriptDiv.InnerHtml = "<script>alert(\"Facility successfully deleted!\");window.location.href=\"Admin.aspx\"; </script>";
                /// scriptDiv.InnerHtml = "<script>alert(\"Facility successfully deleted!\")</script>";
                ///ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Facility successfully deleted')", true);
                Response.Redirect(Request.RawUrl);

            }

        }
        protected void removePoolRoomFunction(Object sender, EventArgs e) {
            string selectedPoolRoom = poolDropDownList.SelectedItem.Value;
            if (selectedPoolRoom == "test")
            {


                scriptDiv.InnerHtml = "<script>alert(\"You have not selected a pool room.\")</script>";

            }
            else {

                SqlConnection connect4 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                connect4.Open();
                string poolString = "UPDATE Room SET pool=0 WHERE roomID=" + selectedPoolRoom;
                SqlCommand poolCommand = new SqlCommand(poolString, connect4);
                poolCommand.ExecuteNonQuery();

                Response.Redirect(Request.RawUrl);
            
            
            
            }
        
        }
        protected void addPoolRoomFunction(Object sender, EventArgs e) {

            string selectedRoom = roomDropDownList.SelectedItem.Value;
            if (selectedRoom == "test")
            {


                scriptDiv.InnerHtml = "<script>alert(\"You have not selected a room.\")</script>";

            }
            else
            {

                SqlConnection connect5 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                connect5.Open();
                string roomString = "UPDATE Room SET pool=1 WHERE roomID=" + selectedRoom;
                SqlCommand roomCommand = new SqlCommand(roomString, connect5);
                roomCommand.ExecuteNonQuery();

                Response.Redirect(Request.RawUrl);



            }
        
        
        }
        protected void editFacilitiesFunction(Object sender, EventArgs e) {

            string selectedRoomToEdit = editFacilitiesList.SelectedItem.Value;
            if (selectedRoomToEdit == "test") {

                scriptDiv.InnerHtml = "<script>alert(\"You have not selected a room.\")</script>";
            
            }

            else {
                Session["selectedRoomToEdit"] = selectedRoomToEdit;
            Response.Redirect("Facilities.aspx");


            }
                
        
        
        }
                                 

        
        
    }
}
