using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;

namespace Team11
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string roundLabelSQL = "SELECT * FROM Rounds";
            SqlConnection roundLabelConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            SqlCommand roundLabelCmd = new SqlCommand(roundLabelSQL, roundLabelConnection);
            SqlDataReader roundLabelReader;
            roundLabelConnection.Open();
            roundLabelReader = roundLabelCmd.ExecuteReader();
            int round = 0; string date = "";
            while (roundLabelReader.Read())
            {
                round = Convert.ToInt32(roundLabelReader["round"]);
                date = roundLabelReader["dateToAdvance"].ToString();

            }
            string roundString = " (Current round: " + round + " - Date to advance round: " + date + ")"; 
            displayRound.InnerHtml = roundString;

        }
    }
}
