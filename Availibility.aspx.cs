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

namespace Team11
{
    public partial class Availibility : System.Web.UI.Page
    {
        string moduleCode = "";
        int userID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // read the userid from the querystring
            userID = Convert.ToInt32(Session["userID"]);

            if (!IsPostBack)
            {
                SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                connect.Open();

                //Find all rooms
                string findrooms = "Select roomName from [Room]";
                SqlCommand roomscommand = new SqlCommand(findrooms, connect);
                SqlDataReader rooms = roomscommand.ExecuteReader();
                DropDownListRooms.Items.Clear();
                //Add the results to the dropdownlist
                while (rooms.Read())
                {
                    DropDownListRooms.Items.Add(rooms.GetString(0).ToString());
                }
                connect.Close();
                //Add the user's department's private roooms at the bottom of the dropdown box
                SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                connect2.Open();
                string findroomsql2 = "SELECT Room.roomName FROM Room INNER JOIN Building ON Room.buildingID = Building.buildingID INNER JOIN [User] ON Building.deptName = [User].deptName AND [User].userID = " + userID + " AND Room.private <> 0";
                SqlCommand roomcommand2 = new SqlCommand(findroomsql2, connect2);
                SqlDataReader rooms2 = roomcommand2.ExecuteReader();
                //add a non-selectable title at the bottom of the rooms dropwdown to seperate other private rooms from the rest
                DropDownListRooms.Items.Add("Private Rooms");
                DropDownListRooms.Items.FindByText("Private Rooms").Attributes.Add("disabled", "disabled");
                while (rooms2.Read())
                {
                    DropDownListRooms.Items.Add(rooms2.GetString(0).ToString());
                }
                connect2.Close();
                DropDownListRooms.Items.FindByText("Private Rooms").Attributes.Add("disabled", "disabled");
                roomavailibility();
            }
        }
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
                this.divBookingByRoom.Visible = false;
            }

        }
        protected void RadioButtonListPark_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Retrieve all Buildings belonging to the selected park and add them to dropdownlist
            DropDownListBuilding.Items.Clear();
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect.Open();
            int parkID = RadioButtonListPark.SelectedIndex + 1;
            string findbuildings = "Select buildingName from [Building] where parkID =' " + parkID + "'";
            SqlCommand buildingscommand = new SqlCommand(findbuildings, connect);

            SqlDataReader buildings = buildingscommand.ExecuteReader();
            while (buildings.Read())
            {
                DropDownListBuilding.Items.Add(buildings.GetString(0).ToString());
            }
            connect.Close();
            findrooms();
            roomavailibility();
        }
        protected void DropDownListBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            findrooms();
            roomavailibility();
        }

        public void findrooms()
        {

            DropDownListRooms.Items.Clear();
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect.Open();

            //Find the BuildingID of the selected Building
            string findbuildingID = "Select buildingID from [Building] where buildingName='" + DropDownListBuilding.SelectedValue + "'";
            SqlCommand IDcommand = new SqlCommand(findbuildingID, connect);
            string buildingID = IDcommand.ExecuteScalar().ToString();

            //Find the rooms belonging to that buildingID
            string findrooms = "Select roomName from [Room] where buildingID =' " + buildingID + "'";
            SqlCommand roomscommand = new SqlCommand(findrooms, connect);
            SqlDataReader rooms = roomscommand.ExecuteReader();

            //Add the results to the dropdownlist
            while (rooms.Read())
            {
                DropDownListRooms.Items.Add(rooms.GetString(0).ToString());
            }
            connect.Close();


            //Add the user's department's private roooms at the bottom of the dropdown box
            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect2.Open();
            string findroomsql2 = "SELECT Room.roomName FROM Room INNER JOIN Building ON Room.buildingID = Building.buildingID INNER JOIN [User] ON Building.deptName = [User].deptName AND [User].userID = " + userID + " AND Room.private <> 0";
            SqlCommand roomcommand2 = new SqlCommand(findroomsql2, connect2);
            SqlDataReader rooms2 = roomcommand2.ExecuteReader();
            //add a non-selectable title at the bottom of the rooms dropwdown to seperate other private rooms from the rest
            DropDownListRooms.Items.Add("Private Rooms");
            DropDownListRooms.Items.FindByText("Private Rooms").Attributes.Add( "disabled", "disabled" );
            while (rooms2.Read())
            {
                DropDownListRooms.Items.Add(rooms2.GetString(0).ToString());
            }
            connect2.Close();


        }



        public void roomavailibility()
        {
            // Initialise the two button arrays
            CheckBox[,] boxes = { { CheckBoxM1, CheckBoxM2, CheckBoxM3, CheckBoxM4, CheckBoxM5, CheckBoxM6, CheckBoxM7, CheckBoxM8, CheckBoxM9 }, 
                                                                    {CheckBoxT1, CheckBoxT2, CheckBoxT3, CheckBoxT4, CheckBoxT5, CheckBoxT6, CheckBoxT7, CheckBoxT8, CheckBoxT9 },
                                                                    {CheckBoxW1, CheckBoxW2, CheckBoxW3, CheckBoxW4, CheckBoxW5, CheckBoxW6, CheckBoxW7, CheckBoxW8, CheckBoxW9  },
                                                                    {CheckBoxJ1, CheckBoxJ2, CheckBoxJ3, CheckBoxJ4, CheckBoxJ5, CheckBoxJ6, CheckBoxJ7, CheckBoxJ8, CheckBoxJ9  },
                                                                    {CheckBoxM1, CheckBoxF2, CheckBoxF3, CheckBoxF4, CheckBoxF5, CheckBoxF6, CheckBoxF7, CheckBoxF8, CheckBoxF9  }};
            Button[,] labels = { { LabelM1, LabelM2, LabelM3, LabelM4, LabelM5, LabelM6, LabelM7, LabelM8, LabelM9 },
                                   {LabelT1, LabelT2, LabelT3, LabelT4, LabelT5, LabelT6, LabelT7, LabelT8, LabelT9 },
                                   {LabelW1, LabelW2, LabelW3, LabelW4, LabelW5, LabelW6, LabelW7, LabelW8, LabelW9 },
                                   {LabelJ1, LabelJ2, LabelJ3, LabelJ4, LabelJ5, LabelJ6, LabelJ7, LabelJ8, LabelJ9 },
                                   {LabelF1, LabelF2, LabelF3, LabelF4, LabelF5, LabelF6, LabelF7, LabelF8, LabelF9 } };

            //Reinitialize the view at every search function call.
            for (int day = 0; day <= 4; day++)
            {
                for (int period = 0; period < 9; period++)
                {
                    boxes[day, period].Enabled = true;
                    boxes[day, period].Visible = true;
                    labels[day, period].Visible = false;

                }
            }

            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect.Open();
            string selectedroom = DropDownListRooms.SelectedValue;

            int selectedweek = DropDownListWeekNumber.SelectedIndex + 1;

            string sqlquery = String.Format(@"
Select distinct moduleCode, [day], periodStart, periodEnd
from [Room] 
Inner Join [BookedRoom]  On [Room].roomID=[BookedRoom].roomID 
Inner join [Request] on [BookedRoom].requestID=[Request].requestID 
inner join [Week] on [Request].weekID=[Week].weekID 
where [Room].roomName='{0}' and [Week].week1={1}", selectedroom, selectedweek);
            SqlCommand roomcommand = new SqlCommand(sqlquery, connect);
            SqlDataReader roominfo = roomcommand.ExecuteReader();

           while (roominfo.Read())
            {
               // use GetOrdinal to find the column number so column ordering isn't so important
                string dayName = roominfo.GetString(roominfo.GetOrdinal("day"));
                int start = roominfo.GetInt32(roominfo.GetOrdinal("periodStart"));
                int end = roominfo.GetInt32(roominfo.GetOrdinal("periodEnd"));
                string moduleCode = roominfo.GetString(roominfo.GetOrdinal("moduleCode"));

                // determine the index in to the arrays from the day name
                int dayIndex = 0;
                switch (dayName)
                {
                    case "Monday": dayIndex = 0; break;
                    case "Tuesday": dayIndex = 1; break;
                    case "Wednesday": dayIndex = 2; break;
                    case "Thursday": dayIndex = 3; break;
                    case "Friday": dayIndex = 4; break;
                }

                for (int period = start - 1; period < end; period++ )
                {
                    boxes[dayIndex, period].Enabled = false;
                    boxes[dayIndex, period].Visible = false;
                    labels[dayIndex, period].Text = moduleCode;
                    labels[dayIndex, period].Visible = true;
                }
             }

            connect.Close();
        }

        protected void DropDownListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            roomavailibility();
        }

        protected void DropDownListWeekNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            roomavailibility();
        }


        //Find available rooms (by DATE)
        //Find available rooms (by DATE)
        //Find available rooms (by DATE)
        protected void ButtonFindRooms_Click(object sender, EventArgs e)
        {
            DropDownListRoomsByDate.Items.Clear();
            // initialise the checkbox array
            CheckBox[,] boxes = { { CheckBoxM1, CheckBoxM2, CheckBoxM3, CheckBoxM4, CheckBoxM5, CheckBoxM6, CheckBoxM7, CheckBoxM8, CheckBoxM9 }, 
                                                                    {CheckBoxT1, CheckBoxT2, CheckBoxT3, CheckBoxT4, CheckBoxT5, CheckBoxT6, CheckBoxT7, CheckBoxT8, CheckBoxT9 },
                                                                    {CheckBoxW1, CheckBoxW2, CheckBoxW3, CheckBoxW4, CheckBoxW5, CheckBoxW6, CheckBoxW7, CheckBoxW8, CheckBoxW9  },
                                                                    {CheckBoxJ1, CheckBoxJ2, CheckBoxJ3, CheckBoxJ4, CheckBoxJ5, CheckBoxJ6, CheckBoxJ7, CheckBoxJ8, CheckBoxJ9  },
                                                                    {CheckBoxM1, CheckBoxF2, CheckBoxF3, CheckBoxF4, CheckBoxF5, CheckBoxF6, CheckBoxF7, CheckBoxF8, CheckBoxF9  }};

            string day = "";
            int week = DropDownListWeeks.SelectedIndex + 1;
            int periodStart = 0;
            List<string> days = new List<string>();
            List<int> periods = new List<int>();
            for (int period = 0; period < 9; period++)
            {
                if (boxes[0, period].Checked)
                {
                    days.Add("Monday");
                    periods.Add(period + 1);
                }
                else if (boxes[1, period].Checked)
                {
                    days.Add("Tuesday");
                    periods.Add(period + 1);
                }
                else if (boxes[2, period].Checked)
                {
                    days.Add("Wednesday");
                    periods.Add(period + 1);
                }
                else if (boxes[3, period].Checked)
                {
                    days.Add("Thursday");
                    periods.Add(period + 1);
                }
                else if (boxes[4, period].Checked)
                {
                    days.Add("Friday");
                    periods.Add(period + 1);
                }
            }

            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect.Open();
            string findroomsql = "SELECT roomName FROM [Room] WHERE (private = 0)";
            for (int y = 0; y < days.Count; y++)
            {
                if (y == 0)
                    findroomsql += "AND (RoomID NOT IN(SELECT roomID FROM [BookedRoom] INNER JOIN [Request] ON [BookedRoom].requestID = [Request].requestID INNER JOIN [Week] ON [Request].weekID = [Week].weekID WHERE [Request].day ='" + days[y] + "' AND (([Request].periodStart = " + periods[y] + ") OR ([Request].periodEnd = " + periods[y] + ")) AND [Week].week" + week + " = 1))";
                else
                    findroomsql += "AND (RoomID NOT IN(SELECT roomID FROM [BookedRoom] INNER JOIN [Request] ON [BookedRoom].requestID = [Request].requestID INNER JOIN [Week] ON [Request].weekID = [Week].weekID WHERE [Request].day ='" + days[y] + "' AND (([Request].periodStart = " + periods[y] + ") OR ([Request].periodEnd = " + periods[y] + ")) AND [Week].week" + week + " = 1))";
             }
            SqlCommand roomcommand = new SqlCommand(findroomsql, connect);
            SqlDataReader rooms = roomcommand.ExecuteReader();
            while (rooms.Read())
            {
                DropDownListRoomsByDate.Items.Add(rooms.GetString(0).ToString());
            }
            connect.Close();

            //Add the user's department's private roooms at the bottom of the dropdown box
            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect2.Open();
            string findroomsql2 = "SELECT Room.roomName FROM Room INNER JOIN Building ON Room.buildingID = Building.buildingID INNER JOIN [User] ON Building.deptCode = [User].deptCode AND [User].userID = " + userID + " AND Room.private <> 0";
            SqlCommand roomcommand2 = new SqlCommand(findroomsql2, connect2);
            SqlDataReader rooms2 = roomcommand2.ExecuteReader();
            //add a non-selectable title at the bottom of the rooms dropwdown to seperate other private rooms from the rest
            DropDownListRoomsByDate.Items.Add("Private Rooms");
            DropDownListRoomsByDate.Items.FindByText("Private Rooms").Attributes.Add( "disabled", "disabled" );
            while (rooms2.Read())
            {
                DropDownListRoomsByDate.Items.Add(rooms2.GetString(0).ToString());
            }
            connect2.Close();
        }

        //Book buttons
        protected void ButtonBookByDate_Click(object sender, EventArgs e)
        {


            SqlConnection connection7 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connection7.Open();

            // initialise the checkbox array
            CheckBox[,] weekdayCheckBoxes = { { CheckBoxM1, CheckBoxM2, CheckBoxM3, CheckBoxM4, CheckBoxM5, CheckBoxM6, CheckBoxM7, CheckBoxM8, CheckBoxM9 }, 
                                                                    {CheckBoxT1, CheckBoxT2, CheckBoxT3, CheckBoxT4, CheckBoxT5, CheckBoxT6, CheckBoxT7, CheckBoxT8, CheckBoxT9 },
                                                                    {CheckBoxW1, CheckBoxW2, CheckBoxW3, CheckBoxW4, CheckBoxW5, CheckBoxW6, CheckBoxW7, CheckBoxW8, CheckBoxW9  },
                                                                    {CheckBoxJ1, CheckBoxJ2, CheckBoxJ3, CheckBoxJ4, CheckBoxJ5, CheckBoxJ6, CheckBoxJ7, CheckBoxJ8, CheckBoxJ9  },
                                                                    {CheckBoxM1, CheckBoxF2, CheckBoxF3, CheckBoxF4, CheckBoxF5, CheckBoxF6, CheckBoxF7, CheckBoxF8, CheckBoxF9  }};

            //Retrieve module name
            string moduleName = DropDownListModulesByDate.SelectedValue;
            string modulesquery = "SELECT moduleCode FROM [Module] WHERE moduleTitle ='" + moduleName + "'";
            SqlCommand modulessql = new SqlCommand(modulesquery, connection7);
            string moduleCodeText = modulessql.ExecuteScalar().ToString();

            string[] dayName = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            bool[] weekdayRequest = new bool[10] { false, false, false, false, false, false, false, false, false, false };

            for (int day = 0; day < 5; day++)
            {
                for (int period = 0; period < 9; period++)
                {
                    if (!weekdayCheckBoxes[day, period].Checked)
                    {
                        weekdayRequest[period] = true;
                    }
                }
            }
            
            //Weeks part
            int weeknumber = DropDownListWeeks.SelectedIndex;
            int[] weeksarray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            weeksarray[weeknumber] = 1;

            int weekIDText;
            string weekquery = "SELECT COUNT(weekID) FROM [Week] WHERE week1= " + weeksarray[0] + " AND week2= " + weeksarray[1] + " AND week3= " + weeksarray[2] + " AND week4= " + weeksarray[3] + " AND week5= " + weeksarray[4] + " AND week6= " + weeksarray[5] + " AND week7= " + weeksarray[6] + " AND week8= " + weeksarray[7] + " AND week9= " + weeksarray[8] + " AND week10= " + weeksarray[9] + " AND week11= " + weeksarray[10] + " AND week12= " + weeksarray[11] + " AND week13= " + weeksarray[12] + " AND week14= " + weeksarray[13] + " AND week15= " + weeksarray[14];
            SqlCommand weeksql = new SqlCommand(weekquery, connection7);
            int testtwo = Convert.ToInt32(weeksql.ExecuteScalar().ToString());

            /* If there is a corresponding weekID in database, select it */
            if (testtwo != 0)
            {
                string weekquery2 = "SELECT weekID FROM [Week] WHERE week1= " + weeksarray[0] + " AND week2= " + weeksarray[1] + " AND week3= " + weeksarray[2] + " AND week4= " + weeksarray[3] + " AND week5= " + weeksarray[4] + " AND week6= " + weeksarray[5] + " AND week7= " + weeksarray[6] + " AND week8= " + weeksarray[7] + " AND week9= " + weeksarray[8] + " AND week10= " + weeksarray[9] + " AND week11= " + weeksarray[10] + " AND week12= " + weeksarray[11] + " AND week13= " + weeksarray[12] + " AND week14= " + weeksarray[13] + " AND week15= " + weeksarray[14];
                SqlCommand weeksql2 = new SqlCommand(weekquery2, connection7);
                weekIDText = Convert.ToInt32(weeksql2.ExecuteScalar().ToString());
            }
            /* If there is no corresponding weekID, make a new one and use that */
            else
            {
                string insweekquery = "INSERT INTO [Week] VALUES(" + weeksarray[0] + "," + weeksarray[1] + "," + weeksarray[2] + "," + weeksarray[3] + "," + weeksarray[4] + "," + weeksarray[5] + "," + weeksarray[6] + "," + weeksarray[7] + "," + weeksarray[8] + "," + weeksarray[9] + "," + weeksarray[10] + "," + weeksarray[11] + "," + weeksarray[12] + "," + weeksarray[13] + "," + weeksarray[14] + ")";

                SqlCommand insweeksql = new SqlCommand(insweekquery, connection7);
                insweeksql.ExecuteNonQuery();
                string newweek = "SELECT MAX(weekID) FROM [Week]";
                SqlCommand maxweeksql = new SqlCommand(newweek, connection7);
                weekIDText = Convert.ToInt32(maxweeksql.ExecuteScalar().ToString());
            }


            string selectedroom = DropDownListRoomsByDate.Text;


            string getroomid = "SELECT roomID FROM [Room] WHERE roomName='" + selectedroom + "'";
            SqlCommand getroomidsql = new SqlCommand(getroomid, connection7);
            int roomid = Convert.ToInt32(getroomidsql.ExecuteScalar().ToString());

            if (selectedroom != "")
            {
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
                                    && n < 8
                                    && weekdayCheckBoxes[day, n + 1].Checked)
                                {
                                    duration++;
                                    period++;
                                }
                                else
                                {
                                    int endTime = startTime + duration;
                                    string status = "";
                                    //TODO: figure out how to determine if it is private or not
                                    bool isPrivateRoom = false;
                                    if (isPrivateRoom)
                                        status = "Accepted";
                                    else
                                        status = "Pending";

                                    string insreq = "INSERT INTO [Request] VALUES ('" + moduleCodeText + "','" + status + "'," + weekIDText + ",'" + dayName[day] + "'," + startTime + "," + endTime + ",1,2014,3)";
                                    SqlCommand insreqsql = new SqlCommand(insreq, connection7);
                                    insreqsql.ExecuteNonQuery();
                                    string booked = "INSERT INTO [BookedRoom] VALUES ((SELECT MAX(requestID) FROM [Request])," + roomid + ")";
                                    SqlCommand bookedsql = new SqlCommand(booked, connection7);
                                    bookedsql.ExecuteNonQuery();
                                    ended = false;

                                }
                            }
                        }
                    }
                }
                connection7.Close();
            }
        }

        protected void ButtonBookByRoom_Click(object sender, EventArgs e)
        {
            // initialise the checkbox array
            CheckBox[,] boxes = { { CheckBoxM1, CheckBoxM2, CheckBoxM3, CheckBoxM4, CheckBoxM5, CheckBoxM6, CheckBoxM7, CheckBoxM8, CheckBoxM9 }, 
                                                                    {CheckBoxT1, CheckBoxT2, CheckBoxT3, CheckBoxT4, CheckBoxT5, CheckBoxT6, CheckBoxT7, CheckBoxT8, CheckBoxT9 },
                                                                    {CheckBoxW1, CheckBoxW2, CheckBoxW3, CheckBoxW4, CheckBoxW5, CheckBoxW6, CheckBoxW7, CheckBoxW8, CheckBoxW9  },
                                                                    {CheckBoxJ1, CheckBoxJ2, CheckBoxJ3, CheckBoxJ4, CheckBoxJ5, CheckBoxJ6, CheckBoxJ7, CheckBoxJ8, CheckBoxJ9  },
                                                                    {CheckBoxM1, CheckBoxF2, CheckBoxF3, CheckBoxF4, CheckBoxF5, CheckBoxF6, CheckBoxF7, CheckBoxF8, CheckBoxF9  }};

            requestDetails.Visible = false;
            this.divBookingByRoom.Visible = true;
            LabelRoom.Text = DropDownListRooms.Text;
            LabelWeek.Text = DropDownListWeekNumber.SelectedValue;
            string day1 = "";
            string day2 = "";
            string day3 = "";
            string day4 = "";
            string day5 = "";
            string periods = "";
            for (int j = 0; j < 9; j++)
            {
                if (boxes[0, j].Checked) { day1 += "Monday: " + (j+1) + ", ";}
                if (boxes[1, j].Checked) { day2 += "Tuesday: " + (j + 1) + ", "; }
                if (boxes[2, j].Checked) { day3 += "Wednesday: " + (j + 1) + ", "; }
                if (boxes[3, j].Checked) { day4 += "Thursday: " + (j + 1) + ", "; }
                if (boxes[4, j].Checked) { day5 += "Friday: " + (j + 1) + ", "; }
            }
            periods = day1 + day2 + day3 + day4 + day5;
            if (periods.Length != 0)
                LabelPeriod.Text = periods.Substring(0, periods.Length - 2);
        }


        protected void ButtonClearPeriods_Click(object sender, EventArgs e)
        {
            ClearPeriods();
        }

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

        protected void ButtonMonday_Click1(object sender, EventArgs e)
        {
            CheckBoxM10.Checked = true;
            CheckBoxM11.Checked = true;
            CheckBoxM12.Checked = true;
            CheckBoxM13.Checked = true;
            CheckBoxM14.Checked = true;
            CheckBoxM15.Checked = true;
            CheckBoxM16.Checked = true;
            CheckBoxM17.Checked = true;
            CheckBoxM18.Checked = true;
        }

        protected void ButtonTuesday_Click1(object sender, EventArgs e)
        {
            CheckBoxT10.Checked = true;
            CheckBoxT11.Checked = true;
            CheckBoxT12.Checked = true;
            CheckBoxT13.Checked = true;
            CheckBoxT14.Checked = true;
            CheckBoxT15.Checked = true;
            CheckBoxT16.Checked = true;
            CheckBoxT17.Checked = true;
            CheckBoxT18.Checked = true;
        }

        protected void ButtonWednesday_Click1(object sender, EventArgs e)
        {
            CheckBoxW10.Checked = true;
            CheckBoxW11.Checked = true;
            CheckBoxW12.Checked = true;
            CheckBoxW13.Checked = true;
            CheckBoxW14.Checked = true;
            CheckBoxW15.Checked = true;
            CheckBoxW16.Checked = true;
            CheckBoxW17.Checked = true;
            CheckBoxW18.Checked = true;
        }

        protected void ButtonThursday_Click1(object sender, EventArgs e)
        {
            CheckBoxJ10.Checked = true;
            CheckBoxJ11.Checked = true;
            CheckBoxJ12.Checked = true;
            CheckBoxJ13.Checked = true;
            CheckBoxJ14.Checked = true;
            CheckBoxJ15.Checked = true;
            CheckBoxJ16.Checked = true;
            CheckBoxJ17.Checked = true;
            CheckBoxJ18.Checked = true;
        }

        protected void ButtonFriday_Click1(object sender, EventArgs e)
        {
            CheckBoxF10.Checked = true;
            CheckBoxF11.Checked = true;
            CheckBoxF12.Checked = true;
            CheckBoxF13.Checked = true;
            CheckBoxF14.Checked = true;
            CheckBoxF15.Checked = true;
            CheckBoxF16.Checked = true;
            CheckBoxF17.Checked = true;
            CheckBoxF18.Checked = true;
        }

        protected void ButtonPeriod1_Click1(object sender, EventArgs e)
        {
            CheckBoxM10.Checked = true;
            CheckBoxT10.Checked = true;
            CheckBoxW10.Checked = true;
            CheckBoxJ10.Checked = true;
            CheckBoxF10.Checked = true;
        }

        protected void ButtonPeriod2_Click1(object sender, EventArgs e)
        {
            CheckBoxM11.Checked = true;
            CheckBoxT11.Checked = true;
            CheckBoxW11.Checked = true;
            CheckBoxJ11.Checked = true;
            CheckBoxF11.Checked = true;
        }

        protected void ButtonPeriod3_Click1(object sender, EventArgs e)
        {
            CheckBoxM12.Checked = true;
            CheckBoxT12.Checked = true;
            CheckBoxW12.Checked = true;
            CheckBoxJ12.Checked = true;
            CheckBoxF12.Checked = true;
        }

        protected void ButtonPeriod4_Click1(object sender, EventArgs e)
        {
            CheckBoxM13.Checked = true;
            CheckBoxT13.Checked = true;
            CheckBoxW13.Checked = true;
            CheckBoxJ13.Checked = true;
            CheckBoxF13.Checked = true;
        }

        protected void ButtonPeriod5_Click1(object sender, EventArgs e)
        {
            CheckBoxM14.Checked = true;
            CheckBoxT14.Checked = true;
            CheckBoxW14.Checked = true;
            CheckBoxJ14.Checked = true;
            CheckBoxF14.Checked = true;
        }

        protected void ButtonPeriod6_Click1(object sender, EventArgs e)
        {
            CheckBoxM15.Checked = true;
            CheckBoxT15.Checked = true;
            CheckBoxW15.Checked = true;
            CheckBoxJ15.Checked = true;
            CheckBoxF15.Checked = true;
        }

        protected void ButtonPeriod7_Click1(object sender, EventArgs e)
        {
            CheckBoxM16.Checked = true;
            CheckBoxT16.Checked = true;
            CheckBoxW16.Checked = true;
            CheckBoxJ16.Checked = true;
            CheckBoxF16.Checked = true;
        }

        protected void ButtonPeriod8_Click1(object sender, EventArgs e)
        {
            CheckBoxM17.Checked = true;
            CheckBoxT17.Checked = true;
            CheckBoxW17.Checked = true;
            CheckBoxJ17.Checked = true;
            CheckBoxF17.Checked = true;
        }

        protected void ButtonPeriod9_Click1(object sender, EventArgs e)
        {
            CheckBoxM18.Checked = true;
            CheckBoxT18.Checked = true;
            CheckBoxW18.Checked = true;
            CheckBoxJ18.Checked = true;
            CheckBoxF18.Checked = true;
        }

        protected void ButtonClearPeriods_Click1(object sender, EventArgs e)
        {
            CheckBoxM11.Checked = false;
            CheckBoxM12.Checked = false;
            CheckBoxM13.Checked = false;
            CheckBoxM14.Checked = false;
            CheckBoxM15.Checked = false;
            CheckBoxM16.Checked = false;
            CheckBoxM17.Checked = false;
            CheckBoxM18.Checked = false;
            CheckBoxM10.Checked = false;
            CheckBoxT11.Checked = false;
            CheckBoxT12.Checked = false;
            CheckBoxT13.Checked = false;
            CheckBoxT14.Checked = false;
            CheckBoxT15.Checked = false;
            CheckBoxT16.Checked = false;
            CheckBoxT17.Checked = false;
            CheckBoxT18.Checked = false;
            CheckBoxT10.Checked = false;
            CheckBoxW11.Checked = false;
            CheckBoxW12.Checked = false;
            CheckBoxW13.Checked = false;
            CheckBoxW14.Checked = false;
            CheckBoxW15.Checked = false;
            CheckBoxW16.Checked = false;
            CheckBoxW17.Checked = false;
            CheckBoxW18.Checked = false;
            CheckBoxW10.Checked = false;
            CheckBoxJ11.Checked = false;
            CheckBoxJ12.Checked = false;
            CheckBoxJ13.Checked = false;
            CheckBoxJ14.Checked = false;
            CheckBoxJ15.Checked = false;
            CheckBoxJ16.Checked = false;
            CheckBoxJ17.Checked = false;
            CheckBoxJ18.Checked = false;
            CheckBoxJ10.Checked = false;
            CheckBoxF11.Checked = false;
            CheckBoxF12.Checked = false;
            CheckBoxF13.Checked = false;
            CheckBoxF14.Checked = false;
            CheckBoxF15.Checked = false;
            CheckBoxF16.Checked = false;
            CheckBoxF17.Checked = false;
            CheckBoxF18.Checked = false;
            CheckBoxF10.Checked = false;
        }

        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            SqlConnection connection7 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connection7.Open();
            // initialise the checkbox array
            CheckBox[,] boxes = { { CheckBoxM1, CheckBoxM2, CheckBoxM3, CheckBoxM4, CheckBoxM5, CheckBoxM6, CheckBoxM7, CheckBoxM8, CheckBoxM9 }, 
                                                                    {CheckBoxT1, CheckBoxT2, CheckBoxT3, CheckBoxT4, CheckBoxT5, CheckBoxT6, CheckBoxT7, CheckBoxT8, CheckBoxT9 },
                                                                    {CheckBoxW1, CheckBoxW2, CheckBoxW3, CheckBoxW4, CheckBoxW5, CheckBoxW6, CheckBoxW7, CheckBoxW8, CheckBoxW9  },
                                                                    {CheckBoxJ1, CheckBoxJ2, CheckBoxJ3, CheckBoxJ4, CheckBoxJ5, CheckBoxJ6, CheckBoxJ7, CheckBoxJ8, CheckBoxJ9  },
                                                                    {CheckBoxM1, CheckBoxF2, CheckBoxF3, CheckBoxF4, CheckBoxF5, CheckBoxF6, CheckBoxF7, CheckBoxF8, CheckBoxF9  }};

            bool[] mondayrequest = new bool[10] { false, false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < 9; i++)
            {
                if (boxes[0, i].Checked)
                {
                    mondayrequest[i] = true;
                }
            }
            bool[] tuesdayrequest = new bool[10] { false, false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < 9; i++)
            {
                if (boxes[1, i].Checked)
                {
                    tuesdayrequest[i] = true;
                }
            }
            bool[] wednesdayrequest = new bool[10] { false, false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < 9; i++)
            {
                if (boxes[2, i].Checked)
                {
                    wednesdayrequest[i] = true;
                }
            }
            bool[] thursdayrequest = new bool[10] { false, false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < 9; i++)
            {
                if (boxes[3, i].Checked)
                {
                    thursdayrequest[i] = true;
                }
            }
            bool[] fridayrequest = new bool[10] { false, false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < 9; i++)
            {
                if (boxes[4, i].Checked)
                {
                    fridayrequest[i] = true;
                }
            }

            //Retrieve module name
            string moduleName = DropDownListModuleByRoom.SelectedValue;
            string modulesquery = "SELECT moduleCode FROM [Module] WHERE moduleTitle ='" + moduleName + "'";
            SqlCommand modulessql = new SqlCommand(modulesquery, connection7);
            string moduleCodeText = modulessql.ExecuteScalar().ToString();

            int weeknumber = DropDownListWeekNumber.SelectedIndex;
            int[] weeksarray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            weeksarray[weeknumber] = 1;

            int weekIDText;
            string weekquery = "SELECT COUNT(weekID) FROM [Week] WHERE week1= " + weeksarray[0] + " AND week2= " + weeksarray[1] + " AND week3= " + weeksarray[2] + " AND week4= " + weeksarray[3] + " AND week5= " + weeksarray[4] + " AND week6= " + weeksarray[5] + " AND week7= " + weeksarray[6] + " AND week8= " + weeksarray[7] + " AND week9= " + weeksarray[8] + " AND week10= " + weeksarray[9] + " AND week11= " + weeksarray[10] + " AND week12= " + weeksarray[11] + " AND week13= " + weeksarray[12] + " AND week14= " + weeksarray[13] + " AND week15= " + weeksarray[14];
            SqlCommand weeksql = new SqlCommand(weekquery, connection7);
            int testtwo = Convert.ToInt32(weeksql.ExecuteScalar().ToString());

            /* If there is a corresponding weekID in database, select it */
            if (testtwo != 0)
            {
                string weekquery2 = "SELECT weekID FROM [Week] WHERE week1= " + weeksarray[0] + " AND week2= " + weeksarray[1] + " AND week3= " + weeksarray[2] + " AND week4= " + weeksarray[3] + " AND week5= " + weeksarray[4] + " AND week6= " + weeksarray[5] + " AND week7= " + weeksarray[6] + " AND week8= " + weeksarray[7] + " AND week9= " + weeksarray[8] + " AND week10= " + weeksarray[9] + " AND week11= " + weeksarray[10] + " AND week12= " + weeksarray[11] + " AND week13= " + weeksarray[12] + " AND week14= " + weeksarray[13] + " AND week15= " + weeksarray[14];
                SqlCommand weeksql2 = new SqlCommand(weekquery2, connection7);
                weekIDText = Convert.ToInt32(weeksql2.ExecuteScalar().ToString());
            }
            /* If there is no corresponding weekID, make a new one and use that */
            else
            {
                string insweekquery = "INSERT INTO [Week] VALUES(" + weeksarray[0] + "," + weeksarray[1] + "," + weeksarray[2] + "," + weeksarray[3] + "," + weeksarray[4] + "," + weeksarray[5] + "," + weeksarray[6] + "," + weeksarray[7] + "," + weeksarray[8] + "," + weeksarray[9] + "," + weeksarray[10] + "," + weeksarray[11] + "," + weeksarray[12] + "," + weeksarray[13] + "," + weeksarray[14] + ")";

                SqlCommand insweeksql = new SqlCommand(insweekquery, connection7);
                insweeksql.ExecuteNonQuery();
                string newweek = "SELECT MAX(weekID) FROM [Week]";
                SqlCommand maxweeksql = new SqlCommand(newweek, connection7);
                weekIDText = Convert.ToInt32(maxweeksql.ExecuteScalar().ToString());
            }

            string selectedroom = DropDownListRooms.Text;


            string getroomid = "SELECT roomID FROM [Room] WHERE roomName='" + selectedroom + "'";
            SqlCommand getroomidsql = new SqlCommand(getroomid, connection7);
            int roomid = Convert.ToInt32(getroomidsql.ExecuteScalar().ToString());

            if (dayLabel.Text != "")
            {
                for (int i = 0; i < 9; i++)
                {
                    if (mondayrequest[i] == true)
                    {
                        int startTime = i + 1;
                        int duration = 0;
                        int n = i - 1;
                        bool ended = true;
                        while (ended)
                        {
                            n++;
                            switch (mondayrequest[n] == true && mondayrequest[n + 1] == true)
                            {
                                case true:
                                    duration++;
                                    i++;
                                    break;
                                case false:
                                    int endTime = startTime + duration;
                                    string insreq = "INSERT INTO [Request] VALUES ('" + moduleCodeText + "','Accepted'," + weekIDText + ",'Monday'," + startTime + "," + endTime + ",1,2014,3)";
                                    SqlCommand insreqsql = new SqlCommand(insreq, connection7);
                                    insreqsql.ExecuteNonQuery();
                                    string booked = "INSERT INTO [BookedRoom] VALUES ((SELECT MAX(requestID) FROM [Request])," + roomid + ")";
                                    SqlCommand bookedsql = new SqlCommand(booked, connection7);
                                    bookedsql.ExecuteNonQuery();
                                    ended = false;
                                    break;
                            }
                        }
                    }
                }
                for (int i = 0; i < 9; i++)
                {
                    if (tuesdayrequest[i] == true)
                    {
                        int startTime = i + 1;
                        int duration = 0;
                        int n = i - 1;
                        bool ended = true;
                        while (ended)
                        {
                            n++;
                            switch (tuesdayrequest[n] == true && tuesdayrequest[n + 1] == true)
                            {
                                case true:
                                    duration++;
                                    i++;
                                    break;
                                case false:
                                    int endTime = startTime + duration;
                                    string insreq = "INSERT INTO [Request] VALUES ('" + moduleCodeText + "','Accepted'," + weekIDText + ",'Tuesday'," + startTime + "," + endTime + ",1,2014,3)";
                                    SqlCommand insreqsql = new SqlCommand(insreq, connection7);
                                    insreqsql.ExecuteNonQuery();
                                    string booked = "INSERT INTO [BookedRoom] VALUES ((SELECT MAX(requestID) FROM [Request])," + roomid + ")";
                                    SqlCommand bookedsql = new SqlCommand(booked, connection7);
                                    bookedsql.ExecuteNonQuery();
                                    ended = false;
                                    break;
                            }
                        }
                    }
                }
                for (int i = 0; i < 9; i++)
                {
                    if (wednesdayrequest[i] == true)
                    {
                        int startTime = i + 1;
                        int duration = 0;
                        int n = i - 1;
                        bool ended = true;
                        while (ended)
                        {
                            n++;
                            switch (wednesdayrequest[n] == true && wednesdayrequest[n + 1] == true)
                            {
                                case true:
                                    duration++;
                                    i++;
                                    break;
                                case false:
                                    int endTime = startTime + duration;
                                    string insreq = "INSERT INTO [Request] VALUES ('" + moduleCodeText + "','Accepted'," + weekIDText + ",'Wednesday'," + startTime + "," + endTime + ",1,2014,3)";
                                    SqlCommand insreqsql = new SqlCommand(insreq, connection7);
                                    insreqsql.ExecuteNonQuery();
                                    string booked = "INSERT INTO [BookedRoom] VALUES ((SELECT MAX(requestID) FROM [Request])," + roomid + ")";
                                    SqlCommand bookedsql = new SqlCommand(booked, connection7);
                                    bookedsql.ExecuteNonQuery();
                                    ended = false;
                                    break;
                            }
                        }
                    }
                }
                for (int i = 0; i < 9; i++)
                {
                    if (thursdayrequest[i] == true)
                    {
                        int startTime = i + 1;
                        int duration = 0;
                        int n = i - 1;
                        bool ended = true;
                        while (ended)
                        {
                            n++;
                            switch (thursdayrequest[n] == true && thursdayrequest[n + 1] == true)
                            {
                                case true:
                                    duration++;
                                    i++;
                                    break;
                                case false:
                                    int endTime = startTime + duration;
                                    string insreq = "INSERT INTO [Request] VALUES ('" + moduleCodeText + "','Accepted'," + weekIDText + ",'Thursday'," + startTime + "," + endTime + ",1,2014,3)";
                                    SqlCommand insreqsql = new SqlCommand(insreq, connection7);
                                    insreqsql.ExecuteNonQuery();
                                    string booked = "INSERT INTO [BookedRoom] VALUES ((SELECT MAX(requestID) FROM [Request])," + roomid + ")";
                                    SqlCommand bookedsql = new SqlCommand(booked, connection7);
                                    bookedsql.ExecuteNonQuery();
                                    ended = false;
                                    break;
                            }
                        }
                    }
                }
                for (int i = 0; i < 9; i++)
                {
                    if (fridayrequest[i] == true)
                    {
                        int startTime = i + 1;
                        int duration = 0;
                        int n = i - 1;
                        bool ended = true;
                        while (ended)
                        {
                            n++;
                            switch (fridayrequest[n] == true && fridayrequest[n + 1] == true)
                            {
                                case true:
                                    duration++;
                                    i++;
                                    break;
                                case false:
                                    int endTime = startTime + duration;
                                    string insreq = "INSERT INTO [Request] VALUES ('" + moduleCodeText + "','Accepted'," + weekIDText + ",'Friday'," + startTime + "," + endTime + ",1,2014,3)";
                                    SqlCommand insreqsql = new SqlCommand(insreq, connection7);
                                    insreqsql.ExecuteNonQuery();
                                    string booked = "INSERT INTO [BookedRoom] VALUES ((SELECT MAX(requestID) FROM [Request])," + roomid + ")";
                                    SqlCommand bookedsql = new SqlCommand(booked, connection7);
                                    bookedsql.ExecuteNonQuery();
                                    ended = false;
                                    break;
                            }
                        }
                    }
                }
            }
            else LabelError.Text = "Please Select at least 1 period";
           
        }
        public void ShowBookedDetails()
        {
            //Declare variables where details will be stored
            int requestid = 0;
            int semester = 0;
            int year = 0;
            string department = "";
            string moduleTitle = "";
            string day = "";
            int weekid = 0;
            string weeks = "";
            string status = "";
            int periodStart = 0;
            int periodEnd = 0;
            string bookedRoom = "";
            string alternateRoom = "";
            string facilities = "";
            string building = "";
            string park = "";


            //Make the div visible that shows the details on screen
            this.divBookingByRoom.Visible = false;
            requestDetails.Visible = true;
            
            
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            //Retrieve the request ID
            connect.Open();
            string getrequestID = "SELECT requestID FROM [Request] WHERE moduleCode='" + moduleCode + "'";
            SqlCommand getrequestIDCommand = new SqlCommand(getrequestID, connect);
            SqlDataReader getRequestID = getrequestIDCommand.ExecuteReader();
            if (getRequestID.Read())
            {
                requestid = getRequestID.GetInt32(0);
            }
            connect.Close();
            connect.Open();
            string requestdetails = "Select * FROM [Request] where requestID=" + requestid;
            SqlCommand requestcommand = new SqlCommand(requestdetails, connect);
            //Retrieve the request details and store them in appropriate Variables
            SqlDataReader timedetails = requestcommand.ExecuteReader();
            if (timedetails.Read())
            {
                moduleCode = timedetails.GetString(1);
                status = timedetails.GetString(2);
                weekid = timedetails.GetInt32(3);
                day = timedetails.GetString(4);
                periodStart = timedetails.GetInt32(5);
                periodEnd = timedetails.GetInt32(6);
                semester = timedetails.GetInt32(7);
                year = timedetails.GetInt32(8);
            }
            connect.Close();
            connect.Open();
            string requestfacdetails = "SELECT facilityName FROM [Facility] LEFT JOIN [RequestFacilities] ON [Facility].facilityID = [RequestFacilities].facilityID WHERE [RequestFacilities].requestID = " + requestid;
            SqlCommand requestfaccommand = new SqlCommand(requestfacdetails, connect);
            //Retrieve the request details and store them in appropriate Variables
            SqlDataReader facdetails = requestfaccommand.ExecuteReader();
            while (facdetails.Read())
            {
                facilities += facdetails.GetString(0) + ", ";
            }
            connect.Close();
            connect.Open();
            string roomdetails = "SELECT roomName FROM [Room] LEFT JOIN [BookedRoom] ON [Room].roomID = [BookedRoom].roomID LEFT JOIN [Request] ON [BookedRoom].requestID = [Request].requestID WHERE [Request].requestID = " + requestid;
            SqlCommand roomcommand = new SqlCommand(roomdetails, connect);
            //Retrieve the request details and store them in appropriate Variables
            SqlDataReader bookedroomdetails = roomcommand.ExecuteReader();
            while (bookedroomdetails.Read())
            {
                bookedRoom += bookedroomdetails.GetString(0) + ", ";
            }
            connect.Close();
            connect.Open();
            string alternateroomdetails = "SELECT roomName FROM [Room] LEFT JOIN [BookedRoom] ON [Room].roomID = [BookedRoom].roomID LEFT JOIN [Request] ON [BookedRoom].requestID = [Request].requestID WHERE [Request].requestID = " + requestid;
            SqlCommand altroomcommand = new SqlCommand(alternateroomdetails, connect);
            //Retrieve the request details and store them in appropriate Variables
            SqlDataReader altroomdetails = altroomcommand.ExecuteReader();
            while (altroomdetails.Read())
            {
                alternateRoom += altroomdetails.GetString(0) + ", ";
            }
            connect.Close();
            connect.Open();
            string modtitledetails = "SELECT moduleTitle FROM [Module] WHERE moduleCode ='" + moduleCode + "'";
            SqlCommand moduletitlecommand = new SqlCommand(modtitledetails, connect);
            //Retrieve the request details and store them in appropriate Variables
            SqlDataReader moduletitledetails = moduletitlecommand.ExecuteReader();
            if (moduletitledetails.Read())
            {
                moduleTitle = moduletitledetails.GetString(0);
            }
            connect.Close();
            connect.Open();
            string deptdetails = String.Format("SELECT deptCode FROM [User] WHERE userID = {0}", userID);
            SqlCommand departmentcommand = new SqlCommand(deptdetails, connect);
            //Retrieve the request details and store them in appropriate Variables
            SqlDataReader departmentdetails = departmentcommand.ExecuteReader();
            if (departmentdetails.Read())
            {
                department = departmentdetails.GetString(0);
            }
            connect.Close();
            connect.Open();
            int weekval;
            string wk = "SELECT week1,week2,week3,week4,week5,week6,week7,week8,week9,week10,week11,week12,week13,week14,week15 FROM [Week] WHERE weekID=" + weekid;
            SqlCommand weeksql = new SqlCommand(wk, connect);
            SqlDataReader getweek = weeksql.ExecuteReader();
            if (getweek.Read())
            {
                for (int n = 0; n < 15; n++)
                {
                    weekval = getweek.GetInt32(n);
                    if (weekval == 1)
                        weeks += (n + 1) + ", ";
                }
            }
            connect.Close();
            connect.Open();
            string builddetails = "SELECT buildingName FROM [Building] LEFT JOIN [Room] ON [Building].buildingID = [Room].buildingID LEFT JOIN [BookedRoom] ON [Room].roomID = [BookedRoom].roomID WHERE [BookedRoom].requestID =" + requestid;
            SqlCommand buildingcommand = new SqlCommand(builddetails, connect);
            //Retrieve the request details and store them in appropriate Variables
            SqlDataReader buildingdetails = buildingcommand.ExecuteReader();
            if (buildingdetails.Read())
            {
                building = buildingdetails.GetString(0);
            }
            connect.Close();
            connect.Open();
            string parknamedetails = "SELECT parkName FROM [Park] LEFT JOIN [Building] ON [Park].parkID = [Building].parkID WHERE [Building].buildingName ='" + building + "'";
            SqlCommand parkcommand = new SqlCommand(parknamedetails, connect);
            //Retrieve the request details and store them in appropriate Variables
            SqlDataReader parkdetails = parkcommand.ExecuteReader();
            if (parkdetails.Read())
            {
                park = parkdetails.GetString(0);
            }
            connect.Close();

            //Output them in the textboxes.
            requestidLabel.Text = Convert.ToString(requestid);
            semesterLabel.Text = Convert.ToString(semester);
            yearLabel.Text = Convert.ToString(year);
            departmentLabel.Text = department;
            modulecodeLabel.Text = moduleCode;
            moduletitleLabel.Text = moduleTitle;
            dayLabel.Text = day;
            if (weeks.Length != 0)
                weeks = weeks.Substring(0, weeks.Length - 2);
            weeksLabel.Text = weeks;
            statusLabel.Text = status;
            if (periodStart == periodEnd)
                periodLabel.Text = Convert.ToString(periodStart);
            else
                periodLabel.Text = periodStart + " - " + periodEnd;
            if (bookedRoom.Length != 0)
                bookedRoom = bookedRoom.Substring(0, bookedRoom.Length - 2);
            bookedroomLabel.Text = bookedRoom;
            if (alternateRoom.Length != 0)
            {
                alternateRoom = alternateRoom.Substring(0, alternateRoom.Length - 2);
                altroomLabel.Text = alternateRoom;
            }
            else
                altroomLabel.Text = "N/A";
            if (facilities.Length != 0)
            {
                facilities = facilities.Substring(0, facilities.Length - 2);
                facilitiesLabel.Text = facilities;
            }
            else
                facilitiesLabel.Text = "N/A";
            buildingLabel.Text = building;
            parkLabel.Text = park;
        }

        protected void ButtonHideDetails_Click(object sender, EventArgs e)
        {
            //Button to hide details
            this.requestDetails.Visible = false;
        }

        protected void LabelM1_Click(object sender, EventArgs e)
        {
            moduleCode = LabelM1.Text;
            ShowBookedDetails();
        }
        protected void LabelM2_Click(object sender, EventArgs e)
        {
            moduleCode = LabelM2.Text;
            ShowBookedDetails();
        }

        protected void LabelM3_Click(object sender, EventArgs e)
        {
            moduleCode = LabelM3.Text;
            ShowBookedDetails();
        }

        protected void LabelM4_Click(object sender, EventArgs e)
        {
            moduleCode = LabelM4.Text;
            ShowBookedDetails();
        }

        protected void LabelM5_Click(object sender, EventArgs e)
        {
            moduleCode = LabelM5.Text;
            ShowBookedDetails();
        }

        protected void LabelM6_Click(object sender, EventArgs e)
        {
            moduleCode = LabelM6.Text;
            ShowBookedDetails();
        }

        protected void LabelM7_Click(object sender, EventArgs e)
        {
            moduleCode = LabelM7.Text;
            ShowBookedDetails();
        }

        protected void LabelM8_Click(object sender, EventArgs e)
        {
            moduleCode = LabelM8.Text;
            ShowBookedDetails();
        }

        protected void LabelM9_Click(object sender, EventArgs e)
        {
            moduleCode = LabelM9.Text;
            ShowBookedDetails();
        }

        protected void LabelT1_Click(object sender, EventArgs e)
        {
            moduleCode = LabelT1.Text;
            ShowBookedDetails();
        }

        protected void LabelT2_Click(object sender, EventArgs e)
        {
            moduleCode = LabelT2.Text;
            ShowBookedDetails();
        }

        protected void LabelT3_Click(object sender, EventArgs e)
        {
            moduleCode = LabelT3.Text;
            ShowBookedDetails();
        }

        protected void LabelT4_Click(object sender, EventArgs e)
        {
            moduleCode = LabelT4.Text;
            ShowBookedDetails();
        }

        protected void LabelT5_Click(object sender, EventArgs e)
        {
            moduleCode = LabelT5.Text;
            ShowBookedDetails();
        }

        protected void LabelT6_Click(object sender, EventArgs e)
        {
            moduleCode = LabelT6.Text;
            ShowBookedDetails();
        }

        protected void LabelT7_Click(object sender, EventArgs e)
        {
            moduleCode = LabelT7.Text;
            ShowBookedDetails();
        }

        protected void LabelT8_Click(object sender, EventArgs e)
        {
            moduleCode = LabelT8.Text;
            ShowBookedDetails();
        }

        protected void LabelT9_Click(object sender, EventArgs e)
        {
            moduleCode = LabelT9.Text;
            ShowBookedDetails();
        }

        protected void LabelW1_Click(object sender, EventArgs e)
        {
            moduleCode = LabelW1.Text;
            ShowBookedDetails();
        }

        protected void LabelW2_Click(object sender, EventArgs e)
        {
            moduleCode = LabelW2.Text;
            ShowBookedDetails();
        }

        protected void LabelW3_Click(object sender, EventArgs e)
        {
            moduleCode = LabelW3.Text;
            ShowBookedDetails();
        }

        protected void LabelW4_Click(object sender, EventArgs e)
        {
            moduleCode = LabelW4.Text;
            ShowBookedDetails();
        }

        protected void LabelW5_Click(object sender, EventArgs e)
        {
            moduleCode = LabelW5.Text;
            ShowBookedDetails();
        }

        protected void LabelW6_Click(object sender, EventArgs e)
        {
            moduleCode = LabelW6.Text;
            ShowBookedDetails();
        }

        protected void LabelW7_Click(object sender, EventArgs e)
        {
            moduleCode = LabelW7.Text;
            ShowBookedDetails();
        }

        protected void LabelW8_Click(object sender, EventArgs e)
        {
            moduleCode = LabelW8.Text;
            ShowBookedDetails();
        }

        protected void LabelW9_Click(object sender, EventArgs e)
        {
            moduleCode = LabelW9.Text;
            ShowBookedDetails();
        }

        protected void LabelJ1_Click(object sender, EventArgs e)
        {
            moduleCode = LabelJ1.Text;
            ShowBookedDetails();
        }

        protected void LabelJ2_Click(object sender, EventArgs e)
        {
            moduleCode = LabelJ2.Text;
            ShowBookedDetails();
        }

        protected void LabelJ3_Click(object sender, EventArgs e)
        {
            moduleCode = LabelJ3.Text;
            ShowBookedDetails();
        }

        protected void LabelJ4_Click(object sender, EventArgs e)
        {
            moduleCode = LabelJ4.Text;
            ShowBookedDetails();
        }

        protected void LabelJ5_Click(object sender, EventArgs e)
        {
            moduleCode = LabelJ5.Text;
            ShowBookedDetails();
        }

        protected void LabelJ6_Click(object sender, EventArgs e)
        {
            moduleCode = LabelJ6.Text;
            ShowBookedDetails();
        }

        protected void LabelJ7_Click(object sender, EventArgs e)
        {
            moduleCode = LabelJ7.Text;
            ShowBookedDetails();
        }

        protected void LabelJ8_Click(object sender, EventArgs e)
        {
            moduleCode = LabelJ8.Text;
            ShowBookedDetails();
        }

        protected void LabelJ9_Click(object sender, EventArgs e)
        {
            moduleCode = LabelJ9.Text;
            ShowBookedDetails();
        }

        protected void LabelF1_Click(object sender, EventArgs e)
        {
            moduleCode = LabelF1.Text;
            ShowBookedDetails();
        }

        protected void LabelF2_Click(object sender, EventArgs e)
        {
            moduleCode = LabelF2.Text;
            ShowBookedDetails();
        }

        protected void LabelF3_Click(object sender, EventArgs e)
        {
            moduleCode = LabelF3.Text;
            ShowBookedDetails();
        }

        protected void LabelF4_Click(object sender, EventArgs e)
        {
            moduleCode = LabelF4.Text;
            ShowBookedDetails();
        }

        protected void LabelF5_Click(object sender, EventArgs e)
        {
            moduleCode = LabelF5.Text;
            ShowBookedDetails();
        }

        protected void LabelF6_Click(object sender, EventArgs e)
        {
            moduleCode = LabelF6.Text;
            ShowBookedDetails();
        }

        protected void LabelF7_Click(object sender, EventArgs e)
        {
            moduleCode = LabelF7.Text;
            ShowBookedDetails();
        }

        protected void LabelF8_Click(object sender, EventArgs e)
        {
            moduleCode = LabelF8.Text;
            ShowBookedDetails();
        }

        protected void LabelF9_Click(object sender, EventArgs e)
        {
            moduleCode = LabelF9.Text;
            ShowBookedDetails();
        }

        protected void ButtonCancel_Click(object sender, EventArgs e) 
        {
            this.divBookingByRoom.Visible = false;
        }
    }
}