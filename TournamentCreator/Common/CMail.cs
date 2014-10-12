using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Collections;


namespace TournamentCreator.Common
{
    public class CMail
    {
        
        public bool SendResultMismatchMail(CGameResultInfo oGameInfo1, CGameResultInfo oGameInfo2)
        {
            bool bMailSent = false;
            try
            {
                CCommon oCommon = new CCommon();
                CUser oUserMgr = new CUser();

                CUserInfo oUser1 = oUserMgr.GetUserDetailsByUserID(oGameInfo1.FirstUserID);
                CUserInfo oUser2 = oUserMgr.GetUserDetailsByUserID(oGameInfo1.SecondUserID);

                string strSiteName = oCommon.getWebSiteName();
                string strSiteAddress = oCommon.GetWebSiteAddress();
                
                string strSMTPClient = GetSMTPClient();
                int nSMTPPort = GetSMTPPort();

                string strEmail = GetSenderEmail();
                string strPassword = GetSenderPassword();

                string strMailText = "Dear Members, ";

                strMailText += "<br/><br/>The result submitted by you for the game ,";
                strMailText += oUser1.FirstName + " " + oUser1.LastName + " vs " + oUser2.FirstName + " " + oUser2.LastName;
                strMailText += "<br/> does not match by the result submitted by the other player. Please correct it as soon as possible.";
                
                

                strMailText += "<br/><br/>BestRegards,";
                strMailText += "<br/>" + strSiteName + " Team";


                // Instantiate a new instance of MailMessage
                MailMessage mMailMessage = new MailMessage();

                // Set the sender address of the mail message
                mMailMessage.From = new MailAddress("no-reply@vindinfo.se");
                // Set the recepient address of the mail message
                mMailMessage.To.Add(new MailAddress(oUser1.EmailAddress));
                mMailMessage.To.Add(new MailAddress(oUser2.EmailAddress));
                // Check if the bcc value is null or an empty string




                mMailMessage.Subject = "Email Verification for ...:::" + strSiteName;
                // Set the body of the mail message
                mMailMessage.Body = strMailText;
                // Set the format of the mail message body as HTML
                mMailMessage.IsBodyHtml = true;
                // Set the priority of the mail message to normal
                mMailMessage.Priority = MailPriority.Normal;

                // Instantiate a new instance of SmtpClient
                //SmtpClient mSmtpClient = new SmtpClient();
                //// Send the mail message
                //mSmtpClient.Send(mMailMessage);

                SmtpClient smtp = new SmtpClient(strSMTPClient, nSMTPPort);
                smtp.Credentials = new System.Net.NetworkCredential(strEmail, strPassword);
                smtp.Send(mMailMessage);
                bMailSent = true;
            }
            catch (Exception ex)
            {
            }
            return bMailSent;
        }

        public bool SendVerificationMail(CUserInfo oUser)
        {
            bool bMailSent = false;
            try
            {
                CCommon oCommon = new CCommon();
                string strUserEmail = oUser.EmailAddress;
                string strSiteName = oCommon.getWebSiteName();
                string strSiteAddress = oCommon.GetWebSiteAddress();
                
                string strSMTPClient = GetSMTPClient();
                int nSMTPPort = GetSMTPPort();

                string strEmail = GetSenderEmail();
                string strPassword = GetSenderPassword();

                string strMailText = "Hi " + oUser.FirstName + " " + oUser.LastName + ",";
                strMailText += "<br/><br/>Thank you for Signing up to " + strSiteAddress;
                strMailText += "<br/><br/>Please Follow the link below to verify your email address : ";
                strMailText += "<br/>http://" + strSiteAddress  + "/UserManager/VerifyAccount.aspx?vid=" + oCommon.EncryptPassword(strUserEmail);
                strMailText += "<br/><br/>BestRegards,";
                strMailText += "<br/>" + strSiteName + " Team";


                // Instantiate a new instance of MailMessage
                MailMessage mMailMessage = new MailMessage();

                // Set the sender address of the mail message
                mMailMessage.From = new MailAddress("no-reply@vindinfo.se");
                // Set the recepient address of the mail message
                mMailMessage.To.Add(new MailAddress(strUserEmail));
                // Check if the bcc value is null or an empty string




                mMailMessage.Subject = "Email Verification for ...:::" + strSiteName;
                // Set the body of the mail message
                mMailMessage.Body = strMailText;
                // Set the format of the mail message body as HTML
                mMailMessage.IsBodyHtml = true;
                // Set the priority of the mail message to normal
                mMailMessage.Priority = MailPriority.Normal;

                // Instantiate a new instance of SmtpClient
                //SmtpClient mSmtpClient = new SmtpClient();
                //// Send the mail message
                //mSmtpClient.Send(mMailMessage);

                SmtpClient smtp = new SmtpClient(strSMTPClient, nSMTPPort);
                smtp.Credentials = new System.Net.NetworkCredential(strEmail, strPassword);
                smtp.Send(mMailMessage);
                bMailSent = true;
            }
            catch (Exception ex)
            {
            }
            return bMailSent;
        }

