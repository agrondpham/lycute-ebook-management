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
using System.Data.Objects;
//using System.Linq;

namespace Agrond.Lycute.Bus
{
    public class BookLib
    {
        BookInfo bookinfo = new BookInfo();
        PublisherLib publisherLib = new PublisherLib();
        AuthorLib authorLib = new AuthorLib();
        CopyFile.FileCopy copy = new CopyFile.FileCopy();
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        
        /*get data from internet*/
        public ObservableCollection<Book> GetInformation(string pKeyword, bool pIsISBN)
        {
            //try
            //{
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
            //}
            //catch { 
                
            //}
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
        public string GetReview(string pFileURL) {
            try
            {
                XmlTextReader xmlReader = new XmlTextReader(pFileURL);
                string strReview = "";
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Text)
                    {
                        strReview = xmlReader.Value;
                    }
                }
                return strReview;
            }
            catch (Exception e) {
                return "Can not load Review";
            }
        }
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
        

        /*Show ebook data from database*/
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
                string strAuthors = AuthorLib.ToString(b.Authors);
                string strFirstAuthor = ConvertData.ToList(strAuthors)[0];
                b.bok_ImageURl = LycuteApplication.GetLocationString() + "\\"+NameCreater.CreateLocation(strFirstAuthor,NameCreater.CreateName(b.bok_Title))+"\\"+b.bok_ImageURl;
                b.bok_Rank = Rank.RankImage(Convert.ToInt32(b.bok_Rank));
                b.bok_Review = GetReview(LycuteApplication.GetLocationString() + "\\" + NameCreater.CreateLocation(strFirstAuthor, NameCreater.CreateName(b.bok_Title)) + "\\" + b.bok_Review);

            }
            return obserCollectionBook;
        }
        /*Edit ebook*/
        public void Edit(Book pEbook,string pAuthor,string pOldDirectory,string pReview,string pOldEbookFile)
        {
            string strBookName="";
            //error to create folder with voulume
            //if(pEbook.bok_Volume==1){
                strBookName = NameCreater.CreateName(pEbook.bok_Title);
            //}
            //else
            //{
            //    strBookName = NameCreater.CreateName(pEbook.bok_Title,pEbook.bok_Volume.ToString());
            //}
            string shortDirectory=NameCreater.CreateLocation(ConvertData.ToList(pAuthor)[0], strBookName);
            string newDirectory = LycuteApplication.GetLocationString() +"\\"+ shortDirectory;
            
            //check new directory is availble
            if (pOldDirectory != newDirectory)
            {
                //if (!copy.DirectoryIsExist(newDirectory))
                //{
                    Directory.CreateDirectory(newDirectory);
                    copy.Copy(pOldDirectory, newDirectory,pOldEbookFile,pEbook.bok_Title);
                    copy.Delete(pOldDirectory, "folder");
                //}
            }
            //Copy image
            string strFileType="";
            string strImageURL="";

            if (pEbook.bok_ImageURl != "" && pEbook.bok_ImageURl != null)
            {
                string[] arrayImageSourceURL = pEbook.bok_ImageURl.Split(new string[]{"///"},StringSplitOptions.None);
                string pImageSourceURL = arrayImageSourceURL[arrayImageSourceURL.Count() - 1].Replace("/","\\");
                
                strFileType = NameCreater.GetFileType(pImageSourceURL);
                strImageURL = NameCreater.FileLocation(newDirectory,"cover."+strFileType);
                //copy image
                if(pImageSourceURL!=strImageURL)
                    copy.Copy(pImageSourceURL, strImageURL,"file");
            }
            //Edit book
            LibraryEntities mainDB = new LibraryEntities();
            var ebooks = (from ebook in mainDB.Books
                            where ebook.bok_ID == pEbook.bok_ID
                            select ebook).First();
          
            ebooks.bok_Edition = pEbook.bok_Edition;
            ebooks.bok_ISBN = pEbook.bok_ISBN;
            ebooks.bok_Modified = pEbook.bok_Modified;
            ebooks.bok_Rank = pEbook.bok_Rank;
            ebooks.bok_Title = pEbook.bok_Title;
            ebooks.bok_Volume = pEbook.bok_Volume;
            ebooks.bok_Year = pEbook.bok_Year;
            ebooks.bok_Location = pEbook.bok_Title + "." + NameCreater.GetFileType(pOldEbookFile);
            //add publisher if it is not exist and add publisher to EBook
            Publisher pbl = publisherLib.AddPublisher(pEbook.Publisher, ebooks.Publisher);
            if(pbl!=null)
                ebooks.pbl_ID = pbl.pbl_ID;
            //add author
            List<string> auNames = authorLib.AddAuthor(pEbook.Authors, ebooks.Authors, ebooks.bok_ID);

            foreach (string name in auNames)
            {
                var aut = (from author in mainDB.Authors
                           where author.ath_Name == name
                           select author).First();
                aut.Books.Add(ebooks);
                //mainDB.AddToAuthors(au);
            }        
            //save to database
            mainDB.SaveChanges();
            //edit review
            EditReview(newDirectory+"\\review.xml", pReview);
            
        }
        public void Add(Book pEbook, string pAuthor, string pReview,string pEbookSource,string pOldEbookFile)
        {
            string strBookName = "";
            strBookName = NameCreater.CreateName(pEbook.bok_Title);
            string shortDirectory = NameCreater.CreateLocation(ConvertData.ToList(pAuthor)[0], strBookName);
            string newDirectory = LycuteApplication.GetLocationString() + "\\" + shortDirectory;

            //check new directory is availble
            Directory.CreateDirectory(newDirectory);
            
            //Copy image
            string strFileType = "";
            string strImageURL = "";

            if (pEbook.bok_ImageURl != "" && pEbook.bok_ImageURl != null)
            {
                string[] arrayImageSourceURL = pEbook.bok_ImageURl.Split(new string[] { "///" }, StringSplitOptions.None);
                string pImageSourceURL = arrayImageSourceURL[arrayImageSourceURL.Count() - 1].Replace("/", "\\");

                strFileType = NameCreater.GetFileType(pImageSourceURL);
                strImageURL = NameCreater.FileLocation(newDirectory, "cover." + strFileType);
                //copy image
                if (pImageSourceURL != strImageURL)
                    copy.Copy(pImageSourceURL, strImageURL, "file");
            }
            //copy file
            copy.Copy(pEbookSource, NameCreater.FileLocation(newDirectory, pEbook.bok_Title + "." + NameCreater.GetFileType(pOldEbookFile)), "file");
            //edit review
            EditReview(newDirectory + "\\review.xml", pReview);
            //Save data
            using (LibraryEntities mainDB = new LibraryEntities())
            {
                Book b = new Book();
                b.bok_Edition = pEbook.bok_Edition;
                if (strFileType != "")
                    b.bok_ImageURl = "cover." + strFileType;
                b.bok_ISBN = pEbook.bok_ISBN;
                b.bok_ID = System.Guid.NewGuid().ToString();
                b.bok_Modified = pEbook.bok_Modified;
                b.bok_Rank = pEbook.bok_Rank;
                b.bok_Title = pEbook.bok_Title;
                b.bok_Volume = pEbook.bok_Volume;
                b.bok_Year = pEbook.bok_Year;
                b.bok_Location = pEbook.bok_Title + "." + NameCreater.GetFileType(pOldEbookFile);
                b.lng_ID = "0";
                b.bok_Review = "review.xml";

                Publisher pbl = publisherLib.AddPublisher(pEbook.Publisher, b.Publisher);
                b.pbl_ID = pbl.pbl_ID;


                List<string> auNames = authorLib.AddAuthor(pEbook.Authors, null, "");

                foreach (string name in auNames)
                {
                    var aut = (from author in mainDB.Authors
                               where author.ath_Name == name
                               select author).First();
                    aut.Books.Add(b);
                    //mainDB.AddToAuthors(au);
                } 

                mainDB.AddToBooks(b);
                mainDB.SaveChanges();
            }

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

        
        
    }
}
