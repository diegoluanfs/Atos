using Report.Common.Entities;
using Report.Users.Databases;
using Report.Users.Entities;
using System.Data;

namespace Report.Users
{
    public class UsersRepository : BaseRepository
    {
        public async Task<bool> UserExists(string login)
        {
            try
            {
                Database dt = new Database();
                DataSet ds = await dt.ExecuteProcedureDataSet(DataProcedures.SPZIP_RT_LOGIN_EXISTS);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public async Task<bool> UserCreate(SignUpReq signUpReq)
        //{
        //    try
        //    {
        //        CustomParams = new List<SQLParameterCustom>();

        //        //CustomParams.Add(new SQLParameterCustom(DataParams.SETTINGS_NAME, signUpReq.Name));
        //        //CustomParams.Add(new SQLParameterCustom(DataParams.USER_LOGIN, signUpReq.Email));
        //        //CustomParams.Add(new SQLParameterCustom(DataParams.USER_PASS, signUpReq.Password));

        //        Database dt = new Database();
        //        Params = dt.CreateParameter(CustomParams);

        //        DataSet ds = await dt.ExecuteProcedureDataSet(DataProcedures.SPZIP_CR_USER);

        //        SignUpResp signUpResps = new SignUpResp();

        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                //signUpResps.Authorization = DbParse<string>(row, /*authorization*/);
        //                signUpResps.User = new SignUpUserResp();
        //                //signUpResps.User.Id = DbParse<Guid>(row, /*user*/);
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<bool> ChangePassword(int idUser, ChangePasswordReq changePasswordReq)
        {
            try
            {
                bool resp = false;

                CustomParams = new List<SQLParameterCustom>();

                CustomParams.Add(new SQLParameterCustom(UsersDataParams.CHANGE_PASSWORD_ID_USER, idUser));
                CustomParams.Add(new SQLParameterCustom(UsersDataParams.CHANGE_PASSWORD_TX_NEW_PASSWORD, changePasswordReq.NewPassword));
                CustomParams.Add(new SQLParameterCustom(UsersDataParams.CHANGE_PASSWORD_TX_NEW_PASSWORD, changePasswordReq.NewPassword));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(UsersDataProcedures.SPRPT_CHANGE_PASSWORD, Params);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    resp = true;
                }

                return resp;
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
                CustomParams = new List<SQLParameterCustom>();

                CustomParams.Add(new SQLParameterCustom(UsersDataParams.UPDATE_ID_USER, idUser));
                CustomParams.Add(new SQLParameterCustom(UsersDataParams.UPDATE_TX_NAME, updateReq.Name));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(UsersDataProcedures.SPRPT_UPDATE, Params);

                UpdateResp updateResp = new UpdateResp();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //signUpResps.Authorization = DbParse<string>(row, /*authorization*/);
                        //updateResp.User = new UpdateResp();
                        //updateResp.User.Id = DbParse<Guid>(row, UsersDataColumns.USER_SIGN_UP_GUID);
                    }
                }

                return updateResp;
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
        //        //valida as informações do obj de entrada

        //        //Verifica se usuário existe

        //        //var resp = usersRepository.SignUp(SignUpReq);

        //        //Registrar usuário

        //        //Criar chave de autorização

        //        SignInUserResp signInUserResp = new SignInUserResp();
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
                //Pega as informações passadas pelo header, com informações do usuário

                //var resp = usersRepository.SignUp(SignUpReq);

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
