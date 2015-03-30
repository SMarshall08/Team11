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

                removeFacility.Click += new EventHandler(removeFacilityFunction);
                addFacility.Click += new EventHandler(addFacilityFunction);
                string getRoomsSQL = "SELECT roomName FROM Room WHERE roomID =" + roomID;
                SqlConnection getRoomsConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand getRoomsCmd = new SqlCommand(getRoomsSQL, getRoomsConnection);
                
                getRoomsConnection.Open();



                
                string roomName = getRoomsCmd.ExecuteScalar().ToString();
                
                


                labelID.Text = "You are editing the facilities for room " + roomName + ". If this is not correct, you can ";

                string listSQL = "SELECT facilityID FROM Facility";

                SqlConnection deleteFacilitiesConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand(listSQL, deleteFacilitiesConnection);
                SqlDataReader reader;
                
                var facilityIDs = new List<string>();
                deleteFacilitiesConnection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {  
                   
                   string tempID = reader["facilityID"].ToString();
                   facilityIDs.Add(tempID);
                }
                reader.Close();


                string listSQL2 = "SELECT facilityID FROM RoomFacilities WHERE roomID ="+roomID;

                SqlConnection deleteFacilitiesConnection2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand cmd2 = new SqlCommand(listSQL2, deleteFacilitiesConnection2);
                SqlDataReader reader2;

                var roomsFacilityIDs = new List<string>();
                deleteFacilitiesConnection2.Open();
                reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {

                    string tempID = reader2["facilityID"].ToString();
                    roomsFacilityIDs.Add(tempID);
                }
                reader2.Close();
                
                var facilitiesTheRoomDoesNotHave = new List<string>();
                facilitiesTheRoomDoesNotHave = (facilityIDs.Except(roomsFacilityIDs)).ToList();
             ///   String joined1 = String.Join(", ", roomsFacilityIDs);
              ///  String joined2 = String.Join(", ", facilitiesTheRoomDoesNotHave);
             ///   labelID2.Text = "The room " + roomName + " has facilities " + joined1 + ". It does not have facilities " + joined2 + ".";
             ///   
                
                var facilitiesIn = new List<string>();
                var facilitiesOut = new List<string>();
                string convertID;
                string convertID2;


                foreach (string value in facilitiesTheRoomDoesNotHave)
                {
                    convertID = "SELECT facilityName FROM Facility WHERE facilityID =" + value;
                    
                    SqlCommand convertCmd = new SqlCommand(convertID, getRoomsConnection);
                    
                    string facilityName = convertCmd.ExecuteScalar().ToString();
                    facilitiesOut.Add(facilityName);
                    

                }

                foreach (string value in roomsFacilityIDs)
                {
                    convertID2 = "SELECT facilityName FROM Facility WHERE facilityID =" + value;
                    
                    SqlCommand convertCmd2 = new SqlCommand(convertID2, getRoomsConnection);
                    
                    string facilityName2 = convertCmd2.ExecuteScalar().ToString();
                    facilitiesIn.Add(facilityName2);
                    

                }
                ///Below was commented out because it is better to just have the facilities in a ddl than have them in text form.
              ///  String joined1 = String.Join(", ", facilitiesIn);
              ///  String joined2 = String.Join(", ", facilitiesOut);
              ///  labelID2.Text = "The room " + roomName + " has facilities " + joined1 + ". It does not have facilities " + joined2 + ".";

                ///Below was used to put the facilities into a drop down list, but it has been commented out because it's better to do it a different way.
             ///   foreach (string value in facilitiesIn) { inDDL.Items.Add(new ListItem(value, "no")); } //Every facility the room has is added to this ddl.

            ///    foreach (string value in facilitiesOut) { outDDL.Items.Add(new ListItem(value, "no")); } //Every facility it doesn't have is added to this ddl.
            ///    

                ///Below, the lists of IDs and names are zipped together.
                var bothLists = roomsFacilityIDs.Zip(facilitiesIn, (i, n) => new { Identifier = i, NameString = n });
                var bothLists2 = facilitiesTheRoomDoesNotHave.Zip(facilitiesOut, (i, n) => new { Identifier = i, NameString = n });

                //The text value of an item in the ddl is the facility name and the actual value is the facility id.
                foreach (var value in bothLists) {

                    string ID = value.Identifier;
                    string name = value.NameString;
                    inDDL.Items.Add(new ListItem(name, ID));
                
                }
                foreach (var value in bothLists2)
                {

                    string ID = value.Identifier;
                    string name = value.NameString;
                    outDDL.Items.Add(new ListItem(name, ID));

                }
            }

        }

        protected void addFacilityFunction(Object sender, EventArgs e)
        {
            string roomID = Session["selectedRoomToEdit"].ToString();
            string selectedFacility = outDDL.SelectedItem.Value;
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect.Open();
            string addFacilityString = "INSERT INTO RoomFacilities VALUES (" + roomID + "," + selectedFacility + ")";
            SqlCommand addFacilityCommand = new SqlCommand(addFacilityString, connect);
            addFacilityCommand.ExecuteNonQuery();
            Response.Redirect(Request.RawUrl);



        }
        protected void removeFacilityFunction(Object sender, EventArgs e) {

            string roomID = Session["selectedRoomToEdit"].ToString();
            string selectedFacility = inDDL.SelectedItem.Value;
            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect2.Open();
            string removeFacilityString = "DELETE FROM RoomFacilities WHERE roomID =" + roomID + " AND facilityID =" + selectedFacility;
            SqlCommand removeFacilityCommand = new SqlCommand(removeFacilityString, connect2);
            removeFacilityCommand.ExecuteNonQuery();
            Response.Redirect(Request.RawUrl);    
                      
        
        }

    }
}
