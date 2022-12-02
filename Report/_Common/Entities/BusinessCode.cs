using Report.Common.Entities;
using System.ComponentModel;

namespace Report.Common.Entities
{
    public enum BusinessCode
    {
        [Description("Descrição")]
        [CustomDescription("Inválido", "PT-BR")]
        [CustomDescription("Invalid", "EN-US")]
        Invalid = 1
    }
}
