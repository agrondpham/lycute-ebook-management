using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agrond.Lycute.DAO;
using System.Collections.ObjectModel;

namespace Agrond.Lycute.Bus
{
    public class AuthorLib
    {
        /*Show all author*/
        public ObservableCollection<Author> ShowAuthor()
        {
            LibraryEntities mainDB = new LibraryEntities();
            var authors = from author in mainDB.Authors
                          select author;
            ObservableCollection<Author> _authors = new ObservableCollection<DAO.Author>(authors);
            return _authors;
        }
        public Author Default()
        {
            Author au = new Author();
            au.ath_ID = "0";
            au.ath_Name = "Unknow";
            return au;
        }
        public static string ToString(ICollection<Author> pAuthors)
        {
            string strAuthor = "";

            foreach (var a in pAuthors)
            {
                if(strAuthor=="")
                    strAuthor = a.ath_Name;
                else
                    strAuthor = strAuthor+";"+a.ath_Name;
            }
            return strAuthor;
        }
        //public static ICollection<Author> ToString(string pStrAuthors)
        //{
            
        //    string[] arrayStrAuthors = pStrAuthors.Split(';');
        //    ICollection<Author> au=new ICollection<Author>();
        //    foreach (var a in arrayStrAuthors)
        //    {
        //        au.Add(new Author { ath_Name = a });
        //    }
        //    return au;
        //}
        public List<string> AddAuthor(ICollection<Author> newPbl, ICollection<Author> oldPbl,string ebookID)
        {
            if (oldPbl == null || ebookID == "")
            {
                List<string> lstAuthorName = new List<string>();
                foreach (Author au in newPbl)
                {
                    if (!Exist(au.ath_Name))
                    {
                        Add(au.ath_Name);
                    }
                    lstAuthorName.Add(au.ath_Name);
                }
                return lstAuthorName;
            }
            else
            {
                List<string> lstAuthorName = new List<string>();
                List<string> lstAuthorRemove = new List<string>();// =  ;

                foreach (Author newauthor in newPbl)
                {
                    foreach (Author oldauthor in oldPbl)
                    {
                        if (oldauthor.ath_Name == newauthor.ath_Name)
                        {
                            //newPbl.Remove(newauthor);
                            oldPbl.Remove(oldauthor);
                            break;
                        }
                    }
                }
                //remove all relation ship
                foreach (Author au in oldPbl)
                {
                    Remove(au.ath_Name, ebookID);
                }
                //add new relation ship
                foreach (Author au in newPbl)
                {
                    if (!Exist(au.ath_Name))
                    {
                        Add(au.ath_Name);
                    }
                    lstAuthorName.Add(au.ath_Name);
                }
                return lstAuthorName;
            }
        }
        public Author GetCurrentAuthor(string pName)
        {
            LibraryEntities mainDB = new LibraryEntities();
            var authors = (from author in mainDB.Authors
                              where author.ath_Name == pName
                              select author).First();
            return authors;
        }
        public bool Exist(string pName)
        {
            LibraryEntities mainDB = new LibraryEntities();
            var authors = (from author in mainDB.Authors
                              where author.ath_Name == pName
                              select author).Count();
            if (authors != 0)
                return true;
            else
                return false;
        }
        public void Remove(string pName,string bookID) {
            LibraryEntities mainDB = new LibraryEntities();
            var au = (from author in mainDB.Authors
                        where author.ath_Name == pName
                        select author).First();
            var book=(from ebook in mainDB.Books
                         where ebook.bok_ID==bookID
                         select ebook).First() ;
            au.Books.Remove(book);
            mainDB.SaveChanges();
        }
        public void Add(string pName)
        {
            LibraryEntities mainDB = new LibraryEntities();
            Author au = new Author { ath_ID = System.Guid.NewGuid().ToString(), ath_Name = pName };
            mainDB.AddToAuthors(au);
            mainDB.SaveChanges();
        }
        //public static bool IsEdited(ICollection<Author> pOldPbl, ICollection<Author> pNewPbl)
        //{
        //    foreach (Author oldauthor in pOldPbl)
        //    {
        //        foreach (Author newauthor in pNewPbl) {
        //            if (oldauthor.ath_Name == newauthor.ath_Name)
        //            {
        //                pNewPbl.Remove(newauthor);
        //                pOldPbl.Remove(oldauthor);
        //                break;
        //            }
        //        }
        //    }
        //}
    }
}
