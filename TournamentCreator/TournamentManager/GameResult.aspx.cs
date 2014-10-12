using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator.TournamentManager
{
    public partial class GameResult : System.Web.UI.Page
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

            string strRID = Request.QueryString["rid"] != null ? Request.QueryString["rid"] : "";
            int nRID = 0;

            // check if id is valid
            if (!int.TryParse(strRID, out nRID))
            {
                Response.Redirect("../Notify.aspx?mc=10090060");
            }
            if (nRID <= 0)
            {
                Response.Redirect("../Notify.aspx?mc=10090060");
            }



            if (Session["currUser"] == null)
            {
                
                
                initContentsGuest();
            }
            else
            {
                initContentsUser();
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

            #region populate data

            CGame oGameMgr = new CGame();
            CGameResultInfo oGameResult = oGameMgr.GetGameResultDetails(nRID);
            if(oGameResult == null)
                Response.Redirect("../Notify.aspx?mc=10090060");


            lblTxtGameName.Text = "Game : ";
            lblGameName.Text = oGameResult.FirstPlayerName + " vs " + oGameResult.SecondPlayerName;
            lblTxtFirstPlayerWonSet.Text = oGameResult.FirstPlayerName + " Won Sets : ";
            lblFirstPlayerWonSet.Text = oGameResult.FirstPlayerWonSet.ToString() ;
            lblTxtSecondPlayerWonSet.Text = oGameResult.SecondPlayerName + " Won Sets : ";
            lblSecondPlayerWonSet.Text = oGameResult.SecondPlayerWonSet.ToString() ;

            lblTxtFirstPlayserLostSet.Text = oGameResult.FirstPlayerName + " Lost Sets : ";
            lblFirstPlayerWonSet.Text = oGameResult.FirstPlayerWonSet.ToString();
            lblTxtSecondPlayserLostSet.Text = oGameResult.SecondPlayerName + " Lost Sets : ";
            lblSecondPlayerWonSet.Text = oGameResult.SecondPlayerWonSet.ToString();

            lblTxtWinner.Text = "Winner : ";
            lblWinner.Text = oGameResult.WinnerName;
            

            if (Session["currUser"] != null)
            {
                CUserInfo oUser = (CUserInfo)Session["currUser"];
                if (oUser.UserLevel == "admin")
                {
                    spOptions.InnerHtml = "<a href=\"SubmitResult.aspx?e=1&rid=\"" + nRID.ToString() + ">Edit</a>";
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
                if (oUser.UserLevel == "admin")
                    spOptions.InnerHtml = "<a hre=SubmitResult?e=1&rid=" + Request.QueryString["rid"];
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

        public bool initContentsGuest()
        {
            bool contentsInit = false;
            try
            {
                
                // initializing the top labels
                lblTop.Text = "Welcome <a href=\"UserManager/Login.aspx\">Sign in </a> | or <a href=\"UserManager/Signup.aspx\">Sign up </a>";
                contentsInit = true;

                // initializing the neviataion list
                string strNavList = "<li><a href=\"../Default.aspx\">Home</a></li>";
                strNavList += "<li><a href=\"TournamentsList.aspx\">Tournaments</a></li>";
                navlist.InnerHtml = strNavList;

                // initializing the paths
                lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"#\"> Tournaments </a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>Manage your tournaments, by Signin or Signup</h2>";
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }
    }
}