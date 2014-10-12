using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator.TournamentManager
{
    public partial class CreateTournament : System.Web.UI.Page
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
            bool bContentsInit = false;
            // if the user is not logged in deny the access to this page
            if (Session["currUser"] == null)
            {
                Response.Redirect("../Notify.aspx?mc=10090020");
            }

            initContentsUser();

            string strEdit = Request.QueryString["e"] != null ? Request.QueryString["e"] : "";
            if (strEdit == "1")
            {
                string strTID = Request.QueryString["tid"] != null ? Request.QueryString["tid"] : "";
                lblTID.Text = strTID;
                int nTID = 0;
                if (int.TryParse(strTID, out nTID))
                {
                    CTournament oTouramentMgr = new CTournament();
                    CTournamentInfo oTournament = oTouramentMgr.GetTournamentDetailsByID(nTID);
                    if (oTournament != null)
                    {
                        txtName.Text = oTournament.TournamentName;
                        txtName.Enabled = false;
                        txtDesc.Text = oTournament.TournamentDesc;
                        txtMaxNum.Text = oTournament.MaxNumPlayers.ToString();
                        if (!oTournament.StartManually)
                        {
                            liStartAuto.Selected = true;
                            cldStartDate.SelectedDate = oTournament.StartDate;
                        }

                        if (!oTournament.StopManually)
                        {
                            liStopAuto.Selected = true;
                            cldStopDate.SelectedDate = oTournament.StopDate;
                        }

                        if (!(oTournament.TournamentType == "Cup"))
                            liEAE.Selected = true;

                        Page.Title = "Tournament Creator :: Edit Tournament";

                        lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                        lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"TournamentsList.aspx\"> Tournaments </a> > <a href=\"#\">Edit Tournament</a> >";

                        lblHeading.Text = "<h1>Tournament Creator</h1><h2>Edit Tournament</h2>";

                        btnSubmit.Text = "Update Tournament";

                    }
                    else
                        Response.Redirect("../Notify.aspx?mc=10090060");
                }
                else
                    Response.Redirect("../Notify.aspx?mc=10090060");
            }

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

            return bContentsInit;
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
                strNavList += "<li><a href=\"TournamentsList.aspx\">Tournaments</a></li>";
                strNavList += "<li><a href=\"MyGames.aspx\">My Games</a></li>";
                strNavList += "<li><a href=\"../UserManager/MyProfile.aspx\">My Profile</a></li>";
                navlist.InnerHtml = strNavList;

                // initializing the paths
                lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"TournamentsList.aspx\"> Tournaments </a> > <a href=\"#\">Create Tournament</a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>Create New Tournament</h2>";
                contentsInit = true;
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            CTournament oTournamentMgr = new CTournament();
            CTournamentInfo oTournament = new CTournamentInfo();
            CUserInfo oUser = (CUserInfo)Session["currUser"];

            string strName = txtName.Text.Trim();
            string strDesc = txtDesc.Text.Trim();
            bool bStartMan = liStartMan.Selected;       // whether or not to start manually
            DateTime oStartDate = cldStartDate.SelectedDate;
            bool bStopMan = liStopMan.Selected;
            DateTime oStopDate = cldStopDate.SelectedDate;
            string strType = liCup.Selected == true ? "Cup" : "Everyone Against Everyone";
            string strMaxNum = txtMaxNum.Text;

            int nMaxNum = 0;

            // empty check
            if (string.IsNullOrEmpty(strName) || string.IsNullOrEmpty(strDesc))
                Response.Redirect("CreateTournament.aspx?mc=10090010");

            if (!int.TryParse(strMaxNum, out nMaxNum))
                Response.Redirect("CreateTournament.aspx?mc=10090010");

            // the stop date cant be less than the start date
            if (!bStartMan && !bStopMan && oStopDate < oStartDate)
                Response.Redirect("CreateTournament.aspx?mc=10030010");
            
            if(nMaxNum < 2)
                Response.Redirect("CreateTournament.aspx?mc=10030010");

            if(oTournamentMgr.IsTournamentNameExists(strName))
                Response.Redirect("CreateTournament.aspx?mc=10030030");

            if (oUser.UserID > 0)
            {
                oTournament.CreatorID = oUser.UserID;
                oTournament.TournamentName = strName;
                oTournament.TournamentDesc = strDesc;
                oTournament.TournamentType = strType;
                oTournament.StartManually = bStartMan;
                oTournament.StopManually = bStopMan;
                oTournament.StartDate = oStartDate;
                oTournament.StopDate = oStopDate;

                if (oTournamentMgr.CreateTournament(oTournament))
                    Response.Redirect("TournamentsList.aspx?mc=10030021");
                else
                    Response.Redirect("CreateTournament.aspx?mc=10030020");
            }
            else
            {
                Response.Redirect("CreateTournament.aspx?mc=10030020");
            }

        }
    }
}