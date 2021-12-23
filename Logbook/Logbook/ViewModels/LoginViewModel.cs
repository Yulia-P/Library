using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Logbook.Commands;
using Logbook.ViewModels;
using Logbook.Views;
using Logbook.Models;
using System.ComponentModel;
using System.Windows.Controls;

namespace Logbook.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public LoginViewModel()
        {

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

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        private bool errorVisiblity;
        public bool ErrorVisibility
        {
            get { return errorVisiblity; }
            set
            {
                errorVisiblity = value;
                OnPropertyChanged("ErrorVisibility");
            }
        }

        private RelayCommand registerCommand;
        public RelayCommand RegisterCommand
        {
            get
            {
                return registerCommand ??
                    (registerCommand = new RelayCommand(obj =>
                    {
                        MainWindow mainWindow = App.Current.MainWindow as MainWindow;

                        RegisterView registerView = new RegisterView();
                        registerView.DataContext = new RegisterViewModel();

                        mainWindow.OutputView.Content = registerView;
                    }));
            }
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                (loginCommand = new RelayCommand(obj =>
                {
                    if(Login != null)
                    {
                        PasswordBox passwordBox = obj as PasswordBox;
                        string password = passwordBox.Password;

                        if (UsersRepository.ContainsUser(login))
                        {
                            User user = UsersRepository.GetUser(Login);

                            if (user != null)
                            {
                                string passwordHash = UsersRepository.HashPassword(password);
                                if (passwordHash == user.Password)
                                {
                                    UsersRepository.AddVisit(user);

                                    MainWindow mainWindow = App.Current.MainWindow as MainWindow;

                                    SideMenuView sideMenuView = new SideMenuView();
                                    sideMenuView.DataContext = new SideMenuViewModel(user);

                                    mainWindow.OutputView.Content = sideMenuView;

                                }
                            }
                        }
                    }
                    ErrorVisibility = true;
                }));
            }
        }
    }
}
