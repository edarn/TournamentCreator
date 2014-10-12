using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;


namespace TournamentCreator.Common
{
    public class CGame
    {
        public ArrayList GetTournamentUnplayedGamesList(int nTournamentID)
        {
            ArrayList alGames = null;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT TournamentGameID, FirstUserID AS U1ID, SecondUserID AS U2ID, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U1ID) AS FirstPlayer, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U2ID) AS SecondPlayer FROM tournamentgames WHERE TournamentID =  " + nTournamentID + " and IsPlayed=0";


                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oReader = cmd.ExecuteReader();
                while (oReader.Read())
                {
                    CGameInfo oGame = new CGameInfo();
                    string strTournamentGameID = oReader["TournamentGameID"].ToString();
                    int nTournamentGameID = 0;
                    if (!string.IsNullOrEmpty(strTournamentGameID))
                    {
                        if (int.TryParse(strTournamentGameID, out nTournamentGameID))
                        {
                            if (nTournamentGameID > 0)
                            {
                                oGame.GameID = nTournamentGameID;
                                oGame.FirstPlayerName = oReader["FirstPlayer"].ToString();
                                oGame.SecondPlayerName = oReader["SecondPlayer"].ToString();

                                alGames.Add(oGame);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                alGames = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return alGames;
        }

        public CGameInfo GetGameDetails(int nGameID)
        {
            MySqlConnection oConn = null;
            CGameInfo oGame = new CGameInfo();
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT TournamentGameID, FirstUserID AS U1ID, SecondUserID AS U2ID, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U1ID) AS FirstPlayer, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U2ID) AS SecondPlayer FROM tournamentgames WHERE TournamentID =  " + nTournamentID + " and IsPlayed=0";


                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    
                    string strTournamentGameID = oReader["TournamentGameID"].ToString();
                    int nTournamentGameID = 0;
                    if (!string.IsNullOrEmpty(strTournamentGameID))
                    {
                        if (int.TryParse(strTournamentGameID, out nTournamentGameID))
                        {
                            if (nTournamentGameID > 0)
                            {
                                oGame.GameID = nTournamentGameID;
                                int nFirstPlayerID = 0, nSecondPlayerID = 0;
                                int.TryParse(oReader["U1ID"].ToString(), out nFirstPlayerID);
                                int.TryParse(oReader["U2ID"].ToString(), out nSecondPlayerID);
                                oGame.FirstPlayerID = nFirstPlayerID;
                                oGame.SecondPlayerID = nSecondPlayerID;
                                oGame.FirstPlayerName = oReader["FirstPlayer"].ToString();
                                oGame.SecondPlayerName = oReader["SecondPlayer"].ToString();
                            }
                            else
                                oGame = null;
                        }
                        else
                            oGame = null;
                    }
                    else
                        oGame = null;
                }

            }
            catch (Exception ex)
            {
                oGame = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return oGame;
        }
        

        public int AddCupGame(int nTournamentID, int nTournamentGameID, int nFirstUserID, int nSecondUserID, int nIsWaiting, int nFirstUserWaitingID, int nSecondUserWaitingID, int nIsPlayed)
        {
            int nCupGameID = 0;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO cupgames(TournamentID, TournamentGameID, FirstUserID, SecondUserID, IsWaiting, FirstUserWaitingID, SecondUserWaitingID, Isplayed) VALUES (" + nTournamentID + "," + nTournamentGameID + "," + nFirstUserID + "," + nSecondUserID + "," + nIsWaiting + "," + nFirstUserWaitingID + "," + nSecondUserWaitingID + "," + nIsPlayed + ")";
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                int nRet = cmd.ExecuteNonQuery();
                nCupGameID = (int)cmd.LastInsertedId;
                
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
            return nCupGameID;
        }

        public int AddTournamentGame(int nTournamentID, int nFirstUserID, int nSecondUserID, int nIsPlayed)
        {
            int nGameID = 0;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO tournamentgames(TournamentID, FirstUserID, SecondUserID, IsWaiting, FirstUserWaitingID, SecondUserWaitingID, Isplayed) VALUES (" + nTournamentID + "," + nFirstUserID + "," + nSecondUserID + nIsPlayed + ")";
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                int nRet = cmd.ExecuteNonQuery();
                nGameID = (int)cmd.LastInsertedId;

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
            return nGameID;
        }

