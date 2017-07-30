﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tag.Core.Validations;
using Xamarin.Forms;


namespace Tag.Core
{
    class AuthPageVM : ViewModelBase
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;
        private bool _isValid;
        private bool _isEnabled;
        private readonly IAuthenticationService _authService;

        public AuthPageVM(IAuthenticationService authenticationService)
        {
            _authService = authenticationService;
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            AddValidations();
            // #debug
            if (dilgSrv == null)
            {
                throw new Exception("dialog service is null");
            }
        }

        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }

        public ICommand AuthenticateViaTagCommand => new Command(TagAuthAsync);
        public ICommand AuthenticateViaGoogleCommand => new Command(GoogleAuthAsync);


        public ICommand SignInCommand => new Command(SignInAsync);

        public ICommand GoToSignUpCommand => new Command(GoToSignUp);

        public ICommand ValidateCommand
        {
            get { return new Command(() => Enable()); }
        }

        // methods
        private async void GoogleAuthAsync(object obj)
        {
            if (obj == null)
            {

            }
            await _authService.InitGoogleAuthenticator();
        }
        private async void TagAuthAsync()
        {
            // #test
            //await DialogService.ShowAlertAsync("tag auth", "tag", "ok");
            await NavigationService.NavigateToAsync<SignUpPageVM>();
        }
        private async void SignInAsync()
        {
            IsBusy = true;
            IsValid = true;
            bool isValid = Validate();
            bool isAuthenticated = false;

            if (isValid)
            {
                //try
                //{
                //    isAuthenticated = await _authenticationService.LoginAsync(UserName.Value, Password.Value);
                //}
                //catch (ServiceAuthenticationException)
                //{
                //    await DialogService.ShowAlertAsync("Invalid credentials", "Login failure", "Try again");
                //}
                //catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
                //{
                //    Debug.WriteLine($"[SignIn] Error signing in: {ex}");
                //    await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine($"[SignIn] Error signing in: {ex}");
                //}
            }
            else
            {
                IsValid = false;
            }

            if (isAuthenticated)
            {
                //await NavigationService.NavigateToAsync<MainViewModel>();
            }

            IsBusy = false;
        }

        private async void GoToSignUp()
        {
            //await NavigationService.NavigateToAsync<SignUpViewModel>();
        }

        private bool Validate()
        {
            bool isValidUser = _userName.Validate();
            bool isValidPassword = _password.Validate();

            return isValidUser && isValidPassword;
        }

        private void Enable()
        {
            IsEnabled = !string.IsNullOrEmpty(UserName.Value) &&
                !string.IsNullOrEmpty(Password.Value);
        }

        private void AddValidations()
        {
            //_userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username should not be empty" });
            //_password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password should not be empty" });
        }

        public override Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

    }
}
