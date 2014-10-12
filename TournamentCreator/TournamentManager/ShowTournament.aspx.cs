using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TournamentCreator.Common;

namespace TournamentCreator.TournamentManager
{
    public partial class ShowTournament : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initContents();            


                CTournament oTournamentMgr = new CTournament();
                


                

                
            }
        }

        public bool initContents()
        {
            bool contentsInit = false;
            try
            {
                #region init common contents
                // initializing common contents
                string strTournamentName = Request.QueryString["tn"] != null ? Request.QueryString["tn"] : "";
                lbltname.Text = strTournamentName;

                if (string.IsNullOrEmpty(strTournamentName))
                    Response.Redirect("../Notify.aspx?mc=10090060");

                CTournament oTournamentMgr = new CTournament();
                CTournamentInfo oTournament = oTournamentMgr.GetTournamentDetailsByName(strTournamentName);

                if (oTournament == null || oTournament.TournamentID <= 0)
                    Response.Redirect("../Notify.aspx?mc=10090060");

                lbltid.Text = oTournament.TournamentID.ToString();

                lblName.Text = oTournament.TournamentName;
                tdDesc.InnerHtml = oTournament.TournamentDesc;
                lblType.Text = oTournament.TournamentType;

                string strStatus = "";
                if (oTournament.IsStopped)
                    strStatus = "<span style=\"color:Blue\">Stopped</span>";
                else if (oTournament.IsStarted)
                    strStatus = "<span style=\"color:Red\">Started</span>";
                else if (oTournament.IsStarted)
                    strStatus = "<span style=\"color:Green\">Not Started Yet</span>";

                lblStatus.Text = strStatus;

                lblNumPlayers.Text = oTournament.NumPlayers.ToString();

                spMustPlayedIn.Visible = true;
                if (oTournament.TournamentType == "Cup" && oTournament.MustPlayedIn > 0)
                {
                    trMustPlayedIn.Visible = true;
                    lblMustPlayedIn.Text = oTournament.MustPlayedIn.ToString() + " " + "days";
                }
                else
                {
                    trMustPlayedIn.Visible = false;
                }

                if (oTournament.IsStarted)
                {
                    StartedTournament.Visible = true;
                    NonStartedTournament.Visible = false;

                    string strSortBy = Request.QueryString["ob"] != null ? Request.QueryString["ob"] : "TournamentName";
                    string strSortOrder = Request.QueryString["ot"] != null ? Request.QueryString["ot"] : "asc";

                    string strSortBy2 = Request.QueryString["ob"] != null ? Request.QueryString["ob2"] : "TournamentName";
                    string strSortOrder2 = Request.QueryString["ot"] != null ? Request.QueryString["ot2"] : "asc";

                    CreateTableHeaders(strSortBy, strSortOrder, strSortBy2, strSortOrder2);

                    DataSet dsTournamentScores = oTournamentMgr.GetTournamentScores(oTournament.TournamentID, strSortBy, strSortOrder);
                    repScoreBoard.DataSource = dsTournamentScores;
                    repScoreBoard.DataBind();

                    DataSet dsGamesResults = oTournamentMgr.GetTournamentGamesResults(oTournament.TournamentID, strSortBy2, strSortOrder2);
                    repMatchResult.DataSource = dsGamesResults;
                    repMatchResult.DataBind();
                }
                else
                {
                    StartedTournament.Visible = false;
                    NonStartedTournament.Visible = true;
                }
                #endregion

                #region spOptions

                string strOptions = "";
                if(oTournament.IsStopped)
                {
                    ; // do nothing
                }
                else if (oTournament.IsStarted)
                {
                    if (Session["currUser"] != null)
                    {
                        CUserInfo oUser = (CUserInfo)Session["currUser"];

                        if (oTournamentMgr.CheckUserRights(oTournament.TournamentID, oUser.UserID))
                        {
                            // specify action module and action type
                            strOptions = "<a href=\"../GenActions.aspx?am=tournament&at=stop&tid=" + oTournament.TournamentID + "&ref=TournamentManager/ShowTournament?tn=" + oTournament.TournamentName + "\">Stop Tournament</a>";
                            
                        }
                    }
                }
                else
                {
                    if (Session["currUser"] != null)
                    {
                        CUserInfo oUser = (CUserInfo)Session["currUser"];

                        if (oTournamentMgr.CheckUserRights(oTournament.TournamentID, oUser.UserID))
                        {
                            // specify action module and action type
                            strOptions = "<a href=\"CreateTournament.aspx?e=1&tid=" + oTournament.TournamentID + "\">Edit<a> | ";
                            strOptions += "<a href=\"../GenActions.aspx?am=tournament&at=start&tid=" + oTournament.TournamentID + "&ref=TournamentManager/ShowTournament?tn=" + oTournament.TournamentName + "\">Stop Tournament</a>";

                        }

                        if (oUser.UserLevel != "admin")
                        {
                            if (strOptions == "")
                                strOptions += " | ";
                            if (oTournamentMgr.IsUserInTournament(oTournament.TournamentID, oUser.UserID))
                                strOptions += "<a href=\"../GenActions.aspx?am=tournament&at=unjoin&tid=" + oTournament.TournamentID + "&ref=TournamentManager/ShowTournament?tn=" + oTournament.TournamentName + "\">Unjoin Tournament</a>";
                            else
                                strOptions += "<a href=\"../GenActions.aspx?am=tournament&at=join&tid=" + oTournament.TournamentID + "&ref=TournamentManager/ShowTournament?tn=" + oTournament.TournamentName + "\">Join Tournament</a>";
                        }
                        else
                        {
                            if (strOptions == "")
                                strOptions += " | ";
                            strOptions += "<a href=\"#\" onClick=\"if(confirm('Are you sure you want to delete this Tournament?')) window.location='../GenActions.aspx?am=tournament&at=delete&tid=" + oTournament.TournamentID + "&ref=TournamentManager/ShowTournament?tn=" + oTournament.TournamentName + "' \">Delete Tournament</a>";
                        }
                    }
                }

                #endregion

                    // if user is logged in
                    if (Session["currUser"] != null)
                    {
                        contentsInit = initContentsUser(oTournament);
                    }
                    // if the user is not logged in
                    else
                    {
                        contentsInit = initContentsGuest(oTournament);
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
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        public bool CreateTableHeaders(string strSortBy, string strSortOrder, string strSortBy2, string strSortOrder2)
        {
            bool bHeadersCreated = true;
            
            try
            {
                string strTableHeader = "";

                #region first header

                strTableHeader += "<tr><th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob=FirstName";
                strTableHeader += "&ot=" + ((strSortBy == "FirstName" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Name";
                if (strSortBy == "FirstName" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "FirstName" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob=Score";
                strTableHeader += "&ot=" + ((strSortBy == "Score" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Score";
                if (strSortBy == "Score" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "Score" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob=WonMatches";
                strTableHeader += "&ot=" + ((strSortBy == "WonMatches" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Won Matches";
                if (strSortBy == "WonMatches" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "WonMatches" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob=LostMatches";
                strTableHeader += "&ot=" + ((strSortBy == "LostMatches" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Lost Matches";
                if (strSortBy == "LostMatches" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "LostMatches" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob=WonSets";
                strTableHeader += "&ot=" + ((strSortBy == "IsStarted" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Won Sets";
                if (strSortBy == "WonSets" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "WonSets" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob=LostSets";
                strTableHeader += "&ot=" + ((strSortBy == "IsStarted" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Lost Sets";
                if (strSortBy == "LostSets" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "LostSets" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob=NumGamesPlayed";
                strTableHeader += "&ot=" + ((strSortBy == "IsStarted" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Played Games";
                if (strSortBy == "NumGamesPlayed" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "NumGamesPlayed" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "</tr>";

                thScoreBoard.InnerHtml = strTableHeader;

                #endregion

                #region second header

                strTableHeader = "";

                strTableHeader += "<tr><th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob2=GameName";
                strTableHeader += "&ot2=" + ((strSortBy == "GameName" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Name";
                if (strSortBy == "GameName" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "GameName" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob2=FirstPlayer";
                strTableHeader += "&ot2=" + ((strSortBy == "FirstPlayer" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">First Player";
                if (strSortBy == "FirstPlayer" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "Score" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob2=SecondPlayer";
                strTableHeader += "&ot2=" + ((strSortBy == "SecondPlayer" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Second Player";
                if (strSortBy == "SecondPlayer" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "SecondPlayer" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"ShowTournament.aspx?ob2=WinnerName";
                strTableHeader += "&ot2=" + ((strSortBy == "WinnerName" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Winner";
                if (strSortBy == "WinnerName" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "WinnerName" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

               

                strTableHeader += "</tr>";
                thMatchResult.InnerHtml = strTableHeader;

                #endregion
            }
            catch(Exception ex)
            {
            }
            return bHeadersCreated;
        }

        public bool initContentsUser(CTournamentInfo oTournament)
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
                lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"TournamentsList.aspx\"> Tournaments </a> > <a href=\"#\"> Show Tournament </a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>"+ oTournament.TournamentName + "</h2>";
                contentsInit = true;

                if (oUser.UserLevel == "admin")
                {
                    trMustPlayedIn.Visible = true;
                    spMustPlayedIn.Visible = true;
                    txtMustPlayedIn.Text = oTournament.MustPlayedIn.ToString();
                    lblMustPlayedIn.Text = " days (0 means Unlimited)";
                }

                
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        public bool initContentsGuest(CTournamentInfo oTournament)
        {
            bool contentsInit = false;
            try
            {
                CUserInfo oUser = (CUserInfo)Session["currUser"];


                // initializing the top labels
                lblTop.Text = "Welcome <a href=\"UserManager/Login.aspx\">Sign in </a> | or <a href=\"UserManager/Signup.aspx\">Sign up </a>";
                contentsInit = true;


                // initializing the neviataion list
                string strNavList = "<li><a href=\"\">Home</a></li>";
                strNavList += "<li><a href=\"TournamentsList.aspx\">Tournaments</a></li>";
                navlist.InnerHtml = strNavList;

                // initializing the paths
                lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"TournamentsList.aspx\"> Tournaments </a> > <a href=\"#\"> Show Tournament </a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>" + oTournament.TournamentName + "</h2>";
                contentsInit = true;

                if (oUser.UserLevel == "admin")
                {
                    trMustPlayedIn.Visible = true;
                    spMustPlayedIn.Visible = true;
                    txtMustPlayedIn.Text = oTournament.MustPlayedIn.ToString();
                    lblMustPlayedIn.Text = " days (0 means Unlimited)";
                }


            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            CTournament oTournamentMgr = new CTournament();
            string strMaxDays = txtMustPlayedIn.Text;
            string strTournamentID = lbltid.Text;
            int nTournamentID = 0, nMaxDays = 0;

            if (string.IsNullOrEmpty(strMaxDays))
                Response.Redirect("ShowTournament.aspx?tn=" + lbltname + "&mc=10030040");
           
            if(!int.TryParse(strTournamentID, out nTournamentID) || !int.TryParse(strMaxDays, out nMaxDays))
                Response.Redirect("ShowTournament.aspx?tn=" + lbltname + "&mc=10030040");

            if(oTournamentMgr.SetTournamentMaxDays(nTournamentID, nMaxDays))
                Response.Redirect("ShowTournament.aspx?tn=" + lbltname + "&mc=10030041");

            Response.Redirect("ShowTournament.aspx?tn=" + lbltname + "&mc=10030040");
        }
    }
}