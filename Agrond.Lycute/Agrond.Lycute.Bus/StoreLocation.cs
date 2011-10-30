using System;
using System.IO;
using System.Data.SqlServerCe;
using System.Configuration;
using Agrond.CopyFile;

namespace Agrond.Lycute.Bus
{
    public class StoreLocation
    {
        public static bool IsDBConfiged()
        {
            string strLocation = Agrond.Lycute.Bus.LycuteApplication.GetLocationString();
            if (strLocation != "") { Bus.LycuteOption._RootFolderDrection = strLocation;return true; }
            else {  return false; }
        }
        public void CheckDatabase(string pStrLibLocation) {
            string strDBFileName = "\\Library.sdf";
            //bool checkResult = false;
            if (Directory.Exists(pStrLibLocation))
            {
                if (!File.Exists(pStrLibLocation + "\\Library.sdf"))
                {
                    CreateDB(pStrLibLocation, "\\Library.sdf");
                }
            }
            else {
                Directory.CreateDirectory(pStrLibLocation);
                CreateDB(pStrLibLocation,strDBFileName);
            }
        }
        private void CreateDB(string pStrLocation,string pStrFileName) {
            Agrond.CopyFile.FileCopy CopyLib = new FileCopy();
            string applicationLocation = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            CopyLib.Copy(applicationLocation+"//Database//"+pStrFileName, pStrLocation+pStrFileName,"file");
        }
    }
}
