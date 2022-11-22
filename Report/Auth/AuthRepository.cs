using report.Auth.Databases;
using report.Auth.Entities;
using report.Common.Entities;
using System.Data;

namespace report.Auth
{
    public class AuthRepository : BaseRepository
    {
        public async Task<bool> AuthExists(string login)
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
        public async Task<SignUpResp> SignUp(AuthCreate userCreate)
        {
            try
            {
                CustomParams = new List<SQLParameterCustom>();

                CustomParams.Add(new SQLParameterCustom(AuthDataParams.NAME, userCreate.Name));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.EMAIL, userCreate.Email));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.PASSWORD, userCreate.Password));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.ID_LANGUAGE, userCreate.IdLanguage));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.CREATED, userCreate.Created));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.CREATED_BY, userCreate.CreatedBy));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.ID_STATUS, (int)userCreate.FkStatus));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.TOKEN, userCreate.Token));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(AuthDataProcedures.SPRPT_SIGN_UP, Params);

                SignUpResp signUpResps = new SignUpResp();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //signUpResps.Authorization = DbParse<string>(row, /*authorization*/);
                        signUpResps.Auth = new SignUpAuthResp();
                        signUpResps.Auth.Id = DbParse<Guid>(row, AuthDataColumns.USER_SIGN_UP_GUID);
                    }
                }

                return signUpResps;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<SignInResp> SignIn(SignInReq signInReq)
        {
            try
            {
                CustomParams = new List<SQLParameterCustom>();

                CustomParams.Add(new SQLParameterCustom(AuthDataParams.EMAIL, signInReq.Email));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.NAME, signInReq.Password));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(AuthDataProcedures.SPRPT_RT_LOGIN, Params);

                SignInResp signInResps = new SignInResp();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        //signUpResps.Authorization = DbParse<string>(row, /*authorization*/);
                        signInResps.Auth = new SignInAuthResp();
                        //signInResps.Auth.Id = DbParse<Guid>(row, AuthDataColumns.USER_SIGN_UP_GUID);
                    }
                }

                return signInResps;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SaveKey(SignInReq signInReq, Login login, string key, DateTime dtExpire, DateTime dtCreated, string _remoteIP)
        {
            try
            {
                CustomParams = new List<SQLParameterCustom>();

                CustomParams.Add(new SQLParameterCustom(AuthDataParams.CREATED, dtCreated));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.FK_USER, login.Id));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.HASH, login.Hash));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.KEY, key));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.EXPIRE, dtExpire));
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.IP, _remoteIP));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                var resp = await dt.ExecuteProcedureParams(AuthDataProcedures.SPRPT_CR_KEY_ACCESS, Params);

                return resp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> ForgotPassword(ForgotPasswordReq forgotPasswordReq)
        {
            try
            {
                bool resp = false;

                CustomParams = new List<SQLParameterCustom>();

                CustomParams.Add(new SQLParameterCustom(AuthDataParams.EMAIL, forgotPasswordReq.Email));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(AuthDataProcedures.SPRPT_FORGOT_PASSWORD, Params);

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

        public async Task<bool> Activate(ActivateReq activateReq)
        {
            try
            {
                bool resp = false;

                CustomParams = new List<SQLParameterCustom>();
                CustomParams.Add(new SQLParameterCustom(AuthDataParams.CODE, activateReq.Code));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(AuthDataProcedures.SPRPT_ACTIVATE, Params);

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

        public async Task<Login> GetLogin(string email)
        {
            try
            {

                CustomParams = new List<SQLParameterCustom>();

                CustomParams.Add(new SQLParameterCustom(AuthDataParams.LOGIN, email));

                Database dt = new Database();

                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(AuthDataProcedures.SPRPT_RT_LOGIN, Params);

                Login login = new Login();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        login.Id = Int32.Parse(row["ID"].ToString());
                        login.Hash = row["ID_HASH"].ToString();
                        login.Password = row["PASS"].ToString();
                    }
                }

                return login;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}

