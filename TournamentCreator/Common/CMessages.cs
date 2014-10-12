using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentCreator.Common
{
    public class CMessages
    {
        public string GetHTMLMessageString(int nMsgCode)
        {
            string strHTMLMsg = "";
            if (nMsgCode % 10 == 0)
            {
                strHTMLMsg = "<span style=\"color:red; font-weight:bold;\">" + GetMessageString(nMsgCode) +"</span>"; 
            }
            else
            {
                strHTMLMsg = "<span style=\"color:green; font-weight:bold;\">" + GetMessageString(nMsgCode) + "</span>"; 
            }
            return strHTMLMsg;
        }

        public string GetMessageString(int nMsgCode)
        {
            string strMsg = "";
            try
            {
                switch (nMsgCode)
                {
                    #region User Manager
                    case 10010000:
                        strMsg = "An account with the provided email address already exists.";
                        break;
                    case 10010010:
                        strMsg = "An account with the provided email address already exists; But not verified. Please verify the account.";
                        break;
                    case 10010020:
                        strMsg = "Unable to verify email address.";
                        break;
                    case 10010021:
                        strMsg = "Email Address verified successfully";
                        break;
                    case 10010030:
                        strMsg = "Invalid Email Address or Password.";
                        break;
                    case 10010040:
                        strMsg = "Your Email Address has not yet been verified. Please verify it to continue.";
                        break;
                    case 10010051:
                        strMsg = "A verification email has sent to you. Please verify your email address to continue.";
                        break;
                    case 10010060:
                        strMsg = "Error occured while loggin in.";
                        break;
                    case 10010070:
                        strMsg = "Unable to update profile information.";
                        break;
                    case 10010071:
                        strMsg = "Profile Information Updated Successfully.";
                        break;
                    case 10010080:
                        strMsg = "Unable to save admin settings.";
                        break;
                    case 10010081:
                        strMsg = "Admin settings saved successfully.";
                        break;

                    #endregion

                    #region Email manager
                    case 10020010:
                        strMsg = "Unable to send recovery email. Please try again later";
                        break;
                    case 10020011:
                        strMsg = "A recovery email has sent to your email address.";
                        break;
                    #endregion

                    #region Tournaments
                    case 10030010:
                        strMsg = "The Stop Date can't be lesser than the Start Date.";
                        break;

                    case 10030020:
                        strMsg = "Unable to Create Tournament.";
                        break;
                    case 10030021:
                        strMsg = "Tournament Created Successfully.";
                        break;
                    case 10030030:
                        strMsg = "Tournament Name already Exists.";
                        break;
                    case 10030040:
                        strMsg = "Unable to save Tournament changes.";
                        break;
                    case 10030041:
                        strMsg = "Tournament changes saved successfully.";
                        break;
                    case 10030050:
                        strMsg = "Unable to Start Tournament.";
                        break;
                    case 10030051:
                        strMsg = "Tournament Started Successfully.";
                        break;

                    case 10030060:
                        strMsg = "Unable to Stop Tournament.";
                        break;
                    case 10030061:
                        strMsg = "Tournament Stopped Successfully.";
                        break;

                    case 10030070:
                        strMsg = "Unable to Unjoin Tournament.";
                        break;
                    case 10030071:
                        strMsg = "You have Unjoined Tournament Successfully.";
                        break;

                    case 10030080:
                        strMsg = "Unable to Join Tournament.";
                        break;
                    case 10030081:
                        strMsg = "You have Joined Tournament Successfully.";
                        break;

                    case 10030090:
                        strMsg = "Unable to Join Tournament.";
                        break;
                    case 10030091:
                        strMsg = "You have Joined Tournament Successfully.";
                        break;

                    case 10030100:
                        strMsg = "Unable to Delete Tournament.";
                        break;
                    case 10030101:
                        strMsg = "Tournament Deleted Successfully.";
                        break;

                    case 10030110:
                        strMsg = "The Maximum number of Tournament Players must be atleast 2.";
                        break;
                    case 10030120:
                        strMsg = "Unable to Start Tournament, atleast 2 Players have to join the Tournament before it starts.";
                        break;

                    case 10030131:
                        strMsg = "Game Result Updated Successfully.";
                        break;
                    case 10030141:
                        strMsg = "Game Result Stored Successfully.";
                        break;
                    #endregion

                    #region Common Form

                    case 10090010:
                        strMsg = "Some Required fileds are missing.";
                     break;
                     case 10090020:
                        strMsg = "Access to the page is denied.";
                        break;
                    case 10090030:
                        strMsg = "Database Connection Error.";
                        break;

                    case 10090040:
                        strMsg = "Some Exception Occured.";
                        break;

                    case 10090050:
                        strMsg = "The Provided Email Address does not belong to any account. Please Signup to create account.";
                        break;

                    case 10090060:
                        strMsg = "Page Not Found.";
                        break;

                    
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            return strMsg;
        }
    }
}