using Business.Interfaces;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ToDo_Presentation.ViewModels
{
    public class LoginViewVM : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private string _username;
        private string _password;
        private string _errorMessage;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewVM(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new RelayCommand(OnLogin);
            RegisterCommand = new RelayCommand(OnRegister);
        }

        private void OnLogin()
        {
            if (_userService.Login(Username, Password))
            {
                // Navigate to MainWindow
                var mainWindow = new MainWindow();
                mainWindow.Show();

                // Close the current login window
                App.Current.Windows[0]?.Close();
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }

        private void OnRegister()
        {
            if (_userService.Register(Username, Password))
            {
                ErrorMessage = "User registered successfully!";
            }
            else
            {
                ErrorMessage = "Username already exists.";
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
