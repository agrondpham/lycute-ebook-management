using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Agrond.DataAccess;
using System.Collections.ObjectModel;

namespace Agrond.ObjectLib
{
    public class AuthorLib
    {
        /*Show all author*/
        public ObservableCollection<Author> ShowAuthor()
        {
            LibraryEntities mainDB = new LibraryEntities();
            var authors = from author in mainDB.Authors
                          select author;
            ObservableCollection<Author> _authors = new ObservableCollection<Author>(authors);
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
    }
}
