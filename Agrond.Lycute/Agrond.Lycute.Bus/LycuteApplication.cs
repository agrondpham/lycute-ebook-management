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
        //static LycuteEntities _mainDB = new LycuteEntities();
        public static string GetLocationString() { 
            LycuteEntities _mainDB=new LycuteEntities();
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
            LycuteEntities _mainDB = new LycuteEntities();
            var strStructures = from location in _mainDB.Configurations where location.Type == "Structure" select location.Value;
            foreach (string strStru in strStructures)
            {
                strStructure = strStru;
            }
            return strStructure;
        }
        public static void SetLocation(string pStrLocation) {
            //string strLocation = "";
            LycuteEntities _mainDB = new LycuteEntities();
            var Locations = from location in _mainDB.Configurations where location.Type == "Location" select location;
            foreach (var Location in Locations)
            {
                Location.Value=pStrLocation;
            }
            _mainDB.SaveChanges();
            //convert old store to new store
            //old store link
            //new store link
        }
    }
}
