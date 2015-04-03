using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
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
        }
    }
}
