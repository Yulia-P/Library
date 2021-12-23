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
    public class AllBooksViewModel : INotifyPropertyChanged
    {
        public User user;
        public AllBooksViewModel(User user)
        {
            this.user = user;

            avaibleBooks = BooksRepository.GetPrintedProductsByAvaiblity(true).OrderBy(p => p.Name).ToList();
            unavaibleBooks = BooksRepository.GetPrintedProductsByAvaiblity(false).OrderBy(p => p.Name).ToList();
            allBooks = new List<PrintedProduct>();
            allBooks.AddRange(avaibleBooks);
            allBooks.AddRange(unavaibleBooks);

            Books = new ObservableCollection<PrintedProductViewModel>(allBooks.Select(p => new PrintedProductViewModel(p,user)));
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

        public List<PrintedProduct> allBooks;
        public List<PrintedProduct> avaibleBooks;
        public List<PrintedProduct> unavaibleBooks;

        private ObservableCollection<PrintedProductViewModel> books;
        public ObservableCollection<PrintedProductViewModel> Books
        {
            get { return books; }
            set
            {
                books = value;
                OnPropertyChanged("Books");
            }
        }

        private string search;
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged("Search");
            }
        }
        private RelayCommand findCommand;
        public RelayCommand FindCommand
        {
            get
            {
                return findCommand ??
                    (findCommand = new RelayCommand(obj =>
                    {
                        if (Search != null)
                        {
                            DoSearch();
                        }
                        else
                        {
                            Books = new ObservableCollection<PrintedProductViewModel>(allBooks.OrderBy(p => p.Name).Select(p => new PrintedProductViewModel(p,user)));
                        }
                    }));
            }
        }

        public void DoSearch()
        {
            Books = new ObservableCollection<PrintedProductViewModel>(allBooks.Where(p => p.Name.Contains(Search)).OrderBy(p => p.Name).Select(p => new PrintedProductViewModel(p, user)));
        }
    }
}
