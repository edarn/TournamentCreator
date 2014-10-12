using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator
{
    public partial class GenActions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strModule = Request.QueryString["am"] != null ? Request.QueryString["am"] : ""; // module
            string strActionType = Request.QueryString["at"] != null ? Request.QueryString["at"] : ""; // action type
            if (string.IsNullOrEmpty(strModule))
                Response.Redirect("Notify.aspx?mc=10090020");

            if (strModule == "tournament")
            {
                CUserInfo oUser = (CUserInfo)Session["currUser"];
                CTournament oTournamentMgr = new CTournament();
                string strTournamentID = Request.QueryString["tid"] != null ? Request.QueryString["tid"] : "";
                string strRef = Request.QueryString["ref"];
                int nTournamentID = 0;

                if(!int.TryParse(strTournamentID, out nTournamentID))
                    Response.Redirect("Notify.aspx?mc=10090020");
                else if(nTournamentID <= 0)
                    Response.Redirect("Notify.aspx?mc=10090020");

                // start tournament
                if (strActionType == "start")
                {
                    if (oTournamentMgr.CheckUserRights(nTournamentID, oUser.UserID))
                    {
                        if(oTournamentMgr.GetNumberOfPlayers(nTournamentID) < 2)
                            Response.Redirect(strRef + "&mc=10030120");
                        // check the number of minimum of players
                        if (oTournamentMgr.StartTournament(nTournamentID))
                            Response.Redirect(strRef + "&mc=10030051");
                        else
                            Response.Redirect(strRef + "&mc=10030050");
                    }
                    else
                        Response.Redirect("Notify.aspx?mc=10090020");
                }

                // stop tournament
                else if (strActionType == "stop")
                {
                    if (oTournamentMgr.CheckUserRights(nTournamentID, oUser.UserID))
                    {
                        if(oTournamentMgr.StopTournament(nTournamentID))
                            Response.Redirect(strRef + "&mc=10030061");
                        else
                            Response.Redirect(strRef + "&mc=10030060");
                    }
                    else
                        Response.Redirect("Notify.aspx?mc=10090020");
                }

                // join tournament
                else if (strActionType == "join")
                {
                    if (!oTournamentMgr.IsUserInTournament(nTournamentID, oUser.UserID))
                    {
                        if(oTournamentMgr.JoinTournament(nTournamentID, oUser.UserID))
                            Response.Redirect(strRef + "&mc=10030081");
                        else
                            Response.Redirect(strRef + "&mc=10030080");
                    }
                    else
                        Response.Redirect("Notify.aspx?mc=10090020");
                }

                // unjoin tournament
                else if (strActionType == "unjoin")
                {
                    if (oTournamentMgr.IsUserInTournament(nTournamentID, oUser.UserID))
                    {
                        if(oTournamentMgr.UnjoinTournament(nTournamentID, oUser.UserID))
                            Response.Redirect(strRef + "&mc=10030071");
                        else
                            Response.Redirect(strRef + "&mc=10030070");
                    }
                    else
                        Response.Redirect("Notify.aspx?mc=10090020");
                }

                // delete tournament
                else if (strActionType == "delete")
                {
                    if (oUser.UserLevel == "admin")
                    {
                        if(oTournamentMgr.DeleteTournament(nTournamentID))
                            Response.Redirect(strRef + "&mc=10030101");
                        else
                            Response.Redirect(strRef + "&mc=10030100");
                    }
                    else
                        Response.Redirect("Notify.aspx?mc=10090020");
                }
            }
        }
    }
}