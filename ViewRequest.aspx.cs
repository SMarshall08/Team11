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
    public partial class ViewRequest : System.Web.UI.Page
    {
        string header1 = "";
        string header2 = "";
        string header3 = "";
        List<string> moduleslist = new List<string>();
        List<string> roomslist = new List<string>();
        int prefPeriod = 0;
        int prefhr24 = 0;
        List<string> headerOne = new List<string>();
        List<string> headerTwo = new List<string>();
        List<string> headerThree = new List<string>();
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
            string getModule = "SELECT moduleCode, moduleTitle FROM [Module] WHERE userID=" + Session["userID"]; //Session["userID"] is intialised upon login.
            SqlConnection connect11 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            connect11.Open();
            SqlCommand getModuleSql = new SqlCommand(getModule, connect11);
            SqlDataReader getmoduledata = getModuleSql.ExecuteReader();
            while (getmoduledata.Read())
            {
                DropDownListFilterModule.Items.Add(getmoduledata.GetString(1) + " / " + getmoduledata.GetString(0));
            }
            connect11.Close();
            DropDownListFilterBuilding.Items.Add("Please Select a Building To Filter By");
            string getBuilding = "";
            if (DropDownListFilterPark.SelectedValue == "0")
                getBuilding = "SELECT buildingName FROM [Building]";
            else
                getBuilding = "SELECT buildingName FROM [Building] WHERE parkID=" + DropDownListFilterPark.SelectedValue;
           
            DropDownListFilterRooms.Items.Add("Please Select a Room To Filter By");
            string getRooms;
            if (DropDownListFilterBuilding.SelectedIndex == 0)
                getRooms = "SELECT roomName FROM [Room]";
            else
                getRooms = "SELECT roomName FROM [Room] INNER JOIN [Building] ON [Room].buildingID = [Building].buildingID WHERE [Building].buildingName='" + DropDownListFilterBuilding.SelectedValue + "'";
            connect11.Open();
            SqlCommand getRoomsSql = new SqlCommand(getRooms, connect11);
            SqlDataReader getroomdata = getRoomsSql.ExecuteReader();
            while (getroomdata.Read())
            {
                DropDownListFilterRooms.Items.Add(getroomdata.GetString(0));
            }
            connect11.Close();
            
            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            string preferencesquery = "SELECT period, hr24Format, header1, header2, header3 FROM [Preferences] WHERE userID='" + Session["userID"] + "'";
            connect.Open();
            SqlCommand preferencessql = new SqlCommand(preferencesquery, connect);
            SqlDataReader preferences = preferencessql.ExecuteReader();
            if (preferences.Read())
            {
                prefPeriod = preferences.GetInt32(0);
                prefhr24 = preferences.GetInt32(1);
                header1 = preferences.GetString(2);
                header2 = preferences.GetString(3);
                header3 = preferences.GetString(4);
            }
            connect.Close();
        
            //Collect filter values
            string codeStr = "";
            if (DropDownListFilterPark.SelectedIndex != 0)
                codeStr += " AND [Park].parkID='" + DropDownListFilterPark.SelectedValue + "'";
            if (DropDownListFilterBuilding.SelectedIndex != 0)
                codeStr += " AND [Building].buildingName='" + DropDownListFilterBuilding.SelectedValue + "'";
            if (DropDownListFilterRooms.SelectedIndex != 0)
                codeStr += " AND [Room].roomName='" + DropDownListFilterRooms.SelectedValue + "'";
            if (RadioButtonListFilterSemester.SelectedIndex != 0)
                codeStr += " AND [Request].semester=" + Convert.ToInt32(RadioButtonListFilterSemester.SelectedValue);
            if (RadioButtonListFilterStatus.SelectedIndex != 0)
                codeStr += " AND [Request].status='" + RadioButtonListFilterStatus.SelectedValue + "'";
            if (DropDownListFilterModule.SelectedIndex != 0)
                codeStr += " AND [Request].moduleCode='" + DropDownListFilterModule.SelectedValue.Substring(DropDownListFilterModule.SelectedValue.Length - 6, 6) + "'";
            if (DropDownList2.SelectedIndex == 1)
                codeStr += " AND [Request].year=2013";
            else if (DropDownList2.SelectedIndex == 2)
                    codeStr += " AND [Request].year=2014";
            

            //Collect all requests fitting filter
            string reqid = @"
SELECT DISTINCT [Request].requestID, 
                moduleTitle + ' / ' + module.moduleCode AS module, 
                request.day, 
                periodStart, 
                periodEnd, 
                status, 
                parkName, 
                semester, 
                request.year, 
                week1, week2, week3, week4, week5, week6, week7, week8, week9, week10, week11, week12, week13, week14, week15
FROM [Request] 
INNER JOIN [Week] on [Week].weekID = [request].weekID
INNER JOIN [Module] ON [Module].moduleCode = [request].moduleCode 
INNER JOIN [BookedRoom] ON [Request].requestID = [BookedRoom].requestID 
INNER JOIN [Room] ON [BookedRoom].roomID = [Room].roomID 
INNER JOIN [Building] ON [Room].buildingID = [Building].buildingID 
INNER JOIN [Park] ON [Building].parkID = [Park].parkID 
WHERE [Module].userID = '" + Session["userID"] + "'" + codeStr;
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            conn.Open();
            SqlCommand reqidsql = new SqlCommand(reqid, conn);
            SqlDataReader reqs = reqidsql.ExecuteReader();
            while (reqs.Read())
            {
                requests.Add(reqs.GetInt32(reqs.GetOrdinal("requestID")));
                park.Add(reqs.GetString(reqs.GetOrdinal("parkName")));
                semester.Add(reqs.GetInt32(reqs.GetOrdinal("semester")));
                year.Add(reqs.GetInt32(reqs.GetOrdinal("year")));
                modTitle.Add(reqs.GetString(reqs.GetOrdinal("module")));
                day.Add(reqs.GetString(reqs.GetOrdinal("day")));
                status.Add(reqs.GetString(reqs.GetOrdinal("status")));

                int periodst = reqs.GetInt32(reqs.GetOrdinal("periodStart"));
                int periodnd = reqs.GetInt32(reqs.GetOrdinal("periodEnd"));
                //CHANGE DUE TO PREFERENCES
                if (prefPeriod == 1)
                {
                    period.Add(periodst + " - " + periodnd);
                }
                else
                {
                    if (prefhr24 == 1)
                    {
                        period.Add((periodst + 8) + ":00 - " + (periodnd + 8) + ":50");
                    }
                    else
                    {
                        periodst += 8;
                        periodnd += 8;
                        if (periodst > 12)
                            periodst -= 12;
                        if (periodnd > 12)
                            periodnd -= 12;
                        period.Add(periodst + ":00 - " + periodnd + ":50");
                    }
                }

                int weekval;
                string weekcol = "";
                for (int n = reqs.GetOrdinal("week1"); n <= reqs.GetOrdinal("week15"); n++)
                {
                    weekval = reqs.GetInt32(n);
                    if (weekval == 1)
                        weekcol += (n + 1) + ", ";
                }
                if (weekcol.Length != 0)
                    weekcol = weekcol.Substring(0, weekcol.Length - 2);

                weeks.Add(weekcol);
            }
            conn.Close();
            
            for (int request = 0; request < requests.Count; request++)
            {
                string facilitiesList = "";
                string facname = @"
SELECT facilityName 
FROM [Facility] 
INNER JOIN [RequestFacilities] ON [Facility].facilityID = [RequestFacilities].facilityID 
WHERE [RequestFacilities].requestID = " + requests[request];
                conn.Open();
                SqlCommand facilitysql = new SqlCommand(facname, conn);

                SqlDataReader facility = facilitysql.ExecuteReader();
                while (facility.Read())
                {
                    facilitiesList += facility.GetString(0) + ", ";
                }
                if (facilitiesList.Length > 3)
                    facilitiesList = facilitiesList.Substring(0, facilitiesList.Length - 2);
                else if (facilitiesList.Length == 0)
                    facilitiesList = "N/A";
                conn.Close();
                facilities.Add(facilitiesList);

                conn.Open();
                string bookedRoomsList = "";
                string buildingsList = "";
                int totalcapacity = 0;
                string bookroom = @"
SELECT roomName, capacity, buildingName 
FROM [Room] 
INNER JOIN [Building] ON [Room].buildingID = [Building].buildingID 
INNER JOIN [BookedRoom] ON [Room].roomID = [BookedRoom].roomID 
INNER JOIN [Request] ON [BookedRoom].requestID = [Request].requestID 
WHERE [Request].requestID = " + requests[request];
                SqlCommand bookroomsql = new SqlCommand(bookroom, conn);
                SqlDataReader getbookroom = bookroomsql.ExecuteReader();
                while (getbookroom.Read())
                {
                    bookedRoomsList += getbookroom.GetString(getbookroom.GetOrdinal("roomName")) + ", ";
                    totalcapacity += getbookroom.GetInt32(getbookroom.GetOrdinal("capacity"));
                    buildingsList += getbookroom.GetString(getbookroom.GetOrdinal("buildingName")) + ", ";
                }
                conn.Close();
                room.Add(bookedRoomsList);
                capacity.Add(totalcapacity);
                building.Add(buildingsList);

                conn.Open();
                string alternateRoomsList = "";
                string altroom = @"
SELECT roomName 
FROM [Room] 
INNER JOIN [PreferredRoom] ON [Room].roomID = [PreferredRoom].roomID 
INNER JOIN [Request] ON [PreferredRoom].requestID = [Request].requestID 
WHERE [Request].requestID = " + requests[request];
                SqlCommand altroomsql = new SqlCommand(altroom, conn);
                SqlDataReader getaltroom = altroomsql.ExecuteReader();
                while (getaltroom.Read())
                {
                    alternateRoomsList += getaltroom.GetString(0) + ", ";
                }
                conn.Close();

                if (alternateRoomsList.Length > 3)
                    alternateRoomsList = alternateRoomsList.Substring(0, alternateRoomsList.Length - 2);
                else if (alternateRoomsList.Length == 0)
                    alternateRoomsList = "N/A";
                alternateRooms.Add(alternateRoomsList);
            }

            HtmlTable myTable = new HtmlTable();
            myTable.Border = 1;
            myTable.ID = "RequestsTable";
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell0 = new HtmlTableCell("th");
            cell0.InnerText = "Reference #";
            row.Cells.Add(cell0);
            HtmlTableCell cell = new HtmlTableCell("th");
            cell.InnerText = "Module";
            row.Cells.Add(cell);
            HtmlTableCell cell4 = new HtmlTableCell("th");
            cell4.InnerText = "Day";
            row.Cells.Add(cell4);
            HtmlTableCell cell2 = new HtmlTableCell("th");
            if (prefPeriod == 1)
                cell2.InnerText = "Period";
            else if (prefPeriod == 0)
                cell2.InnerText = "Times";
            row.Cells.Add(cell2);

            HtmlTableCell cell3 = new HtmlTableCell("th");
            cell3.InnerText = "Weeks";
            row.Cells.Add(cell3);
            HtmlTableCell cell5 = new HtmlTableCell("th");
            cell5.InnerText = "Requested Room(s)";
            row.Cells.Add(cell5);
            HtmlTableCell cell6 = new HtmlTableCell("th");
            cell6.InnerText = header1;
            row.Cells.Add(cell6);
            HtmlTableCell cell7 = new HtmlTableCell("th");
            cell7.InnerText = header2;
            row.Cells.Add(cell7);
            HtmlTableCell cell8 = new HtmlTableCell("th");
            cell8.InnerText = header3;
            row.Cells.Add(cell8);
            HtmlTableCell cell9 = new HtmlTableCell("th");
            cell9.InnerText = "Status";
            row.Cells.Add(cell9);
            myTable.Rows.Add(row);

            for (int request = 0; request < requests.Count; request++)
            {
                HtmlTableRow row2 = new HtmlTableRow();
                HtmlTableCell cellx;
                cellx = new HtmlTableCell();
                cellx.InnerText = Convert.ToString(requests[request]);
                row2.Cells.Add(cellx);
                cellx = new HtmlTableCell();
                cellx.InnerText = modTitle[request];
                row2.Cells.Add(cellx);
                cellx = new HtmlTableCell();
                cellx.InnerText = day[request];
                row2.Cells.Add(cellx);
                cellx = new HtmlTableCell();
                cellx.InnerText = period[request];
                row2.Cells.Add(cellx);
                cellx = new HtmlTableCell();
                cellx.InnerText = weeks[request];
                row2.Cells.Add(cellx);
                cellx = new HtmlTableCell();
                cellx.InnerText = room[request];
                row2.Cells.Add(cellx);
                SetHeader(header1, request, row2);
                SetHeader(header2, request, row2);
                SetHeader(header3, request, row2);
                cellx = new HtmlTableCell();
                cellx.InnerText = status[request];
                row2.Cells.Add(cellx);
                myTable.Rows.Add(row2);
            }
            ViewTable.Controls.Clear();
            ViewTable.Controls.Add(myTable);
            if (!IsPostBack)
            {
                //Populate button request list
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add("Please Select a Reference");
                for (int u = 0; u < requests.Count; u++)
                {
                    DropDownList1.Items.Add(Convert.ToString(requests[u]));
                }
            }
        }

        private void SetHeader(string header, int request, HtmlTableRow row2)
        {
            HtmlTableCell cellx = new HtmlTableCell();
            switch (header)
            {
                case "facilities":
                    cellx.InnerText = facilities[request];
                    break;
                case "park":
                    cellx.InnerText = park[request];
                    break;
                case "altrooms":
                    cellx.InnerText = alternateRooms[request];
                    break;
                case "building":
                    cellx.InnerText = building[request];
                    break;
                case "semester":
                    cellx.InnerText = Convert.ToString(semester[request]);
                    break;
                case "year":
                    cellx.InnerText = Convert.ToString(year[request]);
                    break;
                case "numerstudents":
                    cellx.InnerText = Convert.ToString(capacity[request]);
                    break;
            }
            row2.Cells.Add(cellx);
        }

        protected void ButtonRefreshSearch_Click(object sender, EventArgs e)
        {
            //Populate button request list
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add("Please Select a Reference");
            for (int u = 0; u < requests.Count; u++)
            {
                DropDownList1.Items.Add(Convert.ToString(requests[u]));
            }
        }



        protected void ButtonHideDetails_Click(object sender, EventArgs e)
        {
            //Button to hide details
            this.requestDetails.Visible = false;
        }

        //Details Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex != 0)
            {
                int requestid = Convert.ToInt32(DropDownList1.SelectedValue);
                //Declare variables where details will be stored
                int semester = 0;
                int year = 0;
                string department = "";
                string moduleCode = "";
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
                this.requestDetails.Visible = true;

                SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
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
                string requestfacdetails = "SELECT facilityName FROM [Facility] INNER JOIN [RequestFacilities] ON [Facility].facilityID = [RequestFacilities].facilityID WHERE [RequestFacilities].requestID = " + requestid;
                SqlCommand requestfaccommand = new SqlCommand(requestfacdetails, connect);
                //Retrieve the request details and store them in appropriate Variables
                SqlDataReader facdetails = requestfaccommand.ExecuteReader();
                while (facdetails.Read())
                {
                    facilities += facdetails.GetString(0) + ", ";
                }
                connect.Close();
                connect.Open();
                string roomdetails = "SELECT roomName FROM [Room] INNER JOIN [BookedRoom] ON [Room].roomID = [BookedRoom].roomID INNER JOIN [Request] ON [BookedRoom].requestID = [Request].requestID WHERE [Request].requestID = " + requestid;
                SqlCommand roomcommand = new SqlCommand(roomdetails, connect);
                //Retrieve the request details and store them in appropriate Variables
                SqlDataReader bookedroomdetails = roomcommand.ExecuteReader();
                while (bookedroomdetails.Read())
                {
                    bookedRoom += bookedroomdetails.GetString(0) + ", ";
                }
                connect.Close();
                connect.Open();
                string alternateroomdetails = "SELECT roomName FROM [Room] INNER JOIN [BookedRoom] ON [Room].roomID = [BookedRoom].roomID INNER JOIN [Request] ON [BookedRoom].requestID = [Request].requestID WHERE [Request].requestID = " + requestid;
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
                string deptdetails = "SELECT deptCode FROM [User] WHERE userID =" + Session["userID"];
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
                string builddetails = "SELECT buildingName FROM [Building] INNER JOIN [Room] ON [Building].buildingID = [Room].buildingID INNER JOIN [BookedRoom] ON [Room].roomID = [BookedRoom].roomID WHERE [BookedRoom].requestID =" + requestid;
                SqlCommand buildingcommand = new SqlCommand(builddetails, connect);
                //Retrieve the request details and store them in appropriate Variables
                SqlDataReader buildingdetails = buildingcommand.ExecuteReader();
                if (buildingdetails.Read())
                {
                    building = buildingdetails.GetString(0);
                }
                connect.Close();
                connect.Open();
                string parknamedetails = "SELECT parkName FROM [Park] INNER JOIN [Building] ON [Park].parkID = [Building].parkID WHERE [Building].buildingName ='" + building + "'";
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
        }

        //Edit Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex != 0)
            {
                int requestid = Convert.ToInt32(DropDownList1.SelectedValue);
                Response.Redirect("EditRequest.aspx?request=" + requestid + "&edit=1");
            }
        }

        //Delete Button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedIndex != 0)
            {
                int requestid = Convert.ToInt32(DropDownList1.SelectedValue);
                SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                connect.Open();

                string deleterequest = "Delete from [Request] WHERE requestID=" + requestid;
                string deleterequestfacilities = "Delete from [RequestFacilities] WHERE requestID=" + requestid;
                string deletebookedroom = "Delete from [BookedRoom] Where requestID=" + requestid;
                string deletepreferred = "DELETE FROM [PreferredRoom] WHERE requestID=" + requestid;
                SqlCommand bookedroomcommand = new SqlCommand(deletebookedroom, connect);
                SqlCommand requestcommand = new SqlCommand(deleterequest, connect);
                SqlCommand facilitiescommand = new SqlCommand(deleterequestfacilities, connect);
                SqlCommand preferredcommand = new SqlCommand(deletepreferred, connect);
                facilitiescommand.ExecuteNonQuery();
                bookedroomcommand.ExecuteNonQuery();
                preferredcommand.ExecuteNonQuery();
                requestcommand.ExecuteNonQuery();
                connect.Close();
            }
        }

        /*int way = 1;
        //Sort functions. Asc, Desc alternating. Bubble sort.
		public void sortTable(int colnumber){
            //Make array of requestIds
            List<int> rows = new List<int>();
            ///RequestsTable.child.child:first.child


			//Fill 2D array with each row of table.
            List<List<dynamic>> value = new List<List<dynamic>>();
			var rows = ViewTable.getElementsByTagName("tr");
			for (int i = 1; i < rows.length; i++){
				var row = rows[i];
				var cols = row.getElementsByTagName("td");
				for (int j = 0; j < cols.length; j++){
					value[i][j]=cols[j].INNERHTML;
				}
			}
			//Sort descending.
			List<dynamic> temp = new List<dynamic>();
			if (way == 1){
				bool swapped = true;
				while(swapped){
					swapped = false;
					for (int k = 1; k < value.Count-1; k++){
						if (value[k][colnumber] > value[k+1][colnumber]){
							temp[0]=value[k];
							value[k]=value[k+1];
							value[k+1]=temp[0];
							swapped = true;
						}
					}
				}
				way = 2;
			}
			//Sort ascending
			else if (way == 2){
				bool swapped2 = true;
				while(swapped2){
					swapped2 = false;
					for (int m = 1; m < value.Count-1; m++){
						if (value[m][colnumber] < value[m+1][colnumber]){
							temp[0]=value[m];
							value[m]=value[m+1];
							value[m+1]=temp[0];
							swapped2 = true;
						}
					}
				}
				way = 1;
			}
			// Recreate table after sorting
			string codeStr = "";
			codeStr += "<table id='RequestsTable'>";
			codeStr += "<tr>";
			codeStr += "<td>" + value[0][0] + "</td>";
			codeStr += "<td>" + value[0][1] + "</td>";
			codeStr += "<td>" + value[0][2] + "</td>";
			codeStr += "<td>" + value[0][3] + "</td>";
			codeStr += "<td>" + value[0][4] + "</td>";
			codeStr += "<td>" + value[0][5] + "</td>";
			codeStr += "<td>" + value[0][6] + "</td>";
			codeStr += "<td>" + value[0][7] + "</td>";
			codeStr += "<td>" + value[0][8] + "</td>";
			codeStr += "<td>" + value[0][9] + "</td>";
			codeStr += "<td>" + value[0][10] + "</td>";
			codeStr += "<td>" + value[0][11] + "</td>";
            codeStr += "</tr>";
			for(int l=1;l<value.Count;l++){
				codeStr += "	<tr id=r"+l+">";
				codeStr += "    	<td>" + value[l][0] + "</td>";
				codeStr += "    	<td>" + value[l][1] + "</td>";
				codeStr += "    	<td>" + value[l][2] + "</td>";
				codeStr += "    	<td>" + value[l][3] + "</td>";
				codeStr += "    	<td>" + value[l][4] + "</td>";
				codeStr += "    	<td>" + value[l][5] + "</td>";
				codeStr += "    	<td>" + value[l][6] + "</td>";
				codeStr += "    	<td>" + value[l][7] + "</td>";
				codeStr += "    	<td>" + value[l][8] + "</td>";
				codeStr += "    	<td>" + value[l][9] + "</td>";
				codeStr += "    	<td>" + value[l][10] + "</td>";
				codeStr += "    	<td>" + value[l][11] + "</td>";
				codeStr += "	</tr>";
			}
			codeStr += "</table>";
			//Empty and refill table's div tag.
            ViewTable.INNERHtml = "";
            ViewTable.INNERHtml = codeStr;

			
		}*/

        protected void DropDownListFilterPark_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonRefreshSearch_Click(null, null);
            DropDownListFilterBuilding.Items.Clear();
            DropDownListFilterRooms.Items.Clear();
            SqlConnection connect11 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            DropDownListFilterBuilding.Items.Add("Please Select a Building To Filter By");
            string getBuilding = "";
            if (DropDownListFilterPark.SelectedValue == "0")
                getBuilding = "SELECT buildingName FROM [Building]";
            else
                getBuilding = "SELECT buildingName FROM [Building] WHERE parkID=" + DropDownListFilterPark.SelectedValue;
            connect11.Open();
            SqlCommand getBuildingSql = new SqlCommand(getBuilding, connect11);
            SqlDataReader getBuildingData = getBuildingSql.ExecuteReader();
            while (getBuildingData.Read())
            {
                DropDownListFilterBuilding.Items.Add(getBuildingData.GetString(0));
            }
            connect11.Close();
            DropDownListFilterRooms.Items.Add("Please Select a Room To Filter By");
            string getRooms;
            if (DropDownListFilterBuilding.SelectedIndex == 0)
                getRooms = "SELECT roomName FROM [Room]";
            else
                getRooms = "SELECT roomName FROM [Room] INNER JOIN [Building] ON [Room].buildingID = [Building].buildingID WHERE [Building].buildingName='" + DropDownListFilterBuilding.SelectedValue + "'";
            connect11.Open();
            SqlCommand getRoomsSql = new SqlCommand(getRooms, connect11);
            SqlDataReader getroomdata = getRoomsSql.ExecuteReader();
            while (getroomdata.Read())
            {
                DropDownListFilterRooms.Items.Add(getroomdata.GetString(0));
            }
            connect11.Close();
        }

        protected void DropDownListFilterBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonRefreshSearch_Click(null, null);
            DropDownListFilterRooms.Items.Clear();
            SqlConnection connect11 = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            DropDownListFilterRooms.Items.Add("Please Select a Room To Filter By");
            string getRooms;
            if (DropDownListFilterBuilding.SelectedIndex == 0)
                getRooms = "SELECT roomName FROM [Room]";
            else
                getRooms = "SELECT roomName FROM [Room] INNER JOIN [Building] ON [Room].buildingID = [Building].buildingID WHERE [Building].buildingName='" + DropDownListFilterBuilding.SelectedValue + "'";
            connect11.Open();
            SqlCommand getRoomsSql = new SqlCommand(getRooms, connect11);
            SqlDataReader getroomdata = getRoomsSql.ExecuteReader();
            while (getroomdata.Read())
            {
                DropDownListFilterRooms.Items.Add(getroomdata.GetString(0));
            }
            connect11.Close();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int requestid = Convert.ToInt32(DropDownList1.SelectedValue);
            Response.Redirect("EditRequest.aspx?request=" + requestid + "&edit=0");
        }

        protected void DropDownListFilterModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonRefreshSearch_Click(null, null);
        }

        protected void RadioButtonListFilterSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonRefreshSearch_Click(null, null);
        }

        protected void RadioButtonListFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
           ButtonRefreshSearch_Click(null, null);
        }

        protected void DropDownListFilterRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonRefreshSearch_Click(null, null);
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonRefreshSearch_Click(null, null);
        }
    }
}