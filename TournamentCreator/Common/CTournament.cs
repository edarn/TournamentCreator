using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace TournamentCreator.Common
{
    public class CTournament
    {
        public CTournament()
        {
            
        }

        public ArrayList GetStartedTournamentsList()
        {
            ArrayList alTournaments = null;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT * FROM tournaments WHERE IsStarted=1 and IsStopped=0";

                
                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oReader = cmd.ExecuteReader();
                while (oReader.Read())
                {
                    CTournamentInfo oTournament = new CTournamentInfo();
                    string strTournamentID = oReader["TournamentID"].ToString();
                    int nTournamentID = 0;
                    if (!string.IsNullOrEmpty(strTournamentID))
                    {
                        if (int.TryParse(strTournamentID, out nTournamentID))
                        {
                            if (nTournamentID > 0)
                            {
                                oTournament.TournamentID = nTournamentID;
                                oTournament.TournamentName = oReader["TournamentName"].ToString();
                                if (!string.IsNullOrEmpty(oTournament.TournamentName))
                                {
                                    alTournaments.Add(oTournament);
                                }

                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                alTournaments = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return alTournaments;
        }


        public bool IsTournamentNameExists(string strName)
        {
            bool bTournamentNameExists = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT TournamentID FROM tournaments WHERE TournamentName = '" + strName + "'";
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    bTournamentNameExists = true;
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bTournamentNameExists;
        }

        public bool CreateTournament(CTournamentInfo oTournament)
        {
            bool bTournamentCreated = false;
            MySqlConnection oConn = null;
            CCommon oCommon = new CCommon();
            try
            {
                int nStartManually = oTournament.StartManually == true ? 1 : 0;
                int nStopManually = oTournament.StopManually == true ? 1 : 0;

                string strStartDate = oCommon.DateToString(oTournament.StartDate);
                string strStopDate = oCommon.DateToString(oTournament.StopDate);

                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO tournaments(TournamentName, TournamentDesc, TournamentType, StartManually, StartDate, StopManually, StopDate, NumMaxPlayers, MustPlayedIn, IsStarted, IsStopped, CreatorID) VALUES ('" + oTournament.TournamentName + "', '" + oTournament.TournamentDesc + "', '" + oTournament.TournamentType + "' , " + nStartManually + ", '" + strStartDate + "', " + nStopManually + ", '" + strStopDate + "', " + oTournament.MaxNumPlayers + ", " + 0 + ", " + 0 + ", " + 0 + ", " + oTournament.CreatorID + ")";
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                int nRet = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return bTournamentCreated;
        }

        public DataSet GetTournamentsList(int nCurrUserID, string strSortBy, string strSortOrder, string strOpt)
        {
            DataSet dsTournaments = null;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT * FROM tournaments ";
                if (strOpt == "started")
                    strQry += " WHERE IsStarted=1";
                else if (strOpt == "notstarted")
                    strQry += " WHERE IsStarted!=1";
                else if (strOpt == "my")
                {
                    strQry += " INNER JOIN tournamentusers ON tournamentusers.TournamentID = tournaments.TournamentID";
                    strQry += " WHERE tournamentusers.UserID=" + nCurrUserID;
                }
                else if (strOpt == "myCreated")
                {
                    
                    strQry += " WHERE tournaments.CreatorID=" + nCurrUserID;
                }
                strQry += " ORDER BY " + strSortBy + " " + strSortOrder;
                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader nRet = cmd.ExecuteReader();
                dsTournaments = oCommon.MySqlDataReaderToDataSet(nRet);
            }
            catch (Exception ex)
            {
                dsTournaments = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return dsTournaments;
        }

        public CTournamentInfo GetTournamentDetailsByName(string strTournamentName)
        {
            CTournamentInfo oTournament = new CTournamentInfo();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT * FROM tournaments WHERE TournamentName = " + strTournamentName;
                
                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oDataReader = cmd.ExecuteReader();

                if (oDataReader.Read())
                {
                    int nTournamentID, nStartManually, nStopManually, nMaxPlayers, nIsStarted, nIsStopped;
                    nTournamentID = nStartManually = nStopManually = nMaxPlayers = nIsStarted = nIsStopped = 0;

                    if (int.TryParse(oDataReader["TournamentID"].ToString(), out nTournamentID))
                    {
                        oTournament.TournamentID = nTournamentID;
                        oTournament.TournamentName = oDataReader["TournamentName"].ToString();
                        oTournament.TournamentDesc = oDataReader["TournamentDesc"].ToString();
                        oTournament.TournamentType = oDataReader["TournamentType"].ToString();
                        int.TryParse(oDataReader["StartManully"].ToString(), out nStartManually);
                        int.TryParse(oDataReader["StopManully"].ToString(), out nStopManually);
                        int.TryParse(oDataReader["NumMaxPlayers"].ToString(), out nMaxPlayers);
                        int.TryParse(oDataReader["IsStarted"].ToString(), out nStopManually);
                        int.TryParse(oDataReader["IsStopped"].ToString(), out nMaxPlayers);

                        oTournament.StartManually = nStartManually == 1 ? true : false;
                        oTournament.StopManually = nStopManually == 1 ? true : false;
                        oTournament.IsStarted = nIsStarted == 1 ? true : false;
                        oTournament.IsStopped = nIsStopped == 1 ? true : false;
                        oTournament.MaxNumPlayers = nMaxPlayers;

                        oTournament.NumPlayers = GetNumberOfPlayers(nTournamentID);
                    }
                    else
                    {
                        oTournament = null;
                    }
                    oDataReader.Close();
                }
                else
                {
                    oTournament = null;
                }
            }
            catch (Exception ex)
            {
                oTournament = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return oTournament;
        }

        public CTournamentInfo GetTournamentDetailsByID(int nTournamentID)
        {
            CTournamentInfo oTournament = new CTournamentInfo();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT * FROM tournaments WHERE TournamentID = " + nTournamentID;

                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oDataReader = cmd.ExecuteReader();

                if (oDataReader.Read())
                {
                    int  nStartManually, nStopManually, nMaxPlayers, nIsStarted, nIsStopped;
                    nTournamentID = nStartManually = nStopManually = nMaxPlayers = nIsStarted = nIsStopped = 0;

                    
                    oTournament.TournamentID = nTournamentID;
                    oTournament.TournamentName = oDataReader["TournamentName"].ToString();
                    oTournament.TournamentDesc = oDataReader["TournamentDesc"].ToString();
                    oTournament.TournamentType = oDataReader["TournamentType"].ToString();
                    int.TryParse(oDataReader["StartManully"].ToString(), out nStartManually);
                    int.TryParse(oDataReader["StopManully"].ToString(), out nStopManually);
                    int.TryParse(oDataReader["NumMaxPlayers"].ToString(), out nMaxPlayers);
                    int.TryParse(oDataReader["IsStarted"].ToString(), out nStopManually);
                    int.TryParse(oDataReader["IsStopped"].ToString(), out nMaxPlayers);

                    oTournament.StartManually = nStartManually == 1 ? true : false;
                    oTournament.StopManually = nStopManually == 1 ? true : false;
                    oTournament.IsStarted = nIsStarted == 1 ? true : false;
                    oTournament.IsStopped = nIsStopped == 1 ? true : false;
                    oTournament.MaxNumPlayers = nMaxPlayers;

                    oTournament.NumPlayers = GetNumberOfPlayers(nTournamentID);
                    
                    oDataReader.Close();
                }
                else
                {
                    oTournament = null;
                }
            }
            catch (Exception ex)
            {
                oTournament = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return oTournament;
        }

        public int GetNumberOfPlayers(int nTournamentID)
        {
            int nNumberPlayers = 0;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT COUNT(TournamentUserID) FROM tournamentusers WHERE TournamentID = " + nTournamentID;
                
                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oDataReader = cmd.ExecuteReader();

                if (oDataReader.Read())
                {
                    string strNum = oDataReader["Count(TournamentUserID)"].ToString() ;
                    int.TryParse(strNum,  out nNumberPlayers);
                    oDataReader.Close();
                }
            }
            catch
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return nNumberPlayers;
        }

        public DataSet GetTournamentScores(int nTournamentID, string strSortBy, string strSortOrder)
        {
            DataSet dsTournamentScores = null;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT FirstName, LastName, Score, WonMatches, LostMatches, WonSets, LostSets, NumGamesPlayed FROM tournamentresults INNER JOIN users ON tournamentresults.UserID = users.UserID WHERE TournamentID = " + nTournamentID.ToString();
                strQry += " ORDER BY " + strSortBy + " " + strSortOrder;

                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader nRet = cmd.ExecuteReader();
                dsTournamentScores = oCommon.MySqlDataReaderToDataSet(nRet);
            }
            catch (Exception ex)
            {
                dsTournamentScores = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return dsTournamentScores;
        }

        public DataSet GetTournamentGamesResults(int nTournamentID, string strSortBy, string strSortOrder)
        {
            DataSet dsTournamentGames = null;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT FirstUserID AS U1ID, SecondUserID AS U2ID, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U1ID) AS FirstPlayer, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U2ID) AS SecondPlayer, Concat(FirstName, LastName) AS WinnerName FROM tournamentgames INNER JOIN gameresults ON tournamentgames.TournamentGameID = gameresults.TournamentGameID LEFT OUTER JOIN users ON gameresults.WinnerID = users.UserID WHERE tournamentgames.TournamentID = " + nTournamentID.ToString();
                strQry += " ORDER BY " + strSortBy + " " + strSortOrder;

                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader nRet = cmd.ExecuteReader();
                dsTournamentGames = oCommon.MySqlDataReaderToDataSet(nRet);
            }
            catch (Exception ex)
            {
                dsTournamentGames = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return dsTournamentGames;
        }

        public bool SetTournamentMaxDays(int nTournamentID, int nMaxDays)
        {
            bool bMaxDaysSet = false;
            MySqlConnection oConn = null;
            CCommon oCommon = new CCommon();
            try
            {
                
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE tournaments SET MustPlayedIn = " + nMaxDays + " WHERE TournamentID = " + nTournamentID;
                cmd.Connection = oConn;
                
                int nRet = cmd.ExecuteNonQuery();
                bMaxDaysSet = true;

            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return bMaxDaysSet;
        }

        public bool CheckUserRights(int nTournamentID, int nUserID)
        {
            bool bUserRights = false;
            MySqlConnection oConn = null;
            try
            {
                CUser oUserMgr = new CUser();
                
                if (oUserMgr.GetUserLevel(nUserID) == "admin")
                    return true;

                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT TournamentID from tournaments where TournamentID = " + nTournamentID + " and CreatorID=" + nUserID;
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                    bUserRights = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return bUserRights;

        }

        public bool IsUserInTournament(int nTournamentID, int nUserID)
        {
            bool bUserRights = false;
            MySqlConnection oConn = null;
            try
            {
                
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT TournamentUserID from tournamentusers where TournamentID = " + nTournamentID + " and UserID=" + nUserID;
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    bUserRights = true;
                    oReader.Close();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return bUserRights;

        }

        public bool AddTournamentResultForUser(int nTournamentID, int nUserID)
        {
            bool bIsAdded = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO tournamentresults(TournamentID, UserID, Score, WonMatches, LostMatches, WonSets, LostSets, NumGamesPlayed) VALUES (" + nTournamentID + "," + nUserID + ",0,0,0,0,0)";
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bIsAdded = true;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bIsAdded;
        }

        public bool StartCupTournament(CTournamentInfo oTournament)
        {
            bool bCupTournamentStarted = false;
            try
            {
                CGame oGame = new CGame();
                CMail oMail = new CMail();
                CUser oUserMgr = new CUser();
                ArrayList alUsers = oUserMgr.GetTournamentUsersList(oTournament.TournamentID);

                if (alUsers != null)
                {
                    #region add tournament results
                    for (int n = 0; n < alUsers.Count; n++)
                    {
                        CUserInfo oUser = (CUserInfo)alUsers[n];
                        AddTournamentResultForUser(oTournament.TournamentID, oUser.UserID);
                    }
                    #endregion

                    #region add first level games
                    Stack sCupGameIDs = new Stack();    // id of the games that are being watied for some other games to be finished
                    Stack sAloneUsersIDs = new Stack(); // id of the players that are not waiting for result
                    for (int i = 0; i < alUsers.Count; i += 2)
                    {
                        CUserInfo oUser1 = (CUserInfo)alUsers[i];
                        if ((i + 1) < alUsers.Count)
                        {
                            CUserInfo oUser2 = (CUserInfo)alUsers[i];
                            int nTournamentGameID = oGame.AddTournamentGame(oTournament.TournamentID, oUser1.UserID, oUser2.UserID, 0);
                            if (nTournamentGameID > 0)
                            {
                                int nCupGameID = oGame.AddCupGame(oTournament.TournamentID,nTournamentGameID, oUser1.UserID, oUser2.UserID, 0, -1, -1, 0);
                                if (nCupGameID > 0)
                                {
                                    oMail.SendCupMatchMail(oUser1, oUser2, oTournament);
                                    sCupGameIDs.Push(nCupGameID);
                                }
                            }
                        }
                        else
                        {
                            oMail.SendCupAloneMail(oUser1, oTournament);
                            sAloneUsersIDs.Push(oUser1.UserID);
                        }
                    }
                    #endregion

                    #region set all pending tournaments
                    Stack sAloneCupGameID = new Stack();

                    bool bKeepGoing = true;
                    while (bKeepGoing)
                    {

                        int nTotalNum = 0; 
                        int nStatus = 0;
                        int nPreGame1 = 0;
                        int nPreGame2 = 0;
                        Stack sNewCupGameIDs = new Stack();

                        bool bFirst = true;

                        foreach (int nCupGameID in sCupGameIDs)
                        {
                            // the number of the added .. if 1 then that means that this will be final
                            // for the first iteration and there is any user that skiped game due to odd number
                            if(bFirst && sAloneUsersIDs.Peek() != null)
                            {
                                int nUser1ID = (int)sAloneUsersIDs.Pop();
                                int nNewID = oGame.AddCupGame(oTournament.TournamentID, -1, nUser1ID, -1, 1, -1, nCupGameID, 0);
                                if(nNewID > 0)
                                    nTotalNum++;
                            }
                            else if(bFirst && sAloneCupGameID.Peek() != null)
                            {
                                int nGame1ID = (int)sAloneCupGameID.Pop();
                                int nNewID = oGame.AddCupGame(oTournament.TournamentID, -1, -1, -1, 1, nGame1ID, nCupGameID, 0);
                                if (nNewID > 0)
                                    nTotalNum++;
                            }
                            else
                            {
                                if(nStatus == 0)
                                    nPreGame1 = nCupGameID;
                                else if(nStatus == 1)
                                {
                                    nPreGame2 = nCupGameID;
                                    int nNewID = oGame.AddCupGame(oTournament.TournamentID, -1, -1, -1, 0, nPreGame1, nPreGame2, 0);
                                    if(nNewID > 0)
                                    {
                                        sNewCupGameIDs.Push(nNewID);
                                        nTotalNum++;
                                    }
                                    nStatus = 0;
                                    
                                }
                            }
                        }
                        if(nStatus == 1)
                        {
                            sAloneCupGameID.Clear();
                            sAloneCupGameID.Push(nPreGame1);
                        }

                        sCupGameIDs = sNewCupGameIDs;
                        
                        if (nTotalNum <= 1)
                            bKeepGoing = false;

                    }
                    #endregion

                    bCupTournamentStarted = true;
                }
                    
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return bCupTournamentStarted;
        }

        public bool StartEAETournament(CTournamentInfo oTournament)
        {
            bool bEAETournamentStarted = false;
            try
            {
                CGame oGame = new CGame();
                CMail oMail = new CMail();
                CUser oUserMgr = new CUser();
                ArrayList alUsers = oUserMgr.GetTournamentUsersList(oTournament.TournamentID);
                if (alUsers != null)
                {
                    #region add tournament results
                    for (int n = 0; n < alUsers.Count; n++)
                    {
                        CUserInfo oUser = (CUserInfo)alUsers[n];
                        AddTournamentResultForUser(oTournament.TournamentID, oUser.UserID);
                    }
                    #endregion

                    for(int i = 0; i < alUsers.Count; i++)
                    {
                        for (int j = 0; j < alUsers.Count; j++)
                        {
                            CUserInfo oFirstUser = (CUserInfo)alUsers[i];
                            CUserInfo oSecondUser = (CUserInfo)alUsers[j];

                            int nFirstUserID = oFirstUser.UserID;
                            int nSecondUserID = oSecondUser.UserID;
                            oGame.AddTournamentGame(oTournament.TournamentID, nFirstUserID, nSecondUserID, 0);
                        }
                    }
                }
                oMail.SendEAETournamentMail(oTournament, alUsers);
                bEAETournamentStarted = true;

            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return bEAETournamentStarted;
        }

        public bool SetTournamentIsStarted(int nTournamentID, int nIsStarted)
        {
            bool nValSet = false;
            MySqlConnection oConn = null;
            try
            {

                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE tournaments SET IsStarted = " + nIsStarted + " WHERE TournamentID = " + nTournamentID;
                cmd.Connection = oConn;

                int nReader = cmd.ExecuteNonQuery();
                nValSet = true;
               
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return nValSet;

        }

        public bool SetTournamentIsStopped(int nTournamentID, int nIsStopped)
        {
            bool nValSet = false;
            MySqlConnection oConn = null;
            try
            {

                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE tournaments SET IsStopped = " + nIsStopped + " WHERE TournamentID = " + nTournamentID;
                cmd.Connection = oConn;

                int nReader = cmd.ExecuteNonQuery();
                nValSet = true;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return nValSet;

        }


        public bool StartTournament(int nTournamentID)
        {
            bool bTournamentStarted = false;
            try
            {
                CTournamentInfo oTournament = GetTournamentDetailsByID(nTournamentID);
                if (oTournament != null)
                {
                    if (oTournament.TournamentType.ToLower() == "cup")
                        bTournamentStarted = StartCupTournament(oTournament);
                    else if(oTournament.TournamentType.ToLower() == "everyone against everyone")
                        bTournamentStarted = StartEAETournament(oTournament);

                    if (bTournamentStarted == true)
                        SetTournamentIsStarted(nTournamentID, 1);
                }
            }
            catch
            {
            }
            
            return bTournamentStarted;
        }

        public CUserInfo GetTournamentWinner(int nTournamentID)
        {
            CUserInfo oUser = new CUserInfo();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM users INNER JOIN tournamentresults ON tournamentresults.UserID = users.UserID WHERE TournamentID = " + nTournamentID + " ORDER BY tournamentresults.Score, tournamentresults.WonMatches, tournamentresults.WonSets  ";
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

        public bool DeleteUnPlayedTournamentGames(int nTournamentID)
        {
            bool bDeleted = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "DELETE FROM tournamentgames where TournamentID = " + nTournamentID + " and IsPlayed=0" ;
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bDeleted = true;
                
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bDeleted;
        }

        public bool StopTournament(int nTournamentID)
        {
            bool bTournamentStopped = false;
            try
            {
                CTournamentInfo oTournament = GetTournamentDetailsByID(nTournamentID);
                CMail oMailMgr = new CMail();
                CUser oUserMgr = new CUser();
                if (oTournament != null)
                {
                    if(DeleteUnPlayedTournamentGames(nTournamentID))
                    {
                        if (SetTournamentIsStopped(nTournamentID, 1))
                        {
                            ArrayList alUsers  = oUserMgr.GetTournamentUsersList(nTournamentID);
                            CUserInfo oWinner = GetTournamentWinner(nTournamentID);
                            oMailMgr.SentTournamentStopMail(oTournament, alUsers, oWinner);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return bTournamentStopped;
        }

        public bool JoinTournament(int nTournamentID, int nUserID)
        {
            bool bTournamentJoined = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO tournamentusers(UserID, TournamentID) VALUES (" + nTournamentID + "," + nUserID + ")";
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bTournamentJoined = true;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bTournamentJoined;
        }

        public bool UnjoinTournament(int nTournamentID, int nUserID)
        {
            bool bTournamentJoined = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "DELETE FROM tournamentusers WHERE TournamentID = " + nTournamentID + " and UserID = " + nUserID;
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bTournamentJoined = true;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bTournamentJoined;
        }

        public bool DeleteTournament(int nTournamentID)
        {
            bool bDeleted = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                #region deleting from tables

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "DELETE FROM tournamentgames, gameresults USING tournamentgames LEFT OUTER JOIN gameresults ON tournamentgames.TournamentGameID = gameresults.TournamentGameID where TournamentID = " + nTournamentID;
                cmd.Connection = oConn;
                int nRet = cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM tournamentusers where TournamentID = " + nTournamentID;
                cmd.Connection = oConn;
                nRet = cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM tournamentresults where TournamentID = " + nTournamentID;
                cmd.Connection = oConn;
                nRet = cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM cupgames where TournamentID = " + nTournamentID;
                cmd.Connection = oConn;
                nRet = cmd.ExecuteNonQuery();
                #endregion

                bDeleted = true;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bDeleted;
        }

        public int GetTournamentIDByTournamentGameID(int nGameID)
        {
            int nTournamentID = 0;
            
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT TournamentID FROM tournamentgames WHERE TournamentGameID = " + nGameID;
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    string strTournamentID = oReader["TournamentID"].ToString();
                    int.TryParse(strTournamentID, out nTournamentID);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

           
            return nTournamentID;
        }

        public bool AddTournamentResultValues(int nTournamentID, int nUserID, int nScore, int nWonMatches, int nLostMatches, int nWonSets, int nLostSets, int nNumPlayedGames)
        {
            bool bValuesAdded = false;
            MySqlConnection oConn = null;
            CCommon oCommon = new CCommon();
            try
            {

                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE tournamentresults SET Score =Score+" + nScore + " , WonMatches=WonMatches+" + nWonMatches + " , LostMatches=LostMatches+" + nLostMatches + " , WonSets=WonSets+" + nWonSets + " , LostSets=LostSets+" + nLostSets + " , NumGamesPlayed=NumGamesPlayed+" + nNumPlayedGames;
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bValuesAdded = true;

            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return bValuesAdded;
        }


        public bool SetCupTournamentNextGame(CGameResultInfo oGameResult)
        {
            bool bIsSet = false;
            try
            {
                CGame oGameMgr = new CGame();
                int nCupGameID = oGameMgr.GetCupGameIDByGameID(oGameResult.GameID);
                if (nCupGameID > 0)
                {
                    oGameMgr.SetCupGamePlayed(nCupGameID, 1);
                    if (oGameResult.WinnerID == 0)
                        oGameResult.WinnerID = oGameResult.FirstUserID;
                    oGameMgr.SetCupGamesPrerequisites(nCupGameID, oGameResult.WinnerID);
                }
            }
            catch (Exception ex)
            {
            }
            return bIsSet;
        }


        public bool IsTournamentStopped(CTournamentInfo oTournament)
        {
            bool bIsStopped = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();

                if (oTournament.TournamentType == "Cup")
                {
                    cmd.CommandText = "SELECT CupGameID FROM cupgames WHERE TournamentID = " + oTournament.TournamentID + " and IsPlayed=0";
                    cmd.Connection = oConn;
                    MySqlDataReader oReader = cmd.ExecuteReader();
                    if (oReader.Read())
                    {
                        oReader.Close();
                        bIsStopped = false;
                    }
                    else
                        bIsStopped = true;
                }
                else
                {
                    cmd.CommandText = "SELECT TournamentGameID FROM TournamentGames WHERE TournamentID = " + oTournament.TournamentID + " and IsPlayed=0";
                    cmd.Connection = oConn;
                    MySqlDataReader oReader = cmd.ExecuteReader();
                    if (oReader.Read())
                    {
                        oReader.Close();
                        bIsStopped = false;
                    }
                    else
                        bIsStopped = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bIsStopped;
            
        }
    }


    public class CTournamentInfo
    {
        private int nTournamentID;
        private string strTournamentName;
        private string strTournamentDesc;
        private string strTournamentType;
        private bool bStartManually;
        private bool bStopManually;
        private DateTime oStartDate;
        private DateTime oStopDate;
        private int nCreatorID;
        private int nNumMaxPlayers;
        private int nNumPlayers;
        private bool bIsStarted;
        private bool bIsStopped;
        private int nMustPlayedIn;


        public CTournamentInfo()
        {
            
        }

        public int TournamentID
        {
            get { return nTournamentID; }
            set { nTournamentID = value; }
        }

        public string TournamentName
        {
            get { return strTournamentName; }
            set { strTournamentName = value; }
        }


        public string TournamentDesc
        {
            get { return strTournamentDesc; }
            set { strTournamentDesc = value; }
        }

        public string TournamentType
        {
            get { return strTournamentType; }
            set { strTournamentType = value; }
        }
        public bool StartManually
        {
            get { return bStartManually; }
            set { bStartManually = value; }
        }

        public bool StopManually
        {
            get { return bStopManually; }
            set { bStopManually = value; }
        }

        public DateTime StartDate
        {
            get { return oStartDate; }
            set { oStartDate = value; }
        }

        public DateTime StopDate
        {
            get { return oStopDate; }
            set { oStopDate = value; }
        }

        public int CreatorID
        {
            get { return nCreatorID; }
            set { nCreatorID = value; }
        }

        public int MaxNumPlayers
        {
            get { return nNumMaxPlayers; }
            set { nNumMaxPlayers = value; }
        }

        public int NumPlayers
        {
            get { return nNumPlayers; }
            set { nNumPlayers = value; }
        }

        public bool IsStarted
        {
            get { return bIsStarted; }
            set { bIsStarted = value; }
        }

        public bool IsStopped
        {
            get { return bIsStopped; }
            set { bIsStopped = value; }
        }

        public int MustPlayedIn
        {
            get { return nMustPlayedIn; }
            set { nMustPlayedIn = value; }
        }
    }
}