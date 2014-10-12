using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace TournamentCreator.Common
{
    public class CGlobalAttr
    {
        public bool SetGlobalAttr(string strAttrName, string strAttrVal)
        {
            bool bAttrExists = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                // if the attribute already exists change its value
                if (IsGlobalAttrExist(strAttrName))
                    cmd.CommandText = "UPDATE globalattrs SET AttrVal ='" + strAttrVal+ "'  WHERE AttrKey = '" + strAttrName + "'";
                else
                    cmd.CommandText = "INSERT INTO globalattrs(AttrKey, AttrVal) VALUE('" + strAttrName + "','" + strAttrVal + "')";
                cmd.Connection = oConn;

                int nRet = cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (oConn != null)
                    oConn.Close();
            }

            return bAttrExists;
        }

        public bool IsGlobalAttrExist(string strAttrName)
        {
            bool bAttrExists = false;
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT GlobalAttrID FROM globalattrs WHERE AttrKey = '" + strAttrName + "'";
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    bAttrExists = true;
                  
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

            return bAttrExists;
        }

        public string GetGlobalAttr(string strAttrName)
        {
            string strAttrVal = "";
            MySqlConnection oConn = null;
            try
            {
                CDatabase oDB = new CDatabase();
                oConn = new MySqlConnection(oDB.GetConnectionString());
                oConn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT AttrVal FROM globalattrs WHERE AttrKey = '" + strAttrName + "'";
                cmd.Connection = oConn;

                MySqlDataReader oReader = cmd.ExecuteReader();
                if (oReader.Read())
                {
                    oReader.Close();
                    strAttrVal = oReader["AttrVal"].ToString();

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

            return bAttrExists;
        }
    }
}