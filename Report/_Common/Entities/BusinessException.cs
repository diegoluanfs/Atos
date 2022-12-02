using System.ComponentModel;
using System.Reflection;

namespace Report.Common.Entities
{
    public class BusinessException : Exception
    {
        private string _message;

        public override string Message
        {
            get { return _message; }
        }

        public string? GetEnumDescription(BusinessCode value, int dic)
        {
            return
               value
                   .GetType()
                   .GetMember(value.ToString())
                   [dic]
                   ?.GetCustomAttribute<DescriptionAttribute>()
                   ?.Description;
        }

        public BusinessException(BusinessCode enumExceptionCode)
        {
            _message = GetEnumDescription(enumExceptionCode, 0) ?? "Unknown";
        }
    }
}
