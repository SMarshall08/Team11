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
        int prefPeriod = 0;
        int prefhr24 = 0;
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
            DropDownListFilterModule.Items.Add("Please Select a Module To filter By");
            string getModule = "SELECT moduleCode, moduleTitle FROM [Module] WHERE userID=" +Session["userID"]; //Session["userID"] is intialised upon login.
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect.Open();
            SqlCommand getModuleSql = new SqlCommand(getModule, connect);
            SqlDataReader getmoduledata = getModuleSql.ExecuteReader();
            while (getmoduledata.Read())
            {
                DropDownListFilterModule.Items.Add(getmoduledata.GetString(1) + " / " + getmoduledata.GetString(0));
            }
            connect.Close();

            /*
            DropDownListFilterBuilding.Items.Add("Please Select a Park To filter By");
            string getBuilding = "SELECT park FROM [Park] "; 
            SqlCommand getBuildingSql = new SqlCommand(getPark, connect11);
            SqlDataReader getbuildingdata = getParkSql.ExecuteReader();
            while (getbuildingdata.Read())
            {
                DropDownListFilterBuilding.Items.Add(getbuildingdata.GetString(0));
            }*/
            

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
inner join Week on week.weekID = request.weekID
WHERE week1 = 1 ";
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
            ViewTable.Controls.Clear();
            ViewTable.Controls.Add(myTable);
        }

        protected void DropDownListFilterModule_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void DropDownListFilterPark_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DropDownListFilterWeek_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DropDownListFilterYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DropDownListFilterPart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonRefreshSearch_Click(object sender, EventArgs e)
        {

        }

        protected void RadioButtonListFilterSemester_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButtonListFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
