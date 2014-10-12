using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;



namespace TournamentCreator.Common
{
    public class CUser
    {
        int nMsgCode;

        public CUser()
        {
            
        }
        public bool AddUser(CUserInfo oUser)
        {
            bool bUserAdded = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO users(FirstName, LastName, UserLevel, Address, PhoneNo, EmailAddress, Password) VALUES ('" + oUser.FirstName + "', '" + oUser.LastName +"', 'user', '" + oUser.Address +"', '" + oUser.PhoneNumber +"', '" + oUser.EmailAddress +"', '" + oUser.Password +"')";
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                int nRet = cmd.ExecuteNonQuery();
                oUser.UserID = (int)cmd.LastInsertedId;
                if (nRet > 0)
                {
                    CMail oMail = new CMail();
                    oMail.SendVerificationMail(oUser);
                    bUserAdded = true;
                    nMsgCode = 10010051;
                }
            }
            catch (Exception ex)
            {
                nMsgCode = 10090040;
                System.Console.Write(ex.StackTrace);
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return bUserAdded;
        }

        public string GetUserLevel(int nUserID)
        {
            string strUserLevel = "";
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT UserLevel FROM users WHERE UserID=" + nUserID;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    strUserLevel = oReader["UserLevel"].ToString();
                }
            }
            catch (Exception ex)
            {
                
                System.Console.Write(ex.StackTrace);
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return strUserLevel;
        }

        public int GetMsgCode()
        {
            return nMsgCode;
        }

