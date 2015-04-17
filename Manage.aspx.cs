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

namespace Team11
{
    public partial class Manage : System.Web.UI.Page
    {
        string moduleCode = "";
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
        }

        //When department or central button is pressed
        protected void RadioButtonListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Search preference by Room or Date, hide the unselected one
            if (this.RadioButtonListView.SelectedIndex == 0)
            {
                this.divByRoom.Visible = true;
                this.divByDate.Visible = false;
            }
            else
            {
                this.divByRoom.Visible = false;
                this.divByDate.Visible = true;
               // this.divBookingByRoom.Visible = false;
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
        protected void RadioButtonListMakePrivate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAvailableRoomID = DropDownListRooms.SelectedItem.Value;
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect.Open();
            string roomString = "UPDATE Room SET private=1 WHERE roomID= " + selectedAvailableRoomID;
            SqlCommand roomCommand = new SqlCommand(roomString, connect);
            roomCommand.ExecuteNonQuery();

            Response.Redirect(Request.RawUrl);
        }

        //when the 'Remove Room from Private' button is clicked
        protected void RadioButtonListRemovePrivate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPrivateRoomID = DropDownListPrivateRooms.SelectedItem.Value;
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect.Open();
            string roomString = "UPDATE Room SET private=0 WHERE roomID= " + selectedPrivateRoomID;
            SqlCommand roomCommand = new SqlCommand(roomString, connect);
            roomCommand.ExecuteNonQuery();

            Response.Redirect(Request.RawUrl);
            
        }

    }
}