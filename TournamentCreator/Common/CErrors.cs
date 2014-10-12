using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentCreator.Common
{
    public class CErrors
    {
        public string GetErrorCodeText(int nErrorCode)
        {
            string strErrorText = "";
            try
            {
                switch (nErrorCode)
                {
                    case 10:
                        strErrorText = "Could not connect to database.";
                    break;

                    case 20:
                        strErrorText = "Some required fields are missing.";
                    break;

                    case 2:
                        strErrorText = "Access to the page is denied.";
                    break;

                    case 3:
                    break;

                    case 4:
                    break;

                    case 5:
                    break;

                    case 6:
                    break;
                    
                }
            }
            catch (Exception ex)
            {
            }
            return strErrorText;
        }

        public string GetMessageText(int nErrorCode)
        {
            string strErrorText = "";
            try
            {
                switch (nErrorCode)
                {
                    case 0:
                        strErrorText = "Could not connect to database.";
                        break;

                    case 1:
                        strErrorText = "Some required fields are missing.";
                        break;

                    case 2:
                        strErrorText = "Access to the page is denied.";
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        break;

                    case 6:
                        break;

                }
            }
            catch (Exception ex)
            {
            }
            return strErrorText;
        }
    }
}