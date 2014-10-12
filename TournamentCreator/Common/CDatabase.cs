using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentCreator.Common
{
    public class CDatabase
    {
        public CDatabase()
        {

        }

        public string GetConnectionString()
        {
            return "server=localhost;uid=root;database=TournamentCreator;";
        }
    }
}