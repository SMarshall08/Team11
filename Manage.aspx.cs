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
    public partial class Manage : System.Web.UI.Page
    {
        string moduleCode = "";
        int userID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // read the userid from the querystring
            userID = Convert.ToInt32(Session["userID"]);
        }

        protected void RadioButtonListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Search preference by Room or Date, hide the unselected one
            if (this.RadioButtonListView.SelectedIndex == 0)
            {
                this.divByRoom.Visible = true;
                this.divByDate.Visible = false;
            }
            else
            {
                this.divByRoom.Visible = false;
                this.divByDate.Visible = true;
               // this.divBookingByRoom.Visible = false;
            }

        }

    }
}