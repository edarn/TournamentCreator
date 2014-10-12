using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator.TournamentManager
{
    public partial class USubmitResult : System.Web.UI.Page
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

            string strEdit = Request.QueryString["e"] != null ? Request.QueryString["e"] : "";

            if (strEdit == "1")
            {
                lblEdit.Text = "1";
                string strRID = Request.QueryString["rid"] != null ? Request.QueryString["rid"] : "";

                lblResultID.Text = strRID;
                int nRID = 0;

                if (!int.TryParse(strRID, out nRID))
                    Response.Redirect("../Notify.aspx?mc=10090060");
                if (nRID <= 0)
                    Response.Redirect("../Notify.aspx?mc=10090060");


                CGame oGameMgr = new CGame();
                CGameResultInfo oGameRes = oGameMgr.GetGameResultDetails(nRID);
                CUserInfo oUser = (CUserInfo)Session["currUser"];

                // check if this is the same user who submitted the result
                if (!oGameMgr.IsResultSubmitedBy(nRID, oUser.UserID))
                    Response.Redirect("../Notify.aspx?mc=10090020");
                tblResultInput.Visible = true;
                

                lblTxtFirstPlayerWonSet.Text = oGameRes.FirstPlayerName + " Won Sets : ";
                lblTxtFirstPlayserLostSet.Text = oGameRes.FirstPlayerName + " Lost Sets : ";
                lblTxtSecondPlayerWonSet.Text = oGameRes.SecondPlayerName + " Won Sets : ";
                lblTxtSecondPlayserLostSet.Text = oGameRes.SecondPlayerName + " Lost Sets : ";
                lblTxtWinner.Text = "Winner : ";

                rdFirstPlayer.Text = oGameRes.FirstPlayerName;
                rdFirstPlayer.Value = oGameRes.FirstUserID.ToString();
                rdSecondPlayer.Text = oGameRes.SecondPlayerName;
                rdSecondPlayer.Value = oGameRes.SecondUserID.ToString();

                txtFirstPlayerWonSet.Text = oGameRes.FirstPlayerWonSet.ToString();
                txtFirstPlayerLostSet.Text = oGameRes.FirstPlayerLostSet.ToString();
                txtSecondPlayerWonSet.Text = oGameRes.SecondPlayerWonSet.ToString();
                txtSecondPlayerLostSet.Text = oGameRes.SecondPlayerLostSet.ToString();

                if (rdFirstPlayer.Value == oGameRes.WinnerID.ToString())
                {
                    rdFirstPlayer.Selected = true;
                }
                else if (rdSecondPlayer.Value == oGameRes.WinnerID.ToString())
                {
                    rdSecondPlayer.Selected = true;
                }
                else
                {
                    rdDraw.Selected = true;
                }
            }
            else
            {
                tblResultInput.Visible = false;
                string strGameID = Request.QueryString["gid"] != null ? Request.QueryString["gid"] : "";
                lblGameID.Text = strGameID;
                int nGameID = 0;
                if (int.TryParse(strGameID, out nGameID))
                {
                    if (nGameID > 0)
                    {
                        CGame oGameMgr = new CGame();
                        CGameInfo oGame = oGameMgr.GetGameDetails(nGameID);
                        if (oGame != null)
                        {
                            tblResultInput.Visible = true;
                            lblTxtFirstPlayerWonSet.Text = oGame.FirstPlayerName + " Won Sets : ";
                            lblTxtFirstPlayserLostSet.Text = oGame.FirstPlayerName + " Lost Sets : ";
                            lblTxtSecondPlayerWonSet.Text = oGame.SecondPlayerName + " Won Sets : ";
                            lblTxtSecondPlayserLostSet.Text = oGame.SecondPlayerName + " Lost Sets : ";
                            lblTxtWinner.Text = "Winner : ";

                            rdFirstPlayer.Text = oGame.FirstPlayerName;
                            rdFirstPlayer.Value = oGame.FirstPlayerID.ToString();
                            rdSecondPlayer.Text = oGame.SecondPlayerName;
                            rdSecondPlayer.Value = oGame.SecondPlayerID.ToString();
                        }
                        else
                            Response.Redirect("../Notify.aspx?mc=10090060");
                    }
                    else
                        Response.Redirect("../Notify.aspx?mc=10090060");
                }
                Response.Redirect("../Notify.aspx?mc=10090060");
            }


            return bContentsInit;
        }

        public bool initContentsUser()
        {
            bool contentsInit = false;
            try
            {
                CUserInfo oUser = (CUserInfo)Session["currUser"];

                if (oUser.UserLevel != "admin")
                    Response.Redirect("../Notify.aspx?mc=10090020");

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
                lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"TournamentsList.aspx\"> Tournaments </a> > <a href=\"#\">Submit Result</a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>Submit Result</h2>";
                contentsInit = true;
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lblEdit.Text == "1")
            {
                string strFirstPlayerWonSets = txtFirstPlayerWonSet.Text;
                string strFirstPlayerLostSets = txtFirstPlayerLostSet.Text;
                string strSecondPlayerWonSets = txtSecondPlayerWonSet.Text;
                string strSecondPlayerLostSets = txtSecondPlayerLostSet.Text;
                string strWinnerID = rblWinner.SelectedValue;
                string strGameID = lblResultID.Text;

                int nFirstPlayerWonSets, nFirstPlayerLostSets, nSecondPlayerWonSets, nSecondPlayerLostSets, nWinnerID, nGameRID;
                nFirstPlayerWonSets = nFirstPlayerLostSets = nSecondPlayerWonSets = nSecondPlayerLostSets = nWinnerID = nGameRID = 0;

                if (int.TryParse(strFirstPlayerWonSets, out nFirstPlayerWonSets) && int.TryParse(strFirstPlayerLostSets, out nFirstPlayerWonSets) && int.TryParse(strSecondPlayerWonSets, out nSecondPlayerWonSets) && int.TryParse(strSecondPlayerLostSets, out nSecondPlayerLostSets) && int.TryParse(strWinnerID, out nWinnerID) && int.TryParse(strGameID, out nGameRID))
                {
                    CGame oGameMgr = new CGame();
                    CGameResultInfo oResult = new CGameResultInfo();
                    oResult.FirstPlayerWonSet = nFirstPlayerWonSets;
                    oResult.FirstPlayerLostSet = nFirstPlayerLostSets;
                    oResult.SecondPlayerWonSet = nSecondPlayerWonSets;
                    oResult.SecondPlayerLostSet = nSecondPlayerLostSets;
                    oResult.GameResultID = nGameRID;

                    if (oGameMgr.EditUResult(oResult))
                        Response.Redirect("TournamentsList.aspx?mc=10030131");
                    else
                        lblError.Text = "Unable to update result.";
                }
                else
                    lblError.Text = "Some fields have invalid values.";
            }
            else
            {
                string strFirstPlayerWonSets = txtFirstPlayerWonSet.Text;
                string strFirstPlayerLostSets = txtFirstPlayerLostSet.Text;
                string strSecondPlayerWonSets = txtSecondPlayerWonSet.Text;
                string strSecondPlayerLostSets = txtSecondPlayerLostSet.Text;
                string strWinnerID = rblWinner.SelectedValue;
                string strGameID = lblGameID.Text;

                int nFirstPlayerWonSets, nFirstPlayerLostSets, nSecondPlayerWonSets, nSecondPlayerLostSets, nWinnerID, nGameID;
                nFirstPlayerWonSets = nFirstPlayerLostSets = nSecondPlayerWonSets = nSecondPlayerLostSets = nWinnerID = nGameID = 0;

                if (int.TryParse(strFirstPlayerWonSets, out nFirstPlayerWonSets) && int.TryParse(strFirstPlayerLostSets, out nFirstPlayerWonSets) && int.TryParse(strSecondPlayerWonSets, out nSecondPlayerWonSets) && int.TryParse(strSecondPlayerLostSets, out nSecondPlayerLostSets) && int.TryParse(strWinnerID, out nWinnerID) && int.TryParse(strGameID, out nGameID))
                {
                    CUserInfo oUser = (CUserInfo)Session["currUser"];

                    if (oUser.UserID > 0)
                    {
                        CGame oGameMgr = new CGame();
                        CGameResultInfo oResult = new CGameResultInfo();
                        oResult.FirstPlayerWonSet = nFirstPlayerWonSets;
                        oResult.FirstPlayerLostSet = nFirstPlayerLostSets;
                        oResult.SecondPlayerWonSet = nSecondPlayerWonSets;
                        oResult.SecondPlayerLostSet = nSecondPlayerLostSets;
                        oResult.GameID = nGameID;
                        oResult.SubmitterID = oUser.UserID;

                        if (oGameMgr.AddUResult(oResult))
                            Response.Redirect("TournamentsList.aspx?mc=10030141");
                        else
                            lblError.Text = "Unable to add result.";
                    }
                    else
                        lblError.Text = "Unable to add result.";
                }
                else
                    lblError.Text = "Some fields have invalid values.";

            }
        }
    }
}