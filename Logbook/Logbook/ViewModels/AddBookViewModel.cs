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
    public class AddBookViewModel:INotifyPropertyChanged
    {
        public User user;
        public AddBookViewModel(User user)
        {
            this.user = user;

            Types = new ObservableCollection<string>(BooksRepository.GetPrintedTypes().Select(p => p.Name));
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

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private string author;
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }
        private string selectedType;
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }
        private ObservableCollection<string> types;
        public ObservableCollection<string> Types
        {
            get { return types; }
            set
            {
                types = value;
                OnPropertyChanged("Types");
            }
        }



        private RelayCommand addBookCommand;
        public RelayCommand AddBookCommand
        {
            get
            {
                return addBookCommand ??
                    (addBookCommand = new RelayCommand(obj =>
                    {
                        if(Name != null)
                        {
                            if(Author != null)
                            {
                                if(SelectedType != null)
                                {
                                    BooksRepository.AddBook(Name, BooksRepository.GetPrintedType(SelectedType), Author);

                                    SelectedType = null;
                                    Name = null;
                                    Author = null;
                                }
                            }
                        }
                    }));
            }
        }
    }
}
