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
    public class SideMenuViewModel : INotifyPropertyChanged
    {
        public User user;
        public SideMenuViewModel(User user)
        {
            this.user = user;
            if(user.UserType == 1)
            {
                roleString = "Администратор";
                AdminVisibility = true;
                UserVisibility = false;



            }
            else
            {
                roleString = "Читатель";
                AdminVisibility = false;
                UserVisibility = true;
            }


            int period = 7; //Период в днях
            UniqueUsersCount = UsersRepository.GetUniqueVisits(period).Count.ToString();
            UsersCount = UsersRepository.GetVisits(period).Count.ToString();
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

        

        private bool adminVisibility;
        public bool AdminVisibility
        {
            get { return adminVisibility; }
            set
            {
                adminVisibility = value;
                OnPropertyChanged("AdminVisibility");
            }
        }
        private bool userVibisibility;
        public bool UserVisibility
        {
            get { return userVibisibility; }
            set
            {
                userVibisibility = value;
                OnPropertyChanged("UserVisibility");
            }
        }


        private string roleString;
        public string RoleString
        {
            get { return roleString; }
            set
            {
                roleString = value;
                OnPropertyChanged("RoleString");
            }
        }
        public string Name { get { return user.Name; } }

        private RelayCommand exitCommand;
        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ??
                    (exitCommand = new RelayCommand(obj =>
                    {
                        MainWindow mainWindow = App.Current.MainWindow as MainWindow;

                        LoginView view = new LoginView();
                        view.DataContext = new LoginViewModel();

                        mainWindow.OutputView.Content = view;
                    }));
            }
        }

        private RelayCommand allBooksCommand;
        public RelayCommand AllBooksCommand
        {
            get
            {
                return allBooksCommand ??
                    (allBooksCommand = new RelayCommand(obj =>
                    {
                        MainWindow mainWindow = App.Current.MainWindow as MainWindow;

                        SideMenuView sideMenuView = new SideMenuView();
                        sideMenuView.DataContext = new SideMenuViewModel(user);

                        mainWindow.OutputView.Content = sideMenuView;

                        AllBooksView logbooksView = new AllBooksView();
                        logbooksView.DataContext = new AllBooksViewModel(user);

                        sideMenuView.OutputView.Content = logbooksView;
                    }));
            }
        }



        /*   АДМИНСКИЕ КОМАНДЫ    */

        private string usersCount;
        public string UsersCount
        {
            get { return usersCount; }
            set
            {
                usersCount = value;
                OnPropertyChanged("UsersCount");
            }
        }
        private string uniqueUsersCount;
        public string UniqueUsersCount
        {
            get { return uniqueUsersCount; }
            set
            {
                uniqueUsersCount = value;
                OnPropertyChanged("UniqueUsersCount");
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
                        MainWindow mainWindow = App.Current.MainWindow as MainWindow;

                        SideMenuView sideMenuView = new SideMenuView();
                        sideMenuView.DataContext = new SideMenuViewModel(user);

                        mainWindow.OutputView.Content = sideMenuView;

                        AddBookView logbooksView = new AddBookView();
                        logbooksView.DataContext = new AddBookViewModel(user);

                        sideMenuView.OutputView.Content = logbooksView;
                    }));
            }
        }


        private RelayCommand logbooksCommand;
        public RelayCommand LogbooksCommand
        {
            get
            {
                return logbooksCommand ??
                    (logbooksCommand = new RelayCommand(obj =>
                    {
                        MainWindow mainWindow = App.Current.MainWindow as MainWindow;

                        SideMenuView sideMenuView = new SideMenuView();
                        sideMenuView.DataContext = new SideMenuViewModel(user);

                        mainWindow.OutputView.Content = sideMenuView;

                        LogbooksView logbooksView = new LogbooksView();
                        logbooksView.DataContext = new LogbooksViewModel(user);

                        sideMenuView.OutputView.Content = logbooksView;
                    }));
            }
        }




        /*  ЮЗВЕРСКИЕ КОМАНДЫ   */

        private RelayCommand userLogbooksCommand;
        public RelayCommand UserLogbooksCommand
        {
            get
            {
                return userLogbooksCommand ??
                    (userLogbooksCommand = new RelayCommand(obj =>
                    {
                        MainWindow mainWindow = App.Current.MainWindow as MainWindow;

                        SideMenuView sideMenuView = new SideMenuView();
                        sideMenuView.DataContext = new SideMenuViewModel(user);

                        mainWindow.OutputView.Content = sideMenuView;

                        UserLogbookView logbooksView = new UserLogbookView();
                        logbooksView.DataContext = new UserLogbookViewModel(user);

                        sideMenuView.OutputView.Content = logbooksView;
                    }));
            }
        }
    }
}