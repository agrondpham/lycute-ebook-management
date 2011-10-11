using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agrond.AmazonAPI;
//using Agrond.Lycute.Object;
using Agrond.Lycute.DAO;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Xml;
//using System.Linq;

namespace Agrond.Lycute.Bus
{
    public class BookLib
    {
        BookInfo bookinfo = new BookInfo();
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        private ObservableCollection<Author> _authors = new ObservableCollection<Author>();
        public ObservableCollection<Book> GetAuthor()
        {
            XmlDocument xdoc = bookinfo.GetCover(false, false, "keyword", "", "peterpan");
            XDocument xBookInfo;
            xBookInfo = XDocument.Load(new XmlNodeReader(xdoc));
            XNamespace ns = "http://webservices.amazon.com/AWSECommerceService/2011-04-01";
            var items = from item in xBookInfo.Descendants(ns + "ItemAttributes")
                        select
                            new Book()
                            {
                                //Author = item.Element(ns + "Author").Value,
                                bok_Title = item.Element(ns + "Title").Value,
                                //Manufacturer = item.Element(ns+"Manufacturer").Value
                                //Author = "def",
                                //Title = "abc",
                                //Manufacturer = "123"

                            };
            foreach (Book b in items)
            {
                _books.Add(b);
                //return b;
            }
            return _books;
        }
        //public void GetCover()
        //{
        //    XmlDocument xdoc = bookinfo.GetCover(true, false, "keyword", "", "peterpan");
        //    XDocument xBookInfo;
        //    xBookInfo = XDocument.Load(new XmlNodeReader(xdoc));
        //    XNamespace ns = "http://webservices.amazon.com/AWSECommerceService/2011-04-01";
        //    var items = from item in xBookInfo.Descendants(ns + "ItemAttributes")
        //                select
        //                    new Book()
        //                    {
        //                        //Author = item.Element(ns + "Author").Value,
        //                        Title = item.Element(ns + "Title").Value,
        //                        //Manufacturer = item.Element(ns+"Manufacturer").Value
        //                        //Author = "def",
        //                        //Title = "abc",
        //                        //Manufacturer = "123"

        //                    };
        //    foreach (Book b in items)
        //    {
        //        _books.Add(b);
        //    }
        //}
        //public void GetReview()
        //{
        //    XmlDocument xdoc = bookinfo.GetCover(false,true, "keyword", "", "peterpan");
        //    XDocument xBookInfo;
        //    xBookInfo = XDocument.Load(new XmlNodeReader(xdoc));
        //    XNamespace ns = "http://webservices.amazon.com/AWSECommerceService/2011-04-01";
        //    var items = from item in xBookInfo.Descendants(ns + "ItemAttributes")
        //                select
        //                    new Book()
        //                    {
        //                        //Author = item.Element(ns + "Author").Value,
        //                        Title = item.Element(ns + "Title").Value,
        //                        //Manufacturer = item.Element(ns+"Manufacturer").Value
        //                        //Author = "def",
        //                        //Title = "abc",
        //                        //Manufacturer = "123"

        //                    };
        //    foreach (Book b in items)
        //    {
        //        _books.Add(b);
        //    }
        //}
        /*Show all ebook*/
        public ObservableCollection<Book> ShowAll() { 
            LibraryEntities mainDB=new LibraryEntities();
            var ebooks = from ebook in mainDB.Books
                         select ebook;
                             //select new 
                         //{ 
                         //    bok_Title= ebook.bok_Title,
                         //    bok_ISBN=ebook.bok_ISBN,
                         //    bok_ImageURl=ebook.bok_ImageURl,
                         //    bok_Rank=ebook.bok_Rank,
                         //    bok_Year = ebook.bok_Year//,
                         //    //bok_Edition = ebook.bok_Year,
                         //    //bok_Volumn=ebook.bok_Year
                         //};
            _books.Clear();
            foreach( var b in ebooks)
            {
                _books.Add(new Book(){
                    bok_Title=b.bok_Title,
                    bok_ISBN = b.bok_ISBN,
                    bok_ImageURl = b.bok_ImageURl,
                    bok_Rank = Rank.RankImage(Convert.ToInt32(b.bok_Rank)),
                    bok_Year=b.bok_Year,
                    bok_Edition=b.bok_Edition,
                    bok_Location=b.bok_Location,
                    bok_Modified=b.bok_Modified,
                    bok_Volume=b.bok_Volume,
                    bok_ID=b.bok_ID
                });
            }
            return _books;
        }
        /*Show all information ebook*/
        public ObservableCollection<Author> ShowAuthorByBookID(int pId) { 
            LibraryEntities mainDB = new LibraryEntities();
            var authors = from ebook in mainDB.Books
                         from author in ebook.Authors where ebook.bok_ID==pId
                         select author;
                         //select author
            foreach (var a in authors)
            {
                _authors.Add(new Author()
                {
                    ath_ID=a.ath_ID,
                    ath_Name=a.ath_Name
                });

            }
            return _authors;
        }
        
        
        /*Show image*/
        //public ObservableCollection<Book>
    }
}
