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
    public partial class Facilities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["selectedRoomToEdit"] != null)
            {
                string roomID = Session["selectedRoomToEdit"].ToString();


                string getRoomsSQL = "SELECT roomName FROM Room WHERE roomID =" + roomID;
                SqlConnection getRoomsConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand getRoomsCmd = new SqlCommand(getRoomsSQL, getRoomsConnection);
                
                getRoomsConnection.Open();



                
                string roomName = getRoomsCmd.ExecuteScalar().ToString();
                
                


                labelID.Text = "You are editing the facilities for room " + roomName + ". If this is not correct, you can ";
            }

        }
    }
}
