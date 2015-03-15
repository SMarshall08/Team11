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

            string listSQL = "SELECT facilityID,facilityName FROM Facility";

            SqlConnection deleteFacilitiesConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand(listSQL, deleteFacilitiesConnection);
            SqlDataReader reader;

            try
            {
                if (!Page.IsPostBack)
                {

                    facilityList.Items.Clear(); //Clears the drop down list (for deleting facilities). Without this, every time the page is reloaded, the list would get bigger and bigger.

                    ListItem newItem = new ListItem();
                    newItem.Text = "Choose a facility";
                    newItem.Value = "test";
                    facilityList.Items.Add(newItem);

                    deleteFacilitiesConnection.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        newItem = new ListItem();
                        newItem.Text = reader["facilityName"].ToString();
                        newItem.Value = reader["facilityID"].ToString();
                        facilityList.Items.Add(newItem);
                    }
                    reader.Close();
                }
            }
            catch (Exception err)
            {
                //TODO
            }
            finally
            {
                deleteFacilitiesConnection.Close();
            }
            

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



             
             
             
             addFacility.Click += new EventHandler(addFacilityFunction);
             deleteFacility.Click += new EventHandler(deleteFacilityFunction);
             
             

             

             
             
           }
           else {
                //Below executed if user logged in is not an admin.

               areYouAdmin.InnerHtml = "You are not logged in as an admin. Please log in to an account with administrator privileges in order to view the admin options.";
                //Because the inner html is set to the above, it rewrites all the html in admin.aspx, rendering the page blank for those who are not admin.
               
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
        protected void deleteFacilityFunction(Object sender, EventArgs e) //This is executed when the delete facility button is pressed.
        {



            string selectedFacility = facilityList.SelectedItem.Value;
            Response.Write(selectedFacility);
            
            SqlConnection connect3 = new SqlConnection(WebConfigurationManager.ConnectionStrings["AdminConnectionString"].ToString());
            connect3.Open();
            string deleteFacilityString = "DELETE FROM Facility WHERE facilityID=" + selectedFacility;   
            SqlCommand deleteFacilityCommand = new SqlCommand(deleteFacilityString, connect3);
            deleteFacilityCommand.ExecuteNonQuery();

            scriptDiv.InnerHtml = "<script>alert(\"Facility successfully deleted!\")</script>";
            


        }
                                 

        
        
    }
}
