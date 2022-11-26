using report._Common;
using report._Common.Entities;
using report.Map.Entities;
using report.Common.Entities;
using report.Users;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using Report.Map.Entities;
using report.Auth.Entities;

namespace report.Map
{
    public class MapBusiness
    {
        const string RegexName = @"^[a-z ]+$";
        const string RegexEmail = @"/[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/gm";
        const string RegexPassword = @"/(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$/gm";

        private IKeyManager _keyManager;

        public MapBusiness(IKeyManager keyManager)
        {
            _keyManager = keyManager;
        }

        public MapBusiness()
        {

        }

        public async Task<bool> Create(string latitude, string longitude)
        {
            try
            {
                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields

                #endregion

                MapCreate mapCreate = new MapCreate();
                mapCreate.Created = DateTime.Now;

                bool resp = false;

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Check if user exists
                    MapRepository mapsRepository = new MapRepository();
                    resp = await mapsRepository.Create(mapCreate);
                    //Register Map and Create Maporization Key

                    transactionScope.Complete();
                }

                return resp;
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
        public async Task<string> Search(string _remoteIP)
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
                    //Check if user exists
                    MapRepository mapsRepository = new MapRepository();
                    IList<Occurrence> occurrences = await mapsRepository.Search();
                    //Register Map and Create Maporization Key

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
                    MapRepository usersRepository = new MapRepository();
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
                    MapRepository usersRepository = new MapRepository();
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
