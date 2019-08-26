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
        Desassociado = 3,
        Dissociado = 4
    }
}
