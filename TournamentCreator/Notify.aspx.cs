using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator
{
    public partial class Notify : System.Web.UI.Page
    {
    

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initContents();
            }
        }

        private bool initContents()
        {
            bool contentsInit = false;

            // if user is logged in
            if (Session["currUser"] != null)
            {
                contentsInit = initContentsUser();
            }
            // if the user is not logged in
            {
                contentsInit = initContentsGuest();
            }


            return contentsInit;
        }

        private bool initContentsUser()
        {
            bool contentsInit = false;
            try
            {
                contentsInit = true;
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        private bool initContentsGuest()
        {
            bool contentsInit = false;
            try
            {
                // initializing the top labels
                lblTop.Text = "Welcome <a href=\"UserManager/Login.aspx\">Sign in </a> | or <a href=\"UserManager/Signup.aspx\">Sign up </a>";
                contentsInit = true;

                // initializing the neviataion list
                string strNavList = "<li><a href=\"../Default.aspx\">Home</a></li>";
                strNavList += "<li><a href=\"TournamentManager/TournamentsList.aspx\">Tournaments</a></li>";
                navlist.InnerHtml = strNavList;

                // initializing the paths
                lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"#\">Notification</a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>Notification</h2>";


                #region Error Label

                //get message code
                string strMC = Request.QueryString["mc"] != null ? Request.QueryString["mc"] : "";

                //verify the code
                if (!string.IsNullOrEmpty(strMC))
                {
                    CMessages oMsg = new CMessages();
                    int nMC = 0;
                    // get the HTML msg
                    if (int.TryParse(strMC, out nMC))
                    {
                        lblNotification.Text = oMsg.GetHTMLMessageString(nMC);
                    }
                    else
                    {
                        lblNotification.Text = "Error displaying notification.";
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }
    }
}