        public bool SendPasswordRecoveryMail(string strEmailAddress)
        {
            bool bMailSent = false;
            try
            {
                CCommon oCommon = new CCommon();
                CUser oUserMgr = new CUser();
                string strUserEmail = strEmailAddress;
                string strSiteName = oCommon.getWebSiteName();
                string strSiteAddress = oCommon.GetWebSiteAddress();

                CUserInfo oUser = oUserMgr.GetUserDetailsByEmail(strEmailAddress);

                if (oUser != null)
                {
                    string strSMTPClient = GetSMTPClient();
                    int nSMTPPort = GetSMTPPort();

                    string strEmail = GetSenderEmail();
                    string strPassword = GetSenderPassword();

                    string strMailText = "Hi " + oUser.FirstName + " " + oUser.LastName + ",";

                    strMailText += "<br/><br/>Following is your password at " + strSiteAddress;
                    strMailText += "<br/>" + oCommon.DecryptPassword(oUser.Password);
                    strMailText += "<br/><br/>BestRegards,";
                    strMailText += "<br/>" + strSiteName + " Team";


                    // Instantiate a new instance of MailMessage
                    MailMessage mMailMessage = new MailMessage();

                    // Set the sender address of the mail message
                    mMailMessage.From = new MailAddress("no-reply@vindinfo.se");
                    // Set the recepient address of the mail message
                    mMailMessage.To.Add(new MailAddress(strUserEmail));
                    // Check if the bcc value is null or an empty string

                    mMailMessage.Subject = "Password recovery for account on ...:::" + strSiteName;
                    // Set the body of the mail message
                    mMailMessage.Body = strMailText;
                    // Set the format of the mail message body as HTML
                    mMailMessage.IsBodyHtml = true;
                    // Set the priority of the mail message to normal
                    mMailMessage.Priority = MailPriority.Normal;

                    SmtpClient smtp = new SmtpClient(strSMTPClient, nSMTPPort);
                    smtp.Credentials = new System.Net.NetworkCredential(strEmail, strPassword);
                    smtp.Send(mMailMessage);

                    bMailSent = true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return bMailSent;
        }

        public bool SendCupAloneMail(CUserInfo oUser, CTournamentInfo oTournament)
        {
            bool bMailSent = false;
            try
            {
                CCommon oCommon = new CCommon();
                CUser oUserMgr = new CUser();
                string strUserEmail = oUser.EmailAddress;
                string strSiteName = oCommon.getWebSiteName();
                string strSiteAddress = oCommon.GetWebSiteAddress();

                if (oUser != null)
                {
                    string strSMTPClient = GetSMTPClient();
                    int nSMTPPort = GetSMTPPort();

                    string strEmail = GetSenderEmail();
                    string strPassword = GetSenderPassword();

                    string strMailText = "Hi " + oUser.FirstName + " " + oUser.LastName + ",";

                    strMailText += "<br/><br/>Your have send to the next round because in the tournament " + "http://" + oCommon.GetWebSiteAddress() + "/" + oTournament.TournamentName;
                    strMailText += "<br/>because of odd number of players.";
                    strMailText += "<br/><br/>BestRegards,";
                    strMailText += "<br/>" + strSiteName + " Team";


                    // Instantiate a new instance of MailMessage
                    MailMessage mMailMessage = new MailMessage();

                    // Set the sender address of the mail message
                    mMailMessage.From = new MailAddress("no-reply@vindinfo.se");
                    // Set the recepient address of the mail message
                    mMailMessage.To.Add(new MailAddress(strUserEmail));
                    // Check if the bcc value is null or an empty string

                    mMailMessage.Subject = "Password recovery for account on ...:::" + strSiteName;
                    // Set the body of the mail message
                    mMailMessage.Body = strMailText;
                    // Set the format of the mail message body as HTML
                    mMailMessage.IsBodyHtml = true;
                    // Set the priority of the mail message to normal
                    mMailMessage.Priority = MailPriority.Normal;

                    SmtpClient smtp = new SmtpClient(strSMTPClient, nSMTPPort);
                    smtp.Credentials = new System.Net.NetworkCredential(strEmail, strPassword);
                    smtp.Send(mMailMessage);

                    bMailSent = true;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            return bMailSent;
        }

        public bool SendEAETournamentMail(CTournamentInfo oTournament, ArrayList alUsers)
        {
            bool bMailSent = false;
            try
            {
                CCommon oCommon = new CCommon();
                CUser oUserMgr = new CUser();
                string strSiteName = oCommon.getWebSiteName();
                string strSiteAddress = oCommon.GetWebSiteAddress();

                if (alUsers.Count > 0)
                {
                    string strSMTPClient = GetSMTPClient();
                    int nSMTPPort = GetSMTPPort();

                    string strEmail = GetSenderEmail();
                    string strPassword = GetSenderPassword();

                    string strMailText = "Hi Member,";

                    strMailText += "<br/><br/>The Tournament Has Started " + "http://" + oCommon.GetWebSiteAddress() + "/" + oTournament.TournamentName;
                    strMailText += "<br/><br/>The Players in this Tournament are : <br/>";
                    for (int i = 0; i < alUsers.Count; i++)
                    {
                        CUserInfo oUser = (CUserInfo)alUsers[i];
                        strMailText += oUser.FirstName + " " + oUser.LastName + "<br/>"
                    }

                    strMailText += "<br/><br/>Play the games against every player and submit the result<br/><br/>";
                    

                        strMailText += "<br/><br/>BestRegards,";
                    strMailText += "<br/>" + strSiteName + " Team";


                    // Instantiate a new instance of MailMessage
                    MailMessage mMailMessage = new MailMessage();

                    // Set the sender address of the mail message
                    mMailMessage.From = new MailAddress("no-reply@vindinfo.se");
                    // Set the recepient address of the mail message

                    for (int i = 0; i < alUsers.Count; i++)
                    {
                        CUserInfo oUser = (CUserInfo)alUsers[i];
                        mMailMessage.To.Add(new MailAddress(oUser.EmailAddress));
                    }
                    // Check if the bcc value is null or an empty string

                    mMailMessage.Subject = "Cup Tournament Match Begin ...:::" + strSiteName;
                    // Set the body of the mail message
                    mMailMessage.Body = strMailText;
                    // Set the format of the mail message body as HTML
                    mMailMessage.IsBodyHtml = true;
                    // Set the priority of the mail message to normal
                    mMailMessage.Priority = MailPriority.Normal;

                    SmtpClient smtp = new SmtpClient(strSMTPClient, nSMTPPort);
                    smtp.Credentials = new System.Net.NetworkCredential(strEmail, strPassword);
                    smtp.Send(mMailMessage);

                    bMailSent = true;
                }
            }
            catch (Exception ex)
            {
            }
            return bMailSent;
        }

        public bool SentTournamentStopMail(CTournamentInfo oTournament, ArrayList alUsers, CUserInfo oWinner)
        {
            bool bMailSent = false;
            try
            {
                CCommon oCommon = new CCommon();
                CUser oUserMgr = new CUser();
                string strSiteName = oCommon.getWebSiteName();
                string strSiteAddress = oCommon.GetWebSiteAddress();

                if (alUsers.Count > 0)
                {
                    string strSMTPClient = GetSMTPClient();
                    int nSMTPPort = GetSMTPPort();

                    string strEmail = GetSenderEmail();
                    string strPassword = GetSenderPassword();

                    string strMailText = "Hi Member,";

                    strMailText += "<br/><br/>The Tournament \""+ oTournament.TournamentName +"\" Has Ended";
                    strMailText += "<br/><br/>The Winner of this Tournament Is : " + oWinner.FirstName + " " + oWinner.LastName;
                    strMailText += "<br/><br/> You can watch the scoreboard at http:" + oCommon.GetWebSiteAddress() + "/" + oTournament.TournamentName;
                    
                    strMailText += "<br/><br/>Play the games against every player and submit the result<br/><br/>";
                    
                    strMailText += "<br/><br/>BestRegards,";
                    strMailText += "<br/>" + strSiteName + " Team";


                    // Instantiate a new instance of MailMessage
                    MailMessage mMailMessage = new MailMessage();

                    // Set the sender address of the mail message
                    mMailMessage.From = new MailAddress("no-reply@vindinfo.se");
                    // Set the recepient address of the mail message

                    for (int i = 0; i < alUsers.Count; i++)
                    {
                        CUserInfo oUser = (CUserInfo)alUsers[i];
                        mMailMessage.To.Add(new MailAddress(oUser.EmailAddress));
                    }
                    // Check if the bcc value is null or an empty string

                    mMailMessage.Subject = "Cup Tournament Match Begin ...:::" + strSiteName;
                    // Set the body of the mail message
                    mMailMessage.Body = strMailText;
                    // Set the format of the mail message body as HTML
                    mMailMessage.IsBodyHtml = true;
                    // Set the priority of the mail message to normal
                    mMailMessage.Priority = MailPriority.Normal;

                    SmtpClient smtp = new SmtpClient(strSMTPClient, nSMTPPort);
                    smtp.Credentials = new System.Net.NetworkCredential(strEmail, strPassword);
                    smtp.Send(mMailMessage);

                    bMailSent = true;
                }
            }
            catch (Exception ex)
            {
            }
            return bMailSent;
        }

        public bool SendCupMatchMail(CUserInfo oUser1, CUserInfo oUser2, CTournamentInfo oTournament)
        {
            bool bMailSent = false;
            try
            {
                CCommon oCommon = new CCommon();
                CUser oUserMgr = new CUser();
                string strSiteName = oCommon.getWebSiteName();
                string strSiteAddress = oCommon.GetWebSiteAddress();

                if (oUser1 != null && oUser2 != null)
                {
                    string strSMTPClient = GetSMTPClient();
                    int nSMTPPort = GetSMTPPort();

                    string strEmail = GetSenderEmail();
                    string strPassword = GetSenderPassword();

                    string strMailText = "Hi " + oUser1.FirstName + " " + oUser1.LastName + " and " + oUser1.FirstName + " " + oUser2.LastName + ",";

                    strMailText += "<br/><br/>Your Match for Cup tournament " + "http://" + oCommon.GetWebSiteAddress() + "/" + oTournament.TournamentName;
                    strMailText += " is started. Play the match and submit the result.";
                    strMailText += "<br/><br/>BestRegards,";
                    strMailText += "<br/>" + strSiteName + " Team";


                    // Instantiate a new instance of MailMessage
                    MailMessage mMailMessage = new MailMessage();

                    // Set the sender address of the mail message
                    mMailMessage.From = new MailAddress("no-reply@vindinfo.se");
                    // Set the recepient address of the mail message
                    mMailMessage.To.Add(new MailAddress(oUser1.EmailAddress));
                    mMailMessage.To.Add(new MailAddress(oUser2.EmailAddress));
                    // Check if the bcc value is null or an empty string

                    mMailMessage.Subject = "Cup Tournament Match Begin ...:::" + strSiteName;
                    // Set the body of the mail message
                    mMailMessage.Body = strMailText;
                    // Set the format of the mail message body as HTML
                    mMailMessage.IsBodyHtml = true;
                    // Set the priority of the mail message to normal
                    mMailMessage.Priority = MailPriority.Normal;

                    SmtpClient smtp = new SmtpClient(strSMTPClient, nSMTPPort);
                    smtp.Credentials = new System.Net.NetworkCredential(strEmail, strPassword);
                    smtp.Send(mMailMessage);

                    bMailSent = true;
                }
            }
            catch (Exception ex)
            {
            }
            return bMailSent;
        }

        private string GetSMTPClient()
        {
            return "smtp.vindinfo.se";
        }

        private int GetSMTPPort()
        {
            return 587;
        }

        private string GetSenderEmail()
        {
            return "no-reply@vindinfo.se";
        }

        private string GetSenderPassword()
        {
            return "TournamentCreator";
        }
    }
}