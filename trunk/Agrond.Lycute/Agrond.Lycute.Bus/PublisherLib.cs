using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Agrond.Lycute.DAO;

namespace Agrond.Lycute.Bus
{
    public class PublisherLib
    {
        /*Show all publisher*/
        public ObservableCollection<Publisher> ShowPublisher()
        {
            LibraryEntities mainDB = new LibraryEntities();
            var publishers = from publisher in mainDB.Publishers
                             select publisher;
            ObservableCollection<Publisher> _publisher = new ObservableCollection<Publisher>(publishers);
            return _publisher;
        }
        public Publisher Default() {
            Publisher pb = new Publisher() { pbl_ID = "0", pbl_Name = "Unknow" };
            return pb;
        }
        public static string ToString(ObservableCollection<Publisher> pPublishers)
        {
            string strPublisher = "";

            foreach (var p in pPublishers)
            {
                strPublisher = p.pbl_Name + ";" + strPublisher;
            }
            return strPublisher;
        }
        public Publisher GetCurrentPublisher(string pName)
        {
            LibraryEntities mainDB = new LibraryEntities();
            var publishers = (from publisher in mainDB.Publishers where publisher.pbl_Name == pName
                             select publisher).First();
            //ObservableCollection<Publisher> _publisher = new ObservableCollection<Publisher>(publishers);
            //return _publisher;
            return publishers;
        }
        public bool Exist(string pName) {
            LibraryEntities mainDB = new LibraryEntities();
            var publishers = (from publisher in mainDB.Publishers where publisher.pbl_Name == pName
                             select publisher).Count();
            if (publishers != 0)
                return true;
            else 
                return false;
        }
        public void Add(string pName) {
            LibraryEntities mainDB = new LibraryEntities();
            Publisher pb = new Publisher { pbl_ID = System.Guid.NewGuid().ToString(), pbl_Name = pName };
            mainDB.AddToPublishers(pb);
            mainDB.SaveChanges();
        }
        public static bool IsEdited(Publisher pOldPbl,Publisher pNewPbl) {
            if (pOldPbl.pbl_Name == pNewPbl.pbl_Name)
                return false;
            else
                return true;
        }
        public Publisher AddPublisher(Publisher newPbl, Publisher oldPbl)
        {
            if (oldPbl == null) {
                if (Exist(newPbl.pbl_Name))
                {
                    return GetCurrentPublisher(newPbl.pbl_Name);
                }
                else
                {
                    Add(newPbl.pbl_Name);
                    return GetCurrentPublisher(newPbl.pbl_Name);
                }
            }else
            if (IsEdited(oldPbl, newPbl))
            {
                if (Exist(newPbl.pbl_Name))
                {
                    return GetCurrentPublisher(newPbl.pbl_Name);
                }
                else
                {
                    Add(newPbl.pbl_Name);
                    return GetCurrentPublisher(newPbl.pbl_Name);
                }
            }
            else return null;
        }
    }
}
