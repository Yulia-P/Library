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
    public class PrintedProductViewModel:INotifyPropertyChanged
    {
        public PrintedProduct PrintedProduct;
        public PrintedType PrintedType;
        public User User;

        public PrintedProductViewModel(PrintedProduct printedProduct, User user)
        {
            PrintedProduct = printedProduct;
            PrintedType = BooksRepository.GetPrintedTypeByID(printedProduct.TypeID);
            User = user;

            AvaibleVisibility = printedProduct.IsAvaible;
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


        public string ID { get { return PrintedProduct.ID.ToString(); } }
        public string Name { get { return PrintedProduct.Name; } }
        public string Type { get { return PrintedType.Name; } }
        public string Author { get { return PrintedProduct.Author; } }
        public bool IsAvaible { 
            get 
            {
                if(User.UserType == 1) // Если админ
                {
                    return false;
                }
                return PrintedProduct.IsAvaible; 
            } 
        }
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
        private bool avaibleVisibility;
        public bool AvaibleVisibility
        { 
            get 
            { 
                return avaibleVisibility; 
            }
            set
            {
                avaibleVisibility = value;
                OnPropertyChanged("AvaibleVisibility");
            }
        }

        private RelayCommand takeBookCommand;
        public RelayCommand TakeBookCommand
        {
            get
            {
                return takeBookCommand ??
                    (takeBookCommand = new RelayCommand(obj =>
                    {
                        BooksRepository.TookBook(User, PrintedProduct);
                        AvaibleVisibility = false;
                    }));
            }
        }
    }
}
