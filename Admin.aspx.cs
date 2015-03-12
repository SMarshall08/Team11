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



             TextBox facilityText = new TextBox(); //This is the box that will have the facility entered into.
             facilityText.Text = "Type facility name here.";
             facilityText.ID = "que1";
             areYouAdmin.Controls.Add(facilityText);
             Button addFacility = new Button(); //This button will submit the facility.
             addFacility.ID = "addFacility";
             addFacility.Text = "Add Facility";
             addFacility.Click += new EventHandler(addFacilityFunction);
             areYouAdmin.Controls.Add(addFacility);
             

             

             
             
           }
           else {
                //Below executed if user logged in is not an admin.

               areYouAdmin.InnerHtml = "<script>document.GetElementById(areYouAdmin).style.visibility=\"hidden\"</script>";
               
                 }

           


            connect.Close();
        
        
        }

        protected void addFacilityFunction(Object sender, EventArgs e) //This is executed when the add facility button is pressed.
        {

            
            scriptDiv.InnerHtml = "<script>alert(\"Facility successfully added!\")</script>";
        
        
        }
                                 

        
        
    }
}
