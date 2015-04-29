// Include references to all the system libraries being used
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
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Handles the Click event of the ButtonLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DropDownChange(object sender, EventArgs e)
        {
            LabelHint.Text = "";
        }
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            // Connect to database - put it in a using so that it gets cleaned up properly
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                int userID = Convert.ToInt32(DropDownListDept.SelectedValue);

                conn.Open();
                // Count how many users match - there should be only one. Lookup by ID not name
                string checkuser = String.Format("select count(*) from [User] where userId={0}", userID);
                SqlCommand countCmd = new SqlCommand(checkuser, conn);
                int matchCount = Convert.ToInt32(countCmd.ExecuteScalar());
                conn.Close();

                if (matchCount == 1)
                {
                    string page = "";
                    conn.Open();
                    // look up the users password so it can be compared to the entered value
                    string checkpassword = String.Format("Select password from [User] where userId={0}", userID);
                    SqlCommand passwordCmd = new SqlCommand(checkpassword, conn);
                    // Gets rid of the space if there is one e.g. by habit putting a space at the end
                    string password = passwordCmd.ExecuteScalar().ToString().Replace(" ", "");
                    conn.Close();

                    if (password == TextBoxPassword.Text)
                    {
                        // if the password is correct
                        conn.Open();
                        // see if the user has a preference page
                        string checkpref = String.Format("select defaultPage from [Preferences] where userId={0}", userID);
                        SqlCommand defaultCmd = new SqlCommand(checkpref, conn);
                        SqlDataReader defaultReader = defaultCmd.ExecuteReader();

                        if (defaultReader.Read())
                            // If the user has a preference then read it, otherwise it will just default
                            page = defaultReader.GetString(0);

                        // determine what page to redirect to
                        string redirectPage = "Availibility.aspx";
                        switch (page)
                        {
                            case "Create":
                                redirectPage = "CreateRequest.aspx";
                                break;
                            case "View":
                                redirectPage = "ViewRequest.aspx";
                                break;
                        }

                        conn.Close();
                        //initiate a new session that stores the userID of the person that logs in.
                        Session["userID"] = userID;
                        // redirect after the databse connection has been closed
                        Response.Redirect(redirectPage);
                    }
                    else
                        incorrect.Text = "Password is incorrect";
                }
                else
                    incorrect.Text = "Username is incorrect";
            }
        }

        protected void ButtonForgot_Click(object sender, EventArgs e)
        {
           

            int userID = Convert.ToInt32(DropDownListDept.SelectedValue);

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ToString()))
            {
                conn.Open();
                // look up the users password so it can be compared to the entered value

                string getHint = String.Format("Select hint from [User] where userId={0}", userID);
                SqlCommand hintCmd = new SqlCommand(getHint, conn);
                // Gets rid of the space if there is one e.g. by habit putting a space at the end
                string hint = hintCmd.ExecuteScalar().ToString();
                conn.Close();
                LabelHint.Text = "Hint: " + hint;
            }
        }
    }
}
