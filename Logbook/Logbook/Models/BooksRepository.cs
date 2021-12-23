using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.Models
{
    public static class BooksRepository
    {
        static string imagesPath = @"Images/";
        static LogbookDBEntities db;
        static BooksRepository()
        {
            db = new LogbookDBEntities();
        }

        public static bool ContainsBook(string bookName)
        {
            if (db.PrintedProducts.Where(p => p.Name == bookName).Count() > 0)
                return true;
            return false;
        }
        public static PrintedType GetPrintedType(string type)
        {
            return db.PrintedTypes.Where(p => p.Name == type).First();
        }
        public static List<PrintedType> GetPrintedTypes()
        {
            return db.PrintedTypes.ToList();
        }
        public static void TookBook(User user, PrintedProduct printedProduct, int days = 1)
        {
            printedProduct.IsAvaible = false;

            Record record = new Record()
            {
                UserID = user.ID,
                ProductID = printedProduct.ID,
                IsReturned = false,
                Date_Issue = DateTime.Now.Date,
                Date_Return = DateTime.Now.Date.AddDays(+days)
            };
            db.Records.Add(record);

            db.SaveChanges();
        }
        public static List<PrintedProduct> GetPrintedProducts(PrintedType printedType = null)
        {
            if(printedType != null)
            {
                return db.PrintedProducts.Where(p => p.TypeID == printedType.ID).ToList();
            }


            return db.PrintedProducts.ToList();
        }
        public static List<PrintedProduct> GetPrintedProductsByAvaiblity(bool avaible = true)
        {
            List<PrintedProduct> printedProducts = new List<PrintedProduct>();

            foreach(PrintedProduct printedProduct in db.PrintedProducts.Where(p => p.IsAvaible == avaible))
            {
                printedProducts.Add(printedProduct);
            }

            return printedProducts;
        }
        public static void ReturnBook(User user, PrintedProduct printedProduct)
        {
            Record record = db.Records.Where(p => p.UserID == user.ID && p.ProductID == printedProduct.ID).OrderByDescending(p => p.ID).First();
            record.IsReturned = true;

            printedProduct.IsAvaible = true;

            db.SaveChanges();
        }
        public static void AddBook(string name, PrintedType type, string author)
        {
            if (!ContainsBook(name))
            {
                PrintedProduct printedProduct = new PrintedProduct()
                {
                    Name = name,
                    Author = author,
                    IsAvaible = true,
                    TypeID = type.ID
                };

                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == true)
                {
                    openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                    string filepath = openFileDialog.FileName;

                    FileInfo file = new FileInfo(filepath);

                    string filename = file.Name;

                    file.CopyTo(imagesPath + filename, true);

                    printedProduct.Cover = imagesPath + filename;
                    db.PrintedProducts.Add(printedProduct);
                    db.SaveChanges();
                }
            }
        }
        public static PrintedType GetPrintedTypeByID(int id)
        {
            return db.PrintedTypes.Where(p => p.ID == id).First();
        }
        public static PrintedProduct GetPrintedProductByID(int id)
        {
            return db.PrintedProducts.Where(p => p.ID == id).First();
        }


        public static List<Record> GetAllLogbooks()
        {
            return db.Records.ToList();
        }
        public static List<Record> GetLogbooks(User owner)
        {
            return db.Records.Where(p => p.UserID == owner.ID).ToList();
        }
    }
}
