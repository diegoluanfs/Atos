using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Users.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Pendente")]
        PedingValidation = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Ativo")]
        Active = 2,

        /// <summary>
        /// 
        /// </summary>
        [Description("Inativo")]
        Inactive = 3,

        /// <summary>
        /// 
        /// </summary>
        [Description("Removido")]
        Deleted = 4


    }
}
