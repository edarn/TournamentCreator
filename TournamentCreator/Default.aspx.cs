using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initContents();
            }
        }

        public bool initContents()
        {
            bool contentsInit = false;
            try
            {
                // if user is logged in
                if (Session["currUser"] != null)
                {
                    contentsInit = initContentsUser();
                }
                // if the user is not logged in
                else 
                {
                    contentsInit = initContentsGuest();
                }
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        public bool initContentsUser()
        {
            bool contentsInit = false;
            try
            {
                CUserInfo oUser = (CUserInfo)Session["currUser"];

                // initializing the top labels
                lblTop.Text = "Welcome <a href=\"#\">" + oUser.FirstName + " " + oUser.LastName + " </a> | <a href=\"UserManager/Signout.aspx\">Sign Out </a>";
                contentsInit = true;

                // initializing the neviataion list
                string strNavList = "<li><a href=\"#\">Home</a></li>";
                strNavList += "<li><a href=\"TournamentManager/TournamentsList.aspx\">Tournaments</a></li>";
                strNavList += "<li><a href=\"TournamentManager/MyGames.aspx\">My Games</a></li>";
                strNavList += "<li><a href=\"UserManager/MyProfile.aspx\">My Profile</a></li>";
                navlist.InnerHtml = strNavList;

                // initializing the paths
                lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                lblNavigation.Text += "<a href=\"#\">Home</a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>Manage your tournaments and matches</h2>";
                contentsInit = true;
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        public bool initContentsGuest()
        {
            bool contentsInit = false;
            try
            {
                // initializing the top labels
                lblTop.Text = "Welcome <a href=\"UserManager/Login.aspx\">Sign in </a> | or <a href=\"UserManager/Signup.aspx\">Sign up </a>";
                contentsInit = true;

                // initializing the neviataion list
                string strNavList = "<li><a href=\"#\">Home</a></li>";
                strNavList += "<li><a href=\"TournamentManager/TournamentsList.aspx\">Tournaments</a></li>";
                navlist.InnerHtml = strNavList;

                // initializing the paths
                lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                lblNavigation.Text += "<a href=\"#\">Home</a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>Manage your tournaments, by Signin or Signup</h2>";
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }
    }
}
