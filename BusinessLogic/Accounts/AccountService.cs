using Core.Accounts;
using Persistence.RepositoryInterfaces;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace BusinessLogic.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public CreateAccountResultDto CreateAccount(CreateAccountDto accountInfo)
        {
            var account = _accountRepository.GetAccountOrDefaultByEmail(accountInfo.Email);

            // if the account already exists, a new one should not be added
            if(account != null)
            {
                return new CreateAccountResultDto()
                {
                    ResultType = AccountCreationResultTypeEnum.AlreadyExists,
                    LoginData = null
                };
            }

            if(!ValidatePassword(accountInfo.Password))
            {
                return new CreateAccountResultDto()
                {
                    ResultType = AccountCreationResultTypeEnum.PasswordNotCompliant,
                    LoginData = null
                };
            }

            // if the account doesn't exist, create it!
            _accountRepository.AddAccount(new AccountDto()
            {
                Email = accountInfo.Email,
                PasswordHash = HashPassword(accountInfo.Password)
            });

            // now log them in
            return new CreateAccountResultDto()
            {
                ResultType = AccountCreationResultTypeEnum.Success,
                LoginData = Login(new LoginAttemptDto()
                {
                    Email = accountInfo.Email,
                    Password = accountInfo.Password
                })
            };
        }

        public LoginResultDto Login(LoginAttemptDto loginInfo)
        {
            var account = _accountRepository.GetAccountOrDefaultByEmail(loginInfo.Email);

            if(account != null)
            {
                // check to make sure the password matches the stored hash
                if(CheckPassword(loginInfo.Password, account.PasswordHash))
                {
                    return new LoginResultDto()
                    {
                        Email = loginInfo.Email,
                        ResultType = LoginResultTypeEnum.Success,

                        // TO DO: Generate a real token
                        Token = "token123"
                    };
                }
            }

            // if we get here, login failed!
            return new LoginResultDto()
            {
                ResultType = LoginResultTypeEnum.Failure
            };
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        // quick password hashing algorithm for this code sample taken from https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
        private string HashPassword(string rawPassword)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(rawPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        private bool CheckPassword(string password, string passwordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(passwordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidatePassword(string password)
        { 
            // check to make sure it is not null so it doesn't bomb on the actual checks
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length >= 8 
                && password.Any(c => char.IsDigit(c))
                && password.Any(c => char.IsLetter(c))
                )
            {
                return true;
            }

            return false;
        }
    }
}
