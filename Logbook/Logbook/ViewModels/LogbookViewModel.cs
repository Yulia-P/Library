using Logbook.Commands;
using Logbook.Models;
using Logbook.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Logbook.ViewModels
{
    public class LogbookViewModel:INotifyPropertyChanged
    {
        public Record Logbook;
        public User User;
        public PrintedProduct PrintedProduct;

        public LogbookViewModel(Record record)
        {
            Logbook = record;
            User = UsersRepository.GetUserByID(record.UserID);
            PrintedProduct = BooksRepository.GetPrintedProductByID(record.ProductID);

            if (!record.IsReturned)
            {
                IsNotReturned = true;
            }
            else
            {
                IsNotReturned = false;
            }

            if(DateTime.Now.Date > record.Date_Return)
            {
                Outdated = true;
            }
            else
            {
                Outdated = false;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string UserName { get { return User.Name; } }
        public string BookName { get { return PrintedProduct.Name; } }
        public string BookAuthor { get { return PrintedProduct.Author; } }

        public string ReturnDate { get { return Logbook.Date_Return.ToLongDateString(); } }
        public string IssueDate { get { return Logbook.Date_Issue.ToLongDateString(); } }

        public BitmapSource Cover
        {
            get
            {
                if (PrintedProduct.Cover == null || PrintedProduct.Cover == "")
                {
                    return null;
                }
                return new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + PrintedProduct.Cover, UriKind.RelativeOrAbsolute));
            }
        }
        public string ID { get { return PrintedProduct.ID.ToString(); } }
        public string IsReturned 
        {
            get
            {
                if (Logbook.IsReturned)
                {
                    return "Сдано";
                }
                return "Не сдано";
            }
        }
        private bool isNotReturned;
        public bool IsNotReturned
        {
            get { return isNotReturned; }
            set
            {
                isNotReturned = value;
                OnPropertyChanged("IsNotReturned");
            }
        }
        private bool outdated;
        public bool Outdated
        {
            get { return outdated; }
            set
            {
                outdated = value;
                OnPropertyChanged("Outdated");
            }
        }

        private RelayCommand returnBookCommand;
        public RelayCommand ReturnBookCommand
        {
            get
            {
                return returnBookCommand ??
                    (returnBookCommand = new RelayCommand(obj =>
                    {
                        BooksRepository.ReturnBook(this.User, this.PrintedProduct);
                        IsNotReturned = false;
                    }));
            }
        }
    }
}
