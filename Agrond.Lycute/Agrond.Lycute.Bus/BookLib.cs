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
                         select new 
                         { 
                             bok_Title= ebook.bok_Title,
                             bok_ISBN=ebook.bok_ISBN,
                             bok_ImageURl=ebook.bok_ImageURl,
                             bok_Rank=ebook.bok_Rank,
                             bok_Year = ebook.bok_Year//,
                             //bok_Edition = ebook.bok_Year,
                             //bok_Volumn=ebook.bok_Year
                         };
            _books.Clear();
            foreach( var b in ebooks)
            {
                _books.Add(new Book(){
                    bok_Title=b.bok_Title,
                    bok_ISBN = b.bok_ISBN,
                    bok_ImageURl = b.bok_ImageURl,
                    bok_Rank = Rank.RankImage(Convert.ToInt32(b.bok_Rank)),
                    bok_Year=b.bok_Year
                });
            }
            return _books;
        }
        /*Show image*/
        //public ObservableCollection<Book>
    }
}
