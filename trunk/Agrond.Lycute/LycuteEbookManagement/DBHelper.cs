using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agrond.Lycute.Bus;
using System.Configuration;

namespace LycuteEbookManagement
{
    public class DBHelper
    {
        public static void ConfigDatabase() {
            string strLocation = LycuteApplication.GetLocationString();
            if (strLocation != "" && strLocation != null)
            {
                string strConnection = "metadata=res://*/LibraryModel.csdl|res://*/LibraryModel.ssdl|res://*/LibraryModel.msl;provider=System.Data.SqlServerCe.3.5;provider connection string=\"Data Source=" + strLocation + "Library.sdf\";";
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                
                config.ConnectionStrings.ConnectionStrings["LibraryEntities"].ConnectionString=strConnection;;
                config.Save(ConfigurationSaveMode.Modified, true);

                // Now edit the in-memory values to match
                //Properties.Settings.Default["LibraryEntities"] = strConnection;

                
                ConfigurationManager.RefreshSection("connectionStrings");
            }
       }
    }
}
