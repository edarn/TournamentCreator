using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;
using System.Data;

namespace TournamentCreator.TournamentManager
{
    public partial class TournamentsList : System.Web.UI.Page
    {

        int nCurrUserID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                initContents();
                string strOpt = cmbShowTournaments.SelectedValue;
                string strSortBy = Request.QueryString["ob"] != null ? Request.QueryString["ob"] : "TournamentName";
                string strSortOrder = Request.QueryString["ot"] != null ? Request.QueryString["ot"] : "asc";

                if ((strOpt == "my" && nCurrUserID == 0) || (strOpt == "myCreated" && nCurrUserID == 0))
                    Response.Redirect("../Notify.aspx?mc=10090020");
                

                CTournament oTournamentMgr = new CTournament();
                string strTableHeader = "";


                strTableHeader += "<tr><th style=\"background-color:#877065;\"><a href=\"TournamentsList.aspx?ob=TournamentName";
                strTableHeader += "&ot=" +  ((strSortBy == "TournamentName" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Name";
                if (strSortBy == "TournamentName" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "TournamentName" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";
                
                
/*
                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"TournamentsList.aspx?ob=Startdate";
                strTableHeader += "&ot=" + ((strSortBy == "StartDate" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">StartDate";
                if (strSortBy == "StartDate" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "StartDate" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"TournamentsList.aspx?ob=StopDate";
                strTableHeader += "&ot=" + ((strSortBy == "StopDate" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Stop Date";
                if (strSortBy == "StopDate" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "StopDate" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";
 * */

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"TournamentsList.aspx?ob=TournamentType";
                strTableHeader += "&ot=" + ((strSortBy == "TournamentType" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Type";
                if (strSortBy == "TournamentType" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "TournamentType" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th>";

                strTableHeader += "<th style=\"background-color:#877065;\"><a href=\"TournamentsList.aspx?ob=IsStarted";
                strTableHeader += "&ot=" + ((strSortBy == "IsStarted" && strSortOrder == "asc") ? "desc" : "asc");
                strTableHeader += "\">Is Started";
                if (strSortBy == "IsStarted" && strSortOrder == "asc")
                    strTableHeader += "&#9650;";
                else if (strSortBy == "IsStarted" && strSortOrder == "desc")
                    strTableHeader += "&#9660;";
                strTableHeader += "</a></th></tr>";

                thTournamentsList.InnerHtml = strTableHeader;
                DataSet dsTournamentList = oTournamentMgr.GetTournamentsList(nCurrUserID, strSortBy, strSortOrder, strOpt);

                repTournametList.DataSource = dsTournamentList;
                repTournametList.DataBind();

                if (repTournametList.Items.Count == 0)
                    tdNoRecord.Visible = true;
                else
                    tdNoRecord.Visible = false;
                
            }
        }

        public bool initContents()
        {
            bool contentsInit = false;
            try
            {
                cmbShowTournaments.Items.Add(new ListItem("All", "all"));
                cmbShowTournaments.Items.Add(new ListItem("Started", "started"));
                cmbShowTournaments.Items.Add(new ListItem("Not Started", "notstarted"));

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
                nCurrUserID = oUser.UserID;

                cmbShowTournaments.Items.Add(new ListItem("My Tournaments", "my"));
                cmbShowTournaments.Items.Add(new ListItem("Tournaments I Created", "myCreated"));

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

        public bool initContentsGuest()
        {
            bool contentsInit = false;
            try
            {
                spCreateTournament.Visible = false;
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

        protected void cmbShowTournaments_SelectedIndexChanged(object sender, EventArgs e)
        {
            CTournament oTournamentMgr = new CTournament();

            string strOpt = cmbShowTournaments.SelectedValue;
            string strSortBy = Request.QueryString["ob"] != null ? Request.QueryString["ob"] : "TournamentName";
            string strSortOrder = Request.QueryString["ot"] != null ? Request.QueryString["ot"] : "asc";
           
            DataSet dsTournamentList = oTournamentMgr.GetTournamentsList(nCurrUserID, strSortBy, strSortOrder, strOpt);

            repTournametList.DataSource = dsTournamentList;
            repTournametList.DataBind();

            if (repTournametList.Items.Count == 0)
                tdNoRecord.Visible = true;
            else
                tdNoRecord.Visible = false;           
        }
    }
}