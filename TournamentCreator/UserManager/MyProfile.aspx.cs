using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TournamentCreator.Common;

namespace TournamentCreator.UserManager
{
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            CUser oUserMgr = new CUser();

            CUserInfo oUser = (CUserInfo)Session["currUser"];

            if (oUser != null)
            {

                string strFirstName = txtFirstName.Text.Trim();
                string strLastName = txtLastName.Text.Trim();
                string strAddress = txtAddress.Text.Trim();
                string strEmailAddress = txtEmailAddress.Text.Trim();
                string strPassword = txtPassword.Text.Trim();
                string strPhoneNumber = txtPhoneNumber.Text.Trim();

                // again check for 
                if (string.IsNullOrEmpty(strFirstName) || string.IsNullOrEmpty(strLastName) || string.IsNullOrEmpty(strAddress) || string.IsNullOrEmpty(strEmailAddress) || string.IsNullOrEmpty(strPassword) || string.IsNullOrEmpty(strPhoneNumber))
                {
                    Response.Redirect("MyProfile.aspx?mc=10090010");
                }
               

                // no error then create account
                else
                {
                    CCommon oCommon = new CCommon();
                    
                    oUser.FirstName = strFirstName;
                    oUser.LastName = strLastName;
                    oUser.Address = strAddress;
                    oUser.EmailAddress = strEmailAddress;
                    oUser.Password = oCommon.EncryptPassword(strPassword);
                    oUser.PhoneNumber = strPhoneNumber;



                    if (oUserMgr.UpdateUser(oUser))
                    {
                        Response.Redirect("../Notify.aspx?mc=10010071");
                    }
                    else
                        Response.Redirect("../Signup.aspx?mc=10010070");

                }
            }
            else
                lblError.Text = "Please login to continue";
        

        }
    }
}