using System;
using System.Collections.Generic;
using System.Text;

namespace AppRelatorio.Banco
{
    public interface ICaminho
    {
        string ObterCaminho(string arquivo);
    }
}
