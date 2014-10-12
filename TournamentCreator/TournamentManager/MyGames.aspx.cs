using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TournamentCreator.Common;
using System.Collections;


namespace TournamentCreator.TournamentManager
{
    public partial class MyGames : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initContents();
                string strOpt = cmbShowGames.SelectedValue;
                string strSortBy = Request.QueryString["ob"] != null ? Request.QueryString["ob"] : "TournamentName";
                string strSortOrder = Request.QueryString["ot"] != null ? Request.QueryString["ot"] : "asc";


                CTournament oTournamentMgr = new CTournament();
                string strTableHeader = "";

                strTableHeader += "<tr><th style=\"background-color:#877065;\"><a href=\"MyGames.aspx?ob=GameName";
                strTableHeader += "&ot=" + ((strSortBy == "GameName" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Game Name";
                if (strSortBy == "GameName" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "GameName" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<tr><th style=\"background-color:#877065;\"><a href=\"MyGames.aspx?ob=TournamentName";
                strTableHeader += "&ot=" + ((strSortBy == "TournamentName" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Tournament Name";
                if (strSortBy == "TournamentName" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "TournamentName" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                
                strTableHeader += "</tr>";

                thGamesList.InnerHtml = strTableHeader;
                CUserInfo oUser = (CUserInfo)Session["currUser"];

                #region repeater
                CGame oGameMgr = new CGame();
                ArrayList alGameList = oGameMgr.GetUserGames(oUser.UserID, strSortBy, strSortOrder, strOpt);

                DataTable dtGameList = new DataTable();
                dtGameList.Columns.Add("GameName");
                dtGameList.Columns.Add("TournamentName");
                dtGameList.Columns.Add("ref");

                for (int i = 0; i < alGameList.Count; i++)
                {
                    TableRow trRow = new TableRow();
                    CUserGameInfo oGameInfo = (CUserGameInfo)alGameList[i];
                    string strGameName = oGameInfo.GameName;
                    string strTournamentName = oGameInfo.TournamentName;
                    string strRef = "";
                    if(oGameInfo.NumResults > 0)
                        strRef = "GameResult.aspx?rid=" + oGameInfo.GameResultID;
                    else if(oGameInfo.UnverifiedGameResultID > 0)
                        strRef = "UGameResult.aspx?rid=" + oGameInfo.GameResultID;
                    else if (oGameInfo.UnverifiedGameResultID > 0)
                        strRef = "USubmitResult.aspx?gid=" + oGameInfo.GameID;

                    TableCell tCell1 = new TableCell();
                    tCell1.Text = strGameName;
                    TableCell tCell2 = new TableCell();
                    tCell2.Text = strTournamentName;
                    TableCell tCell3 = new TableCell();
                    tCell3.Text = strRef;

                    trRow.Cells.Add(tCell1);
                    trRow.Cells.Add(tCell2);
                    trRow.Cells.Add(tCell3);
                }

                repGamesList.DataSource = dtGameList;
                repGamesList.DataBind();

                if (repGamesList.Items.Count == 0)
                    tdNoRecord.Visible = true;
                else
                    tdNoRecord.Visible = false;
                #endregion

            }
        }

        public bool initContents()
        {
            bool contentsInit = false;
            try
            {
                cmbShowGames.Items.Add(new ListItem("All", "all"));
                cmbShowGames.Items.Add(new ListItem("Played", "played"));
                cmbShowGames.Items.Add(new ListItem("Not Played", "notplayed"));
                cmbShowGames.Items.Add(new ListItem("Pending Results", "pendingresults"));
                cmbShowGames.Items.Add(new ListItem("Invalid Results", "invalidresults"));

                // if user is logged in
                if (Session["currUser"] != null)
                {
                    contentsInit = initContentsUser();
                }
                // if the user is not logged in
                else
                {
                    Response.Redirect("../Notify.aspx?mc=10090020");
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
                strNavList += "<li><a href=\"#\">Tournaments</a></li>";
                strNavList += "<li><a href=\"MyGames.aspx\">My Games</a></li>";
                strNavList += "<li><a href=\"../UserManager/MyProfile.aspx\">My Profile</a></li>";
                navlist.InnerHtml = strNavList;

                // initializing the paths
                lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"#\"> Tournaments </a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>Manage your tournaments</h2>";
                contentsInit = true;
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        protected void cmbShowGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            CUserInfo oUser = (CUserInfo)Session["currUser"];
            CGame oGameMgr = new CGame();
            string strOpt = cmbShowGames.SelectedValue;
            string strSortBy = Request.QueryString["ob"] != null ? Request.QueryString["ob"] : "TournamentName";
            string strSortOrder = Request.QueryString["ot"] != null ? Request.QueryString["ot"] : "asc";


            #region repeater
            
            ArrayList alGameList = oGameMgr.GetUserGames(oUser.UserID, strSortBy, strSortOrder, strOpt);

            DataTable dtGameList = new DataTable();
            dtGameList.Columns.Add("GameName");
            dtGameList.Columns.Add("TournamentName");
            dtGameList.Columns.Add("ref");

            for (int i = 0; i < alGameList.Count; i++)
            {
                TableRow trRow = new TableRow();
                CUserGameInfo oGameInfo = (CUserGameInfo)alGameList[i];
                string strGameName = oGameInfo.GameName;
                string strTournamentName = oGameInfo.TournamentName;
                string strRef = "";
                if (oGameInfo.NumResults > 0)
                    strRef = "GameResult.aspx?rid=" + oGameInfo.GameResultID;
                else if (oGameInfo.UnverifiedGameResultID > 0)
                    strRef = "UGameResult.aspx?rid=" + oGameInfo.GameResultID;
                else if (oGameInfo.UnverifiedGameResultID > 0)
                    strRef = "USubmitResult.aspx?gid=" + oGameInfo.GameID;

                TableCell tCell1 = new TableCell();
                tCell1.Text = strGameName;
                TableCell tCell2 = new TableCell();
                tCell2.Text = strTournamentName;
                TableCell tCell3 = new TableCell();
                tCell3.Text = strRef;

                trRow.Cells.Add(tCell1);
                trRow.Cells.Add(tCell2);
                trRow.Cells.Add(tCell3);
            }

            repGamesList.DataSource = dtGameList;
            repGamesList.DataBind();

            if (repGamesList.Items.Count == 0)
                tdNoRecord.Visible = true;
            else
                tdNoRecord.Visible = false;
            #endregion

            
        }
    }
}