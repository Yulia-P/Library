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
    public class UserLogbookViewModel : INotifyPropertyChanged
    {
        public User user;
        public UserLogbookViewModel(User user)
        {
            this.user = user;

            this.user = user;
            Logbooks = new ObservableCollection<LogbookViewModel>(BooksRepository.GetLogbooks(user).Select(p => new LogbookViewModel(p)));
            Logbooks = new ObservableCollection<LogbookViewModel>(Logbooks.OrderBy(p => p.UserName));

            allLogbooks = BooksRepository.GetAllLogbooks().Select(p => new LogbookViewModel(p)).ToList();
        }

        private List<LogbookViewModel> allLogbooks;
        private List<LogbookViewModel> currentLogbooks;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<LogbookViewModel> logbooks;
        public ObservableCollection<LogbookViewModel> Logbooks
        {
            get { return logbooks; }
            set
            {
                logbooks = value;
                OnPropertyChanged("Logbooks");
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
                            currentLogbooks = allLogbooks.OrderBy(p => p.User.Name).ToList();
                            Logbooks = new ObservableCollection<LogbookViewModel>(currentLogbooks);
                        }
                    }));
            }
        }

        public void DoSearch()
        {
            currentLogbooks = allLogbooks.Where(p => p.User.Name.Contains(Search)).OrderBy(p => p.User.Name).ToList();
            Logbooks = new ObservableCollection<LogbookViewModel>(currentLogbooks);
        }
    }
}

