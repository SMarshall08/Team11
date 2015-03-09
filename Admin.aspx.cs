using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Text.RegularExpressions;
namespace Team11
{
    public partial class Admin : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e) {

            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect.Open();
           
            
            string url = Request.Url.Query; ///Gets url
            Match match = Regex.Match(url, @"=(.*)"); ///Gets user ID from url
            string urlID = match.Groups[1].Value;
            string adminString = "Select administrator from [User] where userID = " + urlID;
            SqlCommand adminCommand = new SqlCommand(adminString, connect);
            string administratorYesNo = adminCommand.ExecuteScalar().ToString();
            string trimmedAdmin = administratorYesNo.Trim();
            bool userIsAdmin = (trimmedAdmin == "yes");
           
             
            if (userIsAdmin)
         {        //Below executed if user logged in is an admin.

               areYouAdmin.InnerHtml="You are an admin.";

           }
           else {
                //Below executed if user logged in is not an admin.

               areYouAdmin.InnerHtml = "You are not an admin.";
                 }


            connect.Close();
        
        
        }

        /// <summary>
        /// Handles the Click event of the ButtonLogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        
    }
}
