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
    public partial class ChangePassword : System.Web.UI.Page
    {
        int userID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(Session["userID"]);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ErrorLabel.Text = " ";
            //Clear the label holding msg for user

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["myConnectionString"].ToString())){
                conn.Open();
                // look up the users password so it can be compared to the entered value
                string checkpassword = String.Format("Select password from [User] where userId={0}", userID);
                SqlCommand passwordCmd = new SqlCommand(checkpassword, conn);
                // Gets rid of the space if there is one e.g. by habit putting a space at the end
                string password = passwordCmd.ExecuteScalar().ToString().Replace(" ", "");
                conn.Close();

                        
                
                if (password == CurrentPassTextBox.Text){
                       

                    if (NewPassTextBox.Text == NewPassTextBox2.Text){
                        if (NewPassTextBox.Text.Length >= 6){
                            //SQL Updating password goes into here
                            ErrorLabel.Text = "Password has been updated. (Hasn't really, UPDATE SQL not in yet)";
                        }
                        else{
                            //***Error for when not longer than 6 chars***
                            ErrorLabel.Text = "Error - Your new password is not longer than 6 characters.";
                        }
                    }
                    else{
                        //***Error for entered two different new passwords***
                        ErrorLabel.Text = "Error - You have entered two different new passwords.";
                    }          
                }
                else{ 
                    //Incorrect password error msg goes here
                    ErrorLabel.Text = "Error - The current password entered is incorrect.";
                }

            }
        }
    }
}