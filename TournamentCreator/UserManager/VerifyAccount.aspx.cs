using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator.UserManager
{
    public partial class VerifyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string strVID = Request.QueryString["vid"] != null ? Request.QueryString["vid"] : "";
                if (string.IsNullOrEmpty(strVID))
                {
                    Response.Redirect("../Notify.aspx?mc=10090020");
                }
                else
                {
                    CUser oUserMgr = new CUser();
                    CCommon oCommon = new CCommon();
                    oUserMgr.VerifyEmailAddress(oCommon.DecryptPassword(strVID));
                    Response.Redirect("../Notify.aspx?mc=" + oUserMgr.GetMsgCode());
                }
                
            }
        }
    }
}