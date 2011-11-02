using System.IO;
//using Agrond.Plus;

namespace Agrond.Plus
{
    public class StoreLocation
    {
        public static bool IsDBConfiged()
        {
            string strLocation = Option.LycuteApplication.GetLocationString();
            if (strLocation != "") { Option.LycuteOption._RootFolderDrection = strLocation;return true; }
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
            FileCopy CopyLib = new FileCopy();
            string applicationLocation = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            CopyLib.Copy(applicationLocation+"//Database//"+pStrFileName, pStrLocation+pStrFileName,"file");
        }
    }
}
