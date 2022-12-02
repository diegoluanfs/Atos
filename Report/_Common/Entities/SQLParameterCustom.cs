using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Common.Entities
{
    public class SQLParameterCustom
    {
        public SQLParameterCustom(string parameterName, object parameterValue, bool acceptZero = false)
        {
            ParameterName = parameterName;
            ParameterValue = parameterValue;
            AcceptZero = acceptZero;
        }


        public string ParameterName { get; set; }
        public object ParameterValue { get; set; }
        public bool AcceptZero { get; set; }

    }


}
