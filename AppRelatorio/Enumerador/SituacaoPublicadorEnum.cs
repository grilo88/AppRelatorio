using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AppRelatorio.Enumerador
{
    public enum SituacaoPublicadorEnum : byte
    {
        Ativo = 1,
        Inativo = 2,
        Dissociado = 3,
        Desassociado = 4,
    }
}
