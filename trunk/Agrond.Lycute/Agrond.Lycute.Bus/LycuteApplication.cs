using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agrond.Lycute.DAO;

namespace Agrond.Lycute.Bus
{
    public class LycuteApplication
    {   
        /// <summary>
        /// Get location of library from database
        /// </summary>
        /// <returns>String location</returns>
        /// 
        static LycuteEntities _mainDB = new LycuteEntities();
        public static string GetLocationString() { 
            //LycuteEntities mainDB=new LycuteEntities();
            string strLocation = "";
            var strLocations = from location in _mainDB.Configurations where location.Type == "Location" select location.Value;
            foreach (string strLoca in strLocations) {
                strLocation = strLoca;
            }
            return strLocation;
        }
        public static string GetStructureFolderString()
        {         
            string strStructure = "";
            var strStructures = from location in _mainDB.Configurations where location.Type == "Structure" select location.Value;
            foreach (string strStru in strStructures)
            {
                strStructure = strStru;
            }
            return strStructure;
        }
    }
}
