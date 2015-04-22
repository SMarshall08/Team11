﻿using System;
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
            }
        }

        //When department or central button is pressed
        protected void RadioButtonListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Search preference by Room or Date, hide the unselected one
            if (this.RadioButtonListView.SelectedIndex == 0)
            {
                this.divByDepartment.Visible = true;
                this.divByCentral.Visible = false;
            }
            else
            {
                this.divByDepartment.Visible = false;
                this.divByCentral.Visible = true;
            }

        }

        //when the selected value in the available rooms dropdown box changes
        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            //roomavailibility();
        }

        //when the selected value in the private rooms dropdown box changes
        protected void DropDownListPrivateRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            //roomavailibility();
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
                reLoadElements("central");
            }

        }

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
            reLoadElements("central");
            
        }

        //reLoadElements//reLoadElements//reLoadElements
        //reLoadElements//reLoadElements//reLoadElements
        //reLoadElements//reLoadElements//reLoadElements
        //reLoadElements//reLoadElements//reLoadElements
        //This Function will reload particualar things on the page so the whole page does not have to be reloaded to refill values
        protected void reLoadElements(string managerType) //the parameter determines which elements will be loaded. either on the central section or the departmental section
        {
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

            if (managerType == "central")
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
        }

    }
}