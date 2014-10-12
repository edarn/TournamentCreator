using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator.UserManager
{
    public partial class AdminSettings : System.Web.UI.Page
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
            if (Session["currUser"] == null)
            {
                Response.Redirect("../Notify.aspx?mc=10090020");
            }
            // if the user is not logged in
            {
                contentsInit = initContentsUser();
            }

            CUser oUserMgr = new CUser();
            if(oUserMgr.IsUserAllowedSubmitResult())
                cbAllowUserResult.Checked = true;
            else
                cbAllowUserResult.Checked = false;

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

            return contentsInit;
        }

        

        private bool initContentsUser()
        {
            bool contentsInit = false;
           
            try
            {
                CUserInfo oUser = (CUserInfo)Session["currUser"];

                if(oUser.UserLevel == "admin")
                {

                    // initializing the top labels
                    lblTop.Text = "Welcome <a href=\"#\">" + oUser.FirstName + " " + oUser.LastName + " </a> | <a href=\"UserManager/Signout.aspx\">Sign Out </a>";
                    contentsInit = true;

                    // initializing the neviataion list
                    string strNavList = "<li><a href=\"#\">Home</a></li>";
                    strNavList += "<li><a href=\"TournamentsList.aspx\">Tournaments</a></li>";
                    strNavList += "<li><a href=\"MyGames.aspx\">My Games</a></li>";
                    strNavList += "<li><a href=\"../UserManager/MyProfile.aspx\">My Profile</a></li>";
                    navlist.InnerHtml = strNavList;

                    // initializing the paths
                    lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                    lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"#\"> Admin Settings </a>";

                    lblHeading.Text = "<h1>Tournament Creator</h1><h2>Create New Tournament</h2>";
                    contentsInit = true;
                }
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strAllow = cbAllowUserResult.Checked == true ? "true" : "false";
            CGlobalAttr oGA = new CGlobalAttr();
            if (oGA.SetGlobalAttr("user-allowed-submit-result", "true"))
                Response.Redirect("AdminSettings.aspx?mc=10010081");
            else
                Response.Redirect("AdminSettings.aspx?mc=10010080");

        }
    }
}