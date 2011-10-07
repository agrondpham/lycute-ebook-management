using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agrond.Lycute.DAO;

namespace Agrond.Lycute.Bus
{
    public class Application
    {   
        /// <summary>
        /// Get location of library from database
        /// </summary>
        /// <returns>String location</returns>
        public static string GetLocationString() { 
            LycuteEntities mainDB=new LycuteEntities();
            string strLocation = "";
            var strLocations = from location in mainDB.Configurations where location.Type == "Location" select location.Value;
            foreach (string strLoca in strLocations) {
                strLocation = strLoca;
            }
            return strLocation;
        }
    }
}
