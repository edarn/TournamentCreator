using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator.UserManager
{
    public partial class ForgetPassword : System.Web.UI.Page
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
                Response.Redirect("../Notify.aspx?mc=10090020");
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
                strNavList += "<li><a href=\"../TournamentManager/TournamentsList.aspx\">Tournaments</a></li>";
                navlist.InnerHtml = strNavList;

                // initializing the paths
                lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"Login.aspx\">Login</a> > <a href=\"#\">Recover Password</a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>Enter email address and password to login</h2>";


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
                        lblError.Text = oMsg.GetHTMLMessageString(nMC);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strEmail = txtEmailAddress.Text.Trim();
            CMail oMailMgr = new CMail();
            CMessages oMsg = new CMessages();
            CUser oUserMgr = new CUser();

            if (string.IsNullOrEmpty(strEmail))
            {
                Response.Redirect("ForgetPassword.aspx?mc=10090010");
            }
            if (oUserMgr.IsEmailExists(strEmail))
            {
                if (oMailMgr.SendPasswordRecoveryMail(strEmail))
                    Response.Redirect("../Notify.aspx?mc=10020011");
                else
                    Response.Redirect("ForgetPassword.aspx?mc=10020010");
            }
            else
            {
                Response.Redirect("ForgetPassword.aspx?mc=10090050");
            }
        }
    }
}