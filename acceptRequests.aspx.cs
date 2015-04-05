using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace Team11
{
    public partial class acceptRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Populating a DataTable from database.
                DataTable dt = this.GetData();

                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Table start.
                html.Append("<table border = '1'>");

                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");

                //Building the Data rows.
                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr>");
                    bool reference = true;
                    foreach (DataColumn column in dt.Columns)
                    {
                      //  string compare = row[column.ColumnName].ToString();
                        if (reference)
                        {
                            string columnName = row[column.ColumnName].ToString();

                            html.Append("<td>");
                            html.Append("<a href=\"acceptRequests2.aspx?userID="+columnName+"\">"+row[column.ColumnName]+"</a>");
                            html.Append("</td>");
                            reference = false;

                        }
                        else { 
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    
                    }
                    }
                    html.Append("</tr>");
                }

                //Table end.
                html.Append("</table>");

                //Append the HTML string to Placeholder.
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            }
        }

        private DataTable GetData()
        {
            string constr = ConfigurationManager.ConnectionStrings["AdminConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT requestID, moduleCode, status, weekID , day, periodStart, periodEnd, semester, year, round FROM Request"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
    }
}
