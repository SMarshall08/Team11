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
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;

namespace Team11
{
    public partial class Schedule : System.Web.UI.Page
    {
        List<string> moduleslist = new List<string>();
        List<string> roomslist = new List<string>();
        List<int> requests = new List<int>();
        List<string> park = new List<string>();
        List<string> building = new List<string>();
        List<int> semester = new List<int>();
        List<int> year = new List<int>();
        List<int> capacity = new List<int>();
        List<string> facilities = new List<string>();
        List<string> alternateRooms = new List<string>();
        List<string> modTitle = new List<string>();
        List<string> period = new List<string>();
        List<string> weeks = new List<string>();
        List<string> day = new List<string>();
        List<string> room = new List<string>();
        List<string> status = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //DropDownListFilterModule.Items.Add("Please Select a Module To filter By:");
                DropDownListFilterModule.Items.Insert(0, "Please Select a Module to Filter By:");
                string getModule = "SELECT moduleCode, moduleTitle FROM [Module] WHERE userID=" + Session["userID"] + "ORDER BY moduleTitle"; //Session["userID"] is intialised upon login.


                SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                connect.Open();
                SqlCommand getModuleSql = new SqlCommand(getModule, connect);
                SqlDataReader getmoduledata = getModuleSql.ExecuteReader();
                while (getmoduledata.Read())
                {
                    ListItem moduleItem = new ListItem(getmoduledata.GetString(1) + " / " + getmoduledata.GetString(0), getmoduledata.GetString(0));
                    DropDownListFilterModule.Items.Add(moduleItem);
                }
                connect.Close();
                DropDownListFilterStaff.Items.Insert(0, "Please Select a Staff Member to Filter By:");

                string getStaff = @"
               SELECT FirstName, LastName, Staff.StaffID
FROM Staff 
WHERE Staff.userID =" + Session["userID"] + "ORDER BY LastName";

                SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                connect2.Open();
                SqlCommand getStaffSql = new SqlCommand(getStaff, connect2);
                SqlDataReader getstaffdata = getStaffSql.ExecuteReader();
                while (getstaffdata.Read())
                {
                    ListItem StaffItem = new ListItem(getstaffdata.GetString(0) + "  " + getstaffdata.GetString(1), getstaffdata.GetInt32(2).ToString());
                    DropDownListFilterStaff.Items.Add(StaffItem);
                }
                connect2.Close();
            }
        }

        protected void RadioButtonScheduleView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Search preference by Room or Date, hide the unselected one
            if (this.RadioButtonScheduleView.SelectedIndex == 0)
            {
                this.divByModule.Visible = true;
                this.divByStaff.Visible = false;

            }
            else
            {
                this.divByModule.Visible = false;
                this.divByStaff.Visible = true;

            }

        }


        protected void DropDownListFilterModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule();
        }

        protected void DropDownListFilterStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule2();
        }
        protected void DropDownListFilterPartStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule2();
        }

        private void RegenerateSchedule()
        {
            string[,] schedule = { { "", "", "", "", "", "", "", "", "", "" },
                                 { "", "", "", "", "", "", "", "", "", "" },
                                 { "", "", "", "", "", "", "", "", "", "" },
                                 { "", "", "", "", "", "", "", "", "", "" },
                                 { "", "", "", "", "", "", "", "", "", "" }
                                 };

            // query the schedule
            //while ()
            string getschedule = @"
SELECT day, buildingName, roomName, moduleCode , periodStart, periodEnd
FROM [request] 
inner join BookedRoom on BookedRoom.requestID = request.requestID
inner join room on BookedRoom.roomID = room.roomid
inner join Building on building.buildingID = room.buildingID
inner join Park ON park.parkid = building.parkid
inner join Week on week.weekID = request.weekID
";

            // add the week where clause
            getschedule += "WHERE Week" + DropDownListFilterWeek.SelectedValue + " = 1 ";

            if (DropDownListFilterModule.SelectedIndex != 0)
                getschedule += "AND Request.moduleCode = '" + DropDownListFilterModule.SelectedValue + "' ";

            if (DropDownListFilterPark.SelectedIndex != 0)
                getschedule += "AND Park.ParkID = '" + DropDownListFilterPark.SelectedValue + "' ";

            if (DropDownListFilterModule.SelectedIndex == 0 && DropDownListFilterPart.SelectedIndex > 0)
            {

                getschedule += "AND Charindex('" + DropDownListFilterPart.SelectedItem.Text + "',moduleCode) = 3";
            }

            if (DropDownListFilterYear.SelectedIndex != 0)
                getschedule += "AND Request.Year = '" + DropDownListFilterYear.SelectedValue + "' ";

            if (RadioButtonListFilterSemester.SelectedIndex != 0)
                getschedule += "AND Request.Semester = '" + RadioButtonListFilterSemester.SelectedValue + "' ";

            if (RadioButtonListFilterStatus.SelectedIndex != 0)
                getschedule += "AND Request.Status = '" + RadioButtonListFilterStatus.SelectedItem.Text + "' ";

            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect2.Open();
            SqlCommand getscheduleSql = new SqlCommand(getschedule, connect2);
            SqlDataReader getScheduleData = getscheduleSql.ExecuteReader();
            while (getScheduleData.Read())
            {
                string dayName = getScheduleData.GetString(getScheduleData.GetOrdinal("day"));
                string buildingName = getScheduleData.GetString(getScheduleData.GetOrdinal("buildingName"));
                string roomName = getScheduleData.GetString(getScheduleData.GetOrdinal("roomName"));
                string moduleCode = getScheduleData.GetString(getScheduleData.GetOrdinal("moduleCode"));

                // switch statement to translate day name to day index
                int day = 0;
                switch (dayName)
                {
                    case "Monday":
                        day = 0;
                        break;
                    case "Tuesday":
                        day = 1;
                        break;
                    case "Wednesday":
                        day = 2;
                        break;
                    case "Thursday":
                        day = 3;
                        break;
                    case "Friday":
                        day = 4;
                        break;
                }

                int period = getScheduleData.GetInt32(getScheduleData.GetOrdinal("periodStart"));
                schedule[day, period] = schedule[day, period] + "Building Name: " + buildingName + " Room Name: " + roomName + " Module Code: " + moduleCode + "\r\n";

            }

            HtmlTable myTable = new HtmlTable();
            myTable.Border = 1;
            myTable.ID = "ScheduleTable";
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell("th");
            cell.InnerText = "Day";
            row.Cells.Add(cell);
            for (int period = 1; period <= 9; period++)
            {
                cell = new HtmlTableCell("th");
                cell.InnerText = period.ToString();
                row.Cells.Add(cell);
            }
            myTable.Rows.Add(row);

            string[] dayNames = { "Monday", "Tuesday", "wednesday", "Thursday", "Friday" };
            for (int day = 0; day <= 4; day++)
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell("th");
                cell.InnerText = dayNames[day];
                row.Cells.Add(cell);
                for (int period = 1; period <= 9; period++)
                {
                    cell = new HtmlTableCell("td");
                    cell.InnerText = schedule[day, period];
                    row.Cells.Add(cell);
                }
                myTable.Rows.Add(row);
            }
            ViewTable.Controls.Clear();
            ViewTable.Controls.Add(myTable);
        }

        private void RegenerateSchedule2()
        {
            string[,] schedule = { { "", "", "", "", "", "", "", "", "", "" },
                                 { "", "", "", "", "", "", "", "", "", "" },
                                 { "", "", "", "", "", "", "", "", "", "" },
                                 { "", "", "", "", "", "", "", "", "", "" },
                                 { "", "", "", "", "", "", "", "", "", "" }
                                 };

            // query the schedule
            //while ()
            string getschedule2 = @"
SELECT day, buildingName, roomName, request.moduleCode , periodStart, periodEnd
FROM [request] 
inner join BookedRoom on BookedRoom.requestID = request.requestID
inner join room on BookedRoom.roomID = room.roomid
inner join Building on building.buildingID = room.buildingID
inner join Park ON park.parkid = building.parkid
inner join Week on week.weekID = request.weekID
inner join moduleStaff on moduleStaff.ModuleCode = request.moduleCode
";

            // add the week where clause
            getschedule2 += "WHERE Week" + DropDownListFilterWeekStaff.SelectedValue + " = 1 ";

            if (DropDownListFilterStaff.SelectedIndex != 0)
                getschedule2 += "AND moduleStaff.staffID = " + DropDownListFilterStaff.SelectedValue + " ";

            if (DropDownListFilterPark.SelectedIndex != 0)
                getschedule2 += "AND Park.ParkID = '" + DropDownListFilterParkStaff.SelectedValue + "' ";

            if (DropDownListFilterYearStaff.SelectedIndex != 0)
                getschedule2 += "AND Request.Year = '" + DropDownListFilterYearStaff.SelectedValue + "' ";

            if (DropDownListFilterPartStaff.SelectedIndex > 0)
            {

                getschedule2 += "AND Charindex('" + DropDownListFilterPartStaff.SelectedItem.Text + "',Request.moduleCode) = 3";
            }


            if (RadioButtonListFilterSemesterStaff.SelectedIndex != 0)
                getschedule2 += "AND Request.Semester = '" + RadioButtonListFilterSemesterStaff.SelectedValue + "' ";

            if (RadioButtonListFilterStatusStaff.SelectedIndex != 0)
                getschedule2 += "AND Request.Status = '" + RadioButtonListFilterStatusStaff.SelectedItem.Text + "' ";

            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect2.Open();
            SqlCommand getschedule2Sql = new SqlCommand(getschedule2, connect2);
            SqlDataReader getSchedule2Data = getschedule2Sql.ExecuteReader();
            while (getSchedule2Data.Read())
            {
                string dayName = getSchedule2Data.GetString(getSchedule2Data.GetOrdinal("day"));
                string buildingName = getSchedule2Data.GetString(getSchedule2Data.GetOrdinal("buildingName"));
                string roomName = getSchedule2Data.GetString(getSchedule2Data.GetOrdinal("roomName"));
                string moduleCode = getSchedule2Data.GetString(getSchedule2Data.GetOrdinal("moduleCode"));

                // switch statement to translate day name to day index
                int day = 0;
                switch (dayName)
                {
                    case "Monday":
                        day = 0;
                        break;
                    case "Tuesday":
                        day = 1;
                        break;
                    case "Wednesday":
                        day = 2;
                        break;
                    case "Thursday":
                        day = 3;
                        break;
                    case "Friday":
                        day = 4;
                        break;
                }

                int period = getSchedule2Data.GetInt32(getSchedule2Data.GetOrdinal("periodStart"));
                schedule[day, period] = schedule[day, period] + buildingName + ": " + roomName + ": " + moduleCode + "\r";

            }

            HtmlTable myTable = new HtmlTable();
            myTable.Border = 1;
            myTable.ID = "ScheduleTable";
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell("th");
            cell.InnerText = "Day";
            row.Cells.Add(cell);
            for (int period = 1; period <= 9; period++)
            {
                cell = new HtmlTableCell("th");
                cell.InnerText = period.ToString();
                row.Cells.Add(cell);
            }
            myTable.Rows.Add(row);

            string[] dayNames = { "Monday", "Tuesday", "wednesday", "Thursday", "Friday" };
            for (int day = 0; day <= 4; day++)
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell("th");
                cell.InnerText = dayNames[day];
                row.Cells.Add(cell);
                for (int period = 1; period <= 9; period++)
                {
                    cell = new HtmlTableCell("td");
                    cell.InnerText = schedule[day, period];
                    row.Cells.Add(cell);
                }
                myTable.Rows.Add(row);
            }
            ViewTableStaff.Controls.Clear();
            ViewTableStaff.Controls.Add(myTable);
        }




        protected void DropDownListFilterPark_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule();
        }

        protected void DropDownListFilterWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule();
        }

        protected void DropDownListFilterYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule();
        }

        protected void DropDownListFilterPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule();
        }

        protected void DropDownListFilterParkStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule2();
        }
        protected void DropDownListFilterWeekStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule2();
        }
        protected void DropDownListFilterYearStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule2();
        }


        protected void ButtonRefreshSearch_Click(object sender, EventArgs e)
        {

        }

        protected void RadioButtonListFilterSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule();
        }


        protected void RadioButtonListFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule();
        }

        protected void RadioButtonListFilterSemesterStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule2();
        }
        protected void RadioButtonListFilterStatusStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateSchedule2();
        }
    }
}
