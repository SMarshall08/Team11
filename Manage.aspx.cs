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


            //fill dropdown box with rooms
            if (!IsPostBack)
            {
                SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                connect.Open();

                //Find all rooms
                string findrooms = "SELECT  Room.roomName FROM Room INNER JOIN Building ON Room.buildingID = Building.buildingID INNER JOIN [User] ON Building.deptName = [User].deptName AND [User].userID =" + userID;
                SqlCommand roomscommand = new SqlCommand(findrooms, connect);
                SqlDataReader rooms = roomscommand.ExecuteReader();

                //Add the results to the dropdownlist
                while (rooms.Read())
                {
                    DropDownListRooms.Items.Add(rooms.GetString(0).ToString());
                }
                connect.Close();
                //roomavailibility();
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

        //when the selected value in the rooms dropdown box changes
        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            //roomavailibility();
        }

    }
}