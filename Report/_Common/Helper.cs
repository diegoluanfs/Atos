using report.Common.Entities;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace report._Common
{
    public static class Helper
    {
        public static IConfiguration Configuration;


        public static string GetAnnotationCustom<T>(T obj)
        {
            var teste = obj.GetType().GetProperties();
            return null;
        }

        public static string GetEnumDescription(Enum value)
        {
            return
               value
                   .GetType()
                   .GetMember(value.ToString())
                   .FirstOrDefault()
                   ?.GetCustomAttribute<DescriptionAttribute>()
                   ?.Description;
        }

        public static string GetEnumDescription<T>(T value) where T : Enum
        {
            return
               value
                   .GetType()
                   .GetMember(value.ToString())
                   .FirstOrDefault()
                   ?.GetCustomAttribute<DescriptionAttribute>()
                   ?.Description;
        }

        public static List<string> GetProperty(object pObject)
        {
            List<string> propertyList = new List<string>();
            if (pObject != null)
            {
                foreach (var prop in pObject.GetType().GetProperties())
                {
                    propertyList.Add(prop.Name);
                }
            }
            return propertyList;
        }


        public static IList<object> ConvertEnumToList<T>() where T : struct
        {
            var enumItens = new List<object>();

            var values = Enum.GetValues(typeof(T));

            foreach (var value in values)
            {
                if ((int)value > 0)
                {
                    enumItens.Add(new
                    {
                        id = (int)value,
                        name = value.ToString()
                    });
                }
            }

            return enumItens;

        }


        public static string FormatErrorToJson(int errorNumber, string errorMessage)
        {
            return "{ \"error\" : \"" + errorNumber + "\", \"message\" : \"" + errorMessage + "\"}";
        }

        public static string FormatCharacterSpecial(string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "¹", "²", "³", "£", "¢", "¬", "º", "¨", "\"", "'", ".", ",", "-", ":", "(", ")", "ª", "|", "\\\\", "°", "_", "@", "#", "!", "$", "%", "&", "*", ";", "/", "<", ">", "?", "[", "]", "{", "}", "=", "+", "§", "´", "`", "^", "~" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }

            /** Troca os caracteres especiais da string por " " **/
            str = Regex.Replace(str, @"[^\w\.@-]", " ",
                                RegexOptions.None, TimeSpan.FromSeconds(1.5));

            return str.Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="parameterDirection"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public static bool IsBrazilTaxId(string taxId)
        {
            if (taxId.Length == 11)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                taxId = taxId.Trim();
                taxId = Regex.Replace(taxId, @"[^\d]", "");


                if (taxId.Length != 11)
                    return false;
                tempCpf = taxId.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return taxId.EndsWith(digito);
            }
            else if (taxId.Length > 11 && taxId.Length < 15)
            {

                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;
                taxId = taxId.Trim();
                taxId = taxId.Replace(".", "").Replace("-", "").Replace("/", "");
                if (taxId.Length != 14)
                    return false;
                tempCnpj = taxId.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return taxId.EndsWith(digito);
            }
            return false;
        }

        public static string ReadFile(string fileName)
        {
            string file = File.ReadAllText(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "/Resources/" + fileName);

            return file;
        }

        public static string ReadImageToBase64(string file)
        {
            try
            {
                string image = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "/" + (GetSettings("pathProductPic") + "/" + file).Replace("\\", "/");
                byte[] imageArray = File.ReadAllBytes(image);
                file = Convert.ToBase64String(imageArray);
            }
            catch
            {
                file = "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";
            }
            return file;

        }

        public static string ReadBytesToBase64(byte[] file)
        {
            string ret = "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";
            if (file != null && file.Length > 0)
            {
                ret = Convert.ToBase64String(file);
            }
            return ret;

        }

        public static string GetSettings(string name)
        {
            string ret = "";
            var obj = (ConfigurationSection)Configuration.GetSection(name);

            if (obj != null)
            {
                ret = obj.Value.ToString();
            }

            return ret;
        }



        public static IList<SqlParameter> CreateParameterProcedure(IList<SQLParameterCustom> mappedParam)
        {

            List<SqlParameter> paramList = new List<SqlParameter>();

            foreach (var item in mappedParam)
            {
                SqlParameter parameter = new SqlParameter();
                parameter.Direction = ParameterDirection.Input;
                parameter.ParameterName = item.ParameterName;

                if (item.ParameterValue == null)
                {
                    parameter.Value = DBNull.Value;
                }
                else if (item.ParameterValue.GetType() == typeof(int?))
                {
                    parameter.DbType = DbType.Int32;

                    parameter.Value = item.ParameterValue;
                }
                else if (item.ParameterValue.GetType() == typeof(int))
                {
                    parameter.DbType = DbType.Int32;
                    if (!item.AcceptZero)
                    {
                        parameter.Value = (int)item.ParameterValue > 0 ? item.ParameterValue : null;
                    }
                    else
                    {
                        parameter.Value = (int)item.ParameterValue;
                    }
                }
                else if (item.ParameterValue.GetType() == typeof(int[]))
                {
                    if (item.ParameterValue.ToString().Length > 0)
                    {
                        parameter.DbType = DbType.String;
                        parameter.Value = string.Join(',', (int[])item.ParameterValue);
                    }
                    else
                    {
                        parameter.DbType = DbType.String;
                        parameter.Value = DBNull.Value;
                    }
                }

                else if (item.ParameterValue.GetType() == typeof(bool?))
                {
                    parameter.DbType = DbType.Boolean;
                    parameter.Value = ((bool?)item.ParameterValue).HasValue ? item.ParameterValue : null;

                }
                else if (item.ParameterValue.GetType() == typeof(bool))
                {
                    parameter.DbType = DbType.Boolean;
                    parameter.Value = item.ParameterValue;

                }
                else if (item.ParameterValue.GetType() == typeof(DateTime?))
                {
                    parameter.DbType = DbType.DateTime;
                    parameter.Value = ((DateTime?)item.ParameterValue).HasValue ? item.ParameterValue : null;

                }
                else if (item.ParameterValue.GetType() == typeof(DateTime))
                {
                    parameter.DbType = DbType.DateTime;
                    parameter.Value = (DateTime)item.ParameterValue != DateTime.MinValue ? item.ParameterValue : null;

                }

                else if (item.ParameterValue.GetType() == typeof(string))
                {
                    if (item.ParameterValue.ToString().Length > 0)
                    {
                        parameter.DbType = DbType.String;
                        parameter.Value = item.ParameterValue;
                    }
                    else
                    {
                        parameter.DbType = DbType.String;
                        parameter.Value = DBNull.Value;

                    }

                }

                else if (item.ParameterValue.GetType() == typeof(decimal))
                {
                    parameter.DbType = DbType.Decimal;

                    if (!item.AcceptZero)
                    {

                        parameter.Value = (decimal)item.ParameterValue > 0 ? item.ParameterValue : null;
                    }
                    else
                    {

                        parameter.Value = (decimal)item.ParameterValue;

                    }
                }
                else if (item.ParameterValue.GetType() == typeof(long))
                {
                    parameter.DbType = DbType.Int64;
                    parameter.Value = (long)item.ParameterValue > 0 ? item.ParameterValue : null;

                }
                else if (item.ParameterValue.GetType() == typeof(byte[]))
                {
                    parameter.DbType = DbType.Binary;
                    parameter.Value = item.ParameterValue;

                }
                else
                {
                    parameter.Value = DBNull.Value;
                }

                paramList.Add(parameter);
            }

            return paramList;
        }
    }
}
