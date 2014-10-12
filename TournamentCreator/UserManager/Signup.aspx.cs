using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator.UserManager
{
    public partial class Signup : System.Web.UI.Page
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
                if (Session["currUser"] != null)
                {
                    Response.Redirect("../Notify.aspx?mc=10090020");
                }
                // if the user is not logged in
                {
                    contentsInit = initContentsGuest();
                }
            
            
            return contentsInit;
        }

        private bool initContentsUser()
        {
            bool contentsInit = false;
            try
            {
                contentsInit = true;
            }
            catch (Exception ex)
            {
            }
            return contentsInit;
        }

        private bool initContentsGuest()
        {
            bool contentsInit = false;
            try
            {
                // initializing the top labels
                lblTop.Text = "";
                contentsInit = true;

                // initializing the neviataion list
                string strNavList = "<li><a href=\"../Default.aspx\">Home</a></li>";
                strNavList += "<li><a href=\"../TournamentManager/TournamentsList.aspx\">Tournaments</a></li>";
                navlist.InnerHtml = strNavList;

                // initializing the paths
                lblNavigation.Text = "&nbsp;&nbsp;&nbsp;";
                lblNavigation.Text += "<a href=\"../Default.aspx\">Home</a> > <a href=\"#\">Signup</a> >";

                lblHeading.Text = "<h1>Tournament Creator</h1><h2>Please fill the form to signup</h2>";


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

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            CUser oUserMgr = new CUser();
            string strFirstName = txtFirstName.Text.Trim();
            string strLastName = txtLastName.Text.Trim();
            string strAddress = txtAddress.Text.Trim();
            string strEmailAddress = txtEmailAddress.Text.Trim();
            string strPassword = txtPassword.Text.Trim();
            string strPhoneNumber = txtPhoneNumber.Text.Trim();
            
            // again check for 
            if (string.IsNullOrEmpty(strFirstName) || string.IsNullOrEmpty(strLastName) || string.IsNullOrEmpty(strAddress) || string.IsNullOrEmpty(strEmailAddress) || string.IsNullOrEmpty(strPassword) || string.IsNullOrEmpty(strPhoneNumber))
            {
                Response.Redirect("Signup.aspx?mc=10090010");
            }
            if (oUserMgr.IsEmailExists(strEmailAddress))
            {
                Response.Redirect("Signup.aspx?mc=" + oUserMgr.GetMsgCode());
            }

            // no error then create account
            else
            {
                CCommon oCommon = new CCommon();
                CUserInfo oUser = new CUserInfo();

                oUser.FirstName = strFirstName;
                oUser.LastName = strLastName;
                oUser.Address = strAddress;
                oUser.EmailAddress = strEmailAddress;
                oUser.Password = oCommon.EncryptPassword(strPassword);
                oUser.PhoneNumber = strPhoneNumber;



                if (oUserMgr.AddUser(oUser))
                {
                    Response.Redirect("../Notify.aspx?mc=" + oUserMgr.GetMsgCode());
                }
                else
                    Response.Redirect("../Signup.aspx?mc=" + oUserMgr.GetMsgCode());

            }
        }

      
    }
}