        public bool IsEmailExists(string strEmail)
        {
            bool bEmailExists = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT UserID FROM users WHERE EmailAddress = '" + strEmail + "'";
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    bEmailExists = true;
                    //int nVerified = int.Parse(oReader["IsVerified"].ToString());
                    //if (nVerified == 0)
                        nMsgCode = 10010000;
                    //else
                    //    nMsgCode = 10010000;
                }
            }
            catch (Exception ex)
            {
                nMsgCode = 10090040;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bEmailExists;
        }

        public bool VerifyEmailAddress(string strEmail)
        {
            bool isVerified = false ;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                // first check if the email address exists
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT UserID FROM users WHERE EmailAddress = '" + strEmail + "'";
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    // that means that user id exists
                    cmd.CommandText = "UPDATE users SET IsVerified=1 WHERE EmailAddress = '" + strEmail + "'";
                    int nRet = cmd.ExecuteNonQuery();
                    if(nRet > 0)
                    {
                        isVerified = true;
                        nMsgCode = 10010021;
                    }
                    else
                        nMsgCode = 10010020;
                }
                else
                    nMsgCode = 10010020;
            }
            catch (Exception ex)
            {
                nMsgCode = 10090040;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return isVerified;
        }

        public CUserInfo GetUserDetailsByUserID(int nUserID)
        {
            CUserInfo oUser = new CUserInfo();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM users WHERE UserID = " + nUserID;
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {

                    
                    if (int.TryParse(oReader["UserID"].ToString(), out nUserID))
                    {
                        if (nUserID > 0)
                        {
                            oUser.UserID = nUserID;
                            oUser.FirstName = oReader["FirstName"].ToString();
                            oUser.LastName = oReader["LastName"].ToString();
                            oUser.UserLevel = oReader["UserLevel"].ToString();
                            oUser.Address = oReader["Address"].ToString();
                            oUser.EmailAddress = oReader["EmailAddress"].ToString();
                            oUser.Password = oReader["Password"].ToString();
                            oUser.PhoneNumber = oReader["PhoneNo"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oUser = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return oUser;
        }

        public CUserInfo GetUserDetailsByEmail(string strEmail)
        {
            CUserInfo oUser = new CUserInfo();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM users WHERE EmailAddress = '" + strEmail + "'";
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                      
                    int nUserID = 0;
                    if (int.TryParse(oReader["UserID"].ToString(), out nUserID))
                    {
                        if (nUserID > 0)
                        {
                            oUser.UserID = nUserID;
                            oUser.FirstName = oReader["FirstName"].ToString();
                            oUser.LastName = oReader["LastName"].ToString();
                            oUser.UserLevel = oReader["UserLevel"].ToString();
                            oUser.Address = oReader["Address"].ToString();
                            oUser.EmailAddress = oReader["EmailAddress"].ToString();
                            oUser.Password = oReader["Password"].ToString();
                            oUser.PhoneNumber = oReader["PhoneNo"].ToString();
                      
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oUser = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return oUser;
        }

        public bool LoginUser(string strEmail, string strPassword)
        {
            bool isLoggedIn = false;
            MySqlConnection oConn = null;
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    CDatabase oDB = new CDatabase();
                    oConn = new MySqlConnection(oDB.GetConnectionString());
                    oConn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT * FROM users WHERE EmailAddress = '" + strEmail + "' and Password='" + strPassword + "' and IsVerified=1";
                    cmd.Connection = oConn;

                    MySqlDataReader oReader = cmd.ExecuteReader();
                    if (oReader.Read())
                    {
                        CUserInfo oUser = new CUserInfo();
                        int nUserID = 0;
                        if (int.TryParse(oReader["UserID"].ToString(), out nUserID))
                        {
                            if (nUserID > 0)
                            {
                                oUser.UserID = nUserID;
                                oUser.FirstName = oReader["FirstName"].ToString();
                                oUser.LastName = oReader["LastName"].ToString();
                                oUser.UserLevel = oReader["UserLevel"].ToString();
                                oUser.Address = oReader["Address"].ToString();
                                oUser.EmailAddress = oReader["EmailAddress"].ToString();
                                oUser.PhoneNumber = oReader["PhoneNo"].ToString();

                                HttpContext.Current.Session["currUser"] = oUser;
                                isLoggedIn = true;
                            }
                            else
                                nMsgCode = 10010060;
                        }
                        else
                            nMsgCode = 10010060;

                        
                    }
                }
            }
            catch (Exception ex)
            {
                nMsgCode = 10090040;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return isLoggedIn;
        }

        public bool IsValidUser(string strEmail, string strPassword)
        {
            bool bIsLoggedIn = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT UserID FROM users WHERE EmailAddress = '" + strEmail + "' and Password='" + strPassword + "' and IsVerified=1";
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    bIsLoggedIn = true;
                }
                else
                {
                    // checking if there is unverified account 
                    cmd.CommandText = "SELECT UserID FROM users WHERE EmailAddress = '" + strEmail + "' and Password='" + strPassword + "'";

                    oReader = cmd.ExecuteReader();
                    if (oReader.Read())
                    {
                        oReader.Close();
                        nMsgCode = 10010040;
                    }
                    else
                        nMsgCode = 10010030;

                }
            }
            catch (Exception ex)
            {
                nMsgCode = 10090040;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return bIsLoggedIn;
        }

        public ArrayList GetTournamentUsersList(int nTournamentID)
        {
            ArrayList oUsersList = new ArrayList();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM users WHERE EmailAddress = '" + strEmail + "'";
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                while (oReader.Read())
                {
                    CUserInfo oUser = new CUserInfo();
                    int nUserID = 0;
                    if (int.TryParse(oReader["UserID"].ToString(), out nUserID))
                    {
                        if (nUserID > 0)
                        {
                            oUser.UserID = nUserID;
                            oUser.FirstName = oReader["FirstName"].ToString();
                            oUser.LastName = oReader["LastName"].ToString();
                            oUser.UserLevel = oReader["UserLevel"].ToString();
                            oUser.Address = oReader["Address"].ToString();
                            oUser.EmailAddress = oReader["EmailAddress"].ToString();
                            oUser.Password = oReader["Password"].ToString();
                            oUser.PhoneNumber = oReader["PhoneNo"].ToString();

                            oUsersList.Add(oUser);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oUsersList = null;
            }
            finally
            {
                if(oConn != null)
                    oConn.Close();
            }
            return oUsersList;
        }

        public bool VerifyUResultRights(int nResultID, int nUserID)
        {
            bool bUserHasRights = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT GameResultUID from gameresultsunverified WHERE GameResultUID=" + nResultID + " and SubmitterID=" + nUserID;
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    bUserHasRights = true;
                    
                }
            }
            catch (Exception ex)
            {
                nMsgCode = 10090040;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bUserHasRights;
        }

        public bool UpdateUser(CUserInfo oUser)
        {
            bool bUserUpdated = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE users SET FirstName=" + oUser.FirstName + ", LastName=" + oUser.LastName + ", Address=" + oUser.Address + ", Password=" + oUser.Password + " WHERE UserID=" + oUser.UserID;
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                LoginUser(oUser.EmailAddress, oUser.Password);

                bUserUpdated = true;
               
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bUserUpdated;
        }

        public bool IsUserAllowedSubmitResult()
        {
            bool bUserAllowed = true;

            CGlobalAttr oGlobalAttr = new CGlobalAttr();
            string strAllowed = oGlobalAttr.GetGlobalAttr("user-allowed-submit-result");
            if (!string.IsNullOrEmpty(strAllowed))
            {
                if (strAllowed.ToLower() == "true")
                    bUserAllowed = true;
                else
                    bUserAllowed = false;
            }

            return bUserAllowed;
        }
    }

    public class CUserInfo
    {
        // class attributes
        private int nUserID;
        private string strFirstName;
        private string strLastName;
        private string strUserLevel;
        private string strAddress;
        private string strEmailAddress;
        private string strPhoneNumber;
        private string strPassword;

        public CUserInfo()
        {
        }

        public string FirstName
        {
            get { return strFirstName; }
            set { strFirstName = value; }
        }

        public string LastName
        {
            get { return strLastName; }
            set { strLastName = value; }
        }

        public string UserLevel
        {
            get { return strUserLevel; }
            set { strUserLevel = value; }
        }

        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }

        public string EmailAddress
        {
            get { return strEmailAddress; }
            set { strEmailAddress = value; }
        }

        public string PhoneNumber
        {
            get { return strPhoneNumber; }
            set { strPhoneNumber = value; }
        }

        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        public int UserID
        {
            get { return nUserID; }
            set { nUserID = value; }
        }
    }
}