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
        int userID = 0;
        protected void Page_Load(object sender, EventArgs e) {
            

            SqlConnection connect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect.Open();
            
            userID = Convert.ToInt32(Request.QueryString["userID"]);
            /*
            string url = Request.Url.Query; ///Gets url
            Match match = Regex.Match(url, @"=(.*)"); ///Gets user ID from url
            string urlID = match.Groups[1].Value;       */
            string adminString = "Select administrator from [User] where userID = " + userID;
            SqlCommand adminCommand = new SqlCommand(adminString, connect);
            string administratorYesNo = adminCommand.ExecuteScalar().ToString();
            string trimmedAdmin = administratorYesNo.Trim();
            bool userIsAdmin = (trimmedAdmin == "yes");
           
             
            if (userIsAdmin)
         {        //Below executed if user logged in is an admin.



             
             Button addFacility = new Button(); //This button will submit the facility.
             addFacility.ID = "addFacility";
             addFacility.Text = "Add Facility";
             
             addFacility.Click += new EventHandler(addFacilityFunction);
             areYouAdmin.Controls.Add(addFacility);
             

             

             
             
           }
           else {
                //Below executed if user logged in is not an admin.

               areYouAdmin.InnerHtml = "You are not logged in as an admin. Please log in to an account with administrator privileges in order to view the admin options.";
               
                 }

           


            connect.Close();
        
        
        }

        protected void addFacilityFunction(Object sender, EventArgs e) //This is executed when the add facility button is pressed.
        {



            string facility = facilityText.Text.ToString();
            
            SqlConnection connect2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect2.Open();
            string addFacilityString = "Insert into Facility (facilityName) VALUES ('"+facility+"')";
            SqlCommand addFacilityCommand = new SqlCommand(addFacilityString, connect2);
            addFacilityCommand.ExecuteNonQuery();
            
            scriptDiv.InnerHtml = "<script>alert(\"Facility successfully added!\")</script>";
            


        }
                                 

        
        
    }
}
