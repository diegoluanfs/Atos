using Report.Common.Entities;
using Report.Users.Entities;
using System;
using System.Text.RegularExpressions;
using System.Transactions;

namespace Report.Users
{
    public class UsersBusiness
    {
        const string RegexName = @"^[a-z ]+$";
        const string RegexEmail = @"/[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/gm";
        const string RegexPassword = @"/(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$/gm";

        public async Task<bool> ChangePassword(int idUser, ChangePasswordReq changePasswordReq)
        {
            try
            {
                #region verify fields
                //valida as informações do obj de entrada
                if (String.IsNullOrEmpty(changePasswordReq.OldPassword))
                {
                    //throw new BusinessExcepetion();
                }
                if (String.IsNullOrEmpty(changePasswordReq.NewPassword))
                {
                    //throw new BusinessExcepetion();
                }
                changePasswordReq.OldPassword = changePasswordReq.OldPassword.Trim();
                changePasswordReq.NewPassword = changePasswordReq.NewPassword.Trim();
                #endregion

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Verifica se usuário está correto
                    UsersRepository usersRepository = new UsersRepository();
                    var resp = await usersRepository.ChangePassword(idUser, changePasswordReq);

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
        public async Task<UpdateResp> Update(int idUser, UpdateReq updateReq)
        {
            try
            {
                #region verify fields
                //valida as informações do obj de entrada
                if (String.IsNullOrEmpty(updateReq.Name))
                {
                    //throw new BusinessExcepetion();
                }
                updateReq.Name = updateReq.Name.Trim();
                #endregion

                UpdateResp resp = new UpdateResp();

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Verifica se usuário está correto
                    UsersRepository usersRepository = new UsersRepository();
                    resp = await usersRepository.Update(idUser, updateReq);

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


        public async Task<bool> Create(CreateReq createReq)
        {
            bool resp;
            try
            {
                #region verify fields
                //valida as informações do obj de entrada
                if (String.IsNullOrEmpty(createReq.FullName))
                {
                    //throw new BusinessExcepetion();
                }

                bool verify = false;
                UsersRepository verifyLogin = new UsersRepository();
                verify = await verifyLogin.UserExists(createReq.Login);

                if(verify == true)
                {
                    throw new Exception("E-mail already registered!");
                }

                #endregion

                CreateReqInt createReqInt = new CreateReqInt();
                createReqInt.FullName = createReq.FullName;
                createReqInt.Document = createReq.Document;
                createReqInt.Email = createReq.Login;
                createReqInt.Created = DateTime.Now;
                createReqInt.Updated = DateTime.Now;
                createReqInt.CreatedBy = 1;
                createReqInt.UpdatedBy = 1;

                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //Verifica se usuário está correto
                    UsersRepository usersRepository = new UsersRepository();
                    resp = await usersRepository.Create(createReqInt);

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
                throw ex;
            }
        }


        public async Task<UpdateResp> Cancel()
        {
            try
            {

                #region default user verification
                //Check who is requesting

                //Check user permission

                #endregion

                #region verify fields

                #endregion

                UpdateResp resp = new UpdateResp();

                using (TransactionScope transactionScope = new TransactionScope())
                {

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

    }
}
