using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Team11
{
    public partial class Manage : System.Web.UI.Page
    {
        int userID = 0;

        //pageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            // read the userid from the querystring
            userID = Convert.ToInt32(Session["userID"]);


            //fill dropdown box with available rooms
            if (!IsPostBack)
            {
                //Find all rooms and their ID
                string availableRoomSQL = "SELECT  Room.roomName, Room.roomID FROM Room INNER JOIN Building ON Room.buildingID = Building.buildingID INNER JOIN [User] ON Building.deptName = [User].deptName AND [User].userID =" + userID + "AND Room.private <> 1 ORDER BY Room.roomName";
                SqlConnection availableRoomConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand availableRoomCmd = new SqlCommand(availableRoomSQL, availableRoomConnection);
                SqlDataReader availableRoomReader;

                DropDownListRooms.Items.Clear();
                ListItem newItem2 = new ListItem();

                availableRoomConnection.Open();
                availableRoomReader = availableRoomCmd.ExecuteReader();
                //add/set the values to the dropdown box
                while (availableRoomReader.Read())
                {
                    newItem2 = new ListItem();
                    newItem2.Text = availableRoomReader["roomName"].ToString();
                    newItem2.Value = availableRoomReader["roomID"].ToString();
                    DropDownListRooms.Items.Add(newItem2);
                }
                availableRoomReader.Close();
            }

            //fill dropdown box with private rooms
            if (!IsPostBack)
            {
                string privateRoomSQL = "SELECT Room.roomName, Room.roomID FROM Room INNER JOIN Building ON Room.buildingID = Building.buildingID INNER JOIN [User] ON Building.deptName = [User].deptName AND [User].userID = " + userID + " AND Room.private <> 0 ORDER BY Room.roomName";
                SqlConnection privateRoomConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand privateRoomCmd = new SqlCommand(privateRoomSQL, privateRoomConnection);
                SqlDataReader privateRoomReader;

                DropDownListPrivateRooms.Items.Clear();
                ListItem newItem2 = new ListItem();

                privateRoomConnection.Open();
                privateRoomReader = privateRoomCmd.ExecuteReader();
                //add/set the values to the dropdown box
                while (privateRoomReader.Read())
                {
                    newItem2 = new ListItem();
                    newItem2.Text = privateRoomReader["roomName"].ToString();
                    newItem2.Value = privateRoomReader["roomID"].ToString();
                    DropDownListPrivateRooms.Items.Add(newItem2);
                }
                privateRoomReader.Close();
            }

            //fill dropdown box with facilities
            if (!Page.IsPostBack)
            {
                string listSQL = "SELECT facilityID,facilityName FROM Facility";

                SqlConnection deleteFacilitiesConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand(listSQL, deleteFacilitiesConnection);
                SqlDataReader reader;
                
                facilityList.Items.Clear(); //Clears the drop down list (for deleting facilities). 
                ListItem newItem = new ListItem();
                /*newItem.Text = "Choose a facility";
                newItem.Value = "test";
                facilityList.Items.Add(newItem);
                */
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
                facilityText.Text = string.Empty;//Clear the text from the textbox used to add facilities
            }


            //fill dropdown box with non-pool rooms
            if (!Page.IsPostBack)
            {
                string roomSQL = "SELECT roomID,roomName FROM Room WHERE pool=0";

                SqlConnection roomConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand roomCmd = new SqlCommand(roomSQL, roomConnection);
                SqlDataReader roomReader;
                roomDropDownList.Items.Clear();

                ListItem newItem3 = new ListItem();
                /*newItem3.Text = "Choose a non-pool room";
                newItem3.Value = "test";
                roomDropDownList.Items.Add(newItem3);
                */
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
            }

            //fill dropdown box with pool rooms
            if (!Page.IsPostBack)
            {
                string poolSQL = "SELECT roomID,roomName FROM Room WHERE pool=1";

                SqlConnection poolConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand poolCmd = new SqlCommand(poolSQL, poolConnection);
                SqlDataReader poolReader;

                poolDropDownList.Items.Clear();

                ListItem newItem2 = new ListItem();
                /*newItem2.Text = "Choose a pool room";
                newItem2.Value = "test";
                poolDropDownList.Items.Add(newItem2);
                */
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
                poolConnection.Close();
            }

            //fill dropdown box  Room to Edit
            if (!Page.IsPostBack)
            {
                string allRoomsSQL = "SELECT roomID,roomName FROM Room";

                SqlConnection allRoomsConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand allRoomsCmd = new SqlCommand(allRoomsSQL, allRoomsConnection);
                SqlDataReader allReader;
                editFacilitiesList.Items.Clear();

                ListItem newItem4 = new ListItem();
                /*newItem4.Text = "Choose a room";
                newItem4.Value = "test";
                editFacilitiesList.Items.Add(newItem4);
                */
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
                allRoomsConnection.Close();

            }
        }

        //When department or central button is pressed
        //controls which part is visible to central/departmental admin.
        protected void RadioButtonListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Search preference by Room or Date, hide the unselected one
            if (this.RadioButtonListView.SelectedIndex == 0)
            {
                this.divByDepartment.Visible = true;
                this.divByCentralFacility.Visible = false;
                this.divByCentralPoolRoom.Visible = false;
                this.divByCentralEditRoom.Visible = false;
                this.divByCentralRespond.Visible = false;
            }
            else
            {
                this.divByDepartment.Visible = false;
                this.divByCentralFacility.Visible = true;
                this.divByCentralPoolRoom.Visible = true;
                this.divByCentralEditRoom.Visible = true;
                this.divByCentralRespond.Visible = true;
            }

        }

        //when the 'Make Room Private' button is clicked
        protected void CheckBoxListMakePrivate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAvailableRoomID = DropDownListRooms.SelectedItem.Value;
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect.Open();
            string roomString = "UPDATE Room SET private=1 WHERE roomID= " + selectedAvailableRoomID;
            SqlCommand roomCommand = new SqlCommand(roomString, connect);
            roomCommand.ExecuteNonQuery();
            connect.Close();
            //Response.Redirect(Request.RawUrl);
            reLoadElements("department");
        }

        //when the 'Remove Room from Private' button is clicked
        protected void CheckBoxListRemovePrivate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPrivateRoomID = DropDownListPrivateRooms.SelectedItem.Value;
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect.Open();
            string roomString = "UPDATE Room SET private=0 WHERE roomID= " + selectedPrivateRoomID;
            SqlCommand roomCommand = new SqlCommand(roomString, connect);
            roomCommand.ExecuteNonQuery();
            connect.Close();
            //Response.Redirect(Request.RawUrl);
            reLoadElements("department");
            
        }

        //when the 'Add Facility' button is pressed
        protected void CheckBoxListaddFacility_SelectedIndexChanged(object sender, EventArgs e)
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
                connect2.Close();
                //Response.Redirect(Request.RawUrl);
                reLoadElements("centralFacility");
            }

        }

        //when the 'Delete Facility' button is pressed
        protected void CheckBoxListdeleteFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFacility = facilityList.SelectedItem.Value;
            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect2.Open();
            string deleteFacilityString = "DELETE FROM Facility WHERE facilityID=" + selectedFacility;
            SqlCommand deleteFacilityCommand = new SqlCommand(deleteFacilityString, connect2);
            deleteFacilityCommand.ExecuteNonQuery();
            //Response.Redirect(Request.RawUrl);
            connect2.Close();
            reLoadElements("centralFacility");
            
        }

        //when the 'Add Pool Room' button is pressed
        protected void CheckBoxListaddPoolRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRoom = roomDropDownList.SelectedItem.Value;
            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect2.Open();
            string roomString = "UPDATE Room SET pool=1 WHERE roomID=" + selectedRoom;
            SqlCommand roomCommand = new SqlCommand(roomString, connect2);
            roomCommand.ExecuteNonQuery();
            connect2.Close();
            //Response.Redirect(Request.RawUrl);
            reLoadElements("centralPoolRoom");
        }

        //when the 'Delete Pool Room' button is pressed
        protected void CheckBoxListremovePoolRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPoolRoom = poolDropDownList.SelectedItem.Value;
            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect2.Open();
            string poolString = "UPDATE Room SET pool=0 WHERE roomID=" + selectedPoolRoom;
            SqlCommand poolCommand = new SqlCommand(poolString, connect2);
            poolCommand.ExecuteNonQuery();
            connect2.Close();
            //Response.Redirect(Request.RawUrl);
            reLoadElements("centralPoolRoom");
        }

        //when the 'Edit Facility' button is pressed
        protected void CheckBoxListeditFacilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRoomToEdit = editFacilitiesList.SelectedItem.Value;
            Session["selectedRoomToEdit"] = selectedRoomToEdit;
            Response.Redirect("Facilities.aspx");
        }
        //when the 'Respond to request' button is pressed
        protected void CheckBoxListRespond_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("acceptRequests.aspx");
        }

        

        //reLoadElements//reLoadElements//reLoadElements
        //reLoadElements//reLoadElements//reLoadElements
        //reLoadElements//reLoadElements//reLoadElements
        //reLoadElements//reLoadElements//reLoadElements
        //This Function will reload particualar things on the page so the whole page does not have to be reloaded to refill values
        protected void reLoadElements(string managerType) //the parameter determines which elements will be loaded. either on the central section or the departmental section
        {

            //Reload the elements in the 'Add/Remove Private Room' container
            if (managerType=="department")
            {
                //Find all rooms and their ID
                string availableRoomSQL = "SELECT  Room.roomName, Room.roomID FROM Room INNER JOIN Building ON Room.buildingID = Building.buildingID INNER JOIN [User] ON Building.deptName = [User].deptName AND [User].userID =" + userID + "AND Room.private <> 1 ORDER BY Room.roomName";
                SqlConnection availableRoomConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand availableRoomCmd = new SqlCommand(availableRoomSQL, availableRoomConnection);
                SqlDataReader availableRoomReader;

                DropDownListRooms.Items.Clear();
                ListItem newItem2 = new ListItem();

                availableRoomConnection.Open();
                availableRoomReader = availableRoomCmd.ExecuteReader();
                //add/set the values to the dropdown box
                while (availableRoomReader.Read())
                {
                    newItem2 = new ListItem();
                    newItem2.Text = availableRoomReader["roomName"].ToString();
                    newItem2.Value = availableRoomReader["roomID"].ToString();
                    DropDownListRooms.Items.Add(newItem2);
                }
                availableRoomReader.Close();
            }

            //Reload the elements in the 'Add/Remove Private Room' container
            if (managerType == "department")
            {
                string privateRoomSQL = "SELECT Room.roomName, Room.roomID FROM Room WHERE (private = 1) ORDER BY Room.roomName";
                SqlConnection privateRoomConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand privateRoomCmd = new SqlCommand(privateRoomSQL, privateRoomConnection);
                SqlDataReader privateRoomReader;

                DropDownListPrivateRooms.Items.Clear();
                ListItem newItem2 = new ListItem();

                privateRoomConnection.Open();
                privateRoomReader = privateRoomCmd.ExecuteReader();
                //add/set the values to the dropdown box
                while (privateRoomReader.Read())
                {
                    newItem2 = new ListItem();
                    newItem2.Text = privateRoomReader["roomName"].ToString();
                    newItem2.Value = privateRoomReader["roomID"].ToString();
                    DropDownListPrivateRooms.Items.Add(newItem2);
                }
                privateRoomReader.Close();
            }

            //Reload the elements in the 'Add/Delete Facility' container
            if (managerType == "centralFacility")
            {
                string listSQL = "SELECT facilityID,facilityName FROM Facility";

                SqlConnection deleteFacilitiesConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand(listSQL, deleteFacilitiesConnection);
                SqlDataReader reader;

                facilityList.Items.Clear(); //Clears the drop down list (for deleting facilities). 
                ListItem newItem = new ListItem();
                /*newItem.Text = "Choose a facility";
                newItem.Value = "test";
                facilityList.Items.Add(newItem);
                */
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
                facilityText.Text = string.Empty;//Clear the text from the textbox used to add facilities
            }

            //Reload the elements in the 'Add/Delete Pool Rooms' container
            if (managerType == "centralPoolRoom")
            {
                //reload pool non-room dropdown box
                string roomSQL = "SELECT roomID,roomName FROM Room WHERE pool=0";

                SqlConnection roomConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand roomCmd = new SqlCommand(roomSQL, roomConnection);
                SqlDataReader roomReader;
                roomDropDownList.Items.Clear();

                ListItem newItem3 = new ListItem();
                /*newItem3.Text = "Choose a non-pool room";
                newItem3.Value = "test";
                roomDropDownList.Items.Add(newItem3);
                */
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
                filterRoom.Text = string.Empty;

                //reload pool room dropdown box
                string poolSQL = "SELECT roomID,roomName FROM Room WHERE pool=1";

                SqlConnection poolConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand poolCmd = new SqlCommand(poolSQL, poolConnection);
                SqlDataReader poolReader;

                poolDropDownList.Items.Clear();

                ListItem newItem2 = new ListItem();
                /*newItem2.Text = "Choose a pool room";
                newItem2.Value = "test";
                poolDropDownList.Items.Add(newItem2);
                */
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
                poolConnection.Close();
                filterPool.Text = string.Empty;

            }

        }

    }
}