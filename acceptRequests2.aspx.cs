using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Web.Configuration;
namespace Team11
{

    public partial class acceptRequests2 : System.Web.UI.Page
    {
        int userID = 0;
        int numberOfRooms = 0;
        string tempString = "";
        string room = "";
        string roomID = "";
        string room1 = "", room2 = "", room3 = "", room4 = "";
        string roomID1 = "", roomID2 = "", roomID3 = "", roomID4 = "";
        List<string> roomIDs = new List<string>();
        List<string> roomNames = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(Session["userID"]);
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect.Open();
            string adminString = "Select administrator from [User] where userID = " + userID;
            SqlCommand adminCommand = new SqlCommand(adminString, connect);
            string administratorYesNo = adminCommand.ExecuteScalar().ToString();
            string trimmedAdmin = administratorYesNo.Trim();
            bool userIsAdmin = (trimmedAdmin == "yes");
            if (!userIsAdmin)
            {
                
                tableDiv.InnerHtml = "You are not currently logged in as an admin. If you wish to use this page, you must be logged in as an admin.";
                buttonDiv.InnerHtml = "";
            }
            else
            {
                string allRoomsSQL = "SELECT roomID,roomName FROM Room";

                SqlConnection allRoomsConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                SqlCommand allRoomsCmd = new SqlCommand(allRoomsSQL, allRoomsConnection);
                SqlDataReader allReader;

                try
                {
                    if (!Page.IsPostBack)
                    {

                        listOfRooms.Items.Clear();
                        listOfRooms2.Items.Clear();
                        listOfRooms3.Items.Clear();
                        listOfRooms4.Items.Clear();

                        ListItem newItem = new ListItem();
                        newItem.Text = "Choose a room";
                        newItem.Value = "test";
                        listOfRooms.Items.Add(newItem);
                        listOfRooms2.Items.Add(newItem);
                        listOfRooms3.Items.Add(newItem);
                        listOfRooms4.Items.Add(newItem);

                        allRoomsConnection.Open();
                        allReader = allRoomsCmd.ExecuteReader();
                        while (allReader.Read())
                        {
                            newItem = new ListItem();
                            newItem.Text = allReader["roomName"].ToString();
                            newItem.Value = allReader["roomID"].ToString();
                            listOfRooms.Items.Add(newItem);
                            listOfRooms2.Items.Add(newItem);
                            listOfRooms3.Items.Add(newItem);
                            listOfRooms4.Items.Add(newItem);
                        }
                        allRoomsConnection.Close();
                    }
                }
                catch (Exception err)
                {
                    //TODO
                }
                finally
                {
                    //Blank, may need to contain connection closing
                }
                acceptRequestedRoom.Click += new EventHandler(acceptRequestedRoomFunction);
                changeRequestedRoom.Click += new EventHandler(changeRequestedRoomFunction);
                rejectRequestedRoom.Click += new EventHandler(rejectRequestedRoomFunction);
                string url = Request.Url.Query; ///Gets url
                Match match = Regex.Match(url, @"=(.*)"); ///Gets reference from url
                string reference = match.Groups[1].Value;
                referenceLabel.Text = "You are responding to the request with reference number: " + reference + ".";

                string listSQL = "SELECT * FROM Request WHERE requestID =" + reference;

                SqlConnection Connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                Connection.Open();
                SqlCommand cmd = new SqlCommand(listSQL, Connection);

                SqlDataReader reader; reader = cmd.ExecuteReader();
                string moduleCode = "", status = "", weekID = "", day = "", periodStart = "", periodEnd = "", semester = "", year = "", round = "";

                while (reader.Read())
                {

                    moduleCode = reader["moduleCode"].ToString();
                    status = reader["status"].ToString();
                    weekID = reader["weekID"].ToString();
                    day = reader["day"].ToString();
                    periodStart = reader["periodStart"].ToString();
                    periodEnd = reader["periodEnd"].ToString();
                    semester = reader["semester"].ToString();
                    year = reader["year"].ToString();
                    round = reader["round"].ToString();

                }
                reader.Close();
                string weeks = "";
                string weekSQL = "SELECT * FROM Week WHERE weekID =" + weekID;
                bool week1 = false, week2 = false, week3 = false, week4 = false, week5 = false, week6 = false, week7 = false, week8 = false, week9 = false, week10 = false, week11 = false, week12 = false, week13 = false, week14 = false, week15 = false;
                SqlCommand cmd2 = new SqlCommand(weekSQL, Connection);
                SqlDataReader reader2; reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {

                    if (reader2["week1"].ToString() == "1") { week1 = true; } else { week1 = false; }
                    if (reader2["week2"].ToString() == "1") { week2 = true; } else { week2 = false; }
                    if (reader2["week3"].ToString() == "1") { week3 = true; } else { week3 = false; }
                    if (reader2["week4"].ToString() == "1") { week4 = true; } else { week4 = false; }
                    if (reader2["week5"].ToString() == "1") { week5 = true; } else { week5 = false; }
                    if (reader2["week6"].ToString() == "1") { week6 = true; } else { week6 = false; }
                    if (reader2["week7"].ToString() == "1") { week7 = true; } else { week7 = false; }
                    if (reader2["week8"].ToString() == "1") { week8 = true; } else { week8 = false; }
                    if (reader2["week9"].ToString() == "1") { week9 = true; } else { week9 = false; }
                    if (reader2["week10"].ToString() == "1") { week10 = true; } else { week10 = false; }
                    if (reader2["week11"].ToString() == "1") { week11 = true; } else { week11 = false; }
                    if (reader2["week12"].ToString() == "1") { week12 = true; } else { week12 = false; }
                    if (reader2["week13"].ToString() == "1") { week13 = true; } else { week13 = false; }
                    if (reader2["week14"].ToString() == "1") { week14 = true; } else { week14 = false; }
                    if (reader2["week15"].ToString() == "1") { week15 = true; } else { week15 = false; }

                }
                reader2.Close();
                room = "No preferred room";
                string roomSQL = "SELECT * FROM PreferredRoom WHERE requestID =" + reference;


                SqlCommand cmd3 = new SqlCommand(roomSQL, Connection);
                SqlDataReader reader3; reader3 = cmd3.ExecuteReader();

                while (reader3.Read())
                {
                    roomID = reader3["roomID"].ToString();
                    if (!(roomID == "")) { roomIDs.Add(roomID); }


                }

                reader3.Close();
                numberOfRooms = roomIDs.Count;
                if (numberOfRooms == 1)
                {
                    listOfRooms2.Attributes.Add("disabled", "disabled"); filterRooms2.Enabled = false;
                    listOfRooms3.Attributes.Add("disabled", "disabled"); filterRooms3.Enabled = false;
                    listOfRooms4.Attributes.Add("disabled", "disabled"); filterRooms4.Enabled = false;

                }
                if (numberOfRooms == 2)
                {
                    listOfRooms3.Attributes.Add("disabled", "disabled"); filterRooms3.Enabled = false;
                    listOfRooms4.Attributes.Add("disabled", "disabled"); filterRooms4.Enabled = false;
                }
                if (numberOfRooms == 3)
                {

                    listOfRooms4.Attributes.Add("disabled", "disabled"); filterRooms4.Enabled = false;

                }

                string roomSQL2 = "";
                if (numberOfRooms > 1)
                {

                    if (numberOfRooms == 2) { roomID1 = roomIDs[0]; roomID2 = roomIDs[1]; roomSQL2 = "SELECT * FROM Room WHERE roomID =" + roomID1 + " OR roomID=" + roomID2; }
                    if (numberOfRooms == 3) { roomID1 = roomIDs[0]; roomID2 = roomIDs[1]; roomID3 = roomIDs[2]; roomSQL2 = "SELECT * FROM Room WHERE roomID =" + roomID1 + " OR roomID=" + roomID2 + " OR roomID=" + roomID3; }
                    if (numberOfRooms == 4) { roomID1 = roomIDs[0]; roomID2 = roomIDs[1]; roomID3 = roomIDs[2]; roomID4 = roomIDs[3]; roomSQL2 = "SELECT * FROM Room WHERE roomID =" + roomID1 + " OR roomID=" + roomID2 + " OR roomID=" + roomID3 + " OR roomID=" + roomID4; }

                }
                else
                {
                    roomSQL2 = "SELECT * FROM Room WHERE roomID =" + roomID;
                }
                if (!(roomID == ""))
                {
                    SqlCommand cmd4 = new SqlCommand(roomSQL2, Connection);
                    SqlDataReader reader4; reader4 = cmd4.ExecuteReader();

                    while (reader4.Read())
                    {
                        if (numberOfRooms > 1) {
                        tempString = reader4["roomName"].ToString();
                        roomNames.Add(tempString);
                        
                        }else{
                        room = reader4["roomName"].ToString();
                        }
                        }
                }

                if (numberOfRooms > 1) {
                    if (numberOfRooms == 2)
                    {
                        room1 = roomNames[0];
                        room2 = roomNames[1];

                        room = room1 + " and " + room2;
                    }
                    if (numberOfRooms == 3)
                    {
                        room1 = roomNames[0];
                        room2 = roomNames[1];
                        room3 = roomNames[2];

                        room = room1 + ", " + room2 + " and " + room3;
                    }
                    if (numberOfRooms == 4)
                    {
                        room1 = roomNames[0];
                        room2 = roomNames[1];
                        room3 = roomNames[2];
                        room4 = roomNames[3];

                        room = room1 + ", " + room2 + " ," + room3 + " and " + room4;
                    }
                    
                
                
                }



                if (week1) { weeks += "1, "; }
                if (week2) { weeks += "2, "; }
                if (week3) { weeks += "3, "; }
                if (week4) { weeks += "4, "; }
                if (week5) { weeks += "5, "; }
                if (week6) { weeks += "6, "; }
                if (week7) { weeks += "7, "; }
                if (week8) { weeks += "8, "; }
                if (week9) { weeks += "9, "; }
                if (week10) { weeks += "10, "; }
                if (week11) { weeks += "11, "; }
                if (week12) { weeks += "12, "; }
                if (week13) { weeks += "13, "; }
                if (week14) { weeks += "14, "; }
                if (week15) { weeks += "15, "; }
                if (weeks == "") { weeks += "None"; }
                else
                {
                    weeks = weeks.Remove(weeks.Length - 2, 2);
                }



                tableDiv.InnerHtml = "<table style=\"border: 4px solid black;\"><tr style=\"border: 4px solid black;\">" +
        "<td style=\"border: 4px solid black;\">Module Code</td>" +
        "<td style=\"border: 4px solid black;\">Status</td>" +
        "<td style=\"border: 4px solid black;\">Weeks</td>" +
    "<td style=\"border: 4px solid black;\">Day</td>" +
    "<td style=\"border: 4px solid black;\">Period Start</td>" +
    "<td style=\"border: 4px solid black;\">Period End</td>" +
    "<td style=\"border: 4px solid black;\">Semester</td>" +
    "<td style=\"border: 4px solid black;\">Year</td>" +
    "<td style=\"border: 4px solid black;\">Round</td>" +
    "<td style=\"border: 4px solid black;\">Preferred Room</td>" +
    "<tr style=\"border: 4px solid black;\">" +
    "<td style=\"border: 4px solid black;\">" + moduleCode + "</td>" +
    "<td style=\"border: 4px solid black;\">" + status + "</td>" +
    "<td style=\"border: 4px solid black;\">" + weeks + "</td>" +
    "<td style=\"border: 4px solid black;\">" + day + "</td>" +
    "<td style=\"border: 4px solid black;\">" + periodStart + "</td>" +
    "<td style=\"border: 4px solid black;\">" + periodEnd + "</td>" +
    "<td style=\"border: 4px solid black;\">" + semester + "</td>" +
    "<td style=\"border: 4px solid black;\">" + year + "</td>" +
    "<td style=\"border: 4px solid black;\">" + round + "</td>" +
    "<td style=\"border: 4px solid black;\">" + room + "</td>" +
    "</tr></table>";


            }
        }
        protected void changeRequestedRoomFunction(Object sender, EventArgs e)
        {
            if (numberOfRooms < 2)
            {
                string selectedRoom = listOfRooms.SelectedItem.Value;
                if (selectedRoom == "test")
                {

                    scriptDiv.InnerHtml = "<script>alert(\"You have not selected a room. Please select a room from the drop down list (that is the room that will be booked for this request).\");</script>";

                }
                else
                {
                    string url = Request.Url.Query; ///Gets url
                    Match match = Regex.Match(url, @"=(.*)"); ///Gets reference from url
                    string reference = match.Groups[1].Value;

                    SqlConnection acceptConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                    acceptConnect.Open();
                    string acceptString = "UPDATE Request SET status='Accepted' WHERE requestID=" + reference;
                    SqlCommand acceptCommand = new SqlCommand(acceptString, acceptConnect);
                    acceptCommand.ExecuteNonQuery();
                    string bookRoomSQL = "INSERT INTO BookedRoom VALUES (" + reference + "," + selectedRoom + ")";
                    SqlCommand bookRoomCommand = new SqlCommand(bookRoomSQL, acceptConnect);
                    bookRoomCommand.ExecuteNonQuery();
                    scriptDiv.InnerHtml = "<script>alert(\"Request successfully accepted.\");</script>";
                    Response.Redirect("acceptRequests.aspx");
                }
            }
            else {
                bool roomsHaveBeenSelected = true;
                string bookRoomSQL = "";
                string url = Request.Url.Query; ///Gets url
                Match match = Regex.Match(url, @"=(.*)"); ///Gets reference from url
                string reference = match.Groups[1].Value;
                if (numberOfRooms == 2) {
                    string selectedRoom1 = listOfRooms.SelectedItem.Value;
                    string selectedRoom2 = listOfRooms2.SelectedItem.Value;
                    if (selectedRoom1 == "test" || selectedRoom2 == "test") { roomsHaveBeenSelected = false; }
                    else { 
                    bookRoomSQL = "INSERT INTO BookedRoom VALUES (" + reference + "," + selectedRoom1 +"),(" + reference + "," + selectedRoom2 + ")";
                    
                    }
                
                }
                if (numberOfRooms == 3)
                {
                    string selectedRoom1 = listOfRooms.SelectedItem.Value;
                    string selectedRoom2 = listOfRooms2.SelectedItem.Value;
                    string selectedRoom3 = listOfRooms3.SelectedItem.Value;
                    if (selectedRoom1 == "test" || selectedRoom2 == "test"|| selectedRoom3=="test") { roomsHaveBeenSelected = false; }
                    else
                    {
                        bookRoomSQL = "INSERT INTO BookedRoom VALUES (" + reference + "," + selectedRoom1 + "),(" + reference + "," + selectedRoom2 + "),(" + reference + "," + selectedRoom3 + ")";

                    }

                }
                if (numberOfRooms == 4)
                {
                    string selectedRoom1 = listOfRooms.SelectedItem.Value;
                    string selectedRoom2 = listOfRooms2.SelectedItem.Value;
                    string selectedRoom3 = listOfRooms3.SelectedItem.Value;
                    string selectedRoom4 = listOfRooms4.SelectedItem.Value;
                    if (selectedRoom1 == "test" || selectedRoom2 == "test" || selectedRoom3 == "test"||selectedRoom4=="test") { roomsHaveBeenSelected = false; }
                    else
                    {
                        bookRoomSQL = "INSERT INTO BookedRoom VALUES (" + reference + "," + selectedRoom1 + "),(" + reference + "," + selectedRoom2 + "),(" + reference + "," + selectedRoom3 + "),(" + reference + "," + selectedRoom4 + ")";

                    }

                }
                if (roomsHaveBeenSelected)
                {
                    SqlConnection acceptConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                    acceptConnect.Open();
                    string acceptString = "UPDATE Request SET status='Accepted' WHERE requestID=" + reference;
                    SqlCommand acceptCommand = new SqlCommand(acceptString, acceptConnect);
                    acceptCommand.ExecuteNonQuery();
                    SqlCommand bookRoomCommand = new SqlCommand(bookRoomSQL, acceptConnect);
                    bookRoomCommand.ExecuteNonQuery();
                    scriptDiv.InnerHtml = "<script>alert(\"Request successfully accepted.\");</script>";
                    Response.Redirect("acceptRequests.aspx");
                }
                else { scriptDiv.InnerHtml = "<script>alert(\"Since the request was for " + numberOfRooms.ToString() + " rooms, you must allocate exactly "+numberOfRooms.ToString() + " rooms.\");</script>"; }
            
            }
        }
        protected void acceptRequestedRoomFunction(Object sender, EventArgs e)
        {

            if (room == "No preferred room")
            {

                scriptDiv.InnerHtml = "<script>alert(\"You cannot accept a request that does not have a preferred room. Please click 'Accept request but assign a different room'.\");</script>";


            }
            else
            {
                string bookRoomSQL = "";
                string url = Request.Url.Query; ///Gets url
                Match match = Regex.Match(url, @"=(.*)"); ///Gets reference from url
                string reference = match.Groups[1].Value;

                SqlConnection acceptConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
                acceptConnect.Open();
                string acceptString = "UPDATE Request SET status='Accepted' WHERE requestID=" + reference;
                SqlCommand acceptCommand = new SqlCommand(acceptString, acceptConnect);
                acceptCommand.ExecuteNonQuery();
                if (numberOfRooms == 1)
                {
                    bookRoomSQL = "INSERT INTO BookedRoom VALUES (" + reference + "," + roomID + ")";
                }


                if (numberOfRooms == 2) {
                    bookRoomSQL = "INSERT INTO BookedRoom VALUES (" + reference + "," + roomID1 + "),(" + reference + ", " + roomID2 + ")";            
                
                }
                if (numberOfRooms == 3)
                {
                    bookRoomSQL = "INSERT INTO BookedRoom VALUES (" + reference + "," + roomID1 + "),(" + reference + ", " + roomID2 + "),(" + reference + ", " + roomID3 + ")";

                }
                if (numberOfRooms == 4)
                {
                    bookRoomSQL = "INSERT INTO BookedRoom VALUES (" + reference + "," + roomID1 + "),(" + reference + ", " + roomID2 + "),(" + reference + ", " + roomID3 + "),(" + reference + ", " + roomID4 + ")";

                }




                SqlCommand bookRoomCommand = new SqlCommand(bookRoomSQL, acceptConnect);
                bookRoomCommand.ExecuteNonQuery();
                scriptDiv.InnerHtml = "<script>alert(\"Request successfully accepted.\");</script>";
                Response.Redirect("acceptRequests.aspx");
            }
        }
        protected void rejectRequestedRoomFunction(Object sender, EventArgs e)
        {
            string url = Request.Url.Query; ///Gets url
            Match match = Regex.Match(url, @"=(.*)"); ///Gets reference from url
            string reference = match.Groups[1].Value;

            SqlConnection rejectConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            rejectConnect.Open();
            string rejectString = "UPDATE Request SET status='Rejected' WHERE requestID=" + reference;
            SqlCommand rejectCommand = new SqlCommand(rejectString, rejectConnect);
            rejectCommand.ExecuteNonQuery();
            scriptDiv.InnerHtml = "<script>alert(\"Request successfully rejected.\");</script>";
            Response.Redirect("acceptRequests.aspx");
        }




    }
}
