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
using System.Data.Objects.DataClasses;
using System.IO;
//using System.Linq;

namespace Agrond.Lycute.Bus
{
    public class BookLib
    {
        BookInfo bookinfo = new BookInfo();
        CopyFile.FileCopy copy = new CopyFile.FileCopy();
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        //private ObservableCollection<Author> _authors = new ObservableCollection<Author>();
        public ObservableCollection<Book> GetInformation(string pKeyword, bool pIsISBN)
        {
            XmlDocument xdoc;
            if (pIsISBN)
            {
                xdoc = bookinfo.GetData(false, false, pKeyword, "");
            }
            else
            {
                xdoc = bookinfo.GetData(false, false, "", pKeyword);
            }
            XDocument xBookInfo;
            xBookInfo = XDocument.Load(new XmlNodeReader(xdoc));
            XNamespace ns = "http://webservices.amazon.com/AWSECommerceService/2011-04-01";
            var items = from item in xBookInfo.Descendants(ns + "Item")
                        from detail in item.Descendants(ns + "ItemAttributes")
                        select
                            new Book()
                            {
                                Authors = GetInternetAuthor(detail.Element(ns + "Author").Value),
                                bok_Title = detail.Element(ns + "Title").Value,
                                Publisher = GetInternetPulisher(detail.Element(ns + "Manufacturer").Value),
                                bok_ISBN = item.Element(ns + "ASIN").Value
                            };
            foreach (Book b in items)
            {
                _books.Add(b);
                //return b;
            }
            return _books;
        }
        public string GetInternetReview(string pISBN)
        {
            string review="";
            XmlDocument  xdoc = bookinfo.GetData(false, true, pISBN, "");
            XDocument xBookInfo;
            xBookInfo = XDocument.Load(new XmlNodeReader(xdoc));
            XNamespace ns = "http://webservices.amazon.com/AWSECommerceService/2011-04-01";
            var items = from item in xBookInfo.Descendants(ns + "EditorialReview")
                        select item.Element(ns + "Content").Value;
            foreach (string re in items) {
                review = re;
            }
            return review;
        }
        public string GetInternetCover(string pISBN)
        {
            string url = "";
            XmlDocument xdoc = bookinfo.GetData(true, false, pISBN, "");
            XDocument xBookInfo;
            xBookInfo = XDocument.Load(new XmlNodeReader(xdoc));
            XNamespace ns = "http://webservices.amazon.com/AWSECommerceService/2011-04-01";
            var items = from item in xBookInfo.Descendants(ns + "LargeImage")
                        select item.Element(ns + "URL").Value;
            foreach (string re in items)
            {
                url = re;
            }
            return url;
        }

        
        //public ObservableCollection<Book> GetAuthor(string pKeyword, bool pIsISBN)
        //{
        //    XmlDocument xdoc;
        //    if (pIsISBN)
        //    {
        //        xdoc = bookinfo.GetCover(false, false, pKeyword, "");
        //    }
        //    else
        //    {
        //        xdoc = bookinfo.GetCover(false, false, "", pKeyword);
        //    }
        //    //XDocument xBookInfo;
        //    //xBookInfo = XDocument.Load(new XmlNodeReader(xdoc));
        //    XmlNodeReader r = new XmlNodeReader(xdoc);
        //    while (r.Read())
        //    {
        //        if (r.NodeType == XmlNodeType.Element)
        //        {
        //            Console.WriteLine("<" + r.Name + ">");
        //            if (r.HasAttributes)
        //            {
        //                for (int i = 0; i < r.AttributeCount; i++)
        //                {
        //                    Console.WriteLine("\tATTRIBUTE: " +
        //                      r.GetAttribute(i));
        //                }
        //            }
        //        }
        //        else if (r.NodeType == XmlNodeType.Text)
        //        {
        //            Console.WriteLine("\tVALUE: " + r.Value);
        //        }
        //    }
        //    //foreach (Book b in items)
        //    //{
        //    //    _books.Add(b);
        //    //    //return b;
        //    //}
        //    return _books;
        //}
        private EntityCollection<Author> GetInternetAuthor(string pAuthor) {
            EntityCollection<Author> collectAu=new EntityCollection<Author>();
            Author au=new Author();
            au.ath_Name = pAuthor;
            collectAu.Add(au);
            return collectAu;
        }
        private Publisher GetInternetPulisher(string pPulisher)
        {
            Publisher pb = new Publisher();
            pb.pbl_Name = pPulisher;
            return pb;
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
        //public Book ShowBookDetail(int id)
        //{
        //    LibraryEntities mainDB = new LibraryEntities();
        //    var ebooks = from ebook in mainDB.Books
        //                 from author in ebook.Authors
        //                 from tag in ebook.Tags
        //                 join publ in mainDB.Publishers on ebook.pbl_ID equals publ.pbl_ID
        //                 join lang in mainDB.Languages on ebook.lng_ID equals lang.lng_ID
        //                 where ebook.bok_ID == id
        //                 select ebook;

        //    foreach (var b in ebooks)
        //    {
        //        b.bok_ImageURl = LycuteApplication.GetLocationString() + b.bok_ImageURl;
        //        b.bok_Rank = Rank.RankImage(Convert.ToInt32(b.bok_Rank));
        //        b.bok_Review = GetReview(LycuteApplication.GetLocationString() + b.bok_Review);

        //    }
        //    return ebooks.ElementAt(0);
        //}
        /*Search ebook*/
        //public ObservableCollection<Book> Search(string pKeywords)
        //{
        //    LibraryEntities mainDB = new LibraryEntities();
        //    var ebooks = from ebook in mainDB.Books
        //                 join publ in mainDB.Publishers on ebook.pbl_ID equals publ.pbl_ID
        //                 select ebook;
        //    if (pKeywords != null && pKeywords != "")
        //    {
        //        ebooks = from ebook in mainDB.Books
        //                 join publ in mainDB.Publishers on ebook.pbl_ID equals publ.pbl_ID
        //                 where ebook.bok_Title.Contains(pKeywords)
        //                 select ebook;
        //    }
        //    _books.Clear();
        //    foreach (var b in ebooks)
        //    {
        //        _books.Add(new Book()
        //        {
        //            bok_Title = b.bok_Title,
        //            bok_ISBN = b.bok_ISBN,
        //            bok_ImageURl = LycuteApplication.GetLocationString() + b.bok_ImageURl,
        //            bok_Rank = Rank.RankImage(Convert.ToInt32(b.bok_Rank)),
        //            bok_Year = b.bok_Year,
        //            bok_Edition = b.bok_Edition,
        //            bok_Location = b.bok_Location,
        //            bok_Modified = b.bok_Modified,
        //            bok_Volume = b.bok_Volume,
        //            bok_ID = b.bok_ID,
        //            bok_Review = GetReview(LycuteApplication.GetLocationString() + b.bok_Review),
        //            Publisher = b.Publisher
        //        });
        //    }
        //    return _books;
        //}
        public ObservableCollection<Book> Search(string pKeywords)
        {
            LibraryEntities mainDB = new LibraryEntities();
            var ebooks = from ebook in mainDB.Books
                         select ebook;
            if (pKeywords != null && pKeywords != "")
            {
                ebooks = from ebook in mainDB.Books
                         where ebook.bok_Title.Contains(pKeywords)
                         select ebook ;

            }
            ObservableCollection<Book> obserCollectionBook=new ObservableCollection<Book>(ebooks);

            foreach (var b in obserCollectionBook)
            {
                b.bok_ImageURl = LycuteApplication.GetLocationString() + b.bok_ImageURl;
                b.bok_Rank = Rank.RankImage(Convert.ToInt32(b.bok_Rank));
                b.bok_Review = GetReview(LycuteApplication.GetLocationString() + b.bok_Review);

            }
            return obserCollectionBook;
        }
        /*Edit ebook*/
        public void Edit(Book pEbook,string pAuthor,string pOldDirectory,string pReview)
        {
            string newDirectory=LycuteApplication.GetLocationString()+ NameCreater.CreateLocation(NameCreater.AuthorStringToList(pAuthor)[0], pEbook.bok_Title+"/");
            //booklib.EditReview(newDirectory+"review.xml", textRange.Text);
            if (!DirectoryIsExist(newDirectory))
            {
                Directory.CreateDirectory(newDirectory);
                //CopyFile.FileCopy copy = new CopyFile.FileCopy();
                copy.Copy(pOldDirectory, newDirectory,"folder");
                copy.Delete(pOldDirectory, "folder");
                //copy file from olddirec to new one
                //CopyFile.FileCopy
            
            }
            string strFileType="";
            string strImageURL="";
            if (pEbook.bok_ImageURl != "")
            {
                string[] arrayImageSourceURL = pEbook.bok_ImageURl.Split(new string[]{"///"},StringSplitOptions.None);
                string pImageSourceURL = arrayImageSourceURL[arrayImageSourceURL.Count()-1];
                string[] FileNameArray = pImageSourceURL.Split('.');
                strFileType = FileNameArray[FileNameArray.Count() - 1];
                strImageURL = LycuteApplication.GetLocationString() + NameCreater.CreateLocation(NameCreater.AuthorStringToList(pAuthor)[0], pEbook.bok_Title, "cover." + strFileType);
                //copy image
                
                copy.Copy(pImageSourceURL, strImageURL,"file");
            }
            LibraryEntities mainDB = new LibraryEntities();
            var ebooks = from ebook in mainDB.Books
                            where ebook.bok_ID == pEbook.bok_ID
                            select ebook;
            foreach (var b in ebooks)
            {
                b.bok_Edition = pEbook.bok_Edition;
                if (pEbook.bok_ImageURl != "")
                {
                    b.bok_ImageURl = NameCreater.CreateLocation(NameCreater.AuthorStringToList(pAuthor)[0], pEbook.bok_Title, "cover." + strFileType);
                }
                b.bok_ISBN = pEbook.bok_ISBN;
                b.bok_Location = pEbook.bok_Location;
                b.bok_Modified = pEbook.bok_Modified;
                b.bok_Rank = pEbook.bok_Rank;
                b.bok_Title = pEbook.bok_Title;
                b.bok_Volume = pEbook.bok_Volume;
                b.bok_Year = pEbook.bok_Year;
                b.bok_Review = NameCreater.CreateLocation(NameCreater.AuthorStringToList(pAuthor)[0], pEbook.bok_Title, "review.xml");
            }
            mainDB.SaveChanges();
            EditReview(newDirectory+"review.xml", pReview);
            
        }
        public bool DirectoryIsExist(string pNewDirectory) {
            if(Directory.Exists(pNewDirectory))
                return true;
            else return false;
        }
        public void EditReview(string pStrXMLURL,string pStrReview) {
            XmlTextWriter xmlWriter = new XmlTextWriter(pStrXMLURL,null);
            xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Review");
                    xmlWriter.WriteStartElement("Content");
                        xmlWriter.WriteString(pStrReview);  
                    xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
        public string GetReview(string pFileURL) {
            XmlTextReader xmlReader = new XmlTextReader(pFileURL);
            string strReview="";
            while (xmlReader.Read())
            {
                if(xmlReader.NodeType==XmlNodeType.Text)
                {
                        strReview= xmlReader.Value;
                }
            }
            return strReview;
        }
        /*Show all author*/
        public ObservableCollection<Author> ShowAuthor() {
            LibraryEntities mainDB = new LibraryEntities();
            var authors= from author in mainDB.Authors
                          select author;
            ObservableCollection<Author> _authors = new ObservableCollection<DAO.Author>(authors);    
            return _authors;
        }
        /*Show all publisher*/
        public ObservableCollection<Publisher> ShowPublisher()
        {
            
            LibraryEntities mainDB = new LibraryEntities();
            var publishers = from publisher in mainDB.Publishers
                          select publisher;
            ObservableCollection<Publisher> _publisher = new ObservableCollection<Publisher>(publishers);
            return _publisher;
        }
        public string ConvertAuthorObservableToString(ICollection<Author> pAuthors)
        {
            string strAuthor = "";

            foreach (var a in pAuthors)
            {
                strAuthor = a.ath_Name + ";" + strAuthor;
            }
            return strAuthor;
        }
        public string ConvertTagObservableToString(ICollection<Tag> pTags)
        {
            string strTarget = "";

            foreach (var t in pTags)
            {
                strTarget= t.tag_Name + ";" + strTarget;
            }
            return strTarget;
        }
        public string ConvertPublisherObservableToString(ObservableCollection<Publisher> pPublishers)
        {
            string strPublisher = "";

            foreach (var p in pPublishers)
            {
                strPublisher = p.pbl_Name + ";" + strPublisher;
            }
            return strPublisher;
        }
        /*Edit author*/
        //public void EditAuthorByBookID(int pId)
        //{
        //    _authors.Clear();
        //    LibraryEntities mainDB = new LibraryEntities();
        //    var authors = from ebook in mainDB.Books
        //                  from author in ebook.Authors
        //                  where ebook.bok_ID == pId
        //                  select author;
        //    //select author
        //    foreach (var a in authors)
        //    {
        //        _authors.Add(new Author()
        //        {
        //            ath_ID = a.ath_ID,
        //            ath_Name = a.ath_Name
        //        });

        //    }
        //    return _authors;
        //}
        /*Show publisher*/

    }
}