        public bool RemoveUResult(int nResultID)
        {
            bool bDeleted = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "DELETE FROM gameresultsunverified where GameResultUID = " + nResultID;
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

        public int AddURestultEntry(CGameResultInfo oResultInfo)
        {
            int nResultID = 0;
            MySqlConnection oConn = null;
            CCommon oCommon = new CCommon();
            try
            {

                CDatabase oDB = new CDatabase();
                
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "INSERT INTO gameresultsunverified(TournamentGameID, SubmitterID, User1WonSet, User1LostSet, User2WonSet, User2LostSet, WinnerID, SubmitDate, ContainsError) VALUES (" + oResultInfo.GameID + " , " + oResultInfo.SubmitterID + " , " + oResultInfo.FirstPlayerWonSet + " , " + oResultInfo.FirstPlayerLostSet + " , " + oResultInfo.WinnerID + " , " + oCommon.DateToString(DateTime.Now) + " , 0 )" ;
                cmd.Connection = oConn;
                int nRet = cmd.ExecuteNonQuery();

                nResultID = (int)cmd.LastInsertedId;

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
            return nResultID;
        }

        public bool SetUResultContainError(int nGameResultUID, int nVal)
        {
            bool bResultUpdated = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE gameresultsunverified SET ContainsError=" + nVal + " WHERE GameResultUID=" + oResultInfo.GameResultID;
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bResultUpdated = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bResultUpdated;
        }

        public bool AddUResult(CGameResultInfo oResultInfo)
        {
            bool bResultAdded = false;
            try
            {
                CGameResultInfo oResultInfo2 = GetGameUResultDetailsByGameID(oResultInfo.GameID);
                if (oResultInfo2 != null)
                {
                    // check if the results are equal
                    if (oResultInfo.FirstPlayerWonSet == oResultInfo2.FirstPlayerWonSet && oResultInfo.FirstPlayerLostSet == oResultInfo2.FirstPlayerLostSet && oResultInfo.SecondPlayerWonSet == oResultInfo2.SecondPlayerWonSet && oResultInfo.SecondPlayerLostSet == oResultInfo2.SecondPlayerLostSet && oResultInfo.WinnerID == oResultInfo2.WinnerID)
                    {
                        AddResult(oResultInfo);
                        RemoveUResult(oResultInfo2.GameResultID);
                    }
                    else
                    {
                        CMail oMailMgr = new CMail();
                        oMailMgr.SendResultMismatchMail(oResultInfo, oResultInfo2);
                        int nRet = AddURestultEntry(oResultInfo);
                        if (nRet > 0)
                        {
                            SetUResultContainError(oResultInfo2.GameResultID, 1);
                            SetUResultContainError(nRet, 1);
                        }
                    }

                }
                else
                {
                    AddURestultEntry(oResultInfo);
                }
            }
            catch (Exception ex)
            {
            }
            return bResultAdded;
        }

        public bool EditUResult(CGameResultInfo oResultInfo)
        {
            bool bResultUpdated = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE gameresultsunverified SET User1WonSet=" + oResultInfo.FirstPlayerWonSet + ", User1LostSet=" + oResultInfo.FirstPlayerLostSet + ", User2WonSet=" + oResultInfo.SecondPlayerWonSet + ", User2LostSet=" + oResultInfo.SecondPlayerLostSet + ", WinnerID=" + oResultInfo.WinnerID + " WHERE GameResultUID=" + oResultInfo.GameResultID;
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                int nLastIns = (int)cmd.LastInsertedId;

                if (nLastIns > 0)
                {
                    CGameResultInfo oResultInfo2 = GetGameUResultDetailsByGameIDExc(oResultInfo.GameID, nLastIns);
                    if (oResultInfo.FirstPlayerWonSet == oResultInfo2.FirstPlayerWonSet && oResultInfo.FirstPlayerLostSet == oResultInfo2.FirstPlayerLostSet && oResultInfo.SecondPlayerWonSet == oResultInfo2.SecondPlayerWonSet && oResultInfo.SecondPlayerLostSet == oResultInfo2.SecondPlayerLostSet && oResultInfo.WinnerID == oResultInfo2.WinnerID)
                    {
                        AddResult(oResultInfo);
                        RemoveUResult(oResultInfo2.GameResultID);
                    }
                }

                bResultUpdated = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bResultUpdated;
        }

        public bool SetGamePlayed(int nGameID, int nIsPlayed)
        {
            bool bUUpdated = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE tournamentgames SET IsPlayed=" + nIsPlayed + " WHERE TournamentGameID=" + nGameID;
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bUUpdated = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bUUpdated;
        }

        public bool SetCupGamesPrerequisites(int nCupGameID, int nWinnerID)
        {
            bool bCupGameSet = false;
            MySqlConnection oConn = null;
            CCommon oCommon = new CCommon();
            try
            {

                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE cupgames FirstUserID = nWinnerID, FirstUserWaitingID=-1  WHERE FirstUserWaitingID=" + nCupGameID;
                cmd.Connection = oConn;
                int nRet = cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE cupgames SecondUserID = nWinnerID, SecondUserWaitingID=-1  WHERE SecondUserWaitingID=" + nCupGameID;
                cmd.Connection = oConn;
                nRet = cmd.ExecuteNonQuery();
                bCupGameSet = true;

                cmd.CommandText = "INSERT INTO tournamentgames(TournamentID, FirstUserID, SecondUserID, IsPlayed) SELECT TournamentID, FirstUserID, SecondUserID, 0 FROM cupgames WHERE (FirstUserID=" + nCupGameID + " OR SecondUserID=" + nCupGameID + ") AND FirstUserWaitingID=-1 AND SecondUserWaitingID=-1 ";
                cmd.Connection = oConn;
                nRet = cmd.ExecuteNonQuery();
                bCupGameSet = true;

                // since there will be Singlel update
                int nLast = (int)cmd.LastInsertedId;

                cmd.CommandText = "UPDATE cupgames set TournamentGameID=" + nLast + " WHERE CupGameID=" + nCupGameID;
                cmd.Connection = oConn;
                nRet = cmd.ExecuteNonQuery();
                bCupGameSet = true;

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
            return bCupGameSet;
        }

        public bool SetCupGamePlayed(int nGameID, int nIsPlayed)
        {
            bool bUUpdated = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE cupgames SET IsPlayed=" + nIsPlayed + " WHERE CupGameID=" + nGameID;
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bUUpdated = true;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bUUpdated;
        }

        public bool AddTournamentResultEntry(CGameResultInfo oResultInfo)
        {
            bool bResultAdded = false;
            MySqlConnection oConn = null;
            try
            {
                SetGamePlayed(oResultInfo.GameID, 1);


                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO gameresults(TournamentGameID, User1WonSet, User1LostSet, User2WonSet, User2LostSet, WinnerID) values (" + oResultInfo.GameID + ", " + oResultInfo.FirstPlayerWonSet + ", " + oResultInfo.SecondPlayerWonSet + ", " + oResultInfo.SecondPlayerLostSet + oResultInfo.WinnerID + ")";
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bResultAdded = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bResultAdded;
        }


        // for admin and if the users results are matched
        public bool AddResult(CGameResultInfo oResultInfo)
        {
            bool bResultAdded = false;
            MySqlConnection oConn = null;
            CTournament oTournamentMgr = new CTournament();
            try
            {
                SetGamePlayed(oResultInfo.GameID, 1);
                AddTournamentResultEntry(oResultInfo);
                int nTournamentID = oTournamentMgr.GetTournamentIDByTournamentGameID(oResultInfo.GameID);
                if (nTournamentID > 0)
                {
                    CTournamentInfo oTournament = oTournamentMgr.GetTournamentDetailsByID(nTournamentID);
                    if (oTournament.TournamentType == "Cup")
                    {
                        oTournamentMgr.SetCupTournamentNextGame(oResultInfo);
                        int nScore1 = oResultInfo.WinnerID == oResultInfo.FirstUserID ? 3 : 0;
                        int nScore2 = oResultInfo.WinnerID == oResultInfo.SecondUserID ? 3 : 0;
                        if(oResultInfo.WinnerID == 0)
                            nScore1 = nScore2 = 1;

                        int nWon1 = 0, nWon2 = 0, nLost1 = 0, nLost2;
                        if(nScore1 == 3)
                        {
                            nWon1 = 1;
                            nLost2 = 1;
                        }
                        else if(nScore2 == 3)
                        {
                            nWon2 = 1;
                            nLost1 = 1;
                        }


                        oTournamentMgr.AddTournamentResultValues(nTournamentID, oResultInfo.FirstUserID, nScore1, nWon1, nLost1, oResultInfo.FirstPlayerWonSet, oResultInfo.FirstPlayerLostSet, 1);

                        oTournamentMgr.AddTournamentResultValues(nTournamentID, oResultInfo.SecondUserID, nScore2, nWon2, nLost2, oResultInfo.SecondPlayerWonSet, oResultInfo.SecondPlayerLostSet, 1);

                        if (oTournamentMgr.IsTournamentStopped(oTournament))
                        {
                            oTournamentMgr.SetTournamentIsStopped(nTournamentID, 1);
                            CMail oMailMgr = new CMail();
                            CUser oUserMgr = new CUser();
                            ArrayList alTournamentUsers = oUserMgr.GetTournamentUsersList(nTournamentID);
                            CUserInfo oWinner = oTournamentMgr.GetTournamentWinner(nTournamentID);
                            oMailMgr.SentTournamentStopMail(oTournament, alTournamentUsers, oWinner);
                        }
                    }
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

            return bResultAdded;
        }

        // only for admin 
        public bool EditResult(CGameResultInfo oResultInfo)
        {
            bool bResultUpdated = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE gameresults SET User1WonSet=" + oResultInfo.FirstPlayerWonSet + ", User1LostSet=" + oResultInfo.FirstPlayerLostSet + ", User2WonSet=" + oResultInfo.SecondPlayerWonSet + ", User2LostSet=" + oResultInfo.SecondPlayerLostSet + ", WinnerID=" + oResultInfo.WinnerID + " WHERE GameResultID=" + oResultInfo.GameResultID;
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                bResultUpdated = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bResultUpdated;
        }

        public CGameResultInfo GetGameUResultDetailsByGameID(int nGameID)
        {
            CGameResultInfo oResultInfo = new CGameResultInfo();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT GameResultUID, FirstUserID AS U1ID, SecondUserID AS U2ID, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U1ID) AS FirstPlayer, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U2ID) AS SecondPlayer, Concat(FirstName, LastName) AS WinnerName, WinnerID, User1WonSet,User1LostSet, User2WonSet, User2LostSet FROM tournamentgames INNER JOIN gameresultsunverified ON tournamentgames.TournamentGameID = gameresultsunverified.TournamentGameID LEFT OUTER JOIN users ON gameresultsunverified.WinnerID = users.UserID WHERE gameresultsunverified.TournamentGameID =  " + nGameID;

                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oDataReader = cmd.ExecuteReader();

                if (oDataReader.Read())
                {
                    int nGameResult, nFirstPlayerLostSet, nSecondPlayerLostSet, nFirstPlayerWonSet, nSecondPlayerWonSet, nFirstUserID, nSecondUserID, nWinnerID;
                    nGameResult = nFirstPlayerLostSet = nSecondPlayerLostSet = nFirstPlayerWonSet = nSecondPlayerWonSet = nFirstUserID = nSecondUserID = nWinnerID = 0;

                    if (int.TryParse(oDataReader["GameResultUID"].ToString(), out nGameResult))
                    {
                        oResultInfo.GameResultID = nGameResult;
                        oResultInfo.FirstPlayerName = oDataReader["FirstPlayer"].ToString();
                        oResultInfo.SecondPlayerName = oDataReader["SecondPlayer"].ToString();
                        oResultInfo.WinnerName = oDataReader["WinnerName"].ToString();

                        int.TryParse(oDataReader["User1WonSet"].ToString(), out nFirstPlayerWonSet);
                        int.TryParse(oDataReader["User1LostSet"].ToString(), out nFirstPlayerLostSet);
                        int.TryParse(oDataReader["User2WonSet"].ToString(), out nSecondPlayerWonSet);
                        int.TryParse(oDataReader["User2LostSet"].ToString(), out nSecondPlayerLostSet);

                        int.TryParse(oDataReader["U1ID"].ToString(), out nFirstUserID);
                        int.TryParse(oDataReader["U2ID"].ToString(), out nSecondUserID);
                        int.TryParse(oDataReader["WinnerID"].ToString(), out nWinnerID);

                        oResultInfo.FirstPlayerWonSet = nFirstPlayerWonSet;
                        oResultInfo.FirstPlayerLostSet = nFirstPlayerWonSet;
                        oResultInfo.SecondPlayerWonSet = nSecondPlayerWonSet;
                        oResultInfo.SecondPlayerLostSet = nSecondPlayerLostSet;

                        oResultInfo.FirstUserID = nFirstUserID;
                        oResultInfo.SecondUserID = nSecondUserID;
                        oResultInfo.WinnerID = nWinnerID;
                    }
                    else
                    {
                        oResultInfo = null;
                    }
                    oDataReader.Close();
                }
                else
                {
                    oResultInfo = null;
                }
            }
            catch (Exception ex)
            {
                oResultInfo = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return oResultInfo;
        }

        public CGameResultInfo GetGameUResultDetailsByGameIDExc(int nGameID, int nExcludingID)
        {
            CGameResultInfo oResultInfo = new CGameResultInfo();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT GameResultUID, FirstUserID AS U1ID, SecondUserID AS U2ID, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U1ID) AS FirstPlayer, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U2ID) AS SecondPlayer, Concat(FirstName, LastName) AS WinnerName, WinnerID, User1WonSet,User1LostSet, User2WonSet, User2LostSet FROM tournamentgames INNER JOIN gameresultsunverified ON tournamentgames.TournamentGameID = gameresultsunverified.TournamentGameID LEFT OUTER JOIN users ON gameresultsunverified.WinnerID = users.UserID WHERE gameresultsunverified.TournamentGameID =  " + nGameID + " and GameResultUID!=" + nExcludingID;

                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oDataReader = cmd.ExecuteReader();

                if (oDataReader.Read())
                {
                    int nGameResult, nFirstPlayerLostSet, nSecondPlayerLostSet, nFirstPlayerWonSet, nSecondPlayerWonSet, nFirstUserID, nSecondUserID, nWinnerID;
                    nGameResult = nFirstPlayerLostSet = nSecondPlayerLostSet = nFirstPlayerWonSet = nSecondPlayerWonSet = nFirstUserID = nSecondUserID = nWinnerID = 0;

                    if (int.TryParse(oDataReader["GameResultUID"].ToString(), out nGameResult))
                    {
                        oResultInfo.GameResultID = nGameResult;
                        oResultInfo.FirstPlayerName = oDataReader["FirstPlayer"].ToString();
                        oResultInfo.SecondPlayerName = oDataReader["SecondPlayer"].ToString();
                        oResultInfo.WinnerName = oDataReader["WinnerName"].ToString();

                        int.TryParse(oDataReader["User1WonSet"].ToString(), out nFirstPlayerWonSet);
                        int.TryParse(oDataReader["User1LostSet"].ToString(), out nFirstPlayerLostSet);
                        int.TryParse(oDataReader["User2WonSet"].ToString(), out nSecondPlayerWonSet);
                        int.TryParse(oDataReader["User2LostSet"].ToString(), out nSecondPlayerLostSet);

                        int.TryParse(oDataReader["U1ID"].ToString(), out nFirstUserID);
                        int.TryParse(oDataReader["U2ID"].ToString(), out nSecondUserID);
                        int.TryParse(oDataReader["WinnerID"].ToString(), out nWinnerID);

                        oResultInfo.FirstPlayerWonSet = nFirstPlayerWonSet;
                        oResultInfo.FirstPlayerLostSet = nFirstPlayerWonSet;
                        oResultInfo.SecondPlayerWonSet = nSecondPlayerWonSet;
                        oResultInfo.SecondPlayerLostSet = nSecondPlayerLostSet;

                        oResultInfo.FirstUserID = nFirstUserID;
                        oResultInfo.SecondUserID = nSecondUserID;
                        oResultInfo.WinnerID = nWinnerID;
                    }
                    else
                    {
                        oResultInfo = null;
                    }
                    oDataReader.Close();
                }
                else
                {
                    oResultInfo = null;
                }
            }
            catch (Exception ex)
            {
                oResultInfo = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return oResultInfo;
        }

        public CGameResultInfo GetGameUResultDetails(int nResultID)
        {
            CGameResultInfo oResultInfo = new CGameResultInfo();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT GameResultUID, FirstUserID AS U1ID, SecondUserID AS U2ID, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U1ID) AS FirstPlayer, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U2ID) AS SecondPlayer, Concat(FirstName, LastName) AS WinnerName, WinnerID, User1WonSet,User1LostSet, User2WonSet, User2LostSet FROM tournamentgames INNER JOIN gameresultsunverified ON tournamentgames.TournamentGameID = gameresultsunverified.TournamentGameID LEFT OUTER JOIN users ON gameresultsunverified.WinnerID = users.UserID WHERE gameresultsunverified.GameResultUID =  " + nResultID;

                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oDataReader = cmd.ExecuteReader();

                if (oDataReader.Read())
                {
                    int nGameResult, nFirstPlayerLostSet, nSecondPlayerLostSet, nFirstPlayerWonSet, nSecondPlayerWonSet, nFirstUserID, nSecondUserID, nWinnerID;
                    nGameResult = nFirstPlayerLostSet = nSecondPlayerLostSet = nFirstPlayerWonSet = nSecondPlayerWonSet = nFirstUserID = nSecondUserID = nWinnerID = 0;

                    if (int.TryParse(oDataReader["GameResultUID"].ToString(), out nGameResult))
                    {
                        oResultInfo.GameResultID = nGameResult;
                        oResultInfo.FirstPlayerName = oDataReader["FirstPlayer"].ToString();
                        oResultInfo.SecondPlayerName = oDataReader["SecondPlayer"].ToString();
                        oResultInfo.WinnerName = oDataReader["WinnerName"].ToString();

                        int.TryParse(oDataReader["User1WonSet"].ToString(), out nFirstPlayerWonSet);
                        int.TryParse(oDataReader["User1LostSet"].ToString(), out nFirstPlayerLostSet);
                        int.TryParse(oDataReader["User2WonSet"].ToString(), out nSecondPlayerWonSet);
                        int.TryParse(oDataReader["User2LostSet"].ToString(), out nSecondPlayerLostSet);

                        int.TryParse(oDataReader["U1ID"].ToString(), out nFirstUserID);
                        int.TryParse(oDataReader["U2ID"].ToString(), out nSecondUserID);
                        int.TryParse(oDataReader["WinnerID"].ToString(), out nWinnerID);

                        oResultInfo.FirstPlayerWonSet = nFirstPlayerWonSet;
                        oResultInfo.FirstPlayerLostSet = nFirstPlayerWonSet;
                        oResultInfo.SecondPlayerWonSet = nSecondPlayerWonSet;
                        oResultInfo.SecondPlayerLostSet = nSecondPlayerLostSet;

                        oResultInfo.FirstUserID = nFirstUserID;
                        oResultInfo.SecondUserID = nSecondUserID;
                        oResultInfo.WinnerID = nWinnerID;
                    }
                    else
                    {
                        oResultInfo = null;
                    }
                    oDataReader.Close();
                }
                else
                {
                    oResultInfo = null;
                }
            }
            catch (Exception ex)
            {
                oResultInfo = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return oResultInfo;
        }

