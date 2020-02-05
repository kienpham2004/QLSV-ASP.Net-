using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Doanbaove
{
    public partial class trangchu : System.Web.UI.MasterPage
    {
        public string mn = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            mn = Request.QueryString.Get("menu");


            if (Session["loginsys"] == null)
            {
                Response.Redirect("~/loginView.aspx");

            }
            else
            {
                lbl_login.Text = Session["loginsys"].ToString();
                lbl_user.Text = Session["loginsys"].ToString();
            }


        }
    }
}