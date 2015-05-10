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
    public partial class CreateRequest : System.Web.UI.Page
    {

        int userID = 0; int round = 0;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string roundLabelSQL = "SELECT * FROM Rounds";
            SqlConnection roundLabelConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            SqlCommand roundLabelCmd = new SqlCommand(roundLabelSQL, roundLabelConnection);
            SqlDataReader roundLabelReader;
            roundLabelConnection.Open();
            roundLabelReader = roundLabelCmd.ExecuteReader();
            DateTime databaseDate = DateTime.Now;
            while (roundLabelReader.Read())
            {
                round = Convert.ToInt32(roundLabelReader["round"]);
                databaseDate = Convert.ToDateTime(roundLabelReader["dateToAdvance"]);

            }

            DateTime theDateNow = DateTime.Now;
            double days = (databaseDate.Subtract(theDateNow).TotalDays);
            double daysRoundedUp = Math.Ceiling(days);
            string roundString = "View Requests: (Current round: " + round + " - Days until next round: " + daysRoundedUp + ")";
            countdownLabel2.Text = roundString;

            // read the userid from the querystring
            userID = Convert.ToInt32(Session["userID"]);
            string fillDeptName = string.Format("SELECT deptName from [User] where userID={0}", Session["userID"]);
            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect2.Open();

            SqlCommand getdeptsql = new SqlCommand(fillDeptName, connect2);
            SqlDataReader getdeptdata = getdeptsql.ExecuteReader();

            while (getdeptdata.Read())
            {
                string deptInnerName = getdeptdata.GetString(0);
                deptName.InnerText = deptInnerName.ToString();
            }

            connect2.Close();

            
            if (!IsPostBack)
            {   // if this is the initial page load, then ...

                // populate the list of module codes as "code : name"
                SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                PopulateModuleList(connect);

                // read the user's preferences and then set up the display accordingly 
                connect.Open();
                string preferencesquery     = String.Format("SELECT period, hr24Format, defaultLocation FROM [Preferences] WHERE userID={0}", userID);
                SqlCommand preferencessql   = new SqlCommand(preferencesquery, connect);
                SqlDataReader preferences   = preferencessql.ExecuteReader();
                if (preferences.Read())
                {
                    /* Sets variables to retrieved data */
                    int periodText              = preferences.GetInt32(0);
                    int hr24FormatText          = preferences.GetInt32(1);
                    string defaultLocationText  = preferences.GetString(2);

                    //Sets default location
                    switch (defaultLocationText)
                    {
                        case "Central":
                            RadioButtonListParks.SelectedIndex = 0;
                            break;
                        case "East":
                            RadioButtonListParks.SelectedIndex = 1;
                            break;
                        case "West":
                            RadioButtonListParks.SelectedIndex = 2;
                            break;
                    }

                    //Set table column headers for periods
                    if (periodText == 1)
                    {   // if periods should be labelled by name ..
                        ButtonPeriod1.Text = "Period 1";
                        ButtonPeriod2.Text = "Period 2";
                        ButtonPeriod3.Text = "Period 3";
                        ButtonPeriod4.Text = "Period 4";
                        ButtonPeriod5.Text = "Period 5";
                        ButtonPeriod6.Text = "Period 6";
                        ButtonPeriod7.Text = "Period 7";
                        ButtonPeriod8.Text = "Period 8";
                        ButtonPeriod9.Text = "Period 9";
                    }
                    else
                    {   // else they will be labelled by time ..
                        ButtonPeriod1.Text = "09:00 - 09:50";
                        ButtonPeriod2.Text = "10:00 - 10:50";
                        ButtonPeriod3.Text = "11:00 - 11:50";
                        ButtonPeriod4.Text = "12:00 - 12:50";

                        if (hr24FormatText == 1)
                        {   // if 24 hour format ...
                            ButtonPeriod5.Text = "13:00 - 13:50";
                            ButtonPeriod6.Text = "14:00 - 14:50";
                            ButtonPeriod7.Text = "15:00 - 15:50";
                            ButtonPeriod8.Text = "16:00 - 16:50";
                            ButtonPeriod9.Text = "17:00 - 17:50";
                        }
                        else
                        {
                            ButtonPeriod5.Text = "01:00 - 01:50";
                            ButtonPeriod6.Text = "02:00 - 02:50";
                            ButtonPeriod7.Text = "03:00 - 03:50";
                            ButtonPeriod8.Text = "04:00 - 04:50";
                            ButtonPeriod9.Text = "05:00 - 05:50";
                        }
                    }
                }
                connect.Close();

                // clear the room list button
                RadioButtonListParks_SelectedIndexChanged(null, null);
            }
        }

        private void PopulateModuleList(SqlConnection connect)
        {
            DropDownListModules.Items.Clear();
            connect.Open();
            string modulesql = String.Format(@"
SELECT moduleCode, moduleTitle 
FROM [Module] 
WHERE userID={0}", userID);
            if (DropDownListPart.SelectedIndex > 0)
            {

                modulesql += "AND Substring (moduleCode, 3,1 ) = '" + DropDownListPart.SelectedItem.Text + "'";
            }

            SqlCommand modulecommand = new SqlCommand(modulesql, connect);
            SqlDataReader modules = modulecommand.ExecuteReader();

            while (modules.Read())
            {
                string modulecode = modules.GetString(0);
                string modulename = modules.GetString(1);
                string module = String.Format("{0} : {1}", modulecode, modulename);
                DropDownListModules.Items.Add(module);

            }
            connect.Close();
        }

        /// <summary>
        /// Searches the rooms.
        /// </summary>
        public void RebuildListOfRooms()
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            conn.Open();

            //Clear the dropdownlists showing the rooms at every call of the search function.
            DropDownListRooms.Items.Clear();
            DropDownListRoomsAlt.Items.Clear();
            DropDownListRooms.Items.Add("Please select");
            DropDownListRoomsAlt.Items.Add("Please select");

            // Get the information from the page content
            int buildingID = 0;
            string building = DropDownListBuildings.Text;

            if (building != "")
            {
                string buildingidquery = String.Format("Select buildingID from [Building] where buildingName='{0}'", building);
                SqlCommand buildingcommand = new SqlCommand(buildingidquery, conn);
                buildingID = Convert.ToInt32(buildingcommand.ExecuteScalar());
            }

            string roomtype = RadioButtonListRoomType.Text;
            if (roomtype == "Lecture")
            { roomtype = "1"; }
            else if (roomtype == "Seminar")
            { roomtype = "2"; }
            else if (roomtype == "Either")
            {
                //roomtype = "1 OR facilityID = 2";
                roomtype = "";
            }

            string arrangement = RadioButtonListArrangement.Text;
            if (arrangement == "Tiered")
            { arrangement = "3"; }
            else if (arrangement == "Flat")
            { arrangement = "4"; }
            else if (arrangement == "Either")
            {
                //arrangement = "3 OR facilityID = 4";
                arrangement = "";
            }

            string projector = RadioButtonListProjector.Text;
            if (projector == "Data Projector")
            { projector = "5"; }
            else if (projector == "Double Projector")
            { projector = "6"; }
            else if (projector == "Either")
            {
                //projector = "5 OR (facilityID = 6)";
                projector = "";
            }
            string board = "";
            string boardtwo = "";
            string wheel = "";
            string visualiser = "";
            string computer = "";
            if (CheckBoxCB.Checked == true)
            { board = "7"; }
            else board = "";

            if (CheckBoxWB.Checked == true)
            { boardtwo = "8"; }
            else boardtwo = "";

            int wheeli = RadioButtonListWheelchair.SelectedIndex;
            int visualiseri = RadioButtonListVisualiser.SelectedIndex;
            int computeri = RadioButtonListComputer.SelectedIndex;
            
            if (wheeli == 0)
                wheel = "9";
            else
                wheel = "";

            if (visualiseri == 0)
                visualiser = "10";
            else
                visualiser = "";

            if (computeri == 0)
                computer = "11";
            else
                computer = "";

            // Make SQL addition for if any facilities have been selected
            string facilitysql = "";
            if (roomtype != "")
                facilitysql += " AND RoomID IN (SELECT DISTINCT RoomID FROM [RoomFacilities] WHERE facilityID = " + roomtype + ")";
            if (arrangement != "")
                facilitysql += " AND RoomID IN (SELECT DISTINCT RoomID FROM [RoomFacilities] WHERE facilityID = " + arrangement + ")";
            if (projector != "")
                facilitysql += " AND RoomID IN (SELECT DISTINCT RoomID FROM [RoomFacilities] WHERE facilityID = " + projector + ")";
            if (wheel != "")
                facilitysql += " AND RoomID IN (SELECT DISTINCT RoomID FROM [RoomFacilities] WHERE facilityID = " + wheel + ")";
            if (visualiser != "")
                facilitysql += " AND RoomID IN (SELECT DISTINCT RoomID FROM [RoomFacilities] WHERE facilityID = " + visualiser + ")";
            if (computer != "")
                facilitysql += " AND RoomID IN (SELECT DISTINCT RoomID FROM [RoomFacilities] WHERE facilityID = " + computer + ")";
            if (board != "")
                facilitysql += " AND RoomID IN (SELECT DISTINCT RoomID FROM [RoomFacilities] WHERE facilityID = " + board + ")";
            if (boardtwo != "")
                facilitysql += " AND RoomID IN (SELECT DISTINCT RoomID FROM [RoomFacilities] WHERE facilityID = " + boardtwo + ")";
            if (building != "")
                facilitysql += " AND [Building].buildingID=" + buildingID;

            string roomquery = "";
            
            if (TextBoxCapacity.Text != "")
            {   // adjust if there are capacity restructions
                int number = 0;
                bool isNumeric = int.TryParse(TextBoxCapacity.Text, out number);
                if (isNumeric)
                {
                    roomquery = @"
SELECT roomName 
FROM [Room] 
LEFT JOIN [Building] ON [Room].buildingID = [Building].buildingID 
LEFT JOIN [Park] ON [Building].parkID = [Park].parkID 
WHERE [Room].capacity >='" + TextBoxCapacity.Text + "' AND [Park].parkName ='" + RadioButtonListParks.Text + "'";
                }
                else
                    roomquery = @"
SELECT roomName 
FROM [Room] 
LEFT JOIN [Building] ON [Room].buildingID = [Building].buildingID 
LEFT JOIN [Park] ON [Building].parkID = [Park].parkID 
WHERE [Park].parkName ='" + RadioButtonListParks.Text + "'";
            }
            else
                roomquery = @"
SELECT roomName 
FROM [Room] 
LEFT JOIN [Building] ON [Room].buildingID = [Building].buildingID 
LEFT JOIN [Park] ON [Building].parkID = [Park].parkID 
WHERE [Park].parkName ='" + RadioButtonListParks.Text + "' AND [Building].buildingName = '" + DropDownListBuildings.SelectedItem + "'";

            // Add facilities information if any were selected
            if (facilitysql != "")
                roomquery += facilitysql;

            // build the list of rooms into both the room and alt reoom drop down lists
            SqlCommand roomsql = new SqlCommand(roomquery, conn);
            SqlDataReader rooms = roomsql.ExecuteReader();
            while (rooms.Read())
            {
                DropDownListRooms.Items.Add(rooms.GetString(0).ToString());
                DropDownListRoomsAlt.Items.Add(rooms.GetString(0).ToString());
            }
            conn.Close();
        }

        /// <summary>
        /// Handles the Click event of the Button1 control to submit the request
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            //VALIDATION
            errorMessage.Text = "";
            int number = 0;
            bool isNumeric = int.TryParse(TextBoxCapacity.Text, out number);
            if (isNumeric)
            {
                if (Convert.ToInt32(TextBoxCapacity.Text) > 0)
                {
                    if (!((LabelRoom3.Text == "None") && (LabelRoom2.Text == "None") && (LabelRoom1.Text == "None")))
                    {
                        if (Week1.Checked || Week2.Checked || Week3.Checked || Week4.Checked || Week5.Checked || Week6.Checked || Week7.Checked || Week8.Checked || Week9.Checked || Week10.Checked || Week11.Checked || Week12.Checked || Week13.Checked || Week14.Checked || Week15.Checked)
                        {   // if any week is checked (ticked) ...
                            if (CheckBoxM1.Checked || CheckBoxM2.Checked || CheckBoxM3.Checked || CheckBoxM4.Checked || CheckBoxM5.Checked || CheckBoxM6.Checked || CheckBoxM7.Checked || CheckBoxM8.Checked || CheckBoxM9.Checked ||
                                CheckBoxT1.Checked || CheckBoxT2.Checked || CheckBoxT3.Checked || CheckBoxT4.Checked || CheckBoxT5.Checked || CheckBoxT6.Checked || CheckBoxT7.Checked || CheckBoxT8.Checked || CheckBoxT9.Checked ||
                                CheckBoxW1.Checked || CheckBoxW2.Checked || CheckBoxW3.Checked || CheckBoxW4.Checked || CheckBoxW5.Checked || CheckBoxW6.Checked || CheckBoxW7.Checked || CheckBoxW8.Checked || CheckBoxW9.Checked ||
                                CheckBoxJ1.Checked || CheckBoxJ2.Checked || CheckBoxJ3.Checked || CheckBoxJ4.Checked || CheckBoxJ5.Checked || CheckBoxJ6.Checked || CheckBoxJ7.Checked || CheckBoxJ8.Checked || CheckBoxJ9.Checked ||
                                CheckBoxF1.Checked || CheckBoxF2.Checked || CheckBoxF3.Checked || CheckBoxF4.Checked || CheckBoxF5.Checked || CheckBoxF6.Checked || CheckBoxF7.Checked || CheckBoxF8.Checked || CheckBoxF9.Checked
                                )
                            {   // if any weekday period is checked

                                /* Get the module code from the selected module title */
                                string moduleTitleText = DropDownListModules.SelectedValue;
                                string moduleCodeText = "";
                                int charindex = moduleTitleText.IndexOf(":") - 1;
                                if (charindex > 0)
                                {
                                    moduleCodeText = moduleTitleText.Substring(0, charindex);
                                }


                                int semesterText = 2;

                                /*Initialise week variables*/
                                int week1 = (Week1.Checked == true) ? 1 : 0;
                                int week2 = (Week2.Checked == true) ? 1 : 0;
                                int week3 = (Week3.Checked == true) ? 1 : 0;
                                int week4 = (Week4.Checked == true) ? 1 : 0;
                                int week5 = (Week5.Checked == true) ? 1 : 0;
                                int week6 = (Week6.Checked == true) ? 1 : 0;
                                int week7 = (Week7.Checked == true) ? 1 : 0;
                                int week8 = (Week8.Checked == true) ? 1 : 0;
                                int week9 = (Week9.Checked == true) ? 1 : 0;
                                int week10 = (Week10.Checked == true) ? 1 : 0;
                                int week11 = (Week11.Checked == true) ? 1 : 0;
                                int week12 = (Week12.Checked == true) ? 1 : 0;
                                int week13 = (Week13.Checked == true) ? 1 : 0;
                                int week14 = (Week14.Checked == true) ? 1 : 0;
                                int week15 = (Week15.Checked == true) ? 1 : 0;

                                /* Find a weekID relating to week selection */
                                int weekIDText;
                                string weekquery = String.Format(@"
                                    SELECT COUNT(weekID) 
                                    FROM [Week] 
                                    WHERE week1= {0}
                                    AND week2= {1} 
                                    AND week3= {2} 
                                    AND week4= {3} 
                                    AND week5= {4} 
                                    AND week6= {5} 
                                    AND week7= {6} 
                                    AND week8= {7} 
                                    AND week9= {8} 
                                    AND week10= {9} 
                                    AND week11= {10} 
                                    AND week12= {11} 
                                    AND week13= {12} 
                                    AND week14= {13} 
                                    AND week15= {14}",
                                week1,week2,week3,week4,week5,week6,week7,week8,week9,week10,week11,week12,week13,week14,week15);
                                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                connection.Open();
                                SqlCommand weeksql = new SqlCommand(weekquery, connection);
                                int testtwo = Convert.ToInt32(weeksql.ExecuteScalar().ToString());
                                connection.Close();

                                /* If there is a corresponding weekID in database, select it */
                                if (testtwo != 0)
                                {
                                    string weekquery2 = "SELECT weekID FROM [Week] WHERE week1= " + week1 + " AND week2= " + week2 + " AND week3= " + week3 + " AND week4= " + week4 + " AND week5= " + week5 + " AND week6= " + week6 + " AND week7= " + week7 + " AND week8= " + week8 + " AND week9= " + week9 + " AND week10= " + week10 + " AND week11= " + week11 + " AND week12= " + week12 + " AND week13= " + week13 + " AND week14= " + week14 + " AND week15= " + week15;
                                    SqlConnection connection2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                    connection2.Open();
                                    SqlCommand weeksql2 = new SqlCommand(weekquery2, connection2);
                                    weekIDText = Convert.ToInt32(weeksql2.ExecuteScalar().ToString());
                                    connection2.Close();
                                }
                                /* If there is no corresponding weekID, make a new one and use that */
                                else
                                {
                                    string insweekquery = "INSERT INTO [Week] VALUES(" + week1 + "," + week2 + "," + week3 + "," + week4 + "," + week5 + "," + week6 + "," + week7 + "," + week8 + "," + week9 + "," + week10 + "," + week11 + "," + week12 + "," + week13 + "," + week14 + "," + week15 + ")";
                                    SqlConnection conne = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                    conne.Open();
                                    SqlCommand insweeksql = new SqlCommand(insweekquery, conne);
                                    insweeksql.ExecuteNonQuery();
                                    conne.Close();
                                    string newweek = "SELECT MAX(weekID) FROM [Week]";
                                    SqlConnection connec = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                    connec.Open();
                                    SqlCommand maxweeksql = new SqlCommand(newweek, connec);
                                    weekIDText = Convert.ToInt32(maxweeksql.ExecuteScalar().ToString());
                                    connec.Close();
                                }

                                /* Make  an array for each day storing each period */
                                CheckBox[,] weekdayCheckBoxes = { { CheckBoxM1, CheckBoxM2, CheckBoxM3, CheckBoxM4, CheckBoxM5, CheckBoxM6, CheckBoxM7, CheckBoxM8, CheckBoxM9 }, 
                                                                    {CheckBoxT1, CheckBoxT2, CheckBoxT3, CheckBoxT4, CheckBoxT5, CheckBoxT6, CheckBoxT7, CheckBoxT8, CheckBoxT9 },
                                                                    {CheckBoxW1, CheckBoxW2, CheckBoxW3, CheckBoxW4, CheckBoxW5, CheckBoxW6, CheckBoxW7, CheckBoxW8, CheckBoxW9  },
                                                                    {CheckBoxJ1, CheckBoxJ2, CheckBoxJ3, CheckBoxJ4, CheckBoxJ5, CheckBoxJ6, CheckBoxJ7, CheckBoxJ8, CheckBoxJ9  },
                                                                    {CheckBoxM1, CheckBoxF2, CheckBoxF3, CheckBoxF4, CheckBoxF5, CheckBoxF6, CheckBoxF7, CheckBoxF8, CheckBoxF9  }};

                                /*Make a list of every selected room*/
                                int numberOfRooms = 0;
                                int numberAltRooms = 0;

                                string label = "None";
                                bool l1 = LabelRoom1.Text.Equals(label);
                                bool l2 = LabelRoom2.Text.Equals(label);
                                bool l3 = LabelRoom3.Text.Equals(label);
                                if (!l1) { numberOfRooms++; }
                                if (!l2) { numberOfRooms++; }
                                if (!l3) { numberOfRooms++; }
                                string roomname = LabelRoom1.Text;
                                string roomname2 = LabelRoom2.Text;
                                string roomname3 = LabelRoom3.Text;

                                bool l1alt = LabelRoomAlt1.Text.Equals(label);
                                bool l2alt = LabelRoomAlt2.Text.Equals(label);
                                bool l3alt = LabelRoomAlt3.Text.Equals(label);
                                if (!l1alt) { numberAltRooms++; }
                                if (!l2alt) { numberAltRooms++; }
                                if (!l3alt) { numberAltRooms++; }
                                string roomnamealt = LabelRoomAlt1.Text;
                                string roomname2alt = LabelRoomAlt2.Text;
                                string roomname3alt = LabelRoomAlt3.Text;
                                int roomid1Alt;
                                int roomid2Alt;
                                int roomid3Alt;
                                List<int> roomlistAlt = new List<int>();

                                int roomid;
                                int roomid2;
                                int roomid3;

                                List<int> roomlist = new List<int>();
                                if (numberOfRooms == 1)
                                {
                                    if (roomname != "")
                                    {
                                        string getroomid = "SELECT roomID FROM [Room] WHERE roomName='" + roomname + "'";
                                        SqlConnection connection7 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection7.Open();
                                        SqlCommand getroomidsql = new SqlCommand(getroomid, connection7);
                                        roomid = Convert.ToInt32(getroomidsql.ExecuteScalar().ToString());
                                        connection7.Close();
                                        roomlist.Add(roomid);
                                    }
                                }
                                else if (numberOfRooms == 2)
                                {
                                    if (roomname != "")
                                    {
                                        string getroomid = "SELECT roomID FROM [Room] WHERE roomName='" + roomname + "'";
                                        SqlConnection connection7 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection7.Open();
                                        SqlCommand getroomidsql = new SqlCommand(getroomid, connection7);
                                        roomid = Convert.ToInt32(getroomidsql.ExecuteScalar().ToString());
                                        connection7.Close();
                                        roomlist.Add(roomid);
                                    }
                                    if (roomname2 != "")
                                    {
                                        string getroomid2 = "SELECT roomID FROM [Room] WHERE roomName='" + roomname2 + "'";
                                        SqlConnection connection72 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection72.Open();
                                        SqlCommand getroomidsql2 = new SqlCommand(getroomid2, connection72);
                                        roomid2 = Convert.ToInt32(getroomidsql2.ExecuteScalar().ToString());
                                        connection72.Close();
                                        roomlist.Add(roomid2);
                                    }
                                }
                                else if (numberOfRooms == 3)
                                {
                                    if (roomname != "")
                                    {
                                        string getroomid = "SELECT roomID FROM [Room] WHERE roomName='" + roomname + "'";
                                        SqlConnection connection7 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection7.Open();
                                        SqlCommand getroomidsql = new SqlCommand(getroomid, connection7);
                                        roomid = Convert.ToInt32(getroomidsql.ExecuteScalar().ToString());
                                        connection7.Close();
                                        roomlist.Add(roomid);
                                    }
                                    if (roomname2 != "")
                                    {
                                        string getroomid2 = "SELECT roomID FROM [Room] WHERE roomName='" + roomname2 + "'";
                                        SqlConnection connection72 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection72.Open();
                                        SqlCommand getroomidsql2 = new SqlCommand(getroomid2, connection72);
                                        roomid2 = Convert.ToInt32(getroomidsql2.ExecuteScalar().ToString());
                                        connection72.Close();
                                        roomlist.Add(roomid2);
                                    }
                                    if (roomname3 != "")
                                    {
                                        string getroomid3 = "SELECT roomID FROM [Room] WHERE roomName='" + roomname3 + "'";
                                        SqlConnection connection73 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection73.Open();
                                        SqlCommand getroomidsql3 = new SqlCommand(getroomid3, connection73);
                                        roomid3 = Convert.ToInt32(getroomidsql3.ExecuteScalar().ToString());
                                        connection73.Close();
                                        roomlist.Add(roomid3);
                                    }
                                }
                                /*Makes a list of all the facilityIDs selected*/
                                string board = "";
                                string boardtwo = "";
                                int roomtype = RadioButtonListRoomType.SelectedIndex;
                                int arrangement = RadioButtonListArrangement.SelectedIndex;
                                int projector = RadioButtonListProjector.SelectedIndex;
                                if (CheckBoxCB.Checked == true)
                                    board = "ChalkBoard";
                                if (CheckBoxWB.Checked == true)
                                    boardtwo = "WhiteBoard";
                                string wheel = RadioButtonListWheelchair.SelectedValue;
                                string visualiser = RadioButtonListVisualiser.SelectedValue;
                                string computer = RadioButtonListComputer.SelectedValue;
                                string reqfac = "";
                                List<int> list = new List<int>();
                                if (roomtype != -1)
                                    list.Add(roomtype + 1);
                                if (arrangement != -1)
                                    list.Add(arrangement + 3);
                                if (projector != -1)
                                    list.Add(projector + 5);
                                if (wheel == "Yes")
                                    list.Add(9);
                                if (visualiser == "Yes")
                                    list.Add(10);
                                if (computer == "Yes")
                                    list.Add(11);
                                if (board != "")
                                    list.Add(7);
                                if (boardtwo != "")
                                    list.Add(8);

                                /* Cycle through each array and, if any were selected, create a request with that data */
                                /* The while loops find any multiple period requests */
                                //bool[] availableDays = new bool[32] {mondayrequest,tuesdayrequest,wednesdayrequest,thursdayrequest,fridayrequest};
                                string[] dayName = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                                for (int day = 0; day < 5; day++)
                                {
                                    for (int period = 0; period < 9; period++)
                                    {
                                        if (weekdayCheckBoxes[day, period].Checked)
                                        {
                                            int startTime = period + 1;
                                            int duration = 0;
                                            int n = period - 1;
                                            bool ended = true;
                                            while (ended)
                                            {
                                                n++;
                                                if (weekdayCheckBoxes[day, n].Checked 
                                                    && n < 8 // only check the next period if there is a next period (i.e.: we are not at the last period)
                                                    && weekdayCheckBoxes[day, n + 1].Checked)
                                                {
                                                        duration++;
                                                        period++;
                                                }
                                                else
                                                {
                                                    int endTime = startTime + duration;
                                                    // determine request status based on whether it is private or not
                                                    string status = "";
                                                    //TODO: figure out how to determine if it is private or not
                                                    bool isPrivateRoom = false;
                                                    if (isPrivateRoom)
                                                        status = "Accepted";
                                                    else
                                                        status = "Pending";
                                                    string insreq = "INSERT INTO [Request] VALUES ('" + moduleCodeText + "','" + status + "'," + weekIDText + ",'" + dayName[day] + "'," + startTime + "," + endTime + "," + semesterText + ","+DateTime.Now.Year.ToString()+","+round+")";
                                                    SqlConnection connection6 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                                    connection6.Open();
                                                    SqlCommand insreqsql = new SqlCommand(insreq, connection6);
                                                    insreqsql.ExecuteNonQuery();
                                                    connection6.Close();
                                                    string booked = "";
                                                    if (roomlist.Count != 0)
                                                    {
                                                        SqlConnection conn2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                                        conn2.Open();
                                                        for (int y = 0; y < roomlist.Count; y++)
                                                        {
                                                            booked = "INSERT INTO [PreferredRoom] (requestID,roomID) VALUES ((SELECT MAX(requestID) FROM [Request])," + roomlist[y] + ")";
                                                            SqlCommand bookedsql = new SqlCommand(booked, conn2);
                                                            bookedsql.ExecuteNonQuery();
                                                        }

                                                        conn2.Close();
                                                    }
                                                    else
                                                    {
                                                        SqlConnection conn4 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                                        conn4.Open();
                                                        for (int x = 0; x < numberOfRooms; x++)
                                                        {
                                                            booked = "INSERT INTO [PreferredRoom] (requestID,roomID) VALUES ((SELECT MAX(requestID) FROM [Request]),(SELECT roomID FROM [Room] WHERE roomName='" + DropDownListRooms.Items[x].Value + "'))";
                                                            SqlCommand bookedsql = new SqlCommand(booked, conn4);
                                                            bookedsql.ExecuteNonQuery();
                                                        }
                                                        conn4.Close();
                                                    }
                                                    if (list.Count != 0)
                                                    {
                                                        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                                        conn.Open();
                                                        for (int z = 0; z < list.Count; z++)
                                                        {
                                                            reqfac = "INSERT INTO [RequestFacilities] VALUES ((SELECT MAX(requestID) FROM [Request])," + list[z] + ")";
                                                            SqlCommand reqfacsql = new SqlCommand(reqfac, conn);
                                                            reqfacsql.ExecuteNonQuery();
                                                        }
                                                        conn.Close();
                                                    }
                                                    ended = false;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (numberAltRooms == 1)
                                {
                                    if (roomnamealt != "")
                                    {
                                        string getroomid = "SELECT roomID FROM [Room] WHERE roomName='" + roomnamealt + "'";
                                        SqlConnection connection7 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection7.Open();
                                        SqlCommand getroomidsql = new SqlCommand(getroomid, connection7);
                                        roomid1Alt = Convert.ToInt32(getroomidsql.ExecuteScalar().ToString());
                                        connection7.Close();
                                        roomlistAlt.Add(roomid1Alt);
                                    }
                                }
                                else if (numberAltRooms == 2)
                                {
                                    if (roomnamealt != "")
                                    {
                                        string getroomid = "SELECT roomID FROM [Room] WHERE roomName='" + roomnamealt + "'";
                                        SqlConnection connection7 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection7.Open();
                                        SqlCommand getroomidsql = new SqlCommand(getroomid, connection7);
                                        roomid1Alt = Convert.ToInt32(getroomidsql.ExecuteScalar().ToString());
                                        connection7.Close();
                                        roomlistAlt.Add(roomid1Alt);
                                    }
                                    if (roomname2alt != "")
                                    {
                                        string getroomid2 = "SELECT roomID FROM [Room] WHERE roomName='" + roomname2alt + "'";
                                        SqlConnection connection72 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection72.Open();
                                        SqlCommand getroomidsql2 = new SqlCommand(getroomid2, connection72);
                                        roomid2Alt = Convert.ToInt32(getroomidsql2.ExecuteScalar().ToString());
                                        connection72.Close();
                                        roomlistAlt.Add(roomid2Alt);
                                    }
                                }
                                else if (numberAltRooms == 3)
                                {
                                    if (roomnamealt != "")
                                    {
                                        string getroomid = "SELECT roomID FROM [Room] WHERE roomName='" + roomnamealt + "'";
                                        SqlConnection connection7 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection7.Open();
                                        SqlCommand getroomidsql = new SqlCommand(getroomid, connection7);
                                        roomid1Alt = Convert.ToInt32(getroomidsql.ExecuteScalar().ToString());
                                        connection7.Close();
                                        roomlistAlt.Add(roomid1Alt);
                                    }
                                    if (roomname2alt != "")
                                    {
                                        string getroomid2 = "SELECT roomID FROM [Room] WHERE roomName='" + roomname2alt + "'";
                                        SqlConnection connection72 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection72.Open();
                                        SqlCommand getroomidsql2 = new SqlCommand(getroomid2, connection72);
                                        roomid2Alt = Convert.ToInt32(getroomidsql2.ExecuteScalar().ToString());
                                        connection72.Close();
                                        roomlistAlt.Add(roomid2Alt);
                                    }
                                    if (roomname3alt != "")
                                    {
                                        string getroomid3 = "SELECT roomID FROM [Room] WHERE roomName='" + roomname3alt + "'";
                                        SqlConnection connection73 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                                        connection73.Open();
                                        SqlCommand getroomidsql3 = new SqlCommand(getroomid3, connection73);
                                        roomid3Alt = Convert.ToInt32(getroomidsql3.ExecuteScalar().ToString());
                                        connection73.Close();
                                        roomlistAlt.Add(roomid3Alt);
                                    }
                                }

                                int altroomcount = roomlistAlt.Count;
                                for (int i = 0; i < altroomcount; i++)
                                {
                                    int RoomToAdd = roomlistAlt[i];

                                    connection.Open();
                                    string altroomsql = "Insert into [AltRoom] (requestID,AltRoom) values((SELECT MAX(requestID) FROM [Request])," + RoomToAdd + ")";
                                    SqlCommand altroomcommand = new SqlCommand(altroomsql, connection);
                                    altroomcommand.ExecuteNonQuery();
                                    connection.Close();
                                }
                                createRequestScriptDiv.InnerHtml = "<script>alert(\"Request successfully made\");</script>";
                                //Messagebox


                                clearEverything();
                            }
                            else
                                errorMessage.Text = "Please Select At Least One Period.";
                        }
                        else
                            errorMessage.Text = "Please Select At Least One Week.";
                    }
                    else
                        errorMessage.Text = "Please Select At Least One Room.";
                }
                else
                    errorMessage.Text = "Please Select a Valid Capacity.";
            }
            else
                errorMessage.Text = "Please Enter a Number for Capacity.";
        }


        /// <summary>
        /// Handles the Click event of the All control to set all values to true
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void All_Click(object sender, EventArgs e)
        {
            /*CheckBox[] weekChecked = { Week1, Week2, Week3, Week4, Week5, Week6, Week7,
                                  Week8, Week9, Week10, Week11, Week12, Week13, Week14, Week15};
            foreach (checked week in weekChecked )
            {
                weekChecked[week] = true;
            }*/
            Week1.Checked = true;
            Week2.Checked = true;
            Week3.Checked = true;
            Week4.Checked = true;
            Week5.Checked = true;
            Week6.Checked = true;
            Week7.Checked = true;
            Week8.Checked = true;
            Week9.Checked = true;
            Week10.Checked = true;
            Week11.Checked = true;
            Week12.Checked = true;
            Week13.Checked = true;
            Week14.Checked = true;
            Week15.Checked = true;
        }

        protected void Twelve_Click(object sender, EventArgs e)
        {
            Week1.Checked = true;
            Week2.Checked = true;
            Week3.Checked = true;
            Week4.Checked = true;
            Week5.Checked = true;
            Week6.Checked = true;
            Week7.Checked = true;
            Week8.Checked = true;
            Week9.Checked = true;
            Week10.Checked = true;
            Week11.Checked = true;
            Week12.Checked = true;
            Week13.Checked = false;
            Week14.Checked = false;
            Week15.Checked = false;
        }

        protected void Odd_Click(object sender, EventArgs e)
        {
            Week1.Checked = true;
            Week3.Checked = true;
            Week5.Checked = true;
            Week7.Checked = true;
            Week9.Checked = true;
            Week11.Checked = true;
            Week13.Checked = true;
            Week15.Checked = true;
            Week2.Checked = false;
            Week4.Checked = false;
            Week6.Checked = false;
            Week8.Checked = false;
            Week10.Checked = false;
            Week12.Checked = false;
            Week14.Checked = false;
        }

        protected void Even_Click(object sender, EventArgs e)
        {
            Week2.Checked = true;
            Week4.Checked = true;
            Week6.Checked = true;
            Week8.Checked = true;
            Week10.Checked = true;
            Week12.Checked = true;
            Week14.Checked = true;
            Week1.Checked = false;
            Week3.Checked = false;
            Week5.Checked = false;
            Week7.Checked = false;
            Week9.Checked = false;
            Week11.Checked = false;
            Week13.Checked = false;
            Week15.Checked = false;
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            Week1.Checked = false;
            Week2.Checked = false;
            Week3.Checked = false;
            Week4.Checked = false;
            Week5.Checked = false;
            Week6.Checked = false;
            Week7.Checked = false;
            Week8.Checked = false;
            Week9.Checked = false;
            Week10.Checked = false;
            Week11.Checked = false;
            Week12.Checked = false;
            Week13.Checked = false;
            Week14.Checked = false;
            Week15.Checked = false;
        }

        int mondayway = 0;
        protected void ButtonMonday_Click(object sender, EventArgs e)
        {
            if (mondayway == 0)
            {
                CheckBoxM1.Checked = true;
                CheckBoxM2.Checked = true;
                CheckBoxM3.Checked = true;
                CheckBoxM4.Checked = true;
                CheckBoxM5.Checked = true;
                CheckBoxM6.Checked = true;
                CheckBoxM7.Checked = true;
                CheckBoxM8.Checked = true;
                CheckBoxM9.Checked = true;
                mondayway = 1;
            }
            else
            {
                CheckBoxM1.Checked = false;
                CheckBoxM2.Checked = false;
                CheckBoxM3.Checked = false;
                CheckBoxM4.Checked = false;
                CheckBoxM5.Checked = false;
                CheckBoxM6.Checked = false;
                CheckBoxM7.Checked = false;
                CheckBoxM8.Checked = false;
                CheckBoxM9.Checked = false;
                mondayway = 0;
            }

        }

        int tuesdayway = 0;
        protected void ButtonTuesday_Click(object sender, EventArgs e)
        {
            if (tuesdayway == 0)
            {
                CheckBoxT1.Checked = true;
                CheckBoxT2.Checked = true;
                CheckBoxT3.Checked = true;
                CheckBoxT4.Checked = true;
                CheckBoxT5.Checked = true;
                CheckBoxT6.Checked = true;
                CheckBoxT7.Checked = true;
                CheckBoxT8.Checked = true;
                CheckBoxT9.Checked = true;
                tuesdayway = 1;
            }
            else
            {
                CheckBoxT1.Checked = false;
                CheckBoxT2.Checked = false;
                CheckBoxT3.Checked = false;
                CheckBoxT4.Checked = false;
                CheckBoxT5.Checked = false;
                CheckBoxT6.Checked = false;
                CheckBoxT7.Checked = false;
                CheckBoxT8.Checked = false;
                CheckBoxT9.Checked = false;
                tuesdayway = 0;
            }
        }

        int wednesdayway = 0;
        protected void ButtonWednesday_Click(object sender, EventArgs e)
        {
            if (wednesdayway == 0)
            {
                CheckBoxW1.Checked = true;
                CheckBoxW2.Checked = true;
                CheckBoxW3.Checked = true;
                CheckBoxW4.Checked = true;
                CheckBoxW5.Checked = true;
                CheckBoxW6.Checked = true;
                CheckBoxW7.Checked = true;
                CheckBoxW8.Checked = true;
                CheckBoxW9.Checked = true;
                wednesdayway = 1;
            }
            else
            {
                CheckBoxW1.Checked = false;
                CheckBoxW2.Checked = false;
                CheckBoxW3.Checked = false;
                CheckBoxW4.Checked = false;
                CheckBoxW5.Checked = false;
                CheckBoxW6.Checked = false;
                CheckBoxW7.Checked = false;
                CheckBoxW8.Checked = false;
                CheckBoxW9.Checked = false;
                wednesdayway = 0;
            }
        }

        int thursdayway = 0;
        protected void ButtonThursday_Click(object sender, EventArgs e)
        {
            if (thursdayway == 0)
            {
                CheckBoxJ1.Checked = true;
                CheckBoxJ2.Checked = true;
                CheckBoxJ3.Checked = true;
                CheckBoxJ4.Checked = true;
                CheckBoxJ5.Checked = true;
                CheckBoxJ6.Checked = true;
                CheckBoxJ7.Checked = true;
                CheckBoxJ8.Checked = true;
                CheckBoxJ9.Checked = true;
                thursdayway = 1;
            }
            else
            {
                CheckBoxJ1.Checked = false;
                CheckBoxJ2.Checked = false;
                CheckBoxJ3.Checked = false;
                CheckBoxJ4.Checked = false;
                CheckBoxJ5.Checked = false;
                CheckBoxJ6.Checked = false;
                CheckBoxJ7.Checked = false;
                CheckBoxJ8.Checked = false;
                CheckBoxJ9.Checked = false;
                thursdayway = 0;
            }
        }

        int fridayway = 0;
        protected void ButtonFriday_Click(object sender, EventArgs e)
        {
            if (fridayway == 0)
            {
                CheckBoxF1.Checked = true;
                CheckBoxF2.Checked = true;
                CheckBoxF3.Checked = true;
                CheckBoxF4.Checked = true;
                CheckBoxF5.Checked = true;
                CheckBoxF6.Checked = true;
                CheckBoxF7.Checked = true;
                CheckBoxF8.Checked = true;
                CheckBoxF9.Checked = true;
                fridayway = 1;
            }
            else
            {
                CheckBoxF1.Checked = false;
                CheckBoxF2.Checked = false;
                CheckBoxF3.Checked = false;
                CheckBoxF4.Checked = false;
                CheckBoxF5.Checked = false;
                CheckBoxF6.Checked = false;
                CheckBoxF7.Checked = false;
                CheckBoxF8.Checked = false;
                CheckBoxF9.Checked = false;
                fridayway = 0;
            }
        }

        int oneway = 0;
        protected void ButtonPeriod1_Click(object sender, EventArgs e)
        {
            if (oneway == 0)
            {
                CheckBoxM1.Checked = true;
                CheckBoxT1.Checked = true;
                CheckBoxW1.Checked = true;
                CheckBoxJ1.Checked = true;
                CheckBoxF1.Checked = true;
                oneway = 1;
            }
            else
            {
                CheckBoxM1.Checked = false;
                CheckBoxT1.Checked = false;
                CheckBoxW1.Checked = false;
                CheckBoxJ1.Checked = false;
                CheckBoxF1.Checked = false;
                oneway = 0;
            }
        }

        int twoway = 0;
        protected void ButtonPeriod2_Click(object sender, EventArgs e)
        {
            if (twoway == 0)
            {
                CheckBoxM2.Checked = true;
                CheckBoxT2.Checked = true;
                CheckBoxW2.Checked = true;
                CheckBoxJ2.Checked = true;
                CheckBoxF2.Checked = true;
                twoway = 1;
            }
            else
            {
                CheckBoxM2.Checked = false;
                CheckBoxT2.Checked = false;
                CheckBoxW2.Checked = false;
                CheckBoxJ2.Checked = false;
                CheckBoxF2.Checked = false;
                twoway = 0;
            }
        }

        int threeway = 0;
        protected void ButtonPeriod3_Click(object sender, EventArgs e)
        {
            if (threeway == 0)
            {
                CheckBoxM3.Checked = true;
                CheckBoxT3.Checked = true;
                CheckBoxW3.Checked = true;
                CheckBoxJ3.Checked = true;
                CheckBoxF3.Checked = true;
                threeway = 1;
            }
            else
            {
                CheckBoxM3.Checked = false;
                CheckBoxT3.Checked = false;
                CheckBoxW3.Checked = false;
                CheckBoxJ3.Checked = false;
                CheckBoxF3.Checked = false;
                threeway = 0;
            }
        }

        int fourway = 0;
        protected void ButtonPeriod4_Click(object sender, EventArgs e)
        {
            if (fourway == 0)
            {
                CheckBoxM4.Checked = true;
                CheckBoxT4.Checked = true;
                CheckBoxW4.Checked = true;
                CheckBoxJ4.Checked = true;
                CheckBoxF4.Checked = true;
                fourway = 1;
            }
            else
            {
                CheckBoxM4.Checked = false;
                CheckBoxT4.Checked = false;
                CheckBoxW4.Checked = false;
                CheckBoxJ4.Checked = false;
                CheckBoxF4.Checked = false;
                fourway = 0;
            }
        }

        int fiveway = 0;
        protected void ButtonPeriod5_Click(object sender, EventArgs e)
        {
            if (fiveway == 0)
            {
                CheckBoxM5.Checked = true;
                CheckBoxT5.Checked = true;
                CheckBoxW5.Checked = true;
                CheckBoxJ5.Checked = true;
                CheckBoxF5.Checked = true;
                fiveway = 1;
            }
            else
            {
                CheckBoxM5.Checked = false;
                CheckBoxT5.Checked = false;
                CheckBoxW5.Checked = false;
                CheckBoxJ5.Checked = false;
                CheckBoxF5.Checked = false;
                fiveway = 0;
            }
        }

        int sixway = 0;
        protected void ButtonPeriod6_Click(object sender, EventArgs e)
        {
            if (sixway == 0)
            {
                CheckBoxM6.Checked = true;
                CheckBoxT6.Checked = true;
                CheckBoxW6.Checked = true;
                CheckBoxJ6.Checked = true;
                CheckBoxF6.Checked = true;
                sixway = 1;
            }
            else
            {
                CheckBoxM6.Checked = false;
                CheckBoxT6.Checked = false;
                CheckBoxW6.Checked = false;
                CheckBoxJ6.Checked = false;
                CheckBoxF6.Checked = false;
                sixway = 0;
            }
        }

        int sevenway = 0;
        protected void ButtonPeriod7_Click(object sender, EventArgs e)
        {
            if (sevenway == 0)
            {
                CheckBoxM7.Checked = true;
                CheckBoxT7.Checked = true;
                CheckBoxW7.Checked = true;
                CheckBoxJ7.Checked = true;
                CheckBoxF7.Checked = true;
                sevenway = 1;
            }
            else
            {
                CheckBoxM7.Checked = false;
                CheckBoxT7.Checked = false;
                CheckBoxW7.Checked = false;
                CheckBoxJ7.Checked = false;
                CheckBoxF7.Checked = false;
                sevenway = 0;
            }
        }

        int eightway = 0;
        protected void ButtonPeriod8_Click(object sender, EventArgs e)
        {
            if (eightway == 0)
            {
                CheckBoxM8.Checked = true;
                CheckBoxT8.Checked = true;
                CheckBoxW8.Checked = true;
                CheckBoxJ8.Checked = true;
                CheckBoxF8.Checked = true;
                eightway = 1;
            }
            else
            {
                CheckBoxM8.Checked = false;
                CheckBoxT8.Checked = false;
                CheckBoxW8.Checked = false;
                CheckBoxJ8.Checked = false;
                CheckBoxF8.Checked = false;
                eightway = 0;
            }
        }

        int nineway = 0;
        protected void ButtonPeriod9_Click(object sender, EventArgs e)
        {
            if (nineway == 0)
            {
                CheckBoxM9.Checked = true;
                CheckBoxT9.Checked = true;
                CheckBoxW9.Checked = true;
                CheckBoxJ9.Checked = true;
                CheckBoxF9.Checked = true;
                nineway = 1;
            }
            else
            {
                CheckBoxM9.Checked = false;
                CheckBoxT9.Checked = false;
                CheckBoxW9.Checked = false;
                CheckBoxJ9.Checked = false;
                CheckBoxF9.Checked = false;
                nineway = 0;
            }
        }

        /// <summary>
        /// Deselect any selected periods
        /// </summary>
        public void ClearPeriods()
        {
            CheckBoxM1.Checked = false;
            CheckBoxM2.Checked = false;
            CheckBoxM3.Checked = false;
            CheckBoxM4.Checked = false;
            CheckBoxM5.Checked = false;
            CheckBoxM6.Checked = false;
            CheckBoxM7.Checked = false;
            CheckBoxM8.Checked = false;
            CheckBoxM9.Checked = false;
            CheckBoxT1.Checked = false;
            CheckBoxT2.Checked = false;
            CheckBoxT3.Checked = false;
            CheckBoxT4.Checked = false;
            CheckBoxT5.Checked = false;
            CheckBoxT6.Checked = false;
            CheckBoxT7.Checked = false;
            CheckBoxT8.Checked = false;
            CheckBoxT9.Checked = false;
            CheckBoxW1.Checked = false;
            CheckBoxW2.Checked = false;
            CheckBoxW3.Checked = false;
            CheckBoxW4.Checked = false;
            CheckBoxW5.Checked = false;
            CheckBoxW6.Checked = false;
            CheckBoxW7.Checked = false;
            CheckBoxW8.Checked = false;
            CheckBoxW9.Checked = false;
            CheckBoxJ1.Checked = false;
            CheckBoxJ2.Checked = false;
            CheckBoxJ3.Checked = false;
            CheckBoxJ4.Checked = false;
            CheckBoxJ5.Checked = false;
            CheckBoxJ6.Checked = false;
            CheckBoxJ7.Checked = false;
            CheckBoxJ8.Checked = false;
            CheckBoxJ9.Checked = false;
            CheckBoxF1.Checked = false;
            CheckBoxF2.Checked = false;
            CheckBoxF3.Checked = false;
            CheckBoxF4.Checked = false;
            CheckBoxF5.Checked = false;
            CheckBoxF6.Checked = false;
            CheckBoxF7.Checked = false;
            CheckBoxF8.Checked = false;
            CheckBoxF9.Checked = false;
        }

        /// <summary>
        /// Handles the Click event of the ButtonClearPeriods control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ButtonClearPeriods_Click(object sender, EventArgs e)
        {
            ClearPeriods();
        }


        /// <summary>
        /// Handles the TextChanged event of the TextBoxCapacity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void TextBoxCapacity_TextChanged(object sender, EventArgs e)
        {
            RebuildListOfRooms();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the RadioButtonListRoomType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonListRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildListOfRooms();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the RadioButtonListArrangement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonListArrangement_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildListOfRooms();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the RadioButtonListProjector control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonListProjector_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildListOfRooms();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the RadioButtonListWheelchair control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonListWheelchair_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildListOfRooms();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the RadioButtonListVisualiser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonListVisualiser_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildListOfRooms();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the RadioButtonListComputer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonListComputer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildListOfRooms();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the RadioButtonListParks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RadioButtonListParks_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBuildingsForSelectedPark();
            RebuildListOfRooms();
        }

        /// <summary>
        /// Fills the buildings for selected park.
        /// </summary>
        public void FillBuildingsForSelectedPark()
        {
            DropDownListBuildings.Items.Clear();
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect.Open();
            int selectedpark = RadioButtonListParks.SelectedIndex + 1;
            string buildingsql = String.Format("Select buildingName from [Building] where parkID = {0}", selectedpark);
            SqlCommand buildingscommand = new SqlCommand(buildingsql, connect);
            SqlDataReader buildings = buildingscommand.ExecuteReader();
            while (buildings.Read())
            {
                DropDownListBuildings.Items.Add(buildings.GetString(0));
            }

            connect.Close();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListRooms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            string label = "None";
            string label1 = LabelRoom1.Text;
            string label2 = LabelRoom2.Text;
            string label3 = LabelRoom3.Text;
            string selectedroom = DropDownListRooms.SelectedValue;
            bool l1 = label1.Equals(label);
            bool l2 = label2.Equals(label);
            bool l3 = label3.Equals(label);

            if (l1)
            {
                LabelRoom1.Text = selectedroom;
            }
            else if (l2)
            {
                LabelRoom2.Text = selectedroom;
            }
            else if (l3)
            {
                LabelRoom3.Text = selectedroom;
            }
            DropDownListRooms.Items.Remove(selectedroom);
            DropDownListRoomsAlt.Items.Remove(selectedroom);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListRoomsAlt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DropDownListRoomsAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            string label = "None";
            string label1 = LabelRoomAlt1.Text;
            string label2 = LabelRoomAlt2.Text;
            string label3 = LabelRoomAlt3.Text;
            string selectedroom = DropDownListRoomsAlt.SelectedValue;
            bool l1 = label1.Equals(label);
            bool l2 = label2.Equals(label);
            bool l3 = label3.Equals(label);

            if (l1)
            {
                LabelRoomAlt1.Text = selectedroom;
            }
            else if (l2)
            {
                LabelRoomAlt2.Text = selectedroom;
            }
            else if (l3)
            {
                LabelRoomAlt3.Text = selectedroom;
            }
            DropDownListRoomsAlt.Items.Remove(selectedroom);
        }

        /// <summary>
        /// Handles the Click event of the ButtonDeleteRoom1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ButtonDeleteRoom1_Click(object sender, EventArgs e)
        {
            string label = "None";
            string roomname = LabelRoom1.Text;
            bool l1 = LabelRoom1.Text.Equals(label);
            if (!l1)
            {
                DropDownListRooms.Items.Add(roomname);
                DropDownListRoomsAlt.Items.Add(roomname);
                LabelRoom1.Text = "None";
            }
        }

        protected void ButtonDeleteRoom2_Click(object sender, EventArgs e)
        {
            string label = "None";
            string roomname = LabelRoom2.Text;
            bool l1 = LabelRoom2.Text.Equals(label);
            if (!l1)
            {
                DropDownListRooms.Items.Add(roomname);
                DropDownListRoomsAlt.Items.Add(roomname);
                LabelRoom2.Text = "None";
            }
        }

        protected void ButtonDeleteRoom3_Click(object sender, EventArgs e)
        {
            string label = "None";
            string roomname = LabelRoom3.Text;
            bool l1 = LabelRoom3.Text.Equals(label);
            if (!l1)
            {
                DropDownListRooms.Items.Add(roomname);
                DropDownListRoomsAlt.Items.Add(roomname);
                LabelRoom3.Text = "None";
            }
        }

        protected void ButtonDeleteRoomAlt1_Click(object sender, EventArgs e)
        {
            string label = "None";
            string roomname = LabelRoomAlt1.Text;
            bool l1 = LabelRoomAlt1.Text.Equals(label);
            if (!l1)
            {
                DropDownListRoomsAlt.Items.Add(roomname);
                LabelRoomAlt1.Text = "None";
            }
        }

        protected void ButtonDeleteRoomAlt2_Click(object sender, EventArgs e)
        {
            string label = "None";
            string roomname = LabelRoomAlt2.Text;
            bool l1 = LabelRoomAlt2.Text.Equals(label);
            if (!l1)
            {
                DropDownListRoomsAlt.Items.Add(roomname);
                LabelRoomAlt2.Text = "None";
            }
        }

        protected void ButtonDeleteRoomAlt3_Click(object sender, EventArgs e)
        {
            string label = "None";
            string roomname = LabelRoomAlt3.Text;
            bool l1 = LabelRoomAlt3.Text.Equals(label);
            if (!l1)
            {
                DropDownListRoomsAlt.Items.Add(roomname);
                LabelRoomAlt3.Text = "None";
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListBuildings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DropDownListBuildings_SelectedIndexChanged(object sender, EventArgs e)
        {

            RebuildListOfRooms();
        }

        /// <summary>
        /// Clears all selections.
        /// </summary>
        public void clearEverything() {
            ClearPeriods();
            Week1.Checked = false;
            Week2.Checked = false;
            Week3.Checked = false;
            Week4.Checked = false;
            Week5.Checked = false;
            Week6.Checked = false;
            Week7.Checked = false;
            Week8.Checked = false;
            Week9.Checked = false;
            Week10.Checked = false;
            Week11.Checked = false;
            Week12.Checked = false;
            Week13.Checked = false;
            Week14.Checked = false;
            Week15.Checked = false;

            TextBoxCapacity.Text = "";
            TextBox2.Text = "";
            RadioButtonListComputer.SelectedIndex = 1;
            RadioButtonListVisualiser.SelectedIndex = 1;
            RadioButtonListWheelchair.SelectedIndex = 1;
            CheckBoxWB.Checked = false;
            CheckBoxCB.Checked = false;
            RadioButtonListProjector.SelectedIndex = 2;
            RadioButtonListArrangement.SelectedIndex = 2;
            RadioButtonListRoomType.SelectedIndex = 2;
            DropDownListModules.SelectedIndex = 0;
            RadioButtonListParks.SelectedIndex = 0;
            DropDownListBuildings.Items.Clear();

            string label = "None";
            string roomname = LabelRoom1.Text;
            bool l1 = LabelRoom1.Text.Equals(label);
            if (!l1)
            {
                DropDownListRooms.Items.Add(roomname);
                DropDownListRoomsAlt.Items.Add(roomname);
                LabelRoom1.Text = "None";
            }

            string roomname2 = LabelRoom2.Text;
            bool l2 = LabelRoom2.Text.Equals(label);
            if (!l2)
            {
                DropDownListRooms.Items.Add(roomname2);
                DropDownListRoomsAlt.Items.Add(roomname2);
                LabelRoom2.Text = "None";
            }
            string roomname3 = LabelRoom3.Text;
            bool l3 = LabelRoom3.Text.Equals(label);
            if (!l3)
            {
                DropDownListRooms.Items.Add(roomname3);
                DropDownListRoomsAlt.Items.Add(roomname3);
                LabelRoom3.Text = "None";
            }

            string roomnameAlt = LabelRoomAlt1.Text;
            bool l4 = LabelRoomAlt1.Text.Equals(label);
            if (!l4)
            {
                DropDownListRoomsAlt.Items.Add(roomnameAlt);
                LabelRoomAlt1.Text = "None";
            }
            string roomnameAlt2 = LabelRoomAlt2.Text;
            bool l5 = LabelRoomAlt2.Text.Equals(label);
            if (!l5)
            {
                DropDownListRoomsAlt.Items.Add(roomnameAlt2);
                LabelRoomAlt2.Text = "None";
            }
            string roomnameAlt3 = LabelRoomAlt3.Text;
            bool l6 = LabelRoomAlt1.Text.Equals(label);
            if (!l6)
            {
                DropDownListRoomsAlt.Items.Add(roomnameAlt3);
                LabelRoomAlt3.Text = "None";
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonClearAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ButtonClearAll_Click(object sender, EventArgs e)
        {
            clearEverything();
            RebuildListOfRooms();
        }

        protected void DropDownListPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            PopulateModuleList(connect);
        }

        protected void DropDownListModules_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}