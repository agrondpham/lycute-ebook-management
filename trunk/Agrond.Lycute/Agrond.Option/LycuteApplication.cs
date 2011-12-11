using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agrond.DataAccess;

namespace Agrond.Option
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
            try
            {
                LycuteEntities _mainDB = new LycuteEntities();
                string strLocation = (from location in _mainDB.Configurations where location.Type == "Location" select location.Value).First();
                return strLocation;
            }
            catch (Exception e) { return ""; }
        }
        public static string GetStructureFolderString()
        {         
            string strStructure = "";
            LycuteEntities _mainDB = new LycuteEntities();
            var strStructures = (from location in _mainDB.Configurations where location.Type == "Structure" select location.Value).First();
            strStructure = strStructures;
            //foreach (string strStru in strStructures)
            //{
            //    strStructure = strStru;
            //}
            return strStructure;
        }
        public static void SetLocation(string pStrLocation) {
            //string strLocation = "";
            LycuteEntities _mainDB = new LycuteEntities();
            var Locations = (from location in _mainDB.Configurations where location.Type == "Location" select location).First();
            Locations.Value = pStrLocation;
            //foreach (var Location in Locations)
            //{
            //    Location.Value=pStrLocation;
            //}
            _mainDB.SaveChanges();
            //convert old store to new store
            //old store link
            //new store link
        }
    }
}
