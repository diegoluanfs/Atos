using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Auth.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public enum AuthStatus
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Aguardando Confirmação")]
        PedingValidation = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Inativo")]
        Inactive = 2,

        /// <summary>
        /// 
        /// </summary>
        [Description("Ativo")]
        Active = 3,

        /// <summary>
        /// 
        /// </summary>
        [Description("Bloqueado")]
        Blocked = 4


    }
}