        public CGameResultInfo GetGameResultDetails(int nResultID)
        {
            CGameResultInfo oResultInfo = new CGameResultInfo();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT GameResultID, FirstUserID AS U1ID, SecondUserID AS U2ID, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U1ID) AS FirstPlayer, (SELECT Concat(FirstName, LastName) FROM users WHERE UserID=U2ID) AS SecondPlayer, Concat(FirstName, LastName) AS WinnerName, WinnerID, User1WonSet,User1LostSet, User2WonSet, User2LostSet FROM tournamentgames INNER JOIN gameresults ON tournamentgames.TournamentGameID = gameresults.TournamentGameID LEFT OUTER JOIN users ON gameresults.WinnerID = users.UserID WHERE gameresults.GameResultID =  " + nResultID;

                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oDataReader = cmd.ExecuteReader();

                if (oDataReader.Read())
                {
                    int nGameResult, nFirstPlayerLostSet, nSecondPlayerLostSet, nFirstPlayerWonSet, nSecondPlayerWonSet, nFirstUserID, nSecondUserID, nWinnerID;
                    nGameResult = nFirstPlayerLostSet = nSecondPlayerLostSet = nFirstPlayerWonSet = nSecondPlayerWonSet = nFirstUserID = nSecondUserID = nWinnerID = 0;

                    if (int.TryParse(oDataReader["GameResultID"].ToString(), out nGameResult))
                    {
                        oResultInfo.GameResultID = nGameResult;
                        oResultInfo.FirstPlayerName = oDataReader["FirstPlayer"].ToString();
                        oResultInfo.SecondPlayerName = oDataReader["SecondPlayer"].ToString();
                        oResultInfo.WinnerName = oDataReader["WinnerName"].ToString();

                        int.TryParse(oDataReader["User1WonSet"].ToString(), out nFirstPlayerWonSet);
                        int.TryParse(oDataReader["User1LostSet"].ToString(), out nFirstPlayerLostSet);
                        int.TryParse(oDataReader["User2WonSet"].ToString(), out nSecondPlayerWonSet);
                        int.TryParse(oDataReader["User2LostSet"].ToString(), out nSecondPlayerLostSet);

                        int.TryParse(oDataReader["U1ID"].ToString(), out nFirstUserID);
                        int.TryParse(oDataReader["U2ID"].ToString(), out nSecondUserID);
                        int.TryParse(oDataReader["WinnerID"].ToString(), out nWinnerID);

                        oResultInfo.FirstPlayerWonSet = nFirstPlayerWonSet;
                        oResultInfo.FirstPlayerLostSet = nFirstPlayerWonSet;
                        oResultInfo.SecondPlayerWonSet = nSecondPlayerWonSet;
                        oResultInfo.SecondPlayerLostSet = nSecondPlayerLostSet;

                        oResultInfo.FirstUserID = nFirstUserID;
                        oResultInfo.SecondUserID = nSecondUserID;
                        oResultInfo.WinnerID = nWinnerID;
                    }
                    else
                    {
                        oResultInfo = null;
                    }
                    oDataReader.Close();
                }
                else
                {
                    oResultInfo = null;
                }
            }
            catch (Exception ex)
            {
                oResultInfo = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return oResultInfo;
        }

        public bool IsResultSubmitedBy(int nUResultID, int nUserID)
        {
            bool bResultSubmittedBy = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT GameResultUID FROM gameresultsunverified WHERE GameResultUID = " + nUResultID + " and SubmitterID=" + nUserID;
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    bResultSubmittedBy = true;
                    
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

            return bResultSubmittedBy;
        }

        public ArrayList GetUserGames(int nUserID, string strSortBy, string strSortOrder, string strOpt)
        {
            ArrayList alGames = new ArrayList();
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                CCommon oCommon = new CCommon();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                string strQry = "SELECT tournamentgames.GameID AS GID, gameresults.GameResultID AS GRID, gameresultsunverified.GameResultUID AS GRUID  , tournaments.TournamentName AS TournamentName, tournamentgames.FirstUserID AS U1ID, tournamentgames.SecondUserID AS U2ID, (SELECT Concat(FirstName, ' ' , LastName) FROM users WHERE UserID=U1ID) AS FirstPlayer, (SELECT Concat(FirstName, ' ', LastName) FROM users WHERE UserID=U2ID) AS SecondPlayer, (SELECT Concat(FirstPlayer, ' vs ', SecondPlayer)) AS GameName , ";
                strQry += " (SELECT COUNT(*) FROM gameresults WHERE TournamentGameID = GID) AS NumResults, (SELECT COUNT(*) FROM gameresultsunverified WHERE TournamentGameID = GID ) AS NumUResults, (SELECT COUNT(*) FROM gameresultsunverified WHERE TournamentGameID = GID and ContainsError ) AS NumUResultsInvalid ";

                strQry += " FROM  tournamentgames INNER JOIN tournaments ON tournamentgames.TournamentID = tournaments.TournamentID ";
                strQry += " INNER JOIN tournamentusers ON tournamentgames.TournamentID = tournamentusers.TournamentID ";
                strQry += " LEFT OUTER JOIN gameresults ON gameresults.TournamentGameID = tournamentgames.TournamentGameID ";
                strQry += " WHERE tournamentusers.UserID = " + nUserID + " ";

                if (strOpt == "played")
                    strQry += " NumResults > 0 ";
                else if (strOpt == "notplayed")
                    strQry += " NumResults <= 0 ";
                if (strOpt == "pendingresults")
                    strQry += " NumUResults > 0 ";
                if (strOpt == "invalidresults")
                    strQry += " NumUResultsInvalid > 0 ";

                strQry += " ORDER BY " + strSortBy + " " + strSortOrder;
                cmd.CommandText = strQry;
                cmd.Connection = oConn;
                //cmd.CommandType = CommandType.TableDirect;
                MySqlDataReader oReader = cmd.ExecuteReader();
                while (oReader.Read())
                {
                    CUserGameInfo oGameInfo = new CUserGameInfo();
                    int nGameID, nGameResultID, nUnverifiedGameResultID, nNumResults, nNumUResults, nNumUResultsInvalid;
                    nGameID = nGameResultID = nUnverifiedGameResultID = nNumResults = nNumUResults = nNumUResultsInvalid;
                    oGameInfo.TournamentName = oReader["TournamentName"].ToString();
                    oGameInfo.GameName = oReader["GameName"].ToString();

                    int.TryParse(oReader["GID"].ToString(), out nGameID);
                    int.TryParse(oReader["GRID"].ToString(), out nGameResultID);
                    int.TryParse(oReader["GRUID"].ToString(), out nUnverifiedGameResultID);
                    int.TryParse(oReader["NumResults"].ToString(), out nNumResults);
                    int.TryParse(oReader["NumUResults"].ToString(), out nNumUResults);
                    int.TryParse(oReader["NumUResultsInvalid"].ToString(), out nNumUResultsInvalid);

                    oGameInfo.GameID = nGameID;
                    oGameInfo.GameResultID = nGameResultID;
                    oGameInfo.UnverifiedGameResultID = nUnverifiedGameResultID;

                    oGameInfo.NumResults = nNumResults;
                    oGameInfo.NumUnverifiedResults = nNumUResults;
                    oGameInfo.NumUResultsInvalid = nNumUResultsInvalid;


                    if (oGameInfo.GameID > 0)
                        alGames.Add(oGameInfo);
                }

            }
            catch (Exception ex)
            {
                alGames = null;
            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }
            return alGames;
        }

        public int GetCupGameIDByGameID(int nGameID)
        {
            int nCupGameID = 0;
            
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT CupGameID FROM cupgames WHERE TournamentGameID = " + nGameID;
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    string strCupGameID = oReader["CupGameID"].ToString();
                    int.TryParse(strCupGameID, out nCupGameID);
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

           
            return nCupGameID;
        }
    }

    public class CGameInfo
    {
        private int nGameID;
        private string strFirstPlayerName;
        private string strSecondPlayerName;
        private int nFirstPlayerID;
        private int nSecondPlayerID;

        public string FirstPlayerName
        {
            get { return strFirstPlayerName; }
            set { strFirstPlayerName = value; }
        }

        public string SecondPlayerName
        {
            get { return strSecondPlayerName; }
            set { strSecondPlayerName = value; }
        }

        public int FirstPlayerID
        {
            get { return nFirstPlayerID; }
            set { nFirstPlayerID = value; }
        }

        public int SecondPlayerID
        {
            get { return nSecondPlayerID; }
            set { nSecondPlayerID = value; }
        }

        public int GameID
        {
            get { return nGameID; }
            set { nGameID = value; }
        }
    }

    public class CGameResultInfo
    {
        private int nGameResultID;
        private int nGameID;
        private string strFirstPlayerName;
        private string strSecondPlayerName;
        private int nFirstPlayerLostSet;
        private int nSecondPlayerLostSet;
        private int nFirstPlayerWonSet;
        private int nSecondPlayerWonSet;
        private string strWinnerName;
        private int nWinnerID;
        private int nFirstUserID;
        private int nSecondUserID;
        private int nSubmitterID;


        public int GameID
        {
            get { return nGameID; }
            set { nGameID = value; }
        }

        

        public int GameResultID
        {
            get { return nGameResultID; }
            set { nGameResultID = value; }
        }

     

        public string FirstPlayerName
        {
            get { return strFirstPlayerName; }
            set { strFirstPlayerName = value; }
        }

        public string SecondPlayerName
        {
            get { return strSecondPlayerName; }
            set { strSecondPlayerName = value; }
        }

        public int FirstPlayerLostSet
        {
            get { return nFirstPlayerLostSet; }
            set { nFirstPlayerLostSet = value; }
        }

        public int SecondPlayerLostSet
        {
            get { return nSecondPlayerLostSet; }
            set { nSecondPlayerLostSet = value; }
        }

        public int FirstPlayerWonSet
        {
            get { return nFirstPlayerWonSet; }
            set { nFirstPlayerWonSet = value; }
        }

        public int SecondPlayerWonSet
        {
            get { return nSecondPlayerWonSet; }
            set { nSecondPlayerWonSet = value; }
        }

        public string WinnerName
        {
            get { return strWinnerName; }
            set { strWinnerName = value; }
        }

        public int WinnerID
        {
            get { return nWinnerID; }
            set { nWinnerID = value; }
        }

        public int FirstUserID
        {
            get { return nFirstUserID; }
            set { nFirstUserID = value; }
        }

        public int SecondUserID
        {
            get { return nSecondUserID; }
            set { nSecondUserID = value; }
        }

        public int SubmitterID
        {
            get { return nSubmitterID; }
            set { nSubmitterID = value; }
        }
    }

    public class CUserGameInfo
    {
        int nGameID;
        int nGameResultID;
        int nUnverifiedGameResultID;
        string strTournamentName;
        string strGameName;
        int nNumResults, nNumUResults, nNumUResultsInvalid;

        public int GameID
        {
            get { return nGameID; }
            set { nGameID = value; }
        }

        public int NumResults
        {
            get { return nNumResults; }
            set { nNumResults = value; }
        }

        public int NumUnverifiedResults
        {
            get { return nNumUResults; }
            set { nNumUResults = value; }
        }

        public int NumUResultsInvalid
        {
            get { return nNumUResultsInvalid; }
            set { nNumUResultsInvalid = value; }
        }

        public int GameResultID
        {
            get { return nGameResultID; }
            set { nGameResultID = value; }
        }

        public int UnverifiedGameResultID
        {
            get { return nUnverifiedGameResultID; }
            set { nUnverifiedGameResultID = value; }
        }

        public string TournamentName
        {
            get { return strTournamentName; }
            set { strTournamentName = value; }
        }

        public string GameName
        {
            get { return strGameName; }
            set { strGameName = value; }
        }
    }
}