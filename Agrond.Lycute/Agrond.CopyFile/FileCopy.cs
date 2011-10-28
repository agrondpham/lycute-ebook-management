﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Permissions;
using System.Security;
using System.Security.AccessControl;
using System.Net.Mime;
using System.Net;

namespace Agrond.CopyFile
{
    public class FileCopy
    {
        protected void CopyFile(string pStrSourceFile, string pStrTargetFile) {
            FileStream fsIn;
            FileStream fsOut;
            try
            {
                fsIn = new FileStream(pStrSourceFile, FileMode.Open, FileAccess.Read);
                fsOut = new FileStream(pStrTargetFile, FileMode.Create);
                fsIn.CopyTo(fsOut);
                fsIn.Close();
                fsOut.Close();
            }
            catch(Exception e) { 
                Console.WriteLine(e.Message);
            }
        }
        protected void DownloadImage(string pStrSourceFile, string pStrTargetFile) { 
            byte[] b;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(pStrSourceFile);
            WebResponse Response = Request.GetResponse();

            Stream stream = Response.GetResponseStream();
            using (BinaryReader br = new BinaryReader(stream))
            {
                b = br.ReadBytes(500000);
                br.Close();
            }
            Response.Close();

            FileStream fs = new FileStream(pStrTargetFile, FileMode.Create);
            BinaryWriter binaryw = new BinaryWriter(fs);
            try
            {
                binaryw.Write(b);
            }
            finally
            {
                fs.Close();
                binaryw.Close();
            }
        }
        protected bool CopyFolder(string pStrSourceFile, string pStrTargetFile)
        {
            FileStream fsIn;
            FileStream fsOut;
            try
            {
                string[] files= Directory.GetFiles(pStrSourceFile);
                foreach (string s in files)
                {
                    fsIn = new FileStream(s, FileMode.Open, FileAccess.Read);
                    string[] arrayFile=s.Split(new string[]{"\\"},StringSplitOptions.None);
                    fsOut = new FileStream(pStrTargetFile + "/" + arrayFile[arrayFile.Count()-1] , FileMode.Create);
                    fsIn.CopyTo(fsOut);
                    fsIn.Close();
                    fsOut.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        protected bool DeleteFile(string pStrDeleteFile) {
            System.IO.FileInfo fi = new System.IO.FileInfo(pStrDeleteFile);
            try
            {
                fi.Delete();
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        protected bool DeleteFolder(string pStrDeleteFolder) {
                try
                {
                    System.IO.Directory.Delete(pStrDeleteFolder, true);
                    return true;
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
        }
        //public function
        public void Copy(string pStrSourcePath, string pStrTargetPath,string pStrType)
        {
            if (pStrType.ToLower() == "file")
            {
                if (pStrSourcePath.StartsWith("http://"))
                    DownloadImage(pStrSourcePath, pStrTargetPath);
                else
                    CopyFile(pStrSourcePath, pStrTargetPath);
            }
            else CopyFolder(pStrSourcePath, pStrTargetPath);
        }
        public string Delete(string pStrDeleteFile,string pStrType) {
            if (pStrType == "file")
            {
                if (DeleteFile(pStrDeleteFile))
                    return "successful";
                else return "There is/are errors when copy file";
            }
            else
            {
                if (System.IO.Directory.Exists(pStrDeleteFile))
                {
                    if (DeleteFolder(pStrDeleteFile))
                        return "successful";
                    else return "There is/are errors when delete file";
                }
                else return "Folder is not existed";
            }
        }
    }
}
