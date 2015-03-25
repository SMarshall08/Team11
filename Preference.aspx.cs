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
    public partial class Preference : System.Web.UI.Page
    {
        int userID = 0;
        int periodval;
        int hr24val;
        string locationval;
        string loadingval;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // read the userid from the querystring
            userID = Convert.ToInt32(Request.QueryString["userID"]);

            if (!IsPostBack)
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
                conn.Open();
                string preferencesquery = String.Format("SELECT period, hr24Format, defaultLocation, defaultPage, header1, header2, header3 FROM [Preferences] WHERE userID = {0}", userID);
                SqlCommand preferencessql = new SqlCommand(preferencesquery, conn);
                SqlDataReader preferences = preferencessql.ExecuteReader();
                if (preferences.Read())
                {
                    int periodText = preferences.GetInt32(0);
                    int hr24FormatText = preferences.GetInt32(1);
                    string defaultLocationText = preferences.GetString(2);
                    string defaultPageText = preferences.GetString(3);
                    string header1Text = preferences.GetString(4);
                    string header2Text = preferences.GetString(5);
                    string header3Text = preferences.GetString(6);

                    if (defaultPageText == "Create")
                        create.Checked = true;
                    else if (defaultPageText == "View")
                        view.Checked = true;
                    else if (defaultPageText == "Adhoc")
                        adhoc.Checked = true;

                    if (defaultLocationText == "Central")
                        central.Checked = true;
                    else if (defaultLocationText == "East")
                        east.Checked = true;
                    else if (defaultLocationText == "West")
                        west.Checked = true;
                    else
                        any.Checked = true;

                    if (hr24FormatText == 1)
                        hr24.Checked = true;
                    else if (hr24FormatText == 0)
                        hr12.Checked = true;

                    if (periodText == 1)
                        period.Checked = true;
                    else if (periodText == 0)
                        time.Checked = true;

                    header1.SelectedValue = header1Text;
                    header2.SelectedValue = header2Text;
                    header3.SelectedValue = header3Text;
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the Button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (create.Checked)
                loadingval = "Create";
            else if (view.Checked)
                loadingval = "View";
            else if (adhoc.Checked)
                loadingval = "Adhoc";

            if (central.Checked)
                locationval = "Central";
            else if (east.Checked)
                locationval = "East";
            else if (west.Checked)
                locationval = "West";
            else if (any.Checked)
                locationval = "";

            if (hr24.Checked)
                hr24val = 1;
            else if (hr12.Checked)
                hr24val = 0;

            if (period.Checked)
                periodval = 1;
            else if (time.Checked)
                periodval = 0;

            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ParkConnectionString"].ToString());
            string preferencesExistsSql = String.Format("SELECT COUNT(*) FROM Preferences WHERE userID={0}", userID);
            SqlCommand preferencessqlCmd = new SqlCommand(preferencesExistsSql, conn);
            conn.Open();
            int exists = Convert.ToInt32(preferencessqlCmd.ExecuteScalar());
            conn.Close();

            string preferencesquerynew = "";
            if (exists == 1)
            {
                preferencesquerynew = String.Format(@"
Update [Preferences] 
SET period={0}, 
    hr24Format={1}, 
    defaultLocation='{2}', 
    defaultPage='{3}', 
    header1='{4}', 
    header2='{5}', 
    header3='{6}' 
WHERE userID={7}",
                                                        periodval,
                                                        hr24val,
                                                        locationval,
                                                        loadingval,
                                                        header1.SelectedValue,
                                                        header2.SelectedValue,
                                                        header3.SelectedValue,
                                                        userID);
            }
            else
            {
                preferencesquerynew = String.Format(@"
INSERT INTO [Preferences] (period, hr24Format, defaultLocation, defaultPage, header1, header2, header3, userID) 
values ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7})",
                                                        periodval,
                                                        hr24val,
                                                        locationval,
                                                        loadingval,
                                                        header1.SelectedValue,
                                                        header2.SelectedValue,
                                                        header3.SelectedValue,
                                                        userID);
            }
            SqlCommand preferencessql = new SqlCommand(preferencesquerynew, conn);
            conn.Open();
            preferencessql.ExecuteNonQuery();
            conn.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx?userID=" + userID);



        }
    }
}