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
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public RegisterViewModel()
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
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        private string passwordAgain;
        public string PasswordAgain
        {
            get { return passwordAgain; }
            set
            {
                passwordAgain = value;
                OnPropertyChanged("PasswordAgain");
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
                        if(Name != null)
                        {
                            if(Login != null)
                            {
                                if(Password != null && password == passwordAgain)
                                {
                                    if (!UsersRepository.ContainsUser(Login))
                                    {
                                        UsersRepository.AddUser(Login, Name, Password);
                                        User user = UsersRepository.GetUser(Login);

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

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                (loginCommand = new RelayCommand(obj =>
                {
                    MainWindow mainWindow = App.Current.MainWindow as MainWindow;

                    LoginView registerView = new LoginView();
                    registerView.DataContext = new LoginViewModel();

                    mainWindow.OutputView.Content = registerView;
                }));
            }
        }
    }
}
