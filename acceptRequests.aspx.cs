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
        protected void Page_Load(object sender, EventArgs e)
        {
             
            string url = Request.Url.Query; ///Gets url
            Match match = Regex.Match(url, @"=(.*)"); ///Gets reference from url
            string reference = match.Groups[1].Value;
            referenceLabel.Text = "You are responding to the request with reference number: " + reference+".";

            string listSQL = "SELECT * FROM Request WHERE requestID ="+reference;

            SqlConnection Connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            Connection.Open();
            SqlCommand cmd = new SqlCommand(listSQL, Connection);
            
            SqlDataReader reader; reader = cmd.ExecuteReader();
            string moduleCode="", status="", weekID="", day="", periodStart="", periodEnd="", semester="", year="", round="";
            
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
            tableDiv.InnerHtml = "<table style=\"border: 4px solid black;\"><tr style=\"border: 4px solid black;\">" +
    "<td style=\"border: 4px solid black;\">Module Code</td>" +
    "<td style=\"border: 4px solid black;\">Status</td>" +
    "<td style=\"border: 4px solid black;\">Week</td>" +
"<td style=\"border: 4px solid black;\">Day</td>" +
"<td style=\"border: 4px solid black;\">Period Start</td>" +
"<td style=\"border: 4px solid black;\">Period End</td>" +
"<td style=\"border: 4px solid black;\">Semester</td>" +
"<td style=\"border: 4px solid black;\">Year</td>" +
"<td style=\"border: 4px solid black;\">Round</td>" +
"</tr>" +
"<tr style=\"border: 4px solid black;\">" +
"<td style=\"border: 4px solid black;\">" + moduleCode + "</td>" +
"<td style=\"border: 4px solid black;\">" + status + "</td>" +
"<td style=\"border: 4px solid black;\">" + weekID + "</td>" +
"<td style=\"border: 4px solid black;\">" + day + "</td>" +
"<td style=\"border: 4px solid black;\">" + periodStart + "</td>" +
"<td style=\"border: 4px solid black;\">" + periodEnd + "</td>" +
"<td style=\"border: 4px solid black;\">" + semester + "</td>" +
"<td style=\"border: 4px solid black;\">" + year + "</td>" +
"<td style=\"border: 4px solid black;\">" + round + "</td>" +
"</tr></table>";


        }
    }
}
