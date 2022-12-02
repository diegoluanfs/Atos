using Report._Common;
using Report._Common.Entities;
using Report.Auth.Entities;
using Report.Common.Entities;
using Report.Users;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;

namespace Report.Auth
{
    public class AuthBusiness
    {
        const string RegexName = @"^[a-z ]+$";
        const string RegexEmail = @"/[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/gm";
        const string RegexPassword = @"/(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$/gm";

        private IKeyManager _keyManager;

        public AuthBusiness(IKeyManager keyManager)
        {
            _keyManager = keyManager;
        }

        public AuthBusiness()
        {

        }

        public async Task<SignUpResp> SignUp(SignUpReq signUpReq)
        {
            try
            {
                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields
                if (String.IsNullOrEmpty(signUpReq.Name))
                {
                    //throw new BusinessExcepetion();
                }
                if (String.IsNullOrEmpty(signUpReq.Email))
                {
                    //throw new BusinessExcepetion();
                }
                if (String.IsNullOrEmpty(signUpReq.Password))
                {
                    //throw new BusinessExcepetion();
                }
                signUpReq.Name = signUpReq.Name.Trim();
                signUpReq.Email = signUpReq.Email.Trim();
                signUpReq.Password = signUpReq.Password.Trim();

                //if (!Regex.IsMatch(signUpReq.Name, RegexName))
                //{
                //    throw new BusinessException(BusinessCode.Invalid);
                //}
                //if (!Regex.IsMatch(signUpReq.Email, RegexEmail))
                //{
                //    //throw new BusinessException();
                //}
                //if (!Regex.IsMatch(signUpReq.Password, RegexPassword))
                //{
                //    //throw new BusinessExcepetion();
                //}
                #endregion

                AuthCreate userCreate = new AuthCreate();
                userCreate.Name = signUpReq.Name;
                userCreate.Email = signUpReq.Email;

                //Encrypted password
                userCreate.Password = signUpReq.Password;
                userCreate.Created = DateTime.Now;
                userCreate.CreatedBy = 1;
                userCreate.FkStatus = AuthStatus.PedingValidation;
                userCreate.IdLanguage = 1;

                //Generate a Access Token
                userCreate.Token = "Token";

                SignUpResp signUpResp = new SignUpResp();

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Check if user exists
                    AuthRepository usersRepository = new AuthRepository();
                    signUpResp = await usersRepository.SignUp(userCreate);
                    //Register Auth and Create Authorization Key

                    transactionScope.Complete();
                }

                return signUpResp;
            }
            catch (BusinessException eb)
            {
                throw eb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> SignIn(SignInReq signInReq, string _remoteIP)
        {
            try
            {

                #region verify fields
                //validates input information
                if (String.IsNullOrEmpty(signInReq.Email) || String.IsNullOrEmpty(signInReq.Password))
                {
                    //throw new BusinessException();
                }

                signInReq.Email = signInReq.Email.Trim();
                signInReq.Password = signInReq.Password.Trim();

                if (!Regex.IsMatch(signInReq.Email, RegexEmail))
                {
                    //throw new BusinessExcepetion();
                }
                if (!Regex.IsMatch(signInReq.Password, RegexPassword))
                {
                    //throw new BusinessExcepetion();
                }
                #endregion

                SignInResp signInResp = new SignInResp();


                AuthRepository usersRepository = new AuthRepository();

                Login login = await usersRepository.GetLogin(signInReq.Email);

                if (!String.IsNullOrEmpty(login.Password))
                {
                    //throw new BusinessExcepetion();
                }

                if (!VerifyMd5Hash(MD5.Create(), login.Password, signInReq.Password))
                {
                    //   throw new SecurityException(BusinessExceptionCode.InvalidUserGeneric);

                }

                var key = new byte[64];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                string apiKey = Convert.ToBase64String(key);

                DateTime dtExpire = DateTime.Now.AddHours(8);
                DateTime dtCreated = DateTime.Now;

                AuthRepository userKey = new AuthRepository();
                bool respKey = await userKey.SaveKey(signInReq, login, apiKey, dtExpire, dtCreated, _remoteIP);

                if (!respKey)
                {
                    //throw new BusinessExcepetion();
                }

                KeyPass keyPass = new KeyPass();
                keyPass.Key = apiKey;
                keyPass.Hash = login.Hash;
                keyPass.IdUser = login.Id;
                keyPass.Expire = dtExpire;
                keyPass.Created = dtCreated;
                keyPass.Status = 1;

                if (_keyManager.KeyList.Count > 0)
                {
                    KeyPass? keyPassOld = _keyManager.KeyList.Where(x => x.IdUser == login.Id).FirstOrDefault();
                    if (keyPassOld != null)
                    {
                        _keyManager.KeyList.Remove(keyPassOld);
                    }
                }

                _keyManager.KeyList.Add(keyPass);

                return apiKey;
            }
            catch (BusinessException eb)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ForgotPassword(ForgotPasswordReq forgotPasswordReq)
        {
            try
            {
                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields
                //valida as informações do obj de entrada
                if (String.IsNullOrEmpty(forgotPasswordReq.Email))
                {
                    //throw new BusinessExcepetion();
                }
                forgotPasswordReq.Email = forgotPasswordReq.Email.Trim();

                if (!Regex.IsMatch(forgotPasswordReq.Email, RegexEmail))
                {
                    //throw new BusinessExcepetion();
                }
                #endregion
                bool resp;

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Verifica se usuário está correto
                    AuthRepository usersRepository = new AuthRepository();
                    resp = await usersRepository.ForgotPassword(forgotPasswordReq);

                    if (resp == true)
                    {
                        // Trigger forgot password email

                    }
                    transactionScope.Complete();
                }
                return resp;
            }
            catch (BusinessException eb)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Activate(ActivateReq activateReq)
        {
            try
            {
                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields
                //valida as informações do obj de entrada
                if (String.IsNullOrEmpty(activateReq.Code))
                {
                    //throw new BusinessExcepetion();
                }
                activateReq.Code = activateReq.Code.Trim();
                #endregion

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Ativa o usuário
                    AuthRepository usersRepository = new AuthRepository();
                    var resp = usersRepository.Activate(activateReq);

                    transactionScope.Complete();
                }
                return true;
            }
            catch (BusinessException eb)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {

                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields

                #endregion


                using (TransactionScope transactionScope = new TransactionScope())
                {

                    transactionScope.Complete();
                }

                return true;
            }
            catch (BusinessException eb)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
