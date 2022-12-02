using Report._Common.Entities;
using Report.Auth;
using Report.Auth.Databases;
using Report.Auth.Entities;
using Report.Common.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Data;
using System.Data.SqlClient;

namespace Report._Common
{
    public class KeyManager : IKeyManager
    {

        internal IList<SqlParameter> Params { get; set; }
        internal IList<SQLParameterCustom> CustomParams { get; set; }
        public IList<KeyPass> KeyList { get; set; }

        const string SPRPT_RT_VALIDATE_KEY = "SPRPT_RT_VALIDATE_KEY";
        const string SIGN_IN_TX_KEY = "@TX_KEY";
        const string SIGN_IN_DT_EXPIRE = "@DT_EXPIRE";

        public KeyManager()
        {
            KeyList = new List<KeyPass>();
        }

        public async Task<int> ValidateKey(string key)
        {
            try
            {
                if (KeyList.Count == 0)
                {
                    throw new SecurityException();
                }

                KeyPass? keyPassOld = KeyList.Where(x => x.Key == key).FirstOrDefault();
                if(keyPassOld == null || keyPassOld.Expire <= DateTime.Now)
                {
                    throw new SecurityException();
                }

                return keyPassOld.IdUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<int> ValidateKeyDB(string key)
        {
            try
            {
                CustomParams = new List<SQLParameterCustom>();

                CustomParams.Add(new SQLParameterCustom(SIGN_IN_TX_KEY, key));
                CustomParams.Add(new SQLParameterCustom(SIGN_IN_DT_EXPIRE, DateTime.Now));

                Database dt = new Database();
                Params = dt.CreateParameter(CustomParams);

                DataSet ds = await dt.ExecuteProcedureParamsDataSet(SPRPT_RT_VALIDATE_KEY, Params);

                int resp = 0;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        resp = Int32.Parse(row["FK_USER"].ToString());
                    }
                }

                if (resp == 0)
                {
                    throw new SecurityException();
                }

                return resp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
