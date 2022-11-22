using System.ComponentModel;

namespace report.Common.Entities
{
    [AttributeUsage(AttributeTargets.Field,
                       AllowMultiple = true)  // multiuse attribute  
]
    public class CustomDescription : Attribute
    {
        private string info;
        private string lang;

        public CustomDescription(string info, string lang)
        {
            this.info = info;
            this.lang = lang;
        }
    }
}
