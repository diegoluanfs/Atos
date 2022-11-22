using report.Common.Entities;
using report.Users.Entities;
using System.Text.RegularExpressions;
using System.Transactions;

namespace report.Users
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
        //public async Task<SignInUserResp> Get()
        //{
        //    try
        //    {
        //        #region default user verification
        //        //Check who is requesting

        //        //Check user permission

        //        #endregion

        //        #region verify fields

        //        #endregion

        //        SignInUserResp signInUserResp = new SignInUserResp();

        //        using (TransactionScope transactionScope = new TransactionScope())
        //        {

        //            transactionScope.Complete();
        //        }
        //        return signInUserResp;
        //    }
        //    catch (BusinessException eb)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
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
